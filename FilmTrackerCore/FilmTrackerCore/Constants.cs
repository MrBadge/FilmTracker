using System;
using System.Collections;
using System.Collections.Generic;

namespace FilmTrackerCore
{
    internal class Constants
    {
        private static readonly ArrayList User_Agents = new ArrayList
        {
            //linux
            "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.8.0.7) Gecko/20060928 (Debian|Debian-1.8.0.7-1) Epiphany/2.14",
            "Mozilla/5.0 (compatible; Konqueror/4.3; Linux) KHTML/4.3.5 (like Gecko)",
            "Lynx/2.8.6rel.4 libwww-FM/2.14 SSL-MM/1.4.1 OpenSSL/0.9.8g",
            "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.1.9) Gecko/20100318 Mandriva/2.0.4-69.1mib2010.0 SeaMonkey/2.0.4",
            "Opera/9.80 (X11; Linux i686; U; en) Presto/2.9.168 Version/11.52",
            "Mozilla/5.0 (X11; Linux i686; rv:5.0.1) Gecko/20100101 Firefox/5.0.1",
            "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.12 Safari/535.11",
            "Mozilla/5.0 (X11; U; Linux x86_64; en-US) AppleWebKit/534.7 (KHTML, like Gecko) Epiphany/2.30.6 Safari/534.7",
            "Mozilla/5.0 (X11; U; Linux x86; en-US) AppleWebKit/534.7 (KHTML, like Gecko) Epiphany/2.30.6 Safari/534.7",
            "Mozilla/5.0 (X11; U; OpenBSD arm; en-us) AppleWebKit/531.2+ (KHTML, like Gecko) Safari/531.2+ Epiphany/2.30.0",
            "Mozilla/5.0 (X11; U; FreeBSD amd64; en-us) AppleWebKit/531.2+ (KHTML, like Gecko) Safari/531.2+ Epiphany/2.30.0",
            "Mozilla/5.0 (X11; Linux i686; rv:6.0) Gecko/20100101 Firefox/6.0",
            "Mozilla/5.0 (X11; U; Linux amd64; rv:5.0) Gecko/20100101 Firefox/5.0 (Debian)",
            "Opera/9.80 (X11; Linux x86_64; U; en) Presto/2.9.168 Version/11.50",
            "Opera/9.80 (X11; Linux i686; U; en) Presto/2.8.131 Version/11.11",
            "Opera/9.80 (X11; Linux x86_64; U; Ubuntu/10.10 (maverick); en) Presto/2.7.62 Version/11.01",
            "Opera/9.80 (X11; Linux i686; U; en) Presto/2.7.62 Version/11.00",

            //MacOs
            "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_7; en-US) AppleWebKit/534.16 (KHTML, like Gecko) Chrome/10.0.648.205 Safari/534.16",
            "Opera/9.80 (Macintosh; Intel Mac OS X 10.6.7; U; en) Presto/2.8.131 Version/11.10",
            "Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en) AppleWebKit/124 (KHTML, like Gecko) Safari/125",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:9.0a2) Gecko/20111101 Firefox/9.0a2",
            "Opera/9.80 (Macintosh; Intel Mac OS X 10.6.8; U; en) Presto/2.9.168 Version/11.52",
            "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_8; en-US) AppleWebKit/533.21.1 (KHTML, like Gecko) Version/5.0.5 Safari/533.21.1",
            "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_6; en-us) AppleWebKit/533.20.25 (KHTML, like Gecko) Version/5.0.4 Safari/533.20.27",

