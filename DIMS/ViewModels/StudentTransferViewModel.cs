// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.StudentTransferViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class StudentTransferViewModel : EntityBase
  {
    [PrimaryKey]
    public int StudentId { get; set; }

    [Display(Name = "Department")]
    public int DeptId { get; set; }

    public string DeptName { get; set; }

    [DisplayName("Course")]
    public int SelectStudentCourseId { get; set; }

    public string SelectedStudents { get; set; }

    [DisplayName("Year")]
    public int SelectStudentYearId { get; set; }

    public string CourseYearName { get; set; }

    [DisplayName("Course")]
    public int StudentCourseId { get; set; }

    public string CourseName { get; set; }

    [DisplayName("Course Year")]
    public int StudentYearId { get; set; }

    public string StudentYearName { get; set; }

    public string Batch { get; set; }

    public int? CodeId { get; set; }

    [DisplayName("Course Year")]
    public int ToStudentYearId { get; set; }

    public int ToCodeId { get; set; }

    public string ModifiedBy { get; set; }

    public string CreatedSystem { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string ModifiedSystem { get; set; }

    public IEnumerable<MASCode> Batchlist { get; set; }

    public IEnumerable<MASCode> ToBatchlist { get; set; }

    public List<SelectListItem> Yearlist { get; set; }

    public List<SelectListItem> ToYearlist { get; set; }

    public List<SelectListItem> CourseList { get; set; }

    public IEnumerable<int> FromSelectStudent { get; set; }

    public IEnumerable<int> ToSelectStudent { get; set; }

    public IEnumerable<MASStudentRegistration> FromStudentList { get; set; }

    public IEnumerable<MASStudentRegistration> ToStudentList { get; set; }

    public IEnumerable<MASDepartment> Departmentlist { get; set; }

    public List<SelectListItem> SelectCourseList { get; set; }

    public IEnumerable<MASDepartment> SelectDepartmentlist { get; set; }

    public List<SelectListItem> SelectYearlist { get; set; }
  }
}
