using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmTrackerCore
{
    class Program
    {
        static void Main(string[] args)
        {
            string site1 = "http://rutor.org";
            string site2 = "http://opensharing.org";
            string site3 = "http://kinopoisk.ru";
            //Tools.GetURIContent(new Uri(site3));
            //Console.WriteLine(Tools.IsSiteAvailable(site));
            //Film film = new Film("The Wolf of Wall Street");
            //Film film2 = new Film("Железный человек 2", "2010");
            Film film3 = new Film("Титаник");
            TrackerSearch.Search(film3);
            //KinopoiskSearch.GetFilmID("Iron Man", "2013");
            Console.ReadKey();
        }
    }
}
