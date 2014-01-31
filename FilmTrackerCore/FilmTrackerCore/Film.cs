using System;
using System.Collections;
using System.Collections.Generic;

namespace FilmTrackerCore
{
    internal class Film
    {
        public int Id { get; set; }
        private DateTime DVD { get; set; }
        private DateTime BluRay { get; set; }
        private DateTime premiereWorld { get; set; }
        private DateTime premiereRus { get; set; }
        public Dictionary<string, List<string>> InfoTable;

        public Film(string filmName, string filmYear = "")
        {
            Id = KinopoiskSearch.GetFilmID(filmName, filmYear);
            InfoTable = new Dictionary<string, List<string>>();
            KinopoiskSearch.GetFilmInfo(this);
            if (InfoTable.ContainsKey("премьера (мир)"))
            {
                premiereWorld = DateTime.Parse((string) InfoTable["премьера (мир)"][0]);
            }
            if (InfoTable.ContainsKey("премьера (РФ)"))
            {
                premiereRus = DateTime.Parse((string) InfoTable["премьера (РФ)"][0]);
            }
            if (InfoTable.ContainsKey("релиз на DVD"))
            {
                DVD = DateTime.Parse((string) InfoTable["релиз на DVD"][0]);
            }
            if (InfoTable.ContainsKey("релиз на Blu-Ray"))
            {
                BluRay = DateTime.Parse((string) InfoTable["релиз на Blu-Ray"][0]);
            }
            //Console.ReadKey(); //DEBUG
        }
    }
}