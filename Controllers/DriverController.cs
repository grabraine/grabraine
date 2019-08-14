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
    public class DriverController : Controller
    {
         private DataContext _context; 
       
   public DriverController( DataContext context )
        {
            _context = context;
         
    
        }
      [Route("GetAllDriverReg")]
        public List<DriverReg> GetAllDriverReg()
        {
            return _context.DriverReg.ToList();
        }
              [Route("DriverReg/")]
           [Route("DriverReg/{filter}/{value}/")]
        public  List<DriverReg> GetAllDriverRegByOrder(string filter,string value,int pageSize, int pageNumber)
        {
          // var totalCount = _context.Inquiry.ToList().Count(); 
           // var totalPages = Math.Ceiling((double)totalCount / pageSize); 
            List<DriverReg> Res = new List<DriverReg>(); 
             //  var Res = _context.Inquiry.ToList(); 
            switch (filter)
            {
                case "LastName":
                    Res = (from c in _context.DriverReg
                           where c.LastName.StartsWith(value)
                              select c ).OrderByDescending(a => a.TimeStamp).ToList();
                    break;
                case "State":
                    Res = (from c in _context.DriverReg
                           where c.State.Equals(value)
                          select c).ToList();
                    break;
                case "ID":
                    Res = (from c in _context.DriverReg
                           where c.ID.Equals(value)
                          select c).ToList();
                    break;
                  case "DriverID":
                    Res = (from c in _context.DriverReg
                           where c.DriverID.Equals(value)
                          select c).ToList();
                    break;
                 default:
                  Res =  _context.DriverReg.OrderByDescending(a => a.TimeStamp).ToList(); 
                     break;
        
            }
              // var clubs = Res.Skip((pageNumber - 1) * pageSize)                             
               //             .Take(pageSize)                 
                //            .ToList(); 
 
                //var result = new 
                //{ 
                 //   TotalCount = totalCount, 
                  //  totalPages = totalPages, 
               
            
                return Res; 
        }

        [HttpPost("AddDriver")]
        public IActionResult AddDriver(DriverReg drive, [FromBody]DriverReg driver)
        {
            // map dto to entity
       
            try 
            {
                // save 
                  drive.ID = Guid.NewGuid().ToString();
                         driver.TimeStamp = DateTime.UtcNow;
                        _context.DriverReg.Add(driver);
                        _context.SaveChanges();  
                        return Ok(driver);  
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
      
        [HttpPut("Update/{ID}")]
        public IActionResult Update(string ID, [FromBody]DriverReg Drive, DriverReg drive1)
        {
          
            try 
            {
                // save 
                 Drive.TimeStamp = DateTime.Now;
                 _context.DriverReg.Update(Drive);
                   _context.SaveChanges(); 
                return Ok(drive1);
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        
   
            
         
      
          
    }
}
