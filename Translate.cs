using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication12
{
    public class Translate
    {
        private String text;
        private String type;

        public Translate(String text, String type)
        {
            this.text = text;
            this.type = type;
        }

        public String sendAndGet()
        {
            String q = text;
            q = Encoding.Default.GetString(Encoding.UTF8.GetBytes(q));
            String from = "auto";
            String to = type;
            long appid; // your baidu Translate appid
            String key ; // your baidu Translate secret key 
            //apply :http://api.fanyi.baidu.com/api/trans/product/index
            long salt = 1234567890;
            String tmp = appid + q + salt + key;
            String sign = MD5(tmp);
            if (type.Equals("en"))
            {
                q = UrlEncode(q).ToUpper();
            }
            String p = "q=" + q + "&from=" + from + "&to=" + to + "&appid=" + appid +
                "&salt=" + salt + "&sign=" + sign;
            tmp = send(p);
            int pre = tmp.IndexOf("dst\":\"") + "dst\":\"".Length;
            int nex = tmp.IndexOf("\"}", pre);
            tmp = tmp.Substring(pre, nex - pre);
            try
            {
                return unicodeDecode(tmp);
            }
            catch (Exception ) {
                return tmp;
            }
            
        }

        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.Default.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }

        public String unicodeDecode(String text)
        {

            string sOut = "";//转换后
            string[] arr = text.Split(new string[] { "\\u" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in arr)
            {
                sOut += (char)Convert.ToInt32(s.Substring(0, 4), 16) + s.Substring(4);
            }
            return sOut;
        }
        public String send(String p)
        {
            HttpWebRequest request = HttpWebRequest.Create("http://api.fanyi.baidu.com/api/trans/vip/translate?" + p)
                as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }
        public String MD5(String text)
        { //32位MD5文本签名
            MD5 MD5 = new MD5CryptoServiceProvider();

            Byte[] bytes = MD5.ComputeHash(Encoding.Default.GetBytes(text));
            String sign = "";
            foreach (var i in bytes)
            {
                sign += i.ToString("x2");
            }
            return sign;
        }

      

    }
}
