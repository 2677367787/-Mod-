using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using xkfy_mod.Helper;
using xkfy_mod.Entity;
using xkfy_mod.Utils;
using System.Drawing;

namespace xkfy_mod
{
    public partial class FrmExplain : Form
    {
        /// <summary>
        /// 存储表格类型
        /// </summary>
        private Dictionary<string, string> _tbConfig;

        private string _tbName;
        private IList<TableExplain> _toolColumns;

        public FrmExplain()
        {
            InitializeComponent();
            //自已绘制
            tvMenu.DrawMode = TreeViewDrawMode.OwnerDrawText;
            tvMenu.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
        }

        private void formExplain_Load(object sender, EventArgs e)
        {
            //dg1.AllowUserToAddRows = false;
            IList<MyConfig> list = FileHelper.ReadTableConfig();
            IList<LeftMenu> menus = FileUtils.ReadConfig<LeftMenu>(PathHelper.MenuConfigPath);
 
            foreach (LeftMenu m in menus)
            {
                TreeNode node = new TreeNode();
                node.Text = m.MenuText;
                node.Tag = m.MenuTag;
                node.Name = m.MenuName;
                tvMenu.Nodes.Add(node);
            }

            _tbConfig = new Dictionary<string, string>();
            foreach (MyConfig item in list)
            {
                TreeNode chldNode = new TreeNode
                {
                    Text = $"{item.Notes}({item.TxtName}",
                    Tag = item.MainDtName
                };
                tvMenu.Nodes[item.Classify].Nodes.Add(chldNode);
                _tbConfig.Add(item.MainDtName, item.DtType);
            }
//
//            TreeNode mapFile = new TreeNode
//            {
//                Text = @"NPC文件",
//                Tag = "NpcConduct"
//            };
//            tvMenu.Nodes["map"].Nodes.Add(mapFile);
//
//            TreeNode npcFile = new TreeNode
//            {
//                Text = @"Map文件",
//                Tag = "Map"
//            };
//            tvMenu.Nodes["map"].Nodes.Add(npcFile);
        }

        private void tvMenu_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = e.Node;
            if (currentNode.Parent == null)
            {
                return;
            }
            _tbName = e.Node.Tag.ToString();
            _toolColumns = FileHelper.GetColumnData(e.Node.Tag.ToString());

            BindingList<TableExplain> bl = new BindingList<TableExplain>(_toolColumns);

            dg1.DataSource = bl;

        }

        private void button1_Click(object sender, EventArgs e)
        {
             Close();
        } 
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DataHelper.ToolColumnConfig.ContainsKey(_tbName))
            {
                DataHelper.ToolColumnConfig.Remove(_tbName);
                DataHelper.ToolColumnConfig.Add(_tbName,_toolColumns);
            }
            FileHelper.SaveColumnData(_toolColumns, _tbName);
            MessageBox.Show(@"修改成功！");
        }

        #region 单击更改颜色
        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //显示为玫瑰红
                e.Graphics.FillRectangle(Brushes.MistyRose, e.Node.Bounds);

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.Black, Rectangle.Inflate(e.Bounds, 2, 0));
            }
            else
            {
                e.DrawDefault = true;
            }

            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }

        }
        #endregion
    }
}
