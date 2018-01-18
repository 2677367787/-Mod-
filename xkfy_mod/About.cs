using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Helper;

namespace xkfy_mod
{
    public partial class About : DockContent
    {
        public About()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(((Label)sender).Tag.ToString());
        }

        private void About_Load(object sender, EventArgs e)
        { 

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://weibo.com/6387835066/profile?topnav=1&wvr=6"); 
        }
    }
}
