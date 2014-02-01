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
            if (InfoTable.ContainsKey("premiere (world)"))
            {
                premiereWorld = DateTime.Parse(InfoTable["premiere (world)"][0]);
            }
            if (InfoTable.ContainsKey("premiere (Russia)"))
            {
                premiereRus = DateTime.Parse(InfoTable["premiere (Russia)"][0]);
            }
            if (InfoTable.ContainsKey("DVD"))
            {
                DVD = DateTime.Parse(InfoTable["DVD"][0]);
            }
            if (InfoTable.ContainsKey("Blu-Ray"))
            {
                BluRay = DateTime.Parse(InfoTable["Blu-Ray"][0]);
            }
            //Console.ReadKey(); //DEBUG
        }
    }
}