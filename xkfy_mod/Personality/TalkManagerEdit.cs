using System;
using System.Data;
using System.Drawing; 
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Helper;

namespace xkfy_mod.Personality
{
    public partial class TalkManagerEdit : Form
    {
        private readonly DataGridViewRow _dr;
        private DataRow _datarow;
        private DataRow[] _talkGroup;
        private readonly string _type;
        private int _rowOrder; 
        public TalkManagerEdit(DataGridViewRow dr, string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        public TalkManagerEdit(DataRow[] talkGroup, string type)
        {
            _talkGroup = talkGroup;
            _type = type;
            InitializeComponent();
        }

        public TalkManagerEdit(string type)
        {
            _type = type;
            InitializeComponent();
        }

        private void TalkManager_Edit_Load(object sender, EventArgs e)
        {
             
            DataView dv = DataHelper.DdlDataSet.Tables["TalkManager.iGtype"].DefaultView;
            dv.RowFilter = "Type='iGtype'";
            txtiGType1.DataSource = dv;

            DataView dv1 = dv.Table.Copy().DefaultView;
            dv1.RowFilter = "Type='iGtype'";
            txtiGType2.DataSource = dv1;

            DataView dv2 = dv.Table.Copy().DefaultView;
            dv2.RowFilter = "Type='iGtype'";
            txtiGType3.DataSource = dv2; 

            switch (_type)
            {
                case "Modify":
                    //修改
                    DataHelper.SetCtrlByDataRow(this, _dr);
                    btnUpdate.Visible = true;
                    break;
                case "Add":
                    //新增
                    btnAdd.Visible = true;
                    break;
                case "CopyAdd":
                    //复制新增
                    DataHelper.SetCtrlByDataRow(this, _dr);
                    btnAdd.Visible = true;
                    break;
                case "debug":
                    //BindData(datarow);
                    btnUpdate.Visible = true;
                    break;
                default:
                    lblTotal.Text = $"本组对话共{_talkGroup.Length}条";
                    plTalkGroup.Visible = true;
                    BinderTalkGroupy();
                    break;
            }
        } 

        public void BindData(DataRow row)
        {
            _datarow = row;
            DataHelper.SetCtrlByDataRow(this, row);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        { 
            if (_type == "debug")
            { 
                if (string.IsNullOrEmpty(txtIndexSn.Text.Trim()))
                    txtIndexSn.Text = Const.Zero;
                DataHelper.SetDataRowByCtrl(this, _datarow);
                if (string.IsNullOrEmpty(_datarow["rowState"].ToString()))
                    _datarow["rowState"] = "0";
            }
            else
            {
                DataHelper.UpdateData(this, _dr);
            } 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            if (!string.IsNullOrEmpty(txtIndexSn.Text))
            {
                try
                {
                    rowIndex = Convert.ToInt16(txtIndexSn.Text);
                }
                catch
                {
                    MessageBox.Show(@"必须填入数字");
                    return;
                }
            }  
            DataRow dr = DataHelper.XkfyData.Tables["TalkManager"].NewRow();
            DataHelper.SetDataRowByCtrl(this, dr);
            
            if (string.IsNullOrEmpty(dr["rowState"].ToString()))
                dr["rowState"] = "1";
            DataHelper.XkfyData.Tables["TalkManager"].Rows.InsertAt(dr, rowIndex);
            
            Close();
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_dr.ToString());
        }
         
        private void cboiGType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb = ((ComboBox)sender);
                string name = ((ComboBox)sender).Tag.ToString();
                string iGtype = ((ComboBox)sender).SelectedValue.ToString();

                DataRow[] driGtype = ((DataView) cb.DataSource).Table.Select("Type='iGtype' and Value='" + iGtype + "'");
                if (driGtype.Length != 1)
                {
                    return;
                }

                Control[] text = groupBox1.Controls.Find(name, false);
                if (text.Length > 0)
                {
                    text[0].Text = cb.SelectedValue.ToString();
                }

                Control[] cbo = groupBox1.Controls.Find("cbo" + name, false);
                DataView dv = ((DataView) cb.DataSource).Table.Copy().DefaultView;
                if (driGtype[0]["ChildType"].ToString() != "isNull")
                {
                    
                    dv.RowFilter = "Type='" + driGtype[0]["ChildType"] + "'";
                    if (cbo.Length > 0)
                    {
                        ((ComboBox)cbo[0]).DataSource = dv;
                    }
                }
                else
                {
                    if (cbo.Length > 0)
                    {
                        DataView dvNull = ((DataView) cb.DataSource).Table.Clone().DefaultView;
                        ((ComboBox)cbo[0]).DataSource = dvNull;
                    }
                }
                Control[] btn = groupBox1.Controls.Find("btn" + name, false);
                btn[0].Visible = false;
                switch (iGtype)
                {
                    case "6":
                    case "7":
                    case "13":
                    case "16":
                    case "17":
                    case "19":
                        if (cbo.Length > 0)
                        {
                            btn[0].Visible = true;
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenForm(string iGtype,TextBox txtSelValue2)
        {
            string[] row = null;
            string tbName = string.Empty;

            switch (iGtype)
            {
                case "6":
                    row = new[] { "iID", "sNpcName", "sIntroduction" };
                    tbName = "NpcData";
                    break;
                case "7":
                    row = new[] { "iItemID$0", "sItemName$1", "sTip$5" };
                    tbName = "ItemData";
                    break;
                case "16":
                    row = new[] { "iRoutineID", "sRoutineName", "sModelName" };
                    tbName = "RoutineData";
                    break;
                case "17":
                    row = new[] { "ID", "Name", "Desc" };
                    tbName = "BattleNeigong";
                    break;
                case "13":
                    row = new[] { "iID", "sTitle", "sTip" };
                    tbName = "TitleData";
                    break;
                case "19":
                    row = new[] { "iID", "sAbilityID", "sBookTip" };
                    tbName = "AbilityBookData";
                    break;
            }
            if (!string.IsNullOrEmpty(tbName))
            {
                ToolsHelper.OpenChooseForm(tbName, row, txtSelValue2, null, "1");
            }
        }

        private void cboiGType1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb = ((ComboBox)sender);
                string name = ((ComboBox)sender).Tag.ToString(); 

                Control[] text = groupBox1.Controls.Find(name, false);
                if (text.Length > 0)
                {
                    text[0].Text = cb.SelectedValue.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btniGType2_Click(object sender, EventArgs e)
        {
            OpenForm(txtiGType2.SelectedValue.ToString(), sGArg2);
        }

        private void btniGType1_Click(object sender, EventArgs e)
        {
            OpenForm(txtiGType1.SelectedValue.ToString(), sGArg1);
        }

        private void btniGType3_Click(object sender, EventArgs e)
        {
            OpenForm(txtiGType3.SelectedValue.ToString(), sGArg3);
        }
 
        Label _oldlab;
        private void label34_Click(object sender, EventArgs e)
        {
            Label cb = ((Label)sender);
            cb.ForeColor = Color.Red;
            txtiMasgPlace.Text = cb.Tag.ToString(); 
        } 

        private void pictureBox1_Click(object sender, EventArgs e)
        { 
            string name = ((PictureBox)sender).Tag.ToString();
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath + "\\Images",
                Multiselect = true,
                Title = @"请选择文件",
                Filter = @"图片文件|*.gif;*.jpg;*.png;*.bmp;*.jpeg"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = fileDialog.SafeFileName;
                if (file != null)
                {
                    file = file.Substring(0, file.IndexOf(".", StringComparison.Ordinal));
                    Control[] text = Controls.Find(name, false);
                    if (text.Length > 0)
                    {
                        text[0].Text = file;
                    }
                    Image img = Image.FromFile(fileDialog.FileName);
                    ((PictureBox) sender).BackgroundImage = img;
                }
            }
        }

        private void BinderTalkGroupy()
        {
            btnNext.Enabled = true;
            btnPre.Enabled = true;
            if(_rowOrder == _talkGroup.Length-1)
            {
                btnNext.Enabled = false;
            }
            else if (_rowOrder == _talkGroup.Length)
            {
                _rowOrder = _talkGroup.Length;
                btnNext.Enabled = false;
                return;
            }
            if (_rowOrder == 0)
            {
                btnPre.Enabled = false;
            }
            else if (_rowOrder < 0)
            {
                _rowOrder = 0;
                btnPre.Enabled = false;
                return;
            }

            lblOrder.Text = $"当前第{_rowOrder + 1}条";  

            for (int i = 1; i < 9; i++)
            {
                Control[] picBoxsNpcQName = Controls.Find("picBoxsNpcQName" + i, false);
                if (picBoxsNpcQName.Length > 0)
                {
                    picBoxsNpcQName[0].BackgroundImage = null;
                }
                Control[] sNpcQName = Controls.Find("sNpcQName" + i, false);
                if (sNpcQName.Length > 0)
                {
                    sNpcQName[0].Text = "";
                }
            }
            DataHelper.SetCtrlByDataRow(this, _talkGroup[_rowOrder]);
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            _rowOrder--;
            BinderTalkGroupy();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {  
            _rowOrder++;
            BinderTalkGroupy();
        }

        private void btnSaveGroup_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(_talkGroup[_rowOrder]["rowState"].ToString()))
                _talkGroup[_rowOrder]["rowState"] = "0";
            DataHelper.SetDataRowByCtrl(this, _talkGroup[_rowOrder]);
        }

        private void btnInsTalk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtiQGroupID.Text))
            {
                return;
            }
            
