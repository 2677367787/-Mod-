using System;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Helper;

namespace xkfy_mod.Personality
{
    public partial class BattleAreaDataEdit : Form
    { 
        private DataGridViewRow _dr;
        private string _type;
        public BattleAreaDataEdit(DataGridViewRow dr, string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        private void BattleAreaData_Edit_Load(object sender, EventArgs e)
        {
            if (_type == "Update")
            {
                //修改
                DataHelper.SetCtrlByDataRow(this, _dr);
                btnUpdate.Visible = true;
            }
            else if (_type == "Add")
            {
                //新增
                btnAdd.Visible = true;
            }
            else if (_type == "CopyAdd")
            {
                //复制新增
                DataHelper.SetCtrlByDataRow(this, _dr);
                btnAdd.Visible = true;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnMustJoin_Click(object sender, EventArgs e)
        {
            string[] row = new string[3] { "CharID", "Name", "HP" };
            string tbName = "BattleCharacterData";

            ToolsHelper.OpenMultiForm(tbName, row, txtMustJoin,null);
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            cboHuihe.Text = ((RadioButton)sender).Text;
        }

        private void btnZuhe_Click(object sender, EventArgs e)
        {
            string huihe = cboHuihe.Text;
            if (string.IsNullOrEmpty(huihe))
            {
                MessageBox.Show(@"请选择回合！");
            }
            
            string zhenYin;
            if (rdo1.Checked)
                zhenYin = "0";
            else if (rdo2.Checked)
                zhenYin = "1";
            else
                zhenYin = "2";

            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(txtMustJoinStaff.Text))
            {
                sb.Append("*");
            }
            
            string[] mustJoin = txtMustJoin.Text.Split(',');
            for (int i = 0; i < mustJoin.Length; i++)
            {
                sb.AppendFormat("({0},{1},{2})*", huihe, mustJoin[i], zhenYin);
            }
            txtMustJoinStaff.Text += sb.ToString().TrimEnd('*');
            if (chkLikeUp.Checked)
            {
                txtsJoinStaff.Text = txtMustJoinStaff.Text;
            }
            this.txtMustJoin.Text = "";
        }

        private void chkLikeUp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLikeUp.Checked)
            {
                txtsJoinStaff.Text = txtMustJoinStaff.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] row = new string[2] { "iRID", "sRewardData" };
            string tbName = "RewardData";

            ToolsHelper.OpenRadioForm(tbName, row, txtsRewardWin, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] row = new string[2] { "iRID", "sRewardData" };
            string tbName = "RewardData";

            ToolsHelper.OpenRadioForm(tbName, row, txtRewardFale, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataHelper.AddData(this, "BattleAreaData");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataHelper.UpdateData(this, _dr);
        }
    }
}
