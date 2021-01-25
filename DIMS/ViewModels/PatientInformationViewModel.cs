// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PatientInformationViewModel
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
  public class PatientInformationViewModel : EntityBase
  {
    public int PatientId { get; set; }

    [Display(Name = "OP #")]
    public long OpNo { get; set; }

    [ReadOnly(true)]
    [Display(Name = "Patient Name")]
    public string PatientName { get; set; }

    [Display(Name = "Address")]
    [DataType(DataType.MultilineText)]
    public string Address { get; set; }

    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    [DisplayName("Age/Gender")]
    public string AgeGender { get; set; }

    public string Area { get; set; }

    [Display(Name = "Student")]
    public string StudentName { get; set; }

    [Display(Name = "Reg #")]
    public string StudentRegNo { get; set; }

    public long ReferredId { get; set; }

    public int DeptId { get; set; }

    public int StudentId { get; set; }

    public int GenderId { get; set; }

    public long AllotId { get; set; }

    public int Age { get; set; }

    [Display(Name = "Doctor")]
    public int? DoctorId { get; set; }

    public string ProcedureNotes { get; set; }

    public string DoctorApproval { get; set; }

    public string DoctorName { get; set; }

    [Display(Name = "Department")]
    public string DeptName { get; set; }

    public IEnumerable<MASDoctor> Doctorlist { get; set; }

    [Display(Name = "Category")]
    public string CategoryName { get; set; }

    [Display(Name = "Alloted To")]
    public string AllotedTo { get; set; }

    [Display(Name = "Due Amt")]
    public Decimal DueAmount { get; set; }
  }
}
