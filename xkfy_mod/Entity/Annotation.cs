/***************************************************** 
** 命名空间：xkfy_mod.Entity
** 文件名称：Annotation
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2017/12/23 10:18:03
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xkfy_mod.Entity
{
    public class Annotation
    {
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public List<RowFormat> ExplainList { get; set; }
    }
}
