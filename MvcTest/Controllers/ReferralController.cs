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
using MvcTest.DataHelper;

namespace MvcTest.Controllers
{
    public class ReferralController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReferralHelper referralHelper;

        public ReferralController(ILogger<HomeController> logger, ApplicationDbContext _dbContext)
        {
            _logger = logger;
            referralHelper = new ReferralHelper(_dbContext);
        }

        public IActionResult List(string searchItem = "")
        {
            var referralList = referralHelper.GetReferrals(searchItem);
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
            if (!referralHelper.IsDataValid(referral, out string errorMessage))
            {
                ViewData["ValidationError"] =  errorMessage;
                return View(referral);
            }

            if (referralHelper.CreateReferral(referral) == true)
            {
                referralHelper.NotifyManager(referral);
            }   

            return View("ReferralAdded");
        }
    }
}
