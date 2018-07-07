using System;
using System.Collections.Generic;

namespace app.Models
{
    public partial class FakeTesters
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string IsConfirmed { get; set; }
        public string NoOfTests { get; set; }
        public string PointsEarned { get; set; }
    }
}
