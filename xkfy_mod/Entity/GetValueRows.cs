/***************************************************** 
** 命名空间：xkfy_mod.Entity
** 文件名称：GetValueRows
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2017/12/23 20:31:31
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xkfy_mod.Entity
{
    public class GetValueRows
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 返回的列名称
        /// </summary>
        public string ReturntRow { get; set; }

        /// <summary>
        /// 查询的列名称
        /// </summary>
        public string SelectRow { get; set; }

        /// <summary>
        /// 查询依据 Id
        /// </summary>
        public string Id { get; set; }

        public GetValueRows(string tbName,string returntRow,string selectRow)
        {
            TableName = tbName;
            ReturntRow = returntRow;
            SelectRow = selectRow;
        }

        public GetValueRows(string tbName, string returntRow, string selectRow,string id)
        {
            TableName = tbName;
            ReturntRow = returntRow;
            SelectRow = selectRow;
            Id = id;
        }

        public GetValueRows()
        {
        }
    }
}
