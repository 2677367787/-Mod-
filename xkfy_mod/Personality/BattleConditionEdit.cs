using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Helper;
using xkfy_mod.SetUp;

namespace xkfy_mod.Personality
{
    public partial class BattleConditionEdit : Form
    { 
        private readonly DataGridViewRow _dr;
        private readonly string _type;
        private string _initialId = string.Empty;

        private readonly DataTable _battleConditionD = DataHelper.XkfyData.Tables["BattleCondition_D"];
        private readonly DataTable _conditionD = DataHelper.XkfyData.Tables["BattleCondition_D"].Clone();

        public BattleConditionEdit(DataGridViewRow dr, string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        private void BattleCondition_Edit_Load(object sender, EventArgs e)
        {
            DataHelper.BinderComboBox(cboEffectType1, Const.BaCoEffecttype);
            DataHelper.BinderComboBox(cboAccumulate, Const.BaCoAccumulate);

            if (_type != "Add")
            {
                DataHelper.SetCtrlByDataRow(groupBox1, _dr);
                _initialId = txtConditionID.Text;
                SetdgXiaoGuo();
            }
            dgXiaoGuo.DataSource = _conditionD;
            if (_conditionD.Rows.Count > 0)
            {
                dgXiaoGuo.Rows[0].Selected = true;
                dgXiaoGuo_CellClick(null,null);
            }
        }

        #region 绑定内功效果的DataTable的值到页面控件上
        /// <summary>
        /// 绑定内功效果的DataTable的值到页面控件上
        /// </summary>
        public void SetdgXiaoGuo()
        {
            DataRow[] bnD = _battleConditionD.Select("ConditionID='" + txtConditionID.Text + "'");
            foreach (DataRow item in bnD)
            {
                _conditionD.ImportRow(item);
            }
        }
        #endregion

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
            DefaultValue();
            DataRow dr = _conditionD.NewRow();
            dr["ConditionID"] = txtConditionID.Text;
            DataHelper.SetDataRowByCtrl(groupBox2, dr);
            _conditionD.Rows.Add(dr);
        }

        DataGridViewRow _dgvDrc; 

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
                DataRow[] mdRow = _battleConditionD.Select("ConditionID='" + ngId + "'");
                foreach (DataRow item in mdRow)
                {
                    _battleConditionD.Rows.Remove(item);
                }

                DataHelper.XkfyData.Tables["BattleCondition_D"].Merge(_conditionD);
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
                DataHelper.XkfyData.Tables["BattleCondition_D"].Merge(_conditionD);
            }
            _dr.DataGridView.CurrentCell = null;
            Close();
        }

        private void dgXiaoGuo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CleatState();

            _dgvDrc = dgXiaoGuo.CurrentRow;
            if (_dgvDrc == null)
                return;
            DataHelper.SetCtrlByDataRow(groupBox2, _dgvDrc);
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
                txtXgName2.Visible = true;
                btnSelBattleCondition2.Visible = true;
                txtXgName2.Text = ExplainHelper.GetConditionName(txtValue2.Text);
            }
            lblMsg.Text = $"当前修改第{_dgvDrc.Index + 1}行数据";
        }

        private void txtConditionID_TextChanged(object sender, EventArgs e)
        {
            DataTable dataTable = dgXiaoGuo.DataSource as DataTable;
            if (dataTable == null) return;
            foreach (DataRow dr in dataTable.Rows)
            {
                dr[0] = txtConditionID.Text;
            }
        }

        private void cboEffectType1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSelBattleCondition_Click(object sender, EventArgs e)
        {
            ToolsHelper.OpenBattleCondition(txtValue1, txtXgName, Const.OpenType.Radio);
        }

        private void btnSelBattleCondition2_Click(object sender, EventArgs e)
        {
            ToolsHelper.OpenBattleCondition(txtValue2, txtXgName2, Const.OpenType.Radio);
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
            fs.ShowDialog();
        }

        private void 生效方式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetCbo fs = new FrmSetCbo(cboAccumulate, PathHelper.BaCoAccumulatePath);
            fs.ShowDialog();
        }
    }
}
