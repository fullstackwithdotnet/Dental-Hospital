// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PrescriptionsViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class PrescriptionsViewModel : EntityBase
  {
    [PrimaryKey]
    public int PrescriptionId { get; set; }

    public DateTime? PrescriptionDate { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public int StudentId { get; set; }

    public int DeptId { get; set; }

    public long AllotId { get; set; }

    public int ReferredTreatmentId { get; set; }

    [Display(Name = "Type")]
    public int TypeId { get; set; }

    [Display(Name = "Medication")]
    public string PresMedication { get; set; }

    [Display(Name = "Strength")]
    public string PresStrength { get; set; }

    [Display(Name = "Qty")]
    public Decimal PrescriptionQty { get; set; }

    [Display(Name = "Frequency")]
    public string PresFrequency { get; set; }

    [Display(Name = "Times")]
    public string PresTimes { get; set; }

    [Display(Name = "Days")]
    public string PresDays { get; set; }

    [Display(Name = "Notes")]
    public string PresNotes { get; set; }

    [Display(Name = "Date")]
    public string DisplayDate { get; set; }

    public string Department { get; set; }

    public string Doctor { get; set; }

    public string PatientName { get; set; }

    [Display(Name = "Age/Gender")]
    public string AgeGender { get; set; }

    public string Age { get; set; }

    public string GenderName { get; set; }

    public string TypeName { get; set; }

    public string DoctorSignature { get; set; }

    public string Link { get; set; }

    public IEnumerable<MASCode> Typelist { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsList { get; set; }
  }
}
