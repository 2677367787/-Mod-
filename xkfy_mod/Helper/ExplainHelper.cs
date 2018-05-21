using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Helper;
using xkfy_mod.Entity;
using xkfy_mod.Utils;

namespace xkfy_mod.Helper
{
    public class ExplainHelper
    { 
        public static string ExplainRewardData(string[] sRewardData)
        { 
            FileHelper.LoadTable("String_table");
            DataTable dt = DataHelper.DdlDataSet.Tables["TalkManager.iGtype"];
            DataTable strT = DataHelper.XkfyData.Tables["String_table"];

            StringBuilder sbExplain = new StringBuilder();

            foreach (string rw in sRewardData)
            {
                string[] reward = rw.Replace(")", "").Replace("(", "").Split(',');
                if (reward.Length < 4)
                {
                    //lblMsg.Text = "本行数据有误,请检查!";
                    continue;
                }

                string jl = reward[0];
                string jlType = reward[1];
                string value = reward[2];
                string str_msg = reward[3];

                DataRow[] driJl = dt.Select("Type='Reward' and Value='" + jl + "'");
                if (driJl.Length == 0)
                {
                    //lblMsg.Text = "未在配置文件中找到对应的配置,请联系作者";
                    continue;
                }

                if (driJl.Length > 1)
                {
                    //lblMsg.Text = string.Format("配置文件有误，请联系作者！");
                    continue;
                }

                string tempStr = string.Empty;
                switch (jl)
                {
                    case "1":
                    case "4":
                    case "5":
                    case "24":
                    case "30":
                    case "32":
                        DataRow[] drjlType = dt.Select("Type='" + driJl[0]["ChildType"].ToString() + "' and Value='" + jlType + "'");
                        if (drjlType.Length == 0)
                            continue;
                        DataRow dr_str = strT.Select("iID='" + str_msg + "'")[0];
                        tempStr = dr_str["sString"].ToString();
                        sbExplain.AppendFormat(tempStr + "", value);
                        break;
                    case "6":
                        sbExplain.AppendFormat("【{0}】对你好感增加了{1}点  ", DataHelper.GetValue("NpcData", "sNpcName", "iID", jlType), value);
                        break;
                    case "7":
                        sbExplain.AppendFormat("获得物品【{0}】", DataHelper.GetValue("ItemData", "sItemName$1", "iItemID$0", jlType));
                        break;
                    case "13":
                        sbExplain.AppendFormat("获得称号【{0}】", DataHelper.GetValue("TitleData", "sTitle", "iID", jlType));
                        break;
                    case "16":
                        sbExplain.AppendFormat("获得套路【{0}】", DataHelper.GetValue("RoutineData", "sRoutineName", "iRoutineID", jlType));
                        break;
                    case "17":
                        sbExplain.AppendFormat("获得内功【{0}】", DataHelper.GetValue("BattleNeigong", "Name", "ID", jlType));
                        break;
                    case "18":
                        sbExplain.AppendFormat("开启经历【{0}】", DataHelper.GetValue("JourneyData", "sTip", "iID", jlType));
                        break;
                    case "19":
                        sbExplain.AppendFormat("学会技能【{0}】", DataHelper.GetValue("AbilityBookData", "sAbilityID", "iID", jlType));
                        break;
                    case "21":
                        sbExplain.AppendFormat("触发场景对话,对话ID为【#{0}#】", value);
                        break;
                    case "14":
                    case "27":
                    case "28":
                        sbExplain.AppendFormat("【{0}】增加了【{1}】", driJl[0]["text"], value);
                        break;
                    case "40":
                        sbExplain.AppendFormat("【{0}】加入队伍", DataHelper.GetValue("BattleCharacterData", "Name", "CharID", value));
                        break;
                    default:
                        sbExplain.AppendFormat("弱鸡作者不理解{0}代表什么意思", rw);
                        break;
                }
                sbExplain.Append("\r\n");
            }
            return sbExplain.ToString();
        }
        　
        /// <summary>
        /// 返回buff名称 文件名 BattleCondition
        /// </summary>
        /// <param name="id">效果ID</param>
        /// <returns></returns>
        public static string GetConditionName(string id)
        {
            GetValueRows gvr = new GetValueRows()
            {
                TableName = Const.BattleCondition,
                ReturntRow = "CondName",
                SelectRow = "ConditionID",
                Id = id
            };
            return DataHelper.GetValue(gvr);
        }

        public GetValueRows GetExplainParam(string tbName,string returntRow,string selectRow,string id)
        {
            return new GetValueRows()
            {
                TableName = tbName,
                ReturntRow = returntRow,
                SelectRow = selectRow,
                Id = id
            };
        }

        /// <summary>
        /// 内功名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetNeiGongName()
        {
            return DataHelper.GetValue(Const.NeiGongFe);
        }

        /// <summary>
        /// 套路名称
        /// </summary>
        /// <returns></returns>
        public static string GetBattleAbilityName()
        {
            return DataHelper.GetValue(Const.BattleAbilityFe);
        } 

        public static string GetNpcName(string id)
        {
            return DataHelper.GetValue("NpcData", "sNpcName", "iID", id);
        }
        
        /// <summary>
        /// 物品名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetItemName(string id)
        {
            return DataHelper.GetValue("ItemData", "sItemName$1", "@iItemID$0", id);
        }

        /// <summary>
        /// 天赋文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetTalentName(string id)
        {
            return DataHelper.GetValue("TalentNewData", "sTalenName$1", "@iTalenID$0", id);
        }　

        /// <summary>
        /// 招式名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetRoutineName(string id)
        {
            return DataHelper.GetValue("RoutineNewData", "sRoutineName", "@iRoutineID", id);
        }

        /// <summary>
        /// 通用提示字符串
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetStringTab(string id)
        {
            return DataHelper.GetValue("String_table", "sString", "@iID", id);
        }

        public static string GetQuestDataManager(string id)
        {
            return DataHelper.GetValue("QuestDataManager", "sQuestName", "@sID", id);
        }

        /// <summary>
        /// NPC武学信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetCharacterData(string id)
        {
            return DataHelper.GetValue("CharacterData", "Helper", "@CharID", id);
        }

        /// <summary>
        /// 对话
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetMapTalkManager(string id)
        {
            FileHelper.LoadTable(Const.MapTalkManager);
            StringBuilder sb = new StringBuilder();
            DataRow[] drs = DataHelper.XkfyData.Tables[Const.MapTalkManager].Select($"sGroupID='{id}'");
            foreach (DataRow dr in drs)
            {
                sb.Append(dr["sManager"]);
                sb.Append("\r\n");
            } 
            return sb.ToString();
        } 
    }
}
