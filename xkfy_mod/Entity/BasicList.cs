/***************************************************** 
** 命名空间：xkfy_mod.Entity
** 文件名称：BasicList
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/1/1 12:37:24
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xkfy_mod.Entity
{
    public class BasicList
    {
        /// <summary>
        /// 存储在集合DropDownListData的唯一Key值
        /// </summary>
        public string UniqueKey { get; set; }

        /// <summary>
        /// 存储在集合DropDownListData对应的集合值
        /// </summary>
        public List<DicConfig> BasicInfo { get; set; }
    }
}
