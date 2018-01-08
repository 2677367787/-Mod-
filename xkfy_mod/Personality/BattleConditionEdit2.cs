using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Helper;
using xkfy_mod.SetUp;

namespace xkfy_mod.Personality
{
    public partial class BattleConditionEdit2 : Form
    {
        private readonly DataGridViewRow _dr;
        private readonly string _type;
        private string _initialId = string.Empty;
        private DataGridViewRow _dgvDrc;
        private bool _valid = true;
        private readonly DataTable _battleConditionD = DataHelper.XkfyData.Tables["BattleCondition_D"];

        public BattleConditionEdit2(DataGridViewRow dr, string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        private void BattleCondition_Edit_Load(object sender, EventArgs e)
        { 
            try
            {
                DataHelper.BinderComboBox(cboEffectType1, Const.BaCoEffecttype);
                DataHelper.BinderComboBox(cboAccumulate, Const.BaCoAccumulate);
                if (_type == "Add")
                {

                }
                else if (_type != "Add")
                {
                    DataHelper.SetCtrlByDataRow(groupBox1, _dr);
                    _initialId = txtConditionID.Text;
                    if (_type == "CopyAdd")
                    {
                        txtConditionID.Text = Const.Zero;
                        DataRow[] newRow = _battleConditionD.Select("ConditionID='" + _initialId + "'");
                        foreach (var drs in newRow)
                        {
                            DataRow dr = _battleConditionD.NewRow();
                            dr[0] = Const.Zero;
                            for (int i = 1; i < _battleConditionD.Columns.Count; i++)
                            {
                                dr[i] = drs[i];
                            }
                            _battleConditionD.Rows.Add(dr);
                            _initialId = Const.Zero;
                        }
                    }
                    DataView dv = _battleConditionD.DefaultView;
                    string where = $"ConditionID='{_initialId}'";
                    dv.RowFilter = where;
                    dgXiaoGuo.DataSource = dv;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dg1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dgXiaoGuo.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgXiaoGuo.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgXiaoGuo.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void btnAddXg_Click(object sender, EventArgs e)
        {
            if (!_valid)
            {
                lblMsg.Text = @"ID已经存在,请先修改ID在新增！！";
                return;
            }

            if (string.IsNullOrEmpty(txtConditionID.Text))
            {
                lblMsg.Text = @"ID不能为空！！";
                return;
            }

            DefaultValue();
            DataRow dr = _battleConditionD.NewRow();
            dr["ConditionID"] = txtConditionID.Text;
            DataHelper.SetDataRowByCtrl(groupBox2, dr);
            _battleConditionD.Rows.Add(dr);
        } 

        private void btnSaveXg_Click(object sender, EventArgs e)
        {
            DefaultValue();
            //修改页面表格的值
            if (_dgvDrc.DataGridView != null)
            {
                DataHelper.SetDataRowByCtrl(groupBox2, _dgvDrc);
            }
            dgXiaoGuo.CurrentCell = null;
            //恢复控件的初始状态
            CleatState();
            lblMsg.ForeColor = Color.Blue;
            lblMsg.Text = @"修改选中的效果成功！";
        }

        private void DefaultValue()
        {
            if (string.IsNullOrEmpty(txtValue1.Text))
                txtValue1.Text = Const.Zero;
            if (string.IsNullOrEmpty(txtValue2.Text))
                txtValue2.Text = Const.Zero;
            if (string.IsNullOrEmpty(txtValueLimit.Text))
                txtValueLimit.Text = Const.Zero;
        }

        private void CleatState()
        {
            cboAccumulate.SelectedIndex = 0;
            cboEffectType1.SelectedIndex = 0;
            txtValue1.Text = "";
            txtValue2.Text = "";
            txtValueLimit.Text = "";
            btnAddXg.Visible = true;
            btnSaveXg.Visible = false;

            txtXgName.Text = "";
            txtXgName2.Text = "";
            txtXgName.Visible = false;
            txtXgName.Visible = false;
            btnSelBattleCondition2.Visible = false;
            btnSelBattleCondition.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblMsg.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtConditionID.Text))
            {
                lblMsg.Text = @"特效ID已经存在，请修改！";
                return;
            }
            string ngId = txtConditionID.Text;
            if (_type == "Modify")
            {
                if (_initialId != ngId)
                {
                    DataRow[] bnDr = _battleConditionD.Select("ConditionID='" + ngId + "'");
                    if (bnDr.Length > 0)
                    {
                        lblMsg.Text = @"特效ID已经存在，请修改！";
                        return;
                    }
                }
                DataHelper.SetDataRowByCtrl(groupBox1, _dr);
                if (string.IsNullOrEmpty(_dr.Cells["rowState"].Value.ToString()))
                    _dr.Cells["rowState"].Value = "0";
            }
            else
            {
                DataRow[] bnDr = DataHelper.XkfyData.Tables["BattleCondition"].Select("ConditionID='" + ngId + "'");
                if (bnDr.Length > 0)
                {
                    lblMsg.Text = @"特效ID已经存在，请修改！！";
                    return;
                }
                DataRow bnNewRow = DataHelper.XkfyData.Tables["BattleCondition"].NewRow();
                DataHelper.SetDataRowByCtrl(groupBox1, bnNewRow);
                if (string.IsNullOrEmpty(bnNewRow["rowState"].ToString()))
                    bnNewRow["rowState"] = "1";
                DataHelper.XkfyData.Tables["BattleCondition"].Rows.InsertAt(bnNewRow, 0); 
            }
            _dr.DataGridView.CurrentCell = null;
            Close();
        }

        private bool IsExists(string id)
        {
            DataRow[] bnDr = DataHelper.XkfyData.Tables["BattleCondition"].Select("ConditionID='" + id + "'");
            if (bnDr.Length <= 0) return true; 
            _valid = false;
            return false;
        }

        private void dgXiaoGuo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CleatState(); 

            _dgvDrc = dgXiaoGuo.CurrentRow;
            DataHelper.SetCtrlByDataRow(groupBox1, _dgvDrc);

            btnAddXg.Visible = false;
            btnSaveXg.Visible = true;

            if (txtValue1.Text.Length > 5)
            {
                txtXgName.Visible = true;
                btnSelBattleCondition.Visible = true;
                txtXgName.Text = ExplainHelper.GetConditionName(txtValue1.Text);
            }

            if (txtValue2.Text.Length > 5)
            {
                txtXgName.Visible = true;
                btnSelBattleCondition2.Visible = true;
                txtXgName.Text = ExplainHelper.GetConditionName(txtValue2.Text);
            }
            if (dgXiaoGuo.CurrentRow != null) lblMsg.Text = $"当前修改第{(dgXiaoGuo.CurrentRow.Index + 1)}行数据";
        }
        
        private void cboEffectType1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSelBattleCondition_Click(object sender, EventArgs e)
        {
            ToolsHelper.OpenBattleCondition(txtValue1, txtXgName, "radio");
        }

        private void btnSelBattleCondition2_Click(object sender, EventArgs e)
        { 
            ToolsHelper.OpenBattleCondition(txtValue2, txtXgName2, "radio");
        }

        private void cboAccumulate_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelBattleCondition.Visible = false;
            btnSelBattleCondition2.Visible = false;
            txtXgName.Visible = false;
            txtXgName2.Visible = false;
            if (cboAccumulate.SelectedValue == null)
                return;
            switch (cboAccumulate.SelectedValue.ToString())
            {
                case "7":
                case "10":
                    btnSelBattleCondition.Visible = true;
                    btnSelBattleCondition2.Visible = true;
                    txtXgName.Visible = true;
                    txtXgName2.Visible = true;
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CleatState();
        }

        private void 招式效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmSetCbo fs = new FrmSetCbo(cboEffectType1, PathHelper.BaCoEffecttypePath);
            fs.Show();
        }

        private void 生效方式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetCbo fs = new FrmSetCbo(cboAccumulate, PathHelper.BaCoAccumulatePath);
            fs.Show();
        }

        private void txtConditionID_Leave(object sender, EventArgs e)
        {
            if (_valid) return;
            lblMsg.Text = @"ID设置有问题，请修改！！";
            txtConditionID.Focus();
        }

        
        private void txtConditionID_TextChanged_1(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (string.IsNullOrEmpty(txtConditionID.Text))
            {
                lblMsg.Text = @"ID设置有问题，请修改！！";
                return;
            }

            if (!IsExists(txtConditionID.Text))
            {
                lblMsg.Text = @"特效ID已经存在，请修改！！";
                return;
            }

            DataView dataTable = dgXiaoGuo.DataSource as DataView;
            if (dataTable == null) return;
            foreach (DataRow dr in dataTable.Table.Select("ConditionID='" + _initialId + "'"))
            {
                dr[0] = txtConditionID.Text;
            }
            _initialId = txtConditionID.Text;
            string where = $"ConditionID='{_initialId}'";
            dataTable.RowFilter = where;
            dgXiaoGuo.DataSource = dataTable;
            _valid = true;
        }
    }
}
