using System;

namespace Dojo
{
    public class Concert
    {
        public string Artist { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public string Date { get; set; } // should be a DateTime (time constraints)
        public string Price { get; set; } // Should be a decimal (time constraints)
    }
}