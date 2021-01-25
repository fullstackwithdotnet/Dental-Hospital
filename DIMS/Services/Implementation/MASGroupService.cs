// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MASGroupService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class MASGroupService : ServiceBase<MASGroup>, IMASGroupService, IService<MASGroup>
  {
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;

    public MASGroupService(IUnitOfWork uow)
      : base(uow)
    {
      _uow = uow;
      _Dropdownservice = new MASCodeService(_uow);
    }

    public IEnumerable<GroupViewModel> ServicesList()
    {
      List<GroupViewModel> groupViewModelList = new List<GroupViewModel>();
      foreach (MASGroup masGroup in _uow.Repository<MASGroup>().GetAll())
        groupViewModelList.Add(new GroupViewModel()
        {
          GroupId = masGroup.GroupId,
          GroupName = masGroup.GroupName,
          DeptId = masGroup.DeptId,
          DeptName = _uow.Repository<MASDepartment>().Get(masGroup.DeptId).DeptName
        });
      return groupViewModelList;
    }

    public GroupViewModel BindGroup()
    {
      GroupViewModel groupViewModel = new GroupViewModel();
            // string whereClause = "Deptid in ( " + 16 + " ," + 20 + " )";
        string whereClause = "IsService = 'Y'";
            groupViewModel.DepartmentList = _uow.Repository<MASDepartment>().GetAll(whereClause).ToList().OrderBy(A => A.DeptName);
      return groupViewModel;
    }

    public GroupViewModel BindEditGroup(int id)
    {
      GroupViewModel groupViewModel = new GroupViewModel();
      string whereClause = "Deptid in ( '" + 16 + "' ,'" + 20 + "' )";
      groupViewModel.DepartmentList = _uow.Repository<MASDepartment>().GetAll(whereClause).ToList().OrderBy(A => A.DeptName);
      return groupViewModel;
    }

    public bool CheckGroupName(string GroupName, int DeptId)
    {
      bool flag = false;
      MASGroup masGroup = _uow.Repository<MASGroup>().GetEntitiesBySql(string.Format("select GroupName from [MASGroup] where GroupName = '{0}' and DeptId={1}", GroupName, DeptId)).FirstOrDefault();
      if (masGroup != null && masGroup.GroupName != null)
        flag = true;
      return flag;
    }
  }
}
