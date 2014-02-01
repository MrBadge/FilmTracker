using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace FilmTrackerCore
{
    class TrackerSearch
    {
        //private List<string, Action<T>> actionList = new List<string, Action<T>>();
        //private static readonly Regex TableRegex = new Regex("<table.*?</table>", RegexOptions.Singleline);
        //private static readonly Regex lines = new Regex("<tr.*?</tr>", RegexOptions.Singleline);
        //private static readonly Regex cells = new Regex("<td.*?</td>", RegexOptions.Singleline);
        //private static readonly Regex hrefs = new Regex("href=\".*?\"", RegexOptions.Singleline);
        //private static readonly Regex names = new Regex(">.*?</a>", RegexOptions.Singleline);
        private static readonly Regex sz = new Regex(@"\d[.\d]* (GB|MB|TB)");
        //private static readonly Regex TableRegex = new Regex("<table.?*</table>", RegexOptions.Singleline);

        private static List<TrackerTopic> RutorSearch(Film film)
        {
            Uri uri = new Uri("http://rutor.org/search/0/0/300/2/" + film.InfoTable["name"][0] + " BDRemux");
            var htmlCode = Tools.GetURIContent(uri);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlCode);
            var results = htmlDoc.GetElementById("index");
            if (results.InnerText.IndexOf("Результатов поиска") == -1) return null;
            Regex searchResCount = new Regex(@"Результатов поиска \d+");
            var ResCount = searchResCount.Match(results.InnerText);
            if (!ResCount.Success) return null;
            int resultsCount = Convert.ToInt32(ResCount.Value.Substring(ResCount.Value.IndexOf("а ") + 2));
            if (resultsCount == 0)
                return new List<TrackerTopic>();
            //main part
            var magnet_tmp = "";
            var scr_tmp = "";
            var tor_file = "";
            var name_tmp = "";
            var size_tmp = "";
            int found = 0;
            var row = 1;
            List<TrackerTopic> TorFound = new List<TrackerTopic>();
            var table = results.Element("table");
            List<HtmlNode> x = table.Elements("tr").ToList();
            foreach (HtmlNode node in x)
            {
                if (node.Attributes.Contains("class") && node.Attributes["class"].Value == "backgr" ||
                    row == 5)
                    continue;
                List<HtmlNode> s = node.Elements("td").ToList();
                foreach (HtmlNode item in s)
                {
                    switch (row)
                    {
                        case 2:
                        {
                            var hrefs = item.Descendants("a").Where(t => t.Attributes.Contains("href")).ToList();
                            magnet_tmp = hrefs[0].Attributes["href"].Value;
                            scr_tmp = "http://rutor.org" + hrefs[1].Attributes["href"].Value;
                            name_tmp = item.InnerText.Replace("&nbsp;", " ").Trim();
                            break;
                        }
                        case 4:
                        case 3: //if there is no comments
                        {
                            Match szMatch = sz.Match(item.InnerText.Replace("&nbsp;", " "));
                            if (szMatch.Success)
                                size_tmp = item.InnerText.Replace("&nbsp;", " ");
                            break;
                        }
                    }
                    Console.WriteLine(item.InnerText.Replace("&nbsp;", " ")); //DEBUG
                    row++;
                }
                if (IsCorrectFilmTorrent(name_tmp, film))
                {
                    Regex rutogFilmIdRegex = new Regex(@"/torrent/[\d]+/");
                    var rutorFilmIdMatch = rutogFilmIdRegex.Match(scr_tmp);
                    var rutorFilmId = "";
                    if (rutorFilmIdMatch.Success)
                    {
                        rutorFilmId = rutorFilmIdMatch.Value.Substring("/torrent/".Length,
                            rutorFilmIdMatch.Value.Length - "/torrent/".Length - 1);
                        tor_file = "http://d.rutor.org/download/" + rutorFilmId;
                        TorFound.Add(new TrackerTopic(name_tmp, scr_tmp, tor_file, size_tmp, magnet_tmp));
                        found++;
                        scr_tmp = name_tmp = size_tmp = tor_file = magnet_tmp = "";
                    }
                }
                row = 1;
                if (found >= Settings.MAX_SEARCH_RESULT)
                    break;
                Console.WriteLine("-------"); //DEBUG
            }
            return TorFound;
        }

        private static bool IsCorrectFilmTorrent(string name, Film film)
        {
            return (name.Contains(film.InfoTable["name"][0]) && name.Contains(film.InfoTable["year"][0]));
        }

        public static List<TrackerTopic> Search(Film film)
        {
            return RutorSearch(film);
        }
    }
}

/*

            Match tableMatch = TableRegex.Match(htmlCode);
            htmlCode = tableMatch.Value; //htmlCode.Substring(htmlCode.IndexOf("Результатов поиска")); //
            Match rows = lines.Match(htmlCode);
            rows = rows.NextMatch();
            while (rows.Success && found < Settings.MAX_SEARCH_RESULT)
            {
                //Console.WriteLine("-------"); //DEBUG
                Match cell = cells.Match(rows.Value);
                while (cell.Success)
                {
                    Match val = hrefs.Match(cell.Value);
                    while (val.Success)
                    {
                        if (val.Value.Contains("magnet"))
                        {
                            magnet_tmp = val.Value.Substring("href=\"".Length, val.Value.Length - "href=\"".Length - 1);
                        }
                        else
                        {
                            scr_tmp = val.Value.Substring("href=\"".Length, val.Value.Length - "href=\"".Length - 1);
                            Match val2 = names.Match(cell.Value);
                            while (val2.NextMatch().Success == val2.Success)
                                val2 = val2.NextMatch();
                            if (val2.Success)
                                name_tmp = val2.Value.Substring(">".Length, val2.Value.Length - ">".Length - 5);
                        }
                        val = val.NextMatch();
                        //Console.ReadKey();
                    }
                    Match szMatch = sz.Match(cell.Value.Replace("&nbsp;", " "));
                    if (szMatch.Success)
                        size_tmp = szMatch.Value.Substring(">".Length, szMatch.Value.Length - ">".Length - 1);
                    cell = cell.NextMatch();
                }
                if (IsCorrectFilmTorrent(name_tmp, film))
                {
                    Regex rutogFilmIdRegex = new Regex(@"/torrent/[\d]+/");
                    var rutorFilmIdMatch = rutogFilmIdRegex.Match(scr_tmp);
                    var rutorFilmId = "";
                    if (rutorFilmIdMatch.Success)
                    {
                        rutorFilmId = rutorFilmIdMatch.Value.Substring("/torrent/".Length,
                            rutorFilmIdMatch.Value.Length - "/torrent/".Length - 1);
                        tor_file = "http://d.rutor.org/download/" + rutorFilmId;
                        TorFound.Add(new TrackerTopic(name_tmp, scr_tmp, tor_file, size_tmp, magnet_tmp));
                        found++;
                        scr_tmp = name_tmp = size_tmp = tor_file = magnet_tmp = "";
                    }
                }
                rows = rows.NextMatch();
            }
*/