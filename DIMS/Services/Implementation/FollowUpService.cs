// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.FollowUpService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System.Collections.Generic;
using System.Linq;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class FollowUpService : ServiceBase<FollowUp>, IFollowUpService, IService<FollowUp>
  {
    private IUnitOfWork _uow;

    public FollowUpService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<FollowupViewModal> LoadFollowupList(int patientId, int DeptId, int TreatmentId)
    {
      return (IEnumerable<FollowupViewModal>) this._uow.Repository<FollowupViewModal>().GetEntitiesBySql(string.Format(Queries.FollowUpDetailsList, (object) patientId, (object) DeptId, (object) TreatmentId)).ToList<FollowupViewModal>();
    }
  }
}
