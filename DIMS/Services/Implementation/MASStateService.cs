// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MASStateService
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
  public class MASStateService : ServiceBase<MASState>, IMASStateService, IService<MASState>
  {
    private IUnitOfWork _uow;

    public MASStateService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<StateViewModal> StateList()
    {
      List<StateViewModal> stateViewModalList = new List<StateViewModal>();
      foreach (MASState masState in this._uow.Repository<MASState>().GetAll())
        stateViewModalList.Add(new StateViewModal()
        {
          StateId = masState.StateId,
          CountryId = masState.CountryId,
          Name = masState.Name
        });
      return (IEnumerable<StateViewModal>) stateViewModalList;
    }

    public IEnumerable<StateViewModal> GetStatesById(int id)
    {
      List<StateViewModal> stateViewModalList = new List<StateViewModal>();
      foreach (MASState masState in this._uow.Repository<MASState>().GetAll(string.Format("CountryId= {0}", (object) id)))
        stateViewModalList.Add(new StateViewModal()
        {
          StateId = masState.StateId,
          CountryId = masState.CountryId,
          Name = masState.Name
        });
      return (IEnumerable<StateViewModal>) stateViewModalList;
    }
  }
}
