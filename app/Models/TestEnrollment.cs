using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class TestEnrollment
    {
        public int TestId { get; set; }
        public int TestOpportunityId { get; set; }
        public int TesterId { get; set; }
        public string CompletedTest { get; set; }

        public TestOpportunities TestOpportunity { get; set; }
        public Testers Tester { get; set; }
    }
}
