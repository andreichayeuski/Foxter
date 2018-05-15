using System;

namespace ClassLibrary
{
    [Serializable]
    public class Cinema
    {
        public string Address { get; set; }
        public int Id { get; set; }
        public string Image_Main { get; set; }
        public string Images { get; set; }
        public string Info { get; set; }
        public string Info_Little { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Schedule { get; set; }
    }
}
