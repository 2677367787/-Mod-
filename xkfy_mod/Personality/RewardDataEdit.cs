using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Helper;

namespace xkfy_mod.Personality
{
    public partial class RewardDataEdit : Form
    { 
        private readonly DataGridViewRow _dr;
        private readonly string _type;
        public RewardDataEdit(DataGridViewRow dr, string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            AttributeList al = new AttributeList(PathHelper.RewardDataSelConfig, txtSelValue, txtSelName)
            {
                Location =
                    new Point(Location.X + btnSel.Location.X + groupBox1.Location.X,
                        Location.Y + btnSel.Location.Y + groupBox1.Location.Y)
            };

            al.ShowDialog();
        }

        private void RewardData_Edit_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("6", "好感度");
            dic.Add("7", "获得物品");
            dic.Add("14", "金钱");
            dic.Add("16", "套路");
            dic.Add("17", "内功");
            dic.Add("27", "攻击");
            dic.Add("28", "防御");
            dic.Add("40", "队友入队");
            DataHelper.BinderComboBox(cboItem, dic);

            if (_type == "Modify")
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

        private void button2_Click(object sender, EventArgs e)
        {
            ToolsHelper tl = new ToolsHelper();
            string[] sRewardData = txtsRewardData.Text.Split('*');
            lblExplain.Text = tl.ExplainRewardData(sRewardData, DataHelper.DdlDataSet.Tables["TalkManager.iGtype"]);
        }

        private void btnSelMsgStr_Click(object sender, EventArgs e)
        {
            string[] row = new string[3] { "iID", "sString", "xUse" };
            ToolsHelper.OpenRadioForm("String_table", row, txtMsgStr, txtMsgString);
        }

        private void btnAddSel_Click(object sender, EventArgs e)
        {
            StringBuilder sbItem = new StringBuilder();
            if (!string.IsNullOrEmpty(txtsRewardData.Text))
            {
                sbItem.Append("*");
            }
            sbItem.AppendFormat("({0},{1},{2})", txtSelValue.Text, txtValue.Text, txtMsgStr.Text);
            txtsRewardData.Text += sbItem.ToString();
        }

        private void cboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSel2.Visible = true;
            txtSelValue2.Enabled = true;
            txtSelValue2.Text = "";
            label6.Text = "值";
            switch (cboItem.SelectedValue.ToString())
            {
                case "14":
                    setSel1();
                    txtMsgStr2.Text = "200067";
                    break;
                case "27":
                    txtMsgStr2.Text = "200089";
                    setSel1();
                    break;
                case "28":
                    txtMsgStr2.Text = "200090";
                    setSel1();
                    break;
                case "6":
                    txtMsgStr2.Text = "200005";
                    break;
                case "7":
                    label6.Text = "数量";
                    txtMsgStr2.Text = "200054";
                    break;
                case "16":
                    txtMsgStr2.Text = "";
                    break;
                case "17":
                    txtMsgStr2.Text = "200083";
                    break;
            }
        }

        private void setSel1()
        {
            btnSel2.Visible = false;
            txtSelValue2.Enabled = false;
            txtSelValue2.Text = "0";
        }

        private void btnSel2_Click(object sender, EventArgs e)
        {
            string[] row = null;
            string tbName = string.Empty;
            switch (cboItem.SelectedValue.ToString())
            {
                case "6":
                    row = new string[3] { "iID", "sNpcName", "sIntroduction" };
                    tbName = "NpcData";
                    break;
                case "7":
                    row = new string[3] { "iItemID$0", "sItemName$1", "sTip$5" };
                    tbName = "ItemData";
                    break;
                case "16":
                    row = new string[3] { "iRoutineID", "sRoutineName", "sModelName" };
                    tbName = "RoutineData";
                    break;
                case "17":
                    row = new string[3] { "ID", "Name", "Desc" };
                    tbName = "BattleNeigong";
                    break;
                case "40":
                    row = new string[3] { "CharID", "Name", "HP" };
                    tbName = "BattleCharacterData";
                    break;
                    
            }

            ToolsHelper.OpenRadioForm(tbName, row, txtSelValue2, txtSelName2); 
        }

        private void btnAddSel2_Click(object sender, EventArgs e)
        {
            StringBuilder sbItem = new StringBuilder();
            if (!string.IsNullOrEmpty(txtsRewardData.Text))
            {
                sbItem.Append("*");
            }
            sbItem.AppendFormat("({0},{1},{2},{3})", cboItem.SelectedValue,txtSelValue2.Text, txtValue2.Text, txtMsgStr2.Text);
            txtsRewardData.Text += sbItem.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataHelper.AddData(this, "RewardData");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataHelper.UpdateData(this, _dr);
        }

        private void btnSelMsgStr2_Click(object sender, EventArgs e)
        {

        }
    }
}
