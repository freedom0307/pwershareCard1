namespace PowerShareCard
{
    partial class FrmRecharge
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
            this.btnReader = new System.Windows.Forms.Button();
            this.btnRecharge = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtMobile = new System.Windows.Forms.RichTextBox();
            this.PRODUCT_LAB = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbCardNo = new System.Windows.Forms.RichTextBox();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbMoney = new System.Windows.Forms.RichTextBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.rtbBalance = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReader
            // 
            this.btnReader.Location = new System.Drawing.Point(539, 74);
            this.btnReader.Name = "btnReader";
            this.btnReader.Size = new System.Drawing.Size(88, 35);
            this.btnReader.TabIndex = 8;
            this.btnReader.Text = "读卡";
            this.btnReader.UseVisualStyleBackColor = true;
            this.btnReader.Click += new System.EventHandler(this.btnReader_Click);
            // 
            // btnRecharge
            // 
            this.btnRecharge.Location = new System.Drawing.Point(226, 430);
            this.btnRecharge.Name = "btnRecharge";
            this.btnRecharge.Size = new System.Drawing.Size(88, 35);
            this.btnRecharge.TabIndex = 16;
            this.btnRecharge.Text = "充值";
            this.btnRecharge.UseVisualStyleBackColor = true;
            this.btnRecharge.Click += new System.EventHandler(this.btnRecharge_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(344, 430);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 35);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtMobile
            // 
            this.txtMobile.Enabled = false;
            this.txtMobile.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMobile.Location = new System.Drawing.Point(226, 186);
            this.txtMobile.Multiline = false;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtMobile.Size = new System.Drawing.Size(281, 35);
            this.txtMobile.TabIndex = 3;
            this.txtMobile.Text = "";
            this.txtMobile.WordWrap = false;
            // 
            // PRODUCT_LAB
            // 
            this.PRODUCT_LAB.AutoSize = true;
            this.PRODUCT_LAB.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PRODUCT_LAB.Location = new System.Drawing.Point(138, 200);
            this.PRODUCT_LAB.Name = "PRODUCT_LAB";
            this.PRODUCT_LAB.Size = new System.Drawing.Size(51, 20);
            this.PRODUCT_LAB.TabIndex = 21;
            this.PRODUCT_LAB.Text = "手机号";
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.Location = new System.Drawing.Point(226, 241);
            this.txtUserName.Multiline = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtUserName.Size = new System.Drawing.Size(281, 35);
            this.txtUserName.TabIndex = 4;
            this.txtUserName.Text = "";
            this.txtUserName.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(138, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "姓名";
            // 
            // rtbCardNo
            // 
            this.rtbCardNo.Enabled = false;
            this.rtbCardNo.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbCardNo.Location = new System.Drawing.Point(226, 74);
            this.rtbCardNo.Multiline = false;
            this.rtbCardNo.Name = "rtbCardNo";
            this.rtbCardNo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbCardNo.Size = new System.Drawing.Size(281, 35);
            this.rtbCardNo.TabIndex = 1;
            this.rtbCardNo.Text = "";
            this.rtbCardNo.WordWrap = false;
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardNo.Location = new System.Drawing.Point(138, 89);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(37, 20);
            this.lblCardNo.TabIndex = 25;
            this.lblCardNo.Text = "卡号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(138, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "充值类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(138, 362);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "金额";
            // 
            // rtbMoney
            // 
            this.rtbMoney.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbMoney.Location = new System.Drawing.Point(226, 349);
            this.rtbMoney.Multiline = false;
            this.rtbMoney.Name = "rtbMoney";
            this.rtbMoney.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbMoney.Size = new System.Drawing.Size(281, 35);
            this.rtbMoney.TabIndex = 6;
            this.rtbMoney.Text = "";
            this.rtbMoney.WordWrap = false;
            // 
            // cmbType
            // 
            this.cmbType.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "现金"});
            this.cmbType.Location = new System.Drawing.Point(226, 296);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(281, 33);
            this.cmbType.TabIndex = 5;
            // 
            // rtbBalance
            // 
            this.rtbBalance.Enabled = false;
            this.rtbBalance.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbBalance.Location = new System.Drawing.Point(226, 128);
            this.rtbBalance.Multiline = false;
            this.rtbBalance.Name = "rtbBalance";
            this.rtbBalance.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbBalance.Size = new System.Drawing.Size(281, 35);
            this.rtbBalance.TabIndex = 2;
            this.rtbBalance.Text = "";
            this.rtbBalance.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(138, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 31;
            this.label3.Text = "余额";
            // 
            // FrmRecharge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 569);
            this.Controls.Add(this.rtbBalance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.rtbMoney);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbCardNo);
            this.Controls.Add(this.lblCardNo);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMobile);
            this.Controls.Add(this.PRODUCT_LAB);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRecharge);
            this.Controls.Add(this.btnReader);
            this.Name = "FrmRecharge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "充值";
            this.Load += new System.EventHandler(this.FrmRecharge_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReader;
        private System.Windows.Forms.Button btnRecharge;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RichTextBox txtMobile;
        private System.Windows.Forms.Label PRODUCT_LAB;
        private System.Windows.Forms.RichTextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtbCardNo;
        private System.Windows.Forms.Label lblCardNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbMoney;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.RichTextBox rtbBalance;
        private System.Windows.Forms.Label label3;
    }
}