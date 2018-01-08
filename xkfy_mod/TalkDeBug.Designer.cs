namespace xkfy_mod
{
    partial class TalkDeBug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TalkDeBug));
            this.txtNext = new System.Windows.Forms.Button();
            this.panelBackground = new System.Windows.Forms.Panel();
            this.lblWin = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbZd = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFailure = new System.Windows.Forms.Button();
            this.btnWin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbCondition = new System.Windows.Forms.GroupBox();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.panelTalk = new System.Windows.Forms.Panel();
            this.lbltkMsg = new System.Windows.Forms.Label();
            this.btnUpdateTalk = new System.Windows.Forms.Button();
            this.btnD = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.btnB = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.txtTalk = new System.Windows.Forms.TextBox();
            this.chkTalk = new System.Windows.Forms.CheckBox();
            this.gbZd.SuspendLayout();
            this.gbCondition.SuspendLayout();
            this.panelTalk.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNext
            // 
            this.txtNext.Location = new System.Drawing.Point(935, 17);
            this.txtNext.Name = "txtNext";
            this.txtNext.Size = new System.Drawing.Size(99, 76);
            this.txtNext.TabIndex = 0;
            this.txtNext.Text = "开始";
            this.txtNext.UseVisualStyleBackColor = true;
            this.txtNext.Click += new System.EventHandler(this.txtNext_Click);
            // 
            // panelBackground
            // 
            this.panelBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelBackground.Location = new System.Drawing.Point(14, 32);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(411, 227);
            this.panelBackground.TabIndex = 2;
            // 
            // lblWin
            // 
            this.lblWin.AutoSize = true;
            this.lblWin.Location = new System.Drawing.Point(442, 59);
            this.lblWin.Name = "lblWin";
            this.lblWin.Size = new System.Drawing.Size(0, 12);
            this.lblWin.TabIndex = 14;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(442, 32);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 12);
            this.lblMsg.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "背景图片：";
            // 
            // gbZd
            // 
            this.gbZd.Controls.Add(this.label3);
            this.gbZd.Controls.Add(this.btnFailure);
            this.gbZd.Controls.Add(this.btnWin);
            this.gbZd.Location = new System.Drawing.Point(694, 20);
            this.gbZd.Name = "gbZd";
            this.gbZd.Size = new System.Drawing.Size(198, 76);
            this.gbZd.TabIndex = 16;
            this.gbZd.TabStop = false;
            this.gbZd.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "触发战斗，请选择战斗结果";
            // 
            // btnFailure
            // 
            this.btnFailure.Location = new System.Drawing.Point(26, 47);
            this.btnFailure.Name = "btnFailure";
            this.btnFailure.Size = new System.Drawing.Size(50, 23);
            this.btnFailure.TabIndex = 1;
            this.btnFailure.Text = "输";
            this.btnFailure.UseVisualStyleBackColor = true;
            this.btnFailure.Click += new System.EventHandler(this.btnWin_Click);
            // 
            // btnWin
            // 
            this.btnWin.Location = new System.Drawing.Point(124, 47);
            this.btnWin.Name = "btnWin";
            this.btnWin.Size = new System.Drawing.Size(49, 23);
            this.btnWin.TabIndex = 0;
            this.btnWin.Text = "赢";
            this.btnWin.UseVisualStyleBackColor = true;
            this.btnWin.Click += new System.EventHandler(this.btnWin_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(2, 459);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 260);
            this.panel1.TabIndex = 17;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // gbCondition
            // 
            this.gbCondition.Controls.Add(this.btnYes);
            this.gbCondition.Controls.Add(this.btnNo);
            this.gbCondition.Controls.Add(this.txtCondition);
            this.gbCondition.Location = new System.Drawing.Point(704, 9);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Size = new System.Drawing.Size(225, 138);
            this.gbCondition.TabIndex = 18;
            this.gbCondition.TabStop = false;
            this.gbCondition.Visible = false;
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(6, 110);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(69, 23);
            this.btnYes.TabIndex = 19;
            this.btnYes.Text = "满足条件";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(170, 110);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(49, 23);
            this.btnNo.TabIndex = 18;
            this.btnNo.Text = "不满足";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // txtCondition
            // 
            this.txtCondition.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCondition.Location = new System.Drawing.Point(6, 11);
            this.txtCondition.Multiline = true;
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.ReadOnly = true;
            this.txtCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCondition.Size = new System.Drawing.Size(213, 93);
            this.txtCondition.TabIndex = 20;
            // 
            // panelTalk
            // 
            this.panelTalk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelTalk.BackgroundImage")));
            this.panelTalk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelTalk.Controls.Add(this.lbltkMsg);
            this.panelTalk.Controls.Add(this.btnUpdateTalk);
            this.panelTalk.Controls.Add(this.btnD);
            this.panelTalk.Controls.Add(this.btnC);
            this.panelTalk.Controls.Add(this.btnB);
            this.panelTalk.Controls.Add(this.btnA);
            this.panelTalk.Controls.Add(this.txtTalk);
            this.panelTalk.Location = new System.Drawing.Point(12, 265);
            this.panelTalk.Name = "panelTalk";
            this.panelTalk.Size = new System.Drawing.Size(413, 188);
            this.panelTalk.TabIndex = 13;
            this.panelTalk.Visible = false;
            // 
            // lbltkMsg
            // 
            this.lbltkMsg.AutoSize = true;
            this.lbltkMsg.BackColor = System.Drawing.Color.White;
            this.lbltkMsg.ForeColor = System.Drawing.Color.Blue;
            this.lbltkMsg.Location = new System.Drawing.Point(94, 167);
            this.lbltkMsg.Name = "lbltkMsg";
            this.lbltkMsg.Size = new System.Drawing.Size(0, 12);
            this.lbltkMsg.TabIndex = 15;
            // 
            // btnUpdateTalk
            // 
            this.btnUpdateTalk.Location = new System.Drawing.Point(212, 162);
            this.btnUpdateTalk.Name = "btnUpdateTalk";
            this.btnUpdateTalk.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateTalk.TabIndex = 14;
            this.btnUpdateTalk.Text = "修改对话";
            this.btnUpdateTalk.UseVisualStyleBackColor = true;
            this.btnUpdateTalk.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnD
            // 
            this.btnD.Location = new System.Drawing.Point(212, 121);
            this.btnD.Name = "btnD";
            this.btnD.Size = new System.Drawing.Size(190, 23);
            this.btnD.TabIndex = 13;
            this.btnD.Text = "button3";
            this.btnD.UseVisualStyleBackColor = true;
            this.btnD.Visible = false;
            this.btnD.Click += new System.EventHandler(this.btnA_Click);
            // 
            // btnC
            // 
            this.btnC.Location = new System.Drawing.Point(9, 121);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(190, 23);
            this.btnC.TabIndex = 12;
            this.btnC.Text = "button4";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Visible = false;
            this.btnC.Click += new System.EventHandler(this.btnA_Click);
            // 
            // btnB
            // 
            this.btnB.Location = new System.Drawing.Point(212, 92);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(190, 23);
            this.btnB.TabIndex = 11;
            this.btnB.Text = "button2";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Visible = false;
            this.btnB.Click += new System.EventHandler(this.btnA_Click);
            // 
            // btnA
            // 
            this.btnA.Location = new System.Drawing.Point(9, 92);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(190, 23);
            this.btnA.TabIndex = 10;
            this.btnA.Text = "button1";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Visible = false;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // txtTalk
            // 
            this.txtTalk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTalk.Location = new System.Drawing.Point(9, 8);
            this.txtTalk.Multiline = true;
            this.txtTalk.Name = "txtTalk";
            this.txtTalk.Size = new System.Drawing.Size(395, 77);
            this.txtTalk.TabIndex = 9;
            // 
            // chkTalk
            // 
            this.chkTalk.Location = new System.Drawing.Point(936, 100);
            this.chkTalk.Name = "chkTalk";
            this.chkTalk.Size = new System.Drawing.Size(104, 42);
            this.chkTalk.TabIndex = 19;
            this.chkTalk.Text = "我想修改对话和贴图位置";
            this.chkTalk.UseVisualStyleBackColor = true;
            // 
            // TalkDeBug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 724);
            this.Controls.Add(this.chkTalk);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbZd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelTalk);
            this.Controls.Add(this.lblWin);
            this.Controls.Add(this.txtNext);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.panelBackground);
            this.Controls.Add(this.gbCondition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TalkDeBug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TalkDeBug";
            this.Load += new System.EventHandler(this.TalkDeBug_Load);
            this.gbZd.ResumeLayout(false);
            this.gbZd.PerformLayout();
            this.gbCondition.ResumeLayout(false);
            this.gbCondition.PerformLayout();
            this.panelTalk.ResumeLayout(false);
            this.panelTalk.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button txtNext;
        private System.Windows.Forms.Panel panelBackground;
        private System.Windows.Forms.TextBox txtTalk;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Panel panelTalk;
        private System.Windows.Forms.Label lblWin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbZd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFailure;
        private System.Windows.Forms.Button btnWin;
        private System.Windows.Forms.Button btnD;
        private System.Windows.Forms.Button btnC;
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbCondition;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.Button btnUpdateTalk;
        private System.Windows.Forms.Label lbltkMsg;
        private System.Windows.Forms.CheckBox chkTalk;
    }
}