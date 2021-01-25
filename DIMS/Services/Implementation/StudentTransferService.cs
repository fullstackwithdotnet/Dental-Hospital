// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.StudentTransferService
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
  public class StudentTransferService : ServiceBase<MASStudentRegistration>, IStudentTransferService, IService<MASStudentRegistration>
  {
    private const int CourseId = 1;
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;

    public StudentTransferService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
    }

    public StudentTransferViewModel BindStudentTransferModel()
    {
      StudentTransferViewModel transferViewModel = new StudentTransferViewModel()
      {
        CourseList = this.GetCourse(),
        Batchlist = this._Dropdownservice.GetCodesById(275)
      };
      transferViewModel.Yearlist = this.GetCourseYearById(Convert.ToInt32(transferViewModel.CourseList.FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected)).Value));
      transferViewModel.Departmentlist = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadDept, (object) 0));
      transferViewModel.FromStudentList = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.StudentScheduleFromDept, (object) 0, (object) 0));
      transferViewModel.ToBatchlist = this._Dropdownservice.GetCodesById(275);
      transferViewModel.ToYearlist = this.GetCourseYearById(Convert.ToInt32(transferViewModel.CourseList.FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected)).Value));
      transferViewModel.ToStudentList = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.StudentScheduleFromDept, (object) 0, (object) 0));
      transferViewModel.ToStudentList = transferViewModel.FromStudentList;
      return transferViewModel;
    }

    public int UpdateStudentTransfer(StudentTransferViewModel model)
    {
      MASStudentRegistration studentRegistration = new MASStudentRegistration();
      MASStudentRegistration entity = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<StudentTransferViewModel, MASStudentRegistration>())).CreateMapper().Map<StudentTransferViewModel, MASStudentRegistration>(model);
      entity.StudentYearId = model.ToStudentYearId;
      entity.BatchTypeId = model.ToCodeId;
      this.Update(entity);
      return model.StudentId;
    }

    public List<SelectListItem> GetAllStudent(string StudentCourseId, string StudentYearId, int DeptId, string BatchTypeId)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      if (Convert.ToInt16(StudentCourseId) == (short) 1)
        selectListItemList = BatchTypeId != null ? this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.GetStudentDetailsbyBatchId, (object) StudentCourseId, (object) StudentYearId, (object) BatchTypeId)).Select<MASStudentRegistration, SelectListItem>((Func<MASStudentRegistration, SelectListItem>) (x => new SelectListItem()
        {
          Text = x.StudentName,
          Value = x.StudentId.ToString()
        })).ToList<SelectListItem>() : this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.LoadAllStudentInDept, (object) StudentCourseId, (object) StudentYearId)).Select<MASStudentRegistration, SelectListItem>((Func<MASStudentRegistration, SelectListItem>) (x => new SelectListItem()
        {
          Text = x.StudentName,
          Value = x.StudentId.ToString()
        })).ToList<SelectListItem>();
      else if (DeptId != 0)
        selectListItemList = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.GetMDSStudentbyDeptId, (object) StudentCourseId, (object) StudentYearId, (object) DeptId)).Select<MASStudentRegistration, SelectListItem>((Func<MASStudentRegistration, SelectListItem>) (x => new SelectListItem()
        {
          Text = x.StudentName,
          Value = x.StudentId.ToString()
        })).ToList<SelectListItem>();
      return selectListItemList;
    }

    private List<SelectListItem> GetCourse()
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      foreach (MASCourse masCourse in (IEnumerable<MASCourse>) this._uow.Repository<MASCourse>().GetAll(string.Format("delInd=0 and CourseId not in ({0})", (object) 4)).ToList<MASCourse>())
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
      List<SelectListItem> list = this._uow.Repository<MASCourseYear>().GetAll(string.Format("delInd=0 and CourseId= {0} ", (object) id)).Select<MASCourseYear, SelectListItem>((Func<MASCourseYear, SelectListItem>) (x => new SelectListItem()
      {
        Text = x.CourseYearName,
        Value = x.CourseYearId.ToString()
      })).ToList<SelectListItem>();
      if (list.Count<SelectListItem>() > 0)
        list.First<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
      return list;
    }
  }
}
