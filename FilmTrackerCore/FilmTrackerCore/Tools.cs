using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FilmTrackerCore
{
    class Tools
    {
        public static string GetURIContent(Uri uri, string encoding)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.UserAgent = Constants.GetRandUserAgent();
            request.Referer = "http://www.google.com";
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream, Encoding.GetEncoding(encoding));
            Encoding win1251 = Encoding.GetEncoding(encoding);
            byte[] win1251Bytes = win1251.GetBytes(reader.ReadToEnd());
            byte[] uniBytes = Encoding.Convert(win1251, Encoding.Unicode, win1251Bytes);
            string msg = Encoding.Unicode.GetString(uniBytes);
            return msg;
        }
        
        public static bool CheckSiteAvailability(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
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
