// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.ReportSearchViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Enums;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class ReportSearchViewModel : EntityBase
  {
    private DateTime _From_Date = DateTime.Now;
    private DateTime _To_Date = DateTime.Now;

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "FROM DATE")]
    public DateTime From_Date
    {
      get
      {
        return this._From_Date;
      }
      set
      {
        this._From_Date = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "TO DATE")]
    public DateTime To_Date
    {
      get
      {
        return this._To_Date;
      }
      set
      {
        this._To_Date = value;
      }
    }

    [Display(Name = "VISIT TYPE")]
    public string VisitType { get; set; }

    public string NewVisitCount { get; set; }

    public string ReVisitCount { get; set; }

    public string TotalCount { get; set; }

    public string MaleNewVisit { get; set; }

    public string FemaleNewVisit { get; set; }

    public string MaleReVisit { get; set; }

    public string FemaleReVisit { get; set; }

    public List<ReportViewModel> SearchDetails { get; set; }

    public IEnumerable<MASDepartment> Departmentlist { get; set; }

    public IEnumerable<MASCourse> CourseIndexlist { get; set; }

    public IEnumerable<MASCategory> categoryList { get; set; }

    public IEnumerable<ReferralStatus> RefList { get; set; }

    public int CategoryId { get; set; }

    public IEnumerable<OPDPatientRegistration> OpdRegList { get; set; }

    [Display(Name = "CATEGORY")]
    public string CategoryName { get; set; }

    [Display(Name = "AREA")]
    public string Area { get; set; }

    [Display(Name = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateRange { get; set; }

    public string FromDateValue { get; set; }

    public string ToDateValue { get; set; }

    [Display(Name = "Department")]
    public int DeptId { get; set; }

    [Display(Name = "Department")]
    public string DeptName { get; set; }

    [Display(Name = "Doctor")]
    public int DoctorId { get; set; }

    public string DoctorName { get; set; }

    [Display(Name = "User")]
    public int UserId { get; set; }

    public string UserName { get; set; }

    [Display(Name = "Course")]
    public int CourseId { get; set; }

    public string CourseName { get; set; }

    public int StudentId { get; set; }

    [Display(Name = "Student")]
    public string StudentName { get; set; }

    public int PatientId { get; set; }

    public IList<SelectListItem> StudentList { get; set; }

    public IList<SelectListItem> CoursesList { get; set; }

    public IList<SelectListItem> DeptList { get; set; }

    public IList<SelectListItem> DoctorList { get; set; }

    public IList<SelectListItem> userList { get; set; }

    [DisplayName("Visit Type")]
    public Visit Visit { get; set; }

    public IEnumerable<SelectListItem> VisitTypeLister { get; set; }

    public ReportSearchViewModel()
    {
      if (this.VisitTypeLister != null)
        return;
      this.VisitTypeLister = ((IEnumerable<string>) Enum.GetNames(typeof (Visit))).Select<string, SelectListItem>((Func<string, SelectListItem>) (name => new SelectListItem()
      {
        Text = name,
        Value = name
      }));
    }
  }
}
