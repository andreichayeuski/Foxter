using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    [Serializable]
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }

        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }
        [ForeignKey("Film")]
        public int FilmId { get; set; }

        public Film Film { get; set; }
        public Cinema Cinema { get; set; }
    }
}
