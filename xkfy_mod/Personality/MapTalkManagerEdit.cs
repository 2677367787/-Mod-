using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Entity;
using xkfy_mod.Helper;

namespace xkfy_mod.Personality
{
    public partial class MapTalkManagerEdit : Form
    { 

        private readonly DataGridViewRow _dr;
        private DataRow _datarow;
        private DataRow[] _talkGroup;
        private readonly string _type;
        private int _rowOrder;
        public MapTalkManagerEdit(DataGridViewRow dr, string type)
        {
            _dr = dr;
            _type = type;
            InitializeComponent();
        }

        public MapTalkManagerEdit(DataRow[] talkGroup, string type)
        {
            _talkGroup = talkGroup;
            _type = type;
            InitializeComponent();
        }

        private void MapTalkManagerEdit_Load(object sender, EventArgs e)
        { 
            DataHelper.SetCtrlLabelData(this, FileHelper.GetColumnData(Const.MapTalkManager));
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

        private void BinderTalkGroupy()
        {
            btnNext.Enabled = true;
            btnPre.Enabled = true;
            if (_rowOrder == _talkGroup.Length - 1)
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
            if (string.IsNullOrEmpty(_talkGroup[_rowOrder]["rowState"].ToString()))
                _talkGroup[_rowOrder]["rowState"] = "0";
            DataHelper.SetDataRowByCtrl(this, _talkGroup[_rowOrder]);
        }

        private void btnInsTalk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtsGroupID.Text))
            {
                return;
            }

            DataRow newRow = DataHelper.XkfyData.Tables[Const.MapTalkManager].NewRow();
            DataHelper.SetDataRowByCtrl(this, newRow); 
            int index = DataHelper.XkfyData.Tables[Const.MapTalkManager].Rows.IndexOf(_talkGroup[_rowOrder]);
            newRow["rowState"] = "1";
            DataHelper.XkfyData.Tables[Const.MapTalkManager].Rows.InsertAt(newRow, index+1);
            DataHelper.XkfyData.Tables[Const.MapTalkManager].AcceptChanges();
            //DataRow[] talkGroup = DataHelper.XkfyData.Tables[Const.MapTalkManager].Select($"sGroupID='{txtsGroupID.Text}'", "indexSn Asc");

            _talkGroup = DataHelper.XkfyData.Tables[Const.MapTalkManager].Select($"sGroupID='{txtsGroupID.Text}'", "indexSn Asc");
            for (int i = 0; i < _talkGroup.Length; i++)
            {
                _talkGroup[i]["indexSn"] = i;
            }
            lblTotal.Text = $"本组对话共{_talkGroup.Length}条";
            _rowOrder++;
            BinderTalkGroupy();
            txtsManager.Text = @"[请修改]";
            label36.Text = @"当前在修改复制的行,请注意修改序号";
             
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataHelper.UpdateData(this, _dr);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataHelper.AddData(this,Const.MapTalkManager);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            ToolsHelper.OpenRewardData(txtiGiftID, null, Const.OpenType.Radio);
        }
    }
}
