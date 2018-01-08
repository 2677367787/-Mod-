/***************************************************** 
** 命名空间：xkfy_mod.Entity
** 文件名称：FormData
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2017/12/22 22:49:21
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xkfy_mod.Entity
{
    public class FormData
    {
        public DataGridViewRow Dr { get; set; }
        public string TableName { get; set; }
        public string Type { get; set; }
        public string ConfigKeyName { get; set; }
        public IDictionary<string, TableExplain> DictTableExplain { get; set; }
    }
}
