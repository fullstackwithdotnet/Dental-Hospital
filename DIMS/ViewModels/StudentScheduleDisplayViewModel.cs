// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.StudentScheduleDisplayViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class StudentScheduleDisplayViewModel : EntityBase
  {
    [Key]
    public int StudentSchId { get; set; }

    public int StudentId { get; set; }

    public string StudentName { get; set; }

    public string StudentRegNo { get; set; }

    public string CourseName { get; set; }

    public string CourseYearName { get; set; }

    public string SchFromDate { get; set; }

    public string SchToDate { get; set; }

    public string DeptCode { get; set; }
  }
}