             //Windows
            "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",
            "Mozilla/5.0 (Windows; I; Windows NT 5.1; en; rv:1.9.2.13) Gecko/20100101 Firefox/4.0",
            "Opera/9.80 (Windows NT 6.1; U; en) Presto/2.8.131 Version/11.10",
            "Opera/9.80 (Windows NT 6.1; U; en) Presto/2.8.131 Version/11.52",
            "Mozilla/5.0 (Windows; I; Windows NT 5.1; en; rv:1.9.2.13) Gecko/20100101 Firefox/5.0.1",
            "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/18.6.872.0 Safari/535.2 UNTRUSTED/1.0 3gpp-gba UNTRUSTED/1.0",
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.8 (KHTML, like Gecko) Chrome/17.0.940.0 Safari/535.8",
            "Mozilla/5.0 (Windows NT 6.0; WOW64) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.75 Safari/535.7",
            "Mozilla/5.0 (Windows NT 6.0) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.75 Safari/535.7",
            "Mozilla/5.0 (Windows NT 6.2; rv:9.0.1) Gecko/20100101 Firefox/9.0.1",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:6.0a2) Gecko/20110613 Firefox/6.0a2",
            "Mozilla/5.0 (Windows NT 6.1; rv:6.0) Gecko/20110814 Firefox/6.0",
            "Mozilla/5.0 (compatible; MSIE 10.6; Windows NT 6.1; Trident/5.0; InfoPath.2; SLCC1; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 2.0.50727) 3gpp-gba UNTRUSTED/1.0",
            "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)",
            "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)",
            "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/5.0)",
            "Mozilla/4.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/5.0)",
            "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)",
            "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Win64; x64; Trident/5.0; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET CLR 2.0.50727; Media Center PC 6.0)",
            "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Win64; x64; Trident/5.0",
            "Opera/9.80 (Windows NT 6.1; U; en-US) Presto/2.9.181 Version/12.00",
            "Opera/9.80 (Windows NT 5.1; U; en) Presto/2.9.168 Version/11.51",
            "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; en) Opera 11.51",
            "Mozilla/5.0 (Windows NT 5.1; U; en; rv:1.8.1) Gecko/20061208 Firefox/5.0 Opera 11.11",
            "Opera/9.80 (Windows NT 6.1; U; en-US) Presto/2.7.62 Version/11.01",
            "Opera/9.80 (Windows NT 6.1 x64; U; en) Presto/2.7.62 Version/11.00",
            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; en) Opera 11.00",
            "Opera/9.80 (Windows NT 6.1; U; en) Presto/2.6.31 Version/10.70",
            "Mozilla/5.0 (Windows NT 5.1; U; en-US; rv:1.9.1.6) Gecko/20091201 Firefox/3.5.6 Opera 10.70",
            "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; en) Opera 10.62",
            "Opera/9.80 (Windows NT 6.1; U; en) Presto/2.6.30 Version/10.61",
            "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/533.20.25 (KHTML, like Gecko) Version/5.0.4 Safari/533.20.27",
            "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US) AppleWebKit/533.20.25 (KHTML, like Gecko) Version/5.0.3 Safari/533.19.4",
            "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/533.20.25 (KHTML, like Gecko) Version/5.0.3 Safari/533.19.4",
            "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/533.19.4 (KHTML, like Gecko) Version/5.0.2 Safari/533.18.5"
        };

        internal class Quality
        {
            private ArrayList names;
            private string Description;

            public Quality(ArrayList names, string Description)
            {
                this.names = names;
                this.Description = Description;
            }
        }

        public static readonly ArrayList VeryLowQuality = new ArrayList()
        {
            new Quality(new ArrayList()
            {
                "CAMRip",
                "CAM"
            },
                "фильм, отснятый с экрана зала кинотеатра при помощи обычной камеры без точной синхронизации с проектором. Самое низкое качество из всех возможных. В некоторых фильмах видны головы других кинозрителей. Качество звука никуда не годится, возможны помехи в виде смеха публики или постороннего разговора."),
            new Quality(new ArrayList()
            {
                "TS",
                "TeleSync"
            },
                "запись профессиональной (цифровой) камерой, установленной на штатив в пустом кинотеатре с экрана. Съёмка производится с синхронизацией, при которой камера получает от проекционного оборудования сигнал, позволяющий точно определить момент полного открытия обтюратора проектора. Качество чуть лучше, чем с простой камеры. Звук записывается напрямую с проектора или с другого отдельного выхода, например, гнездо для наушников в кресле (как в самолёте). Звук, записанный таким образом, получается более-менее приемлемого качества и без помех. Как правило, звук в режиме стерео.")
        };

        /*public static readonly Dictionary<string, string> VeryLowQuality = new Dictionary<string, string>()
        {
            {"CAMRip", ""},
            {"TS", ""}
        };*/

        public static readonly Dictionary<string, string> Monthes = new Dictionary<string, string>()
        {
            {"января", "January"},
            {"февраля", "February"},
            {"марта", "March"},
            {"апреля", "Aplril"},
            {"мая", "May"},
            {"июня", "June"},
            {"июля", "July"},
            {"августа", "August"},
            {"сентября", "September"},
            {"октября", "October"},
            {"ноября", "November"},
            {"декабря", "December"}
        };

        public static readonly Dictionary<string, string> KeyWords = new Dictionary<string, string>()
        {
            {"название", "name"},
            {"постер", "poster"},
            {"год", "year"},
            {"страна", "country"},
            {"слоган", "motto"},
            {"режиссер", "director"},
            {"сценарий", "scenario"},
            {"продюсер", "producer"},
            {"оператор", "cameraman"},
            {"композитор", "composer"},
            {"художник", "film artist"},
            {"монтаж", "editing"},
            {"жанр", "genre"},
            {"бюджет", "budget"},
            {"премьера (мир)", "premiere (world)"},
            {"премьера (РФ)", "premiere (Russia)"},
            {"релиз на Blu-Ray", "Blu-Ray"},
            {"релиз на DVD", "DVD"},
            {"время", "duration"}
        }; 

        public static string GetRandUserAgent()
        {
            var rnd = new Random();
            return (string)User_Agents[rnd.Next(User_Agents.Count)];
        }

    }
}