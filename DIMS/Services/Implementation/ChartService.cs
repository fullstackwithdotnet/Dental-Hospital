// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.ChartService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using Metron.Entities;
using Repository.Base;
using Repository.Core;

namespace DIMS.Services.Implementation
{
  public class ChartService : ServiceBase<OPDPatientRegistration>, IChartService, IService<OPDPatientRegistration>
  {
    private IUnitOfWork _uow;

    public ChartService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }
  }
}
