using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using xkfy_mod.Entity;
using xkfy_mod.Helper;

namespace xkfy_mod
{
    public partial class RadioList : Form
    {　
        private readonly ChooseData _cd; 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cd"></param> 
        public RadioList(ChooseData cd)
        {
            _cd = cd;
            InitializeComponent();
        }

        private void RadioList_Load(object sender, EventArgs e)
        {
            if (_cd.SelType == "2")
                btnOK.Visible = true;
            label1.Text = _cd.Row[0];
            label2.Text = _cd.Row[1];
            textBox1.Tag = _cd.Row[0];
            textBox2.Tag = _cd.Row[1];
            dg1.AllowUserToAddRows = false;
            FileHelper.LoadTable(_cd.TableName);
            dg1.DataSource = DataHelper.XkfyData.Tables[_cd.TableName].Copy().DefaultView.ToTable(true, _cd.Row).DefaultView;
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

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = string.Empty;
            string name = string.Empty;
            if (!string.IsNullOrEmpty(_cd.TextId.Text))
                id = ",";
            if (!string.IsNullOrEmpty(_cd.TextId.Text))
                name = ",";
            id += this.dg1.CurrentRow.Cells[0].Value.ToString();
            name += this.dg1.CurrentRow.Cells[1].Value.ToString();

            if (_cd.SelType == "2")
            {
                if (_cd.TextId != null)
                    _cd.TextId.Text += id;
                if (_cd.TextName != null)
                    _cd.TextName.Text += name;
            }
            else
            {
                if (_cd.TextId != null)
                    _cd.TextId.Text = this.dg1.CurrentRow.Cells[0].Value.ToString(); ;
                if (_cd.TextName != null)
                    _cd.TextName.Text = this.dg1.CurrentRow.Cells[1].Value.ToString(); ;
            }
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string name = textBox2.Text;
            string where = "1 = 1";
            if (!string.IsNullOrEmpty(id))
            {
                where += " and " + textBox1.Tag + " like '%" + id + "%' ";
            }

            if (!string.IsNullOrEmpty(name))
            {
                where += " and " + textBox2.Tag + " like '%" + name + "%' ";
            }
            DataView dv = dg1.DataSource as DataView;
            dv.RowFilter = where;
            this.dg1.DataSource = dv;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string id = string.Empty;
            string name = string.Empty;
            if (!string.IsNullOrEmpty(_cd.TextId.Text))
                id = ",";
            if (!string.IsNullOrEmpty(_cd.TextId.Text))
                name = ",";

            foreach (DataGridViewRow row in dg1.SelectedRows)
            {
                id += row.Cells[0].Value + ",";
                name += row.Cells[1].Value + ",";
            }
            if(_cd.TextId != null)
                _cd.TextId.Text += id.TrimEnd(new char[] { ',' });

            if (_cd.TextName != null)
                _cd.TextName.Text += name.TrimEnd(new char[] { ',' });
            Close();
        }
    }
}
