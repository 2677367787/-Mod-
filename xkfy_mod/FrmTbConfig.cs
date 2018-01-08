using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using xkfy_mod.Entity;
using xkfy_mod.Helper;
using xkfy_mod.Utils;
using xkfy_mod.XML;

namespace xkfy_mod
{
    public partial class FrmTbConfig : Form
    {
        public FrmTbConfig()
        {
            InitializeComponent();
        }
         
        List<MyConfig> _menuList;
        private void FrmTbConfig_Load(object sender, EventArgs e)
        { 
            _menuList = XmlHelper.XmlDeserializeFromFile<List<MyConfig>>(PathHelper.TableConfigPath, Encoding.UTF8);
            BindingList<MyConfig> bl = new BindingList<MyConfig>(_menuList);

            dg1.DataSource = bl;
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            FileHelper.SaveTableConfig(_menuList);
            MessageBox.Show(@"修改成功！");
        }
    }
}
