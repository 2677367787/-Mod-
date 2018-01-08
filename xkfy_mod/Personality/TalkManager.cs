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
    public partial class TalkManager : DockContent
    {
        public TalkManager()
        {
            InitializeComponent();
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string sManager = txtName.Text;
            string where = "1 = 1";
            if (!string.IsNullOrEmpty(id))
            {
                where += " and iQGroupID like '%" + id + "%' ";
            }

            if (!string.IsNullOrEmpty(sManager))
            {
                where += " and sManager like '%" + sManager + "%' ";
            }

            if (!string.IsNullOrEmpty(txtiGtype1.Text))
            {
                where += " and iGtype1 like '%" + txtiGtype1.Text + "%' ";
            }

            if (!string.IsNullOrEmpty(txtiGtype2.Text))
            {
                where += " and iGtype2 like '%" + txtiGtype2.Text + "%' ";
            }

            if (!string.IsNullOrEmpty(txtiGtype3.Text))
            {
                where += " and iGtype3 like '%" + txtiGtype3.Text + "%' ";
            }
            DataView dv = dg1.DataSource as DataView;
            dv.RowFilter = where;
            this.dg1.DataSource = dv;
        }

        private void TalkManager_Load(object sender, EventArgs e)
        {
            dg1.DataSource = DataHelper.xkfyData.Tables["TalkManager"].DefaultView;
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = dg1.CurrentRow;
            TalkManager_Edit ne = new TalkManager_Edit(dr, "0");
            ne.ShowDialog();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            
        }

        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ToolsHelper tl = new ToolsHelper();
            DataGridViewRow dv = dg1.CurrentRow;
            label3.Text = tl.ExplainTalkManager(dv);
        }
    }
}
