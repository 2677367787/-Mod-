using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Helper;

namespace xkfy_mod
{
    public partial class ItemData : DockContent
    {
        public ItemData()
        {
            InitializeComponent();
        }

        private void ItemData_Load(object sender, EventArgs e)
        {
            dg1.DataSource = DataHelper.XkfyData.Tables["ItemData"].DefaultView;
            ToolsHelper tl = new ToolsHelper();
            tl.updateCellName("ItemData", dg1);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtName.Text;
            string npcID = txtNpcId.Text;
            string where = "1 = 1";
            if (!string.IsNullOrEmpty(id))
            {
                where += " and iItemID$0 like '%" + id + "%' ";
            }

            if (!string.IsNullOrEmpty(name))
            {
                where += " and sItemName$1 like '%" + name + "%' ";
            }

            if (!string.IsNullOrEmpty(npcID))
            {
                where += " and sNpcLike$28 like '%" + npcID + "%' ";
            }
            DataView dv = dg1.DataSource as DataView;
            dv.RowFilter = where;
            this.dg1.DataSource = dv;
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.dg1.CurrentRow;
            ItemData_Edit ie = new ItemData_Edit(dr,"0");
            ie.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ItemData_Edit ie = new ItemData_Edit("1");
            ie.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = this.dg1.CurrentRow;
            ItemData_Edit ie = new ItemData_Edit(dr,"2");
            ie.ShowDialog();
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
            string rowState = "";
            if (dg1.Rows[e.RowIndex].Cells["rowState"].Value != null)
            {
                rowState = dg1.Rows[e.RowIndex].Cells["rowState"].Value.ToString();
                if (rowState == "0")
                {
                    dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;
                }
                else if (rowState == "1")
                {
                    dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }

        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private DataRow copyRow = null;
        private void tsmCopyRow_Click(object sender, EventArgs e)
        {
            DataRow dr = (dg1.DataSource as DataView).Table.NewRow();
            for (int i = 0; i < dg1.Columns.Count; i++)
            {
                dr[dg1.Columns[i].Name] = dg1.CurrentRow.Cells[dg1.Columns[i].Name].Value;
            }
            dr["rowState"] = "1";
            copyRow = dr;
        }

        private void tsmInsertCopyRow_Click(object sender, EventArgs e)
        {
            (dg1.DataSource as DataView).Table.Rows.InsertAt(copyRow, dg1.CurrentRow.Index);
            dg1.Rows[dg1.CurrentRow.Index - 1].DefaultCellStyle.BackColor = Color.MistyRose;
            copyRow = null;
        }

        private void tsmDelRow_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = MessageBox.Show("是否确定要删除当前选择的行？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogR == DialogResult.Yes)
            {
                dg1.Rows.Remove(this.dg1.CurrentRow);
            }
        }

        private void tsmInsertRow_Click(object sender, EventArgs e)
        {
            DataRow dr = (dg1.DataSource as DataView).Table.NewRow();
            (dg1.DataSource as DataTable).Rows.InsertAt(dr, dg1.CurrentCell.RowIndex);
            dg1.Rows[dg1.CurrentRow.Index - 1].DefaultCellStyle.BackColor = Color.MistyRose;
        }

        private void dg1RightMenu_Opening(object sender, CancelEventArgs e)
        {
            if (copyRow == null)
            {
                tsmInsertCopyRow.Enabled = false;
            }
            else
            {
                tsmInsertCopyRow.Enabled = true;
            }
        }

        private void dg1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (string.IsNullOrEmpty(dg1.Rows[e.RowIndex].Cells["rowState"].Value.ToString()))
                dg1.Rows[e.RowIndex].Cells["rowState"].Value = "0";
            dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;
        }

        private void dg1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dg1.CurrentRow == null)
                return;
            
        }
    }
}
