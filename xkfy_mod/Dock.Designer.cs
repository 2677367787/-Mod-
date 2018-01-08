namespace xkfy_mod
{
    partial class Dock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // MenuTree
            // 
            this.MenuTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuTree.Location = new System.Drawing.Point(0, 0);
            this.MenuTree.Name = "MenuTree";
            this.MenuTree.Size = new System.Drawing.Size(257, 569);
            this.MenuTree.TabIndex = 0;
            this.MenuTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Menu_NodeMouseDoubleClick);
            // 
            // Dock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 569);
            this.Controls.Add(this.MenuTree);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Dock";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
            this.Text = "Dock";
            this.Load += new System.EventHandler(this.Dock_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView MenuTree;
    }
}