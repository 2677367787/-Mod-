using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Data;

namespace xkfy_mod
{
    public partial class BattleNeigong : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public BattleNeigong()
        {
            InitializeComponent();
        }

        private void Content_Load(object sender, EventArgs e)
        {
            dg1.DataSource = DataHelper.xkfyData.Tables["BattleNeigong"].DefaultView;
            ToolsHelper tl = new ToolsHelper();
            tl.updateCellName("BattleNeigong", dg1);
        }

        private void btnAddNeiGong_Click(object sender, EventArgs e)
        {
            AddNeiGong ng = new AddNeiGong();
            ng.Owner = this;
            ng.ShowDialog();
        }

        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblMsg.Text = "";
            int index = dg1.CurrentRow.Index;
            string id = dg1.Rows[index].Cells[0].Value.ToString();
            ToolsHelper tl = new ToolsHelper();
            lblExplain.Text = tl.ExplainNeiGong(id);
        }
        
        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dg1.CurrentRow.Index;
            string id = dg1.Rows[index].Cells[0].Value.ToString();
            DataGridViewRow dr = this.dg1.CurrentRow;
            
            AddNeiGong ng = new AddNeiGong(id, dr,"0");
            ng.Owner = this;
            ng.ShowDialog();
        }


        private void btnCopyAdd_Click(object sender, EventArgs e)
        {
            int index = dg1.CurrentRow.Index;
            string id = dg1.Rows[index].Cells[0].Value.ToString();
            DataGridViewRow dr = this.dg1.CurrentRow;
            AddNeiGong ng = new AddNeiGong(id, dr, "2");
            ng.Owner = this;
            ng.ShowDialog();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtName.Text;
            string where = "1 = 1";
            if (!string.IsNullOrEmpty(id))
            {
                where += " and ID like '%" + id + "%' ";
            }

            if (!string.IsNullOrEmpty(name))
            {
                where += " and Name like '%" + name + "%' ";
            }
            DataView dv = dg1.DataSource as DataView;
            dv.RowFilter = where;
            this.dg1.DataSource = dv;
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
