using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace FilmTrackerCore
{
    internal class KinopoiskSearch
    {
        //private static readonly Regex lines = new Regex("<tr.*?</tr>", RegexOptions.Singleline);
        //private static readonly Regex cells = new Regex("<td.*?</td>", RegexOptions.Singleline);
        //private static readonly Regex celVal = new Regex(">.*?<", RegexOptions.Singleline);
        //private static readonly Regex for_name = new Regex(">.*?<", RegexOptions.Multiline);

        /*private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }*/

        public static int GetFilmID(string movieName, string movieYear)
        {
            var uri = new Uri("http://www.kinopoisk.ru/s/type/film/find/" + movieName + "/m_act%5Byear%5D/" +
                              movieYear + '/');
            return ParseID(Tools.GetURIContent(uri));
        }

        public static void GetFilmInfo(Film film)
        {
            if (film.Id == -1)
                return;
            var uri = new Uri("http://www.kinopoisk.ru/film/" + Convert.ToString(film.Id) + '/');
            string HTMLCode = Tools.GetURIContent(uri);
            ParseInfoTable(HTMLCode, film);
        }

        private static string MakeValueClear(string val)
        {
            var tmp = val /*.Substring(1, val.Length - 2)*/
                .Replace("\n", " ")
                .Replace("&laquo;", "\"")
                .Replace("&raquo;", "\"")
                .Replace("&nbsp;", " ")
                .Replace("...", "")
                .Replace("слова", "")
                .Replace("сборы", "")
                /*.Replace(",", " ")*/
                .Trim();
            while (tmp.IndexOf("  ") != -1)
                tmp = tmp.Replace("  ", " ");
            if (tmp[tmp.Length - 1] == ',')
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }

        private static void ClearHTMLCode(HtmlNode doc)
        {
            doc.Descendants()
                .Where(
                    n => n.Name == "script" || n.Name == "style" || n.NodeType == HtmlNodeType.Comment)
                .ToList()
                .ForEach(n => n.Remove());
        }

        private static void ParseInfoTable(string htmlCode, Film film)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlCode);
            var names = htmlDoc.GetElementById("headerFilm");
            if (names == null) return;
            var _names = names.Descendants()
                .Where(t => t.Attributes.Contains("itemprop")).ToArray();
            film.InfoTable.Add("name",
                new List<string>()
                {
                    MakeValueClear(_names[0].InnerText),
                    MakeValueClear(_names[1].InnerText)
                });
            SetPosterInfo(film);
            var table = htmlDoc.GetElementById("infoTable");
            ClearHTMLCode(table);
            table = table.Element("table");
            string tmp = "";
            bool key = true;
            string cur_key = "";
            List<HtmlNode> x = table.Elements("tr").ToList();
            foreach (HtmlNode node in x)
            {
                List<HtmlNode> s = node.Elements("td").ToList();
                foreach (HtmlNode item in s)
                {
                    tmp = MakeValueClear(item.InnerText);
                    if (!String.IsNullOrWhiteSpace(tmp))
                    {
                        if (key && Constants.KeyWords.ContainsKey(tmp))
                        {
                            film.InfoTable.Add(Constants.KeyWords[tmp], new List<string>());
                            cur_key = tmp;
                            key = false;
                        }
                        else
                        {
                            if (!key)
                            {
                                if (cur_key != "слоган")
                                {
                                    var Arr_tmp = tmp.Split(',');
                                    foreach (var i in Arr_tmp)
                                    {
                                        film.InfoTable[Constants.KeyWords[cur_key]].Add(i.Trim());
                                    }
                                }
                                else
                                    film.InfoTable[Constants.KeyWords[cur_key]].Add(tmp);
                            }
                        }
                    }
                    Console.WriteLine(tmp); //DEBUG
                }
                Console.WriteLine("-------"); //DEBUG
                key = true;
            }         
            //Console.ReadKey();
        }

        private static void SetPosterInfo(Film film)
        {
            film.InfoTable.Add("poster", new List<string>()
            {
                "www.kinopoisk.ru/images/film_big/" + film.Id + ".jpg",
                "www.kinopoisk.ru/images/film/" + film.Id + ".jpg"
            });
        }

        private static int ParseID(string htmlCode)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlCode);
            var results = htmlDoc.DocumentNode
                .Descendants("div")
                .Where(x => x.Attributes.Contains("class") &&
                            x.Attributes["class"].Value.Contains("element most_wanted")).ToArray();
            if (results.Length != 1) return -1;
            var hrefs = results[0].Descendants("a").Where(x => x.Attributes.Contains("href")).ToArray();
            var r = new Regex(@"\/film\/(\d)+\/");
            Match tmp = r.Match(hrefs[0].GetAttributeValue("href", "nop"));
            if (!tmp.Success) return -1;
            string preId = tmp.Value.Substring("/film/".Length, tmp.Value.Length - "/film/".Length - 1);
            return Convert.ToInt32(preId);
        }
    }
}

