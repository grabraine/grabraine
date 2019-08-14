using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Sched2
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
        public string Patient_MemberID{get; set;}
        public string Patient_Email {get; set;}
        public string Time {get; set;}
        public string Patient_FirstName {get; set;}
        public string Patient_MiddleName {get; set;}
        public string Patient_LastName {get; set;}
        public string Patient_Address {get; set;}
        public string Patient_City {get; set;}
        public string Patient_State {get; set;}
        public string Patient_Zip {get; set;}
        public string PatientId {get; set;}
        public string Priority {get; set;}
        public string PhoneNumber{get; set;}
        public string Pickup {get;set;}
        public string Destination {get; set;}
        public string DXCode {get; set;}
        public string ProvidersId {get; set;}
        public string FacilityId {get; set;}
        public string TypeOfVehicle {get; set;}
        public string FacilityAddress {get; set;}
        public int? DoctorsId {get; set;}
        public string PatientAddress {get; set;}
        public string DoctorsAddress {get; set;}
        public double? DropOffLattitude {get; set;}
        public double? DropOffLongitude {get; set;}
        public double? PickupLongitude {get; set;}
        public double? PickupLattitude {get; set;}
        public string DistanceInMiles {get; set;}
        public string TypeOfVisit {get; set;}
        public int User_id {get; set;}
        public string EnteredBy {get;set;}
        public DateTime? UpdatedDate {get; set;}
        public int? DriverID {get; set;}
        public string DriversFullname {get; set;}

    }
}