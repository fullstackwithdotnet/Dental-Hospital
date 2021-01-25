// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MasDepartmentService
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
  public class MasDepartmentService : ServiceBase<MASDepartment>, IMASDepartmentService, IService<MASDepartment>
  {
    private IUnitOfWork _uow;

    public MasDepartmentService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<DepartmentViewModal> DepartmentList()
    {
      List<DepartmentViewModal> departmentViewModalList = new List<DepartmentViewModal>();
      foreach (MASDepartment masDepartment in this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadServiceDept, (object) 0)))
        departmentViewModalList.Add(new DepartmentViewModal()
        {
          DeptId = masDepartment.DeptId,
          DeptName = masDepartment.DeptName
        });
      return (IEnumerable<DepartmentViewModal>) departmentViewModalList;
    }
  }
}