            DataRow newRow = DataHelper.XkfyData.Tables[Const.TalkManager].NewRow();
            if (string.IsNullOrEmpty(_talkGroup[_rowOrder]["rowState"].ToString()))
                _talkGroup[_rowOrder]["rowState"] = "1";
            DataHelper.SetDataRowByCtrl(this, newRow);
            DataHelper.XkfyData.Tables[Const.TalkManager].Rows.InsertAt(newRow, DataHelper.XkfyData.Tables[Const.TalkManager].Rows.IndexOf(_talkGroup[_rowOrder]));
            DataHelper.XkfyData.Tables[Const.TalkManager].AcceptChanges();
            _talkGroup = DataHelper.XkfyData.Tables[Const.TalkManager].Select($"iQGroupID='{txtiQGroupID.Text}'","indexSn Asc");
            lblTotal.Text = $"本组对话共{_talkGroup.Length}条";
            BinderTalkGroupy();
            _rowOrder++;
            label36.Text = @"当前在修改复制的行,请注意修改序号";
        }

        private void sNpcQName1_TextChanged(object sender, EventArgs e)
        {
            string tag = ((TextBox) sender).Tag.ToString();
            string text = ((TextBox)sender).Text;
            if (!string.IsNullOrEmpty(text) && DataHelper.DictImages.ContainsKey(text))
            { 
                Image img = Image.FromFile(DataHelper.DictImages[text]);
                Control[] picBoxControls = Controls.Find("picBox"+tag, false);
                if (picBoxControls.Length > 0)
                {
                    picBoxControls[0].BackgroundImage = img;
                } 
            } 
        }

