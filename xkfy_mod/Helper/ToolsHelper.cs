using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Entity;
using xkfy_mod.Utils;
using xkfy_mod.XML;

namespace xkfy_mod.Helper
{
    public class ToolsHelper
    {
        #region 解释物品奖励

        /// <summary>
        /// 解释物品奖励
        /// </summary>
        /// <param name="sRewardData">列</param>
        /// <param name="dt">数据源</param>
        /// <returns></returns>
        public string ExplainRewardData(string[] sRewardData, DataTable dt)
        {
            FileHelper.LoadTable("String_table");
            DataTable str_t = DataHelper.XkfyData.Tables["String_table"];

            StringBuilder sbExplain = new StringBuilder();

            foreach (string rw in sRewardData)
            {
                string[] reward = rw.Replace(")", "").Replace("(", "").Split(',');
                if (reward.Length < 4)
                {
                    sbExplain.Append("本行数据有误,请检查!");
                    continue;
                }

                string jl = reward[0];
                string jlType = reward[1];
                string value = reward[2];
                string str_msg = reward[3];

                DataRow[] driJl = dt.Select("Type='Reward' and Value='" + jl + "'");
                if (driJl.Length == 0)
                {
                    sbExplain.Append("未在配置文件中找到对应的配置,请联系作者");
                    continue;
                }

                if (driJl.Length > 1)
                {
                    sbExplain.Append("配置文件有误，请联系作者！");
                    continue;
                }

                string tempStr = string.Empty;
                switch (jl)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "24":
                    case "30":
                    case "32":
                        DataRow[] drjlType =
                            dt.Select("Type='" + driJl[0]["ChildType"].ToString() + "' and Value='" + jlType + "'");
                        if (drjlType.Length == 0)
                            continue;
                        DataRow dr_str = str_t.Select("iID='" + str_msg + "'")[0];
                        tempStr = dr_str["sString"].ToString();
                        sbExplain.AppendFormat(tempStr + "", value);
                        break;
                    case "6":
                        sbExplain.AppendFormat("【{0}】对你好感增加了{1}点  ",
                            DataHelper.GetValue("NpcData", "sNpcName", "iID", jlType), value);
                        break;
                    case "7":
                        sbExplain.AppendFormat("获得物品【{0}】",
                            DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", jlType));
                        break;
                    case "13":
                        sbExplain.AppendFormat("获得称号【{0}】", DataHelper.GetValue("TitleData", "sTitle", "iID", jlType));
                        break;
                    case "16":
                        sbExplain.AppendFormat("获得套路【{0}】",
                            DataHelper.GetValue("RoutineData", "sRoutineName", "iRoutineID", jlType));
                        break;
                    case "17":
                        sbExplain.AppendFormat("获得内功【{0}】", DataHelper.GetValue("BattleNeigong", "Name", "ID", jlType));
                        break;
                    case "18":
                        sbExplain.AppendFormat("增加经历【{0}】", DataHelper.GetValue("JourneyData", "sTip", "iID", jlType));
                        break;
                    case "19":
                        sbExplain.AppendFormat("增加【{0}】说明",
                            DataHelper.GetValue("AbilityBookData", "sAbilityID", "iID", jlType));
                        break;
                    case "20":
                        sbExplain.AppendFormat("触发动画{0}", rw);
                        break;
                    case "21":
                        sbExplain.AppendFormat("触发场景对话,对话ID为【{0}】", value);
                        break;
                    case "14":
                    case "27":
                    case "28":
                        sbExplain.AppendFormat("【{0}】增加了【{1}】", driJl[0]["text"], value);
                        break;
                    case "31":
                        string hh = value + "回合对应的年月未配置，请联系作者补全！";
                        if (DataHelper.DropDownListDict["Public.HuiHe"].ContainsKey(value))
                        {
                            hh = DataHelper.DropDownListDict["Public.HuiHe"][value];
                        }
                        sbExplain.AppendFormat("回到逍遥谷，{0}", hh);
                        break;
                    case "38":
                        sbExplain.AppendFormat("开启前传章节【{0}】", str_msg);
                        break;
                    case "39":
                        sbExplain.AppendFormat("时间过去【{0}】分钟", jlType);
                        break;
                    case "40":
                        sbExplain.AppendFormat("【{0}】加入队伍",
                            DataHelper.GetValue("BattleCharacterData", "Name", "CharID", value));
                        break;
                    case "42":
                        sbExplain.AppendFormat("内功【{0}】升到10级",
                            DataHelper.GetValue("BattleNeigong", "Name", "ID", jlType));
                        break;
                    default:
                        sbExplain.AppendFormat("弱鸡作者不理解{0}代表什么意思", rw);
                        break;
                }
                sbExplain.Append("\r\n");
            }
            return sbExplain.ToString();
        }

        #endregion

        #region 解释对话

