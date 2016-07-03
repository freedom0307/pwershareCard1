using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CardInfo
    {
        /// <summary>
        /// 卡号
        /// </summary>
        private string card_no;

        public string Card_no
        {
            get { return card_no; }
            set { card_no = value; }
        }

        /// <summary>
        /// 金额
        /// </summary>
        private UInt32 money;

        public UInt32  Money
        {
            get { return money; }
            set { money = value; }
        }

        /// <summary>
        /// 代码：0成功 -1失败
        /// </summary>
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        private string user_name;

        public string User_name
        {
            get { return user_name; }
            set { user_name = value; }
        }

        /// <summary>
        /// 手机号
        /// </summary>
        private string mobile;

        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
    }
}
