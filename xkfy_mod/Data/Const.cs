/***************************************************** 
** 命名空间：xkfy_mod.Data
** 文件名称：Const
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2017/12/23 14:13:45
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xkfy_mod.Entity;

namespace xkfy_mod.Data
{
    public class Const
    {
        /// <summary>
        /// 常量 0
        /// </summary>
        public static string Zero => "0";

        /// <summary>
        /// 选择窗类型
        /// </summary>
        public enum OpenType
        {
            /// <summary>
            /// 单选
            /// </summary>
            Radio,
            /// <summary>
            /// 多选
            /// </summary>
            Mulit
        }

        /// <summary>
        /// 明细表后缀
        /// </summary>
        public const string DetailTbName = "_D";
        

        #region 文件名称常量
        public const string AbilityBookData = "AbilityBookData";
        public const string AchievementData = "AchievementData";
        public const string AlchemyProduceData = "AlchemyProduceData";
        public const string AlchemyScene = "AlchemyScene";
        public const string BattleAbility = "BattleAbility";
        public const string BattleAreaData = "BattleAreaData";
        public const string BattleCharacterData = "BattleCharacterData";
        public const string BattleCondition = "BattleCondition";
        public const string BattleNeigong = "BattleNeigong";
        public const string BattleSchedule = "BattleSchedule";
        public const string BattleTactic = "BattleTactic";
        public const string BlacksmithData = "BlacksmithData";
        public const string BowData = "BowData";
        public const string ChildEndData = "ChildEndData";
        public const string DevelopBtnData = "DevelopBtnData";
        public const string DevelopBtnRandomQuest = "DevelopBtnRandomQuest";
        public const string DevelopExp = "DevelopExp";
        public const string DevelopQuestData = "DevelopQuestData";
        public const string EndMoveData = "EndMoveData";
        public const string FishData = "FishData";
        public const string FishRodData = "FishRodData";
        public const string FlowerData = "FlowerData";
        public const string GoToMapData = "GoToMapData";
        public const string HerbsData = "HerbsData";
        public const string HuntData = "HuntData";
        public const string ItemData = "ItemData";
        public const string JourneyData = "JourneyData";
        public const string KofuRankData = "KofuRankData";
        public const string MainEndData = "MainEndData";
        public const string MapId = "MapID";
        public const string MapTalkManager = "MapTalkManager";
        public const string MiningData = "MiningData";
        public const string MoodTalk = "MoodTalk";
        public const string NeigongUpValue = "NeigongUpValue";
        public const string NpcData = "NpcData";
        public const string PlotData = "PlotData";
        public const string QuestDataManager = "QuestDataManager";
        public const string QuestionData = "QuestionData";
        public const string QuestionMenu = "QuestionMenu";
        public const string RewardData = "RewardData";
        public const string RoutineData = "RoutineData";
        public const string RoutineExp = "RoutineExp";
        public const string ScenesAudio = "ScenesAudio";
        public const string StoreData = "StoreData";
        public const string StrinTable = "String_table";
        public const string TalentData = "TalentData";
        public const string TalkManager = "TalkManager";
        public const string TitleData = "TitleData";
        public const string TreasureBox = "TreasureBox";
        public const string ValleyData = "ValleyData";
        #endregion

        #region 下拉框键值对常量
        public const string BaNeAccumulate = "BattleNeigong.Accumulate";
        public const string BaNeEffecttype = "BattleNeigong.Effecttype";
        public const string BaCoAccumulate = "BattleCondition.Accumulate";
        public const string BaCoEffecttype = "BattleCondition.Effecttype";
        #endregion

        /// <summary>
        /// RewardDataRow.txt
        /// </summary>
        public static readonly string[] RewardDataRow = { "iRID", "sRewardData"};
        /// <summary>
        /// BattleConditionRow.txt
        /// </summary>
        public static readonly string[] BattleConditionRow = { "ConditionID", "CondName", "CondDesc" };

        /// <summary>
        /// BattleNeigongRow.txt
        /// </summary>
        public static readonly string[] BattleNeigongRow = { "ID", "Name", "Desc" };

        /// <summary>
        /// BattleAbilityRow.txt
        /// </summary>
        public static readonly string[] BattleAbilityRow =　{ "SkillNo", "SkillName", "NeedToSelectTarget" };

        /// <summary>
        /// ItemData.txt
        /// </summary>
        public static readonly string[] ItemDataRow =  { "iItemID$0", "sItemName$1", "sTip$5" };

        /// <summary>
        /// NpcData.txt
        /// </summary>
        public static readonly string[] NpcDataRow = { "iID", "sNpcName", "sIntroduction" };

        #region 查询专用
        /// <summary>
        /// 内功名称查询参数
        /// </summary>
        public static GetValueRows NeiGongFe = new GetValueRows(BattleNeigong, "Name", "Id");

        public static GetValueRows BattleAbilityFe = new GetValueRows(BattleAbility, "SkillName", "SkillNo");
        
        #endregion
    }
}
