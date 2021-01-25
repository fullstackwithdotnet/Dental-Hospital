// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OPDPatientRegistrationViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Enums;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.ViewModels
{
    public class OPDPatientRegistrationViewModel : EntityBase
    {



        private DateTime _RegDate = DateTime.Now;
        private DateTime _Dob = DateTime.Now;

        [PrimaryKey] public int PatientId { get; set; }

        [Display(Name = "OP #")] public long OpNo { get; set; }

        public string MandatoryDummy { get; set; }

        [Display(Name = "Reg.Date & Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime RegDate
        {
            get { return this._RegDate; }
            set { this._RegDate = value; }
        }

        [Display(Name = "Reg.Date")] public string RegDateDisplay { get; set; }

        public string RegistrationDate { get; set; }

        [Required]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Display(Name = "Father/Husband Name")]
        public string FatherOrHusband { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime Dob
        {
            get { return this._Dob; }
            set { this._Dob = value; }
        }

        public string DobDisplay { get; set; }

        public int? Age { get; set; }

        [Display(Name = "Phone")] public string Phone { get; set; }

        public string CountryCode { get; set; }

        [Display(Name = "Alter Phone")] public string AlterPhone { get; set; }

        public string Area { get; set; }

        public string Address { get; set; }

        [Display(Name = "Country")] public int CountryId { get; set; }

        public string CountryName { get; set; }

        [Display(Name = "State")] public int StateId { get; set; }

        public string StateName { get; set; }

        [Display(Name = "City")] public int CityId { get; set; }

        public string CityName { get; set; }

        public string PinCode { get; set; }

        [Display(Name = "Aadhaar No.")] public string AadharNo { get; set; }

        public string AadharNo1 { get; set; }

        public string AadharNo2 { get; set; }

        public string AadharNo3 { get; set; }

        [Display(Name = "Occupation")] public string Occupation { get; set; }

        public string Education { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        public string Birthmark { get; set; }

        [Display(Name = "Discount (%)")] public Decimal DiscountPer { get; set; }

        [Display(Name = "Fees")] public Decimal TotalAmt { get; set; }

        [Display(Name = "Net Amount")] public Decimal NetAmt { get; set; }

        [Display(Name = "Paid Amount")] public Decimal PaidAmt { get; set; }

        [Display(Name = "MLC Details")] public string MLCProblem { get; set; }

        public string IsDummy { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedSystem { get; set; }

        public string ModifiedSystem { get; set; }

        public long ReferredId { get; set; }

        public string TreatmentStatus { get; set; }

        [Display(Name = "Service")] public int ServiceId { get; set; }

        public int BillId { get; set; }

        public int BillDetId { get; set; }

        public int BillQueueId { get; set; }

        public int BillPayId { get; set; }

        public string BillDateTime { get; set; }

        public IEnumerable<MASBillingServices> BillDropServicesList { get; set; }

        [DisplayName("Gender")] public Gender Gender { get; set; }

        public IEnumerable<SelectListItem> GenderLister { get; set; }

        public int? GenderId { get; set; }

        [Display(Name = "Marital Status")] public MaritalStatus MaritalStatus { get; set; }

        public IEnumerable<SelectListItem> MaritalStatusLister { get; set; }

        public int? MaritalStatusId { get; set; }

        public Religion Religion { get; set; }

        public IEnumerable<SelectListItem> ReligionLister { get; set; }

        [Display(Name = "Religion")] public int? ReligionId { get; set; }

        public string BloodGroupName { get; set; }

        public IEnumerable<MASCode> BloodGrouplist { get; set; }

        [Display(Name = "Blood Group")] public int? BloodGroupId { get; set; }

        public IList<SelectListItem> Countries { get; set; }

        public IList<SelectListItem> States { get; set; }

        public IList<SelectListItem> Cities { get; set; }

        [DisplayName("Category")] public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<MASCategory> Categorylist { get; set; }

        [DisplayName("Mode of Payment")] public int PaymodeId { get; set; }

        public string PaymodeName { get; set; }

        public IEnumerable<MASPaymode> Paymodelist { get; set; }

        [DisplayName("Title")] public int? TitleId { get; set; }

        public string TitleName { get; set; }

        public IEnumerable<MASCode> Titlelist { get; set; }

        public IEnumerable<OPDPatientRegistrationProperties> Proplist { get; set; }

        [DisplayName("File Name")] public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileDisplayPath { get; set; }

        public string Description { get; set; }

        [Display(Name = "Upload File")] public HttpPostedFileBase[] postedFiles { get; set; }

        public string CategoryEditYN { get; set; }

        public int DCardAge { get; set; }

        public string DCardArea { get; set; }

        public string DCardPhone { get; set; }

        public int DepartmentId { get; set; }

        public bool IsCreated { get; set; }

        public OPDPatientRegistrationViewModel()
        {
            this.CategoryEditYN = "Y";
            this.DepartmentId = (int) Department.OPD;
            if (this.GenderLister == null)
                this.GenderLister =
                    ((IEnumerable<string>) Enum.GetNames(typeof(Gender))).Select<string, SelectListItem>(
                        (Func<string, SelectListItem>) (name => new SelectListItem()
                        {
                            Text = name,
                            Value = name
                        }));
            if (this.MaritalStatusLister == null)
                this.MaritalStatusLister =
                    ((IEnumerable<string>) Enum.GetNames(typeof(MaritalStatus))).Select<string, SelectListItem>(
                        (Func<string, SelectListItem>) (name => new SelectListItem()
                        {
                            Text = name,
                            Value = name
                        }));
            if (this.ReligionLister == null)
                this.ReligionLister =
                    ((IEnumerable<string>) Enum.GetNames(typeof(Religion))).Select<string, SelectListItem>(
                        (Func<string, SelectListItem>) (name => new SelectListItem()
                        {
                            Text = name,
                            Value = name
                        }));
            this.Countries = (IList<SelectListItem>) new List<SelectListItem>();
            this.States = (IList<SelectListItem>) new List<SelectListItem>();
            this.Cities = (IList<SelectListItem>) new List<SelectListItem>();
        }
    }
}
