namespace PowerShareCard
{
    partial class FrmNewCard
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtMobile = new System.Windows.Forms.RichTextBox();
            this.PRODUCT_LAB = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCardNo = new System.Windows.Forms.RichTextBox();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(417, 309);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 48);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(244, 309);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(101, 48);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "开卡";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtMobile
            // 
            this.txtMobile.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMobile.Location = new System.Drawing.Point(244, 117);
            this.txtMobile.Multiline = false;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtMobile.Size = new System.Drawing.Size(281, 35);
            this.txtMobile.TabIndex = 1;
            this.txtMobile.Text = "";
            this.txtMobile.WordWrap = false;
            // 
            // PRODUCT_LAB
            // 
            this.PRODUCT_LAB.AutoSize = true;
            this.PRODUCT_LAB.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PRODUCT_LAB.Location = new System.Drawing.Point(156, 124);
            this.PRODUCT_LAB.Name = "PRODUCT_LAB";
            this.PRODUCT_LAB.Size = new System.Drawing.Size(51, 20);
            this.PRODUCT_LAB.TabIndex = 19;
            this.PRODUCT_LAB.Text = "手机号";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.Location = new System.Drawing.Point(244, 169);
            this.txtUserName.Multiline = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtUserName.Size = new System.Drawing.Size(281, 35);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.Text = "";
            this.txtUserName.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(156, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "姓名";
            // 
            // txtCardNo
            // 
            this.txtCardNo.Enabled = false;
            this.txtCardNo.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardNo.Location = new System.Drawing.Point(244, 220);
            this.txtCardNo.Multiline = false;
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtCardNo.Size = new System.Drawing.Size(281, 35);
            this.txtCardNo.TabIndex = 3;
            this.txtCardNo.Text = "";
            this.txtCardNo.WordWrap = false;
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardNo.Location = new System.Drawing.Point(156, 227);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(37, 20);
            this.lblCardNo.TabIndex = 23;
            this.lblCardNo.Text = "卡号";
            // 
            // FrmNewCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 569);
            this.Controls.Add(this.txtCardNo);
            this.Controls.Add(this.lblCardNo);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMobile);
            this.Controls.Add(this.PRODUCT_LAB);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Name = "FrmNewCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开卡";
            this.Load += new System.EventHandler(this.FrmNewCard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RichTextBox txtMobile;
        private System.Windows.Forms.Label PRODUCT_LAB;
        private System.Windows.Forms.RichTextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtCardNo;
        private System.Windows.Forms.Label lblCardNo;
    }
}