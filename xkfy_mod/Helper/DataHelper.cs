using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Entity;
using xkfy_mod.Utils; 

namespace xkfy_mod.Helper
{
    public static class DataHelper
    {
        static DataHelper()
        {
            DictModFiles = new Dictionary<string, string>();
            FormConfig = new Dictionary<string, MyConfig>();
            ToolColumnConfig = new Dictionary<string, IList<TableExplain>>();
            DropDownListDict = new Dictionary<string, Dictionary<string, string>>();
            XkfyData = new DataSet();
            DdlDataSet = new DataSet();
            DictImages = new Dictionary<string, string>();
        }

        /// <summary>
        /// Map
        /// </summary>
        public static string MapConfigFileName => "Map";

        /// <summary>
        /// NpcConduct
        /// </summary>
        public static string NpcConfigFileName => "NpcConduct";


        /// <summary>
        /// 文件路径
        /// </summary>
        public static string ModFoderPath = string.Empty;

        /// <summary>
        /// 地图文件路径
        /// </summary>
        public static string MapFoderPath = string.Empty;

        /// <summary>
        /// 工具保存的XML文件路径
        /// </summary>
        public static string ToolFilesPath = string.Empty;

        /// <summary>
        /// 读取文件的错误信息
        /// </summary>
        public static StringBuilder ReadError = new StringBuilder();

        /// <summary>
        /// 存放所有Text数据转化的DataTable
        /// </summary>
        public static DataSet XkfyData { get; set; }
          
        /// <summary>
        /// 存储所有的MOD文件的文件路径,key是带txt后缀的文件名,value是全路径
        /// </summary>
        public static Dictionary<string, string> DictModFiles { get; set; }

        /// <summary>
        /// 文件配置信息,包括是否分为主表,是否开启缓存等信息 key是不带txt后缀的文件名
        /// </summary>
        public static Dictionary<string, MyConfig> FormConfig { get; set; }

        /// <summary>
        /// 列的配置信息,解释信息,宽度,是否作为查询列等 key是不带txt后缀的文件名
        /// </summary>
        public static Dictionary<string, IList<TableExplain>> ToolColumnConfig { get; set; }

        /// <summary>
        /// 存储所有下拉框数据源
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> DropDownListDict { get; set; }

        /// <summary>
        /// DropDownListDataSet 存储复杂下拉框数据源,比如联动下拉框
        /// </summary>
        public static DataSet DdlDataSet { get; set; }

        /// <summary>
        /// 存放所有图片路径
        /// </summary>
        public static Dictionary<string, string> DictImages { get; set; }

        /// <summary>
        /// 存储解释配置数字需要用到的数据
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> ExplainConfig = new Dictionary<string, Dictionary<string, string>>();

        public static void AddData(Form form, string tbName)
        {
            DataRow dr = XkfyData.Tables[tbName].NewRow();
            SetDataRowByCtrl(form, dr);

            if (string.IsNullOrEmpty(dr["rowState"].ToString()))
                dr["rowState"] = "1";
            XkfyData.Tables[tbName].Rows.InsertAt(dr, 0);
            form.Close();
        }

        public static void UpdateData(Form form, DataGridViewRow dr)
        {
            SetDataRowByCtrl(form, dr);
            if (string.IsNullOrEmpty(dr.Cells["rowState"].Value.ToString()))
                dr.Cells["rowState"].Value = "0";
            dr.DataGridView.CurrentCell = null;
            form.Close();
        }

        public static void SaveData(string tbName,IList<TableExplain> list)
        { 
            if (ToolColumnConfig.ContainsKey(tbName))
            {
                ToolColumnConfig.Remove(tbName);
                ToolColumnConfig.Add(tbName, list);
            }
            FileHelper.SaveColumnData(list, tbName);
        }

        #region 绑定数据到下拉框
        /// <summary>
        /// 绑定数据到下拉框
        /// </summary>
        /// <param name="cb">下拉对象</param>
        /// <param name="key">DropDownListData.Key</param>
        public static void BinderComboBox(ComboBox cb, string key)
        {
            if (!DropDownListDict.ContainsKey(key))
                return;
            BindingSource bs = new BindingSource {DataSource = DropDownListDict[key]};
            cb.DataSource = bs;
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }

