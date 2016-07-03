namespace PowerShareCard
{
    partial class FrmSecret
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
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.txtKeyB = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKeyA = new System.Windows.Forms.RichTextBox();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbMode
            // 
            this.cmbMode.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Items.AddRange(new object[] {
            "默认模式",
            "优灵模式"});
            this.cmbMode.Location = new System.Drawing.Point(164, 170);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(281, 33);
            this.cmbMode.TabIndex = 55;
            // 
            // txtKeyB
            // 
            this.txtKeyB.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtKeyB.Location = new System.Drawing.Point(164, 113);
            this.txtKeyB.Multiline = false;
            this.txtKeyB.Name = "txtKeyB";
            this.txtKeyB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtKeyB.Size = new System.Drawing.Size(281, 35);
            this.txtKeyB.TabIndex = 51;
            this.txtKeyB.Text = "";
            this.txtKeyB.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(76, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 20);
            this.label3.TabIndex = 54;
            this.label3.Text = "秘钥B";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(75, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 53;
            this.label1.Text = "加密模式";
            // 
            // txtKeyA
            // 
            this.txtKeyA.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtKeyA.Location = new System.Drawing.Point(164, 56);
            this.txtKeyA.Multiline = false;
            this.txtKeyA.Name = "txtKeyA";
            this.txtKeyA.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtKeyA.Size = new System.Drawing.Size(281, 35);
            this.txtKeyA.TabIndex = 50;
            this.txtKeyA.Text = "";
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardNo.Location = new System.Drawing.Point(75, 63);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(47, 20);
            this.lblCardNo.TabIndex = 52;
            this.lblCardNo.Text = "秘钥A";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(167, 235);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(103, 41);
            this.btnOk.TabIndex = 57;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(300, 235);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 41);
            this.btnClose.TabIndex = 56;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmSecret
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 369);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.txtKeyB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKeyA);
            this.Controls.Add(this.lblCardNo);
            this.Name = "FrmSecret";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "秘钥设置";
            this.Load += new System.EventHandler(this.FrmSecret_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.RichTextBox txtKeyB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtKeyA;
        private System.Windows.Forms.Label lblCardNo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnClose;
    }
}