        /// <summary>
        /// 解释对话
        /// </summary>
        /// <param name="dv">DataGridViewRow 类型的列</param>
        /// <returns></returns>
        public string ExplainTalkManager(DataGridViewRow dv)
        {
            FileHelper.LoadTable("String_table");
            DataTable dt = DataHelper.DdlDataSet.Tables["TalkManager.iGtype"];
            StringBuilder sbExplain1 = new StringBuilder();
            DataTable str_t = DataHelper.XkfyData.Tables["String_table"];
            for (int i = 1; i < 4; i++)
            {
                string iGtype = dv.Cells["iGtype" + i].Value.ToString();
                string sGArg = dv.Cells["sGArg" + i].Value.ToString();
                string iAmount = dv.Cells["iAmount" + i].Value.ToString();
                string sStringID = dv.Cells["sStringID" + i].Value.ToString();

                if (string.IsNullOrEmpty(iGtype) ||
                    (iGtype == "0" && sGArg == "0" && iAmount == "0" && sStringID == "0"))
                    continue;

                DataRow[] driGtype = dt.Select("Type='iGtype' and Value='" + iGtype + "'");
                if (driGtype.Length == 0)
                {
                    sbExplain1.AppendFormat("iGtype{0}的值,未在配置文件中找到对应的配置！\r\n", i);
                    continue;
                }

                if (driGtype.Length > 1)
                {
                    sbExplain1.AppendFormat("iGtype{0}的配置文件有误，请联系作者！\r\n", i);
                    continue;
                }

                sbExplain1.AppendFormat("【{0}】  ", driGtype[0]["Text"]);
                DataRow[] drsGArg = null;
                switch (iGtype)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "24":
                    case "30":
                        drsGArg =
                            dt.Select("Type='" + driGtype[0]["ChildType"].ToString() + "' and Value='" + sGArg + "'");
                        if (drsGArg.Length == 0)
                            continue;
                        string tempStr = string.Empty;
                        if (sStringID != "0")
                        {
                            tempStr = DataHelper.GetValue("String_table", "sString", "iID", sStringID);
                            sbExplain1.AppendFormat(tempStr, iAmount, drsGArg[0]["Text"].ToString(), iAmount);
                        }
                        else
                        {
                            sbExplain1.AppendFormat("{0}{1}", drsGArg[0]["Text"].ToString(), iAmount);
                        }
                        break;
                    case "6":
                        sbExplain1.AppendFormat("【{0}】对你好感增加了{1}点  ",
                            DataHelper.GetValue("NpcData", "sNpcName", "iID", sGArg), iAmount);
                        break;
                    case "7":
                        sbExplain1.AppendFormat("获得物品【{0}】",
                            DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", sGArg));
                        break;
                    case "13":
                        sbExplain1.AppendFormat("获得称号【{0}】", DataHelper.GetValue("TitleData", "sTitle", "iID", sGArg));
                        break;
                    case "14":
                    case "26":
                    case "29":
                        tempStr = DataHelper.GetValue("String_table", "sString", "iID", sStringID);
                        sbExplain1.AppendFormat(tempStr, iAmount);
                        break;
                    case "16":
                        sbExplain1.AppendFormat("获得套路 【{0}】",
                            DataHelper.GetValue("RoutineData", "sRoutineName", "iRoutineID", sGArg));
                        break;
                    case "17":
                        sbExplain1.AppendFormat("获得内功 【{0}】", DataHelper.GetValue("BattleNeigong", "Name", "ID", sGArg));
                        break;
                    case "18":
                        sbExplain1.AppendFormat("开启新经历 【{0}】", DataHelper.GetValue("JourneyData", "sTip", "iID", sGArg));
                        break;
                    case "19":
                        sbExplain1.AppendFormat("【{0}】",
                            DataHelper.GetValue("AbilityBookData", "sAbilityID", "iID", sGArg));
                        break;
                    case "34":
                        sbExplain1.AppendFormat("{0}", DataHelper.GetValue("DevelopBtnData", "xRemark", "iBtnID", sGArg));
                        break;
                    default:
                        sbExplain1.AppendFormat("弱鸡作者不理解iGtype={0}的意思", iGtype);
                        break;
                }
                sbExplain1.Append("\r\n");
            }
            return sbExplain1.ToString();
        }

