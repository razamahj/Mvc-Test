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

        public IActionResult List(string searchItem = "")
        {
            var referralList = new List<ReferralModel>();
            foreach (var item in dbContext.Referrals.OrderByDescending(r => r.DateOfReferral))
            {
                item.Client = dbContext.Clients.First(c => c.Id == item.ClientId);
                if (string.IsNullOrEmpty(searchItem) || item.Client.Forename.ToLower() == searchItem.ToLower() || item.Client.Surname.ToLower() == searchItem.ToLower()) 
                {
                    item.Service = dbContext.Services.First(c => c.Id == item.ServiceId);
                    var objReferral = new ReferralModel()
                    {
                        Forename = item.Client.Forename,
                        Surname = item.Client.Surname,
                        EmailAddress = item.Client.EmailAddress,
                        ContactTelephoneNumber = item.Client.ContactTelephoneNumber,
                        ServiceName = item.Service.Name,
                        DOR = item.DateOfReferral.Date,
                        DateOfBirth = item.Client.DateOfBirth.Value,
                    };
                    referralList.Add(objReferral);
                }
            }
            return View(referralList);
        }

        public IActionResult Create()
        {
            ViewData["ValidationError"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReferralModel referral)
        {
            if (!isDataValid(referral, out string errorMessage))
            {
                ViewData["ValidationError"] =  errorMessage;
                return View(referral);
            }

            var objClient = dbContext.Clients.FirstOrDefault(c => c.Forename.ToLower() == referral.Forename.ToLower() &&
                                                            c.Surname.ToLower() == referral.Surname.ToLower() &&
                                                            c.DateOfBirth == referral.DateOfBirth);
            if (objClient == null)
            {
                objClient = new Client();
                objClient.Forename = referral.Forename;
                objClient.DateOfBirth = referral.DateOfBirth;
                objClient.Surname = referral.Surname;
                objClient.EmailAddress = referral.EmailAddress;
                objClient.ContactTelephoneNumber = referral.ContactTelephoneNumber;
                dbContext.Clients.Add(objClient);
            }

            var objReferral = new Referral();

            objReferral.Client = objClient;

            var objService = dbContext.Services.First(s => s.Name == referral.ServiceName);
            objReferral.Client = objClient;
            objReferral.Service = objService;

            objReferral.DateOfReferral = DateTime.Now;
            dbContext.Referrals.Add(objReferral);
            dbContext.SaveChanges();

            return View("ReferralAdded");
        }

        private bool isDataValid(ReferralModel referral, out string errorMessage)
        {
            errorMessage = "";
            if (referral.DateOfBirth > DateTime.Now.AddYears(-18))
            {
                errorMessage = "Client is under the age of 18";
                return false;
            }
            if (string.IsNullOrWhiteSpace(referral.EmailAddress) && string.IsNullOrWhiteSpace(referral.ContactTelephoneNumber))
            {
               errorMessage = "Need either Email or Telephone";
               return false;
            }
            return true;
        }
    }
}
