// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IOrpathCasesheetService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IOrpathCasesheetService : IService<ORPATHCasesheet>
  {
    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType);

    List<OrpathRequisitionSearchDetails> OrpathSearchDetails(string From_Date, string To_Date, string url);

    List<StudentAllotmentViewModel> orpathCasesheetApprovalList(string From_Date, string To_Date, string url);

    ORPATHRequisitionViewModel BindTreatmentList(long allotId);

    ORPATHRequisitionViewModel BindRequisitionPatientModel(long allotId);

    int SaveOrpathPatient(ORPATHRequisitionViewModel model);

    ORPATHRequisitionViewModel BindEditOrpathPatientModel(long allotId, int Id);

    int UpdateOrpathPatient(ORPATHRequisitionViewModel ViewModel);

    void UpdateAllotment(ORPATHRequisitionViewModel model);

    void UpdateReferralStatus(ORPATHRequisitionViewModel model);

    void SavefollowUp(ORPATHRequisitionViewModel model);

    void ProcedureApproval(ORPATHRequisitionViewModel model);

    ORPATHRequisitionViewModel BindORPATHPatientReport(int RequisitionId);

    void SendApproval(long AllotId);
  }
}
