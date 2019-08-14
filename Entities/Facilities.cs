using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace WebApi.Entities
{
    public class Facilities
    {
        public int Id { get; set; }
        public string Name {get; set;}
        public string Phone {get; set;}
        public string Email {get; set;}
        public string Fax {get; set;}
        public string SystemNoteKey {get; set;}
        public string Extension {get; set;}
        public string NPI {get; set;}
        public string TaxId {get; set;}
        public string Address_1 {get; set;}
        public string Address_2 {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string Zip {get; set;}
        public string PaytoAddress_1 {get; set;}
        public string PaytoAddress_2 {get; set;}
        public string PaytoCity {get; set;}
        public string PaytoState {get; set;}
        public string PaytoZip {get; set;}
        public string ContactName {get; set;}
        public string BillUnder {get; set;}
    }
}