        /// <summary>
        /// 绑定数据到下拉框
        /// </summary>
        /// <param name="cb">下拉框对象</param>
        /// <param name="dic">数据源</param>
        public static void BinderComboBox(ComboBox cb, IDictionary<string,string> dic)
        {
            BindingSource bs = new BindingSource {DataSource = dic};
            cb.DataSource = bs;
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }

        /// <summary>
        /// 绑定数据到下拉框
        /// </summary>
        /// <param name="cb">下拉框对象</param>
        /// <param name="list">数据源</param>
        public static void BinderComboBox<T>(ComboBox cb, IList<T> list)
        {
            BindingSource bs = new BindingSource { DataSource = list };
            cb.DataSource = bs;
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
            cb.DataSource = bs;
        }

        /// <summary>
        /// 武器招式类型
        /// </summary>
        /// <returns></returns>
        public static void BindiWearAmsType(ComboBox cb)
        {
            IList<DicConfig> list = FileUtils.ReadConfig<DicConfig>(PathHelper.PubWearAmspath);
            BinderComboBox(cb, list);
        }
        #endregion 

        #region 将窗体上的控件赋值给DataRow
        /// <summary>
        /// 将窗体上的控件赋值给DataRow
        /// </summary>
        /// <param name="basicControl">窗体</param>
        /// <param name="dr">要接受值的DataRow</param>
        public static void SetDataRowByCtrl(Control basicControl,DataRow dr)
        {
            foreach (Control control in basicControl.Controls)
            {
                if (control.Tag == null || control is Label)
                {
                    continue;
                }

                string rowName = control.Tag.ToString();
                if (!dr.Table.Columns.Contains(rowName) && rowName != "default") continue;　

                if (control is TextBox)
                {
                    dr[rowName] = ((TextBox)control).Text;
                }
                else if (control is ComboBox)
                {
                    //判断下拉框样式，如果是可选可输入，绑定文本框
                    ComboBox com = ((ComboBox)control);
                    if (com.DropDownStyle == ComboBoxStyle.DropDown)
                    {
                        dr[rowName] = com.Text;
                    }
                    else
                    {
                        //如果是只能选择，绑定下拉框的数据源里选中项
                        dr[rowName] = com.SelectedValue;
                    }
                }
                else if (control is RadioButton)
                {
                    if (((RadioButton)control).Checked)
                    {
                        dr[rowName] = StringUtils.GetCnLength(((RadioButton)control).Text);
                    }
                }
                else if (control is Panel || control is GroupBox)
                {
                    SetDataRowByCtrl(control, dr);
                }
            }
        }

        /// <summary>
        /// 将窗体上控件的值转换为DataGridViewRow对象
        /// </summary>
        /// <param name="basicControl"></param>
        /// <param name="dr"></param>
        public static void SetDataRowByCtrl(Control basicControl, DataGridViewRow dr)
        {
            foreach (Control control in basicControl.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }

                string rowName = control.Tag.ToString();
                if (!dr.DataGridView.Columns.Contains(rowName) && rowName != "default")
                {
                    continue;
                }

                if (control is TextBox)
                {
                    dr.Cells[rowName].Value = ((TextBox)control).Text;
                }
                else if (control is ComboBox)
                {
                    //判断下拉框样式，如果是可选可输入，绑定文本框
                    ComboBox com = ((ComboBox)control);
                    if (com.DropDownStyle == ComboBoxStyle.DropDown)
                    {　
                        dr.Cells[rowName].Value = com.Text;
                    }
                    else
                    {
                        //如果是只能选择，绑定下拉框的数据源里选中项
                        dr.Cells[rowName].Value = com.SelectedValue;
                    }
                }
                else if (control is RadioButton)
                {
                    if (((RadioButton)control).Checked)
                    {
                        dr.Cells[rowName].Value = StringUtils.GetRdoValue(((RadioButton)control).Text);
                    }
                }
                else if (control is Panel || control is GroupBox)
                {
                    SetDataRowByCtrl(control, dr);
                }
            }
        }
        #endregion

