using System;
using System.Windows.Forms;
using xkfy_mod.Helper;

namespace xkfy_mod
{
    public partial class FrmRound : Form
    {
        private readonly TextBox _txtId;
        private readonly TextBox _txtName;
        public FrmRound(TextBox txtId, TextBox txtName)
        {
            _txtId = txtId;
            _txtName = txtName;
            InitializeComponent();
        }

        private void frmRound_Load(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            dg1.DataSource = bs;
            bs.DataSource = DataHelper.DropDownListDict["Public.HuiHe"];
        } 

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_txtId == null) return;
            if (dg1.CurrentRow != null)
            {
                _txtId.Text = dg1.CurrentRow.Cells[0].Value.ToString(); ;
                _txtName.Text = dg1.CurrentRow.Cells[1].Value.ToString();
            } 
            Close();
        }
    }
}
