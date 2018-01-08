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
    public partial class NeigongUpValue : DockContent
    {
        public NeigongUpValue()
        {
            InitializeComponent();
        }

        private void NeigongUpValue_Load(object sender, EventArgs e)
        {
            if (!DataHelper.xkfyData.Tables.Contains("NeigongUpValue"))
            {
                
            }
            dg1.DataSource = DataHelper.xkfyData.Tables["NeigongUpValue"];
        }

        private void dg1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string id = dg1.Rows[dg1.CurrentRow.Index].Cells["iid"].Value.ToString();
            NeigongUpValue_Edit ne = new NeigongUpValue_Edit(id);
            ne.ShowDialog();
        }

        private void dg1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }

        private void btnAddNg_Click(object sender, EventArgs e)
        {
            NeigongUpValue_Edit ne = new NeigongUpValue_Edit();
            ne.ShowDialog();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {

        }

        private void dg1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
    }
}
