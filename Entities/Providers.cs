using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Providers
    {
        public int Id { get; set; }
        public string EIN {get; set;}
        public string SSN {get; set;}
        public string NPI {get; set;}
        public string License {get; set;}
        public string UPIN {get; set;}
        public string SystemNoteKey {get; set;}
        public int? TaxonomyCodeId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string MiddleName {get; set;}
        public string Suffix {get; set;}
        public string Phone_1 {get; set;}
        public string Email {get; set;}
        public string Fax {get; set;}
        public string Phone_2 {get; set;}
        public string Address_1 {get; set;}
        public string Address_2 {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string Zip {get; set;}
        public bool IsCompany {get; set;}
        public DateTime? DateModified {get; set;}
        public bool IsReferrer {get; set;}
        public string Credential {get; set;}
    }
}