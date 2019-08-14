using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "Sched2",
                columns: table => new
                {
                    ScheduleId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    DoctorsName = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    PrimaryInsurance = table.Column<string>(nullable: true),
                    SecondaryInsurance = table.Column<string>(nullable: true),
                    TransportationService = table.Column<string>(nullable: true),
                    Authorization = table.Column<string>(nullable: true),
                    Patient_MemberID = table.Column<string>(nullable: true),
                    Patient_Email = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    Patient_FirstName = table.Column<string>(nullable: true),
                    Patient_MiddleName = table.Column<string>(nullable: true),
                    Patient_LastName = table.Column<string>(nullable: true),
                    Patient_Address = table.Column<string>(nullable: true),
                    Patient_City = table.Column<string>(nullable: true),
                    Patient_State = table.Column<string>(nullable: true),
                    Patient_Zip = table.Column<string>(nullable: true),
                    PatientId = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Pickup = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    DXCode = table.Column<string>(nullable: true),
                    ProvidersId = table.Column<string>(nullable: true),
                    FacilityId = table.Column<string>(nullable: true),
                    TypeOfVehicle = table.Column<string>(nullable: true),
                    FacilityAddress = table.Column<string>(nullable: true),
                    DoctorsId = table.Column<int>(nullable: true),
                    PatientAddress = table.Column<string>(nullable: true),
                    DoctorsAddress = table.Column<string>(nullable: true),
                    DropOffLattitude = table.Column<double>(nullable: true),
                    DropOffLongitude = table.Column<double>(nullable: true),
                    PickupLongitude = table.Column<double>(nullable: true),
                    PickupLattitude = table.Column<double>(nullable: true),
                    DistanceInMiles = table.Column<string>(nullable: true),
                    TypeOfVisit = table.Column<string>(nullable: true),
                    User_id = table.Column<int>(nullable: false),
                    EnteredBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sched2", x => x.ScheduleId);
                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropTable(
                name: "Sched2");

        }
    }
}
