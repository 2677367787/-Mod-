/***************************************************** 
** 命名空间：xkfy_mod.Helper 
** 文件名称：Class1 
** 内容简述：Helper类,是对项目的一种扩展,只用于特定项目 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2017/12/16 9:52:15 
** 修改记录： 
*****************************************************/
using System; 
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text; 
using xkfy_mod.Entity;
using xkfy_mod.Utils;


namespace xkfy_mod.Helper
{
    public class FileHelper
    {
        #region 列配置相关信息

        /// <summary>
        /// 获取表头相关信息,中文,详细解释,是否查询列等
        /// </summary>
        /// <param name="tbName">表名称</param>
        /// <returns></returns>
        public static IList<TableExplain> GetColumnData(string tbName)
        {
            //已经读取过直接返回
            if (DataHelper.ToolColumnConfig.ContainsKey(tbName))
            {
                return DataHelper.ToolColumnConfig[tbName];
            }
            string path = Path.Combine(PathHelper.ExplainFilePath, tbName + ".xml");
            if (!File.Exists(path))
            {
                return new List<TableExplain>();
            }
            return FileUtils.ReadConfig<TableExplain>(path);
        }

        /// <summary>
        /// 保存表头相关信息,中文,详细解释,是否查询列等
        /// </summary>
        /// <param name="list">数据类型</param>
        /// <param name="tbName">表名称</param>
        /// <returns></returns>
        public static void SaveColumnData(IList<TableExplain> list, string tbName)
        {
            string path = Path.Combine(PathHelper.ExplainFilePath, tbName + ".xml");
            FileUtils.SaveConfig(list, path);
        }

        /// <summary>
        /// 加载列详细配置信息
        /// </summary>
        /// <param name="tbName">表名称</param>
        public static void LoadColumnData(string tbName)
        {
            if (!DataHelper.ToolColumnConfig.ContainsKey(tbName))
            {
                DataHelper.ToolColumnConfig.Add(tbName, GetColumnData(tbName));
            }
        }

        #endregion

        #region 表配置信息

        /// <summary>
        /// 表配置信息专项读取方法
        /// </summary> 
        /// <returns>表配置信息List数组</returns>
        public static IList<MyConfig> ReadTableConfig()
        {
            return FileUtils.ReadConfig<MyConfig>(PathHelper.TableConfigPath);
        }

        /// <summary>
        /// 表配置信息专项保存方法
        /// </summary>
        /// <param name="list"></param>
        public static void SaveTableConfig(IList<MyConfig> list)
        {
            FileUtils.SaveConfig(list, PathHelper.TableConfigPath);
        }

        #endregion

        #region 工具配置文件

        /// <summary>
        /// 保存工具配置文件
        /// </summary>
        /// <param name="list"></param>
        public static void SaveAppConfig(IList<AppConfig> list)
        {
            FileUtils.SaveConfig(list, PathHelper.AppConfigPath);
        }

        /// <summary>
        /// 读取工具配置文件
        /// </summary>
        /// <returns></returns>
        public static IList<AppConfig> ReadAppConfig()
        {
            return FileUtils.ReadConfig<AppConfig>(PathHelper.AppConfigPath);
        }

        #endregion

        #region 读取Mod文件数据

        /// <summary>
        /// 加载表数据信息
        /// </summary>
        /// <param name="tbName">表名称</param>
        public static void LoadTable(string tbName)
        {
            //已读取过,不读取
            if (DataHelper.XkfyData.Tables.Contains(tbName))
            {
                return;
            }

            //没有配置文件的不读取
            if (!DataHelper.FormConfig.ContainsKey(tbName))
            {
                return;
            }

            //加载配置文件
            LoadColumnData(tbName);

            MyConfig myConfig = DataHelper.FormConfig[tbName];
            string dtType = myConfig.DtType;
            string isCache = myConfig.IsCache;

            string path = DataHelper.ToolFilesPath + "\\" + tbName + ".xml";
            //如果开启了缓存文件，并且XML文件存在读取XML文件
            if (isCache == "1" && File.Exists(path))
            {
//                DataHelper.XkfyData.Tables.Add(tbName);
//                DataHelper.XkfyData.Tables[tbName].ReadXml(path);
//                //如果类型=1 代表有明细表
//                if (dtType == "1")
//                {
//                    DataHelper.XkfyData.Tables.Add(myConfig.DetailDtName);
//                    string path1 = DataHelper.ToolFilesPath + "\\" + myConfig.DetailDtName + ".xml";
//                    DataHelper.XkfyData.Tables[myConfig.DetailDtName].ReadXml(path1);
//                }
            }
            else
            {
                BuildTableStruct(tbName, DataHelper.ToolColumnConfig[tbName], dtType);
                if (dtType == "1")
                {
                    BuildDetailTableStruct(myConfig.DetailDtName);
                    ReadModFile(myConfig.MainDtName, myConfig.DetailDtName, DataHelper.DictModFiles[myConfig.TxtName],
                        myConfig.BasicCritical, myConfig.EffectCritical);
                }
                else
                {
                    ReadModFile(myConfig.MainDtName, DataHelper.DictModFiles[myConfig.TxtName]);
                }
            }
        }

