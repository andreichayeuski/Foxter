using System;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    [Serializable]
    public class Comment
    {

        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }
        
        public string Date { get; set; }

        public string Place { get; set; }
        
        public string Theme { get; set; }
    }
}
