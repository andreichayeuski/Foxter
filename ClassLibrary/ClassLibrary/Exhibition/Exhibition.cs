using System;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    [Serializable]
    public class Exhibition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image_Main { get; set; }
        public string Address { get; set; }
        public string Event_Date { get; set; }
        public string Info { get; set; }
        public string Images { get; set; }

        public Favourites Favourites { get; set; }
        public int? FavouritesId { get; set; }
    }
}