        #region 构建表结构

        /// <summary>
        /// 根据列配置信息,构建表结构
        /// </summary>
        /// <param name="tbName"></param>
        /// <param name="list"></param>
        /// <param name="dtType"></param>
        private static void BuildTableStruct(string tbName, IList<TableExplain> list, string dtType)
        {
            DataTable dt = new DataTable {TableName = tbName};
            foreach (TableExplain toolsColumn in list)
            {
                string columnName = dtType == "4" ? toolsColumn.ToolColumn : toolsColumn.Column;
                dt.Columns.Add(StringUtils.FormatToolColumn(columnName));
            }
            dt.Columns.Add("rowState");
            dt.Columns.Add("indexSn", typeof (int));
            DataHelper.XkfyData.Tables.Add(dt);
        }

        /// <summary>
        /// 构建明细表结构
        /// </summary>
        /// <param name="tbName"></param>
        private static void BuildDetailTableStruct(string tbName)
        {
            DataTable dt = new DataTable {TableName = tbName};
            IList<TableExplain> list = GetColumnData(tbName);
            foreach (TableExplain toolsColumn in list)
            {
                dt.Columns.Add(StringUtils.FormatToolColumn(toolsColumn.Column));
            }
            DataHelper.XkfyData.Tables.Add(dt);
        }

        #endregion

        #region 读取Mod文件内容

