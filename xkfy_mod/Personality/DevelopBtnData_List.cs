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
    public partial class DevelopBtnData_List : Form
    {
        public DevelopBtnData_List()
        {
            InitializeComponent();
        }

        private void DevelopBtnData_List_Load(object sender, EventArgs e)
        {
            dg1.AllowUserToAddRows = false;
            this.dg1.AutoGenerateColumns = false;
            dg1.DataSource = DataHelper.xkfyData.Tables["DevelopBtnData"];
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               e.RowBounds.Location.Y,
               dg1.RowHeadersWidth - 4,
               e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dg1.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dg1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvDrc = this.dg1.CurrentRow;
            DataRow dr = DataHelper.xkfyData.Tables["DevelopBtnData"].Select("iBtnID='" + dgvDrc.Cells["iBtnID"].Value + "'")[0];

            DevelopBtnData_Edit ie = (DevelopBtnData_Edit)this.Owner;
            DataHelper.CopyRowToData(ie, dr);
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string where = "1 = 1";

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
    }
}
