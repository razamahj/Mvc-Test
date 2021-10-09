using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcTest.Application.Entities;
namespace MvcTest.Models
{
    public class ReferralModel
    {
        public string Forename { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DOR { get; set; }


        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Telephone is required.")]
        public string ContactTelephoneNumber { get; set; }

        public string ServiceName { get; set; }

        public List<SelectListItem> Services { get; } = new List<SelectListItem>
        {
        new SelectListItem { Value = "Service One", Text = "Service One" },
        new SelectListItem { Value = "Service Two", Text = "Service Two" },

    };

        // public Service Service { get; set; }
    }
}
