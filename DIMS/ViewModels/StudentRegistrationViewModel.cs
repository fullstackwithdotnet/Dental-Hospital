// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.StudentRegistrationViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class StudentRegistrationViewModel : EntityBase
  {
    [Key]
    public int StudentId { get; set; }

    [Display(Name = "Name")]
    public string StudentName { get; set; }

    [Display(Name = "Reg No")]
    public string StudentRegNo { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public string Active { get; set; }

    public bool IsChkActive { get; set; }

    [DisplayName("Course")]
    public int StudentCourseId { get; set; }

    public string CourseName { get; set; }

    public List<SelectListItem> CourseList { get; set; }

    [DisplayName("Department")]
    public int DeptId { get; set; }

    public string DeptName { get; set; }

    public IEnumerable<MASDepartment> Departmentlist { get; set; }

    [DisplayName("Course Year")]
    public int StudentYearId { get; set; }

    public string StudentYearName { get; set; }

    public string Link { get; set; }

    public List<SelectListItem> Yearlist { get; set; }

    public IEnumerable<StudentRegistrationViewModel> StudentList { get; set; }

    public bool chkStudentId { get; set; }

    [DisplayName("Date")]
    public string DisplayDate { get; set; }

    public int BatchTypeId { get; set; }

    public string Batch1 { get; set; }

    public string Batch2 { get; set; }

    public string Batch { get; set; }

    public int CodeId { get; set; }

    [DisplayName("Batch")]
    public string CodeDescription { get; set; }

    public IEnumerable<MASCode> Batchlist { get; set; }

    public IEnumerable<MASCourse> CourseIndexlist { get; set; }
  }

    public class DtoStudentModel
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string admission_no{ get;set;}
        public string section { get; set; }
    }
}
