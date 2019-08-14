using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class ScheduleTime
    {

        [Key]
        public string ScheduleId { get; set; }
        public DateTime? Date {get; set;}
        public string DoctorsName {get; set;}
        public string FacilityName {get; set;}
        public string PrimaryInsurance {get; set;}
        public string SecondaryInsurance {get; set;}
        public string TransportationService {get; set;}
        public string Authorization {get; set;}
        public string MemberID{get; set;}
        public string Email {get; set;}
        public string Time {get; set;}
        public string FirstName {get; set;}
        public string MiddleName {get; set;}
        public string LastName {get; set;}
        public string Address {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string Zip {get; set;}
        public string PatientId {get; set;}
        public string Priority {get; set;}
        public string PhoneNumber{get; set;}
        public string Destination {get; set;}
        public string DXCode {get; set;}
        public string ProvidersId {get; set;}
        public string FacilityId {get; set;}
        public string TypeOfVehicle {get; set;}
        public string FacilityAddress {get; set;}
        public int? DoctorsId {get; set;}
        public string PatientAddress {get; set;}
        public string DoctorsAddress {get; set;}
        public double? DocLattitude {get; set;}
        public double? DocLongitude {get; set;}
        public double? PatientLongitude {get; set;}
        public double? PatientLattitude {get; set;}


    }
}
