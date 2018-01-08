using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using xkfy_mod.Helper;
using xkfy_mod.Entity;
using xkfy_mod.Utils;

namespace xkfy_mod
{
    public partial class Dock : DockContent
    {  
        public Dock()
        {
            InitializeComponent();
            MenuTree.HideSelection = false;
            //自已绘制
            MenuTree.DrawMode = TreeViewDrawMode.OwnerDrawText;
            MenuTree.DrawNode += treeView1_DrawNode;
        }

        private void Dock_Load(object sender, EventArgs e)
        {
            try
            {
                //如果文件目录不存在提示出错直接返回
                if (!Directory.Exists(PathHelper.ModifyFolderPath))
                {
                    MessageBox.Show($"工具运行目录,不存在【{PathHelper.ModifyFolderPath}】文件夹！");
                    return;
                }

                //读取Mod文件,保存文件名称和路径
                FileHelper.GetAllFolderFile(PathHelper.ModifyFolderPath, DataHelper.DictModFiles);

                //加载左侧菜单数据
                IList<LeftMenu> menuList = FileUtils.ReadConfig<LeftMenu>(PathHelper.MenuConfigPath);
                
                foreach (LeftMenu m in menuList)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = m.MenuText,
                        Tag = m.MenuTag,
                        Name = m.MenuName
                    };
                    MenuTree.Nodes.Add(node);
                }
                
                #region 循环窗体配置,加载左侧子菜单
                //加载所有表配置
                IList<MyConfig> list = FileHelper.ReadTableConfig();
                //循环所有表配置
                foreach (MyConfig item in list)
                { 
                    if (!DataHelper.DictModFiles.ContainsKey(item.TxtName))
                        continue;

                    TreeNode chldNode = new TreeNode
                    {
                        Text = $"{item.Notes}({item.TxtName})",
                        Tag = item.MainDtName
                    };
                    MenuTree.Nodes[item.Classify].Nodes.Add(chldNode);

                    //保存窗体配置文件 
                    DataHelper.FormConfig.Add(item.MainDtName, item);
                }

                #endregion

                if (DataHelper.DictModFiles.ContainsKey("MapID.txt"))
                {
                    FileHelper.LoadTable("MapID");

                    Dictionary<string, string> mapNo = new Dictionary<string, string>();
                    foreach (DataRow dr in DataHelper.XkfyData.Tables["MapID"].Rows)
                    {
                        TreeNode node = new TreeNode
                        {
                            Text = dr[1].ToString(),
                            Tag = "map",
                            Name = dr[0].ToString()
                        };
                        MenuTree.Nodes.Add(node);
                        if (mapNo.ContainsKey(dr[1].ToString()))
                        {
                            MessageBox.Show(dr[1].ToString());
                        }
                        else
                        {
                            mapNo.Add(dr[0].ToString(), dr[1].ToString());
                        }
                    }   

                    foreach (KeyValuePair<string, string> map in DataHelper.DictModFiles)
                    {
                        if (map.Value.ToUpper().IndexOf("MAPICON", StringComparison.Ordinal) == -1)
                        {
                            continue;
                        }
                        string key = map.Key.Substring(9, 5);
                        string tableName = map.Key.Substring(0, map.Key.LastIndexOf('.'));
                        if (!mapNo.ContainsKey(key))
                        {
                            key = map.Key.Substring(11, 5);
                            if (!mapNo.ContainsKey(key))
                            {
                                continue;
                            }
                        }
                        TreeNode chldNode = new TreeNode
                        {
                            Text = map.Key,
                            Tag = tableName
                        };
                        MenuTree.Nodes[key].Nodes.Add(chldNode);
                    }
                }
                else
                {
                    MessageBox.Show(@"mod文件夹中缺少Mapid.txt文件，无法正确加载地图文件！");
                }

                FileHelper.LoadDropDownListData();
                //读取下拉框联动配置信息
                DataHelper.ReadConfig();
                //设置琴棋书画等数值
                DataHelper.SetDicValue();
                //读取图片信息
                FileHelper.LoadImagesData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Menu_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode currentNode = e.Node;
                //如果双击的节点的父节点是空
                if (currentNode.Parent == null)
                {
                    return;
                }

                //循环已经打开过的串口
                foreach (var dockContent in DockPanel.Contents)
                {
                    var frm = (DockContent) dockContent;
                    //如果当前窗口已经打开过了
                    if (frm.Text == currentNode.Text)
                    {
                        //激活窗口
                        frm.Activate();
                        return;
                    }
                }
                //不带后缀的文件名
                string tbName = currentNode.Tag.ToString();
                JudgeFile(tbName, currentNode.Parent.Tag.ToString(), currentNode.Text);
                //反射实例化窗体
                DockContent dc = (DockContent)Assembly.Load("xkfy_mod").CreateInstance("xkfy_mod." + tbName);
                //如果对象为空，则代表使用的是公共窗口
                if (dc == null)
                {
                    Almighty a = new Almighty(tbName)
                    {
                        Text = currentNode.Text,
                        Tag = tbName
                    };
                    a.Show(DockPanel, DockState.Document);
                }
                else
                { 
                    //显示窗口
                    dc.Text = currentNode.Text;
                    dc.Tag = tbName;
                    dc.Show(DockPanel, DockState.Document);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void JudgeFile(string tbName, string parentTag, string currentNodeText)
        {
            //如果节点的父节点的tag等于map代表，是地图文件和NPC动作文件
            if (parentTag == "map")
            { 
                string typeName = currentNodeText.Substring(0, 3);
                string path = DataHelper.DictModFiles[currentNodeText];
                //如果文件名称的前3个字符等于MAP
                var configKey = typeName.ToUpper() == "MAP" ? DataHelper.MapConfigFileName : DataHelper.NpcConfigFileName;
                //读取NpcConduct
                FileHelper.ReadMapAndNpcText(path, tbName,configKey);
            }
            else
            {
                //加载表对应的列的配置信息
                FileHelper.LoadColumnData(tbName);
                FileHelper.LoadTable(tbName); 
            }
        }
         
        #region 单击更改颜色
        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //显示为玫瑰红
                e.Graphics.FillRectangle(Brushes.MistyRose, e.Node.Bounds);

                Font nodeFont = e.Node.NodeFont ?? ((TreeView)sender).Font;
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

        public void SetSelNode(string text)
        {
            foreach (TreeNode n in from TreeNode node in MenuTree.Nodes from TreeNode n in node.Nodes where n.Text == text select n)
            {
                MenuTree.SelectedNode = n;
            }
        }
    }
}
