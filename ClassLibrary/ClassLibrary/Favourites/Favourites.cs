using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    [Serializable]
    public class Favourites
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public User User { get; set; }

        public List<Film> Films { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Concert> Concerts { get; set; }
        public List<Exhibition> Exhibitions { get; set; }

        public Favourites()
        {
            this.Films = new List<Film>();
            this.Cinemas = new List<Cinema>();
            this.Concerts = new List<Concert>();
            this.Exhibitions = new List<Exhibition>();
        }
    }
}