/***************************************************** 
** 命名空间：xkfy_mod.Helper 
** 文件名称：FileUtils.cs 
** 内容简述：Utils类,属于通用类,所有方法应能独立迁移到别的项目 
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
using System.Windows.Forms;
using xkfy_mod.Helper;
using xkfy_mod.XML;

namespace xkfy_mod.Utils
{
    public class FileUtils
    { 
        /// <summary>
        /// 对外通用读取配置文件方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IList<T> ReadConfig<T>(string path)
        {
            return ReadXmlConfig<T>(path);
        }

        /// <summary>
        /// 对外通用保存配置文件方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="path"></param>
        public static void SaveConfig<T>(IList<T> list, string path)
        {
            SaveXmlConfig<T>(list, path);
        }

        #region XML文件读取保存

        /// <summary>
        /// 通用文件保存类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        private static void SaveXmlConfig<T>(IList<T> list, string filePath)
        {
            try
            {
                XmlHelper.XmlSerializeToFile(list, filePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 通用文件读取类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        private static IList<T> ReadXmlConfig<T>(string path)
        {
            try
            {
                return XmlHelper.XmlDeserializeFromFile<List<T>>(path, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return new List<T>();
            }

        }

        #endregion 

        /// <summary>
        /// 把字符串写入txt文件中
        /// </summary>
        /// <param name="txtConten">内容</param>
        /// <param name="filePath">文件路径</param>
        public static void WriteData(string txtConten, string filePath)
        { 
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    //开始写入
                    sw.Write(txtConten);
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
        }


        #region 把DataSet里的数据生成到xml文件中去
        public static void BuildDataSetXml(string tbName)
        {
            string path = DataHelper.ToolFilesPath + "\\" + tbName + ".xml";
            //检查是否存在目的目录  
            if (!Directory.Exists(DataHelper.ToolFilesPath))
            {
                Directory.CreateDirectory(DataHelper.ToolFilesPath);
            }
            DataHelper.XkfyData.Tables[tbName].WriteXml(path, XmlWriteMode.WriteSchema);
        }

        public static void ReadDataSetXml(string tbName)
        {
            string path = DataHelper.ToolFilesPath + "\\" + tbName + ".xml";
            //检查是否存在目的目录  
            if (!Directory.Exists(DataHelper.ToolFilesPath))
            {
                Directory.CreateDirectory(DataHelper.ToolFilesPath);
            }
            DataHelper.XkfyData.Tables[tbName].WriteXml(path, XmlWriteMode.WriteSchema);
        }
        #endregion

        #region 搜索文件夹中的文件
        /// <summary>
        /// 搜索文件夹中的文件加入到字典,不递归文件夹
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dic"></param>
        public static void GetCurrFolderFile(string filePath, Dictionary<string, string> dic)
        {
            DirectoryInfo di = new DirectoryInfo(filePath);
            FileInfo[] allFile = di.GetFiles();
            foreach (FileInfo fi in allFile)
            {
                if (dic.ContainsKey(fi.Name))
                {
                    continue;
                }
                dic.Add(fi.Name, fi.FullName);
            }
        }

        /// <summary>
        /// 查找特定文件夹中的文件加入到字典
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dic"></param>
        /// /// <param name="folderName">文件夹名称</param>
        public static void GetFolderFileByName(string filePath, Dictionary<string, string> dic, string folderName)
        {
            DirectoryInfo di = new DirectoryInfo(filePath);
            DirectoryInfo[] allDir = di.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                if (folderName.Equals(d.Name.ToUpper()))
                {
                    FileInfo[] allFile = d.GetFiles();
                    foreach (FileInfo fi in allFile)
                    {
                        dic.Add(fi.Name, fi.FullName);
                    }
                    break;
                }
            }
        }
         
        /// <summary>
        /// 删除非空文件夹
        /// </summary>
        /// <param name="path">要删除的文件夹目录</param>
        public static void DeleteDirectory(string path)
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception ex)
            {
                if (!Directory.Exists(path)) return;
                DirectoryInfo di = new DirectoryInfo(path);

                if (di.GetDirectories().Length == 0 && di.GetFiles().Length == 0) return;

                foreach (DirectoryInfo d in di.GetDirectories())
                {
                    DeleteDirectory(d.FullName);
                }

                foreach (FileInfo fi in di.GetFiles())
                {
                    fi.Delete();
                }
                di.Delete();
            } 
        }

        private void DeleteFiles(string path)
        {
        }


        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="directorySource">源文件目录</param>
        /// <param name="directoryTarget">目标目录</param>
        public static void CopyFolderTo(string directorySource, string directoryTarget)
        { 
            //检查是否存在目的目录  
            if (!Directory.Exists(directorySource))
            {
                MessageBox.Show($"无法从工具指定的路径[{directorySource}]获取到Mod文件\r\n请检查，程序根目录是否存在文件夹[{PathHelper.ModFoderPath}]！");
                return;
            }
            //检查是否存在目的目录  
            if (!Directory.Exists(directoryTarget))
            {
                Directory.CreateDirectory(directoryTarget);
            }
            //先来复制文件  
            DirectoryInfo directoryInfo = new DirectoryInfo(directorySource);
            FileInfo[] files = directoryInfo.GetFiles();
            //复制所有文件  
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(directoryTarget, file.Name), true);
            }
            //最后复制目录  
            DirectoryInfo[] directoryInfoArray = directoryInfo.GetDirectories();
            foreach (DirectoryInfo dir in directoryInfoArray)
            {
                CopyFolderTo(Path.Combine(directorySource, dir.Name), Path.Combine(directoryTarget, dir.Name));
            }

        }
        #endregion

        /// <summary>  
        /// 读文件  
        /// </summary>  
        /// <param name="path">文件路径</param>  
        /// <returns></returns>  
        public static string ReadFile(string Path)
        {
            try
            {
                StreamReader sr = new StreamReader(Path,Encoding.GetEncoding("utf-8"));
                string content = sr.ReadToEnd().ToString();
                sr.Close();
                return content;
            }
            catch
            {
                 
            }
            return "";
        }
    }
}
