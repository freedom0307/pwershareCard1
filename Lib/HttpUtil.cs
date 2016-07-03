using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Lib
{
    public class HttpUtil
    {
        public static string CreateHttpResponse(string url, string postData, string code = "utf-8")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            byte[] bs = Encoding.ASCII.GetBytes(postData);
            string responseData = String.Empty;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bs.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Close();
            }

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(code)))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                return e.Message;
            }
            finally
            {
                if (response != null) response.Close();
            }
        }

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}
