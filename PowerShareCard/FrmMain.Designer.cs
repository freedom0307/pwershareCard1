namespace PowerShareCard
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSecret = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(637, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 55);
            this.button2.TabIndex = 2;
            this.button2.Text = "读卡";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(249, 112);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 55);
            this.button3.TabIndex = 3;
            this.button3.Text = "充值";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(55, 112);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(122, 55);
            this.button4.TabIndex = 4;
            this.button4.Text = "办卡";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(637, 112);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(122, 55);
            this.button5.TabIndex = 5;
            this.button5.Text = "刷卡";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(443, 112);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(122, 55);
            this.button6.TabIndex = 6;
            this.button6.Text = "退款、退卡";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(55, 260);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(122, 55);
            this.btnSet.TabIndex = 7;
            this.btnSet.Text = "连接设置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(443, 260);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(122, 55);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(637, 385);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(122, 55);
            this.btnTest.TabIndex = 9;
            this.btnTest.Text = "初始化";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSecret
            // 
            this.btnSecret.Location = new System.Drawing.Point(249, 260);
            this.btnSecret.Name = "btnSecret";
            this.btnSecret.Size = new System.Drawing.Size(122, 55);
            this.btnSecret.TabIndex = 11;
            this.btnSecret.Text = "秘钥设置";
            this.btnSecret.UseVisualStyleBackColor = true;
            this.btnSecret.Click += new System.EventHandler(this.btnSecret_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(55, 375);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 55);
            this.button1.TabIndex = 12;
            this.button1.Text = "有卡or无";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 569);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSecret);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "充值系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSecret;
        private System.Windows.Forms.Button button1;
    }
}

