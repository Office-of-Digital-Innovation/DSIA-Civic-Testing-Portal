using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class DimTime
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Hour { get; set; }
        public string MilitaryHour { get; set; }
        public string Minute { get; set; }
        public string Second { get; set; }
        public string AmPm { get; set; }
        public string StandardTime { get; set; }
    }
}
