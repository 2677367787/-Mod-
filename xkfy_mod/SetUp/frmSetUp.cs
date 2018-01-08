using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Entity;
using xkfy_mod.XML;

namespace xkfy_mod.SetUp
{
    public partial class frmSetUp : Form
    {
        string path = string.Empty;
        List<DicConfig> menuList;
        public frmSetUp(string path)
        {
            this.path = path;
            InitializeComponent();
        }

        private void frmSetUp_Load(object sender, EventArgs e)
        {
            menuList = XmlHelper.XmlDeserializeFromFile<List<DicConfig>>(path, Encoding.UTF8);
            BindingList<DicConfig> bl = new BindingList<DicConfig>(menuList);

            dg1.DataSource = bl;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlHelper.XmlSerializeToFile(menuList, path, Encoding.UTF8);
            MessageBox.Show("修改成功！");
        }
    }
}
