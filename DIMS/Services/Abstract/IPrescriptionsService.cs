﻿// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IPrescriptionsService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IPrescriptionsService : IService<Prescriptions>
  {
    PrescriptionsViewModel LoadPrescriptionsList();

    IEnumerable<PrescriptionsViewModel> LoadPrescriptionsList(int patientId, int DeptId, int TreatmentId);

    int SavePrescriptions(IEnumerable<PrescriptionsViewModel> model, Prescriptions prescriptionsDetails);

    IEnumerable<PrescriptionsViewModel> PreviousPrescriptionsList(int PatientId);

    PrescriptionsViewModel PrescriptionReport(long AllotId);

    MedicalAlertViewModel BindMedicalAlert(int PatientId);
  }
}