        #region 将dataRow的值赋给窗体控件
        /// <summary>
        /// 将DataRow对象的值赋给窗体控件
        /// </summary>
        /// <param name="baseControl"></param> 
        /// <param name="dr"></param>
        public static void SetCtrlByDataRow(Control baseControl,DataRow dr)
        {
            foreach (Control control in baseControl.Controls)
            {
                if (control.Tag == null || control is Label)
                {
                    continue;
                }

                string rowName = control.Tag.ToString();
                if (!dr.Table.Columns.Contains(rowName) && rowName != "default")
                {
                    continue;
                }
                 
                string value = "0";
                if (rowName != "default")
                {
                    value = dr[rowName].ToString();
                }

                if (control is TextBox)
                {
                    ((TextBox)control).Text = value;
                }
                else if (control is ComboBox)
                {
                    //判断下拉框样式，如果是可选可输入，绑定文本框
                    ComboBox com = ((ComboBox)control);
                    if (com.DropDownStyle == ComboBoxStyle.DropDown)
                    {
                        com.Text = value;
                    }
                    else 
                    {
                        //如果是只能选择，绑定下拉框的数据源里选中项
                        com.SelectedValue = value;
                    }
                }
                else if (control is RadioButton)
                {
                    if (((RadioButton)control).Text.IndexOf(value, StringComparison.Ordinal) == -1)
                    {
                        continue;
                    }
                    ((RadioButton)control).Checked = true;
                }
                else if (control is Panel || control is GroupBox)
                {
                    SetCtrlByDataRow(control, dr);
                }
            }
        }

        /// <summary>
        /// 将DataGridViewRow对象的值赋给窗体控件
        /// </summary>
        /// <param name="baseControl"></param> 
        /// <param name="dr"></param>
        public static void SetCtrlByDataRow(Control baseControl,DataGridViewRow dr)
        {
            foreach (Control control in baseControl.Controls)
            {
                if (control.Tag == null || control is Label)
                {
                    continue;
                }

                string rowName = control.Tag.ToString();
                if (!dr.DataGridView.Columns.Contains(rowName) && rowName != "default")
                {
                    continue;
                }

                string value = "0";
                if (rowName != "default")
                {
                    value = dr.Cells[rowName].Value.ToString();
                } 

                if (control is TextBox)
                {
                    ((TextBox)control).Text = value;
                }
                else if (control is ComboBox)
                {
                    //判断下拉框样式，如果是可选可输入，绑定文本框
                    ComboBox com = ((ComboBox)control);
                    if (com.DropDownStyle == ComboBoxStyle.DropDown)
                    {
                        com.Text = value;
                    }
                    else
                    {
                        //如果是只能选择，绑定下拉框的数据源里选中项
                        com.SelectedValue = value;
                    }
                }
                else if (control is RadioButton)
                {
                    if (((RadioButton)control).Text.IndexOf(value, StringComparison.Ordinal) == -1)
                    {
                        continue;
                    }
                    ((RadioButton)control).Checked = true;
                }
                else if (control is Panel || control is GroupBox)
                {
                    SetCtrlByDataRow(control, dr);
                }
            }
        }
        #endregion 

        #region 返回Name等中文名字
        /// <summary>
        /// 返回Name等中文名字
        /// </summary>
        /// <param name="tbName">DataTable的名称</param>
        /// <param name="rtRow">返回的列名</param>
        /// <param name="selRow">查询的列名</param>
        /// <param name="iId">条件</param>
        /// <returns></returns>
        public static string GetValue(string tbName,string rtRow,string selRow, string iId)
        {
            FileHelper.LoadTable(tbName);
            DataRow[] dr = XkfyData.Tables[tbName].Select($"{selRow}='{iId}'");
            if (dr.Length > 1)
                return string.Format("{0}.txt 文件中{2}【{1}】出现了多次", tbName, iId, selRow);
            if (dr.Length == 0)
                return string.Format("{0}.txt 文件中{2}【{1}】未找到", tbName, iId, selRow);
            
            return dr[0][rtRow].ToString();
        }

