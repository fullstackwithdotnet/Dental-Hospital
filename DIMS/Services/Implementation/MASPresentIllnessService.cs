// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MASPresentIllnessService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using Repository.Base;
using Repository.Core;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class MASPresentIllnessService : ServiceBase<MASPresentIllness>, IMASPresentIllnessService, IService<MASPresentIllness>
  {
    private IUnitOfWork _uow;

    public MASPresentIllnessService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<MASPresentIllness> GetPresentIllnessById()
    {
      return this._uow.Repository<MASPresentIllness>().GetEntitiesBySql("SELECT P.PresentIllnessId, (PT.PresentIllnessType + ' : ' + P.PresentIllness ) AS PresentIllness " + "FROM   MASPresentIllness AS P " + "INNER JOIN MASPresentIllnessType AS PT ON P.PresentIllnessTypeId = PT.PresentIllnessTypeId " + "where  P.DelInd=0 ");
    }
  }
}
