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
    public class GovelonowController : Controller
    {
         private GovelonowContext _context; 
         private DataContext _context2;
         

   public GovelonowController( GovelonowContext context ,DataContext context2)
        {
            _context = context;
            _context2 = context2;
    
        }
      [Route("GetAllDrivers")]
        public List<Inquiry> GetAllDrivers()
        {
            return _context.Inquiry.ToList();
        }
              [Route("Drivers/")]
           [Route("Drivers/{filter}/{value}/")]
        public  List<Inquiry> GetAllDriversByOrder(string filter,string value,int pageSize, int pageNumber)
        {
          // var totalCount = _context.Inquiry.ToList().Count(); 
           // var totalPages = Math.Ceiling((double)totalCount / pageSize); 
            List<Inquiry> Res = new List<Inquiry>(); 
             //  var Res = _context.Inquiry.ToList(); 
            switch (filter)
            {
                case "LastName":
                    Res = (from c in _context.Inquiry
                           where c.LastName.StartsWith(value)
                              select c ).OrderByDescending(a => a.TimeStamp).ToList();
                    break;
                case "State":
                    Res = (from c in _context.Inquiry
                           where c.State.Equals(value)
                          select c).ToList();
                    break;
                case "ID":
                    Res = (from c in _context.Inquiry
                           where c.ID.Equals(value)
                          select c).ToList();
                    break;
                 default:
                  Res =  _context.Inquiry.OrderByDescending(a => a.TimeStamp).ToList(); 
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

        
         [Route("AddUser/{value1}/{value2}/{value3}/{value6}/{value4}/{value5}")]
         public int AddUser(User user, string value1, string value2 ,string value3,string value6, string value4, string value5)  
                { 
                 //   url = QueryHelpers.AddQueryString("/api/product/list", queryParams); 
                
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(value6, out passwordHash, out passwordSalt);
                    try  
                    {  
     
           
                    user.Username = value1;
                    user.FirstName = value2;
                    user.LastName = value3;
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.UserRole = value4;
                    user.DatabaseAccess = value5;

                        _context2.Users.Add(user);
                        _context2.SaveChanges();  
                        return 1;  
                    }  
                    catch  
                    {  
                        throw;  
                    }  
                }
            
         
             private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
     
          
    }
}
