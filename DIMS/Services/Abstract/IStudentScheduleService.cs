// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IStudentScheduleService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IStudentScheduleService : IService<StudentSchedule>
  {
    List<SelectListItem> GetFromStudent(string StudentCourseId, string StudentYearId, string SchFromDate, string SchToDate, string DeptId);

    List<SelectListItem> GetToStudent(string StudentCourseId, string StudentYearId, string SchFromDate, string SchToDate, string DeptId);

    List<SelectListItem> GetAllStudent(string StudentCourseId, string StudentYearId, string SchFromDate, string SchToDate, string DeptId);

    StudentScheduleViewModel BindInitialViewModel();

    IEnumerable<StudentScheduleViewModel> SearchDetails(string From_Date, string To_Date, int DeptId);

    StudentScheduleViewModel BindSearch();

    List<StudentScheduleDisplayViewModel> DisplayStudentSchedule(string StudentCourseId, string StudentYearId);

    int SaveStudentSchedule(StudentScheduleViewModel model);

    List<SelectListItem> GetCourseYearById(int id);
  }
}