        /// <summary>
        /// 返回Name等中文名字
        /// </summary> 
        /// <param name="gvr">查询条件实体对象</param>
        /// <returns></returns>
        public static string GetValue(GetValueRows gvr)
        {
            FileHelper.LoadTable(gvr.TableName);
            DataRow[] dr = XkfyData.Tables[gvr.TableName].Select($"{gvr.SelectRow}='{gvr.Id}'");
            if (dr.Length > 1)
                return string.Format("{0}.txt 文件中{2}【{1}】出现了多次", gvr.TableName, gvr.Id, gvr.SelectRow);
            if (dr.Length == 0)
                return string.Format("{0}.txt 文件中{2}【{1}】未找到", gvr.TableName, gvr.Id, gvr.SelectRow);

            return dr[0][gvr.ReturntRow].ToString();
        }

        //返回对话专用
        public static string GetMapTalk(string iId, string iOrder)
        {
            string tbName = "MapTalkManager";
            FileHelper.LoadTable(tbName);
            DataRow[] dr = XkfyData.Tables[tbName].Select($"iOrder='{iOrder}' and sGroupID ='{iId}'");
            if (dr.Length > 1)
                return string.Format("{0}.txt 文件中{2}【{1}】出现了多次", tbName, iId, iOrder);
            if (dr.Length == 0)
                return string.Format("{0}.txt 文件中{2}【{1}】未找到", tbName, iId, iOrder); 

            string npcName = GetValue("NpcData", "sNpcName", "iID", dr[0]["iNpcID"].ToString());
            
            return npcName + ":" + dr[0]["sManager"];
        }
        #endregion

        /// <summary>
        /// 读取联动信息的配置文件
        /// </summary>
        public static void ReadConfig()
        {
            //读取下拉框联动信息
            IList<SelectItem> list = FileUtils.ReadConfig<SelectItem>(PathHelper.TalkManageriGtype);
            DataTable dt = new DataTable {TableName = "TalkManager.iGtype" };
            dt.Columns.Add("Type");
            dt.Columns.Add("ChildType");
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");
            
            foreach (SelectItem dc in list)
            {
                DataRow dr = dt.NewRow();
                dr["Type"] = dc.Type;
                dr["ChildType"] = dc.ChildType;
                dr["Text"] = dc.Text;
                dr["Value"] = dc.Value;
                dt.Rows.Add(dr);
            } 
            DdlDataSet.Tables.Add(dt);
        }

        #region 设置字典的值
        /// <summary>
        /// 设置字典的值
        /// </summary>
        public static void SetDicValue()
        {
            //读取DicConfig.xml 
            //循环读取招式,技艺,等数据
            string path = Application.StartupPath + "/CustomData/";
            IList<DicConfig> list = FileUtils.ReadConfig<DicConfig>(path+ "DicConfig.xml");
            foreach (DicConfig dc in list)
            {
                IList<SelectItem> dicList = FileUtils.ReadConfig<SelectItem>(path + dc.Value);
                Dictionary<string, string> dic = dicList.ToDictionary(item => item.Value, item => item.Text);
                ExplainConfig.Add(dc.Key, dic);
            }
        } 
         
        public static string GetHuiHe(string key)
        {
            if (DropDownListDict["Public.HuiHe"].ContainsKey(key))
                return DropDownListDict["Public.HuiHe"][key];
            return "回合数超过了180，如有需要请自行配置HuiHe.xml文件";
        }
        #endregion

        public static string GetDicValue(string key,string value)
        {
            if (ExplainConfig[key].ContainsKey(value))
            {
                return ExplainConfig[key][value];
            }
            return "";
        }
         
        public static T Clone<T>(T realObject)  
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, realObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(objectStream);
            }
        }
    }
}
