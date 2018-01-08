using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Helper;

namespace xkfy_mod.Personality
{
    public partial class DevelopQuestData : DockContent
    {
        private DataTable dt = null;
        private DataRow copyRow = null;
        private Dictionary<string, string> diciType = null;
        public DevelopQuestData()
        {
            InitializeComponent();
        }
        
        private void DevelopQuestData_Load(object sender, EventArgs e)
        {
            dg1.AllowUserToAddRows = false;
            dg1.DataSource = DataHelper.XkfyData.Tables["DevelopQuestData"].DefaultView;
            dt = DataHelper.XkfyData.Tables["Config"];

            diciType = DataHelper.ExplainConfig["DevelopQuest"];
            if(!diciType.ContainsKey(""))
                diciType.Add("", "--空--");
            BindingSource bs = new BindingSource();
            bs.DataSource = diciType;
            cboDd.DataSource = bs;
            cboDd.DisplayMember = "Value";
            cboDd.ValueMember = "Key";
            cboDd.SelectedValue = "";
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string iArg3 = txtiArg3.Text;
            string sEndAdd = txtsEndAdd.Text;
            string dd = cboDd.SelectedValue.ToString();
            string where = "1 = 1";
            if (!string.IsNullOrEmpty(id))
            {
                where += " and iID like '%" + id + "%' ";
            }

            if (!string.IsNullOrEmpty(iArg3))
            {
                where += " and iArg3 like '%" + iArg3 + "%' ";
            }

            if (!string.IsNullOrEmpty(sEndAdd))
            {
                where += " and sEndAdd like '%" + sEndAdd + "%' ";
            }

            if (!string.IsNullOrEmpty(dd))
            {
                where += " and itype = '" + dd + "'";
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

        private void btnDebug_Click(object sender, EventArgs e)
        {
            
            int index = dg1.CurrentRow.Index;
            string id = dg1.Rows[index].Cells["iID"].Value.ToString();
            DataGridViewRow dr = this.dg1.CurrentRow;

            //AddNeiGong ng = new AddNeiGong(dr,dtNew);

            TalkDeBug td = new TalkDeBug(id);
            td.Show();
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.dg1.CurrentRow;
            DevelopQuestData_Edit ae = new DevelopQuestData_Edit(dr, "0");
            ae.ShowDialog();
        }

        private void dg1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (string.IsNullOrEmpty(dg1.Rows[e.RowIndex].Cells["rowState"].Value.ToString()))
                dg1.Rows[e.RowIndex].Cells["rowState"].Value = "0";

            dg1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;
        }

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

        private void tsmInsertRow_Click(object sender, EventArgs e)
        {
            DataRow dr = (dg1.DataSource as DataView).Table.NewRow();
            (dg1.DataSource as DataView).Table.Rows.InsertAt(dr, dg1.CurrentCell.RowIndex);
            dg1.Rows[dg1.CurrentRow.Index - 1].DefaultCellStyle.BackColor = Color.MistyRose;
        }

        private void tsmDelRow_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = MessageBox.Show("是否确定要删除当前选择的行？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogR == DialogResult.Yes)
            {
                dg1.Rows.Remove(this.dg1.CurrentRow);
            }
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

        private void btnCopyAdd_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = this.dg1.CurrentRow;
            DevelopQuestData_Edit ae = new DevelopQuestData_Edit(dr, "2");
            ae.Show();
        }

        private void dg1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dg1.CurrentRow == null)
                return;
            DataGridViewRow dv = dg1.CurrentRow;
            string[] striType = dv.Cells["iType"].Value.ToString().Split(',');
            string[] striArg1 = dv.Cells["iArg1"].Value.ToString().Split(',');
            string[] striArg2 = dv.Cells["iArg2"].Value.ToString().Split(',');
            string[] striCondition = dv.Cells["iCondition"].Value.ToString().Split(',');
            ToolsHelper tl = new ToolsHelper();
            string[] explain = tl.ExplainDevelopQuest(striCondition, striType, striArg1, striArg2);
            txtQztj.Text = explain[0] + "\r\n\r\n" + explain[1];
        }
    }
}
