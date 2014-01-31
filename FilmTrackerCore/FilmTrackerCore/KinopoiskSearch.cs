using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace FilmTrackerCore
{
    internal class KinopoiskSearch
    {
        private static readonly Regex lines = new Regex("<tr.*?</tr>", RegexOptions.Singleline);
        private static readonly Regex cells = new Regex("<td.*?</td>", RegexOptions.Singleline);
        private static readonly Regex celVal = new Regex(">.*?<", RegexOptions.Singleline);
        private static readonly Regex for_name = new Regex(">.*?<", RegexOptions.Multiline);

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
            return val.Substring(1, val.Length - 2)
                .Replace("\n", " ")
                .Replace("&laquo;", "\"")
                .Replace("&raquo;", "\"")
                .Replace("&nbsp;", " ")
                .Replace("...", "")
                .Replace(",", " ")
                .Trim()
                .Replace("  ", ", ");
        }

        private static void ParseInfoTable(string htmlCode, Film film)
        {
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

            bool key = true;
            string cur_key = "";
            string tmp = "";
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
            bool correct = (htmlCode.IndexOf("element most_wanted") != -1);
            if (!correct) return -1;
            htmlCode = htmlCode.Substring(htmlCode.IndexOf("element most_wanted"));
            var r = new Regex(@"\/film\/(\d)+\/");
            Match tmp = r.Match(htmlCode);
            int index = tmp.Value.IndexOf("/film/") + "/film/".Length;
            string preId = tmp.Value.Substring(index, tmp.Value.Length - index - 1);
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