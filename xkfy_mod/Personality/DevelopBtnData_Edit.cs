using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class DevelopBtnData_Edit : Form
    {
        public DevelopBtnData_Edit()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            DevelopBtnData_List dl = new DevelopBtnData_List();
            dl.ShowDialog(this);
        }

        private void DevelopBtnData_Edit_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow dr = DataHelper.xkfyData.Tables["DevelopBtnData"].NewRow();
            //DataHelper.CopyDataToRow(this, DataHelper.xkfyData.Tables["DevelopBtnData"], dr);
            DataHelper.xkfyData.Tables["DevelopBtnData"].Rows.InsertAt(dr, 0);
        }
    }
}
