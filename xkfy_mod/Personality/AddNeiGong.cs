using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Data;
using xkfy_mod.Helper;
using xkfy_mod.SetUp;

namespace xkfy_mod.Personality
{
    public partial class AddNeiGong : DockContent
    {
        /// <summary>
        /// 保存内功的基本信息，从别的页面传递过来
        /// </summary>
        private readonly DataGridViewRow _dr;

        /// <summary>
        /// 保存当前选中的内功效果信息
        /// </summary>
        private DataGridViewRow _dgvDrc;
        
        private readonly string _type;
        private string _initialId = string.Empty;

        private readonly DataTable _battleNeigongD = DataHelper.XkfyData.Tables["BattleNeigong_D"];
        private readonly DataTable _neiGongD = DataHelper.XkfyData.Tables["BattleNeigong_D"].Clone();
        public AddNeiGong(DataGridViewRow dr,string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        public AddNeiGong()
        {
            InitializeComponent();
        }

        private void AddNeiGong_Load(object sender, EventArgs e)
        {
            DataHelper.BinderComboBox(cboAccumulate, Const.BaNeAccumulate);
            DataHelper.BinderComboBox(cboEffectType1, Const.BaNeEffecttype);
            dgXiaoGuo.AllowUserToAddRows = false;
            toolTip1.SetToolTip(label9, "内功升级所需要的经验,数字越大,经验要求越高,目前只支持1，2，3，4，4个级别");
            toolTip1.SetToolTip(label6, "修改后作用不详，暂时不知道有什么卵用");
            if (_type != "Add")
            {
                DataHelper.SetCtrlByDataRow(gbJiBenInfo, _dr);
                _initialId = txtID.Text;
                SetdgXiaoGuo();
            }
            dgXiaoGuo.DataSource = _neiGongD; 
        }

        #region 绑定内功效果的DataTable的值到页面控件上
        /// <summary>
        /// 绑定内功效果的DataTable的值到页面控件上
        /// </summary>
        public void SetdgXiaoGuo()
        {
            DataRow[] bnD = _battleNeigongD.Select("ID='" + txtID.Text + "'");
            foreach (DataRow item in bnD)
            {
                _neiGongD.ImportRow(item);
            }
        }
        #endregion

        #region 新增效果
        private void btnAddXG_Click(object sender, EventArgs e)
        {
            DefaultValue();
            DataRow dr = _neiGongD.NewRow();
            dr["ID"] = txtID.Text;
            DataHelper.SetDataRowByCtrl(groupBox1, dr);
            _neiGongD.Rows.Add(dr);
        } 
        #endregion

        private void cboEffectType1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            switch (cboEffectType1.SelectedValue.ToString())
            {
                //没有数据只有效果的如必中，连斩，解除负面都是填4
                case "23":
                case "20":
                case "8":
                    cboAccumulate.SelectedValue = "4";
                    txtValue1.Text = Const.Zero;
                    txtValue2.Text = Const.Zero;
                    txtValueLimit.Text = Const.Zero;
                    break;
            }
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

        #region 保存选中的效果的值
        /// <summary>
        /// 保存选中的效果的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveXg_Click(object sender, EventArgs e)
        {
            DefaultValue();
            //修改页面表格的值
            if (_dgvDrc.DataGridView != null)
            {
                DataHelper.SetDataRowByCtrl(groupBox1, _dgvDrc);
            }
            dgXiaoGuo.CurrentCell = null;
            //恢复控件的初始状态
            cboAccumulate.SelectedIndex = 0;
            cboEffectType1.SelectedIndex = 0;
            txtValue1.Text = "";
            txtValue2.Text = "";
            txtValueLimit.Text = "";
            btnAddXg.Visible = true;
            btnSaveXg.Visible = false;
            lblMsg.ForeColor = Color.Blue;
            lblMsg.Text = @"修改选中的内功效果值成功！";
        } 
        #endregion

        /// <summary>
        /// 保存所有信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblMsg.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtID.Text))
            {
                lblMsg.Text = @"请填写内功ID！";
                return;
            }
            string ngId = txtID.Text;
            if (_type == "Modify")
            {
                if (_initialId != ngId)
                {
                    DataRow[] bnDr = DataHelper.XkfyData.Tables["BattleNeigong"].Select("id='" + ngId + "'");
                    if (bnDr.Length > 0)
                    {
                        lblMsg.Text = @"此内功的ID已经存在，请修改！！";
                        return;
                    }
                }
                DataRow[] mdRow = DataHelper.XkfyData.Tables["BattleNeigong_D"].Select("id='" + ngId + "'");
                foreach (DataRow item in mdRow)
                {
                    DataHelper.XkfyData.Tables["BattleNeigong_D"].Rows.Remove(item);
                }

                DataHelper.XkfyData.Tables["BattleNeigong_D"].Merge(_neiGongD);
                
                DataHelper.SetDataRowByCtrl(gbJiBenInfo, _dr);
                if (string.IsNullOrEmpty(_dr.Cells["rowState"].Value.ToString()))
                    _dr.Cells["rowState"].Value = Const.Zero;
            }
            else
            {
                DataRow[] bnDr = DataHelper.XkfyData.Tables["BattleNeigong"].Select("id='" + ngId + "'");
                if (bnDr.Length > 0)
                {
                    lblMsg.Text = @"此内功的ID已经存在，请修改！！";
                    return;
                }
                DataRow bnNewRow = DataHelper.XkfyData.Tables["BattleNeigong"].NewRow();
                DataHelper.SetDataRowByCtrl(gbJiBenInfo, bnNewRow);
                if (string.IsNullOrEmpty(bnNewRow["rowState"].ToString()))
                    bnNewRow["rowState"] = "1";
                DataHelper.XkfyData.Tables["BattleNeigong"].Rows.InsertAt(bnNewRow, 0);
                DataHelper.XkfyData.Tables["BattleNeigong_D"].Merge(_neiGongD);
            }
            _dr.DataGridView.CurrentCell = null;
            Close();
        }

