using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTest.Application.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ManagerId { get; set; }
        public User Manager { get; set; }
    }
}
