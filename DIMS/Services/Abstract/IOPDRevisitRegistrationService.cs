// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IOPDRevisitRegistrationService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IOPDRevisitRegistrationService : IService<OPDRevisitRegistration>
  {
    OPDRevisitRegistrationViewModel BindPatientModel(int PatientId);

    int RevisitSaveFromFollowup(OPDRevisitRegistrationViewModel model);

    int RevisitSave(OPDRevisitRegistrationViewModel model);

    IEnumerable<FollowupViewModal> RevisitFollowUpList(int PatientId);

    List<OPDFollowupSearchDetails> opdFollowupSearchDetails(string From_Date, string To_Date, string url);

    List<OPDFollowupSearchDetails> opdFollowupSearchDetailsbyDeptId(string From_Date, string To_Date, int DeptId, string url);

    List<OPDRevisitSearchDetails> opdRevisitSearchDetails(string From_Date, string To_Date, string url);

    OPDRevisitRegistrationViewModel EditBindOPDRViewmodel(int PatientId, int Revisitid);

    int UpdateRevisit(OPDRevisitRegistrationViewModel ViewModel);

    FollowupViewModal DisplayRescheduleDetails(int FollowupId, int DeptId);
  }
}
