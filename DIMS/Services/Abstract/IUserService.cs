// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IUserService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IUserService : IService<User>
  {
    User GetUserByUserNameAndPassword(string userName, string password);

    User GetUserByUserName(string userName);

    IEnumerable<Role> GetRolesForUser(int userId);

    IEnumerable<UserDepartments> GetDepartmentsForUser(int userId);

    IEnumerable<Permission> GetPermissionsForDepartment(int roledeptId);

    IEnumerable<DeptPermissions> GetDepartmentsPermissionForUser(int userId);

    UserViewModel BindSaveUserModel();

    UserViewModel BindEditUserModel(int id);

    int SaveUser(UserViewModel model);

    bool CheckUserName(string userName);

    int UpdateUser(UserViewModel model);
  }
}
