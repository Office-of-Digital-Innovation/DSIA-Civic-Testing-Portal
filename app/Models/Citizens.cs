using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class Citizens
    {
        public Citizens()
        {
            CitizenProfiles = new HashSet<CitizenProfiles>();
            TestEnrollment = new HashSet<TestEnrollment>();
            TestOutcomes = new HashSet<TestOutcomes>();
        }

        public int CitizenId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IsConfirmed { get; set; }

        public ICollection<CitizenProfiles> CitizenProfiles { get; set; }
        public ICollection<TestEnrollment> TestEnrollment { get; set; }
        public ICollection<TestOutcomes> TestOutcomes { get; set; }
    }
}