        /// <summary>
        /// 解释对话
        /// </summary>
        /// <param name="dv">Datarow 类型的列</param>
        /// <returns></returns>
        public string ExplainTalkManager(DataRow dv)
        {
            DataTable dt = DataHelper.DdlDataSet.Tables["TalkManager.iGtype"];
            StringBuilder sbExplain1 = new StringBuilder();
            for (int i = 1; i < 4; i++)
            {
                string iGtype = dv["iGtype" + i].ToString();
                string sGArg = dv["sGArg" + i].ToString();
                string iAmount = dv["iAmount" + i].ToString();
                string sStringID = dv["sStringID" + i].ToString();

                if (string.IsNullOrEmpty(iGtype) ||
                    (iGtype == "0" && sGArg == "0" && iAmount == "0" && sStringID == "0"))
                    continue;

                DataRow[] driGtype = dt.Select("Type='iGtype' and Value='" + iGtype + "'");
                if (driGtype.Length == 0)
                {
                    sbExplain1.AppendFormat("iGtype{0}的值,未在配置文件中找到对应的配置！\r\n", i);
                    continue;
                }

                if (driGtype.Length > 1)
                {
                    sbExplain1.AppendFormat("iGtype{0}的配置文件有误，请联系作者！\r\n", i);
                    continue;
                }

                sbExplain1.AppendFormat("【{0}】  ", driGtype[0]["Text"]);
                DataRow[] drsGArg = null;
                switch (iGtype)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "24":
                    case "30":
                        drsGArg =
                            dt.Select("Type='" + driGtype[0]["ChildType"].ToString() + "' and Value='" + sGArg + "'");
                        if (drsGArg.Length == 0)
                            continue;
                        string tempStr = string.Empty;
                        if (sStringID != "0")
                        {
                            tempStr = DataHelper.GetValue("String_table", "sString", "iID", sStringID);
                            sbExplain1.AppendFormat(tempStr, iAmount, drsGArg[0]["Text"].ToString(), iAmount);
                        }
                        else
                        {
                            sbExplain1.AppendFormat("{0}{1}", drsGArg[0]["Text"].ToString(), iAmount);
                        }
                        break;
                    case "6":
                        sbExplain1.AppendFormat("【{0}】对你好感增加了{1}点  ",
                            DataHelper.GetValue("NpcData", "sNpcName", "iID", sGArg), iAmount);
                        break;
                    case "7":
                        sbExplain1.AppendFormat("获得物品【{0}】",
                            DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", sGArg));
                        break;
                    case "13":
                        sbExplain1.AppendFormat("获得称号【{0}】", DataHelper.GetValue("TitleData", "sTitle", "iID", sGArg));
                        break;
                    case "14":
                    case "26":
                    case "29":
                        tempStr = DataHelper.GetValue("String_table", "sString", "iID", sStringID);
                        sbExplain1.AppendFormat(tempStr, iAmount);
                        break;
                    case "16":
                        sbExplain1.AppendFormat("获得套路 【{0}】",
                            DataHelper.GetValue("RoutineData", "sRoutineName", "iRoutineID", sGArg));
                        break;
                    case "17":
                        sbExplain1.AppendFormat("获得内功 【{0}】", DataHelper.GetValue("BattleNeigong", "Name", "ID", sGArg));
                        break;
                    case "18":
                        sbExplain1.AppendFormat("开启新经历 【{0}】", DataHelper.GetValue("JourneyData", "sTip", "iID", sGArg));
                        break;
                    case "19":
                        sbExplain1.AppendFormat("【{0}】",
                            DataHelper.GetValue("AbilityBookData", "sAbilityID", "iID", sGArg));
                        break;
                    case "34":
                        sbExplain1.AppendFormat("{0}", DataHelper.GetValue("DevelopBtnData", "xRemark", "iBtnID", sGArg));
                        break;
                    default:
                        sbExplain1.AppendFormat("弱鸡作者不理解iGtype={0}的意思", iGtype);
                        break;
                }
                sbExplain1.Append("\r\n");
            }
            return sbExplain1.ToString();
        }

        #endregion

        #region 解释游戏事件

