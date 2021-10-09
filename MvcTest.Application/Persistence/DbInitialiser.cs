using MvcTest.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTest.Application.Persistence
{
    public static class DbInitialiser
    {
        public static void Initailise(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Database.EnsureCreated();

            if (applicationDbContext.Services.Any()) return;

            var users = new User[] {
                new User { Id = Guid.NewGuid(), Forename = "Manager", Surname = "One", EmailAddress = "manager.one@domain.com"  },
                new User { Id = Guid.NewGuid(), Forename = "Manager", Surname = "Two", EmailAddress = "manager.two@domain.com"  },
            };

            applicationDbContext.Users.AddRange(users);

            var services = new Service[] {
                new Service { Id = Guid.NewGuid(), Name = "Service One", Manager = users[0], ManagerId = users[0].Id },
                new Service { Id = Guid.NewGuid(), Name = "Service Two", Manager = users[1], ManagerId = users[1].Id },
            };

            applicationDbContext.Services.AddRange(services);

            applicationDbContext.SaveChanges();
        }
    }
}
