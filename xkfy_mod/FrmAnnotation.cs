using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Forms;
using xkfy_mod.Data;
using xkfy_mod.Entity;
using xkfy_mod.Helper;
using xkfy_mod.Utils;

namespace xkfy_mod
{
    public partial class FrmAnnotation : Form
    {
        private readonly FormData _fd;
        public FrmAnnotation(FormData fd)
        {
            this._fd = fd;
            InitializeComponent();
        }

        private IList<Annotation> _dataList;

        private void FrmAnnotation_Load(object sender, EventArgs e)
        {
            _dataList = new List<Annotation>();
            IDictionary<string, TableExplain> dict = null;
            if (_fd.DictTableExplain != null)
            {
                dict = _fd.DictTableExplain;
            }

            IList<TableExplain> list = FileHelper.GetColumnData(_fd.TableName);

            int index = 1;
            foreach (TableExplain te in list)
            {
                string id = _fd.TableName + index;
                Annotation an = new Annotation();
                an.Id = id;
                an.ParentId = "Base";
                an.Code = te.Column;
                an.Column = te.Column;
                an.Text = te.Text;
                an.Remark = " ";
                _dataList.Add(an);
                if (te.IsDropDownList == 1)
                {
                    Dictionary<string, string> dropList = DataHelper.DropDownListDict[te.DataKey];
                    foreach (var dictDrop in dropList)
                    {
                        index++;
                        Annotation anLevel2 = new Annotation();
                        anLevel2.Id = _fd.TableName + index;
                        anLevel2.ParentId = id;
                        anLevel2.Column = te.Column;
                        anLevel2.Code = dictDrop.Key;
                        anLevel2.Text = dictDrop.Value;
                        anLevel2.Remark = " ";
                        _dataList.Add(anLevel2);
                    }
                }
                index++;
            }

            list = FileHelper.GetColumnData(_fd.TableName + "_D");
            foreach (TableExplain te in list)
            {
                string id = _fd.TableName + index;
                Annotation an = new Annotation();
                an.Id = id;
                an.ParentId = "Base";
                an.Column = te.Column;
                an.Code = te.Column;
                an.Text = te.Text;
                an.Remark = " ";
                _dataList.Add(an);
                if (te.IsDropDownList == 1)
                {
                    if (!string.IsNullOrEmpty(te.DataKey) && DataHelper.DropDownListDict.ContainsKey(te.DataKey))
                    {
                        Dictionary<string, string> dropList = DataHelper.DropDownListDict[te.DataKey];
                        foreach (var dictDrop in dropList)
                        {
                            index++;
                            Annotation anLevel2 = new Annotation();
                            anLevel2.Id = _fd.TableName + index;
                            anLevel2.ParentId = id;
                            anLevel2.Column = te.Column;
                            anLevel2.Code = dictDrop.Key;
                            anLevel2.Text = dictDrop.Value;
                            anLevel2.Remark = " ";
                            _dataList.Add(anLevel2);
                        }
                    }
                }
                index++;
            }


            FileUtils.SaveConfig(_dataList, PathHelper.GetExplicatePath(_fd.TableName));
            _dataList = FileUtils.ReadConfig<Annotation>(PathHelper.GetExplicatePath(_fd.TableName));
            foreach (var dataBase in _dataList.Where(dl => dl.ParentId == "Base").ToList())
            {
                TreeNode tnParent = new TreeNode()
                {
                    Name = dataBase.Id,
                    Text = dataBase.Column,
                    Tag = dataBase.Id
                };
                foreach (var data in _dataList.Where(dl => dl.ParentId == dataBase.Id).ToList())
                {
                    TreeNode node = new TreeNode()
                    {
                        Name = data.Id,
                        Text = data.Column,
                        Tag = data.Id
                    };
                    tnParent.Nodes.Add(node);
                }
                tvModel.Nodes.Add(tnParent);
            } 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
//            IList<Annotation> modelList = new List<Annotation>((BindingList<Annotation>)this.dgLeftMenu.DataSource);
//            FileUtils.SaveConfig(modelList, PathHelper.GetExplicatePath(_fd.TableName));
//            MessageBox.Show("保存成功！");
        }

        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgLeftMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
//            DataGridViewRow dv = dgLeftMenu.CurrentRow;
//            if (dv != null)
//            {
//                IList<RowFormat> dg1Data = new List<RowFormat>();
//                if (dv.Cells[0].Value != null)
//                {
//                    string columnName = dv.Cells[0].Value.ToString();
//                    dg1Data = _dataList.Where(d => d.ColumnName == columnName).ToList()[0].ExplainList;
//                }  
//                BindingList<RowFormat> bl = new BindingList<RowFormat>(dg1Data);
//                dg1.DataSource = bl;
//            } 
        }

        private void tvModel_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = e.Node; 
            var list = _dataList.Where(dl => dl.Id == currentNode.Name).ToList();
            txtId.Text = list[0].Id;
            txtParentId.Text = list[0].ParentId;
            txtColumn.Text = list[0].Column;
            txtCode.Text = list[0].Code;
            txtText.Text = list[0].Text;
            txtExplain.Text = list[0].Remark; 
        }
    }
}
