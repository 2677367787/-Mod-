namespace xkfy_mod
{
    partial class EventChart
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.txtHuiHe = new System.Windows.Forms.TextBox();
            this.btnDo = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.btnAddEvent = new System.Windows.Forms.Button();
            this.btnUpdateEvent = new System.Windows.Forms.Button();
            this.btnAddTalk = new System.Windows.Forms.Button();
            this.btnUpdateTalk = new System.Windows.Forms.Button();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.col_key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.dgTalk = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTalk)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(163, 11);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(604, 564);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // txtHuiHe
            // 
            this.txtHuiHe.Location = new System.Drawing.Point(933, 14);
            this.txtHuiHe.Name = "txtHuiHe";
            this.txtHuiHe.Size = new System.Drawing.Size(100, 21);
            this.txtHuiHe.TabIndex = 1;
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(773, 167);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(75, 23);
            this.btnDo.TabIndex = 2;
            this.btnDo.Text = "解析";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(773, 138);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 5;
            this.btnDebug.Text = "调试";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnAddEvent
            // 
            this.btnAddEvent.Location = new System.Drawing.Point(773, 12);
            this.btnAddEvent.Name = "btnAddEvent";
            this.btnAddEvent.Size = new System.Drawing.Size(75, 23);
            this.btnAddEvent.TabIndex = 7;
            this.btnAddEvent.Text = "新增事件";
            this.btnAddEvent.UseVisualStyleBackColor = true;
            // 
            // btnUpdateEvent
            // 
            this.btnUpdateEvent.Location = new System.Drawing.Point(773, 41);
            this.btnUpdateEvent.Name = "btnUpdateEvent";
            this.btnUpdateEvent.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateEvent.TabIndex = 8;
            this.btnUpdateEvent.Text = "修改事件";
            this.btnUpdateEvent.UseVisualStyleBackColor = true;
            // 
            // btnAddTalk
            // 
            this.btnAddTalk.Location = new System.Drawing.Point(773, 70);
            this.btnAddTalk.Name = "btnAddTalk";
            this.btnAddTalk.Size = new System.Drawing.Size(75, 27);
            this.btnAddTalk.TabIndex = 9;
            this.btnAddTalk.Text = "新增对话";
            this.btnAddTalk.UseVisualStyleBackColor = true;
            // 
            // btnUpdateTalk
            // 
            this.btnUpdateTalk.Location = new System.Drawing.Point(773, 105);
            this.btnUpdateTalk.Name = "btnUpdateTalk";
            this.btnUpdateTalk.Size = new System.Drawing.Size(75, 27);
            this.btnUpdateTalk.TabIndex = 10;
            this.btnUpdateTalk.Text = "修改对话";
            this.btnUpdateTalk.UseVisualStyleBackColor = true;
            // 
            // dg1
            // 
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_key,
            this.col_value});
            this.dg1.Location = new System.Drawing.Point(8, 11);
            this.dg1.Name = "dg1";
            this.dg1.RowHeadersVisible = false;
            this.dg1.RowTemplate.Height = 23;
            this.dg1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg1.Size = new System.Drawing.Size(149, 639);
            this.dg1.TabIndex = 11;
            this.dg1.CurrentCellChanged += new System.EventHandler(this.dg1_CurrentCellChanged);
            // 
            // col_key
            // 
            this.col_key.DataPropertyName = "Key";
            this.col_key.HeaderText = "回合";
            this.col_key.Name = "col_key";
            this.col_key.Width = 40;
            // 
            // col_value
            // 
            this.col_value.DataPropertyName = "Value";
            this.col_value.HeaderText = "年月";
            this.col_value.Name = "col_value";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(163, 581);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(604, 69);
            this.txtRemark.TabIndex = 12;
            // 
            // dgTalk
            // 
            this.dgTalk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTalk.Location = new System.Drawing.Point(773, 211);
            this.dgTalk.Name = "dgTalk";
            this.dgTalk.RowTemplate.Height = 23;
            this.dgTalk.Size = new System.Drawing.Size(260, 439);
            this.dgTalk.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(874, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "当前ID：";
            // 
            // EventChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 653);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgTalk);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.btnUpdateTalk);
            this.Controls.Add(this.btnAddTalk);
            this.Controls.Add(this.btnUpdateEvent);
            this.Controls.Add(this.btnAddEvent);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnDo);
            this.Controls.Add(this.txtHuiHe);
            this.Controls.Add(this.treeView1);
            this.Name = "EventChart";
            this.Text = "事件树";
            this.Load += new System.EventHandler(this.EventChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTalk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox txtHuiHe;
        private System.Windows.Forms.Button btnDo;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Button btnAddEvent;
        private System.Windows.Forms.Button btnUpdateEvent;
        private System.Windows.Forms.Button btnAddTalk;
        private System.Windows.Forms.Button btnUpdateTalk;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_key;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_value;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.DataGridView dgTalk;
        private System.Windows.Forms.Label label1;
    }
}