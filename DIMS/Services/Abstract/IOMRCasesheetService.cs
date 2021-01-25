// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IOMRCasesheetService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IOMRCasesheetService : IService<OMRCasesheet>
  {
    OMRViewModel GetOMRPatientDetails(int Id);

    IEnumerable<OMRCasesheetProperties> GetProperties();

    OMRViewModel BindOMRPatientModel(long allotId);

    OMRViewModel BindTreatmentList(long allotId);

    int SaveOMRPatient(OMRViewModel model);

    OMRViewModel BindEditOMRPatientModel(long allotId, int OMRId);

    int UpdateOMRPatient(OMRViewModel Regmodal);

    OMRViewModel DisplayOMRPatient(int Id);

    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType);

    void SavefollowUp(OMRViewModel model);

    void UpdateAllotment(OMRViewModel model);

    void SaveAllotment(OMRViewModel model);

    void UpdateReferralStatus(OMRViewModel model);

    void ProcedureApproval(OMRViewModel model);

    List<OMRSearchDetails> omrSearchDetails(string From_Date, string To_Date, string url);

    List<StudentAllotmentViewModel> omrCasesheetApprovalList(string From_Date, string To_Date, string url);

    OMRViewModel BindOMRPatientReport(int OMRId);

    void SendApproval(long AllotId);
  }
}
