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
  public interface IPERIODiagnosisCasesheetService : IService<PERIODiagnosisCasesheet>
  {
    PERIODiagnosisViewModel BindPerioPatientModel(long allotId);

    PERIODiagnosisViewModel BindTreatmentList(long allotId);

    int SavePERIOPatient(PERIODiagnosisViewModel model);

    PERIODiagnosisViewModel BindEditPERIOPatientModel(long allotId, int PerioId);

    int UpdatePERIOPatient(PERIODiagnosisViewModel ViewModel);

    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType);

    void SavefollowUp(PERIODiagnosisViewModel model);

    void UpdateAllotment(PERIODiagnosisViewModel model);

    void UpdateReferralStatus(PERIODiagnosisViewModel model);

    List<PERIOSearchDetails> perioSearchDetails(string From_Date, string To_Date, string url);

    PERIODiagnosisViewModel BindPERIOPatientReport(int PerioId);

    void ProcedureApproval(PERIODiagnosisViewModel model);

    List<StudentAllotmentViewModel> perioDiagnosisCasesheetApprovalList(string From_Date, string To_Date, string url);

    void SendApproval(long AllotId);
  }
}
