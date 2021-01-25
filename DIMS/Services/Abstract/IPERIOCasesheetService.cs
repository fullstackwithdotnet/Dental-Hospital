// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IPERIOCasesheetService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IPERIOCasesheetService : IService<PERIOCasesheet>
  {
    PERIOViewModel BindPerioPatientModel(long allotId);

    PERIOViewModel BindTreatmentList(long allotId);

    int SavePERIOPatient(PERIOViewModel model);

    PERIOViewModel BindEditPERIOPatientModel(long allotId, int PerioId);

    int UpdatePERIOPatient(PERIOViewModel ViewModel);

    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType);

    void SavefollowUp(PERIOViewModel model);

    void UpdateAllotment(PERIOViewModel model);

    void UpdateReferralStatus(PERIOViewModel model);

    List<PERIOSearchDetails> perioSearchDetails(string From_Date, string To_Date, string url);

    PERIOViewModel BindPERIOPatientReport(int PerioId);

    void ProcedureApproval(PERIOViewModel model);

    List<StudentAllotmentViewModel> perioCasesheetApprovalList(string From_Date, string To_Date, string url);

    void SendApproval(long AllotId);
  }
}
