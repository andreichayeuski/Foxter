using System;

namespace ClassLibrary
{
    [Serializable]
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image_Main { get; set; }
        public string Age { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string Country { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }
        public string Info { get; set; }
        public string Video { get; set; }
        public string Images { get; set; }

        public Film()
        {

        }
    }
}
