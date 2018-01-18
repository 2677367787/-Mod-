using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Data;
using xkfy_mod.Entity;
using xkfy_mod.Helper;
using xkfy_mod.Utils;

namespace xkfy_mod
{
    public partial class Almighty : DockContent
    {
        private readonly string _tbName;
        private string _editForm = string.Empty;
        private readonly string _configKey;
        private ToolsHelper tl = new ToolsHelper();

        private readonly IDictionary<string, TableExplain> _dictTableExplain = new Dictionary<string, TableExplain>();
        public Almighty(string tbName)
        {
            _tbName = tbName;
            _configKey = tbName;
            if (!DataHelper.FormConfig.ContainsKey(_tbName))
            {
                string typeName = _tbName.Substring(0, 3); 
                //如果文件名称的前3个字符等于MAP
                _configKey = typeName.ToUpper() == "MAP" ? DataHelper.MapConfigFileName : DataHelper.NpcConfigFileName;
            }
            InitializeComponent();
        }

        private void Almighty_Load(object sender, EventArgs e)
        {
            try
            { 
                FileHelper.LoadTable("RewardData");
 
                BulidSn(DataHelper.XkfyData.Tables[_tbName]);
                dg1.DataSource = DataHelper.XkfyData.Tables[_tbName].DefaultView;

                for (int i = 0; i < dg1.Columns.Count; i++)
                {
                    dg1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                //根据是否有定制窗体,显示复制新增
                if (DataHelper.FormConfig.ContainsKey(_configKey))
                {
                    MyConfig myConfig = DataHelper.FormConfig[_configKey];
                    _editForm = myConfig.EditFormName;
                    if (_configKey == Const.DevelopQuestData)
                    {
                        btnDebug.Visible = true;
                    }
                    else if (_configKey == Const.TalkManager)
                    {
                        btnEditTalkGroup.Visible = true;
                    }
                }

                //生成查询条件
                IList<TableExplain> list = DataHelper.ToolColumnConfig[_configKey]; 
                bool isSelColumn = list.Where(s => s.IsSelColumn == 1).ToList().Count != 0;

                for (int i = 0; i < list.Count; i++)
                {
                    string columnName = StringUtils.FormatToolColumn(list[i].Column);
                    _dictTableExplain.Add(columnName, list[i]);
                    if (list[i].IsSelColumn == 1)
                    {
                        ControlHelper.BuidQueryControl(gbQueryCon, list[i], toolTip1);
                    }

                    //如果没设置查询列 默认前3行为查询列
                    if (!isSelColumn)
                    {
                        if (i < 4)
                        {
                            ControlHelper.BuidQueryControl(gbQueryCon, list[i], toolTip1);
                        }
                    }

                    if (string.IsNullOrEmpty(list[i].Text))
                    {
                        continue;
                    }

                    var dataGridViewColumn = dg1.Columns[columnName];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.HeaderText = list[i].Text;
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dg1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dg1.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dg1.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dg1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

            if (dg1.Rows[e.RowIndex].Cells["rowState"].Value == null) return;
            var state = dg1.Rows[e.RowIndex].Cells["rowState"].Value.ToString();
            switch (state)
            {
                case "0":
                    dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;
                    break;
                case "1":
                    dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
                    break;
            }
        }

        private void BulidSn(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["indexSn"] = i;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string where = "1 = 1";

                foreach (Control c in gbQueryCon.Controls)
                {
                    if (c.Tag == null)
                    {
                        continue;
                    }

                    if (c is TextBox)
                    {
                        var tag = ((TextBox) c).Tag.ToString();
                        var text = ((TextBox) c).Text;

                        if (!string.IsNullOrEmpty(text))
                        {
                            where += " and " + tag + " like '%" + text + "%' ";
                        }
                    }
                    else if (c is CheckBox)
                    {
                        if (((CheckBox) c).Checked)
                        {
                            where += " and (rowState = 1 or rowState = 0)";
                        }
                    }
                    else if (c is ComboBox)
                    {
                        var tag = ((ComboBox) c).Tag.ToString();
                        var text = ((ComboBox) c).SelectedValue;
                        if (text.ToString() != "all")
                        {
                            where += $" and {tag} like '%{text}%' ";
                        } 
                    }
                }

                DataView dv = dg1.DataSource as DataView;
                if (dv != null)
                {
                    dv.RowFilter = where;
                    dg1.DataSource = dv;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 解释战斗信息
        private void ExplainBattleArea(DataGridViewRow dv, DataTable dt)
        {
            StringBuilder sbExplain = new StringBuilder();
            StringBuilder sbExplainRed = new StringBuilder();
            StringBuilder slExplain = new StringBuilder();
            StringBuilder faleExplain = new StringBuilder();
            
            string[] sMustJoinStaff = dv.Cells["sMustJoinStaff$2"].Value.ToString().Split('*');
            foreach (string str in sMustJoinStaff)
            {
                string[] mustJoinStaff = str.Replace(")", "").Replace("(", "").Split(',');
                string huihe = mustJoinStaff[0];
                string npcid = mustJoinStaff[1];
                string zhenyin = mustJoinStaff[2];
                string zyStr;

                string npcName = DataHelper.GetValue("BattleCharacterData", "Name", "CharID", npcid);
                if (zhenyin == "0")
                {
                    zyStr = "我方";
                    sbExplain.AppendFormat("第{0}回合,{1}阵营，加入【{2}】", huihe, zyStr, npcName);
                    sbExplain.Append("\r\n");
                }
                else if (zhenyin == "1")
                {
                    zyStr = "敌对";
                    sbExplainRed.AppendFormat("第{0}回合,{1}阵营，加入【{2}】", huihe, zyStr, npcName);
                    sbExplainRed.Append("\r\n");
                }
                else
                {
                    zyStr = "中立";
                    sbExplain.AppendFormat("第{0}回合,{1}阵营，加入【{2}】", huihe, zyStr, npcName);
                    sbExplain.Append("\r\n");
                }
            }
            //胜利条件
            int i = 0;
            string[] iVictory = dv.Cells["iVictory$4"].Value.ToString().Split('*');
            foreach (string str in iVictory)
            {
                if (i != 0)
                {
                    slExplain.Append(" 或 ");
                }
                i++;
                string[] victory = str.Replace(")", "").Replace("(", "").Split(',');

                if (victory[0] == "0" && victory[1] == "0" && victory[2] == "0")
                {
                    slExplain.Append("对方全灭");
                }
                else if (victory[0] == "1" && victory[1] != "0" && victory[2] == "0")
                {
                    slExplain.AppendFormat("坚持{0}回合！", victory[1]);
                }
                else if (victory[0] == "0" && victory[1] != "0" && victory[2] == "0")
                {
                    slExplain.AppendFormat("坚持{0}回合！", victory[1]);
                }
                else
                {
                    slExplain.AppendFormat("弱鸡作者不明白{0}的意思", str);
                }
            }

            //失败条件
            i = 0;
            string[] iFale = dv.Cells["iFale$5"].Value.ToString().Split('*');
            foreach (string str in iFale)
            {
                if (i != 0)
                {
                    faleExplain.Append(" 或 ");
                }
                i++;
                string[] victory = str.Replace(")", "").Replace("(", "").Split(',');

                if (victory[0] == "0" && victory[1] == "0" && victory[2] == "0")
                {
                    faleExplain.Append("我方全灭");
                }

                else if (victory[0] == "1" && victory[1] != "0" && victory[2] == "0")
                {
                    faleExplain.AppendFormat("对方坚持了{0}回合！", victory[1]);
                }

                else if (victory[0] == "0" && victory[1] != "0" && victory[2] == "0")
                {
                    faleExplain.AppendFormat("对方坚持了{0}回合！", victory[1]);
                }
                else
                {
                    faleExplain.AppendFormat("弱鸡作者不明白{0}的意思", str);
                }
            }
            lblExplainRed.Text = sbExplainRed.ToString();
            lblSl.Text = slExplain.ToString();
            txtExplain.Text = sbExplain.ToString();
            lblFale.Text = faleExplain.ToString();

            DataRow[] drBa = DataHelper.XkfyData.Tables["RewardData"].Select($"iRID='{dv.Cells["sReward$6"].Value}'");
            if (drBa.Length > 1)
            {
                return;
            }

            if (drBa.Length == 0)
            {
                txtWin.Text = $"数据有误,无法解析iRID={dv.Cells["sReward$6"].Value}";
                return;
            }

            string[] sRewardData = drBa[0]["sRewardData"].ToString().Split('*');

            txtWin.Text = tl.ExplainRewardData(sRewardData, dt);
        }
        #endregion 

        private void dg1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.DefaultCellStyle.BackColor = Color.MistyRose;
        }

        private void tsmCopyRow_Click(object sender, EventArgs e)
        {
            if (dg1.CurrentRow == null)
                return;
            DataRow dr = ((DataView) dg1.DataSource).Table.NewRow();
            for (int i = 0; i < dg1.Columns.Count; i++)
            {
                dr[dg1.Columns[i].Name] = dg1.CurrentRow.Cells[dg1.Columns[i].Name].Value;
            }
            dr["rowState"] = "1";
            _copyRow = dr;
        }

        private DataRow _copyRow;
        private void dg1RightMenu_Opening(object sender, CancelEventArgs e)
        {
            tsmInsertCopyRow.Enabled = _copyRow != null;

            tsmCopyRow.Enabled = dg1.CurrentCell != null;
        }

        private void tsmInsertCopyRow_Click(object sender, EventArgs e)
        {
            if (dg1.CurrentRow != null)
            {
                int sn = Convert.ToInt16(dg1.CurrentRow.Cells["indexSn"].Value);
                _copyRow["indexSn"] = (sn + 1).ToString();
                ((DataView) dg1.DataSource).Table.Rows.InsertAt(_copyRow, sn+1);
            }
            _copyRow = null;
            BulidSn(DataHelper.XkfyData.Tables[_tbName]);
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //修改 0
                ShowForm("Modify", "修改");
            } 
        }

        private void btnCopyAdd_Click(object sender, EventArgs e)
        {
            //复制新增 2
            ShowForm("CopyAdd","复制新增");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //新增 1
            ShowForm("Add","复制新增");
        }
        private void ShowForm(string type,string remark)
        { 
            DataGridViewRow dr = dg1.CurrentRow;
            if (!string.IsNullOrEmpty(_editForm) && _editForm != "0")
            {
                //利用反射实例化窗口
                Type t = Type.GetType("xkfy_mod.Personality." + _editForm);//窗体名要加上程序集名称
                if (t == null) return;
                Form form;
                if (type == "EditGroup")
                { 
                    DataRow[] talkGroupRows = DataHelper.XkfyData.Tables[_tbName].Select($"iQGroupID='{dr.Cells["iQGroupID"].Value}'");
                    form = (Form)Activator.CreateInstance(t, talkGroupRows, type); 
                }
                else
                {
                    form = (Form)Activator.CreateInstance(t, dr, type);
                }
                form.Text = remark + _tbName;
                form.ShowDialog();
            }
            else
            { 
                FormData fd = new FormData
                {
                    Dr = dr,
                    Type = type,
                    DictTableExplain = _dictTableExplain,
                    TableName = _tbName,
                    ConfigKeyName = _configKey
                };
                AlmightyEdit ae = new AlmightyEdit(fd) {Text = remark + _tbName};
                ae.Show();
            }
        }

        private void tsmDelRow_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = MessageBox.Show(@"是否确定要删除当前选择的行？", @"提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogR != DialogResult.Yes) return;
            DataGridViewRow currentRow = dg1.CurrentRow;
            if (currentRow != null) dg1.Rows.Remove(currentRow);
        }

        private void dg1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (string.IsNullOrEmpty(dg1.Rows[e.RowIndex].Cells["rowState"].Value.ToString()))
                dg1.Rows[e.RowIndex].Cells["rowState"].Value = "0";
            //dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;

            if (Text.IndexOf("*", StringComparison.Ordinal) == -1)
            {
                Text = Text + @"*";
            }
        }

        private void dg1_CurrentCellChanged(object sender, EventArgs e)
        {
            txtExplain.Text = "";
            DataGridViewRow dv = dg1.CurrentRow;
            if (dv?.Cells[1].Value == null)
                return;
            DataTable dt = DataHelper.DdlDataSet.Tables["TalkManager.iGtype"];
            switch (_tbName)
            {
                case "DevelopQuestData": 
                    string[] striType = dv.Cells["iType"].Value.ToString().Split(',');
                    string[] striArg1 = dv.Cells["iArg1"].Value.ToString().Split(',');
                    string[] striArg2 = dv.Cells["iArg2"].Value.ToString().Split(',');
                    string[] striCondition = dv.Cells["iCondition"].Value.ToString().Split(',');
                    string[] explain = tl.ExplainDevelopQuest(striCondition, striType, striArg1, striArg2);
                    txtExplain.Text = explain[0] + "\r\n\r\n" + explain[1];
                    break;
                //战斗奖励
                case "RewardData":
                    string[] sRewardData = dv.Cells["sRewardData"].Value.ToString().Split('*');
                    txtExplain.Text = tl.ExplainRewardData(sRewardData, dt);
                    break;
                //战斗结果
                case "BattleAreaData":
                    gbZhanDou.Visible = true;
                    ExplainBattleArea(dv, dt);
                    break;
                //内功
                case "BattleNeigong":
                    txtExplain.Text = tl.ExplainNeiGong(dv.Cells["ID"].Value.ToString());
                    break;
                //对话
                case "TalkManager":
                    txtExplain.Text = tl.ExplainTalkManager(dv); 
                    break;
                //招式
                case "RoutineData":
                    txtExplain.Text = tl.ExplainRoutineData(dv); 
                    break;
                //mapTalkManager
                case "MapTalkManager":
                    string iGiftId = dv.Cells["iGiftID"].Value.ToString();
                    if (iGiftId == "0")
                        break;
                    DataRow[] drRw = DataHelper.XkfyData.Tables["RewardData"].Select("iRID='" + iGiftId + "'");
                    if (drRw.Length != 1)
                    {
                        txtExplain.Text = @"奖励ID有误，请检查！";
                    }
                    else
                    {
                        txtExplain.Text = tl.ExplainRewardData(drRw[0]["sRewardData"].ToString().Split('*'), dt);
                    }
                    break;
                case "QuestDataManager":
                    if (dv.Cells["sOConditions"].Value.ToString() != "0")
                    {
                        string[] sOConditions = dv.Cells["sOConditions"].Value.ToString().Split('*');
                        txtExplain.Text = @"触发条件：" + tl.ExplainQuestDataCon(sOConditions, dt);
                    }
                    //关闭剧情
                    string finshQType =dv.Cells["sFinshQType"].Value.ToString();
                    if (finshQType != "0" && finshQType != "(1,0,0)")
                    {
                        string[] sFinshQType = finshQType.Split('*');
                        txtExplain.Text += @"\r\n关闭事件：" + tl.ExplainQuestDataFinish(sFinshQType);
                    }
                    string iGiftId1 = dv.Cells["iGiftID1"].Value.ToString();
                    if (iGiftId1 == "0")
                        break;
                    drRw = DataHelper.XkfyData.Tables["RewardData"].Select("iRID='" + iGiftId1 + "'");
                    if (drRw.Length != 1)
                    {
                        txtExplain.Text += @"\r\n获得奖励：奖励ID有误，请检查！";
                    }
                    else
                    {
                        txtExplain.Text += @"\r\n获得奖励：" + tl.ExplainRewardData(drRw[0]["sRewardData"].ToString().Split('*'), dt);
                    }
                    break;
                //战斗补充文件
                case "BattleSchedule":
                    txtExplain.Text = tl.ExplainBattleSchedule(dv); 
                    break;
                case "ItemData":
                    StringBuilder sbExplain = new StringBuilder(); 
                    if (dv.Cells[0].Value.ToString() == "")
                        return;
                    string snpclike = dv.Cells["sNpcLike$28"].Value.ToString();
                    if (snpclike != "0" && snpclike != "")
                    {
                        string[] snpclikeArray = snpclike.Split('*');
                        for (int i = 0; i < snpclikeArray.Length; i++)
                        {
                            string[] str = snpclikeArray[i].Replace("(", "").Replace(")", "").Split(',');
                            sbExplain.AppendFormat("送给【{0}】增加{1}点好感  ", DataHelper.GetValue("NpcData", "sNpcName", "iID", str[0]), str[1]);
                            sbExplain.Append("\r\n");
                        }
                    }
                    sbExplain.Append("\r\n");
                    string sUseLimit = dv.Cells["sUseLimit$29"].Value.ToString();
                    if (sUseLimit != "0" && !string.IsNullOrEmpty(sUseLimit))
                    {
                        string[] useLimit = sUseLimit.Replace("(", "").Replace(")", "").Split(',');
                        if (useLimit.Length < 3)
                            return;
                        string name = string.Empty;
                        switch (useLimit[0])
                        {
                            case "0":
                                name = DataHelper.GetDicValue("MB", useLimit[1]);
                                break;
                            case "1":
                                name = DataHelper.GetDicValue("JBG", useLimit[1]);
                                break;
                            case "2":
                                name = DataHelper.GetDicValue("ZS", useLimit[1]);
                                break;
                            case "4":
                                name = DataHelper.GetDicValue("JY", useLimit[1]);
                                break;
                        }
                        sbExplain.AppendFormat("使用条件【{0}】{1}", name, useLimit[2]);
                    }
                    txtExplain.Text = sbExplain.ToString();
                    break;
            }
        }

        private void Almighty_KeyDown(object sender, KeyEventArgs e)
        {
            if (dg1.CurrentRow == null)
                return;
            int cellIndex = dg1.CurrentRow.Index;
            int sn = Convert.ToInt16(dg1.CurrentRow.Cells["indexSn"].Value);
            if (e.KeyCode == Keys.Up)
            {
                if (dg1.CurrentRow != null && dg1.CurrentRow.Selected)
                {
                    if (sn - 1 < 0)
                        return;
                    DataTable dt = ((DataView) dg1.DataSource).Table;
                    DataRow dr = dt.NewRow();
                    
                    dr.ItemArray = dt.Rows[sn - 1].ItemArray;
                    dt.Rows[sn - 1].ItemArray = dt.Rows[cellIndex].ItemArray;
                    dt.Rows[sn].ItemArray = dr.ItemArray;
                }
                BulidSn(DataHelper.XkfyData.Tables[_tbName]);
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (dg1.CurrentRow != null && dg1.CurrentRow.Selected)
                {
                    if (sn + 1 >= dg1.Rows.Count)
                        return;
                    DataTable dt = ((DataView) dg1.DataSource).Table;
                    DataRow dr = dt.NewRow();
                    dr.ItemArray = dt.Rows[sn + 1].ItemArray;
                    dt.Rows[sn + 1].ItemArray = dt.Rows[cellIndex].ItemArray;
                    dt.Rows[sn].ItemArray = dr.ItemArray;
                }
                BulidSn(DataHelper.XkfyData.Tables[_tbName]);
            }
            
        }

        private void dg1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dg1.Columns[e.ColumnIndex].Name;
            if (_dictTableExplain.ContainsKey(columnName))
            {
                if (gbQueryCon.Controls.Find(columnName, false).Length > 0)
                {
                    return;
                }
                lblTips.Text = "";
                ControlHelper.BuidQueryControl(gbQueryCon, _dictTableExplain[dg1.Columns[e.ColumnIndex].Name], toolTip1);
            }
            
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            if (dg1.CurrentRow != null)
            {
                int index = dg1.CurrentRow.Index;
                string id = dg1.Rows[index].Cells["iID"].Value.ToString();

                TalkDeBug td = new TalkDeBug(id);
                td.Show();
            }
        }

        private void btnEditTalkGroup_Click(object sender, EventArgs e)
        {
            ShowForm("EditGroup", "编辑对话组");
        }
    }
}
