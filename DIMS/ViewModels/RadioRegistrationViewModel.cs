// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.RadioRegistrationViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class RadioRegistrationViewModel : EntityBase
  {
    public int LabId { get; set; }

    public int LabDetId { get; set; }

    public int RadioTempDetId { get; set; }

    [Display(Name = "Test Items ")]
    public string ServiceItems { get; set; }

    [Display(Name = "Radiology #")]
    public string LabNo { get; set; }

    public int BillId { get; set; }

    public int PatientId { get; set; }

    public string CreatedBy { get; set; }

    [Display(Name = "Date")]
    public string CreatedDateDisplay { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [Display(Name = "Invoice #")]
    public string BillNo { get; set; }

    [Display(Name = "Invoice Date ")]
    public string BillDateTimeDisplay { get; set; }

    [Display(Name = "OP #")]
    public long OpNo { get; set; }

    [Display(Name = "Patient Name ")]
    public string PatientName { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    public int DeptId { get; set; }

    [DisplayName("Department  ")]
    public string DeptCode { get; set; }

    [Display(Name = "Tooth No ")]
    public string TeethNo { get; set; }

    public int ServiceId { get; set; }

    [Display(Name = "Test Names ")]
    public string ServiceName { get; set; }

    public DateTime TestDate { get; set; }

    public int GroupId { get; set; }

    [Display(Name = "Group  ")]
    public string GroupName { get; set; }

    [Display(Name = "Test Done  ")]
    public string TestDone { get; set; }

    [DisplayName("Age/Gender")]
    public string AgeGender { get; set; }

    public string Gender { get; set; }

    [DisplayName("Date of Receiving")]
    public string CreatedDateReceiving { get; set; }

    [DisplayName("Date of Dispatch")]
    public string CreatedDateDispatch { get; set; }

    public string IsHeader { get; set; }

    public int ResultId { get; set; }

    public int ResultDetId { get; set; }

    public int RadioTempId { get; set; }

    public string Impression { get; set; }

    public string Remarks { get; set; }

    public string Result { get; set; }

    [Display(Name = "Area Radiographed")]
    [DataType(DataType.MultilineText)]
    public string AreaRadiographed { get; set; }

    public string Root { get; set; }

    [Display(Name = "Normal Land Marks")]
    [DataType(DataType.MultilineText)]
    public string NormalLandMarks { get; set; }

    [Display(Name = "Radiographic Fault (If Any)")]
    [DataType(DataType.MultilineText)]
    public string RadiographicFault { get; set; }

    [Display(Name = "Radiographic Diagnosis")]
    [DataType(DataType.MultilineText)]
    public string RadiographicDiagnosis { get; set; }

    [DataType(DataType.MultilineText)]
    public string Grade { get; set; }

    [Display(Name = "File Type")]
    public string FileTypeName { get; set; }

    [Display(Name = "File Name")]
    public string FileName { get; set; }

    public string FileDisplayPath { get; set; }

    public long FileId { get; set; }

    public int TestDetId { get; set; }

    public IEnumerable<RadioRegistrationViewModel> TestNameList { get; set; }

    public IEnumerable<RadioRegistrationViewModel> TestNameEditList { get; set; }

    public IEnumerable<RadioRegistrationViewModel> TestItemList { get; set; }

    public RadioRegistrationViewModel radioregistrationViewModel { get; set; }

    public FileUploadViewModel fileuploadviewmodel { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public IEnumerable<RadioRegistrationViewModel> TestNameReportList { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }

    public IEnumerable<RadioRegistrationViewModel> FileUploadlistforRadiography { get; set; }

    public IEnumerable<RADIORegistration> RadioList { get; set; }

    public IEnumerable<RadioRegistrationViewModel> RadioDetforRadiography { get; set; }

    public IEnumerable<RadioRegistrationViewModel> RadioHeaderforRadiography { get; set; }
  }
}
