using Model;
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
    public partial class FrmSecret : Form
    {
        public FrmSecret()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //设置秘钥
            CardInfo cardInfo = Global.card.LoadKey(Global._KeyA, Global._KeyB, Global.Mode);
            if (cardInfo.Code != 0)
            {
                MessageBox.Show("设置读卡器秘钥失败", "提示");
                //throw new Exception("设置秘钥失败:" + cardInfo.Message);
            }
            else
            {
                MessageBox.Show("设置成功", "提示");
            }
        }

        private void FrmSecret_Load(object sender, EventArgs e)
        {
            txtKeyA.Text = Global._KeyA;
            txtKeyB.Text = Global._KeyB;
            cmbMode.Text = Global.Mode;
        }
    }
}
