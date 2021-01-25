// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IPROSCasesheetService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IPROSCasesheetService : IService<TreatmentTypes>
  {
    IEnumerable<PROSTreatmentViewModel> PROSTreatmentList(long allotId);

    PROSCDViewModel BindPROSCDPatientModel(int treatId, long allotId);

    PROSRPDViewModel BindPROSRPDPatientModel(int treatId, long allotId);

    PROSFPDViewModel BindPROSFPDPatientModel(int treatId, long allotId);

    PROSMFPViewModel BindPROSMFPPatientModel(int treatId, long allotId);

    PROSDIMViewModel BindPROSDIMPatientModel(int treatId, long allotId);

    PROSCDViewModel PROSCDBindEditPatientModel(int treatId, long allotId, int PROSCDId);

    PROSRPDViewModel PROSRPDBindEditPatientModel(int treatId, long allotId, int PROSRPDId);

    PROSFPDViewModel PROSFPDBindEditPatientModel(int treatId, long allotId, int PROSFPDId);

    PROSMFPViewModel PROSMFPBindEditPatientModel(int treatId, long allotId, int PROSMFPId);

    PROSDIMViewModel PROSDIMBindEditPatientModel(int treatId, long allotId, int PROSDIMId);

    PROSTreatmentViewModel Treatment(long allotId, int patientId);

    int PROSCDSavePatient(PROSCDViewModel model);

    void PROSCDSavefollowUp(PROSCDViewModel model);

    int PROSCDUpdatePatient(PROSCDViewModel modal);

    void PROSCDUpdateAllotment(PROSCDViewModel model);

    void PROSCDUpdateReferralStatus(PROSCDViewModel model);

    PROSCDViewModel BindPROSCDPatientReport(int PROSCDId);

    int PROSRPDSavePatient(PROSRPDViewModel model);

    void PROSRPDSavefollowUp(PROSRPDViewModel model);

    PROSRPDViewModel BindPROSRPDPatientReport(int PROSRPDId);

    int PROSRPDUpdatePatient(PROSRPDViewModel modal);

    void PROSRPDUpdateAllotment(PROSRPDViewModel model);

    void PROSRPDUpdateReferralStatus(PROSRPDViewModel model);

    int PROSFPDSavePatient(PROSFPDViewModel model);

    void PROSFPDSavefollowUp(PROSFPDViewModel model);

    PROSFPDViewModel BindPROSFPDPatientReport(int PROSFPDId);

    int PROSFPDUpdatePatient(PROSFPDViewModel modal);

    void PROSFPDUpdateAllotment(PROSFPDViewModel model);

    void PROSFPDUpdateReferralStatus(PROSFPDViewModel model);

    int PROSMFPSavePatient(PROSMFPViewModel model);

    void PROSMFPSavefollowUp(PROSMFPViewModel model);

    PROSMFPViewModel BindPROSMFPPatientReport(int PROSMFPId);

    int PROSMFPUpdatePatient(PROSMFPViewModel modal);

    void PROSMFPUpdateAllotment(PROSMFPViewModel model);

    void PROSMFPUpdateReferralStatus(PROSMFPViewModel model);

    IEnumerable<PROSMFPViewModel> MfpTreatList(int PROSMFPId);

    int PROSDIMSavePatient(PROSDIMViewModel model);

    void PROSDIMSavefollowUp(PROSDIMViewModel model);

    PROSDIMViewModel BindPROSDIMPatientReport(int PROSDIMId);

    int PROSDIMUpdatePatient(PROSDIMViewModel modal);

    void PROSDIMUpdateAllotment(PROSDIMViewModel model);

    void PROSDIMUpdateReferralStatus(PROSDIMViewModel model);

    List<StudentAllotmentViewModel> ProsCDCasesheetApprovalList(string From_Date, string To_Date, string url);

    List<StudentAllotmentViewModel> ProsRPDCasesheetApprovalList(string From_Date, string To_Date, string url);

    List<StudentAllotmentViewModel> ProsFPDCasesheetApprovalList(string From_Date, string To_Date, string url);

    List<StudentAllotmentViewModel> ProsMFPCasesheetApprovalList(string From_Date, string To_Date, string url);

    List<StudentAllotmentViewModel> ProsDIMCasesheetApprovalList(string From_Date, string To_Date, string url);

    void CDProcedureApproval(PROSCDViewModel model);

    void RPDProcedureApproval(PROSRPDViewModel model);

    void FPDProcedureApproval(PROSFPDViewModel model);

    void MFPProcedureApproval(PROSMFPViewModel model);

    void DIMProcedureApproval(PROSDIMViewModel model);

    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType);

    List<PROSSearchDetails> prosthoSearchDetails(string From_Date, string To_Date, int TreatmentId, string url);

    void SendApproval(long AllotId);
  }
}
