using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;
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
            
            foreach (TableExplain te in list)
            {
                Annotation an = new Annotation();
                an.ColumnName = te.Column;
                an.ColumnValue = ""; 
                if (te.IsDropDownList == 1)
                {
                    IList<DicConfig> dropList = FileUtils.ReadConfig<DicConfig>(te.DataSourcePath);
                    an.ExplainList = dropList.Select(dc => new RowFormat()
                    {
                        Key = dc.Key, Value = dc.Value, Format = ""
                    }).ToList(); 
                }
                _dataList.Add(an);
            }


            if (_fd.TableName == Const.BattleNeigong)
            {
                list = FileHelper.GetColumnData("DetailTable");
            }
            foreach (TableExplain te in list)
            {
                Annotation an = new Annotation {ColumnName = te.Column};

                if (te.IsDropDownList == 1)
                {
                    IList<DicConfig> dropList = FileUtils.ReadConfig<DicConfig>(te.DataSourcePath);
                    an.ExplainList = dropList.Select(dc => new RowFormat()
                    {
                        Key = dc.Key,
                        Value = dc.Value,
                        Format = ""
                    }).ToList();
                }
                _dataList.Add(an);
            }

            FileUtils.SaveConfig(_dataList, PathHelper.GetExplicatePath(_fd.TableName));
            _dataList = FileUtils.ReadConfig<Annotation>(PathHelper.GetExplicatePath(_fd.TableName));
            BindingList<Annotation> bl = new BindingList<Annotation>(_dataList);

            dgLeftMenu.DataSource = bl;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IList<Annotation> modelList = new List<Annotation>((BindingList<Annotation>)this.dgLeftMenu.DataSource);
            FileUtils.SaveConfig(modelList, PathHelper.GetExplicatePath(_fd.TableName));
            MessageBox.Show("保存成功！");
        }

        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgLeftMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dv = dgLeftMenu.CurrentRow;
            if (dv != null)
            {
                IList<RowFormat> dg1Data = new List<RowFormat>();
                if (dv.Cells[0].Value != null)
                {
                    string columnName = dv.Cells[0].Value.ToString();
                    dg1Data = _dataList.Where(d => d.ColumnName == columnName).ToList()[0].ExplainList;
                }  
                BindingList<RowFormat> bl = new BindingList<RowFormat>(dg1Data);
                dg1.DataSource = bl;
            } 
        }
    }
}
