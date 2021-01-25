// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MasDesignationServices
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using Repository.Base;
using Repository.Core;
using System.Linq;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class MasDesignationServices : ServiceBase<MASDesignation>, IMASDesignationService, IService<MASDesignation>
  {
    private IUnitOfWork _uow;

    public MasDesignationServices(IUnitOfWork uom)
      : base(uom)
    {
      this._uow = uom;
    }

    public bool CheckDesigName(string DesigName)
    {
      bool flag = false;
      MASDesignation masDesignation = this._uow.Repository<MASDesignation>().GetEntitiesBySql(string.Format("select DesigName from [MASDesignation] where DesigName = '{0}'", (object) DesigName)).FirstOrDefault<MASDesignation>();
      if (masDesignation != null && masDesignation.DesigName != null)
        flag = true;
      return flag;
    }
  }
}
