using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class Testers
    {
        public Testers()
        {
            TestEnrollment = new HashSet<TestEnrollment>();
            TestOutcomes = new HashSet<TestOutcomes>();
            TesterProfiles = new HashSet<TesterProfiles>();
        }

        public int TesterId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IsConfirmed { get; set; }

        public ICollection<TestEnrollment> TestEnrollment { get; set; }
        public ICollection<TestOutcomes> TestOutcomes { get; set; }
        public ICollection<TesterProfiles> TesterProfiles { get; set; }
    }
}
