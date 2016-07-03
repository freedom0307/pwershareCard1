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
    public partial class FrmRecharge : Form
    {
        public FrmRecharge()
        {
            InitializeComponent();
        }

        private void btnReader_Click(object sender, EventArgs e)
        {
            readCard();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRecharge_Load(object sender, EventArgs e)
        {
            this.Text = Global.About + "-" + Global.company_name;
            cmbType.DataSource = Global.getPayTypeDt();
            cmbType.DisplayMember = "pay_type_name";
            cmbType.ValueMember = "pay_type_id";
        }

        private void btnRecharge_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rtbCardNo.Text.Trim()))
            {
                MessageBox.Show("卡号不能为空,请读卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
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
                //发起充值申请
                string order_no = recharge();
                if (!string.IsNullOrEmpty(order_no))
                {
                    double balance = double.Parse(this.rtbMoney.Text.Trim()) + double.Parse(this.rtbBalance.Text.Trim());
                    //充值
                    if (CardService.updateRechargeCard(rtbCardNo.Text.Trim(), order_no, 1, balance))
                    {
                        CardInfo cardInfo = Global.card.Recharge(this.rtbCardNo.Text.Trim(), Int32.Parse((double.Parse(this.rtbMoney.Text.Trim()) * 100).ToString()));
                        if (cardInfo.Code == 0)
                        {
                            MessageBox.Show("充值成功");
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                            readCard();
                        }
                        else
                        {
                            throw new Exception("平台充值成功,硬件充值失败");
                            //Global.processException(cardInfo.Code);
                        }
                    }
                }
                else
                {
                    throw new Exception("充值申请失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "充值失败:", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private string recharge()
        {
            //充值
            double money = double.Parse(this.rtbMoney.Text.Trim());
            double balance = money + double.Parse(this.rtbBalance.Text.Trim());
            return CardService.rechargeCard(rtbCardNo.Text.Trim(), money, balance, cmbType.SelectedValue.ToString());
        }

        private void readCard()
        {
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
                CardInfo cardInfo = Global.card.ReadCard(string.Empty, 0);
                if (cardInfo.Code == 0)
                {
                    this.rtbCardNo.Text = cardInfo.Card_no;
                    this.rtbBalance.Text = (double.Parse(cardInfo.Money.ToString()) / 100).ToString();
                    this.rtbMoney.Text = string.Empty;
                    //读取数据库卡信息
                    cardInfo = CardService.readCard(cardInfo.Card_no);
                    if (cardInfo != null)
                    {
                        this.txtMobile.Text = cardInfo.Mobile;
                        this.txtUserName.Text = cardInfo.User_name;
                        //MessageBox.Show("读卡成功");
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
                MessageBox.Show(ex.Message, "读卡失败:", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

    }
}
