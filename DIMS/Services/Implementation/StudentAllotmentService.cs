// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.StudentAllotmentService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Enums;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class StudentAllotmentService : ServiceBase<StudentAllotment>, IStudentAllotmentService, IService<StudentAllotment>
  {
    private IUnitOfWork _uow;

    public StudentAllotmentService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType, int DeptId, string DeptCode)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      if (!string.IsNullOrEmpty(PatientId.ToString()))
      {
        ReferralStatusViewModel referralStatusViewModel1 = new ReferralStatusViewModel();
        ReferralStatusViewModel referralStatusViewModel2 = this._uow.Repository<ReferralStatusViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayPatientAllotment, (object) ReferredId)).SingleOrDefault<ReferralStatusViewModel>();
        allotmentViewModel.patientInformationViewModel = new PatientInformationViewModel()
        {
          PatientId = referralStatusViewModel2.PatientId,
          OpNo = referralStatusViewModel2.OpNo,
          PatientName = referralStatusViewModel2.PatientName,
          Phone = referralStatusViewModel2.Phone,
          AgeGender = referralStatusViewModel2.Age.ToString() + "/" + (object) (Gender) referralStatusViewModel2.GenderId,
          Address = referralStatusViewModel2.Address
        };
        allotmentViewModel.ReferredId = ReferredId;
        allotmentViewModel.DeptId = DeptId;
        allotmentViewModel.AllotDate = new DateTime?(DateTime.Now);
        allotmentViewModel.DeptCode = DeptCode;
        allotmentViewModel.ReferredReason = referralStatusViewModel2.ReferredReason;
        allotmentViewModel.AllotTypelist = this._uow.Repository<MASCourse>().GetAll("DelInd =0");
        switch (CourseType)
        {
          case 2:
            allotmentViewModel.StudentAllotmentlist = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.AllotmentStudentPGDropdownlist, (object) DeptId));
            break;
          case 4:
            allotmentViewModel.StudentAllotmentlist = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.AllotmentDoctorDropdownlist, (object) DeptId));
            break;
          default:
            allotmentViewModel.StudentAllotmentlist = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.AllotmentStudentUGDropdownlist, (object) 1));
            break;
        }
        allotmentViewModel.PreviousAllotmentlist = this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPreviousDetails, (object) referralStatusViewModel2.PatientId, (object) DeptId));
      }
      return allotmentViewModel;
    }

    public IEnumerable<StudentAllotmentViewModel> LoadProcedureNotesList(int patientId, int DeptId, int TreatmentId)
    {
      return (IEnumerable<StudentAllotmentViewModel>) this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedProcedureNotesDetails, (object) patientId, (object) DeptId, (object) TreatmentId)).ToList<StudentAllotmentViewModel>();
    }

    public Decimal ShowPatientBalance(int patientId, int DeptId)
    {
      PatientInformationViewModel informationViewModel = new PatientInformationViewModel();
      return this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayPatientBalanceDueAmount, (object) patientId, (object) DeptId)).FirstOrDefault<PatientInformationViewModel>().DueAmount;
    }
  }
}
