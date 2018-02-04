using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Entity;
using xkfy_mod.Helper;
using xkfy_mod.Properties;
using xkfy_mod.SetUp;
using xkfy_mod.Utils;
using xkfy_mod.XML;

namespace xkfy_mod
{
    public partial class FrmMain : DockContent
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {
            try
            {
                IList<AppConfig> list = FileHelper.ReadAppConfig();
                if (list.Count == 0)
                {
                    MessageBox.Show(@"你还没有新建过方案，请先新建方案！");
                    return;
                }

                if (!string.IsNullOrEmpty(list[0].GameInstallPath))
                {
                    tsmSaveToGamePath.Enabled = true;
                    PathHelper.GamePath = list[0].GameInstallPath;
                    tsmSaveToGamePath.Text += list[0].GameInstallPath;
                }
                if (string.IsNullOrEmpty(list[0].CreatePath)) return;
                ClearData();
                
                Start(list[0].CreatePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 生成所有打开过的MOD文件
        /// <summary>
        /// 生成所有打开过的MOD文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsCreateMOD_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Ctrl+S 保存当前文件
        private void tsmCreateCurr_Click(object sender, EventArgs e)
        {
            DockContent d = (DockContent)dockPanel1.ActiveDocument;
            if (d?.Tag == null)
                return;
            string tbName = d.Tag.ToString();
            FileHelper.Distinguish(tbName);
            d.Text = d.Text.TrimEnd('*');
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            //Ctrl+S 保存当前打开的Mod文件
            if (e.KeyCode == Keys.S && e.Control)
            {
                tsmCreateCurr_Click(null, null);
            }
        }
        #endregion　

        private Dock _dock1;  

        #region 联动树形菜单选择
        /// <summary>
        /// 联动树形菜单选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            DockPanel aa = (DockPanel)(sender);
            if (aa.ActiveDocument != null)
            {
                DockContent d = (DockContent)aa.ActiveDocument;
                _dock1.SetSelNode(d.Text);
            }
        }
        #endregion

        #region 开始
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="path"></param>
        private void Start(string path)
        {
            _dock1?.Close();

            _dock1 = new Dock();
            _dock1.Show(dockPanel1, DockState.DockLeft);
            for (int i = dockPanel1.Documents.Count() - 1; i >= 0; i--)
            {
                DockContent dc = (DockContent) dockPanel1.Documents.ElementAt(i);
                dc.Close();
            } 
            About about = new About {MdiParent = this};
            about.Show(dockPanel1);　
        }
        #endregion

        #region 新建解决方案
        /// <summary>
        /// 新建解决方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmCreate_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.SelectedPath = Application.StartupPath + "\\修改后的文件";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                AppConfig ac = new AppConfig();
                ac.CreatePath = folder.SelectedPath;

                List<AppConfig> listAc = new List<AppConfig>();
                listAc.Add(ac);
                //保存Mod的路径，方便选择
                string appConfigPath = Application.StartupPath + "\\CustomData\\AppConfig.xml";
                XmlHelper.XmlSerializeToFile(listAc, appConfigPath, Encoding.UTF8);

                string filePath = Application.StartupPath + "\\原始文件";
                FileUtils.CopyFolderTo(filePath, folder.SelectedPath);
                ClearData();
                Start(folder.SelectedPath);
            }
        }
        #endregion

