// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MASStudentRegistrationService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System.Collections.Generic;
using System.Linq;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class MASStudentRegistrationService : ServiceBase<MASStudentRegistration>, IMASStudentRegistrationService, IService<MASStudentRegistration>
  {
    private IUnitOfWork _uow;

    public MASStudentRegistrationService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<StudentRegistrationViewModel> GetStudentList()
    {
      return this._uow.Repository<StudentRegistrationViewModel>().GetEntitiesBySql("SELECT S.StudentId, S.StudentName, S.StudentRegNo, C.CourseName as CourseName, Y.CourseYearName as  " + "StudentYearName, D.DeptCode as DeptName FROM MASStudentRegistration AS S INNER JOIN " + "MASCourse AS C ON S.StudentCourseId = C.CourseId INNER JOIN  " + "MASCourseYear AS Y ON S.StudentYearId = Y.CourseYearId LEFT OUTER JOIN " + "MASDepartment AS D ON S.DeptId = D.DeptId where S.DelInd=0");
    }

    public IEnumerable<StudentRegistrationViewModel> GetStudentById(int id)
    {
      List<StudentRegistrationViewModel> registrationViewModelList = new List<StudentRegistrationViewModel>();
      foreach (MASStudentRegistration studentRegistration in this._uow.Repository<MASStudentRegistration>().GetAll(string.Format("StudentCourseId= {0}", (object) id)))
        registrationViewModelList.Add(new StudentRegistrationViewModel()
        {
          StudentId = studentRegistration.StudentId,
          StudentName = studentRegistration.StudentName
        });
      return (IEnumerable<StudentRegistrationViewModel>) registrationViewModelList;
    }

    public IEnumerable<StudentRegistrationViewModel> GetStudentByDeptId(int id)
    {
      List<StudentRegistrationViewModel> registrationViewModelList = new List<StudentRegistrationViewModel>();
      foreach (MASStudentRegistration studentRegistration in this._uow.Repository<MASStudentRegistration>().GetAll(string.Format("DeptId= {0}", (object) id)))
        registrationViewModelList.Add(new StudentRegistrationViewModel()
        {
          StudentId = studentRegistration.StudentId,
          StudentName = studentRegistration.StudentName
        });
      return (IEnumerable<StudentRegistrationViewModel>) registrationViewModelList;
    }

    public IEnumerable<StudentRegistrationViewModel> GetDeptById(int id)
    {
      List<StudentRegistrationViewModel> registrationViewModelList = new List<StudentRegistrationViewModel>();
      foreach (MASDepartment masDepartment in this._uow.Repository<MASDepartment>().GetAll(string.Format("DeptId= {0}", (object) id)))
        registrationViewModelList.Add(new StudentRegistrationViewModel()
        {
          DeptId = masDepartment.DeptId,
          DeptName = masDepartment.DeptName
        });
      return (IEnumerable<StudentRegistrationViewModel>) registrationViewModelList;
    }

    public IEnumerable<ReportSearchViewModel> CourseList()
    {
      List<ReportSearchViewModel> reportSearchViewModelList = new List<ReportSearchViewModel>();
      object[] objArray = new object[5]
      {
        (object) "CourseId  in ( '",
        (object) 1,
        (object) "' ,'",
        (object) 2,
        (object) "' )"
      };
      foreach (MASCourse masCourse in this._uow.Repository<MASCourse>().GetAll(string.Concat(objArray)).ToList<MASCourse>())
        reportSearchViewModelList.Add(new ReportSearchViewModel()
        {
          CourseId = masCourse.CourseId,
          CourseName = masCourse.CourseName
        });
      return (IEnumerable<ReportSearchViewModel>) reportSearchViewModelList;
    }
  }
}
