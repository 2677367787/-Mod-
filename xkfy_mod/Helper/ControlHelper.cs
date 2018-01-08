/***************************************************** 
** 命名空间：xkfy_mod.Helper
** 文件名称：ControlHelper
** 内容简述：控件类.提取所有和控件相关的操作封装
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2017/12/16 22:51:37
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Entity;
using xkfy_mod.Utils;

namespace xkfy_mod.Helper
{
    public class ControlHelper
    {
        #region 创建控件

        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="form"></param>
        /// <param name="dr"></param>
        /// <param name="list"></param>
        /// <param name="toolTip"></param>
        public static void BuildControl(Form form, DataGridViewRow dr, IList<TableExplain> list,ToolTip toolTip)
        { 
            int top = 15;    //顶部起点像素位置
            int left = 15;   //左边起点像素位置 

            foreach (TableExplain columnInfo in list)
            {
                string columnName = StringUtils.FormatToolColumn(columnInfo.Column);
                if (StringUtils.ContainsIgnoreCase("rowState,npcName,Key,indexSn",columnName))
                    continue; 

                string value = dr.Cells[columnName].Value.ToString();
                
                var width = columnInfo.Width; //控件宽度 
                var height = columnInfo.Height; //控件高度 

                if (900 - left - 100 < width)
                {
                    left = 15; 
                    top += 30;
                }
                string labText = columnInfo.Column;
                if (!string.IsNullOrEmpty(columnInfo.Text))
                {
                    labText = columnInfo.Text;
                }
                Label tips = new Label
                {
                    Text = labText,
                    Top = top + 4,
                    Left = left,
                    Width = 100,
                    TextAlign = ContentAlignment.TopRight
                };

                left += tips.Width+10;
                //如果填写了列的注释信息,就附加上去
                if (!string.IsNullOrEmpty(columnInfo.Explain))
                { 
                    toolTip.SetToolTip(tips, columnInfo.Explain);
                } 

                TextBox tb = new TextBox
                {
                    Tag = columnName,
                    Text = value,
                    Top = top,
                    Left = left,
                    Width = width
                };

                left += tb.Width + 5;
                if (height > 21)
                {
                    tb.Multiline = true;
                    tb.Height = height;
                    top += height + 9;
                    left = 15;
                }
                
                form.Controls.Add(tips);
                form.Controls.Add(tb);
            } 
            form.Height = top + 180;
        }
        #endregion

        #region 生成查询控件 
        /// <summary>
        /// 生成查询控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="toolColumn"></param>
        /// <param name="toolTip"></param>
        public static void BuidQueryControl(Control control, TableExplain toolColumn, ToolTip toolTip)
        {
            int top = 12;
            int left = 5;
            int width = 90;
            int controlCount = control.Controls.Count-2;
            if (controlCount == 10)
            {
                top = 40;
                left = 5;
            }
            else
            {
                if (controlCount > 10)
                {
                    controlCount = controlCount - 10;
                    top = 40;
                }
                left += controlCount != 0 ? controlCount*90 : 0;
            }

            Label tips = new Label
            {
                Top = top + 4,
                Left = left,
                Width = width,
                TextAlign = ContentAlignment.TopRight
            };
            left += tips.Width + 5;

            string columnName = StringUtils.FormatToolColumn(toolColumn.Column);
            if (toolColumn.IsDropDownList == 1)
            {
                ComboBox cb = new ComboBox
                {
                    Left = left,
                    Top = top,
                    Width = width,
                    Tag = columnName,
                    Name = columnName,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                DataHelper.BinderComboBox(cb, toolColumn.DataKey);
                control.Controls.Add(cb);
            }
            else
            {
                TextBox tb = new TextBox
                {
                    Name = columnName,
                    Tag = columnName,
                    Top = top,
                    Left = left,
                    Width = width
                };
                control.Controls.Add(tb);
            }


            control.Controls.Add(tips); 
            //把lable文字修改为中文显示
            tips.Text = !string.IsNullOrEmpty(toolColumn.Text) ? toolColumn.Text : columnName;
            toolTip.SetToolTip(tips, toolColumn.Explain); 
        }

        #endregion
    }
}
