using Lib;
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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Login();
        }

        /// <summary>
        /// 登录
        /// </summary>
        private void Login()
        {
            if (this.txtUserName.Text.Trim() == "" || this.txtPwd.Text.Trim() == "")
            {
                MessageBox.Show("用户名密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                string strOut = HttpUtil.CreateHttpResponse(Global.strUrl, string.Format("method=powershare.pfuser.login&login_name={0}&password={1}", this.txtUserName.Text.Trim(), this.txtPwd.Text.Trim()));
                JObject jo = JObject.Parse(strOut);

                //JSONObject jo = JSONConvert.DeserializeObject(strOut);
                if (jo == null)
                {
                    throw new Exception("Json序类化失败" + strOut);
                }
                string strCode = jo["code"].ToString();
                string strMsg = jo["message"].ToString();
                if (strCode == "0")
                {
                    string strData = jo["data"].ToString();
                    JObject joData = JObject.Parse(strData);
                    if (joData == null)
                    {
                        throw new Exception("Json序类化失败" + strOut);
                    }
                    Global.session_id = joData["sessionId"].ToString();
                    //用户信息
                    string strUser = joData["user"].ToString();
                    JObject joUser = JObject.Parse(strUser);
                    if (joUser == null)
                    {
                        throw new Exception("Json序类化失败" + strOut);
                    }
                    Global.user_name = joUser["login_name"].ToString();
                    //运营商
                    string strCompany = joUser["company"].ToString();
                    JObject joCompany = JObject.Parse(strCompany);
                    if (joCompany == null)
                    {
                        throw new Exception("Json序类化失败" + strOut);
                    }
                    Global.company_id = int.Parse(joCompany["company_id"].ToString());
                    Global.company_name = joCompany["company_name"].ToString();
                    //主运营商
                    string strRootCompany = joUser["rootCompany"].ToString();
                    JObject joRootCompany = JObject.Parse(strRootCompany);
                    if (joRootCompany == null)
                    {
                        throw new Exception("Json序类化失败" + strOut);
                    }
                    Global.own_company_id = int.Parse(joRootCompany["company_id"].ToString());
                    //卡秘钥
                    if (joUser["encryption"] != null)
                    {
                        string strEncryption = joUser["encryption"].ToString();
                        JObject joEncryption = JObject.Parse(strEncryption);
                        if (joEncryption == null)
                        {
                            throw new Exception("Json序类化失败" + strOut);
                        }
                        Global._KeyA = joEncryption["key_a"].ToString();
                        Global._KeyB = joEncryption["key_b"].ToString();
                        Global.Mode = joEncryption["mode"].ToString();
                    }
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    IsLogin = true;
                    this.Close();
                }
                else
                {
                    throw new Exception(strMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "登陆失败：", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        public bool IsLogin = false;//是否登陆成功

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsLogin = false;
            this.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.Text = Global.About;
            txtUserName.SelectionStart = 2;
            txtUserName.Focus();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            //输入框回车事件
            switch (e.KeyValue)
            {
                case 13:
                    if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
                    {
                        txtPwd.Focus();
                        txtPwd.SelectAll();
                    }
                    break;
                default:
                    break;
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            //输入框回车事件
            switch (e.KeyValue)
            {
                case 13:
                    if (!string.IsNullOrEmpty(txtPwd.Text.Trim()))
                    {
                        Login();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
