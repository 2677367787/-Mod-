using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Entity;
using xkfy_mod.Helper;
using xkfy_mod.Utils;
using xkfy_mod.XML;

namespace xkfy_mod.Personality
{
    public partial class ItemDataEdit : Form
    {
        private readonly ToolsHelper _tl = new ToolsHelper();
        private readonly DataGridViewRow _dr;
        private readonly string _type;
        public ItemDataEdit(DataGridViewRow dr,string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        public ItemDataEdit(string type)
        {
            _type = type;
            InitializeComponent();
        }

        private Dictionary<string, string> _assist = new Dictionary<string, string>();
        private void ItemData_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                var list = FileUtils.ReadConfig<LinkageDdl>(PathHelper.ItemDataIrecover);
                DataHelper.BinderComboBox(cboiRecover1, list);
                DataHelper.BinderComboBox(cboiRecover2, list);
                DataHelper.BinderComboBox(cboiRecover3, list);

                _assist = list.Where(dict=>dict.DataKey != null).ToDictionary(dict => dict.Key, dict => dict.DataKey);
                //绑定武器类型
                DataHelper.BindiWearAmsType(comboBox1);

                switch (_type)
                {
                    case "Add":
                        btnUpdate.Visible = false;
                        btnSave.Visible = true;
                        break;
                    case "CopyAdd":
                        SetValue();
                        btnUpdate.Visible = false;
                        btnSave.Visible = true;
                        break;
                    default:
                        SetValue();
                        btnUpdate.Visible = true;
                        btnSave.Visible = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetValue()
        {
            DataHelper.SetCtrlByDataRow(this, _dr);
            //DataHelper.SetCtrlByDataRow(gbCondition, _dr);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemID.Text))
            {
                lblMsg.Text = @"请输入ID";
                return;
            }

            DataRow[] addItem = DataHelper.XkfyData.Tables["ItemData"].Select("iItemID$0='" + txtItemID.Text + "'");
            if (addItem.Length > 0)
            {
                lblMsg.Text = @"此ID在源数据中已经存在,请修改ID，并确保在该文件此ID是唯一的";
                return;
            } 
            DataHelper.AddData(this,Const.ItemData);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemID.Text))
            {
                lblMsg.Text = @"请输入ID";
                return;
            }
            if (txtItemID.Text != _dr.Cells["iItemID$0"].Value.ToString())
            {
                DataRow[] addItem = DataHelper.XkfyData.Tables["ItemData"].Select("iItemID$0='" + txtItemID.Text + "'");
                if (addItem.Length > 0)
                {
                    lblMsg.Text = @"此ID在源数据中已经存在,请修改ID，并确保在该文件是唯一的";
                    return;
                }
            } 
            DataHelper.UpdateData(this, _dr);
        }

        private void cboiRecover1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string iRecover = ((ComboBox)sender).SelectedValue.ToString();
              
                string key = "";
                if (_assist.ContainsKey(iRecover))
                {
                    key = _assist[iRecover];
                }
                 
                string name = ((ComboBox)sender).Name;
                bool isHaveKey = string.IsNullOrEmpty(key);
                if (name == "cboiRecover1")
                {
                    txtiRecover1.Text = iRecover; 
                    DataHelper.BinderComboBox(cboiArg1, isHaveKey ? null : DataHelper.DropDownListDict[key]);
                }
                else if (name == "cboiRecover2")
                {
                    txtiRecover2.Text = iRecover; 
                    DataHelper.BinderComboBox(cboiArg2, isHaveKey ? null : DataHelper.DropDownListDict[key]);
                }
                else if (name == "cboiRecover3")
                {
                    txtiRecover3.Text = iRecover;
                    DataHelper.BinderComboBox(cboiArg3, isHaveKey ? null : DataHelper.DropDownListDict[key]); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 

        private void cboiArg1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = ((ComboBox) sender).SelectedValue;
            if (value == null)
            {
                return;
            }
            string iArg = ((ComboBox)sender).SelectedValue.ToString();
            string tag = ((ComboBox)sender).Name;
            switch (tag)
            {
                case "cboiArg1":
                    txtiArg1.Text = iArg;
                    break;
                case "cboiArg2":
                    txtiArg2.Text = iArg;
                    break;
                case "cboiArg3":
                    txtiArg3.Text = iArg;
                    break;
            }
        }

        private void btnSel1_Click(object sender, EventArgs e)
        {
            _tl.GetStringTable(textBox19,null);
        }

        private void btnSel2_Click(object sender, EventArgs e)
        {
            _tl.GetStringTable(textBox25, null); 
        }

        private void btnSel3_Click(object sender, EventArgs e)
        {
            _tl.GetStringTable(textBox21, null);
        } 
    }
}
