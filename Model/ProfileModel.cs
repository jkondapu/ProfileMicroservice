using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMicroservice.Model
{
    public class ProfileModel
    {
        public string ProfileId { get; set; }
        public string IndividualId { get; set; }
        public string HouseholdId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string AccountSourceCode { get; set; }
        public string SourceAccountNumber { get; set; }
        public string BrandOrgCode { get; set; }
        public string ActivityDate { get; set; }
        public string IsTestProfile { get; set; }
        public string Country { get; set; }
    }
}
