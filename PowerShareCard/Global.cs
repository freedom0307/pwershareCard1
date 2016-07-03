using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerShareCard
{
    public class Global
    {

        public static string DbCon = "Data Source=" + Application.StartupPath + @"\System.db" + ";Version=3;New=True;Compress=True;";

        public static string _KeyA = string.Empty;

        public static string _KeyB = string.Empty;

        public static string Mode = string.Empty;

        public static bool card_secret = false;

        public static bool is_open = false;

        public static Cardoperation card = null;
        /// <summary>
        /// About
        /// </summary>
        public static string About = "电享充值系统V1.0.0";

        /// <summary>
        /// session_id
        /// </summary>
        public static string session_id = null;

        /// <summary>
        /// 用户名
        /// </summary>
        public static string user_name = null;

        /// <summary>
        /// 公司编号
        /// </summary>
        public static int company_id;

        /// <summary>
        /// 公司名称
        /// </summary>
        public static string company_name = null;

        /// <summary>
        /// 主运营商编号
        /// </summary>
        public static int own_company_id;

        public static string strUrl = Config.GetValue("url") + @"/api?format=json&v=" + Config.GetValue("version") + @"&app_id=" + Config.GetValue("app_id") + "&";
        //public static void insertCardData(string card_no, int remain_money, string mobile, string user_name, int type, string id)
        //{
        //    string sql = "insert into cardinfo (card_no, mobile, user_name, type, guid, remain_money, create_dtme) values (@card_no, @mobile, @user_name, @type, @guid, @remain_money, @create_dtme)";
        //    SQLiteParameter[] parameters = {
        //            new SQLiteParameter("@card_no", DbType.String),
        //            new SQLiteParameter("@mobile", DbType.String),
        //            new SQLiteParameter("@user_name", DbType.String),
        //            new SQLiteParameter("@type", DbType.Int32),
        //            new SQLiteParameter("@guid", DbType.String),
        //            new SQLiteParameter("@remain_money", DbType.Int32),
        //            new SQLiteParameter("@create_dtme", DbType.DateTime)
        //                                   };
        //    parameters[0].Value = card_no;
        //    parameters[1].Value = mobile;
        //    parameters[2].Value = user_name;
        //    parameters[3].Value = type;
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        parameters[4].Value = System.Guid.NewGuid().ToString();
        //    }
        //    else
        //    {
        //        parameters[4].Value = id;   
        //    }
        //    parameters[5].Value = remain_money;
        //    parameters[6].Value = System.DateTime.Now;
        //    SqliteHelper.ExecuteNonQuery(Global.DbCon, sql, parameters);
        //}

        //public static DataTable getCardData()
        //{
        //    string sql = "select * from cardinfo order by create_dtme";
        //    return SqliteHelper.GetDataSet(Global.DbCon, sql).Tables[0];
        //}

        //public static void deleteCardData(string guid)
        //{
        //    string sql = "delete from cardinfo where guid = @guid)";
        //    SQLiteParameter[] parameters = {
        //            new SQLiteParameter("@guid", DbType.String)
        //                                   };
        //    parameters[0].Value = guid;
        //    SqliteHelper.ExecuteNonQuery(Global.DbCon, sql, parameters);
        //}

        public static DataTable getPayTypeDt()
        {
            DataTable dt = new DataTable("Datas");
            DataColumn dc = null;
            dc = dt.Columns.Add("pay_type_name", Type.GetType("System.String"));
            dc = dt.Columns.Add("pay_type_id", Type.GetType("System.Int32"));
            DataRow newRow;
            newRow = dt.NewRow();
            newRow["pay_type_name"] = "现金支付";
            newRow["pay_type_id"] = 7;
            dt.Rows.Add(newRow);
            return dt;
        }

        public static void processException(int code)
        {
            if (code == -1)
            {
                throw new Exception("无卡");
            }
            else if (code == -2)
            {
                throw new Exception("未知错误和校验错误");
            }
            else if (code == -3)
            {
                throw new Exception("未知异常");
            }
            else if (code == -4)
            {
                throw new Exception("扣款时余额不足");
            }
            else
            {
                throw new Exception("硬件异常");
            }
        }
    }

    /// <summary>
    /// 操作winform配置文件
    /// </summary>
    public static class Config
    {

        public static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            return config.AppSettings.Settings[key].Value;
        }

        /// <summary>
        /// 修改或增加值（保存值）
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public static void SaveValue(string key, string value)
        {
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save(ConfigurationSaveMode.Modified);
        }

        /// <summary>
        /// 删除值
        /// </summary>
        /// <param name="key">key</param>
        public static void DeleteValue(string key)
        {
            config.AppSettings.Settings.Remove(key);
        }

    }
    public enum CardEnum
    {
        new_card = 1,
        read_card = 2,
        recharge_card = 3,
        refund_card = 4
    }

}
