using Lib;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerShareCard
{
    public partial class FrmNewCard : Form
    {
        public FrmNewCard()
        {
            InitializeComponent();
        }

        private void FrmNewCard_Load(object sender, EventArgs e)
        {
            this.Text = Global.About + "-" + Global.company_name;
            //this.lblCardNo.Visible = false;
            //this.txtCardNo.Visible = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtMobile.Text.Trim()))
            {
                MessageBox.Show("手机号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            if (Global.is_open == false)
            {
                MessageBox.Show("硬件连接异常,请设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            if (Global.card_secret == false)
            {
                MessageBox.Show("请设置读卡器秘钥", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                CardInfo cardInfo = Global.card.NewCard(null, 0);
                if (cardInfo.Code == 0)
                {
                    this.txtCardNo.Text = cardInfo.Card_no;
                    if (CardService.newCard(cardInfo.Card_no, this.txtUserName.Text.Trim(), this.txtMobile.Text.Trim()))
                    {
                        MessageBox.Show("开卡成功");
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    }
                }
                else
                {
                    Global.processException(cardInfo.Code);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "开卡失败:", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
