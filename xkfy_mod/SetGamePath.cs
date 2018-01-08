using System;
using System.Collections.Generic;
using System.Windows.Forms;
using xkfy_mod.Entity;
using xkfy_mod.Helper;

namespace xkfy_mod
{
    public partial class SetGamePath : Form
    {
        public SetGamePath()
        {
            InitializeComponent();
        }

        private void BtnSelFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog {SelectedPath = PathHelper.ModifyFolderPath};
            if (folder.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = folder.SelectedPath;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                return;
            }
            _list[0].GameInstallPath = txtFilePath.Text;
            PathHelper.GamePath = txtFilePath.Text;
            FileHelper.SaveAppConfig(_list);
            Close();
        }

        private IList<AppConfig> _list;
        private void SetGamePath_Load(object sender, EventArgs e)
        {
            _list = FileHelper.ReadAppConfig();
            if (_list.Count > 0)
            {
                txtFilePath.Text = _list[0].GameInstallPath;
            }
        }
    }
}
