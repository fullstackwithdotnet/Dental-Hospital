// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IORTHOCasesheetService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IORTHOCasesheetService : IService<ORTHOCasesheet>
  {
    OrthoViewModal BindOrthoPatientModel(long allotId);

    OrthoViewModal BindTreatmentList(long allotId);

    int SaveORTHOPatient(OrthoViewModal model);

    OrthoViewModal BindEditORTHOPatientModel(long allotId, int OrthoId);

    int UpdateORTHOPatient(OrthoViewModal ViewModel);

    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType);

    void SavefollowUp(OrthoViewModal model);

    void UpdateAllotment(OrthoViewModal model);

    void UpdateReferralStatus(OrthoViewModal model);

    List<OrthoSearchDetails> orthoSearchDetails(string From_Date, string To_Date, string url);

    OrthoViewModal BindORTHOPatientReport(int OrthoId);

    List<StudentAllotmentViewModel> orthoCasesheetApprovalList(string From_Date, string To_Date, string url);

    void ProcedureApproval(OrthoViewModal model);

    void SendApproval(long AllotId);
  }
}
