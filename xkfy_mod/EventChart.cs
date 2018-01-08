using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Entity;
using xkfy_mod.Helper;
using xkfy_mod.Utils;
using xkfy_mod.XML;

namespace xkfy_mod
{
    public partial class EventChart : Form
    {
        public EventChart()
        {
            InitializeComponent();
        }

        List<EventData> eventData = new List<EventData>();
        Dictionary<string, string> isHaveEvent = new Dictionary<string, string>();

        private void EventChart_Load(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            dg1.DataSource = bs;
            bs.DataSource = DataHelper.DropDownListDict["Public.HuiHe"];
            
            FileHelper.LoadTable("BattleAreaData");
            FileHelper.LoadTable("RewardData");
            FileHelper.LoadTable("TalkManager");
            FileHelper.LoadTable("DevelopQuestData");

            string path = Application.StartupPath + "\\CustomData\\EventData.xml";
            eventData = XmlHelper.XmlDeserializeFromFile<List<EventData>>(path, Encoding.UTF8);
            if (eventData.Count <= 0)
            {
                JieXi();
                eventData = XmlHelper.XmlDeserializeFromFile<List<EventData>>(path, Encoding.UTF8);
            }
            else
            {
                foreach (EventData ed in eventData)
                {
                    if(ed.Round == null)
                        continue;
                    if (!isHaveEvent.ContainsKey(ed.Round))
                    {
                        isHaveEvent.Add(ed.Round,ed.EventId);
                    }
                }
            }
            SetDg1BgColor();
        }

        private void SetDg1BgColor()
        {
            foreach (DataGridViewRow dr in dg1.Rows)
            {
                if(dr.Cells["col_key"].Value == null)
                    return;
                if (isHaveEvent.ContainsKey(dr.Cells["col_key"].Value.ToString()))
                {
                    dr.DefaultCellStyle.BackColor = Color.SkyBlue;
                }
            }
        }

        private void JieXi()
        {
            List<EventData> listEvent = new List<EventData>();
            foreach (DataRow dr in DataHelper.XkfyData.Tables["DevelopQuestData"].Rows)
            {
                string iType = dr["iType"].ToString();
                string[] iArg1 = dr["iArg1"].ToString().Split(',');
                string[] iArg2 = dr["iArg2"].ToString().Split(',');
                string[] iCondition = dr["iCondition"].ToString().Split(',');

                EventData ed = new EventData();
                ed.EventId = dr["iID"].ToString();
                for (int i = 0; i < iCondition.Length; i++)
                {
                    if (iCondition[i] == "7")
                    {
                        ed.Round = iArg1[i];
                    }

                    if (iCondition[i] == "9")
                    {
                        ed.ParantId = iArg1[i];
                    }
                }
                if (!string.IsNullOrWhiteSpace(ed.ParantId) || !string.IsNullOrWhiteSpace(ed.Round))
                {
                    listEvent.Add(ed);
                }
            }
            string path = Application.StartupPath + "\\CustomData\\EventData.xml";
            XmlHelper.XmlSerializeToFile(listEvent, path, Encoding.UTF8);
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            JieXi();
        }

        private void CreateNode(String Id)
        {
            treeView1.Nodes.Clear();
            DataRow[] drs = DataHelper.XkfyData.Tables["DevelopQuestData"].Select("iid='" + Id + "'");
            foreach (DataRow dr in drs)
            {
                dicEventId.Clear();
                TreeNode node = new TreeNode();
                node.Text = "事件开始";
                node.Name = "开始节点";
                treeView1.Nodes.Add(node);
                SetParatNodes(dr, node, "");
            }
        }
         
        
        Dictionary<string, string> dicEventId = new Dictionary<string, string>();

