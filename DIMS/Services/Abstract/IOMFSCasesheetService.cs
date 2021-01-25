// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IOMFSCasesheetService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IOMFSCasesheetService : IService<OMFSOPCasesheet>
  {
    IEnumerable<OMFSOPCasesheetProperties> GetProperties();

    IEnumerable<OMFSTreatmentViewModel> TreatmentList();

    OMFSTreatmentViewModel BindTreatmentModel(long allotId, int patientId);

    IEnumerable<OMFSTreatmentViewModel> OMFSOPList(int patientId);

    IEnumerable<OMFSTreatmentViewModel> OMFSIPList(int patientId);

    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType);

    OMFSOPViewModel GetOMFSOPPatientDetails(int Id);

    OMFSOPViewModel BindOmfsOpPatientModel(long allotId, int TreatmentId);

    int SaveOMFSOPPatient(OMFSOPViewModel model);

    void OMFSOpUpdateAllotment(OMFSOPViewModel model);

    void OMFSOPSavefollowUp(OMFSOPViewModel model);

    void OMFSOpUpdateReferralStatus(OMFSOPViewModel model);

    OMFSOPViewModel BindEditOmfsOpModel(long allotId, int OMFSOpId);

    int UpdateOMFSOPPatient(OMFSOPViewModel model);

    IEnumerable<OMFSIPCasesheetProperties> GetIPProperties();

    List<OMFSSearchDetails> omfsIPSearchDetails(string From_Date, string To_Date, string url);

    List<OMFSSearchDetails> omfsOPSearchDetails(string From_Date, string To_Date, string url);

    OMFSOPViewModel BindOMFSOPPatientReport(int OMFSOpId);

    List<StudentAllotmentViewModel> omfsOpCasesheetApprovalList(string From_Date, string To_Date, string url);

    List<StudentAllotmentViewModel> omfsIpCasesheetApprovalList(string From_Date, string To_Date, string url);

    void ProcedureApprovalOp(OMFSOPViewModel model);

    void SendApproval(long AllotId);
  }
}
