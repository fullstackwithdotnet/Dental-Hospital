// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.GroupViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class GroupViewModel : EntityBase
  {
    [PrimaryKey]
    public int GroupId { get; set; }

    [DisplayName("Group Name")]
    public string GroupName { get; set; }

    public int DeptId { get; set; }

    [DisplayName("Department")]
    public string DeptName { get; set; }

    [Display(Name = "Radiology")]
    public bool rdnRadiology { get; set; }

    [Display(Name = "Oral Pathology")]
    public bool rdnOralPathology { get; set; }

    public bool rdnSelect { get; set; }

    public IEnumerable<MASDepartment> DepartmentList { get; set; }
  }
}
