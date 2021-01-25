// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IOPDPatientRegistrationService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IOPDPatientRegistrationService : IService<OPDPatientRegistration>
  {
    MaxOpNo GetMaxOpNo();

    OPDPatientRegistrationViewModel GetPatientDetails(int pid);

    IEnumerable<OPDPatientRegistrationProperties> GetProperties();

    OPDPatientRegistrationViewModel BindPatientModel(OPDPatientRegistrationViewModel model);

    int SavePatient(OPDPatientRegistrationViewModel model);

    OPDPatientRegistrationViewModel BindEditPatientModel(int Id);

    int UpdatePatient(OPDPatientRegistrationViewModel Regmodal);

    OPDPatientRegistrationViewModel DisplayPatient(int Id);

    List<SelectListItem> GetCitiesById(int id);

    List<SelectListItem> GetStatesById(int id);

    List<OPDSearchDetails> opdSearchDetails(string From_Date, string To_Date, string url);

    List<OPDSearchDetails> opdSearchDetailsList(string From_Date, string To_Date, string url, string OPNo, string PatientName);

    BillingViewModal BindReport(int BillId, int DeptId);
  }
}