        #region 打开解决方案
        /// <summary>
        /// 打开解决方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            List<AppConfig> list = XmlHelper.XmlDeserializeFromFile<List<AppConfig>>(Application.StartupPath + "/CustomData/AppConfig.xml", Encoding.UTF8);
            if (list.Count > 0 && !string.IsNullOrEmpty(list[0].CreatePath))
            {
                fileDialog.InitialDirectory = list[0].CreatePath;
            }
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "Mod解决方案|*.project";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ClearData();
                string file = fileDialog.FileName;
                Start(file);
            }
        }
        #endregion

       

        #region 清除静态变量
        /// <summary>
        /// 清除静态变量
        /// </summary>
        private void ClearData()
        {
            DataHelper.ReadError.Clear(); 
            DataHelper.FormConfig.Clear(); 
            DataHelper.ExplainConfig.Clear();  
            DataHelper.DictModFiles.Clear();
            DataHelper.DropDownListDict.Clear(); 
            DataHelper.DictImages.Clear();
            DataHelper.XkfyData.Tables.Clear();
            DataHelper.DdlDataSet.Tables.Clear();
            DataHelper.ToolColumnConfig.Clear();
        }
        #endregion

        private void tsmOpenModFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", PathHelper.ModFoderPath);
        }

        #region 导入1.1 注释
        private void tsmImportXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "1.1MOD的注释文件|*.xml";

            DataTable dt = new DataTable();
            dt.Columns.Add("tablename");
            dt.Columns.Add("column");
            dt.Columns.Add("Value");

            Dictionary<string, string> dlcTbName = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;
                XmlDocument doc = new XmlDocument();
                doc.Load(file);    //加载Xml文件  
                XmlElement rootElem = doc.DocumentElement;   //获取根节点  
                XmlNodeList personNodes = rootElem.GetElementsByTagName("Table1"); //获取person子节点集合  
                foreach (XmlNode node in personNodes)
                {
                    XmlNodeList tablename = ((XmlElement)node).GetElementsByTagName("tablename");  //获取age子XmlElement集合  
                    XmlNodeList column = ((XmlElement)node).GetElementsByTagName("column");
                    XmlNodeList value = ((XmlElement)node).GetElementsByTagName("Value");
                    string tbName = tablename[0].InnerText;
                    if (!dlcTbName.ContainsKey(tbName))
                    {
                        dlcTbName.Add(tbName, tbName);
                    }
                    DataRow dr = dt.NewRow();
                    dr["tablename"] = tbName;
                    dr["column"] = column[0].InnerText;
                    dr["Value"] = value[0].InnerText;
                    dt.Rows.Add(dr);
                }
            }
            ToolsHelper tl = new ToolsHelper();
            string path = Application.StartupPath + "\\工具配置文件\\TableExplain";
            foreach (KeyValuePair<string, string> dlc in dlcTbName)
            {
                List<TableExplain> list = new List<TableExplain>();
                DataRow[] drs = dt.Select("tablename='" + dlc.Key + "'");
                foreach (DataRow item in drs)
                {
                    TableExplain te = new TableExplain();
                    te.Column = item["column"].ToString();
                    string value = item["Value"].ToString();
                    if (StringUtils.GetCnLength(value) <= 8)
                    {
                        te.Text = value;
                    }
                    te.Explain = item["Value"].ToString();
                    
                    list.Add(te);
                }
                XmlHelper.XmlSerializeToFile(list, Path.Combine(path, dlc.Key), Encoding.UTF8);
                MessageBox.Show("导入成功！");
            }
        }
        #endregion

        private void tsmWriteExplain_Click(object sender, EventArgs e)
        {
            FrmExplain fe = new FrmExplain();
            fe.ShowDialog();
        }

        private void tsmRound_Click(object sender, EventArgs e)
        {
            FrmRound fr = new FrmRound(null,null);
            fr.Show();
        } 

        #region 导入Mod文件
        private void tsmImportMod_Click(object sender, EventArgs e)
        {
            string modPath = Application.StartupPath + "\\原始文件";
            FileUtils.DeleteDirectory(modPath);

            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                FileUtils.CopyFolderTo(folder.SelectedPath, modPath);
                MessageBox.Show("已经将指定内容导入。点击新建Mod即可开始修改！");
            }
        }


        #endregion

        private void 重新加载当前文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContent activeDocument = (DockContent)dockPanel1.ActiveDocument;
            if (activeDocument?.Tag == null)
                return;
            string tbName = activeDocument.Tag.ToString();
            DialogResult dr = MessageBox.Show($"请确认是否放弃对文件[{tbName}]的所有修改，并重新加载当前文件内容", @"提示信息", MessageBoxButtons.OKCancel);
            if (dr != DialogResult.OK) return;
            DataHelper.XkfyData.Tables.Remove(tbName);
            if (DataHelper.FormConfig[tbName].DtType == "1")
            {
                DataHelper.XkfyData.Tables.Remove(tbName + "_D");
            }
            FileHelper.LoadTable(tbName);
            activeDocument.Close();
        }

        private void 事件树ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventChart ec = new EventChart();
            ec.Show();
        }

        private void 常用贴图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetUp fs = new frmSetUp(PathHelper.NpcImg);
            fs.Show();
        }

        private void 窗体表格配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTbConfig ft = new FrmTbConfig();
            ft.Show();
        } 

        private void 生成到游戏目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Build(true);
        }

        private void tsmSetGameInstallPath_Click(object sender, EventArgs e)
        {
            SetGamePath sgp = new SetGamePath();
            sgp.Show();
        }

        private void Build(bool buildToGamePath)
        {
            if (buildToGamePath)
            {
                if (!Directory.Exists(PathHelper.GamePath))
                {
                    Directory.CreateDirectory(PathHelper.GamePath);
                }
            }
            foreach (DataTable tb in DataHelper.XkfyData.Tables)
            {
                FileHelper.Distinguish(tb.TableName);
                if (buildToGamePath)
                {
                    FileHelper.Distinguish(tb.TableName, Path.Combine(PathHelper.GamePath, tb.TableName + ".txt"));
                }
            }

            if (string.IsNullOrEmpty(PathHelper.ModifyFolderPath)) return;
            DialogResult dialogR = MessageBox.Show(@"Mod数据生成成功！是否打开生成的数据目录？", @"提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogR == DialogResult.Yes)
            {
                Process.Start("explorer.exe", PathHelper.ModifyFolderPath);
            }
        }

        private void 全部生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Build(false);
        }

        private void modToolStripMenuItem_Click(object sender, EventArgs e)
        {
//            FileUtils.GetCurrFolderFile("修改后的文件", DataHelper.DictModFiles);

//            IList<MyConfig> list = FileHelper.ReadTableConfig();
//            foreach (MyConfig item in list)
//            {
//                //保存窗体配置文件 
//                DataHelper.FormConfig.Add(item.MainDtName, item);
//            }

//            foreach (MyConfig item in list)
//            {
//                if (!DataHelper.DictModFiles.ContainsKey(item.TxtName))
//                    continue;
//                FileHelper.LoadTable(item.MainDtName);
//            }
 
//            string[] strs = null;
//            using (StreamReader sr = new StreamReader(DataHelper.DictModFiles["AlchemyScene.txt"], Encoding.Default))
//            {
//                while (!sr.EndOfStream)
//                {
//                    string readStr = sr.ReadLine(); //读取一行数据
//                    if (readStr != null)
//                    {
//                        strs = readStr.Split('\t'); //将读取的字符串按"制表符/t“和””“分割成数组
//                    }
//                    break; 
//                }
//            }
//
//            IList<TableExplain> listTe = new List<TableExplain>();
//            for (int i = 0; i < strs.Length; i++)
//            {
//                TableExplain te = new TableExplain
//                {
//                    ToolColumn = "AlchemyScene" + (i+1),
//                    Column = strs[i]
//                };
//                listTe.Add(te);
//            }
//            FileHelper.SaveColumnData(listTe, "AlchemyScene");
//            FileHelper.LoadTable("AlchemyScene");
//
//            foreach (DataTable table in DataHelper.XkfyData.Tables)
//            {
//                if (table.TableName.Contains("_D"))
//                {
//                    continue;
//                }
//                List<int> maximumLengthForColumns =
//                        Enumerable.Range(0, table.Columns.Count)
//                            .Select(
//                                col => table.AsEnumerable().Select(row => row[col]).OfType<string>().Max(val => val.Length))
//                            .ToList();
//                IList<TableExplain> te = FileHelper.GetColumnData(table.TableName);
//                for (int i = 0; i < table.Columns.Count; i++)
//                {
//                    if (i >= te.Count)
//                    {
//                        continue;
//                    }
//                    int width;
//                    int height = 21;
//                    int cnStrLen = maximumLengthForColumns[i];
//
//                    if (cnStrLen <= 12)
//                    {
//                        width = 100;
//                    }
//                    else if (cnStrLen < 20)
//                    {
//                        width = 315;
//                    }
//                    else if (cnStrLen < 30)
//                    {
//                        width = 530;
//                    }
//                    else if (cnStrLen < 50)
//                    {
//                        width = 745;
//                    }
//                    else if (cnStrLen < 80)
//                    {
//                        width = 745;
//                        height = 42;
//                    }
//                    else
//                    {
//                        width = 745;
//                        height = 63;
//                    }
//                    te[i].Width = width;
//                    te[i].Height = height;
//                    
//                }
//                FileHelper.SaveColumnData(te, table.TableName);
//            }
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormData fd = new FormData()
            {
                TableName = "BattleNeigong"
            };
            FrmAnnotation fa = new FrmAnnotation(fd);
            fa.Show();
//            FormData fd = new FormData()
//            {
//                TableName = "BattleNeigong"
//            };
//            FileUtils.GetCurrFolderFile(PathHelper.ModFoderPath, DataHelper.DictModFiles);
//            IList<MyConfig> list = FileHelper.ReadTableConfig();
//            //循环所有表配置
//            foreach (MyConfig item in list)
//            {
//                if(item.MainDtName.ToUpper() == "MAP") return;
//                //DataHelper.FormConfig.Add(item.MainDtName, item);
//                FileHelper.LoadTable(item.MainDtName);
//                FileHelper.Distinguish(item.MainDtName);
//            }
//            FrmAnnotation at = new FrmAnnotation(fd);
//            at.Show();
        }

        private void tsmCreate_Click_1(object sender, EventArgs e)
        {
            try
            { 
                DialogResult dr = MessageBox.Show(Resources.FrmMain_Restart, Resources.FrmMain_Restart, MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    string path = PathHelper.AppConfigPath;
                    if (!File.Exists(path))
                    {
                        MessageBox.Show(@"你还没有新建过方案，请先新建方案！");
                        return;
                    }
                    var list = FileHelper.ReadAppConfig();

                    list[0].CreatePath = PathHelper.ModifyFolderPath;
                    //保存Mod的路径，方便选择
                    FileHelper.SaveAppConfig(list);

                    FileUtils.DeleteDirectory(PathHelper.ModifyFolderPath);

                    FileUtils.CopyFolderTo(PathHelper.ModFoderPath, PathHelper.ModifyFolderPath);
                    ClearData();

                    Start(PathHelper.ModifyFolderPath);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(@"发生未知异常,请手动复制工具目录【原始文件】文件夹里所有内容\r\n放入【修改后的文件】然后重启工具即可");
            }
        }

        private void 回合信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRound fr = new FrmRound(null, null);
            fr.Show();
        }
    }
}
