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
        [Required(ErrorMessage = "Forename is required.")]
        public string Forename { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }


        [DisplayName("Date Of Referral")]
        [DisplayFormat (DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOR { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        [DisplayName("Telephone")]
        public string ContactTelephoneNumber { get; set; }

        [DisplayName("Service Referred")]
        public string ServiceName { get; set; }
    }
}
