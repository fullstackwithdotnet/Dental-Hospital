// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.StudentScheduleViewModel
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
  public class StudentScheduleViewModel : EntityBase
  {
    [Key]
    public int StudentSchId { get; set; }

    public int StudentId { get; set; }

    [Display(Name = "Department")]
    public int DeptId { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [Display(Name = "From Date")]
    [DataType(DataType.Date)]
    public DateTime SchFromDate { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [Display(Name = "To Date")]
    [DataType(DataType.Date)]
    public DateTime SchToDate { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public string DelInd { get; set; }

    public IEnumerable<MASDepartment> Departmentlist { get; set; }

    [DisplayName("Course")]
    public int SelectStudentCourseId { get; set; }

    public string CourseName { get; set; }

    public List<SelectListItem> SelectCourseList { get; set; }

    [DisplayName("Department")]
    public string DeptName { get; set; }

    public IEnumerable<MASDepartment> SelectDepartmentlist { get; set; }

    [DisplayName("Year")]
    public int SelectStudentYearId { get; set; }

    public string CourseYearName { get; set; }

    public List<SelectListItem> SelectYearlist { get; set; }

    public IEnumerable<int> FromSelectStudent { get; set; }

    public IEnumerable<int> ToSelectStudent { get; set; }

    public string SelectedStudents { get; set; }

    public IEnumerable<MASStudentRegistration> FromStudentList { get; set; }

    public IEnumerable<MASStudentRegistration> ToStudentList { get; set; }

    public IEnumerable<StudentScheduleViewModel> SearchDetails { get; set; }

    public string SchFromDateDisplay { get; set; }

    public string SchToDateDisplay { get; set; }

    public string StudentName { get; set; }

    public string StudentRegNo { get; set; }

    [DisplayName("Already Schedule other department")]
    public bool SearchOtherDept { get; set; }
  }
}
