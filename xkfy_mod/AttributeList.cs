using System;
using System.Collections.Generic;
using System.Windows.Forms;
using xkfy_mod.Entity;
using xkfy_mod.Helper;
using xkfy_mod.Utils;

namespace xkfy_mod
{
    public partial class AttributeList : Form
    {
        private readonly string _filePath;
        private readonly TextBox _txtId;
        private readonly TextBox _txtName;
        public AttributeList(string filePath, TextBox txtId,TextBox txtName)
        {
            _txtId = txtId;
            _txtName = txtName;
            _filePath = filePath;
            InitializeComponent();
        }

        private void AttributeList_Load(object sender, EventArgs e)
        { 
            IList<LeftMenu> list = FileUtils.ReadConfig<LeftMenu>(_filePath);
            foreach (LeftMenu ls in list)
            {
                TreeNode node = new TreeNode
                {
                    Text = ls.MenuText,
                    Tag = ls.MenuTag
                };
                foreach (KeyValuePair<string, string> dic in DataHelper.DropDownListDict[ls.MenuName])
                {
                    TreeNode chldNode = new TreeNode
                    {
                        Text = dic.Value,
                        Tag = dic.Key
                    };
                    node.Nodes.Add(chldNode);
                }
                treeView1.Nodes.Add(node);
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = e.Node;
            if (e.Node.Parent != null)
            {
                _txtId.Text = currentNode.Parent.Tag + @"," + currentNode.Tag;
                _txtName.Text = currentNode.Text;
                Close();
            }
        }
    }
}
