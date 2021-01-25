// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MASCityService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class MASCityService : ServiceBase<MASCity>, IMASCityService, IService<MASCity>
  {
    private IUnitOfWork _uow;

    public MASCityService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<CityViewModal> CityList()
    {
      List<CityViewModal> cityViewModalList = new List<CityViewModal>();
      foreach (MASCity masCity in this._uow.Repository<MASCity>().GetAll())
        cityViewModalList.Add(new CityViewModal()
        {
          CityId = masCity.CityId,
          StateId = masCity.StateId,
          Name = masCity.Name
        });
      return (IEnumerable<CityViewModal>) cityViewModalList;
    }

    public IEnumerable<CityViewModal> GetCitiesById(int id)
    {
      List<CityViewModal> cityViewModalList = new List<CityViewModal>();
      foreach (MASCity masCity in this._uow.Repository<MASCity>().GetAll(string.Format("StateId= {0}", (object) id)))
        cityViewModalList.Add(new CityViewModal()
        {
          CityId = masCity.CityId,
          StateId = masCity.StateId,
          Name = masCity.Name
        });
      return (IEnumerable<CityViewModal>) cityViewModalList;
    }
  }
}
