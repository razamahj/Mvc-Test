using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTest.Application.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ContactTelephoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public ICollection<Referral> Referrals { get; set; }
    }
}
