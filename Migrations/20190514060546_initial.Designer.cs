﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using WebApi.Helpers;

namespace WebApi.Migrations
{
    [DbContext(typeof(ArisContext))]
    [Migration("20190514060546_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview1-28290")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Entities.Facilities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address_1");

                    b.Property<string>("Address_2");

                    b.Property<string>("BillUnder");

                    b.Property<string>("City");

                    b.Property<string>("ContactName");

                    b.Property<string>("Email");

                    b.Property<string>("Extension");

                    b.Property<string>("Fax");

                    b.Property<string>("NPI");

                    b.Property<string>("Name");

                    b.Property<string>("PaytoAddress_1");

                    b.Property<string>("PaytoAddress_2");

                    b.Property<string>("PaytoCity");

                    b.Property<string>("PaytoState");

                    b.Property<string>("PaytoZip");

                    b.Property<string>("Phone");

                    b.Property<string>("State");

                    b.Property<string>("SystemNoteKey");

                    b.Property<string>("TaxId");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("WebApi.Entities.Payers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address_1");

                    b.Property<string>("Address_2");

                    b.Property<string>("City");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("PayerId");

                    b.Property<string>("Phone_1");

                    b.Property<string>("Phone_2");

                    b.Property<string>("State");

                    b.Property<string>("SystemNoteKey");

                    b.Property<string>("Type");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Payers");
                });

            modelBuilder.Entity("WebApi.Entities.Providers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address_1");

                    b.Property<string>("Address_2");

                    b.Property<string>("City");

                    b.Property<string>("Credential");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("EIN");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsCompany");

                    b.Property<bool>("IsReferrer");

                    b.Property<string>("LastName");

                    b.Property<string>("License");

                    b.Property<string>("MiddleName");

                    b.Property<string>("NPI");

                    b.Property<string>("Phone_1");

                    b.Property<string>("Phone_2");

                    b.Property<string>("SSN");

                    b.Property<string>("State");

                    b.Property<string>("Suffix");

                    b.Property<string>("SystemNoteKey");

                    b.Property<int?>("TaxonomyCodeId");

                    b.Property<string>("UPIN");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("WebApi.Entities.Sched", b =>
                {
                    b.Property<string>("ScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Authorization");

                    b.Property<string>("DXCode");

                    b.Property<DateTime?>("Date");

                    b.Property<string>("Destination");

                    b.Property<string>("DistanceInMiles");

                    b.Property<string>("DoctorsAddress");

                    b.Property<int?>("DoctorsId");

                    b.Property<string>("DoctorsName");

                    b.Property<double?>("DropOffLattitude");

                    b.Property<double?>("DropOffLongitude");

                    b.Property<string>("FacilityAddress");

                    b.Property<string>("FacilityId");

                    b.Property<string>("FacilityName");

                    b.Property<string>("PatientAddress");

                    b.Property<string>("PatientId");

                    b.Property<string>("Patient_Address");

                    b.Property<string>("Patient_City");

                    b.Property<string>("Patient_Email");

                    b.Property<string>("Patient_FirstName");

                    b.Property<string>("Patient_LastName");

                    b.Property<string>("Patient_MemberID");

                    b.Property<string>("Patient_MiddleName");

                    b.Property<string>("Patient_State");

                    b.Property<string>("Patient_Zip");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Pickup");

                    b.Property<double?>("PickupLattitude");

                    b.Property<double?>("PickupLongitude");

                    b.Property<string>("PrimaryInsurance");

                    b.Property<string>("Priority");

                    b.Property<string>("ProvidersId");

                    b.Property<string>("SecondaryInsurance");

                    b.Property<string>("Time");

                    b.Property<string>("TransportationService");

                    b.Property<string>("TypeOfVehicle");

                    b.Property<string>("TypeOfVisit");

                    b.HasKey("ScheduleId");

                    b.ToTable("Sched");
                });

            modelBuilder.Entity("WebApi.Entities.Sched2", b =>
                {
                    b.Property<string>("ScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Authorization");

                    b.Property<string>("DXCode");

                    b.Property<DateTime?>("Date");

                    b.Property<string>("Destination");

                    b.Property<string>("DistanceInMiles");

                    b.Property<string>("DoctorsAddress");

                    b.Property<int?>("DoctorsId");

                    b.Property<string>("DoctorsName");

                    b.Property<double?>("DropOffLattitude");

                    b.Property<double?>("DropOffLongitude");

                    b.Property<string>("EnteredBy");

                    b.Property<string>("FacilityAddress");

                    b.Property<string>("FacilityId");

                    b.Property<string>("FacilityName");

                    b.Property<string>("PatientAddress");

                    b.Property<string>("PatientId");

                    b.Property<string>("Patient_Address");

                    b.Property<string>("Patient_City");

                    b.Property<string>("Patient_Email");

                    b.Property<string>("Patient_FirstName");

                    b.Property<string>("Patient_LastName");

                    b.Property<string>("Patient_MemberID");

                    b.Property<string>("Patient_MiddleName");

                    b.Property<string>("Patient_State");

                    b.Property<string>("Patient_Zip");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Pickup");

                    b.Property<double?>("PickupLattitude");

                    b.Property<double?>("PickupLongitude");

                    b.Property<string>("PrimaryInsurance");

                    b.Property<string>("Priority");

                    b.Property<string>("ProvidersId");

                    b.Property<string>("SecondaryInsurance");

                    b.Property<string>("Time");

                    b.Property<string>("TransportationService");

                    b.Property<string>("TypeOfVehicle");

                    b.Property<string>("TypeOfVisit");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<int>("User_id");

                    b.HasKey("ScheduleId");

                    b.ToTable("Sched2");
                });

            modelBuilder.Entity("WebApi.Entities.ScheduleData", b =>
                {
                    b.Property<string>("ScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Authorization");

                    b.Property<string>("City");

                    b.Property<string>("DXCode");

                    b.Property<DateTime?>("Date");

                    b.Property<string>("Destination");

                    b.Property<double?>("DocLattitude");

                    b.Property<double?>("DocLongitude");

                    b.Property<string>("DoctorsAddress");

                    b.Property<int?>("DoctorsId");

                    b.Property<string>("DoctorsName");

                    b.Property<string>("Email");

                    b.Property<string>("FacilityAddress");

                    b.Property<string>("FacilityId");

                    b.Property<string>("FacilityName");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MemberID");

                    b.Property<string>("MiddleName");

                    b.Property<string>("PatientAddress");

                    b.Property<string>("PatientId");

                    b.Property<double?>("PatientLattitude");

                    b.Property<double?>("PatientLongitude");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("PrimaryInsurance");

                    b.Property<string>("Priority");

                    b.Property<string>("ProvidersId");

                    b.Property<string>("SecondaryInsurance");

                    b.Property<string>("State");

                    b.Property<string>("Time");

                    b.Property<string>("TransportationService");

                    b.Property<string>("TypeOfVehicle");

                    b.Property<string>("Zip");

                    b.HasKey("ScheduleId");

                    b.ToTable("ScheduleData");
                });

            modelBuilder.Entity("WebApi.Entities.Subscribers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address_1");

                    b.Property<string>("Address_2");

                    b.Property<string>("City");

                    b.Property<DateTime?>("DateModified");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<DateTime?>("DateOfDeath");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("GroupName");

                    b.Property<string>("GroupNumber");

                    b.Property<bool>("IsDeceased");

                    b.Property<bool>("IsPregnant");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<string>("Notes");

                    b.Property<string>("Phone_1");

                    b.Property<string>("Phone_2");

                    b.Property<string>("PrimaryExpiryDate");

                    b.Property<int?>("PrimaryInsurerId");

                    b.Property<string>("PrimaryMemberId");

                    b.Property<int?>("PrimaryPayerId");

                    b.Property<string>("PrimaryPayerType");

                    b.Property<string>("SSN");

                    b.Property<string>("SecondaryExpiryDate");

                    b.Property<int?>("SecondaryInsurerId");

                    b.Property<string>("SecondaryMemberId");

                    b.Property<int?>("SecondaryPayerId");

                    b.Property<string>("SecondaryPayerType");

                    b.Property<string>("State");

                    b.Property<string>("Suffix");

                    b.Property<string>("SystemNoteKey");

                    b.Property<decimal?>("Weight");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("WebApi.Entities.TimeReport", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PatientId");

                    b.Property<string>("SystemNoteKey");

                    b.Property<string>("TimeDropOff");

                    b.Property<string>("TimePickup");

                    b.HasKey("Id");

                    b.ToTable("TimeReport");
                });
#pragma warning restore 612, 618
        }
    }
}
