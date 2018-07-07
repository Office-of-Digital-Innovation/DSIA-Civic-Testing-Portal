using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class TesterProfiles
    {
        public int TesterProfileId { get; set; }
        public int TesterId { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Password { get; set; }

        public Testers Tester { get; set; }
    }
}
