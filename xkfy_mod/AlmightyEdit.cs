using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Entity;
using xkfy_mod.Helper;

namespace xkfy_mod
{
    public partial class AlmightyEdit : Form
    { 
        private readonly DataGridViewRow _dr;
        private readonly string _tbName;
        private readonly string _type;
        private readonly IDictionary<string, TableExplain> _dictTableExplain;
        private readonly string _configKeyName;
        //public AlmightyEdit(DataGridViewRow dr, string tbName,string type, IDictionary<string, TableExplain> dictTableExplain)
        public AlmightyEdit(FormData fd)
        {
            _dr = fd.Dr;
            _tbName = fd.TableName;
            _type = fd.Type;
            _configKeyName = fd.ConfigKeyName;
            _dictTableExplain = fd.DictTableExplain;
            InitializeComponent();
        }
        public static int GetTextBoxLength(string textboxTextStr)
        {
            int nLength = 0;
            for (int i = 0; i < textboxTextStr.Length; i++)
            {
                if (textboxTextStr[i] >= 0x3000 && textboxTextStr[i] <= 0x9FFF)
                    nLength++;
            }
            return nLength;
        }

        private void Almighty_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                annotationCtrl1.ConfigKeyName = _configKeyName;
                IList<TableExplain> list = FileHelper.GetColumnData(_configKeyName); 
                ControlHelper.BuildControl(this, _dr, list, toolTip);

                if (_type == "Modify")
                {
                    btnUpdate.Visible = true;
                }
                else
                {
                    btnAdd.Visible = true;
                }

                foreach (TextBox c in (from Control c in Controls where c.Tag != null select c).OfType<TextBox>())
                {
                    c.Enter += txtExplain_TextChanged;
                }

                if (DataHelper.FormConfig.ContainsKey(_tbName))
                {
                    if (DataHelper.FormConfig[_tbName].DtType == "1")
                    {
                        panel1.Visible = true;
                        Height = 420;
                        dg1.Visible = true;
                        DataView dv = DataHelper.XkfyData.Tables[_tbName + Const.DetailTbName].DefaultView;
                        dv.RowFilter = $"iTalenID='{_dr.Cells["iTalenID"].Value}'"; 
                        dg1.DataSource = dv;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataHelper.UpdateData(this, _dr);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataHelper.AddData(this, _tbName);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtExplain_TextChanged(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox?.Tag != null)
            {
                if (_dictTableExplain.ContainsKey(textbox.Tag.ToString()))
                {
                    annotationCtrl1.txtExplain.Text = _dictTableExplain[textbox.Tag.ToString()].Explain;
                    annotationCtrl1.txtExplain.Tag = textbox.Tag;

                    annotationCtrl1.txtForShort.Text = _dictTableExplain[textbox.Tag.ToString()].Text;
                    annotationCtrl1.txtForShort.Tag = textbox.Tag;
                }
            }
        }
         
    }
}
