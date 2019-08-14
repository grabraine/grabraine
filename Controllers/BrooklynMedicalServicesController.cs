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

    public class BrooklynMedicalServicesController : Controller
    {
       private BrooklynMedicalServicesContext _context; 
       private HKMasterContext _hkcontext;
       private ScheduleContext _contextSched;
    
   public BrooklynMedicalServicesController(BrooklynMedicalServicesContext context, HKMasterContext hkcontext,ScheduleContext  contextSched)
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
           [HttpPost("AddProvider2")]
         public IActionResult AddProvider2(Providers prov,string EIN,string SSN,string NPI, string License, string UPIN, string SystemNoteKey,
         int TaxonomyCodeId, string FirstName, string LastName, string MiddleName, 
         string Suffix, string Phone_1, string Email, string Fax, string Phone_2, 
         string Address_1, string Address_2, string City, string Zip, string State, 
         bool IsCompany, DateTime DateModified, bool IsReferrer,string Credential)  
                {  
                    try  
                    {  
                    
                        prov.EIN = EIN;
                        prov.SSN = SSN;
                        prov.NPI = NPI;
                        prov.License = License;
                        prov.UPIN = UPIN;
                        prov.SystemNoteKey = Guid.NewGuid().ToString();
                        prov.TaxonomyCodeId = TaxonomyCodeId;
                        prov.FirstName = FirstName;
                        prov.LastName = LastName;
                        prov.MiddleName = MiddleName;
                        prov.Suffix = Suffix;
                        prov.Phone_1 = Phone_1;
                        prov.Email = Email;
                        prov.Fax = Fax;
                        prov.Phone_2 = Phone_2;
                        prov.Address_1 = Address_1;
                        prov.City = City;
                        prov.Zip = Zip;
                        prov.State = State;
                        prov.IsCompany = IsCompany;
                        prov.DateModified =  DateTime.Now;
                        prov.IsReferrer = IsReferrer;
                        prov.Credential = Credential;
                      
                       _context.Providers.Add(prov);
                        _context.SaveChanges();  
                        return Ok(prov);  
                    }  
                    catch  
                    {  
                        throw;  
                    }  
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

        [Route("Delete/{value}")]
       [HttpDelete("{value}")]  
        public async Task<IActionResult> Delete(string value)  
        {  
           
  
            var schedule =  _context.ScheduleData.SingleOrDefault(m => m.ScheduleId == value);  
            if (schedule == null)  
            {  
                 return NotFound();
            }  
         if (ModelState.IsValid)
            {
              _context.ScheduleData.Remove(schedule);  
           await   _context.SaveChangesAsync();  
  
            
            }
            return Ok(schedule);  
        } 
 
  
             [Route("Update/{id}/{date}/{time}")]
       public int UpdateSched(string id,string time,DateTime date)
       {
               try  
                    {  
                        var schedule =   _context.ScheduleData.SingleOrDefault(f => f.ScheduleId == id );
                      schedule.Time = time;
                      schedule.Date = date;
                        _context.SaveChanges();
                        return 1;  
                    }  
                    catch  
                    {  
                        throw;  
                    }  

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
            [Route("Add/{value1}/{value2}/{value3}/{value4}/{value5}/{value6}/{value7}/{value8}/{value9}/{value10}/{value11}/{value12}/{value13}/{value14}/{value15}/{value16}/{value17}/{value18}/{value19}/{value20}/{value21}/{value22}/{value23}")]
         public int AddPatient(ScheduleData schedule,DateTime value1,string value2,string value3, string value4, string value5, string value6, string value7,string value8,string value9,string value10,string value11, string value12, string value13, string value14, string value15,string value16, string value17,string value18, string value19, string value20,string value21, string value22, string value23)  
                {  
                    try  
                    {  
                        schedule.ScheduleId =  Guid.NewGuid().ToString();
                        schedule.Date = value1;
                        schedule.Time = value2;
                        schedule.DXCode = value3;
                        schedule.DoctorsName = value4;
                        schedule.FacilityName = value5;
                        schedule.FacilityAddress = value6;
                        schedule.FirstName = value7;
                        schedule.MiddleName = value8;
                        schedule.LastName = value9;
                        schedule.Address = value10;
                        schedule.City = value11;
                        schedule.State = value12;
                        schedule.Zip = value13;
                        schedule.PhoneNumber = value14;
                        schedule.Email = value15;
                        schedule.PrimaryInsurance = value16;
                        schedule.SecondaryInsurance = value17;
                        schedule.MemberID = value18;
                        schedule.Destination = value19;
                        schedule.TransportationService = value20;
                        schedule.TypeOfVehicle = value21;
                        schedule.Authorization = value22;
                        schedule.Priority = value23;

                       

                        _context.ScheduleData.Add(schedule);
                        _context.SaveChanges();  
                        return 1;  
                    }  
                    catch  
                    {  
                        throw;  
                    }  
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
        /* 
           [Route("TimeReport/")]
            [Route("TimeReport/{filter}/{value}")]
        public IActionResult GetTimeReport(string filter,string value)
        {
              var Res = (from c in _context.TimeReport 
                       orderby c.TimePickup
                            join x in _context.Subscribers on c.PatientId equals x.Id into PatientDetails
                        select new { TimeId = c.Id, PatientId = c.PatientId , LastName = x.LastName , FirstName = x.FirstName, TimePickup = c.TimePickup, TimeDropOff = c.TimeDropOff, Date = c.Date }).ToList();
                        break;
            switch (filter)
            {
              
                case "Date":
                DateTime oDate = Convert.ToDateTime(value);
                Res = (from c in _context.TimeReport 
                       orderby c.TimePickup
                            join x in _context.Subscribers on c.PatientId equals x.Id into PatientDetails
                        where c.Date.Equals(oDate)
                        select new { TimeId = c.Id, PatientId = c.PatientId , LastName = x.LastName , FirstName = x.FirstName, TimePickup = c.TimePickup, TimeDropOff = c.TimeDropOff, Date = c.Date }).ToList();
                        break;
                case "TimePickup":
                Res = (from c in _context.TimeReport 
                       orderby c.TimePickup
                            join x in _context.Subscribers on c.PatientId equals x.Id into PatientDetails
                        where c.TimePickup.Equals(value)
                        select new { TimeId = c.Id, PatientId = c.PatientId , LastName = x.LastName , FirstName = x.FirstName, TimePickup = c.TimePickup, TimeDropOff = c.TimeDropOff, Date = c.Date }).ToList();
                        break;
                case "TimeDropOff":
                Res = (from c in _context.TimeReport 
                       orderby c.TimePickup
                            join x in _context.Subscribers on c.PatientId equals x.Id into PatientDetails
                        where c.TimeDropOff.Equals(value)
                        select new { TimeId = c.Id, PatientId = c.PatientId , LastName = x.LastName , FirstName = x.FirstName, TimePickup = c.TimePickup, TimeDropOff = c.TimeDropOff, Date = c.Date }).ToList();
                        break;
                default:
                    Res = (from c in _context.TimeReport 
                       orderby c.TimePickup
                            join x in _context.Subscribers on c.PatientId equals x.Id into PatientDetails
                        select new { TimeId = c.Id, PatientId = c.PatientId , LastName = x.LastName , FirstName = x.FirstName, TimePickup = c.TimePickup, TimeDropOff = c.TimeDropOff, Date = c.Date }).ToList();
                        break;
        
            }
            return Ok(Res);
        }
        */
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