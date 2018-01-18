using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Entity;
using xkfy_mod.Helper;
using xkfy_mod.SetUp;

namespace xkfy_mod.Personality
{
    public partial class RoutineDataEdit : Form
    {
        private readonly DataGridViewRow _dr;
        private readonly string _type;
        public RoutineDataEdit(DataGridViewRow dr, string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        private void btnSel2_Click(object sender, EventArgs e)
        {
            showForm(txtSkill2ID, txtSkill2Name);
        }

        private void btnSel1_Click(object sender, EventArgs e)
        {
            showForm(txtSkill1ID, txtSkill1Name);
        }

        private void btnSel3_Click(object sender, EventArgs e)
        {
            showForm(txtSkill3ID, txtSkill3Name);
        }

        private void showForm(TextBox txtId, TextBox txtName)
        { 
            ToolsHelper.OpenBattleAbility(txtId, txtName, Const.OpenType.Radio);
        }

        private void RoutineData_Edit_Load(object sender, EventArgs e)
        {
            DataHelper.BindiWearAmsType(cboiWearAmsType);
            switch (_type)
            {
                case "Add":
                    btnAdd.Visible = true;
                    break;
                case "CopyAdd":
                    btnAdd.Visible = true;
                    SetValue();
                    break;
                case "Modify":
                    btnUpdate.Visible = true;
                    SetValue();
                    break;
            }
        }
        private void SetValue()
        {
            DataHelper.SetCtrlByDataRow(this, _dr);

            if (!string.IsNullOrEmpty(txtSkill1ID.Text))
                txtSkill1Name.Text = GetSkillName(txtSkill1ID.Text);
            if (!string.IsNullOrEmpty(txtSkill2ID.Text))
                txtSkill2Name.Text = GetSkillName(txtSkill2ID.Text);
            if (!string.IsNullOrEmpty(txtSkill3ID.Text))
                txtSkill3Name.Text = GetSkillName(txtSkill3ID.Text);
            GetSpecialAdd(txtsSpecialAdd.Text);
        }

        private string GetSkillName(string skillNo)
        { 
            Const.BattleAbilityFe.Id = skillNo;
            return ExplainHelper.GetBattleAbilityName();
        }

        private void GetSpecialAdd(string value)
        {
            string key = "";
            string[] str = value.Split(',');
            switch (str[0])
            {
                case "0":
                    key = "MB";
                    break;
                case "1":
                    key = "JBG";
                    break;
                case "2":
                    key = "ZS";
                    break;
                case "3":
                    key = "SHJN";
                    break;
                case "4":
                    key = "JY";
                    break;
            }
//            switch (str[0])
//            {
//                case "0":
//                    key = "Basic.MianBan";
//                    break;
//                case "1":
//                    key = "Basic.JiBenGong";
//                    break;
//                case "2":
//                    key = "Basic.ZhaoShi";
//                    break;
//                case "3":
//                    key = "Basic.ShengHuoJiNeng";
//                    break;
//                case "4":
//                    key = "Basic.JiYi";
//                    break;
//            }
            Dictionary<string, string> dic = DataHelper.ExplainConfig[key];
            if (dic.ContainsKey(str[1]))
                txtTsName.Text = dic[str[1]];
        }

        private void btnSel4_Click(object sender, EventArgs e)
        {
            AttributeList al = new AttributeList(PathHelper.RoutineDataSelConfig, txtsSpecialAdd, txtTsName);
            al.Location = btnSel4.Location;
            al.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        { 
            DataHelper.UpdateData(this, _dr);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow[] drRd = DataHelper.XkfyData.Tables["RoutineData"].Select("iRoutineID='" + txtiRoutineID.Text + "'");
            if (drRd.Length > 0)
            {
                lblMsg.Text = @"ID已经存在，为了避免游戏错误,不允许新增相同ID的数据";
                return;
            }
             
            DataHelper.AddData(this, "RoutineData");
        }

        private void btnSetValue_Click(object sender, EventArgs e)
        {
            FrmSetCbo fs = new FrmSetCbo( cboiWearAmsType,PathHelper.PubWearAmspath);
            fs.Show();
        }

        private void annotationCtrl1_Load(object sender, EventArgs e)
        { 
            annotationCtrl1.txtExplain.Text = "";
        }
    }
}
