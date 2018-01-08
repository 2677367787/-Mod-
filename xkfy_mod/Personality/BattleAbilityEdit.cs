using System;
using System.Linq;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Helper;

namespace xkfy_mod.Personality
{
    public partial class BattleAbilityEdit : Form
    {
        private readonly DataGridViewRow _dr;
        private string _type;
        public BattleAbilityEdit(DataGridViewRow dr,string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        public BattleAbilityEdit(string type)
        {
            _type = type;
            InitializeComponent();
        }

        private void BattleAbility_Edit_Load(object sender, EventArgs e)
        {
            switch (_type)
            {
                case "Add":
                    btnAdd.Visible = true;
                    break;
                case "CopyAdd":
                    btnAdd.Visible = true;
                    SetValue();
                    break;
                case "Update":
                    //txtSkillNo.Enabled = false;
                    btnUpdate.Visible = true;
                    SetValue();
                    break;
            }
        }

        private void SetValue()
        {
            DataHelper.SetCtrlByDataRow(this, _dr);
            string[] condition = _dr.Cells["condition"].Value.ToString().Split(',');
            string buffName = condition.Where(t => t != "" && t != "0").Aggregate(string.Empty, (current, t) => current + (ExplainHelper.GetConditionName(t) + ","));
            txtBuffName.Text = buffName.TrimEnd(','); 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSkillNo.Text))
            {
                lblMsg.Text = @"请输入ID";
                return;
            } 
            DataHelper.UpdateData(this, _dr);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        { 
            DataHelper.AddData(this, "BattleAbility");
        }

        private void btnSelBuff_Click(object sender, EventArgs e)
        { 
            ToolsHelper.OpenBattleCondition(txtBuffID, txtBuffName,Const.OpenType.Radio);
        }
    }
}