        /// <summary>
        /// 读取普通Mod文件内容
        /// </summary>
        /// <param name="tbName"></param>
        /// <param name="path"></param>
        private static void ReadModFile(string tbName, string path)
        {
            DataTable dt = DataHelper.XkfyData.Tables[tbName];
            bool fristRow = true;
            int rowIndex = 1;
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string readStr = sr.ReadLine(); //读取一行数据
                    if (readStr != null)
                    {
                        string[] strs = readStr.Split('\t'); //将读取的字符串按"制表符/t“和””“分割成数组
                        if (fristRow)
                        {
                            fristRow = false;
                            continue;
                        }
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < strs.Length; i++)
                        {
                            if (i >= dt.Columns.Count)
                            {
                                DataHelper.ReadError.AppendFormat("文件{0}.txt 第{1} 行数据格式有误,数据长度超越了表头,只按表头长度来处理数据\r\n",
                                    dt.TableName, rowIndex);
                                break;
                            }
                            dr[i] = strs[i];
                        }
                        dr["indexSn"] = rowIndex;
                        dt.Rows.Add(dr);
                    }
                }
            }
        }

        /// <summary>
        /// 读取可分为明细表的Mod文件内容
        /// </summary>
        /// <param name="tbName"></param>
        /// <param name="detailTbName"></param>
        /// <param name="path"></param>
        /// <param name="basicCritical"></param>
        /// <param name="effectCritical"></param>
        private static void ReadModFile(string tbName, string detailTbName, string path, int basicCritical,
            int effectCritical)
        {
            DataTable dtMain = DataHelper.XkfyData.Tables[tbName];
            DataTable dtDetail = DataHelper.XkfyData.Tables[detailTbName];
            int factor = effectCritical - basicCritical;
            int index = 1;
            bool fristRow = true;
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string readStr = sr.ReadLine(); //读取一行数据
                    //string[] strs = readStr.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                    if (readStr != null)
                    {
                        string[] strs = readStr.Split('\t');
                        if (fristRow)
                        {
                            fristRow = false;
                            continue;
                        }
                        //小于这个数目，数据肯定不正确了，也不需要继续执行了
                        if (strs.Length < basicCritical)
                        {
                            DataHelper.ReadError.AppendFormat(
                                $"文件{dtMain.TableName}.txt 第{index} 行数据格式有误,可能是多余的空行没有删除，也可能是数据格式不准确,程序将忽略这行数据\r\n");
                            continue;
                        }
                        string id = string.Empty;
                        //7列之前是基本信息
                        DataRow drMain = dtMain.NewRow();
                        for (int i = 0; i < basicCritical; i++)
                        {
                            drMain[i] = strs[i];
                            if (i == 0)
                            {
                                id = strs[i];
                            }
                        }
                        drMain["indexSn"] = index;
                        dtMain.Rows.Add(drMain);

                        DataRow drDetail = dtDetail.NewRow();
                        for (int i = basicCritical; i < strs.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strs[i]))
                                break;
                            int postion = (i - basicCritical)%factor;
                            if (postion == 0)
                            {
                                drDetail = dtDetail.NewRow();
                                drDetail[0] = id;
                                dtDetail.Rows.Add(drDetail);
                            }
                            drDetail[postion + 1] = strs[i];
//                            switch (postion)
//                            {
//                                case 0:
//                                    drDetail = dtDetail.NewRow();
//                                    drDetail["EffectType"] = strs[i];
//                                    break;
//                                case 1:
//                                    drDetail["Accumulate"] = strs[i];
//                                    break;
//                                case 2:
//                                    drDetail["Percent"] = strs[i];
//                                    break;
//                                case 3:
//                                    drDetail["Value1"] = strs[i];
//                                    break;
//                                case 4:
//                                    drDetail["Value2"] = strs[i];
//                                    break;
//                                case 5:
//                                    drDetail["ValueLimit"] = strs[i];
//                                    break;
//                            } 
                        }
                    }
                    index++;
                }
            }
        }

        #endregion

        #region 把NpcConduct和Map文件 读取到DataTable里去

        /// <summary>
        /// 把Map文件 读取到DataTable里去
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="tbName">表名</param>
        /// <param name="configKey">存储列头详细信息的Key</param>
        public static void ReadMapAndNpcText(string path, string tbName, string configKey)
        {
            //已读取过,不读取
            if (DataHelper.XkfyData.Tables.Contains(tbName))
            {
                return;
            }
            //加载表头配置文件
            LoadColumnData(configKey);
            //构建表结构
            BuildTableStruct(tbName, DataHelper.ToolColumnConfig[configKey], "map");
            ReadModFile(tbName, path);
        }

        #endregion

        #endregion

        #region 保存Mod文件数据

        /// <summary>
        /// 文件分类
        /// </summary>
        /// <param name="tbName">表名/文件名</param>
        /// <param name="filePath"></param>
        public static void Distinguish(string tbName, string filePath = null)
        {
            string typeName = tbName.Substring(0, 3).ToUpper();
            if (typeName != "MAP" && typeName != "NPC" && !DataHelper.FormConfig.ContainsKey(tbName))
                return;
            if (filePath == null)
            {
                filePath = DataHelper.DictModFiles[tbName + ".txt"];
            }

            if (DataHelper.FormConfig.ContainsKey(tbName))
            {

                MyConfig myConfig = DataHelper.FormConfig[tbName];
                switch (myConfig.DtType)
                {
                    case "1":
                        BuildData(myConfig.MainDtName, myConfig.DetailDtName, myConfig.TxtName, filePath);
                        break;
                    case "2":
                    case "3":
                        BuildData(myConfig.MainDtName, myConfig.MainDtName, filePath);
                        break;
                }
//                if (myConfig.IsCache == "1")
//                {
//                    FileUtils.BuildDataSetXml(myConfig.MainDtName);
//                    if (myConfig.DtType == "1")
//                    {
//                        FileUtils.BuildDataSetXml(myConfig.DetailDtName);
//                    }
//                }

            }
            else
            {
                string colConfigKey = null;

                //如果文件名称的前3个字符等于MAP
                if (typeName.ToUpper() == "MAP")
                {
                    colConfigKey = DataHelper.MapConfigFileName;
                }
                else if (typeName.ToUpper() == "NPC")
                {
                    colConfigKey = DataHelper.NpcConfigFileName;
                }
                BuildData(tbName, colConfigKey, filePath);
            }
        }

        #region 生成可以拆分为明细表的MOD文件

        /// <summary>
        /// 生成可以拆分为明细表的MOD文件
        /// </summary>
        /// <param name="mainDtName"></param>
        /// <param name="detailDtName"></param>
        /// <param name="txtName"></param>
        /// <param name="filePath">保存路径</param>
        public static void BuildData(string mainDtName, string detailDtName, string txtName, string filePath)
        {
            int maxRow = 0;
            StringBuilder sbTitle = new StringBuilder();
            StringBuilder sbConten = new StringBuilder();
            DataTable dtMain = DataHelper.XkfyData.Tables[mainDtName].Copy();
            DataTable dtDetail = DataHelper.XkfyData.Tables[detailDtName].Copy();
            if (dtMain.Columns.Contains("rowState"))
            {
                dtMain.Columns.Remove("rowState");
            }

            if (dtMain.Columns.Contains("indexSn"))
            {
                dtMain.Columns.Remove("indexSn");
            }
            //循环主表的所有列
            foreach (DataRow dr in dtMain.Rows)
            {
                //把主表的列用/t组合成一个字符串
                sbConten.Append(string.Join("\t", dr.ItemArray));
                //用列名查询明细列
                DataRow[] dRow = dtDetail.Select(
                    $"{dtDetail.Columns[0].ColumnName}='{dr[dtMain.Columns[0].ColumnName]}'");
                //循环明细列记录
                foreach (DataRow row in dRow)
                {
                    //把明细列用\t组合成一个字符串
                    string strRow = string.Join("\t", row.ItemArray);
                    strRow = strRow.Substring(strRow.IndexOf("\t", StringComparison.Ordinal));
                    sbConten.Append(strRow);
                }
                if (dRow.Length > maxRow)
                {
                    maxRow = dRow.Length;
                }
                sbConten.Append("\r\n");
            }

            sbTitle.Append("#");

            List<string> list = new List<string>();
            dtDetail.Columns.Remove(dtDetail.Columns[0].ColumnName);
            for (int i = 0; i < dtMain.Columns.Count; i++)
            {
                list.Add(dtMain.Columns[i].ColumnName);
            }

            for (int j = 0; j < maxRow; j++)
            {
                for (int i = 0; i < dtDetail.Columns.Count; i++)
                {
                    list.Add(dtDetail.Columns[i].ColumnName);
                }
            }
            sbTitle.Append(string.Join("\t", list.ToArray()) + "\r\n");
            sbTitle.Append(sbConten);
            FileUtils.WriteData(sbTitle.ToString(), filePath);
        }

        #endregion

        #region 生成普通Mod文件

        /// <summary>
        /// 生成普通Mod文件
        /// </summary>
        /// <param name="tbName">表名称</param>
        /// <param name="colConfigKey">列配置Key</param>
        /// <param name="filePath">保存路径</param> 
        private static void BuildData(string tbName, string colConfigKey, string filePath)
        {
            StringBuilder sbConten = new StringBuilder();
            DataTable dtMain = DataHelper.XkfyData.Tables[tbName].Copy();

            #region 特殊处理

            if (dtMain.Columns.Contains("rowState"))
            {
                dtMain.Columns.Remove("rowState");
            }

            if (dtMain.Columns.Contains("indexSn"))
            {
                dtMain.Columns.Remove("indexSn");
            }

            #endregion

            sbConten.Append(GetTextTitle(colConfigKey));
            sbConten.Append(GetTextConten(dtMain));

            dtMain.Dispose();
            FileUtils.WriteData(sbConten.ToString(), filePath);
        }

        #endregion

        #region 生成内容标题

        /// <summary>
        /// 生成内容
        /// </summary>
        /// <param name="dtMain"></param>
        /// <returns></returns>
        private static string GetTextConten(DataTable dtMain)
        {
            StringBuilder sbConten = new StringBuilder();
            foreach (DataRow dr in dtMain.Rows)
            {
                sbConten.Append(string.Join("\t", dr.ItemArray));
                sbConten.Append("\r\n");
            }
            return sbConten.ToString();
        }

        /// <summary>
        /// 生成标题
        /// </summary>
        /// <param name="tbName"></param>
        /// <returns></returns>
        private static string GetTextTitle(string tbName)
        {
            IList<TableExplain> list = GetColumnData(tbName);
            foreach (TableExplain tc in list.Where(tc => tc.Column.IndexOf("$", StringComparison.Ordinal) != -1))
            {
                tc.Column = tc.Column.Substring(0, tc.Column.IndexOf("$", StringComparison.Ordinal));
            }

            string[] columns = (from t in list select t.Column).ToArray();
            if (tbName == "RoutineData")
            {
                return string.Join("\t", columns).Replace("plus", " ") + "\r\n";
            }
            return string.Join("\t", columns) + "\r\n";
        }

        #endregion

        #endregion

        #region 搜索文件夹中的文件加入到字典递归所有文件

        /// <summary>
        /// 搜索文件夹中的文件加入到字典递归所有文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dic"></param>
        public static void GetAllFolderFile(string filePath, Dictionary<string, string> dic)
        {
            DirectoryInfo di = new DirectoryInfo(filePath);
            GetAllFolderFile(di, dic);
        }

        /// <summary>
        /// 搜索文件夹中的文件加入到字典递归所有文件
        /// </summary>
        /// <param name="di">文件类</param>
        /// <param name="dic"></param>
        public static void GetAllFolderFile(DirectoryInfo di, Dictionary<string, string> dic)
        {
            FileInfo[] allFile = di.GetFiles();
            foreach (FileInfo fi in allFile)
            {
                if (dic.ContainsKey(fi.Name))
                {
                    string mapFileName = fi.Name.Substring(0, 8).ToUpper();
                    string npcFileName = fi.Name.Substring(0, 10).ToUpper();
                    if ("MAP_ICON" == mapFileName)
                    {
                        if (fi.FullName.ToUpper().IndexOf("MAPICON", StringComparison.Ordinal) == -1)
                        {
                            DataHelper.ReadError.Append($"文件{fi.Name}应该放在MapIcon文件夹里面,否则会无效！");
                        }
                    }
                    else if ("NPCCONDUCT" == npcFileName)
                    {
                        if (fi.FullName.ToUpper().IndexOf("MAPICON", StringComparison.Ordinal) == -1)
                        {
                            DataHelper.ReadError.Append($"文件{fi.Name}应该放在MapIcon文件夹里面,否则会无效！");
                        }
                    }
                    continue;
                }
                dic.Add(fi.Name, fi.FullName);
            }

            DirectoryInfo[] allDir = di.GetDirectories();
            foreach (var d in allDir)
            {
                GetAllFolderFile(d, dic);
            }
        }

        #region 搜索文件夹中的文件,保存到Dict里 KEY是不含后缀的文件名

        /// <summary>
        /// 搜索文件夹中的文件加入到字典递归所有文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dic"></param>
        public static void GetAllFileNotPostfix(string filePath, Dictionary<string, string> dic)
        {
            DirectoryInfo di = new DirectoryInfo(filePath);
            GetAllFileNotPostfix(di, dic);
        }

        /// <summary>
        /// //搜索文件夹中的文件
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        /// <param name="dic"></param>
        public static void GetAllFileNotPostfix(DirectoryInfo dir, Dictionary<string, string> dic)
        {
            FileInfo[] allFile = dir.GetFiles();
            foreach (FileInfo fi in allFile)
            {
                if (dic.ContainsKey(Path.GetFileNameWithoutExtension(fi.FullName)))
                    continue;
                dic.Add(Path.GetFileNameWithoutExtension(fi.FullName), fi.FullName);
            }

            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo directoryInfo in allDir)
            {
                GetAllFileNotPostfix(directoryInfo, dic);
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// 加载所有下拉框数据源
        /// </summary>
        public static void LoadDropDownListData()
        {
            IList<DicConfig> fileList = FileUtils.ReadConfig<DicConfig>(PathHelper.DropDownListDataPath);
            foreach (DicConfig dc in fileList)
            {
                DataHelper.DropDownListDict.Add(dc.Key,
                    FileUtils.ReadConfig<DicConfig>(Path.Combine(PathHelper.DdlFolderPath, dc.Value))
                        .ToDictionary(dic => dic.Key, dic => dic.Value));
            }

            var basicList = FileUtils.ReadConfig<BasicList>(PathHelper.BasicInfoPath);
            foreach (BasicList dc in basicList)
            {
                DataHelper.DropDownListDict.Add(dc.UniqueKey,
                    dc.BasicInfo.ToDictionary(dic => dic.Key, dic => dic.Value));
            }
        }

        /// <summary>
        /// 加载所有下拉框数据源
        /// </summary>
        public static void LoadImagesData()
        {
            GetAllFileNotPostfix(PathHelper.ImagePath, DataHelper.DictImages);
        }
    }
}
