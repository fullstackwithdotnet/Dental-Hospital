// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IMASBillingServicesService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
    public interface IMASBillingServicesService : IService<MASBillingServices>
    {
        IEnumerable<BillingServicesViewModal> ServicesList();

        bool Checkservicename(string ServiceName, int DeptId);
        bool CheckChildServiceName(string serviceName, int deptId, int parentId);

        MASBillingSubServices getChildService(int ServiceId);
    }
}
