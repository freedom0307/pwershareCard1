using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace PowerShareCard
{
    public partial class FrmSet : Form
    {
        //FrmMain frmform;
        Int32 _BaudRate ;
        Int32 _DataSize ;
        Parity _Parity ;
        StopBits _StopBits;
        public FrmSet()
        {
            InitializeComponent();
            //frmform = frm;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 3;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            if (Global.card.serialport1.IsOpen())
            {
                label6.BackColor  = Color.LightGreen  ;
                label6 .Text ="串口已打开";
                comboBox1.Text = Global.card.serialport1.PortName;
                comboBox1.BackColor = Color.LightGreen;
                openport.Text = "关闭";
            }
            else 
            {
                label6.BackColor = Color.Yellow ;
                label6.Text = "串口已关闭";
                comboBox1.BackColor = Color.Red;
                openport.Text = "打开";
            }
        }

        private void openport_Click(object sender, EventArgs e)
        {
          
            try
            {
                if (openport.Text == "打开")
                {
                    COMSET();
                    int isopen = Global.card.serialport1.OpenPort(_BaudRate, _DataSize, _Parity, _StopBits);
                    switch (isopen )
                    {
                        case 0x00:
                            MessageBox.Show("不存在对象！");
                            break;
                        case 0x01:
                            MessageBox.Show("串口不存在！");
                            break;
                        case 0x02:
                            MessageBox.Show("串口资源占用异常！");
                            break;
                        case 0x03:
                            MessageBox.Show("串口打开后不存在赋值端口！");
                            break;
                        case 0xFF:
                            break;

                    }
                    if (isopen==0xFF)
                    {
                        label6.BackColor = Color.LightGreen ;
                        label6.Text = "串口已打开";
                        comboBox1.Text = Global.card.serialport1.PortName;
                        comboBox1.BackColor = Color.LightGreen;
                        openport.Text = "关闭";
                    }
                    else
                    {
                        label6.BackColor = Color.Yellow ;
                        label6.Text = "串口已关闭";
                        comboBox1.BackColor = Color.Red;
                        openport.Text = "打开";
                    }
                }
                else
                {
                    Global.card.serialport1.ClosePort();
                    if (!Global.card.serialport1.IsOpen())
                    {
                        label6.BackColor = Color.Yellow ;
                        label6.Text = "串口已关闭";
                        comboBox1.BackColor = Color.Red;
                        openport.Text = "打开";
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        public void COMSET()
        {
            _BaudRate = Convert.ToInt32(comboBox2 .Text);//设置波特率
            _DataSize = Convert.ToInt32(comboBox3.Text);//设置数据位
            if (comboBox4.Text == "Even")
            {
                _Parity = System.IO.Ports.Parity.Even;  //设置奇偶校验
            }
            else if (comboBox4.Text == "None")
            {
                _Parity = System.IO.Ports.Parity.None;
            }
            else if (comboBox4.Text == "0dd")
            {
                _Parity = System.IO.Ports.Parity.Odd;
            }
            if (comboBox5.Text == "1")
            {
                _StopBits = System.IO.Ports.StopBits.One;
            }
            else if (comboBox5.Text == "2")
            {
                _StopBits = System.IO.Ports.StopBits.Two;
            }
        }
    }
}