        /// <summary>
        /// 解释游戏事件
        /// </summary>
        /// <param name="striCondition"></param>
        /// <param name="striType"></param>
        /// <param name="striArg1"></param>
        /// <param name="striArg2"></param>
        /// <returns></returns>
        public string[] ExplainDevelopQuest(string[] striCondition, string[] striType, string[] striArg1,
            string[] striArg2)
        {
            string[] rtValue = new string[2];

            string Explain = "";
            DataTable dt = DataHelper.DdlDataSet.Tables["TalkManager.iGtype"];
            Dictionary<string, string> diciType = DataHelper.DropDownListDict["DevelopQuestData.iType"];


            if (!diciType.ContainsKey(striType[0]))
            {
                Explain = string.Format("弱鸡作者不明白{0}代表的意思", striType[0]);
            }
            else
            {
                Explain = diciType[striType[0]];
            }


            string Explain1 = "";
            StringBuilder sbExplain1 = new StringBuilder();
            if (striCondition.Length == striArg1.Length && striArg1.Length == striArg2.Length)
            {

                for (int i = 0; i < striCondition.Length; i++)
                {
                    string iCondition = striCondition[i];
                    string iArg1 = striArg1[i];
                    string iArg2 = striArg2[i];
                    DataRow[] drCon = dt.Select("Type='isNull' and Value='" + iCondition + "'");
                    if (drCon.Length == 0)
                        continue;
                    Explain1 += drCon[0]["Text"].ToString();
                    //sbExplain1.Append(drCon[0]["Text"].ToString() + "  ");
                    switch (striCondition[i])
                    {
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                            DataRow drAgr1 =
                                dt.Select("Type='" + drCon[0]["ChildType"] + "' and Value='" + iArg1 + "'")[0];
                            sbExplain1.AppendFormat("{0}:{1}  ", drAgr1["Text"].ToString(), striArg2[i]);
                            break;
                        case "7":
                            sbExplain1.AppendFormat("最早触发回合:{0} 最晚触发回合:{1}  ", DataHelper.GetHuiHe(striArg1[i]),
                                DataHelper.GetHuiHe(striArg2[i]));
                            break;
                        case "2":
                            sbExplain1.AppendFormat("{1}好感度:{0}  ", iArg2,
                                DataHelper.GetValue("NpcData", "sNpcName", "iID", iArg1));
                            break;
                        case "9":
                        case "14":
                        case "13":
                        case "113":
                            sbExplain1.AppendFormat("{0}【{1}】  ", drCon[0]["Text"], iArg1);
                            break;
                        case "8":
                        case "10":
                            sbExplain1.AppendFormat("{0}【{1}】  ", drCon[0]["Text"], iArg2);
                            break;
                        case "11":
                            sbExplain1.AppendFormat("拥有物品【{0}】  ",
                                DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", iArg1));
                            break;
                        default:
                            sbExplain1.AppendFormat("弱鸡作者不明白【{0}】的意思  ", striCondition[i]);
                            break;
                    }

                }
                rtValue[0] = "触发地点 : " + Explain;
                rtValue[1] = "触发条件 : " + sbExplain1.ToString();
            }
            else
            {
                MessageBox.Show("数据格式有错误,触发条件和值不匹配");
            }

            return rtValue;
        }

        #endregion

        #region 解释内功代码

        public string ExplainNeiGong(string id)
        {
            DataRow[] row = DataHelper.XkfyData.Tables["BattleNeigong_D"].Select("ID='" + id + "'");
            StringBuilder sbExplain = new StringBuilder();

            foreach (DataRow item in row)
            {
                string explain = "";
                string tempExplain = string.Empty;
                string strAccumulate = string.Empty;

                string accumulate = item["Accumulate"].ToString();
                string effectType = item["EffectType"].ToString();
                string value1 = item["value1"].ToString();
                string value2 = item["value2"].ToString();
                string percent = item["percent"].ToString() == "1" ? "%":"点";

                var lists = FileUtils.ReadConfig<Annotation>(PathHelper.GetExplicatePath("BattleNeigong"));

                var result = lists.Where(l => l.Column == "Accumulate" && l.Code == accumulate).ToList();


                //通过key从Dictionary读取出对应的Value
                tempExplain = DataHelper.DropDownListDict[Const.BaNeEffecttype].ContainsKey(effectType)
                    ? DataHelper.DropDownListDict[Const.BaNeEffecttype][effectType]
                    : "";

                //如果前四项为0，代表没有效果
                if (effectType == "" || (effectType == "0" && accumulate == "0" && value1 == "0" && value2 == "0"))
                {
                    continue;
                } 

                string conditionId = "";
                switch (accumulate)
                {
                    case "0":
                        strAccumulate = "固定增加";
                        break;
                    case "1":
                        strAccumulate = "累进增加";
                        break;
                    case "4":
                        strAccumulate = "";
                        break;
                    case "5":
                        strAccumulate = $"提高周遭{item["ValueLimit"]}格友军【{tempExplain}】{value1}{percent}";
                        break;
                    case "7":
                        conditionId = (value1 == "0" || string.IsNullOrEmpty(value1)) ? value2 : value1;
                        strAccumulate = string.Format("发动【{0}】后，触发【{1}】", tempExplain, getCondition(conditionId));
                        sbExplain.AppendFormat("{0}\r\n", strAccumulate);
                        continue;
                    case "9":
                        strAccumulate = string.Format("降低周遭{0}格敌军【{1}】{2}{3}", item["ValueLimit"].ToString(),
                            tempExplain, value1, percent);
                        sbExplain.AppendFormat("{0}\r\n", strAccumulate);
                        continue;
                    case "10":
                        conditionId = (value1 == "0" || string.IsNullOrEmpty(value1)) ? value2 : value1;
                        string str_gd = (value1 == "0" || string.IsNullOrEmpty(value1)) ? "高于" : "低于";
                        strAccumulate = string.Format("【{0}】{4}{1}{2}触发【{3}】", tempExplain,
                            item["ValueLimit"].ToString(), percent, getCondition(conditionId), str_gd);
                        sbExplain.AppendFormat("{0}\r\n", strAccumulate);
                        continue;
                }

                string tempa = item["ValueLimit"].ToString();
                tempa = tempa != "0" ? tempa + "个" : "所有";
                switch (item["EffectType"].ToString())
                {
                    case "0":
                    case "1":
                        strAccumulate = "每回合恢复";
                        explain = strAccumulate + item["value1"].ToString() + percent + "【" + tempExplain + "】  最多" +
                                  item["ValueLimit"].ToString() + percent;
                        break;
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                        if (accumulate == "5")
                        {
                            explain = strAccumulate;
                            break;
                        }
                        explain = strAccumulate + item["value1"] + percent + "【" + tempExplain + "】  最多" +
                                  item["ValueLimit"].ToString() + percent;
                        break;
                    case "10":
                    case "24":
                    case "25":
                    case "29":
                    case "30":
                    case "33":
                        explain = $"【{tempExplain}】";
                        break;
                    case "9":
                        explain = "移动范围 +" + item["ValueLimit"].ToString() + "";
                        break;
                    case "15":
                    case "16":
                    case "17":
                    case "18":
                        explain = $"【{tempExplain}】{value1}{percent}  最多{item["ValueLimit"]}{percent}";
                        break;
                    case "20":
                        explain = $"每回合解除【{tempa}】负面状态";
                        break;
                    case "22":
                        explain = $"行动等级 +{item["ValueLimit"].ToString()} 神行";
                        break;
                    case "23":
                        explain = "连斩：击杀敌人後可再行动";
                        break;
                    case "26":
                        strAccumulate = $"保护周遭{item["ValueLimit"].ToString()}格同伴";
                        sbExplain.Append(strAccumulate);
                        break;
                    case "31":
                        explain = "毒体：百毒不侵";
                        break;
                    case "32":
                        sbExplain.AppendFormat("减少{0}{1} - {2}{3}伤害", value1, percent, value2, percent);
                        break;
                    default:
                        sbExplain.AppendFormat("弱鸡作者不明白EffectType={0} 是什么意思！", item["EffectType"].ToString());
                        break;

                }
                sbExplain.Append(explain);
                sbExplain.Append("\r\n");
            }
            return sbExplain.ToString();
        }

        private string getCondition(string conid)
        {
            return DataHelper.GetValue("BattleCondition", "CondName", "ConditionID", conid);
        }

        #endregion

        #region 解释招式伤害范围

        public string ExplainRoutineData(DataGridViewRow dv)
        {
            FileHelper.LoadTable("BattleAbility");
            StringBuilder sbExplain = new StringBuilder();
            string where = "SkillNo = '" + dv.Cells["iLinkSkill1"].Value + "'";
            where += "or SkillNo = '" + dv.Cells["iLinkSkill2"].Value + "'";
            where += "or SkillNo = '" + dv.Cells["iLinkSkill3"].Value + "'";

            DataRow[] row = DataHelper.XkfyData.Tables["BattleAbility"].Select(where);
            int index = 1;
            foreach (DataRow dr in row)
            {
                sbExplain.AppendFormat("第{0}招:{1}  ", index, dr["skillname"]);
                sbExplain.AppendFormat("最小伤害{0}  ", dr["mindamage"]);
                sbExplain.AppendFormat("最大伤害：{0}  ", dr["maxdamage"]);
                sbExplain.AppendFormat("内力消耗：{0}  ", dr["useap"]);
                sbExplain.AppendFormat("CD时间：{0}  ", dr["cd"]);
                string ttName = "";
                string targetarea = dr["targetarea"].ToString();
                switch (targetarea)
                {
                    case "0":
                        ttName = "单体";
                        break;
                    case "2":
                        ttName = "自身";
                        break;
                    case "1":
                        ttName = "直线";
                        break;
                    case "3":
                        ttName = "扇形";
                        break;
                }

                string[] condition = dr["condition"].ToString().Split(',');
                string buffName = string.Empty;
                for (int i = 0; i < condition.Length; i++)
                {
                    if (condition[i] == "" || condition[i] == "0")
                        continue;
                    buffName += DataHelper.GetValue("BattleCondition", "CondName", "ConditionID", condition[i]) + ",";
                }

                sbExplain.AppendFormat("攻击方式：{0} 攻击距离:{1} 溅射：{2}  ", ttName, dr["range"], dr["aoe"]);
                sbExplain.AppendFormat("附加效果：{0}", buffName.TrimEnd(new char[] {','}));
                sbExplain.Append("\r\n\r\n");
                index++;
            }
            return sbExplain.ToString();
        }

        #endregion

        #region 解释大地图事件

        public string ExplainQuestDataCon(string[] sOConditions, DataTable dt)
        {
            Dictionary<string, string> dicCon = null;
            if (!DataHelper.ExplainConfig.ContainsKey("QuestDataCon"))
            {
                string path = Application.StartupPath + "/工具配置文件/";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<SelectItem> dicList =
                    XmlHelper.XmlDeserializeFromFile<List<SelectItem>>(path + "QuestDataManager.xml", Encoding.UTF8);
                foreach (SelectItem item in dicList)
                {
                    dic.Add(item.Value, item.Text);
                }
                DataHelper.ExplainConfig.Add("QuestDataCon", dic);
                dicCon = dic;
            }
            else
            {
                dicCon = DataHelper.ExplainConfig["QuestDataCon"];
            }

            StringBuilder sbExplain = new StringBuilder();
            foreach (string rw in sOConditions)
            {
                string[] reward = rw.Replace(")", "").Replace("(", "").Split(',');
                if (reward.Length < 3)
                {
                    sbExplain.Append("本行数据有误,请检查!");
                    continue;
                }

                string bigClass = reward[0];
                string smallClass = reward[1];
                string value = reward[2];
                switch (bigClass)
                {
                    case "1":
                    case "2":
                    case "4":
                    case "5":
                    case "12":
                        sbExplain.AppendFormat("{0}{1}", DataHelper.ExplainConfig[dicCon[bigClass]][smallClass], value);
                        break;
                    case "6":
                        sbExplain.AppendFormat("【{0}】好感达到{1}点  ",
                            DataHelper.GetValue("NpcData", "sNpcName", "iID", smallClass), value);
                        break;
                    case "7":
                        sbExplain.AppendFormat("拥有物品【{0}】*{1}",
                            DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", smallClass), value);
                        break;
                    case "9":
                        sbExplain.AppendFormat("完成前置剧情 {0}", smallClass);
                        break;
                    case "10":
                        sbExplain.AppendFormat("未完成前置剧情 {0}", smallClass);
                        break;
                    case "11":
                        sbExplain.AppendFormat("拥有金钱{0}", value);
                        break;
                }
                sbExplain.Append("\r\n");
            }
            return sbExplain.ToString();
        }

        public string ExplainQuestDataFinish(string[] sFinish)
        {
            StringBuilder sbExplain = new StringBuilder();
            foreach (string rw in sFinish)
            {
                string[] reward = rw.Replace(")", "").Replace("(", "").Split(',');
                if (reward.Length < 3)
                {
                    sbExplain.Append("本行数据有误,请检查!");
                    continue;
                }

                if (reward[0] == "1")
                    continue;

                string bigClass = reward[0];
                string smallClass = reward[1];
                string value = reward[2];
                switch (bigClass)
                {
                    case "2":
                        sbExplain.AppendFormat("失去物品【{0}】",
                            DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", smallClass));
                        break;
                    case "3":
                        sbExplain.AppendFormat("金钱减少【{0}】", value);
                        break;
                    case "4":
                        sbExplain.AppendFormat("人物作出动作【{0}】", value);
                        break;
                    case "6":
                        sbExplain.AppendFormat("触发动画【{0}】", value);
                        break;
                    case "9":
                        sbExplain.AppendFormat("前置剧情【{0}】", value);
                        break;
                }
                sbExplain.Append("\r\n");
            }
            return sbExplain.ToString();
        }

        #endregion

        #region 解释战斗补充文件

        public string ExplainBattleSchedule(DataGridViewRow dr)
        {
            StringBuilder sb = new StringBuilder();
            int triggerType = Convert.ToInt32(dr.Cells["triggerType"].Value);
            string triggerData = dr.Cells["TriggerData"].Value.ToString();

            string npcName = string.Empty;
            if (triggerType != 7 && triggerType != 8 && triggerType != 9 && triggerType != 0 && triggerType != 6)
            {
                npcName = DataHelper.GetValue("BattleCharacterData", "Name", "CharID", triggerData);
            }

            string zhenYin = string.Empty;
            sb.Append("【触发时间】");
            switch (triggerType)
            {
                case 0:
                    sb.AppendFormat("战斗开始");
                    break;
                case 1:
                    sb.AppendFormat("{0}攻击时", npcName);
                    break;
                case 2:
                    sb.AppendFormat("{0}攻击后", npcName);
                    break;
                case 3:
                    sb.AppendFormat("{0}受伤前", npcName);
                    break;
                case 4:
                    sb.AppendFormat("{0}受伤后", npcName);
                    break;
                case 5:
                    sb.AppendFormat("{0}死亡后", npcName);
                    break;
                case 6:
                    sb.AppendFormat("第{0}回合", triggerData);
                    break;
                case 7:
                    sb.AppendFormat("战斗胜利");
                    break;
                case 8:
                    sb.AppendFormat("战斗失败");
                    break;
                case 9:
                    zhenYin = string.Empty;
                    if (triggerData == "0")
                        zhenYin = "我方阵营";
                    if (triggerData == "2")
                        zhenYin = "中立阵营";
                    if (triggerData == "1")
                        zhenYin = "敌对阵营";
                    sb.AppendFormat("{0}全阵亡", zhenYin);
                    break;
            }
            sb.Append("\r\n");

            int requireType = Convert.ToInt32(dr.Cells["RequireType"].Value);
            int RequireEqual = Convert.ToInt32(dr.Cells["RequireEqual"].Value);
            string value1 = dr.Cells["iRequireValue1"].Value.ToString();
            string value2 = dr.Cells["iRequireValue2"].Value.ToString();
            string panDuan = string.Empty;
            switch (RequireEqual)
            {
                case 0:
                    panDuan = "等于";
                    break;
                case 1:
                    panDuan = "大于";
                    break;
                case 2:
                    panDuan = "小于";
                    break;
                case 3:
                    panDuan = "大于等于";
                    break;
                case 4:
                    panDuan = "小于等于";
                    break;
                case 5:
                    panDuan = "不等于";
                    break;
            }

            sb.Append("【判定条件】");
            switch (requireType)
            {
                case 0:
                    sb.AppendFormat("无条件");
                    break;
                case 1:
                    sb.AppendFormat("血量{0}{1}", panDuan, value1 == "0" ? value2 + "%" : value1);
                    break;
                case 2:
                    sb.AppendFormat("Npc好感{0}", npcName);
                    break;
                case 3:
                    sb.AppendFormat("仇恨值{0}", npcName);
                    break;
                case 4:
                    sb.AppendFormat("{0}阵营,人物数量{1}{2}", value1, panDuan, value2);
                    break;
                case 5:
                    sb.AppendFormat("攻击目标{0}", DataHelper.GetValue("BattleCharacterData", "Name", "CharID", value1));
                    break;
                case 6:
                    sb.AppendFormat("被{0}攻击", DataHelper.GetValue("BattleCharacterData", "Name", "CharID", value1));
                    break;
                case 7:
                    sb.AppendFormat("进行中任务");
                    break;
                case 8:
                    sb.AppendFormat("完成任务");
                    break;
                case 9:
                    sb.AppendFormat("养成");
                    break;
            }

            sb.Append("\r\n");
            int triggerEvent = Convert.ToInt32(dr.Cells["TriggerEvent"].Value);
            string teArg1 = dr.Cells["iTriggerEventArg1"].Value.ToString();
            string teArg2 = dr.Cells["iTriggerEventArg2"].Value.ToString();
            string teArg3 = dr.Cells["iTriggerEventArg3"].Value.ToString();

            if (teArg2 == "0")
                zhenYin = "我方阵营";
            if (teArg2 == "2")
                zhenYin = "中立阵营";
            if (teArg2 == "1")
                zhenYin = "敌对阵营";

            switch (triggerEvent)
            {
                case 0:
                    sb.Append("【显示对话】" + DataHelper.GetMapTalk(teArg1, teArg2));
                    break;
                case 1:
                    sb.AppendFormat("{1}在位置{0} 增援", teArg3, zhenYin);
                    npcName = DataHelper.GetValue("BattleCharacterData", "Name", "CharID", teArg1);
                    sb.Append(npcName);
                    break;
                case 2:
                    sb.AppendFormat("转换【{0}】到{1}", DataHelper.GetValue("BattleCharacterData", "Name", "CharID", teArg1),
                        zhenYin);
                    break;
                case 3:
                    string npc1 = DataHelper.GetValue("BattleCharacterData", "Name", "CharID", teArg1);
                    string jn = DataHelper.GetValue("BattleAbility", "SkillName", "SkillNo", teArg2);
                    string npc2 = DataHelper.GetValue("BattleCharacterData", "Name", "CharID", teArg3);
                    sb.AppendFormat("{0} 向 {1}发动【{2}】", npc1, npc2, jn);
                    break;
                case 4:
                    npc1 = DataHelper.GetValue("BattleCharacterData", "Name", "CharID", teArg1);
                    sb.AppendFormat("{0}{1}获得状态【{2}】", zhenYin, npc1,
                        DataHelper.GetValue("BattleCondition", "CondName", "ConditionID", teArg2));
                    break;
                case 5:
                    sb.AppendFormat("获得进行中的任务：{0}", teArg1);
                    break;
                case 6:
                    sb.AppendFormat("获得已完成的任务：{0}", teArg1);
                    break;
                case 7:
                    sb.AppendFormat("{0}离开战场", DataHelper.GetValue("BattleCharacterData", "Name", "CharID", teArg1));
                    break;
                case 8:
                    sb.Append("播放背景音乐：" + teArg1);
                    break;
                case 9:
                    sb.Append("战斗结束,获胜阵营：" + teArg1);
                    break;
                case 10:
                    sb.AppendFormat("转换AI,具体什么意思,有待实验,如果有知情者,可联系作者！");
                    break;
            }


            return sb.ToString();
        }

        #endregion

        #region 绑定数据到下拉框

        /// <summary>
        /// 绑定数据到下拉框
        /// </summary>
        /// <param name="cb">下拉框对象</param>
        /// <param name="dic">数据源</param>
        public void BinderComboBox(ComboBox cb, Dictionary<string, string> dic)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dic;
            cb.DataSource = bs;
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }

        #endregion

        #region 清除窗体文本框的值

        public void ClearData(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox) c).Text = "";
                }
            }
        }

        #endregion

        #region 将容器里不包括下拉框的值赋给Dr

        /// <summary>
        /// 将容器里不包括下拉框的值赋给Dr
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dr"></param>
        public void CopyRowToData(Control control, DataGridViewRow dr)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag == null)
                {
                    continue;
                }

                string rowName = c.Tag.ToString();
                if (!dr.DataGridView.Columns.Contains(rowName))
                {
                    continue;
                }

                if (c is TextBox)
                {
                    dr.Cells[rowName].Value = ((TextBox) c).Text;
                }
            }
        }

        /// <summary>
        /// 将容器里不包括下拉框的值赋给Dr
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dr"></param>
        public void CopyRowToData(Control control, DataRow dr)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag == null)
                {
                    continue;
                }

                string rowName = c.Tag.ToString();
                if (!dr.Table.Columns.Contains(rowName))
                {
                    continue;
                }

                if (c is TextBox)
                {
                    dr[rowName] = ((TextBox) c).Text;
                }
            }
        }

        #endregion
        public static bool CheckFormIsOpen(string forms)
        {
            return Application.OpenForms.Cast<Form>().Any(frm => frm.Name == forms);
        }

        public void GetStringTable(TextBox id, TextBox name)
        {
            string[] row = new string[3] {"iID", "sString", "xUse"};
            OpenRadioForm("String_table", row, id, name);
        }

        /// <summary>
        /// 奖励专用弹出窗口
        /// </summary> 
        /// <param name="textId">显示ID的TextBox</param>
        /// <param name="textValue">显示编译值的TextBox</param>
        /// <param name="selType">radio单选</param>
        public static void OpenRewardData(TextBox textId, TextBox textValue, Const.OpenType selType)
        {
            OpenChooseForm(Const.RewardData, Const.RewardDataRow, textId, textValue,selType);
        } 

        /// <summary>
        /// Buff专用弹出窗口
        /// </summary> 
        /// <param name="textId">显示ID的TextBox</param>
        /// <param name="textValue">显示编译值的TextBox</param>
        /// <param name="selType">radio单选</param>
        public static void OpenBattleCondition(TextBox textId, TextBox textValue, Const.OpenType selType)
        {
            if (selType == Const.OpenType.Radio)
            {
                OpenRadioForm(Const.BattleCondition, Const.BattleConditionRow, textId, textValue);
            }
            else
            {
                OpenMultiForm(Const.BattleCondition, Const.BattleConditionRow, textId, textValue);
            }
        }

        /// <summary>
        /// BattleNeigong内功窗口
        /// </summary> 
        /// <param name="textId">显示ID的TextBox</param>
        /// <param name="textValue">显示编译值的TextBox</param>
        /// <param name="selType">radio单选</param>
        public static void OpenBattleNeigong(TextBox textId, TextBox textValue, Const.OpenType selType)
        {
            if (selType == Const.OpenType.Radio)
            {
                OpenRadioForm(Const.BattleNeigong, Const.BattleNeigongRow, textId, textValue);
            }
            else
            {
                OpenMultiForm(Const.BattleNeigong, Const.BattleNeigongRow, textId, textValue);
            }
        }

        /// <summary>
        /// ItemData物品窗口
        /// </summary> 
        /// <param name="textId">显示ID的TextBox</param>
        /// <param name="textValue">显示编译值的TextBox</param>
        /// <param name="selType">radio单选</param>
        public static void OpenItemData(TextBox textId, TextBox textValue, Const.OpenType selType)
        {
            if (selType == Const.OpenType.Radio)
            {
                OpenRadioForm(Const.ItemData, Const.ItemDataRow, textId, textValue);
            }
            else
            {
                OpenMultiForm(Const.ItemData, Const.ItemDataRow, textId, textValue);
            }
        }

        /// <summary>
        /// NpcData物品窗口
        /// </summary> 
        /// <param name="textId">显示ID的TextBox</param>
        /// <param name="textValue">显示编译值的TextBox</param>
        /// <param name="selType">radio单选</param>
        public static void OpenNpcData(TextBox textId, TextBox textValue, Const.OpenType selType)
        {
            if (selType == Const.OpenType.Radio)
            {
                OpenRadioForm(Const.NpcData, Const.NpcDataRow, textId, textValue);
            }
            else
            {
                OpenMultiForm(Const.NpcData, Const.NpcDataRow, textId, textValue);
            }
        }

        /// <summary>
        /// NpcData物品窗口
        /// </summary> 
        /// <param name="textId">显示ID的TextBox</param>
        /// <param name="textValue">显示编译值的TextBox</param>
        /// <param name="selType">radio单选</param>
        public static void OpenBattleAbility(TextBox textId, TextBox textValue, Const.OpenType selType)
        {
            if (selType == Const.OpenType.Radio)
            {
                OpenRadioForm(Const.BattleAbility, Const.BattleAbilityRow, textId, textValue);
            }
            else
            {
                OpenMultiForm(Const.BattleAbility, Const.BattleAbilityRow, textId, textValue);
            }
        }
        

        #region 选择窗口
        /// <summary>
        /// 单选窗口
        /// </summary>
        /// <param name="tbName">表名，文件名称</param>
        /// <param name="txtId">显示ID的TextBox</param>
        /// <param name="txtName">显示编译值的TextBox</param>
        /// <param name="row">需要显示的列</param>
        public static void OpenMultiForm(string tbName, string[] row, TextBox txtId, TextBox txtName)
        {
            OpenChooseForm(tbName, row, txtId, txtName, Const.OpenType.Mulit);
        }

        /// <summary>
        /// 单选窗口
        /// </summary>
        /// <param name="tbName">表名，文件名称</param>
        /// <param name="txtId">显示ID的TextBox</param>
        /// <param name="txtName">显示编译值的TextBox</param>
        /// <param name="row">需要显示的列</param>
        public static void OpenRadioForm(string tbName, string[] row, TextBox txtId, TextBox txtName)
        {
            OpenChooseForm(tbName, row, txtId, txtName, Const.OpenType.Radio); 
        }

        /// <summary>
        /// 选择窗口
        /// </summary>
        /// <param name="tbName">表名，文件名称</param>
        /// <param name="txtId">显示ID的TextBox</param>
        /// <param name="txtName">显示编译值的TextBox</param>
        /// <param name="row">需要显示的列</param>
        /// <param name="selType">1 radio 单选,2 Multi 多选</param>
        public static void OpenChooseForm(string tbName, string[] row, TextBox txtId, TextBox txtName, Const.OpenType selType)
        {
            ChooseData cd = new ChooseData()
            {
                TextId = txtId,
                TextName = txtName,
                Row = row,
                TableName = tbName,
                SelType = selType
            };

            RadioList rl = new RadioList(cd);
            rl.ShowDialog();
        }
        #endregion
    }
}