        #region 生成序号
        private void dgXiaoGuo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
        #endregion

        private void cboAccumulate_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblValue2.Text = @"value2【值2】";
            lblValue1.Text = @"value1【值1】";
            if (cboAccumulate.SelectedValue.ToString() == "7" || cboAccumulate.SelectedValue.ToString() == "10")
            {
                btnSelBattleCondition.Visible = true;
                btnSelBattleCondition2.Visible = true;
                txtXgName.Visible = true;
                txtXgName2.Visible = true;
                if (cboAccumulate.SelectedValue.ToString() != "10") return;
                lblValue2.Text = @"高于触发";
                lblValue1.Text = @"低于触发";
            }
            else
            {
                btnSelBattleCondition.Visible = false;
                btnSelBattleCondition2.Visible = false;
                txtXgName.Visible = false;
                txtXgName2.Visible = false;
            }
        }  
        private void button1_Click(object sender, EventArgs e)
        {
            ToolsHelper.OpenBattleCondition(txtValue2, txtXgName2, Const.OpenType.Radio);
        } 

        private void btnSelBattleCondition_Click(object sender, EventArgs e)
        {
            ToolsHelper.OpenBattleCondition(txtValue1, txtXgName, Const.OpenType.Radio);
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (dgXiaoGuo.DataSource == null) return;
            foreach (DataRow dr in ((DataTable) dgXiaoGuo.DataSource).Rows)
            {
                dr[0] = txtID.Text;
            }
        }

        private void dgXiaoGuo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtXgName.Text = "";
            txtXgName2.Text = "";
            if (dgXiaoGuo.CurrentRow == null)
                return;
            _dgvDrc = dgXiaoGuo.CurrentRow; 
            cboEffectType1.SelectedIndexChanged -= cboEffectType1_SelectedIndexChanged; 
            DataHelper.SetCtrlByDataRow(groupBox1, _dgvDrc); 
            cboEffectType1.SelectedIndexChanged += cboEffectType1_SelectedIndexChanged;
            btnAddXg.Visible = false;
            btnSaveXg.Visible = true;

            if (txtValue1.Text != Const.Zero && !string.IsNullOrEmpty(txtValue1.Text))
            {
                txtXgName.Text = ExplainHelper.GetConditionName(txtValue1.Text);
            }

            if (txtValue2.Text != Const.Zero && !string.IsNullOrEmpty(txtValue2.Text))
            {
                txtXgName2.Text = ExplainHelper.GetConditionName(txtValue2.Text);
            }
            lblMsg.Text = $"当前修改第{dgXiaoGuo.CurrentRow.Index + 1}行数据";
        }

        private void dgXiaoGuo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnAddXg.Visible = true;
                btnSaveXg.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cboAccumulate.SelectedIndex = 0;
            cboEffectType1.SelectedIndex = 0;
            txtValue1.Text = "";
            txtValue2.Text = "";
            txtValueLimit.Text = "";
            btnAddXg.Visible = true;
            btnSaveXg.Visible = false;
            lblMsg.ForeColor = Color.Blue;
            lblMsg.Text = "";
        } 

        private void 招式效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetCbo fs = new FrmSetCbo(cboEffectType1, PathHelper.EffecttypePath);
            fs.ShowDialog();
        }

        private void 生效方式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetCbo fs = new FrmSetCbo(cboAccumulate, PathHelper.AccumulatePath);
            fs.ShowDialog();
        }
    }
}