//Search for pair </div> tag
/*int divCount = 0;
var pos = 0;
while (pos < htmlCode.Length - 4)
{
    var tmp = htmlCode[pos].ToString() + htmlCode[pos + 1].ToString() + htmlCode[pos + 2].ToString() +
              htmlCode[pos + 3].ToString();
    if (tmp == "<div")
    {
        divCount++;
        //pos += 3;
    }
    else
    {
        if (tmp + htmlCode[pos + 4].ToString() == "</div")
        {
            divCount--;
            //pos += 4;
        }    
    }
    if (divCount == 0)
        break;
    pos++;
}
pos += 2;*/
//Save(htmlCode, "testInfo.html");


/*
bool correct = (htmlCode.IndexOf("<div id=\"infoTable\">") != -1);
            if (!correct) return;
            htmlCode = htmlCode.Substring(htmlCode.IndexOf("class=\"moviename-big\" itemprop=\"name\""));
            Match name_tmp = for_name.Match(htmlCode);
            film.InfoTable.Add("name",
                new List<string>()
                {
                    MakeValueClear(name_tmp.Value),
                    MakeValueClear(name_tmp.NextMatch().Value)
                });
            SetPosterInfo(film);
            Console.WriteLine(film.InfoTable["name"][0]); //DEBUG
            htmlCode = htmlCode.Substring(htmlCode.IndexOf("<div id=\"infoTable\">"));
            htmlCode = htmlCode.Substring(htmlCode.IndexOf("<table class=\"info\">"));
            htmlCode = htmlCode.Substring(0, htmlCode.IndexOf("</table>") + "</table>".Length);

            
            
            
            Match rows = lines.Match(htmlCode);
            while (rows.Success)
            {
                Console.WriteLine("-------"); //DEBUG
                Match cell = cells.Match(rows.Value);
                while (cell.Success)
                {
                    Match val = celVal.Match(cell.Value);
                    while (val.Success)
                    {
                        tmp = MakeValueClear(val.Value);
                        if (!String.IsNullOrWhiteSpace(tmp) && tmp.IndexOf("var") == -1 && tmp != "слова" &&
                            tmp != "сборы" && tmp != "\"")
                        {
                            if (key && Constants.KeyWords.ContainsKey(tmp))
                            {
                                film.InfoTable.Add(Constants.KeyWords[tmp], new List<string>());
                                cur_key = tmp;
                                key = false;
                            }
                            else
                            {
                                if (!key)
                                {
                                    film.InfoTable[Constants.KeyWords[cur_key]].Add(tmp);
                                }
                            }
                            Console.WriteLine(tmp); //DEBUG
                        }
                        val = val.NextMatch();
                    }
                    cell = cell.NextMatch();
                }
                key = true;
                rows = rows.NextMatch();
            }
*/