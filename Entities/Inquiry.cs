using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebApi.Entities
{
    public class Inquiry
    {
        [Key]
        public string ID {get; set;}


        
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string State { get; set; }

        public string FirstName {get; set;}
        
        public string LastName {get; set;}

        public string JobType {get; set;}

        public string Juno {get; set;}
        public string Lyft {get; set;}
        public string Uber {get; set;}
        public string YourComment {get; set;}
        
        public string Gender {get; set;}

        public string TLCHackNumber {get;set;}

        public string Referral {get; set;}
        
        public int Agent {get; set;}
        
        public string filename {get; set;}
         
        public string CarSeats{get; set;}

        public string Pets {get; set;}
       
        public string Description {get; set;}

        public int uberrate {get; set;}

        public int lyftrate {get; set;}

        public int junorate {get; set;}
    
         public DateTime TimeStamp { get; set; }
    }
   
   
}