﻿using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class TestOpportunities
    {
        public TestOpportunities()
        {
            TestEnrollment = new HashSet<TestEnrollment>();
        }

        public int TestOpportunityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AgencyId { get; set; }
        public string TestUrl { get; set; }
        public DateTime LastUpdate { get; set; }

        public Agencies Agency { get; set; }
        public ICollection<TestEnrollment> TestEnrollment { get; set; }
    }
}
