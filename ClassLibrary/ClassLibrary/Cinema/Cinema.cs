using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    [Serializable]
    public class Cinema
    {
        public string Address { get; set; }
        [Key]
        public int Id { get; set; }
        public string Image_Main { get; set; }
        public string Images { get; set; }
        public string Info { get; set; }
        public string Info_Little { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Schedule { get; set; }
        
        public Favorites Favorites { get; set; }
        public int? FavoritesId { get; set; }

        public List<Session> Sessions { get; set; }

        public Cinema()
        {
            this.Sessions = new List<Session>();
        }
    }
}
