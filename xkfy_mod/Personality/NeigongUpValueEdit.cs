using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Entity;
using xkfy_mod.Helper;

namespace xkfy_mod.Personality
{
    public partial class NeigongUpValueEdit : Form
    { 
        private readonly DataGridViewRow _dr;
        private DataRow _newDr;
        private readonly string _type = string.Empty;
        public NeigongUpValueEdit()
        {
            InitializeComponent();
        }
        public NeigongUpValueEdit(DataGridViewRow dr,string type)
        {
            _type = type;
            _dr = dr;
            InitializeComponent();
        }

        private void NeigongUpValue_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                FileHelper.LoadTable("BattleNeigong");
                if (_type != "Modify")
                {
                    _newDr = DataHelper.XkfyData.Tables["NeigongUpValue"].NewRow();
                    if (_type == "CopyAdd")
                    {
                        for (int i = 0; i < _dr.DataGridView.Columns.Count; i++)
                        {
                            string cName = _dr.DataGridView.Columns[i].Name;
                            _newDr[cName] = _dr.Cells[cName].Value;
                        }
                    }
                    _newDr["rowState"] = "1";
                    btnSelNeiGong.Visible = true;
                }
                else
                {
                    txtiid.Text = _dr.Cells["iID"].Value.ToString();
                }
                cboLv.SelectedItem = "Lv1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var info = _type == "Modify" ? _dr.Cells[cboLv.SelectedItem.ToString()].Value.ToString().Split(',') : _newDr[cboLv.SelectedItem.ToString()].ToString().Split(',');
            if (info.Length >= 6)
            {
                textBox1.Text = info[0];
                textBox2.Text = info[1];
                textBox3.Text = info[2];
                textBox4.Text = info[3];
                textBox5.Text = info[4];
                textBox6.Text = info[5];
            }
            else
            {
                if(_type != "Add")
                    lblMsg.Text = $"第[{cboLv.SelectedItem}]级,数据有误,请检查";
            }
        }

        private void btnSelNeiGong_Click(object sender, EventArgs e)
        { 
            ToolsHelper.OpenBattleNeigong(txtiid, txtNgName,Const.OpenType.Radio);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (string.IsNullOrEmpty(txtiid.Text) || txtiid.Text == "")
            {
                MessageBox.Show(@"请选择内功后在点击保存");
                return;
            }
            string rowValue = textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + "," + textBox4.Text + "," + textBox5.Text + "," + textBox6.Text;

            if (ckbEqualAll.Checked)
            {
                foreach (object selvalue in cboLv.Items)
                {
                    if (_type != "Modify")
                    {
                        _newDr[selvalue.ToString()] = rowValue;
                    }
                    else
                    {
                        _dr.Cells[selvalue.ToString()].Value = rowValue;
                    }
                }
            }
            else
            {
                if (_type != "Modify")
                {
                    _newDr[cboLv.SelectedItem.ToString()] = rowValue;
                }
                else
                {
                    _dr.Cells[cboLv.SelectedItem.ToString()].Value = rowValue;
                }
                
            }

            if (_type != "Modify")
            {
                _newDr["iID"] = txtiid.Text;
                DataHelper.XkfyData.Tables["NeigongUpValue"].Rows.InsertAt(_newDr, 0);
            }
            
            lblMsg.ForeColor = Color.Blue;
            lblMsg.Text = $"保存[{txtNgName.Text}]修炼[{cboLv.SelectedItem}]级,修炼属性成功！";
            _dr.DataGridView.CurrentCell = null;
            Close();
        }

        private void ckbEqualAll_CheckedChanged(object sender, EventArgs e)
        {
            cboLv.Enabled = !ckbEqualAll.Checked;
        }

        private void txtiid_TextChanged(object sender, EventArgs e)
        {
            Const.NeiGongFe.Id = txtiid.Text;
            txtNgName.Text = ExplainHelper.GetNeiGongName();
        }
    }
}
