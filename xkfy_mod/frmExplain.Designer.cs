namespace xkfy_mod
{
    partial class FrmExplain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExplain));
            this.tvMenu = new System.Windows.Forms.TreeView();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.cname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ctext = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.explain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.height = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsDropDownList = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DataKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataSourcePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.SuspendLayout();
            // 
            // tvMenu
            // 
            this.tvMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvMenu.Location = new System.Drawing.Point(3, 12);
            this.tvMenu.Name = "tvMenu";
            this.tvMenu.Size = new System.Drawing.Size(248, 500);
            this.tvMenu.TabIndex = 0;
            this.tvMenu.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMenu_NodeMouseDoubleClick);
            // 
            // dg1
            // 
            this.dg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cname,
            this.IsSelect,
            this.ctext,
            this.explain,
            this.width,
            this.height,
            this.IsDropDownList,
            this.DataKey,
            this.DataSourcePath});
            this.dg1.Location = new System.Drawing.Point(257, 12);
            this.dg1.Name = "dg1";
            this.dg1.RowTemplate.Height = 23;
            this.dg1.Size = new System.Drawing.Size(920, 447);
            this.dg1.TabIndex = 1;
            // 
            // cname
            // 
            this.cname.DataPropertyName = "Column";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cname.DefaultCellStyle = dataGridViewCellStyle1;
            this.cname.HeaderText = "列名";
            this.cname.Name = "cname";
            this.cname.Width = 70;
            // 
            // IsSelect
            // 
            this.IsSelect.DataPropertyName = "IsSelColumn";
            this.IsSelect.FalseValue = "0";
            this.IsSelect.HeaderText = "可查询";
            this.IsSelect.IndeterminateValue = "0";
            this.IsSelect.Name = "IsSelect";
            this.IsSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsSelect.ToolTipText = "选择是可以作为条件显示在窗体";
            this.IsSelect.TrueValue = "1";
            this.IsSelect.Width = 60;
            // 
            // ctext
            // 
            this.ctext.DataPropertyName = "Text";
            this.ctext.HeaderText = "中文显示";
            this.ctext.Name = "ctext";
            // 
            // explain
            // 
            this.explain.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.explain.DataPropertyName = "Explain";
            this.explain.HeaderText = "列详细解释";
            this.explain.Name = "explain";
            // 
            // width
            // 
            this.width.DataPropertyName = "width";
            this.width.HeaderText = "宽度";
            this.width.Name = "width";
            this.width.Visible = false;
            this.width.Width = 60;
            // 
            // height
            // 
            this.height.DataPropertyName = "height";
            this.height.HeaderText = "高度";
            this.height.Name = "height";
            this.height.Visible = false;
            this.height.Width = 60;
            // 
            // IsDropDownList
            // 
            this.IsDropDownList.DataPropertyName = "IsDropDownList";
            this.IsDropDownList.FalseValue = "0";
            this.IsDropDownList.HeaderText = "下拉选择";
            this.IsDropDownList.IndeterminateValue = "0";
            this.IsDropDownList.Name = "IsDropDownList";
            this.IsDropDownList.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsDropDownList.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsDropDownList.TrueValue = "1";
            this.IsDropDownList.Width = 80;
            // 
            // DataKey
            // 
            this.DataKey.DataPropertyName = "DataKey";
            this.DataKey.HeaderText = "唯一键";
            this.DataKey.Name = "DataKey";
            this.DataKey.Width = 65;
            // 
            // DataSourcePath
            // 
            this.DataSourcePath.DataPropertyName = "DataSourcePath";
            this.DataSourcePath.HeaderText = "数据路径";
            this.DataSourcePath.Name = "DataSourcePath";
            this.DataSourcePath.Width = 120;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(258, 474);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(916, 1);
            this.label1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1099, 489);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(1009, 489);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmExplain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 524);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.tvMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmExplain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑注释";
            this.Load += new System.EventHandler(this.formExplain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvMenu;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn cname;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctext;
        private System.Windows.Forms.DataGridViewTextBoxColumn explain;
        private System.Windows.Forms.DataGridViewTextBoxColumn width;
        private System.Windows.Forms.DataGridViewTextBoxColumn height;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsDropDownList;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataSourcePath;
    }
}