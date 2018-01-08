namespace xkfy_mod.Personality
{
    partial class DevelopQuestData
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
            this.components = new System.ComponentModel.Container();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.rowState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg1RightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInsertCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInsertRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmDelRow = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCopyAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDd = new System.Windows.Forms.ComboBox();
            this.btnDebug = new System.Windows.Forms.Button();
            this.txtsEndAdd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtiArg3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtQztj = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.dg1RightMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg1
            // 
            this.dg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rowState});
            this.dg1.ContextMenuStrip = this.dg1RightMenu;
            this.dg1.Location = new System.Drawing.Point(12, 89);
            this.dg1.Name = "dg1";
            this.dg1.RowTemplate.Height = 23;
            this.dg1.Size = new System.Drawing.Size(912, 398);
            this.dg1.TabIndex = 0;
            this.dg1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellDoubleClick);
            this.dg1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellValueChanged);
            this.dg1.CurrentCellChanged += new System.EventHandler(this.dg1_CurrentCellChanged);
            this.dg1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dg1_RowPostPaint);
            // 
            // rowState
            // 
            this.rowState.DataPropertyName = "rowState";
            this.rowState.HeaderText = "rowState";
            this.rowState.Name = "rowState";
            this.rowState.Visible = false;
            // 
            // dg1RightMenu
            // 
            this.dg1RightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCopyRow,
            this.tsmInsertCopyRow,
            this.tsmInsertRow,
            this.toolStripSeparator1,
            this.tsmDelRow});
            this.dg1RightMenu.Name = "dg1RightMenu";
            this.dg1RightMenu.Size = new System.Drawing.Size(137, 98);
            this.dg1RightMenu.Opening += new System.ComponentModel.CancelEventHandler(this.dg1RightMenu_Opening);
            // 
            // tsmCopyRow
            // 
            this.tsmCopyRow.Name = "tsmCopyRow";
            this.tsmCopyRow.Size = new System.Drawing.Size(136, 22);
            this.tsmCopyRow.Text = "复制行";
            this.tsmCopyRow.Click += new System.EventHandler(this.tsmCopyRow_Click);
            // 
            // tsmInsertCopyRow
            // 
            this.tsmInsertCopyRow.Name = "tsmInsertCopyRow";
            this.tsmInsertCopyRow.Size = new System.Drawing.Size(136, 22);
            this.tsmInsertCopyRow.Text = "插入复制行";
            this.tsmInsertCopyRow.Click += new System.EventHandler(this.tsmInsertCopyRow_Click);
            // 
            // tsmInsertRow
            // 
            this.tsmInsertRow.Name = "tsmInsertRow";
            this.tsmInsertRow.Size = new System.Drawing.Size(136, 22);
            this.tsmInsertRow.Text = "插入空白行";
            this.tsmInsertRow.Click += new System.EventHandler(this.tsmInsertRow_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // tsmDelRow
            // 
            this.tsmDelRow.Name = "tsmDelRow";
            this.tsmDelRow.Size = new System.Drawing.Size(136, 22);
            this.tsmDelRow.Text = "删除行";
            this.tsmDelRow.Click += new System.EventHandler(this.tsmDelRow_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCopyAdd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboDd);
            this.groupBox1.Controls.Add(this.btnDebug);
            this.groupBox1.Controls.Add(this.txtsEndAdd);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtiArg3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(912, 70);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnCopyAdd
            // 
            this.btnCopyAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyAdd.Location = new System.Drawing.Point(831, 43);
            this.btnCopyAdd.Name = "btnCopyAdd";
            this.btnCopyAdd.Size = new System.Drawing.Size(75, 23);
            this.btnCopyAdd.TabIndex = 10;
            this.btnCopyAdd.Text = "复制新增";
            this.btnCopyAdd.UseVisualStyleBackColor = true;
            this.btnCopyAdd.Click += new System.EventHandler(this.btnCopyAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "触发地点";
            // 
            // cboDd
            // 
            this.cboDd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDd.FormattingEnabled = true;
            this.cboDd.Location = new System.Drawing.Point(65, 44);
            this.cboDd.Name = "cboDd";
            this.cboDd.Size = new System.Drawing.Size(100, 20);
            this.cboDd.TabIndex = 8;
            // 
            // btnDebug
            // 
            this.btnDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDebug.Location = new System.Drawing.Point(750, 43);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 7;
            this.btnDebug.Text = "调试";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // txtsEndAdd
            // 
            this.txtsEndAdd.Location = new System.Drawing.Point(496, 16);
            this.txtsEndAdd.Name = "txtsEndAdd";
            this.txtsEndAdd.Size = new System.Drawing.Size(100, 21);
            this.txtsEndAdd.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(443, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "sEndAdd";
            // 
            // txtiArg3
            // 
            this.txtiArg3.Location = new System.Drawing.Point(277, 16);
            this.txtiArg3.Name = "txtiArg3";
            this.txtiArg3.Size = new System.Drawing.Size(100, 21);
            this.txtiArg3.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "iArg3";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(65, 15);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 21);
            this.txtID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "iID";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(669, 43);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtQztj);
            this.groupBox2.Location = new System.Drawing.Point(12, 493);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(912, 123);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // txtQztj
            // 
            this.txtQztj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQztj.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQztj.Location = new System.Drawing.Point(9, 16);
            this.txtQztj.Multiline = true;
            this.txtQztj.Name = "txtQztj";
            this.txtQztj.ReadOnly = true;
            this.txtQztj.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQztj.Size = new System.Drawing.Size(897, 97);
            this.txtQztj.TabIndex = 2;
            // 
            // DevelopQuestData
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 628);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dg1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DevelopQuestData";
            this.Text = "事件管理";
            this.Load += new System.EventHandler(this.DevelopQuestData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.dg1RightMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtiArg3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtsEndAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboDd;
        private System.Windows.Forms.TextBox txtQztj;
        private System.Windows.Forms.ContextMenuStrip dg1RightMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyRow;
        private System.Windows.Forms.ToolStripMenuItem tsmInsertCopyRow;
        private System.Windows.Forms.ToolStripMenuItem tsmInsertRow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmDelRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowState;
        private System.Windows.Forms.Button btnCopyAdd;
    }
}