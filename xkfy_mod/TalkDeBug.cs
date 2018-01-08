using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Helper;
using xkfy_mod.Personality;
using xkfy_mod.Utils;

namespace xkfy_mod
{
    public partial class TalkDeBug : Form
    {
        private readonly string _id;
        private int _index;
        private readonly ToolsHelper _tl = new ToolsHelper();
        public TalkDeBug(string id)
        {
            _id = id;
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); //双缓冲
        }

        /// <summary>
        /// 存储对话
        /// </summary>
        private DataRow[] drTalk;

        /// <summary>
        /// 存储事件
        /// </summary>
        private DataRow[] drDq;
        private DataRow[] drBa;
        /// <summary>
        /// 事件结束触发对话
        /// </summary>
        private string sEndAdd;
          
        private void TalkDeBug_Load(object sender, EventArgs e)
        {
            FileHelper.LoadTable("BattleAreaData");
            FileHelper.LoadTable("RewardData");
            FileHelper.LoadTable("TalkManager");
            FileHelper.LoadTable("DevelopQuestData");
            SetTalkDr(_id);
        }

        private void SetTalkDr(string iId)
        {
            //查询事件
            try
            {
                drDq = DataHelper.XkfyData.Tables["DevelopQuestData"].Select("iID='" + iId + "'");
                if (drDq.Length > 1)
                {
                    lblMsg.Text = $"iID为【{iId}】的数据在DevelopQuestData.txt 文件出现了多次,默认取第一条数据";
                }
                //事件类型
                string iType = drDq[0]["iType"].ToString();
                if (iType == "3")
                {
                    Satisfy();
                }
                else
                {
                    //下一事件ID
                    sEndAdd = drDq[0]["sEndAdd"].ToString();
                    //查询对话
                    drTalk = DataHelper.XkfyData.Tables["TalkManager"].Select("iQGroupID='" + drDq[0]["iArg3"] + "'");
                    _index = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetTalkDr2(string iId)
        {
            //查询事件
            try
            {
                DataRow[] drNewDq = DataHelper.XkfyData.Tables["DevelopQuestData"].Select("iID='" + iId + "'");
                if (drNewDq.Length == 0)
                {
                    drTalk = DataHelper.XkfyData.Tables["TalkManager"].Select("iQGroupID='" + iId + "'");
                    _index = 0;
                    sEndAdd = "0";
                }
                else
                {
                    drDq = drNewDq;
                    if (drDq.Length > 1)
                    {
                        lblMsg.Text = $"iID为【{iId}】的数据在DevelopQuestData.txt 文件出现了多次";
                    }

                    //事件类型
                    string iType = drDq[0]["iType"].ToString();
                    if (iType == "3")
                    {
                        Satisfy();
                    }
                    else
                    {
                        //下一事件ID
                        sEndAdd = drDq[0]["sEndAdd"].ToString();
                        //查询对话
                        drTalk = DataHelper.XkfyData.Tables["TalkManager"].Select("iQGroupID='" + drDq[0]["iArg3"] + "'");
                        _index = 0;
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Satisfy()
        {
            string[] striType = drDq[0]["iType"].ToString().Split(',');
            string[] striArg1 = drDq[0]["iArg1"].ToString().Split(',');
            string[] striArg2 = drDq[0]["iArg2"].ToString().Split(',');
            string[] striCondition = drDq[0]["iCondition"].ToString().Split(',');
            ToolsHelper tl = new ToolsHelper();
            string[] explain = tl.ExplainDevelopQuest(striCondition, striType, striArg1, striArg2);
            txtCondition.Text = explain[1];
            gbCondition.Visible = true;
        }

        private void SetTalk(int iTm)
        {
            try
            {
                //如果序号是最后一段对话
                if (iTm >= drTalk.Length)
                {
                    //如果有后续对话
                    if (sEndAdd != "0" || string.IsNullOrEmpty(sEndAdd))
                    {
                        //用第一个事件的结尾查找另一个事件的开端
                        drDq = DataHelper.XkfyData.Tables["DevelopQuestData"].Select("iID='" + sEndAdd + "'");
                        if (drDq.Length == 0)
                        {
                            //一般而言不会执行到这段
                            return;
                        }

                        string iType = drDq[0]["iType"].ToString();
                        if (iType == "3")
                        {
                            Satisfy();
                            return;
                        }
                        //触发战斗,优先执行战斗
                        if (drDq[0]["iDevelopType"].ToString() == "3")
                        {
                            drBa = DataHelper.XkfyData.Tables["BattleAreaData"].Select("iID$0='" + drDq[0]["iArg3"] + "'");
                            gbZd.Visible = true;
                            return;
                        }
                        //无条件就直接执行
                        SetTalkDr(drDq[0]["iArg3"].ToString());
                        iTm = 0;
                    }
                    else
                    {
                        //事件结束
                        txtNext.Text = @"结束";
                        //txtNext.Enabled = false;
                        return;
                    }
                }
                StringBuilder sbMsg = new StringBuilder();
                string sBackground = drDq[0]["sBackground"].ToString();
                string path = "";
                if (DataHelper.DictImages.ContainsKey(sBackground))
                {
                    path = DataHelper.DictImages[sBackground];
                }

                //画出背景图
                if (File.Exists(path))
                {
                    Image img = Image.FromFile(path);
                    panelBackground.BackgroundImage = img; 
                }
                else
                {
                    sbMsg.AppendFormat("没有找到名称为【{0}】的背景图片文件！", drDq[0]["sBackground"]);
                }

                //判断是否有选项
                if (drTalk[iTm]["bInFields"].ToString() == "1")
                {
                    for (int i = 1; i < 5; i++)
                    {
                        string sButtonName = drTalk[iTm]["sButtonName" + i].ToString();
                        if (sButtonName == "0" || string.IsNullOrEmpty(sButtonName))
                            continue;

                        switch (i)
                        {
                            case 1:
                                btnA.Visible = true;
                                btnA.Text = sButtonName;
                                btnA.Tag = drTalk[iTm]["sBArg" + i].ToString();
                                break;
                            case 2:
                                btnB.Visible = true;
                                btnB.Text = sButtonName;
                                btnB.Tag = drTalk[iTm]["sBArg" + i].ToString();
                                break;
                            case 3:
                                btnC.Visible = true;
                                btnC.Text = sButtonName;
                                btnC.Tag = drTalk[iTm]["sBArg" + i].ToString();
                                break;
                            case 4:
                                btnD.Visible = true;
                                btnD.Text = sButtonName;
                                btnD.Tag = drTalk[iTm]["sBArg" + i].ToString();
                                break;
                        }
                    }
                }

                //显示对话内容
                txtTalk.Text = drTalk[iTm]["sManager"].ToString();
                _curIndex = iTm;
                int iMasgPlace = int.Parse(drTalk[iTm]["iMasgPlace"].ToString());

                //画出人物位置
                Graphics g = panel1.CreateGraphics(); 
                g.Clear(panel1.BackColor);
//                Image backImage = Image.FromFile(path);
//                g.DrawImage(backImage, 0, 0, 256, 256);
                for (int i = 1; i < 9; i++)
                {
                    string sNpcQName = drTalk[iTm]["sNpcQName" + i].ToString();
                    if (sNpcQName == "0" || string.IsNullOrEmpty(sNpcQName))
                        continue;
                    DrawTalk(iMasgPlace);
                    path = "";
                    if (DataHelper.DictImages.ContainsKey(sNpcQName))
                    {
                        path = DataHelper.DictImages[sNpcQName];
                    }
                    if (File.Exists(path))
                    {
                        Image img = Image.FromFile(path);
                        DrawImage(img, i, g);
                    }
                    else
                        sbMsg.AppendFormat("\r\n没有找到名称为【{0}】的人物贴图文件！", sNpcQName);

                }
                g.Dispose();

                if (chkTalk.Checked)
                {
                    TalkManagerEdit msgForm;
                    bool wExist = _tl.CheckFormIsOpen("TalkManagerEdit");

                    if (!wExist)
                    {
                        msgForm = new TalkManagerEdit("debug");
                        msgForm.Show();
                        msgForm.BindData(drTalk[iTm]);
                    }
                    else
                    {
                        msgForm = (TalkManagerEdit)Application.OpenForms["TalkManagerEdit"];
                        if (msgForm != null)
                        {
                            msgForm.Activate();
                            msgForm.BindData(drTalk[iTm]);
                        }
                    }
                }
                lblMsg.Text = sbMsg.ToString();
                lblWin.Text += _tl.ExplainTalkManager(drTalk[iTm]);
                panelTalk.Visible = true;
                iTm++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 设置对话框的位置
        /// <summary>
        /// 设置对话框的位置
        /// </summary>
        /// <param name="iMasgPlace">对话框位置</param>
        private void DrawTalk(int iMasgPlace)
        {
            iMasgPlace = iMasgPlace + 1;
            int x = 0; 
            Image img1 = Image.FromFile(PathHelper.TalkBoxPathLeft); 
            Image img = Image.FromFile(PathHelper.TalkBoxPathRight);

            switch (iMasgPlace.ToString())
            {
                case "1":
                    panelTalk.BackgroundImage = img;
                    x = 0;
                    break;
                case "2":
                    panelTalk.BackgroundImage = img;
                    x = 105;
                    break;
                case "3":
                    panelTalk.BackgroundImage = img;
                    x = 213;
                    break;
                case "4":
                    panelTalk.BackgroundImage = img;
                    x = 310;
                    break;
                case "5":
                    panelTalk.BackgroundImage = img;
                    x = 575;
                    break;
                case "6":
                    panelTalk.BackgroundImage = img1;
                    x = 406;
                    break;
                case "7":
                    panelTalk.BackgroundImage = img1;
                    x = 516;
                    break;
                case "8":
                    panelTalk.BackgroundImage = img1;
                    x = 616;
                    break;
            }
            panelTalk.Location = new Point(x, panelTalk.Location.Y);
        }
        #endregion

        #region 画出人物贴图
        private void DrawImage(Image img,int position, Graphics g)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;//高质量显示图片
            int left = 0;
            switch (position)
            {
                case 1:
                    left = -75;
                    break;
                case 2:
                    left = 30;
                    break;
                case 3:
                    left = 135;
                    break;
                case 4:
                    left = 240;
                    break;
                case 5:
                    left = 501;
                    break;
                case 6:
                    left = 606;
                    break;
                case 7:
                    left = 711;
                    break;
                case 8:
                    left = 815;
                    break;
            }
            g.DrawImage(img, left, 0, 256, 256);//将图片画在游戏区
        }
        #endregion

        int _curIndex = -1;
        private void txtNext_Click(object sender, EventArgs e)//iArg3
        {
            lbltkMsg.Text = "";
            txtNext.Text = @"下一步";
            SetTalk(_index);
            _index++;
        }

        private void btnWin_Click(object sender, EventArgs e)
        {
            DataRow[] drRd;
            if (((Button)sender).Name == "btnWin")
            {
                 drRd = DataHelper.XkfyData.Tables["RewardData"].Select("iRID='" + drBa[0]["sReward$6"] + "'");
            }
            else
            {
                 drRd = DataHelper.XkfyData.Tables["RewardData"].Select("iRID='" + drBa[0]["sReward$7"] + "'");
            }

            string[] sRewardData = drRd[0]["sRewardData"].ToString().Split('*');
          
            string rewardData = ExplainHelper.ExplainRewardData(sRewardData);
            string talkid = StringUtils.GetId(rewardData);
            
            lblWin.Text += rewardData.Replace("#","");
            if (talkid != "")
            {
                SetTalkDr(talkid);
            }
            //txtNext.Enabled = true;
            gbZd.Visible = false;
        }
       
        private void btnA_Click(object sender, EventArgs e)
        {
            string sbArg = ((Button)sender).Tag.ToString();
            if (sbArg.IndexOf("|", StringComparison.Ordinal) != -1)
            {
                sbArg = sbArg.Split('|')[0];
            }
            SetTalkDr2(sbArg);
            btnA.Visible = false;
            btnB.Visible = false;
            btnC.Visible = false;
            btnD.Visible = false;
            SetTalk(_index);
            txtNext.Text = @"下一步";
            _index++;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if(_curIndex != -1)
                SetTalk(_curIndex);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            gbCondition.Visible = false;
            txtCondition.Text = "";
            if (((Button)sender).Name == "btnYes")
            {
                SetTalkDr(drDq[0]["iArg3"].ToString());
                //index = 0;
            }
            else
            {
                SetTalkDr(drDq[0]["sEndAdd"].ToString());
                //index = 0;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lbltkMsg.Text = "";
                drTalk[_curIndex]["rowState"] = "0";
                drTalk[_curIndex]["sManager"] = txtTalk.Text;
                lbltkMsg.Text = @"修改成功";
            }
            catch (Exception ex)
            {
                lbltkMsg.Text = ex.Message;
            }
        }
    }
}
