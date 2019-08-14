using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Dtos;
using WebApi.Entities;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ViewModel;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace WebApi.Controllers
{
    
    [Route("api/[controller]")]

    public class DataController : Controller
    {
       private LopezContext _context; 
       private HKMasterContext _hkcontext;
    
   public DataController( LopezContext context, HKMasterContext hkcontext)
        {
            _context = context;
            _hkcontext = hkcontext;
    
        }
        
     
       [Route("GetAllCptCodes")]
        public List<CptCodes> GetCptCodes()
        {
            return _hkcontext.CptCodes.ToList();
        }
           [Route("CptCodes/{filter}/{value}")]
        public List<CptCodes> GetCptCodesByOrder(string filter,string value)
        {
            List<CptCodes> Res = new List<CptCodes>(); 
            switch (filter)
            {
                case "Code":
                    Res = (from c in _hkcontext.CptCodes
                           where c.Code.StartsWith(value)
                              select c ).OrderBy(a => a.Code).ToList();
                    break;
                case "Description":
                    Res = (from c in _hkcontext.CptCodes
                           where c.Description.StartsWith(value)
                          select c).ToList();
                    break;
              
        
            }
            return Res;
        }
         [Route("GetAllICDCodes")]
        public List<ICDCodes> GetICDCodes()
        {
            return _hkcontext.ICDCodes.ToList();
        }
         [Route("ICDCodes")]
            [Route("ICDCodes/{filter}/{value}")]
        public List<ICDCodes> GetICDCodesByOrder(string filter,string value)
        {
            List<ICDCodes> Res = new List<ICDCodes>(); 
            switch (filter)
            {
                case "Code":
                    Res = (from c in _hkcontext.ICDCodes
                           where c.Code.StartsWith(value)
                              select c ).OrderBy(a => a.Code).ToList();
                    break;
                case "Description":
                    Res = (from c in _hkcontext.ICDCodes
                           where c.Description.StartsWith(value)
                          select c).ToList();
                    break;
                  default:
                  Res = (from c in _hkcontext.ICDCodes
                          select c ).OrderBy(a => a.Code).ToList();
                     break;
        
            }
            return Res;
        }

      [Route("GetAllSubscriber")]
        public List<Subscribers> GetSubscriber()
        {
            return _context.Subscribers.ToList();
        }
    [Route("GetAllProvider")]
        public List<Providers> GetProviders()
        {
            return _context.Providers.ToList();
        }
          [Route("ProviderCompany")]
        public List<Providers> ProvidersCompany()
        {
            return _context.Providers.Where(e => e.IsCompany == true ).ToList();
        }
           [Route("ProviderHomeAddress")]
        public List<Providers> ProvidersHomeAddress()
        {
            return _context.Providers.Where(e => e.IsCompany == false ).ToList();
        }
           
             
        [Route("Subscriber/{filter}/{value}")]
        public List<Subscribers> GetSubscriberByOrder(string filter,string value)
        {
            List<Subscribers> Res = new List<Subscribers>(); 
            switch (filter)
            {
                case "LastName":
                    Res = (from c in _context.Subscribers 
                           where c.LastName.StartsWith(value)
                              select c ).OrderBy(a => a.LastName).ToList();
                    break;
                case "PhoneNumber":
                    Res = (from c in _context.Subscribers
                           where c.Phone_1.StartsWith(value)
                          select c).ToList();
                    break;
                case "FirstName":
                    Res = (from c in _context.Subscribers
                           where c.FirstName.StartsWith(value)
                          select c).ToList();
                    break;
                case "ID":
                    Res = (from c in _context.Subscribers
                           where c.Id.ToString().Equals(value)
                          select c).ToList();
                    break;
        
            }
            return Res;
        }
         
          [AllowAnonymous]
        [HttpPost("SendEmail_HealthKinect")]
        public IActionResult SendEmail([FromBody]SendDto senddto)
        {
         var message = new MimeMessage ();
           message.From.Add (new MailboxAddress ("NO REPLY" ,"Maureen@healthkinect.co"));
                message.To.Add (new MailboxAddress (senddto.FromSite, @senddto.Receiver));
                message.Bcc.Add(new MailboxAddress (senddto.FromSite, "grob_18@yahoo.com"));
                message.Subject = senddto.Subject;
                message.Body = new TextPart ("plain") {
                Text =  "Sender "  +  "  " + senddto.FullName + " Msg: " + senddto.Message + " please contact me @ " + senddto.PhoneNumber
                    };
          
                    using (var client = new SmtpClient (new ProtocolLogger (Console.OpenStandardOutput ()))) {
                        // Connect to the server
                        client.Connect("smtpout.secureserver.net", 80, false);

                        // Authenticate ourselves towards the server
                        client.Authenticate("Maureen@healthkinect.co", "Sheba003"); // should fail here
                        client.Send (message);
                        client.Disconnect (true);
                    }
            return Ok(senddto.Receiver);
        }

             [Route("Subscriber/")]
            [Route("Subscriber1/{filter}/{value}")]
        public IActionResult GetSubscriberByOrder1(string filter,string value)
        {
               var Res = (from c in _context.Subscribers 
                            join x in _context.Payers  on c.PrimaryPayerId equals x.Id into PrimaryPayers
                             join y in _context.Payers on c.SecondaryPayerId equals (int?)y.Id into SecondaryPayers
                              select new { Id = c.Id,LastName = c.LastName,FirstName = c.FirstName, MiddleName = c.MiddleName,DateOfBirth = c.DateOfBirth,
                              Gender = c.Gender,PhoneNumber = c.Phone_1,PhoneNumber2 = c.Phone_2,Email = c.Email,
                              Address = c.Address_1, Address2 = c.Address_2,City = c.City,State = c.State , Zip = c.Zip,PrimaryPayer = PrimaryPayers,SecondaryPayer = SecondaryPayers  }).ToList();
            switch (filter)
            {
                case "LastName":
                    Res = (from c in _context.Subscribers 
                       orderby c.LastName
                            join x in _context.Payers  on c.PrimaryPayerId equals x.Id into PrimaryPayers
                            join y in _context.Payers on c.SecondaryPayerId equals (int?)y.Id into SecondaryPayers
                           where c.LastName.StartsWith(value)
                           select new { Id= c.Id,LastName = c.LastName,FirstName = c.FirstName, MiddleName = c.MiddleName,DateOfBirth = c.DateOfBirth,
                              Gender = c.Gender,PhoneNumber = c.Phone_1,PhoneNumber2 = c.Phone_2,Email = c.Email,
                              Address = c.Address_1, Address2 = c.Address_2,City = c.City,State = c.State , Zip = c.Zip,PrimaryPayer = PrimaryPayers,SecondaryPayer = SecondaryPayers}).ToList();
                    break;
                case "ID":
                Res = (from c in _context.Subscribers 
                       orderby c.Id
                            join x in _context.Payers  on c.PrimaryPayerId equals x.Id into PrimaryPayers
                            join y in _context.Payers on c.SecondaryPayerId equals (int?)y.Id into SecondaryPayers
                        where c.Id.ToString().Equals(value)
                        select new { Id = c.Id,LastName = c.LastName,FirstName = c.FirstName, MiddleName = c.MiddleName,DateOfBirth = c.DateOfBirth,
                        Gender = c.Gender,PhoneNumber = c.Phone_1,PhoneNumber2 = c.Phone_2,Email = c.Email,
                        Address = c.Address_1, Address2 = c.Address_2,City = c.City,State = c.State , Zip = c.Zip,PrimaryPayer = PrimaryPayers,SecondaryPayer = SecondaryPayers}).ToList();
                        break;
    
                   default:
                  Res = (from c in _context.Subscribers 
                         orderby c.LastName
                            join x in _context.Payers  on c.PrimaryPayerId equals x.Id into PrimaryPayers
                             join y in _context.Payers on c.SecondaryPayerId equals (int?)y.Id into SecondaryPayers
                              select new { Id = c.Id,LastName = c.LastName,FirstName = c.FirstName, MiddleName = c.MiddleName,DateOfBirth = c.DateOfBirth,
                              Gender = c.Gender,PhoneNumber = c.Phone_1,PhoneNumber2 = c.Phone_2,Email = c.Email,
                              Address = c.Address_1, Address2 = c.Address_2,City = c.City,State = c.State , Zip = c.Zip,PrimaryPayer = PrimaryPayers,SecondaryPayer = SecondaryPayers  }).ToList();
                     break;
        
            }
            return Ok(Res);
        }
                [Route("Subscriber2/")]
            [Route("Subscriber2/{filter}/{value}")]
        public IActionResult GetSubscriberByOrder2(string filter,string value)
        {


               var Res = (from c in _context.Subscribers 
                            join x in _context.Payers  on c.PrimaryPayerId equals x.Id into PrimaryPayers
                             join y in _context.Payers on c.SecondaryPayerId equals (int?)y.Id into SecondaryPayers
                              select new { Id = c.Id,LastName = c.LastName,FirstName = c.FirstName, MiddleName = c.MiddleName,DateOfBirth = c.DateOfBirth,PrimaryMemberId=c.PrimaryMemberId,SecondaryMemberId=c.SecondaryMemberId,
                              Gender = c.Gender,PhoneNumber = c.Phone_1,PhoneNumber2 = c.Phone_2,Email = c.Email,
                              Address = c.Address_1, Address2 = c.Address_2,City = c.City,State = c.State , Zip = c.Zip,PrimaryPayer = PrimaryPayers,SecondaryPayer = SecondaryPayers  }).ToList();
            switch (filter)
            {
                case "LastName":
                    Res = (from c in _context.Subscribers 
                       orderby c.LastName
                            join x in _context.Payers  on c.PrimaryPayerId equals x.Id into PrimaryPayers
                            join y in _context.Payers on c.SecondaryPayerId equals (int?)y.Id into SecondaryPayers
                           where c.LastName.StartsWith(value)
                           select new { Id= c.Id,LastName = c.LastName,FirstName = c.FirstName, MiddleName = c.MiddleName,DateOfBirth = c.DateOfBirth,PrimaryMemberId=c.PrimaryMemberId,SecondaryMemberId=c.SecondaryMemberId,
                              Gender = c.Gender,PhoneNumber = c.Phone_1,PhoneNumber2 = c.Phone_2,Email = c.Email,
                              Address = c.Address_1, Address2 = c.Address_2,City = c.City,State = c.State , Zip = c.Zip,PrimaryPayer = PrimaryPayers,SecondaryPayer = SecondaryPayers}).ToList();
                    break;
                case "ID":
                Res = (from c in _context.Subscribers 
                       orderby c.Id
                            join x in _context.Payers  on c.PrimaryPayerId equals x.Id into PrimaryPayers
                            join y in _context.Payers on c.SecondaryPayerId equals (int?)y.Id into SecondaryPayers
                        where c.Id.ToString().Equals(value)
                        select new { Id = c.Id,LastName = c.LastName,FirstName = c.FirstName, MiddleName = c.MiddleName,DateOfBirth = c.DateOfBirth,PrimaryMemberId=c.PrimaryMemberId,SecondaryMemberId=c.SecondaryMemberId,
                        Gender = c.Gender,PhoneNumber = c.Phone_1,PhoneNumber2 = c.Phone_2,Email = c.Email,
                        Address = c.Address_1, Address2 = c.Address_2,City = c.City,State = c.State , Zip = c.Zip,PrimaryPayer = PrimaryPayers,SecondaryPayer = SecondaryPayers}).ToList();
                        break;
    
                   default:
                  Res = (from c in _context.Subscribers 
                         orderby c.LastName
                            join x in _context.Payers  on c.PrimaryPayerId equals x.Id into PrimaryPayers
                             join y in _context.Payers on c.SecondaryPayerId equals (int?)y.Id into SecondaryPayers
                              select new { Id = c.Id,LastName = c.LastName,FirstName = c.FirstName, MiddleName = c.MiddleName,DateOfBirth = c.DateOfBirth,PrimaryMemberId=c.PrimaryMemberId,SecondaryMemberId =c.SecondaryMemberId,
                              Gender = c.Gender,PhoneNumber = c.Phone_1,PhoneNumber2 = c.Phone_2,Email = c.Email,
                              Address = c.Address_1, Address2 = c.Address_2,City = c.City,State = c.State , Zip = c.Zip,PrimaryPayer = PrimaryPayers,SecondaryPayer = SecondaryPayers  }).ToList();
                     break;
        
            }
        return Ok(new {
                patient = Res
               // AccessInfo
            });;
        }
    
    }
}