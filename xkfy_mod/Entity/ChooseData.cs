/***************************************************** 
** 命名空间：xkfy_mod.Entity
** 文件名称：ChooseData
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2017/12/23 17:41:43
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xkfy_mod.Entity
{
    public class ChooseData
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 类型 1，单选；2,多选
        /// </summary>
        public string SelType { get; set; }

        /// <summary>
        /// 界面显示的列名数组
        /// </summary>
        public string[] Row { get; set; }

        /// <summary>
        /// id的文本框
        /// </summary>
        public TextBox TextId { get; set; }

        /// <summary>
        /// 显示的文本框
        /// </summary>
        public TextBox TextName { get; set; }
    }
}
