using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Helpers;




namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
         private ScheduleContext _context; 

   public ScheduleController( ScheduleContext context)
        {
            _context = context;
    
        }

        [Route("DateTime/{value1}/{value}")]

         public List<ScheduleTime> GetScheduleTime(string filter,string value,string value1)
         {
              List<ScheduleTime> Res = new List<ScheduleTime>(); 
              DateTime oDate = Convert.ToDateTime(value1);
             var schedule = from s in _context.ScheduleTime
            select s;

             Res = schedule.Where(a => a.Date == oDate && a.Time == value) .ToList();
            return Res;

         }

        [Route("DateTimePatient1/{value}")]
        public IActionResult GetScheduleTimePatient1(string filter,string value,string value1,string valueid)
        {
        DateTime oDate = Convert.ToDateTime(value1);
        List<ScheduleTime> Res = new List<ScheduleTime>(); 
        var model = from s in _context.ScheduleTime
            select s;
         var Result = model.Where(a => a.ScheduleId == value) .ToList();
        return Json(Result);
        }

        [Route("Delete/{value}")]
       [HttpDelete("{value}")]  
        public async Task<IActionResult> Delete(string value)  
        {  
           
  
            var schedule =  _context.ScheduleTime.SingleOrDefault(m => m.ScheduleId == value);  
            if (schedule == null)  
            {  
                 return NotFound();
            }  
         if (ModelState.IsValid)
            {
            _context.ScheduleTime.Remove(schedule);  
           await _context.SaveChangesAsync();  
  
            
            }
            return Ok(schedule);  
        }  

        [Route("Add/{value1}/{value2}/{value3}/{value4}/{value5}/{value6}/{value7}/{value8}/{value9}/{value10}/{value11}/{value12}/{value13}/{value14}/{value15}/{value16}/{value17}/{value18}/{value19}/{value20}/{value21}/{value22}/{value23}")]
         public int AddPatient(ScheduleTime schedule,DateTime value1,string value2,string value3, string value4, string value5, string value6, string value7,string value8,string value9,string value10,string value11, string value12, string value13, string value14, string value15,string value16, string value17,string value18, string value19, string value20,string value21, string value22, string value23)  
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

                       

                        _context.ScheduleTime.Add(schedule);
                        _context.SaveChanges();  
                        return 1;  
                    }  
                    catch  
                    {  
                        throw;  
                    }  
                }
             [Route("Update/{id}/{date}/{time}")]
       public int UpdateSched(string id,string time,DateTime date)
       {
               try  
                    {  
                        var schedule = _context.ScheduleTime.SingleOrDefault(f => f.ScheduleId == id );
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
          
    }
}
