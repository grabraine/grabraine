using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace WebApi.Entities
{
    public class TimeReport
    {
        public string Id { get; set; }
        public string PatientId {get; set;}
        public string LastName {get; set;}
        public string FirstName {get; set;}
        public DateTime Date {get; set;}
        public string TimePickup {get; set;}
        public string TimeDropOff {get; set;}
        public string SystemNoteKey {get; set;}
        public string Amount {get; set;}
    }
}