// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.LaboratoryRegistrationViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class LaboratoryRegistrationViewModel : EntityBase
  {
    private DateTime _Date = DateTime.Now;

    [PrimaryKey]
    public int LaboratoryId { get; set; }

    [Display(Name = "Lab #")]
    public string LaboratoryNo { get; set; }

    public int BillId { get; set; }

    public int PatientId { get; set; }

    [Display(Name = "Date of receiving")]
    public string Dateofreceiving { get; set; }

    [Display(Name = "Date")]
    public string SampleCollectedDateDisplay { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "Sample Collected Date")]
    public DateTime SampleCollectedDate
    {
      get
      {
        return this._Date;
      }
      set
      {
        this._Date = value;
      }
    }

    public string DateofDispatch { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Sample Collection Time")]
    public DateTime? SampleCollectedTime { get; set; }

    [Display(Name = "Sample colleted By")]
    public string SampleCollectedBy { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public int LaboratoryDetId { get; set; }

    public int ServiceId { get; set; }

    public string TeethNo { get; set; }

    public string TestDone { get; set; }

    public DateTime? TestDate { get; set; }

    public int LaboratoryResultId { get; set; }

    public int RadioTempId { get; set; }

    public string Impression { get; set; }

    public string Remarks { get; set; }

    public int LaboratoryResultDetId { get; set; }

    public int RadioTempDetId { get; set; }

    public string Result { get; set; }

    [Display(Name = "Invoice # ")]
    public string BillNo { get; set; }

    [Display(Name = "Invoice Date ")]
    public string BillDateTimeDisplay { get; set; }

    [Display(Name = "OP #")]
    public long OpNo { get; set; }

    [Display(Name = " Name ")]
    public string PatientName { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    public int DeptId { get; set; }

    [DisplayName("Department  ")]
    public string DeptCode { get; set; }

    public int GroupId { get; set; }

    [Display(Name = "Group  ")]
    public string GroupName { get; set; }

    [DisplayName("Age/Gender")]
    public string AgeGender { get; set; }

    [Display(Name = "Test Names ")]
    public string ServiceName { get; set; }

    [Display(Name = "Test Items ")]
    public string ServiceItems { get; set; }

    [Display(Name = "Male Normal Range ")]
    public string MaleNormalRange { get; set; }

    [Display(Name = "Female Normal Range ")]
    public string FemaleNormalRange { get; set; }

    public string IsHeader { get; set; }

    [Display(Name = "Unit")]
    public string Unit { get; set; }

    [Display(Name = "Lab No")]
    public string LabNo { get; set; }

    [Display(Name = "File Type")]
    public string FileTypeName { get; set; }

    [Display(Name = "File Name")]
    public string FileName { get; set; }

    public string FileDisplayPath { get; set; }

    public long FileId { get; set; }

    public int TestDetId { get; set; }

    public IEnumerable<LaboratoryRegistrationViewModel> TestNameList { get; set; }

    public IEnumerable<LaboratoryRegistrationViewModel> TestNameEditList { get; set; }

    public IEnumerable<LaboratoryRegistrationViewModel> TestItemList { get; set; }

    public LaboratoryRegistrationViewModel orpathregistrationViewModel { get; set; }

    public FileUploadViewModel fileuploadviewmodel { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public IEnumerable<LaboratoryRegistrationViewModel> TestNameReportList { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }

    public IEnumerable<LaboratoryRegistration> LaboratoryList { get; set; }

    public IEnumerable<LaboratoryRegistrationViewModel> LabDetforLaboratory { get; set; }

    public IEnumerable<LaboratoryRegistrationViewModel> LabHeaderforLaboratory { get; set; }

    public IEnumerable<LaboratoryRegistrationViewModel> FileUploadlistforLaboratory { get; set; }

    public string Gender { get; set; }

    public string Area { get; set; }

    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }
  }
}
