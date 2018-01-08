using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Helper;
using xkfy_mod.Entity;
using xkfy_mod.Utils;
using xkfy_mod.XML;

namespace xkfy_mod.SetUp
{
    public partial class FrmSetCbo : Form
    {
        private readonly string _path;
        List<DicConfig> _menuList;
        private readonly ComboBox _cb;
        public FrmSetCbo(ComboBox cb, string path)
        {
            _path = path;
            _cb = cb;
            InitializeComponent();
        }

        private void frmSetUp_Load(object sender, EventArgs e)
        {
            _menuList = XmlHelper.XmlDeserializeFromFile<List<DicConfig>>(_path, Encoding.UTF8);
            BindingList<DicConfig> bl = new BindingList<DicConfig>(_menuList);

            dg1.DataSource = bl;
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            FileUtils.SaveConfig(_menuList, _path);
            DataHelper.BinderComboBox(_cb, _menuList); 
            MessageBox.Show(@"修改成功！");
            Close();
        }
    }
}
