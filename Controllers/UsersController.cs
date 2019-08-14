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
using System.Threading.Tasks;
using System.Linq;
using WebApi.ViewModel;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.IO;
using System.Net.Http.Headers;



namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
         private DataContext _context; 

        public UsersController(
            IUserService userService,
            IMapper mapper,
            DataContext context,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
             _context = context;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);
   // List<UserAccounts>  AccessResult = new List<UserAccounts>(); 
     // List<Accounts>  AccessInfo = new List<Accounts>(); 
        //var accountsAccess = from s in _context.UserAccounts 
        //join x in _context.Accounts on s.Accounts_Id equals x.Id into Accounts
       // select s;
       var Count = (from c in _context.UserAccounts
                       orderby c.Accounts_Id
                            join x in _context.Accounts  on c.Accounts_Id equals x.Id 
                            //into AccountsInfo
           
                           where c.UserAccount_Account_Id.Equals(user.Id)
                           select new {id = x.Id,name = x.Name}).ToList().Count();

        bool multipleAccess = false;
        if (Count > 1) {
            multipleAccess = true;
        }
        else {
            multipleAccess = false;
        }
          var Access = (from c in _context.UserAccounts
                       orderby c.Accounts_Id
                            join x in _context.Accounts  on c.Accounts_Id equals x.Id 
                            //into AccountsInfo
           
                           where c.UserAccount_Account_Id.Equals(user.Id)
                           select new {id = x.Id,name = x.Name}).ToList();

        // var accountsInfo = from s in _context.Accounts select s;
      //  AccessResult = accountsAccess.Where(a => a.UserAccount_Account_Id == user.Id).ToList();
         // AccessInfo= accountsInfo.Where(x => x.Id == AccessResult.Accounts_Id).ToList();
        
         // var accountsAccess = _context.UserAccounts.SingleOrDefault(m => m.UserAccount_Account_Id == user.Id).ToList();  
          // var AccessInfo = _context.Accounts.SingleOrDefault(m => m.Id == accountsAccess.Accounts_Id);  
           
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DatabaseAccess = user.DatabaseAccess,
                UserRole = user.UserRole,
                Token = tokenString,
                Longitude = user.Longitude,
                Lattitude = user.Lattitude,
               Access,
               multipleAccess
               // AccessInfo
            });
        }
        

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
           var user = _mapper.Map<User>(userDto);
          //  var user = _mapper.Map<IList<User>>(userDto);

            try 
            {
                // save 
                _userService.Create(user, userDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous] 
        [HttpPost("AddAccess")]
        public IActionResult AddAccess([FromBody]UserAccounts userAccounts )
            {  
                
            
                    _context.UserAccounts.Add(userAccounts);
                    _context.SaveChanges();  
                        return Ok(userAccounts);   
                   
                }
          [AllowAnonymous] 
        [HttpPost("AddDatabase")]
        public IActionResult AddDatabase([FromBody]Accounts Accounts )
            {  
                
                   //  acc.DateCreated = DateTime.Now;
                    _context.Accounts.Add(Accounts);
                    _context.SaveChanges();  
                        return Ok(Accounts);   
                   
                }
         [AllowAnonymous]
        [HttpDelete("DeleteDatabase/{id}")]
        public IActionResult DeleteDatabase(int id)
        {
           _userService.DeleteDatabase(id);
            return Ok("ok");
        }
          [AllowAnonymous]
         [HttpGet("getallDatabase")]
          public IActionResult GetAllDatabase()
        {
          var Res =  _context.Accounts;
                        
            return Ok(Res);
        }
      
     
         [AllowAnonymous]
         [HttpGet("getall2")]
          public IActionResult GetAll2()
        {
          var Res = (from c in _context.Users
                        
                        join x in _context.UserAccounts on c.Id equals x.UserAccount_Account_Id
                         //   join y in _context.Accounts on c.Accounts_Id equals y.Id 
               // group new {c,x} by new {c.Id} into g   
                            select new {
                             id =c, test = x }).ToList();
            return Ok(Res);
        }
      
     
        [AllowAnonymous]
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }
       
         [AllowAnonymous]
        [HttpGet("getbyId")]
        public IActionResult GetById(int id)
        {
            var user =  _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
            [AllowAnonymous]
         [HttpGet("getAccess")]
          public IActionResult GetAccess(int id)
        {
             var Access = (from c in _context.UserAccounts
                       orderby c.Accounts_Id
                            join x in _context.Accounts  on c.Accounts_Id equals x.Id 
                            //into AccountsInfo
           
                           where c.UserAccount_Account_Id.Equals(id)
                           select new {id = x.Id,name = x.Name,Accounts_id = c.Id}).ToList();
            return Ok(Access);
        }
         [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try 
            {
                // save 
                _userService.Update(user, userDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
           [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
         [AllowAnonymous]
        [HttpDelete("DeleteAccess/{id}")]
        public IActionResult DeleteAccess(int id)
        {
           _userService.DeleteAccess(id);
            return Ok();
        }
  [AllowAnonymous]
        [HttpPost("upload"), DisableRequestSizeLimit]
public IActionResult Upload()
{
     try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
           catch (Exception ex)
        {
             return StatusCode(500, "Internal server error");
            }
   
}
 
      
    }
}
