using DK;
using Lib;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PowerShareCard
{


    public partial class FrmMain : Form
    {
        //public Cardoperation Card = new Cardoperation();
        //FrmSet form1;
        CardInfo Card_inform;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = Global.About + "-" + Global.company_name;
            Global.card = new Cardoperation();
            int isopen = Global.card.serialport1.OpenPort(9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);//自动打开串口
            if (isopen != 0xFF)
            {
                MessageBox.Show("串口打开失败，请检查设置", "提示");
            }
            else
            {
                Global.is_open = true;
                //设置秘钥
                CardInfo cardInfo = Global.card.LoadKey(Global._KeyA, Global._KeyB, Global.Mode);
                if (cardInfo.Code != 0)
                {
                    MessageBox.Show("设置读卡器秘钥失败", "提示");
                    //throw new Exception("设置秘钥失败:" + cardInfo.Message);
                }
                else
                {
                    Global.card_secret = true;
                }
            }
            //switch (isopen)
            //{
            //    case 0x00:
            //        MessageBox.Show("不存在对象！");
            //        break;
            //    case 0x01:
            //        MessageBox.Show("串口不存在！");
            //        break;
            //    case 0x02:
            //        MessageBox.Show("串口资源占用异常！");
            //        break;
            //    case 0x03:
            //        MessageBox.Show("串口打开后不存在赋值端口！");
            //        break;
            //    case 0xFF:
            //        break;
            //}
            //Card.serialport1.OpenPort(9600, 8, Parity.None, StopBits.One);       

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmNewCard frm = new FrmNewCard();
            frm.Show();
            //Card_inform = Card.NewCard("1234567", 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Card_inform = Global.card.ReadCard("1234567", 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmRecharge frm = new FrmRecharge();
            frm.Show();
            //Card_inform = Card.Recharge("1234567", 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Card_inform = Global.card.SwipingCard("1234567", 0);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmRefund frm = new FrmRefund();
            frm.Show();
            //Card_inform = Card.HuiFCard("1234567", 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSet frm = new FrmSet();
                //frm.Owner = this;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Card_inform = Global.card.NewCard(string.Empty, 0);
            Card_inform = Global.card.HuiFCard(string.Empty, 0);
            MessageBox.Show("初始化成功");
            //Card_inform = Global.card.LoadKey(Global._KeyA, Global._KeyB, Global.Mode);
            //MessageBox.Show("装载密码 卡号" + Card_inform.Card_no + " code" + Card_inform.Code.ToString() + " message" + Card_inform.Message + " 金额" + Card_inform.Money);

            //Card_inform = Global.card.NewCard("1234567", 0);
            //MessageBox.Show("开卡 卡号" + Card_inform.Card_no + " code" + Card_inform.Code.ToString() + " message" + Card_inform.Message + " 金额" + Card_inform.Money);

            //Card_inform = Global.card.ReadCard("1234567", 0);
            //MessageBox.Show("读卡 卡号" + Card_inform.Card_no + " code" + Card_inform.Code.ToString() + " message" + Card_inform.Message + " 金额" + Card_inform.Money);

            //Card_inform = Global.card.Recharge("1234567", 2000);
            //MessageBox.Show("充值 卡号" + Card_inform.Card_no + " code" + Card_inform.Code.ToString() + " message" + Card_inform.Message + " 金额" + Card_inform.Money);

            //Card_inform = Global.card.ReadCard("1234567", 0);
            //MessageBox.Show("读卡 卡号" + Card_inform.Card_no + " code" + Card_inform.Code.ToString() + " message" + Card_inform.Message + " 金额" + Card_inform.Money);

            //Card_inform = Global.card.SwipingCard("1234567", 1000);
            //MessageBox.Show("刷卡 卡号" + Card_inform.Card_no + " code" + Card_inform.Code.ToString() + " message" + Card_inform.Message + " 金额" + Card_inform.Money);

            //Card_inform = Global.card.ReadCard("1234567", 0);
            //MessageBox.Show("读卡 卡号" + Card_inform.Card_no + " code" + Card_inform.Code.ToString() + " message" + Card_inform.Message + " 金额" + Card_inform.Money);

            //Card_inform = Global.card.HuiFCard("1234567", 0);
            //MessageBox.Show("销卡 卡号" + Card_inform.Card_no + " code" + Card_inform.Code.ToString() + " message" + Card_inform.Message + " 金额" + Card_inform.Money);
            //Card_inform = Global.card.ReadCard("1234567", 0);
            //MessageBox.Show("读卡 卡号" + Card_inform.Card_no + " code" + Card_inform.Code.ToString() + " message" + Card_inform.Message + " 金额" + Card_inform.Money);
        }

        private void btnSecret_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSecret frm = new FrmSecret();
                //frm.Owner = this;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CardInfo card1 = null ;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                card1  = Global.card.CardVerdict (string.Empty, 0);              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            MessageBox.Show(card1 .Message +"运行时间："+ts.ToString());
        }
    }
}
