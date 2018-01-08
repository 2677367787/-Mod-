using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Entity;
using xkfy_mod.Helper;
using xkfy_mod.Utils;

namespace xkfy_mod.UserContrl
{
    public partial class AnnotationCtrl : UserControl
    {
        public string ConfigKeyName;
        public AnnotationCtrl()
        {
            InitializeComponent();
        } 
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IList<TableExplain> list = FileHelper.GetColumnData(ConfigKeyName);
                foreach (TableExplain te in list)
                {
                    if (StringUtils.FormatToolColumn(te.Column) == txtExplain.Tag.ToString())
                    {
                        te.Explain = txtExplain.Text;
                        te.Text = txtForShort.Text;
                    }
                }
                DataHelper.SaveData(ConfigKeyName, list);
                label3.Text = @"修改成功";
            }
            catch (Exception ex)
            {
                label3.Text = ex.Message;
            }
        }
    }
}
