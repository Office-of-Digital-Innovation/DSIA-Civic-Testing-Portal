using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class AgencyProfiles
    {
        public int AgencyProfileId { get; set; }
        public int AgencyId { get; set; }
        public string AgencySection { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
