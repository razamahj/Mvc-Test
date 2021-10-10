using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTest.Application.Entities
{
    public class Referral
    {
        public Guid Id { get; set; }
        public DateTime DateOfReferral { get; set; }
        public Service Service { get; set; }
        public Guid ServiceId { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
