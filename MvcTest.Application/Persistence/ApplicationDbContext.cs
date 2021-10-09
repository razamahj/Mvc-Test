using Microsoft.EntityFrameworkCore;
using MvcTest.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTest.Application.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
