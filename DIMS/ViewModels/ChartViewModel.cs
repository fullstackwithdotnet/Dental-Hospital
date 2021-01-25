// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.ChartViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class ChartViewModel
  {
    public string RegDate { get; set; }

    public string Patients { get; set; }

    [Display(Name = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateRange { get; set; }

    public string FromDateValue { get; set; }

    public string ToDateValue { get; set; }

    [Display(Name = "Department")]
    public int DeptId { get; set; }

    public string DeptName { get; set; }

    public IEnumerable<MASDepartment> deptlist { get; set; }
  }
}
