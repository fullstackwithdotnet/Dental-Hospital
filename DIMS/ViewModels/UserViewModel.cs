// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.UserViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Metron.Entities;

namespace DIMS.ViewModels
{
  [Table("User")]
  public class UserViewModel : EntityBase
  {
    [PrimaryKey]
    public int UserId { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "Role")]
    public string RoleName { get; set; }

    public int UserRoleIdPK { get; set; }

    public int RoleId { get; set; }

    public IEnumerable<Role> Roles { get; set; }

    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }

    [Display(Name = "Department")]
    public string DeptName { get; set; }

    [Display(Name = "Department")]
    public string DeptCode { get; set; }

    public int DeptId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int UserDeptId { get; set; }

    public int PermissionId { get; set; }

    public string PermissionName { get; set; }

    public bool PermissionValue { get; set; }

    public bool Add { get; set; }

    public bool Edit { get; set; }

    public bool View { get; set; }

    public bool Discount { get; set; }

    public bool Referral { get; set; }

    public bool Allotment { get; set; }

    public bool CancelAllotment { get; set; }

    public bool Appointment { get; set; }

    public bool AppReschedule { get; set; }

    public bool Investigation { get; set; }

    public bool TreatmentServices { get; set; }

    public bool ProcedureApproval { get; set; }

    public bool CaserecordApproval { get; set; }

    public bool EditFreezeCase { get; set; }

    public IEnumerable<Permission> Permissions { get; set; }

    public IEnumerable<UserDepartments> AssignedDepartments { get; set; }

    public IEnumerable<UserDeptPermissions> DepartmentPermissions { get; set; }

    public IEnumerable<UserViewModel> AssignedPermissions { get; set; }

    public IEnumerable<MASDepartment> DepartmentList { get; set; }

    public IEnumerable<UserViewModel> UserList { get; set; }

    public string Link { get; set; }

    [Display(Name = "New Password")]
    public string NewPassword { get; set; }

    public string Name { get; set; }

    public int SuperAdminId { get; set; }
  }
}
