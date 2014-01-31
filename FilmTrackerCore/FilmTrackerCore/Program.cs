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
            string site = "http://rutor.org";
            Console.WriteLine(Tools.CheckSiteAvailability(site));
            Console.ReadKey();
            //Film film = new Film("The Wolf of Wall Street");
            //Film film2 = new Film("Железный человек 2", "2010");
            //Film film3 = new Film("Титаник");
            //KinopoiskSearch.GetFilmID("Iron Man", "2013");
        }
    }
}
