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
    public partial class FrmRefund : Form
    {
        public FrmRefund()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRefund_Load(object sender, EventArgs e)
        {
            this.Text = Global.About + "-" + Global.company_name;
            cmbType.DataSource = Global.getPayTypeDt();
            cmbType.DisplayMember = "pay_type_name";
            cmbType.ValueMember = "pay_type_id";
        }

        private void btnReader_Click(object sender, EventArgs e)
        {
            readCard();
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

        private void btnRefund_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rtbCardNo.Text.Trim()))
            {
                MessageBox.Show("卡号不能为空,请读卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            if (string.IsNullOrEmpty(this.rtbBalance.Text.Trim()) || this.rtbBalance.Text.Trim() == "0")
            {
                MessageBox.Show("已没有余额，不能退款", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
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
                //发起退款申请
                string order_no = recharge();
                if (!string.IsNullOrEmpty(order_no))
                {
                    //充值
                    double balance = double.Parse(this.rtbBalance.Text.Trim()) - double.Parse(this.rtbMoney.Text.Trim());
                    if (CardService.updateRechargeCard(rtbCardNo.Text.Trim(), order_no, 1, balance))
                    {
                        CardInfo cardInfo = Global.card.SwipingCard(this.rtbCardNo.Text.Trim(), Int32.Parse((double.Parse(this.rtbMoney.Text.Trim()) * 100).ToString()));
                        if (cardInfo.Code == 0)
                        {
                            MessageBox.Show("退款成功");
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                            readCard();
                        }
                        else
                        {
                            throw new Exception("平台扣款成功,硬件扣款失败");
                            //Global.processException(cardInfo.Code);
                        }
                    }
                    else
                    {
                        throw new Exception("退款申请失败");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "退款失败:", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
            double balance = double.Parse(this.rtbBalance.Text.Trim()) - money;
            return CardService.rechargeCard(rtbCardNo.Text.Trim(), -money, balance, cmbType.SelectedValue.ToString());

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rtbCardNo.Text.Trim()))
            {
                MessageBox.Show("卡号不能为空,请读卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!string.IsNullOrEmpty(this.rtbBalance.Text.Trim()) && this.rtbBalance.Text.Trim() != "0")
            {
                MessageBox.Show("此卡有余额,不能退卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
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
                if (CardService.refundCard(this.rtbCardNo.Text.Trim(), int.Parse(rtbBalance.Text.Trim())))
                {
                    CardInfo cardInfo = Global.card.HuiFCard(this.rtbCardNo.Text.Trim(), 0);
                    if (cardInfo.Code == 0)
                    {
                        MessageBox.Show("退卡成功");
                        rtbBalance.Text = string.Empty;
                        rtbCardNo.Text = string.Empty;
                        rtbMoney.Text = string.Empty;
                        txtMobile.Text = string.Empty;
                        txtUserName.Text = string.Empty;
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    }
                    else
                    {
                        Global.processException(cardInfo.Code);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "退卡失败:", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

    }
}
