using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class TestOutcomes
    {
        public int TestOutcomeId { get; set; }
        public int TesterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TestResultUrl { get; set; }
        public DateTime LastUpdate { get; set; }

        public Testers Tester { get; set; }
    }
}
