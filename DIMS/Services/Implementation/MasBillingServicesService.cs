// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MasBillingServicesService
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
  public class MasBillingServicesService : ServiceBase<MASBillingServices>, IMASBillingServicesService, IService<MASBillingServices>
  {
    private IUnitOfWork _uow;

    public MasBillingServicesService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<BillingServicesViewModal> ServicesList()
    {
      var servicesViewModalList = new List<BillingServicesViewModal>();
      foreach (var masBillingServices in this._uow.Repository<MASBillingServices>().GetAll())
        servicesViewModalList.Add(new BillingServicesViewModal()
        {
          ServiceId = masBillingServices.ServiceId,
          ServiceCode = masBillingServices.ServiceCode,
          ServiceName = masBillingServices.ServiceName,
          DeptId = masBillingServices.DeptId,
          DeptName = this._uow.Repository<MASDepartment>().Get(masBillingServices.DeptId).DeptCode,
          ServiceAmount = masBillingServices.ServiceAmount.ToString()
        });
      return servicesViewModalList;
    }

    public bool Checkservicename(string ServiceName, int DeptId)
    {
      var flag = false;
      var masBillingServices = this._uow.Repository<MASBillingServices>().GetEntitiesBySql(
          $"select ServiceName from [MASBillingServices] where ServiceName = '{ServiceName}' and DeptId={DeptId}").FirstOrDefault();
      if (masBillingServices != null && masBillingServices.ServiceName != null)
        flag = true;
      return flag;
    }

      public bool CheckChildServiceName(string serviceName, int deptId, int parentId)
      {
          var flag = false;
          var masBillingServices = this._uow.Repository<MASBillingSubServices>().GetEntitiesBySql(
              $"select ServiceName from [MASBillingSubServices] where ServiceName = '{serviceName}' and DeptId={deptId} and ParentId={parentId}").FirstOrDefault();
          if (masBillingServices != null && masBillingServices.ServiceName != null)
              flag = true;
          return flag;
      }

      public MASBillingSubServices getChildService(int serviceId)
      {

          var masBillingServices = this._uow.Repository<MASBillingSubServices>().GetEntitiesBySql(
              $"select * from [MASBillingSubServices] where ServiceID={serviceId}").FirstOrDefault();
          return masBillingServices;
      }
  }
}
