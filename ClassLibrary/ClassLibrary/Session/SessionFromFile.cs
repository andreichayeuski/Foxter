using System;

namespace ClassLibrary
{
    [Serializable]
    public class SessionFromFile
    {
        public string Time { get; set; }
        public string Date { get; set; }
        public string FilmLink { get; set; }
        public string CinemaLink { get; set; }
    }
}
