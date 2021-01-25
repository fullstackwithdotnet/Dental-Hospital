// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.StudentScheduleService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class StudentScheduleService : ServiceBase<StudentSchedule>, IStudentScheduleService, IService<StudentSchedule>
  {
    private const int CourseId = 1;
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;

    public StudentScheduleService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
    }

    public StudentScheduleViewModel BindSearch()
    {
      return new StudentScheduleViewModel()
      {
        SelectDepartmentlist = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadDept, (object) 0)),
        SchFromDate = DateTime.Now,
        SchToDate = DateTime.Now
      };
    }

    public IEnumerable<StudentScheduleViewModel> SearchDetails(string From_Date, string To_Date, int DeptId)
    {
      return this._uow.Repository<StudentScheduleViewModel>().GetEntitiesBySql(string.Format(Queries.StudentScheduleSearch, (object) From_Date, (object) To_Date, (object) DeptId));
    }

    public StudentScheduleViewModel BindInitialViewModel()
    {
      StudentScheduleViewModel scheduleViewModel = new StudentScheduleViewModel()
      {
        SelectCourseList = this.GetCourse(1),
        SelectYearlist = this.GetCourseYearById(1),
        SelectStudentCourseId = 1,
        SelectStudentYearId = 1,
        SelectDepartmentlist = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadDept, (object) 0)),
        FromStudentList = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.StudentScheduleFromDept, (object) 0, (object) 0)),
        ToStudentList = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.StudentScheduleFromDept, (object) 0, (object) 0))
      };
      scheduleViewModel.ToStudentList = scheduleViewModel.FromStudentList;
      scheduleViewModel.SchFromDate = DateTime.Now;
      scheduleViewModel.SchToDate = DateTime.Now;
      return scheduleViewModel;
    }

    public List<SelectListItem> GetFromStudent(string StudentCourseId, string StudentYearId, string SchFromDate, string SchToDate, string DeptId)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      return this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.LoadStudentScheduleFrom, (object) StudentCourseId, (object) StudentYearId, (object) SchFromDate, (object) SchToDate, (object) DeptId)).Select<MASStudentRegistration, SelectListItem>((Func<MASStudentRegistration, SelectListItem>) (x => new SelectListItem()
      {
        Text = x.StudentName,
        Value = x.StudentId.ToString()
      })).ToList<SelectListItem>();
    }

    public List<SelectListItem> GetToStudent(string StudentCourseId, string StudentYearId, string SchFromDate, string SchToDate, string DeptId)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      return this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.LoadStudentScheduleTo, (object) StudentCourseId, (object) StudentYearId, (object) SchFromDate, (object) SchToDate, (object) DeptId)).Select<MASStudentRegistration, SelectListItem>((Func<MASStudentRegistration, SelectListItem>) (x => new SelectListItem()
      {
        Text = x.StudentName,
        Value = x.StudentId.ToString()
      })).ToList<SelectListItem>();
    }

    public List<SelectListItem> GetAllStudent(string StudentCourseId, string StudentYearId, string SchFromDate, string SchToDate, string DeptId)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      return this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.LoadAllStudentInDept, (object) StudentCourseId, (object) StudentYearId)).Select<MASStudentRegistration, SelectListItem>((Func<MASStudentRegistration, SelectListItem>) (x => new SelectListItem()
      {
        Text = x.StudentName,
        Value = x.StudentId.ToString()
      })).ToList<SelectListItem>();
    }

    public List<StudentScheduleDisplayViewModel> DisplayStudentSchedule(string StudentCourseId, string StudentYearId)
    {
      List<StudentScheduleDisplayViewModel> displayViewModelList = new List<StudentScheduleDisplayViewModel>();
      return this._uow.Repository<StudentScheduleDisplayViewModel>().GetEntitiesBySql(string.Format(Queries.LoadStudentScheduleddetails, (object) StudentCourseId, (object) StudentYearId)).ToList<StudentScheduleDisplayViewModel>();
    }

    public int SaveStudentSchedule(StudentScheduleViewModel model)
    {
      StudentSchedule studentSchedule = new StudentSchedule();
      return this.Add(new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<StudentScheduleViewModel, StudentSchedule>())).CreateMapper().Map<StudentScheduleViewModel, StudentSchedule>(model));
    }

    private List<SelectListItem> GetCourse(int id)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      foreach (MASCourse masCourse in (IEnumerable<MASCourse>) this._uow.Repository<MASCourse>().GetAll("CourseId =" + (object) id).ToList<MASCourse>())
      {
        if (masCourse.CourseId == 1)
          selectListItemList.Add(new SelectListItem()
          {
            Text = masCourse.CourseName,
            Value = Convert.ToString(masCourse.CourseId),
            Selected = true
          });
        else
          selectListItemList.Add(new SelectListItem()
          {
            Text = masCourse.CourseName,
            Value = Convert.ToString(masCourse.CourseId)
          });
      }
      return selectListItemList;
    }

    public List<SelectListItem> GetCourseYearById(int id)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      List<SelectListItem> list = this._uow.Repository<MASCourseYear>().GetAll(string.Format("CourseId= {0}", (object) id)).Select<MASCourseYear, SelectListItem>((Func<MASCourseYear, SelectListItem>) (x => new SelectListItem()
      {
        Text = x.CourseYearName,
        Value = x.CourseYearId.ToString()
      })).ToList<SelectListItem>();
      list.First<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
      return list;
    }
  }
}