        private void SetParatNodes(DataRow drDq, TreeNode pNode, string Text)
        {
            string iId = drDq["iId"].ToString();
            string iDevelopType = drDq["iDevelopType"].ToString();
            //事件类型
            string iType = drDq["iType"].ToString();

            TreeNode node4 = new TreeNode();
            node4.Text = "【事件】" + Text + "[" + iId + "]";
            node4.Name = "[A]" + iId;
            pNode.Nodes.Add(node4);


            //判断是否满足条件
            if (iType == "3")
            {
                //string[] striType = drDq["iType"].ToString().Split(',');
                //string[] striArg1 = drDq["iArg1"].ToString().Split(',');
                //string[] striArg2 = drDq["iArg2"].ToString().Split(',');
                //string[] striCondition = drDq["iCondition"].ToString().Split(',');

                //string[] Explain = tl.ExplainDevelopQuest(striCondition, striType, striArg1, striArg2);
                //string txtCondition = Explain[1];

                TreeNode node1 = new TreeNode();
                node1.Text = "【条件】" + Text + "[" + iId + "]";
                node1.Name = "[B]" + iId;
                node4.Nodes.Add(node1);

                TreeNode nodeConTrue = new TreeNode();
                nodeConTrue.Text = "【满足条件】" + Text + "[" + iId + "]";
                nodeConTrue.Name = iId;
                node1.Nodes.Add(nodeConTrue);
                //满足条件
                SetParatNodes(getDqTb(drDq["iArg3"].ToString()), nodeConTrue, "");

                TreeNode nodeConFalse = new TreeNode();
                nodeConFalse.Text = "【不满足条件】" + Text + "[" + iId + "]";
                nodeConFalse.Name = iId;
                node1.Nodes.Add(nodeConFalse);
                //不满足条件
                SetParatNodes(getDqTb(drDq["sEndAdd"].ToString()), nodeConFalse, "不满足条件");
            }
            else
            {
                //触发战斗,优先执行战斗
                if (iDevelopType == "3")
                {
                    TreeNode node = new TreeNode();
                    node.Text = "【战斗】" + Text + "[" + iId + "]";
                    node.Name = "[C]" + iId;
                    node4.Nodes.Add(node);

                    DataRow[] drBa = DataHelper.XkfyData.Tables["BattleAreaData"].Select("iID$0='" + drDq["iArg3"] + "'");

                    DataRow[] drRd = null;
                    drRd = DataHelper.XkfyData.Tables["RewardData"].Select("iRID='" + drBa[0]["sReward$6"] + "'");

                    string[] sRewardData = drRd[0]["sRewardData"].ToString().Split('*');

                    string rewardData = ExplainHelper.ExplainRewardData(sRewardData);
                    string talkid = StringUtils.GetId(rewardData);

                    TreeNode nodeWin = new TreeNode();
                    nodeWin.Text = "【胜利】" + Text + "[" + iId + "]";
                    nodeWin.Name = iId;
                    node.Nodes.Add(nodeWin);

                    //战斗胜利
                    SetParatNodes(getDqTb(talkid), nodeWin, "战斗胜利");

                    //战斗失败
                    drRd = DataHelper.XkfyData.Tables["RewardData"].Select("iRID='" + drBa[0]["sReward$7"] + "'");
                    sRewardData = drRd[0]["sRewardData"].ToString().Split('*');

                    rewardData = ExplainHelper.ExplainRewardData(sRewardData);
                    talkid = StringUtils.GetId(rewardData);

                    TreeNode nodeFail = new TreeNode();
                    nodeFail.Text = "【失败】" + Text + "[" + iId + "]";
                    nodeFail.Name = iId;
                    node.Nodes.Add(nodeFail);
                    SetParatNodes(getDqTb(talkid), nodeFail, "");
                }

                string iArg3 = drDq["iArg3"].ToString();
                //下一事件ID
                string sEndAdd = drDq["sEndAdd"].ToString();
                AddTalkNode(node4, iArg3, sEndAdd,"");
            }
        }


