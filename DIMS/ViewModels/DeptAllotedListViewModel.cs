// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.DeptAllotedListViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class DeptAllotedListViewModel : EntityBase
  {
    public long AllotId { get; set; }

    [Display(Name = "Alloted Date")]
    public string AllotDate { get; set; }

    [Display(Name = "AllotTime")]
    public string AllotTime { get; set; }

    [Display(Name = "OP No")]
    public long OpNo { get; set; }

    [Display(Name = "Patient Name")]
    public string PatientName { get; set; }

    public int Age { get; set; }

    [Display(Name = "Student")]
    public string StudentName { get; set; }

    [Display(Name = "Reg No")]
    public string StudentRegNo { get; set; }

    public int PatientId { get; set; }

    public long ReferredId { get; set; }

    public int DeptId { get; set; }

    public int StudentId { get; set; }

    public int GenderId { get; set; }

    public string Link { get; set; }

    public string DeleteLink { get; set; }

        public string DiagnosisLink { get; set; }

        public string Phone { get; set; }
  }
}
