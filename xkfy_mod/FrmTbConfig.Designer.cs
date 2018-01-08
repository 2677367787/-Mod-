namespace xkfy_mod
{
    partial class FrmTbConfig
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
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.isCache = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TxtName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Classify = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.SuspendLayout();
            // 
            // dg1
            // 
            this.dg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isCache,
            this.TxtName,
            this.Notes,
            this.Classify});
            this.dg1.Location = new System.Drawing.Point(12, 72);
            this.dg1.Name = "dg1";
            this.dg1.RowTemplate.Height = 23;
            this.dg1.Size = new System.Drawing.Size(1045, 502);
            this.dg1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(982, 43);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // isCache
            // 
            this.isCache.DataPropertyName = "IsCache";
            this.isCache.FalseValue = "0";
            this.isCache.HeaderText = "开启缓存";
            this.isCache.IndeterminateValue = "0";
            this.isCache.Name = "isCache";
            this.isCache.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isCache.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isCache.ToolTipText = "1 是 0 否";
            this.isCache.TrueValue = "1";
            this.isCache.Width = 80;
            // 
            // TxtName
            // 
            this.TxtName.DataPropertyName = "TxtName";
            this.TxtName.HeaderText = "文件名";
            this.TxtName.Name = "TxtName";
            // 
            // Notes
            // 
            this.Notes.DataPropertyName = "Notes";
            this.Notes.HeaderText = "左侧菜单名";
            this.Notes.Name = "Notes";
            // 
            // Classify
            // 
            this.Classify.DataPropertyName = "Classify";
            this.Classify.HeaderText = "菜单分类";
            this.Classify.Name = "Classify";
            // 
            // FrmTbConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 586);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dg1);
            this.Name = "FrmTbConfig";
            this.Text = "FrmTbConfig";
            this.Load += new System.EventHandler(this.FrmTbConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCache;
        private System.Windows.Forms.DataGridViewTextBoxColumn TxtName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Classify;
    }
}