        private void AddTalkNode(TreeNode pNode, string iArg3,string sEndAdd,String Text)
        {
            string iId = iArg3;
            if(!dicEventId.ContainsKey(iArg3))
                dicEventId.Add(iArg3, iArg3);
            bool bInFields = false;
            
            //查询当前事件的对话,判断对话中是否存在可跳转的分支路线
            DataRow[] drTalks = DataHelper.XkfyData.Tables["TalkManager"].Select("iQGroupID='" + iArg3 + "'");

            if (drTalks.Length > 0)
            {
                TreeNode nodeTalk = new TreeNode();
                nodeTalk.Text = "【对话】[" + iArg3 + "]";
                nodeTalk.Name = "[D]" + iArg3;
                pNode.Nodes.Add(nodeTalk);

                DataRow drTalk = drTalks[drTalks.Length - 1];
                if (drTalk["bInFields"].ToString() == "1")
                {
                    TreeNode node3 = new TreeNode();
                    node3.Text = "【对话选项】" + Text + "[" + iArg3 + "]";
                    node3.Name = iArg3;
                    nodeTalk.Nodes.Add(node3);

                    for (int i = 1; i < 5; i++)
                    {
                        string sButtonName = drTalk["sButtonName" + i].ToString();
                        if (sButtonName == "0" || string.IsNullOrEmpty(sButtonName))
                            continue;

                        string sbArg = drTalk["sBArg" + i].ToString();
                        if (sbArg.IndexOf("|") != -1)
                        {
                            sbArg = sbArg.Split('|')[0];
                        }

                        TreeNode nodeLoop = new TreeNode();
                        nodeLoop.Name = sbArg;
                        if (dicEventId.ContainsKey(sbArg))
                        {
                            nodeLoop.Text = "【选项" + i + "】【循环】" + sButtonName + "[" + sbArg + "]";
                            node3.Nodes.Add(nodeLoop);
                        }
                        else
                        {
                            nodeLoop.Text = "【选项" + i + "】" + sButtonName + "[" + sbArg + "]";
                            node3.Nodes.Add(nodeLoop);
                            //判断当前选项是否有事件关联,如果有调用事件递归树,否则递归Talk
                            DataRow[] dr = getDqTb2(sbArg);
                            //大于0 代表是事件,否则代表的是继续跳转对话
                            if (dr.Length > 0)
                            {
                                SetParatNodes(getDqTb(sbArg), nodeLoop, "");
                            }
                            else
                            {
                                AddTalkNode(nodeLoop, sbArg, "", "");
                            }
                        }
                    }
                }
            }

            //如果有后续对话
            if (sEndAdd != "0" && !string.IsNullOrWhiteSpace(sEndAdd))
            {
                //用第一个事件的结尾查找另一个事件的开端
                SetParatNodes(getDqTb(sEndAdd), pNode, "");
            }
            else
            {
                List<EventData> hxEve = eventData.Where(f => f.Round != null && f.ParantId == iId).ToList();
                
                foreach (EventData edHx in hxEve)
                {
                    SetParatNodes(getDqTb(edHx.EventId), pNode, "后续事件");
                }
            }
        }

        private void ToFrist(string sEndAdd, TreeNode node, string iId)
        {

        }

        private DataRow getDqTb(string id)
        {
            return DataHelper.XkfyData.Tables["DevelopQuestData"].Select("iID='" + id + "'")[0];
        }

        private DataRow[] getDqTb2(string id)
        {
            return DataHelper.XkfyData.Tables["DevelopQuestData"].Select("iID='" + id + "'");
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = e.Node;
            txtHuiHe.Text = currentNode.Name;
            if(currentNode.Name == "事件开始")
                return;
            
            string id = currentNode.Name.Substring(3);
            string type = currentNode.Name.Substring(0, 3);
            switch (type)
            {
                case "[A]":
                case "[B]":
                    ExplainEvent(id);
                    break;
                case "[C]":
                    break;
                case "[D]":
                    break;
            }
        }

        ToolsHelper tl = new ToolsHelper();
        private void ExplainEvent(string id)
        {
            DataRow dr = getDqTb(id);
            string[] striType = dr["iType"].ToString().Split(',');
            string[] striArg1 = dr["iArg1"].ToString().Split(',');
            string[] striArg2 = dr["iArg2"].ToString().Split(',');
            string[] striCondition = dr["iCondition"].ToString().Split(',');
            
            string[] Explain = tl.ExplainDevelopQuest(striCondition, striType, striArg1, striArg2);
            txtRemark.Text = Explain[0] + "\r\n\r\n" + Explain[1];
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            try
            {
                long a = Convert.ToInt64(txtHuiHe.Text);
                TalkDeBug td = new TalkDeBug(txtHuiHe.Text);
                td.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("事件ID不对");
            }
            
        }

        private void dg1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dg1.CurrentRow == null)
                    return;
                if (dg1.CurrentRow.Cells[0].Value == null)
                    return;
                string key = dg1.CurrentRow.Cells["col_key"].Value.ToString();

                var result = eventData.Where(f => f.Round == key);
                foreach (EventData eData in result)
                {
                    CreateNode(eData.EventId);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
