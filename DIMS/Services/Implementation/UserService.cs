// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.UserService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
using DIMS.Infrastructure;
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
  public class UserService : ServiceBase<User>, IUserService, IService<User>
  {
    private IUnitOfWork _uow;

    public UserService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<Permission> GetPermissionsForDepartment(int userdeptId)
    {
      return this._uow.Repository<Permission>().GetEntitiesBySql(string.Format("select permission.* from permission inner join UserDeptPermissions on userDeptPermissions.permissionid = [permission].permissionid where UserDeptPermissions.UserDeptId={0}", (object) userdeptId));
    }

    public IEnumerable<UserDepartments> GetDepartmentsForUser(int userId)
    {
      return this._uow.Repository<UserDepartments>().GetEntitiesBySql(string.Format("select u.deptid,m.deptname,m.deptcode,u.UserId, u.UserDeptId from UserDepartments u inner join masdepartment m on  u.deptid=m.deptid where userid={0}", (object) userId));
    }

    public IEnumerable<DeptPermissions> GetDepartmentsPermissionForUser(int userId)
    {
      return this._uow.Repository<DeptPermissions>().GetEntitiesBySql(string.Format("select u.DeptId, p.PermissionId, PE.PermissionName from UserDepartments AS u inner join UserDeptPermissions AS p ON p.UserDeptId = u.UserDeptId inner join Permission AS PE ON p.PermissionId = PE.PermissionId where u.UserId={0} ", (object) userId));
    }

    public User GetUserByUserName(string userName)
    {
      return this._uow.Repository<User>().GetEntitiesBySql(string.Format("select * from [user] where username = '{0}'", (object) userName)).FirstOrDefault<User>();
    }

    public User GetUserByUserNameAndPassword(string userName, string password)
    {
      return this._uow.Repository<User>().GetEntitiesBySql(string.Format("select * from [user] where username = '{0}' and password = '{1}'", (object) userName, (object) password)).FirstOrDefault<User>();
    }

    public IEnumerable<Role> GetRolesForUser(int userId)
    {
      return this._uow.Repository<Role>().GetEntitiesBySql(string.Format("select [role].* from userroles, [role] where userroles.userid={0} and userroles.roleid = [role].roleid", (object) userId));
    }

    public bool CheckUserName(string username)
    {
      bool flag = false;
      User user = this._uow.Repository<User>().GetEntitiesBySql(string.Format("select username from [user] where username = '{0}'", (object) username)).FirstOrDefault<User>();
      if (user != null && user.UserName != null)
        flag = true;
      return flag;
    }

    public UserViewModel BindSaveUserModel()
    {
      return new UserViewModel()
      {
        Roles = this._uow.Repository<Role>().GetAll()
      };
    }

    public int SaveUser(UserViewModel model)
    {
      try
      {
        User user = new User();
        User entity = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<UserViewModel, User>())).CreateMapper().Map<UserViewModel, User>(model);
        entity.CreatedDate = new DateTime?(DateTime.Now);
        entity.UserName = model.UserName.ToLower();
        int num = this._uow.Repository<User>().Add(entity, false);
        if (num > 0)
          this._uow.Repository<UserRoles>().Add(new UserRoles()
          {
            RoleId = model.RoleId,
            UserId = num,
            CreatedDate = new DateTime?(DateTime.Now)
          }, false);
        return num;
      }
      catch
      {
        throw;
      }
    }

    public UserViewModel BindEditUserModel(int id)
    {
      UserViewModel userViewModel1 = new UserViewModel();
      UserViewModel userViewModel2 = this._uow.Repository<UserViewModel>().GetEntitiesBySql(string.Format(Queries.GetUserbyId, (object) id)).SingleOrDefault<UserViewModel>();
      userViewModel1.UserId = userViewModel2.UserId;
      userViewModel1.FirstName = userViewModel2.FirstName;
      userViewModel1.LastName = userViewModel2.LastName;
      userViewModel1.UserName = userViewModel2.UserName;
      userViewModel1.Password = userViewModel2.Password;
      userViewModel1.ConfirmPassword = userViewModel2.Password;
      userViewModel1.Roles = this._uow.Repository<Role>().GetAll();
      userViewModel1.RoleId = userViewModel2.RoleId;
      userViewModel1.RoleName = userViewModel2.RoleName;
      UserRoles userRoles = this._uow.Repository<UserRoles>().GetEntitiesBySql(string.Format(Queries.GetIdbyUseRoleId, (object) userViewModel1.UserId, (object) userViewModel1.RoleId)).SingleOrDefault<UserRoles>();
      userViewModel1.UserRoleIdPK = (int) Convert.ToInt16(userRoles.Id);
      string whereClause = "DelInd=0";
      userViewModel1.Permissions = (IEnumerable<Permission>) this._uow.Repository<Permission>().GetAll(whereClause).ToList<Permission>().OrderBy<Permission, int>((Func<Permission, int>) (A => A.PermissionId));
      userViewModel1.AssignedPermissions = (IEnumerable<UserViewModel>) this._uow.Repository<UserViewModel>().GetEntitiesBySql(string.Format("exec GetUserPermissionData {0}", (object) id)).ToList<UserViewModel>();
      return userViewModel1;
    }

    public int UpdateUser(UserViewModel model)
    {
      try
      {
        User user = new User();
        User entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<UserViewModel, User>())).CreateMapper().Map<UserViewModel, User>(model);
        entity1.ModifiedDate = new DateTime?(DateTime.Now);
        entity1.UserId = model.UserId;
        if (model.UserId > 0)
          this._uow.Repository<User>().Update(entity1, false);
        int userId = model.UserId;
        if (userId > 0)
          this._uow.Repository<UserRoles>().Update(new UserRoles()
          {
            RoleId = model.RoleId,
            UserId = userId,
            Id = model.UserRoleIdPK,
            ModifiedDate = new DateTime?(DateTime.Now)
          }, false);
        if (userId > 0 && model.AssignedPermissions != null)
        {
          if (this._uow.Repository<UserViewModel>().GetEntitiesBySql(string.Format(Queries.DeleteUserPermissions, (object) userId)) != null)
            this._uow.Repository<UserViewModel>().GetEntitiesBySql(string.Format(Queries.DeleteUserDepartmentsById, (object) userId));
          model.Permissions = (IEnumerable<Permission>) this._uow.Repository<Permission>().GetAll().ToList<Permission>().OrderBy<Permission, int>((Func<Permission, int>) (A => A.PermissionId));
          UserDepartments entity2 = new UserDepartments();
          foreach (UserViewModel assignedPermission in model.AssignedPermissions)
          {
            if (assignedPermission.Add || assignedPermission.Edit || (assignedPermission.View || assignedPermission.Discount) || (assignedPermission.Referral || assignedPermission.Allotment || (assignedPermission.CancelAllotment || assignedPermission.Appointment)) || (assignedPermission.AppReschedule || assignedPermission.Investigation || (assignedPermission.TreatmentServices || assignedPermission.ProcedureApproval) || (assignedPermission.CaserecordApproval || assignedPermission.EditFreezeCase)))
            {
              entity2.DeptId = assignedPermission.DeptId;
              entity2.UserId = userId;
              int num = this._uow.Repository<UserDepartments>().Add(entity2, false);
              if (num > 0)
              {
                UserDeptPermissions entity3 = new UserDeptPermissions();
                foreach (Permission permission in model.Permissions)
                {
                  entity3.UserDeptId = num;
                  entity3.PermissionValue = true;
                  entity3.CreatedDate = new DateTime?(DateTime.Now);
                  if (permission.PermissionName == PermissionsEnum.Add.ToString() && assignedPermission.Add)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.Edit.ToString() && assignedPermission.Edit)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.View.ToString() && assignedPermission.View)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.Discount.ToString() && assignedPermission.Discount)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.Referral.ToString() && assignedPermission.Referral)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.Allotment.ToString() && assignedPermission.Allotment)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.CancelAllotment.ToString() && assignedPermission.CancelAllotment)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.Appointment.ToString() && assignedPermission.Appointment)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.AppReschedule.ToString() && assignedPermission.AppReschedule)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.Investigation.ToString() && assignedPermission.Investigation)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.TreatmentServices.ToString() && assignedPermission.TreatmentServices)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.ProcedureApproval.ToString() && assignedPermission.ProcedureApproval)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.CaserecordApproval.ToString() && assignedPermission.CaserecordApproval)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                  if (permission.PermissionName == PermissionsEnum.EditFreezeCase.ToString() && assignedPermission.EditFreezeCase)
                  {
                    entity3.PermissionId = permission.PermissionId;
                    this._uow.Repository<UserDeptPermissions>().Add(entity3, false);
                  }
                }
              }
            }
          }
        }
        return userId;
      }
      catch
      {
        throw;
      }
    }
  }
}
