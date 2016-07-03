using Lib;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerShareCard
{
    class CardService
    {
        public static bool newCard(string card_no, string user_name, string mobile)
        {
            bool b = false;
            try
            {
                string strOut = HttpUtil.CreateHttpResponse(Global.strUrl, string.Format("method=powershare.pfrecharge.createcard&session_id={0}&company_id={1}&own_company_id={2}&card_no={3}&user_name={4}&mobile={5}", Global.session_id, Global.company_id, Global.own_company_id, card_no, user_name, mobile));
                JObject jo = JObject.Parse(strOut);
                if (jo == null)
                {
                    throw new Exception("Json序类化失败" + strOut);
                }
                string strCode = jo["code"].ToString();
                string strMsg = jo["message"].ToString();
                if (strCode == "0")
                {
                    b = true;
                }
                else
                {
                    throw new Exception(strMsg);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
            return b;
        }

        public static CardInfo readCard(string card_no)
        {
            CardInfo cardInfo = null;
            try
            {
                string strOut = HttpUtil.CreateHttpResponse(Global.strUrl, string.Format("method=powershare.pfrecharge.cardinfo&session_id={0}&company_id={1}&own_company_id={2}&card_no={3}", Global.session_id, Global.company_id, Global.own_company_id, card_no));
                JObject jo = JObject.Parse(strOut);
                if (jo == null)
                {
                    throw new Exception("Json序类化失败" + strOut);
                }
                string strCode = jo["code"].ToString();
                string strMsg = jo["message"].ToString();
                if (strCode == "0")
                {
                    cardInfo = new CardInfo();
                    jo = JObject.Parse(jo["data"].ToString());
                    if (jo == null)
                    {
                        throw new Exception("Json序类化失败" + strOut);
                    }
                    cardInfo.User_name = jo["user_name"].ToString();
                    cardInfo.Mobile = jo["mobile"].ToString();
                }
                else
                {
                    throw new Exception(strMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cardInfo;
        }

        public static string rechargeCard(string card_no, double money, double remainMoney, string recharge_type)
        {
            string order_no = null;
            try
            {
                string strOut = HttpUtil.CreateHttpResponse(Global.strUrl, string.Format("method=powershare.pfrecharge.recharge&session_id={0}&company_id={1}&own_company_id={2}&card_no={3}&money={4}&remain_money={5}&recharge_type={6}", Global.session_id, Global.company_id, Global.own_company_id, card_no, money, remainMoney, recharge_type));
                JObject jo = JObject.Parse(strOut);
                if (jo == null)
                {
                    throw new Exception("Json序类化失败" + strOut);
                }
                string strCode = jo["code"].ToString();
                string strMsg = jo["message"].ToString();
                if (strCode == "0")
                {
                    string strDta = jo["data"].ToString();
                    jo = JObject.Parse(strDta);
                    if (jo == null)
                    {
                        throw new Exception("Json序类化失败" + strOut);
                    }
                    if (strCode == "0")
                    {
                        order_no = jo["order_no"].ToString();
                    }
                }
                else
                {
                    throw new Exception(strMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("异常:发起充值申请失败,---" + ex.Message);
            }
            return order_no;
        }

        public static bool updateRechargeCard(string card_no, string order_no, int state_id, double remain_money)
        {
            bool b = false;
            try
            {
                string strOut = HttpUtil.CreateHttpResponse(Global.strUrl, string.Format("method=powershare.pfrecharge.rechargeresult&session_id={0}&company_id={1}&own_company_id={2}&order_no={3}&state_id={4}&remain_money={5}&card_no={6}", Global.session_id, Global.company_id, Global.own_company_id, order_no, state_id, remain_money, card_no));
                JObject jo = JObject.Parse(strOut);
                if (jo == null)
                {
                    throw new Exception("Json序类化失败" + strOut);
                }
                string strCode = jo["code"].ToString();
                string strMsg = jo["message"].ToString();
                if (strCode == "0")
                {
                    b = true;
                }
                else
                {
                    throw new Exception(strMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("异常:更新充值申请状态失败---" + ex.Message);
            }
            return b;
        }

        public static bool refundCard(string card_no, Int32 remain_money)
        {
            bool b = false;
            try
            {
                string strOut = HttpUtil.CreateHttpResponse(Global.strUrl, string.Format("method=powershare.pfrecharge.cancelcard&session_id={0}&company_id={1}&own_company_id={2}&card_no={3}&remain_money={4}", Global.session_id, Global.company_id, Global.own_company_id, card_no, remain_money));
                JObject jo = JObject.Parse(strOut);
                if (jo == null)
                {
                    throw new Exception("Json序类化失败" + strOut);
                }
                string strCode = jo["code"].ToString();
                string strMsg = jo["message"].ToString();
                if (strCode == "0")
                {
                    b = true;
                }
                else
                {
                    throw new Exception(strMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return b;
        }
    }
}
