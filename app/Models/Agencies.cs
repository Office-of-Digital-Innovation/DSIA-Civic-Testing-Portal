using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class Agencies
    {
        public Agencies()
        {
            TestOpportunities = new HashSet<TestOpportunities>();
        }

        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string AgencyUrl { get; set; }

        public ICollection<TestOpportunities> TestOpportunities { get; set; }
    }
}
