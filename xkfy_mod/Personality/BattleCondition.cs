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
    public partial class BattleCondition : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public BattleCondition()
        {
            InitializeComponent();
        }

        private void BattleCondition_Load(object sender, EventArgs e)
        {
            this.dg1.DataSource = DataHelper.xkfyData.Tables["BattleCondition"].DefaultView;
            ToolsHelper tl = new ToolsHelper();
            tl.updateCellName("BattleCondition", dg1);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtName.Text;
            string where = "1 = 1";

            if (!string.IsNullOrEmpty(id))
            {
                where += " and ConditionID like '%" + id + "%' ";
            }

            if (!string.IsNullOrEmpty(name))
            {
                where += " and CondName like '%" + name + "%' ";
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
