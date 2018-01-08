namespace xkfy_mod
{
    partial class DevelopBtnData_List
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
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.iBtnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iPhy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.SuspendLayout();
            // 
            // dg1
            // 
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iBtnID,
            this.xRemark,
            this.iPhy});
            this.dg1.Location = new System.Drawing.Point(12, 41);
            this.dg1.Name = "dg1";
            this.dg1.RowTemplate.Height = 23;
            this.dg1.Size = new System.Drawing.Size(721, 346);
            this.dg1.TabIndex = 0;
            this.dg1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellDoubleClick);
            this.dg1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(658, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(72, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "名称";
            // 
            // iBtnID
            // 
            this.iBtnID.DataPropertyName = "iBtnID";
            this.iBtnID.HeaderText = "iBtnID";
            this.iBtnID.Name = "iBtnID";
            // 
            // xRemark
            // 
            this.xRemark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.xRemark.DataPropertyName = "xRemark";
            this.xRemark.HeaderText = "名称";
            this.xRemark.Name = "xRemark";
            // 
            // iPhy
            // 
            this.iPhy.DataPropertyName = "iPhy";
            this.iPhy.HeaderText = "体力消耗";
            this.iPhy.Name = "iPhy";
            // 
            // DevelopBtnData_List
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 399);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.dg1);
            this.Name = "DevelopBtnData_List";
            this.Text = "DevelopBtnData_List";
            this.Load += new System.EventHandler(this.DevelopBtnData_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iBtnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn xRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn iPhy;
    }
}