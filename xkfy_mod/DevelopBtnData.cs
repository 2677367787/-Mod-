using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Data; 

namespace xkfy_mod
{
    public partial class DevelopBtnData : DockContent
    {
        public DevelopBtnData()
        {
            InitializeComponent();
        }

        private void DevelopBtnData_Load(object sender, EventArgs e)
        {
            dg1.DataSource = DataHelper.xkfyData.Tables["DevelopBtnData"];
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtName.Text;
            string where = "1 = 1";
            if (!string.IsNullOrEmpty(id))
            {
                where += " and iBtnID like '%" + id + "%' ";
            }

            if (!string.IsNullOrEmpty(name))
            {
                where += " and xRemark like '%" + name + "%' ";
            }
            DataRow[] row = DataHelper.xkfyData.Tables["DevelopBtnData"].Select(where);

            DataTable dtNew = DataHelper.xkfyData.Tables["DevelopBtnData"].Clone();
            for (int i = 0; i < row.Length; i++)
            {
                dtNew.ImportRow(row[i]);
            }
            this.dg1.DataSource = dtNew;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DevelopBtnData_Edit dbe = new DevelopBtnData_Edit();
            dbe.Show();
        }
    }
}
