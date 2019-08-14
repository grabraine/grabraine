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

    public class OrthoUrgentCareController : Controller
    {
       private OrthoUrgentCareContext _context; 
       private HKMasterContext _hkcontext;
       private ScheduleContext _contextSched;
    
   public OrthoUrgentCareController(OrthoUrgentCareContext context, HKMasterContext hkcontext,ScheduleContext  contextSched)
        {
            _context = context;
            _hkcontext = hkcontext;
            _contextSched = contextSched;
    
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
         [Route("Provider/")]
            [Route("Provider/{filter}/{value}")]
        public IActionResult GetProviderID(string filter,int value)
        {
               var Res = _context.Providers.ToList();

            switch (filter)
            {
              
                case "ID":
                Res = (from c in _context.Providers
                           where c.Id.Equals(value)
                          select c).ToList();
                        break;
    
                   default:
                  Res = _context.Providers.ToList();
                     break;
        
            }
            return Ok(Res);
        }
          [Route("ProviderCompany")]
        public List<Providers> ProvidersCompany()
        {
           
            return _context.Providers.Where(e => e.IsCompany == true ).Take(1).ToList();
        }
           [Route("ProviderHomeAddress")]
        public List<Providers> ProvidersHomeAddress()
        {
            return _context.Providers.Where(e => e.IsCompany == false ).Take(1).ToList();
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
            [Route("Subscriber/{filter}/{value}")]
        public IActionResult GetSubscriberByOrder1(string filter,string value)
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
            return Ok(Res);
        }
  
     
    [AllowAnonymous] 
    [HttpPost("AddProvider")]
      public IActionResult AddProvider(Providers prov,[FromBody]Providers provider )
                {  
                   
                
                    prov.DateModified = DateTime.Now;
                        _context.Providers.Add(provider);
                        _context.SaveChanges();  
                        return Ok(provider);  
                     
                   
                }
           [AllowAnonymous]
         [Route("DateTime/{value1}/{value}")]

         public List<ScheduleData> GetScheduleTime(string filter,string value,string value1)
         {
              List<ScheduleData> Res = new List<ScheduleData>(); 
              DateTime oDate = Convert.ToDateTime(value1);
             var schedule = from s in   _context.ScheduleData
            select s;

             Res = schedule.Where(a => a.Date == oDate && a.Time == value) .ToList();
            return Res;

         }
         

        [Route("DateTimePatient1/{value}")]
        public IActionResult GetScheduleTimePatient1(string filter,string value,string value1,string valueid)
        {
        DateTime oDate = Convert.ToDateTime(value1);
        List<ScheduleData> Res = new List<ScheduleData>(); 
        var model = from s in   _context.ScheduleData
            select s;
         var Result = model.Where(a => a.ScheduleId == value) .ToList();
        return Json(Result);
        }

         [AllowAnonymous] 
    [HttpPost("AddSubscriber")]
      public IActionResult AddSubscriber(Subscribers sub,[FromBody]Subscribers subscriber )
                {  
                   
                    sub.DateModified = DateTime.Now;
                        _context.Subscribers.Add(subscriber);
                        _context.SaveChanges();  
                           return Ok(subscriber);   
                   
                }
          [AllowAnonymous]
        [HttpPut("Update/Subscribers/{id}")]
        public IActionResult Update(int id, [FromBody]Subscribers sub1,Subscribers sub)
        {
          
            try 
            {
                // save 
                 sub1.DateModified = DateTime.Now;
                 _context.Subscribers.Update(sub1);
                   _context.SaveChanges(); 
                return Ok(sub);
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
         [AllowAnonymous]
        [HttpDelete("Delete/Subscribers/{id}")]
        public IActionResult Delete(int id)
        {
               var subscriber = _context.Subscribers.Find(id);
            
              
                    _context.Subscribers.Remove(subscriber);
                    _context.SaveChanges();
                     return Ok();
                
           
        }
         [Route("GetAllPayers")]
        public List<Payers> GetPayers()
        {
            return _context.Payers.ToList();
        }
      
         [AllowAnonymous] 
    [HttpPost("AddTimeReport")]
      public IActionResult AddTimeReport(TimeReport time,[FromBody]TimeReport Time)  
                {  
                    
                       time.Id =  Guid.NewGuid().ToString();
                       
                        _context.TimeReport.Add(Time);
                        _context.SaveChanges();  
                        return Ok("ok");  
                     
                   
                }
    
           [Route("GetAllFacilities")]
        public List<Facilities> GetFacilities()
        {
            return _context.Facilities.ToList();
        }
            [AllowAnonymous] 
          [HttpPost("AddFacilities")]
      public IActionResult AddFacilities([FromBody]Facilities facilities )
                {  
                   
                
                  
                        _context.Facilities.Add(facilities);
                        _context.SaveChanges();  
                        return Ok(facilities);  
                     
                   
                }
       [AllowAnonymous]
        [HttpGet("GetSched")]
        public IActionResult GetSched(DateTime date,string time)
        {
             List<Sched2> Res = new List<Sched2>(); 
             var sched = from c in _context.Sched2 select c;
                        //   where c.Date.Equals(date)
             int numbers;             
            if (time is null) {
                Res  = sched.Where(a => a.Date == date) .ToList();
                numbers= sched.Where(a => a.Date == date) .ToList().Count();
            }
            else {
                 Res  = sched.Where(a => a.Date == date && a.Time ==  time) .ToList();
                numbers = sched.Where(a => a.Date == date && a.Time ==  time) .ToList().Count();
            }
            
            return Ok(new{Res=Res,numbers=numbers});
        }
          [AllowAnonymous]
        [HttpPut("UpdateSchedule/{id}")]
        public IActionResult UpdateSchedule(string id, [FromBody]Sched2 Sched)
        {
               Sched.ScheduleId = id;
              _context.Sched2.Update(Sched);
                _context.SaveChanges(); 
                return Ok(Sched);
        
        }
          [AllowAnonymous]
        [HttpPut("UpdateTime/{id}")]
        public IActionResult UpdateTime(string id, Sched2 _sched, [FromBody]Sched2 Sched)
        {
              Sched.ScheduleId = id;
              _context.Sched2.Attach(Sched);
            _context.Entry(Sched).Property(x => x.Time).IsModified = true;
              _context.Entry(Sched).Property(x => x.Date).IsModified = true;
                _context.SaveChanges(); 
                return Ok("time set");
        
        }
    
        [AllowAnonymous] 
       [HttpDelete("DeleteSchedule/{id}")]  
        public IActionResult DeleteSchedule(string id)  
          
           {
  
            var schedule =  _context.Sched2.SingleOrDefault(m => m.ScheduleId == id);  
              _context.Sched2.Remove(schedule);  
             _context.SaveChanges();  
  
            return Ok("ok");  
        } 
       
    [AllowAnonymous] 
    [HttpPost("AddSchedule")]
      public IActionResult AddSchedule(Sched2 sched,[FromBody]Sched2 scheduleTime)  
                {  
                    
                       sched.ScheduleId =  Guid.NewGuid().ToString();    
                        _context.Sched2.Add(scheduleTime);
                        _context.SaveChanges();  
                        return Ok("ok");  
                     
                   
                }
    [AllowAnonymous]
        [HttpGet("GetSpecificSched")]
        public IActionResult GetSpecificSched(string id)
        {
             List<Sched2> Res = new List<Sched2>(); 
             var sched = from c in _context.Sched2 select c;
                        //   where c.Date.Equals(date)            
             Res  = sched.Where(a => a.ScheduleId == id ) .ToList();
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