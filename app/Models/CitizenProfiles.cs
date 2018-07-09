using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class CitizenProfiles
    {
        public int CitizenProfileId { get; set; }
        public int CitizenId { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Password { get; set; }

        public Citizens Citizen { get; set; }
    }
}
