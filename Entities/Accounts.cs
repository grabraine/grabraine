using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace WebApi.Entities
{
    public class Accounts
    {
        public int Id { get; set; }
        public string Name {get; set;}
        public DateTime DateCreated {get; set;}
        public bool Inactive {get; set;}
        public string SystemNoteKey {get; set;}
    }
}