using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcTest.Application.Entities;
namespace MvcTest.Models
{
    public class ReferralModel
    {
        public string Forename { get; set; }

        public string Surname { get; set; }

        [DisplayName("Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }


        [DisplayName("Date Of Referral")]
        [DisplayFormat (DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOR { get; set; }


        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        [DisplayName("Telephone")]
        [Required(ErrorMessage = "Telephone is required.")]
        public string ContactTelephoneNumber { get; set; }

        [DisplayName("Service Referred")]
        public string ServiceName { get; set; }
    }
}
