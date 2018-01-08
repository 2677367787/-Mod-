using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xkfy_mod.Helper
{
    public class PathHelper
    {
        //下拉框配置文件路径
        public const string DdlFolderPath = "工具配置文件/下拉框配置文件/";

        //解释信息文件夹
        public const string ExplicatePath = "工具配置文件/解释格式/";

        public const string TalkBoxPathLeft = "Images/1.png";

        public const string TalkBoxPathRight = "Images/2.png";

        public const string ImagePath = "Images";
         
        /// <summary>
        /// 游戏安装目录
        /// </summary>
        public static string GamePath { get; set; }

        public static string GetExplicatePath(string tbName)
        {
            return Path.Combine(ExplicatePath, tbName + ".xml");
        }

        /// <summary>
        /// 原始文件,路径
        /// </summary>
        public static string ModFoderPath => "原始文件";

        /// <summary>
        /// 修改后的文件,路径
        /// </summary>
        public static string ModifyFolderPath => "修改后的文件";

        /// <summary>
        /// 程序配置文件 工具配置文件/AppConfig.xml
        /// </summary>
        public static string AppConfigPath => "工具配置文件/AppConfig.xml";

        /// <summary>
        /// 工具配置文件/TableConfig.xml
        /// </summary>
        public static string TableConfigPath => "工具配置文件/TableConfig.xml";

        /// <summary>
        /// 工具配置文件/下拉框配置文件/DataConfig.xml
        /// </summary>
        public static string DropDownListDataPath => "工具配置文件/下拉框配置文件/DataConfig.xml";
        /// <summary>
        /// 工具配置文件/下拉框配置文件/DataConfig.xml
        /// </summary>
        public static string BasicInfoPath => "工具配置文件/下拉框配置文件/基础信息.xml";

        /// <summary>
        /// 工具配置文件/TableExplain/
        /// </summary>
        public static string ExplainFilePath => "工具配置文件/TableExplain/";

        /// <summary>
        /// MenuConfig.xml
        /// </summary>
        public static string MenuConfigPath => "工具配置文件/MenuConfig.xml";

        /// <summary>
        /// MenuConfig.xml
        /// </summary>
        public static string RoutineDataSelConfig => "工具配置文件/RoutineDataSelConfig.xml";

        /// <summary>
        /// MenuConfig.xml
        /// </summary>
        public static string RewardDataSelConfig => "工具配置文件/RewardDataSelConfig.xml";
        
        /// <summary>
        /// 工具配置文件/HuiHe.xml
        /// </summary>
        public static string HuiHePath => "工具配置文件/HuiHe.xml";

        /// <summary>
        /// 工具配置文件/TalkManager/NpcImg.xml
        /// </summary>
        public static string NpcImg => "工具配置文件/TalkManager/NpcImg.xml";

        /// <summary>
        /// 工具配置文件/DevelopQuestData_Config/NpcImg.xml
        /// </summary>
        public static string TalkManageriGtype => DdlFolderPath + "TalkManager.iGtype.xml";
        

        #region 下拉框配置文件路径
        /// <summary>
        /// BattleCondition，生效方式配置,和内功公用
        /// </summary>
        public static string AccumulatePath => DdlFolderPath + "BattleNeigong.Accumulate.作用方式.xml";

        /// <summary>
        /// BattleNeigong，招式效果
        /// </summary>
        public static string EffecttypePath => DdlFolderPath + "BattleNeigong.Effecttype.效果类型.xml";

        /// <summary>
        /// BattleCondition.Effecttype.效果类型.xml
        /// </summary>
        public static string BaCoEffecttypePath => DdlFolderPath + "BattleCondition.Effecttype.效果类型.xml";

        /// <summary>
        /// BattleCondition.Accumulate.作用方式.xml
        /// </summary>
        public static string BaCoAccumulatePath => DdlFolderPath + "BattleCondition.Accumulate.作用方式.xml";

        /// <summary>
        /// 武器类型 iWearAmsType.xml
        /// </summary>
        public static string PubWearAmspath => DdlFolderPath + "iWearAmsType.公用.武器类型.xml";

        /// <summary>
        /// ItemData.irecover.xml
        /// </summary>
        public static string ItemDataIrecover => DdlFolderPath + "ItemData.iRecover.xml";
        #endregion
    }
}
