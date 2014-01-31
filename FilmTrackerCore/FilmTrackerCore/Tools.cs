using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FilmTrackerCore
{
    class Tools
    {
        //FOR DEBUG!!
        public static void Save(string str, string filename)
        {
            var f = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write),
                Encoding.GetEncoding("windows-1251"));
            f.WriteLine(str);
            f.Close();
        }

        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string GetURIContent(Uri uri, string encoding= "")
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.UserAgent = Constants.GetRandUserAgent();
            request.Referer = "http://www.google.com";
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            if (dataStream == null) return "";
            //var reader = new StreamReader(dataStream); //, Encoding.GetEncoding(encoding)
            //var msg = reader.ReadToEnd();
            byte[] bytes = ReadFully(dataStream);//Encoding.Default.GetBytes(msg);
            var msg = Encoding.UTF8.GetString(bytes);
            Encoding enc = Encoding.Default;
            if (encoding != "")
            {
                enc = Encoding.GetEncoding(encoding);
                //Encoding win1251 = Encoding.GetEncoding(encoding);
                //byte[] win1251Bytes = win1251.GetBytes(msg);
                //byte[] uniBytes = Encoding.Convert(win1251, Encoding.Unicode, win1251Bytes);   
            }
            else
            {
                try
                {
                    Regex charsetRegex = new Regex(@"charset=[A-Za-z]+[\-\d]*");
                    var tmp = charsetRegex.Match(msg);
                    if (tmp.Success)
                    {
                        var charset = tmp.Value.Substring(tmp.Value.IndexOf("=") + 1);
                        enc = Encoding.GetEncoding(charset);
                    }
                }
                catch (Exception)
                {
                    enc = Encoding.GetEncoding(encoding);    
                }
            }
            msg = enc.GetString(bytes);
            //Save(msg, "test.html");
            return msg;
        }
        
        public static bool IsSiteAvailable(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            request.Method = "GET";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var tmp = GetURIContent(new Uri(uri), "utf-8");
                return !tmp.Contains("http://www.zapret-info.gov.ru/") || !(response == null || response.StatusCode != HttpStatusCode.OK);
            }
            catch (WebException wex)
            {
                return false;
            }
        }
    }
}
