// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IORPATHRequisitionService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IORPATHRequisitionService : IService<BillQueueDetails>
  {
    ORPATHRequisitionViewModel BindIndex(long allotId);

    ORPATHRequisitionViewModel BindRequisitionPatientModel(long id);

    IEnumerable<ORPATHCasesheetProperties> GetProperties();

    int SaveRequisition(ORPATHRequisitionViewModel model);

    ORPATHRequisitionViewModel BindEditRequisitionModel(long allotId, int Id);

    int UpdateRequisition(ORPATHRequisitionViewModel ViewModel);

    ORPATHRequisitionViewModel DisplayRequisitionDetails(int Id);

    ORPATHRequisitionViewModel BindORPATHPatientReport(int RequisitionId);
  }
}
