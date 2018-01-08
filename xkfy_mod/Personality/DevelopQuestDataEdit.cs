using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Helper;

namespace xkfy_mod.Personality
{
    public partial class DevelopQuestDataEdit : Form
    { 
        private readonly ToolsHelper _tl = new ToolsHelper();
        private readonly DataGridViewRow _dr;
        private readonly string _type;
        private DataTable _dt;

        public DevelopQuestDataEdit(DataGridViewRow dr,string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        
        private void DevelopQuestData_Edit_Load(object sender, EventArgs e)
        {
            _dt = DataHelper.DdlDataSet.Tables["TalkManager.iGtype"];
            //触发条件
            DataHelper.BinderComboBox(cboCondition, "DevelopQuestData.iCondition");
            //触发类型/地点
            DataHelper.BinderComboBox(cboitype, "DevelopQuestData.iType"); 

            SetValue();
            if (_type == "Modify")
            {
                btnUpdate.Visible = true;
            }
            else
            {
                btnAdd.Visible = true;
            }
        }
        private void SetValue()
        {
            DataHelper.SetCtrlByDataRow(this, _dr);
        }

        private void cboCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboArg1.Visible = true;
            txtiArg1Edit.Visible = false;
            txtiArgName.Visible = false;
            btnSelBtn.Visible = false;
            btnSelBtn2.Visible = false;
            txtiAgr2Name.Visible = false;
            string condition = cboCondition.SelectedValue.ToString();
            string key = "";
            switch (condition)
            {
                case "3":
                    key = "JBG";
                    break;
                case "4":
                    key = "ZS";
                    break;
                case "5":
                    key = "SHJN";
                    break;
                case "6":
                    key = "JY";
                    break;
                case "2":
                case "7":
                case "8":
                case "9":
                case "10":
                case "11":
                case "13":
                    if (condition == "7")
                    {
                        btnSelBtn2.Visible = true;
                        txtiAgr2Name.Visible = true;
                    }

                    if (condition == "2" || condition == "7" || condition == "11")
                    {
                        txtiArgName.Visible = true;
                        btnSelBtn.Visible = true;
                    }
                    cboArg1.Visible = false;
                    txtiArg1Edit.Visible = true;
                    break;
            }

            if (!string.IsNullOrEmpty(key))
            {
                Dictionary<string, string> dic = DataHelper.ExplainConfig[key];
                _tl.BinderComboBox(cboArg1, dic);
            }
        }

        private void btnAddOne_Click(object sender, EventArgs e)
        {
            string iArg1 = string.Empty;
            string iArg2 = string.Empty;
            string iCondition = string.Empty;
            if (txtiArg1.Text != "")
            {
                iArg1 = ",";
            }

            if (txtiArg2.Text != "")
            {
                iArg2 = ",";
            }

            if (txtiCondition.Text != "")
            {
                iCondition = ",";
            }

            //条件代码
            txtiCondition.Text += iCondition + cboCondition.SelectedValue;
            switch (cboCondition.SelectedValue.ToString())
            {
                case "3":
                case "4":
                case "5":
                case "6":
                    txtiArg1.Text += iArg1 + cboArg1.SelectedValue;
                    break;
                case "2":
                case "7":
                case "8":
                case "9":
                case "10":
                case "11":
                case "13":
                    txtiArg1.Text += iArg1 + txtiArg1Edit.Text;
                    break;
            }
            txtiArg2.Text += iArg2 + txtiArg2Edit.Text;
            _tl.ClearData(groupBox1);
        }

        private void btnSelBtn_Click(object sender, EventArgs e)
        {
            switch (cboCondition.SelectedValue.ToString())
            {
                case "2":
                    ToolsHelper.OpenNpcData(txtiArg1Edit, txtiArgName, Const.OpenType.Radio);
                    break;
                case "11": 
                    ToolsHelper.OpenItemData(txtiArg1Edit, txtiArgName,Const.OpenType.Radio);
                    break;
                case "7":
                    FrmRound fr = new FrmRound(txtiArg1Edit, txtiArgName);
                    fr.ShowDialog();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmRound fr = new FrmRound(txtiArg2Edit,txtiAgr2Name);
            fr.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataHelper.AddData(this, "DevelopQuestData");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataHelper.UpdateData(this, _dr);
        }
    }
}
