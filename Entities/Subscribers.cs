using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Subscribers
    {
        
        [Key]
        public int Id {get; set;}
        public string SystemNoteKey {get; set;}
        public string PrimaryMemberId {get; set;}
        public string SecondaryMemberId {get; set;}
        public string PrimaryExpiryDate {get; set;}
        public string SecondaryExpiryDate {get; set;}
        public int? PrimaryPayerId {get; set;}
        public int? SecondaryPayerId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string MiddleName {get; set;}
        public DateTime? DateOfBirth {get; set;}
        public string Gender {get; set;}
        public string Phone_1 {get; set;}
        public string Phone_2 {get; set;}
        public string Email {get; set;}
        public string Fax {get; set;}
        public string Notes {get; set;}
        public string Suffix {get; set;}
        public string Address_1 {get; set;}
        public string Address_2 {get; set;}
        public string City {get; set;}
        public string State{get; set;}
        public string Zip {get; set;}
        public string GroupName {get; set;}
        public string GroupNumber {get; set;}
        public string PrimaryPayerType {get; set;}
        public string SecondaryPayerType{get; set;}
        public bool IsDeceased {get; set;}
        public DateTime? DateOfDeath {get; set;}
        public decimal? Weight {get; set;}
        public bool IsPregnant {get; set;}
        public string SSN {get; set;}
        public DateTime? DateModified {get; set;}
        public int? PrimaryInsurerId {get; set;}
        public int? SecondaryInsurerId {get; set;}
      
         
    }
}
