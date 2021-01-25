// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IRadioRegistrationService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IRadioRegistrationService : IService<RADIORegistration>
  {
    List<RadioRegistrationSearchDetails> BillingList(int DeptId, string From_Date, string To_Date, string url);

    List<RadioRegistrationSearchDetails> SearchList(string From_Date, string To_Date, string url);

    RadioRegistrationViewModel BindRadiologyModel(int id, int patientid);

    IEnumerable<RadioRegistrationViewModel> TestNamesList(int id);

    int SaveRadiolgy(RadioRegistrationViewModel model);

    RadioRegistrationViewModel BindEditRadiologyModel(int id);

    IEnumerable<RadioRegistrationViewModel> TestNameEditList(int labId);

    IEnumerable<RadioRegistrationViewModel> TestItemList(int ServiceId, int LabId, int resultId);

    RadioRegistrationViewModel BindEditHeadRadiologyModel(int ServiceId, int LabId, int resultId);

    int SaveEditRadiolgy(RadioRegistrationViewModel model);

    RadioRegistrationViewModel ResultEntry(int ServiceId, int LabId, int resultId);

    RadioRegistrationViewModel BindReportRadiologyModel(int LabId);
  }
}
