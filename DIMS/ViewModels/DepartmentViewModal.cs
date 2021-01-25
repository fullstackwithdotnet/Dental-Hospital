// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.DepartmentViewModal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class DepartmentViewModal
  {
    public int DeptId { get; set; }

    [Display(Name = "Department Code")]
    public string DeptCode { get; set; }

    [Display(Name = "Department Name")]
    public string DeptName { get; set; }

    public string RoomNo { get; set; }
  }
}
