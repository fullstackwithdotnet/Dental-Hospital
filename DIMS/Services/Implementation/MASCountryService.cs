// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MASCountryService
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
  public class MASCountryService : ServiceBase<MASCountry>, IMASCountryService, IService<MASCountry>
  {
    private IUnitOfWork _uow;

    public MASCountryService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<CountryViewModal> CountryList()
    {
      List<CountryViewModal> countryViewModalList = new List<CountryViewModal>();
      foreach (MASCountry masCountry in this._uow.Repository<MASCountry>().GetAll())
        countryViewModalList.Add(new CountryViewModal()
        {
          CountryId = masCountry.CountryId,
          Name = masCountry.Name
        });
      return (IEnumerable<CountryViewModal>) countryViewModalList;
    }
  }
}
