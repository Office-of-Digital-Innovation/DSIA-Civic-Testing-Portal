using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class Organizations
    {
        public Organizations()
        {
            TestOpportunities = new HashSet<TestOpportunities>();
        }

        public int OrganizationId { get; set; }
        public string OrgName { get; set; }
        public string OrgUrl { get; set; }

        public ICollection<TestOpportunities> TestOpportunities { get; set; }
    }
}
