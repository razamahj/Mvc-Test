using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using MimeKit;
using MvcTest.Application.Entities;
using MvcTest.Application.Persistence;
using MvcTest.Models;

namespace MvcTest.DataHelper
{
    public class ReferralHelper
    {

        private ApplicationDbContext dbContext;

        public ReferralHelper(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public bool IsDataValid(ReferralModel referral, out string errorMessage)
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
        public bool CreateReferral(ReferralModel referral)
        {
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
   
            return true;
            }
        
        public void NotifyManager(ReferralModel referral)
        {

            var managerEmail = (from s in dbContext.Services
                             join u in dbContext.Users on s.ManagerId equals u.Id
                             where s.Name == referral.ServiceName
                             select u).FirstOrDefault();

            string emailPath = @"C:\Emails";
            if (!Directory.Exists(emailPath))
            {
                Directory.CreateDirectory(emailPath);
            }

            var client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = emailPath
            };

            MailAddress from = new MailAddress("junaid2396@hotmail.com",
            "Junaid " + (char)0xD8 + " Mahmood",
            System.Text.Encoding.UTF8);
            // Set destinations for the email message.
            MailAddress to = new MailAddress(managerEmail.EmailAddress);
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = "This is a test email message sent by an application. ";
            // Include some non-ASCII characters in body and subject.
            message.Body += Environment.NewLine;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "New Referral";
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            client.Send(message);
        }

        public List<ReferralModel> GetReferrals(string searchItem)
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
            return referralList;
        }
    }
}
