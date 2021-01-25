// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IPHDCasesheetService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IPHDCasesheetService : IService<PHDCasesheet>
  {
    IEnumerable<PHDCasesheetProperties> GetProperties();

    PHDViewModel BindPHDPatientModel(long allotId);

    PHDViewModel BindTreatmentList(long allotId);

    int SavePHDPatient(PHDViewModel model);

    int SaveOMRPHDDetails(PHDViewModel model);

    int UpdatePHDPatient(PHDViewModel model);

    PHDViewModel GetPHDPatientDetails(int Id);

    OMRPHDCasesheetViewModel GetPHDOMRPatientDetails(int Id);

    PHDViewModel BindEditPHDPatientModel(long allotId, int Id);

    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType);

    void SavefollowUp(PHDViewModel model);

    void UpdateAllotment(PHDViewModel model);

    void UpdateReferralStatus(PHDViewModel model);

    void ProcedureApproval(PHDViewModel model);

    List<PHDSearchDetails> phdSearchDetails(string From_Date, string To_Date, string url);

    List<StudentAllotmentViewModel> phdCasesheetApprovalList(string From_Date, string To_Date, string url);

    PHDViewModel BindPHDPatientReport(int PHDId);

    void SendApproval(long AllotId);
  }
}
