using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MvcTest.Application.Entities;
using MvcTest.Application.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace MvcTest.Controllers
{
    public class ReferralController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext dbContext;

        public ReferralController(ILogger<HomeController> logger, ApplicationDbContext _dbContext)
        {
            _logger = logger;
            this.dbContext = _dbContext;
          
        }

        public IActionResult List()
        {
            var referralList = new List<ReferralModel>();
            foreach(var item in dbContext.Referrals)
            {
                item.Client = dbContext.Clients.First(c => c.Id == item.ClientId);
                item.Service = dbContext.Services.First(c => c.Id == item.ServiceId);
                var objReferral = new ReferralModel()
                {
                    Forename = item.Client.Forename,
                    Surname = item.Client.Surname,
                    EmailAddress = item.Client.EmailAddress,
                    ContactTelephoneNumber = item.Client.ContactTelephoneNumber,
                    ServiceName = item.Service.Name,
                    DOR = item.DateOfReferral,
                    DateOfBirth = item.Client.DateOfBirth.Value,
                };
                referralList.Add(objReferral);
            }
            return View(referralList);
        }

        public IActionResult Create()
        {
            ViewData["Under18"] = false;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReferralModel referral)
        {
            var objClient = new Client();
            var objService = new Service();
            var objReferral = new Referral();

            objClient.Forename = referral.Forename;
            objClient.DateOfBirth = referral.DateOfBirth;
            objClient.Surname = referral.Surname;
            objClient.EmailAddress = referral.EmailAddress;
            objClient.ContactTelephoneNumber = referral.ContactTelephoneNumber;
       
           // objReferral.Service = referral.Service;
            objReferral.Client = objClient;
       
            dbContext.Clients.Add(objClient);
            //dbContext.SaveChanges();

            objService = dbContext.Services.First(s => s.Name == referral.ServiceName);
            objReferral.Client = objClient;
            objReferral.Service = objService;


            if (referral.DateOfBirth > DateTime.Now.AddYears(-18))
            {
                ViewData["Under18"] = true;
                return View();
            }
            else
            {
                ViewData["Under18"] = false;
            }
            
            objReferral.DateOfReferral = DateTime.Now;
            dbContext.Referrals.Add(objReferral);
            dbContext.SaveChanges();

            return View("ReferralAdded");
        }
    }
}
