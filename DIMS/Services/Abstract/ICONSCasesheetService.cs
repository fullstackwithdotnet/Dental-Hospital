// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.ICONSCasesheetService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface ICONSCasesheetService : IService<CONSCasesheet>
  {
    CONSViewModel BindCONSPatientModel(long allotId);

    CONSViewModel BindTreatmentList(long allotId);

    int SaveCONSPatient(CONSViewModel model);

    CONSViewModel BindEditCONSPatientModel(long allotId, int CONSId);

    int UpdateCONSPatient(CONSViewModel ViewModel);

    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType);

    void SavefollowUp(CONSViewModel model);

    void UpdateAllotment(CONSViewModel model);

    void SaveAllotment(CONSViewModel model);

    void UpdateReferralStatus(CONSViewModel model);

    List<CONSSearchDetails> consSearchDetails(string From_Date, string To_Date, string url);

    CONSViewModel BindCONSPatientReport(int CONSId);

    List<StudentAllotmentViewModel> ConsCasesheetApprovalList(string From_Date, string To_Date, string url);

    void ProcedureApproval(CONSViewModel model);

    IEnumerable<CONSViewModel> RpList(int CONSId);

    IEnumerable<CONSViewModel> PcList(int CONSId);

    IEnumerable<CONSViewModel> SpList(int CONSId);

    IEnumerable<CONSViewModel> EcList(int CONSId);

    IEnumerable<CONSViewModel> BlList(int CONSId);

    IEnumerable<CONSViewModel> TtList(int CONSId);

    IEnumerable<CONSViewModel> RcList(int CONSId);

    IEnumerable<CONSViewModel> ReList(int CONSId);

    IEnumerable<CONSViewModel> RfList(int CONSId);

    void SendApproval(long AllotId);
  }
}
