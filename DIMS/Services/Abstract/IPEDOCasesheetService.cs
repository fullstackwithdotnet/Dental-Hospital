// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IPEDOCasesheetService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IPEDOCasesheetService : IService<PEDOCasesheet>
  {
    IEnumerable<PEDOCasesheetProperties> GetProperties();

    PEDOViewModel BindPEDOPatientModel(long allotId);

    PEDOViewModel BindTreatmentList(long allotId);

    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType);

    int SavePEDOPatient(PEDOViewModel model);

    void UpdateAllotment(PEDOViewModel model);

    void UpdateReferralStatus(PEDOViewModel model);

    int UpdatePEDOPatient(PEDOViewModel Regmodal);

    void SavefollowUp(PEDOViewModel model);

    void ProcedureApproval(PEDOViewModel model);

    PEDOViewModel BindEditPEDOPatientModel(long allotId, int PEDOId);

    List<PEDOSearchDetails> pedoSearchDetails(string From_Date, string To_Date, string url);

    List<StudentAllotmentViewModel> pedoCasesheetApprovalList(string From_Date, string To_Date, string url);

    PEDOViewModel BindPEDOPatientReport(int PEDOId);

    void SendApproval(long AllotId);
  }
}
