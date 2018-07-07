using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class DimDate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public string DaySuffix { get; set; }
        public string DayOfWeek { get; set; }
        public byte DowinMonth { get; set; }
        public int DayOfYear { get; set; }
        public byte WeekOfYear { get; set; }
        public byte WeekOfMonth { get; set; }
        public string Month { get; set; }
        public string MonthName { get; set; }
        public byte Quarter { get; set; }
        public string QuarterName { get; set; }
        public string Year { get; set; }
        public string StandardDate { get; set; }
        public string HolidayText { get; set; }
    }
}