        private void txtiMasgPlace_TextChanged(object sender, EventArgs e)
        {  
            Control[] lbliMasgPlace = Controls.Find("lbliMasgPlace" + txtiMasgPlace.Text, false);
            if (lbliMasgPlace.Length > 0)
            {
                if (_oldlab != null)
                    _oldlab.ForeColor = Color.Black;
                lbliMasgPlace[0].ForeColor = Color.Red;
                _oldlab = (Label)lbliMasgPlace[0];


                int x = 0;
                Image imgLeft = Image.FromFile(PathHelper.TalkBoxPathLeft);
                Image imgRight = Image.FromFile(PathHelper.TalkBoxPathRight);
                int iMasgPlace = int.Parse(txtiMasgPlace.Text)+1;
                switch (iMasgPlace)
                {
                    case 1:
                        panelTalk.BackgroundImage = imgLeft;
                        x = 20;
                        break;
                    case 2:
                        panelTalk.BackgroundImage = imgLeft;
                        x = 140;
                        break;
                    case 3:
                        panelTalk.BackgroundImage = imgLeft;
                        x = 260;
                        break;
                    case 4:
                        panelTalk.BackgroundImage = imgLeft;
                        x = 380;
                        break;
                    case 5:
                        panelTalk.BackgroundImage = imgRight;
                        x = 367;
                        break;
                    case 6:
                        panelTalk.BackgroundImage = imgRight;
                        x = 485;
                        break;
                    case 7:
                        panelTalk.BackgroundImage = imgRight;
                        x = 605;
                        break;
                    case 8:
                        panelTalk.BackgroundImage = imgRight;
                        x = 733;
                        break;
                }
                panelTalk.Location = new Point(x, panelTalk.Location.Y);
            } 
        }
    }
}
