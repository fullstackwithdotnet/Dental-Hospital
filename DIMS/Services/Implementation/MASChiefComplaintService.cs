// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MASChiefComplaintService
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
  public class MASChiefComplaintService : ServiceBase<MASChiefComplaint>, IMASChiefComplaintService, IService<MASChiefComplaint>
  {
    private IUnitOfWork _uow;

    public MASChiefComplaintService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<MASChiefComplaint> GetChiefComplaintById(int DeptId)
    {
      return this._uow.Repository<MASChiefComplaint>().GetEntitiesBySql("SELECT  C.ChiefComplaintId, (CT.ChiefComplaintType +' : '+ C.ChiefComplaint) as ChiefComplaint " + "FROM MASChiefComplaint AS C " + "INNER JOIN MASChiefComplaintType AS CT ON C.ChiefComplaintTypeId = CT.ChiefComplaintTypeId " + "where C.DelInd=0 and C.DeptId =" + (object) DeptId);
    }
  }
}
