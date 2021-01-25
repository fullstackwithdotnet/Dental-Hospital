// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.GroupController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class GroupController : BaseController
  {
    private IUnitOfWork _uow;
    private IMASGroupService _service;
    private IMASCodeService _Dropdownservice;

    public GroupController(IUnitOfWork uow, IMASGroupService service, IMASCodeService Dropdownservice, IUserService userservice)
      : base(uow, userservice)
    {
      _uow = uow;
      _service = service;
      _Dropdownservice = Dropdownservice;
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Index()
    {
      GetPermissionforUser();
      if (User.Departments.Contains(17))
        return View(_service.ServicesList().ToList<GroupViewModel>());
      return View("../Error/AccessDenied");
    }

    [HttpGet]
    public ActionResult Create()
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(17))
        return View("../Error/AccessDenied");
      GroupViewModel groupViewModel = new GroupViewModel();
      return View("../Group/Create", _service.BindGroup());
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Create(GroupViewModel group)
    {
      try
      {
        if (!User.Departments.Contains(17))
          return View("../Error/AccessDenied");
        if (!ModelState.IsValid)
          return View();
        if (_service.CheckGroupName(group.GroupName, group.DeptId))
        {
          TempData["Message"] = "Group Name already Exist";
          group = _service.BindGroup();
          return View("../Group/Create", @group);
        }
        _service.Add(new MASGroup()
        {
          GroupName = group.GroupName,
          DeptId = group.DeptId
        });
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        return View(ex);
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Edit(int id)
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(17))
        return View("../Error/AccessDenied");
      MASGroup masGroup = _service.Get(id);
      GroupViewModel groupViewModel = new GroupViewModel();
      groupViewModel.GroupId = masGroup.GroupId;
      groupViewModel.GroupName = masGroup.GroupName;
      groupViewModel.DeptId = masGroup.DeptId;
      string whereClause = "Deptid in ( '" + 16 + "' ,'" + 20 + "' )";
      groupViewModel.DepartmentList = _uow.Repository<MASDepartment>().GetAll(whereClause).ToList<MASDepartment>().OrderBy<MASDepartment, string>(A => A.DeptName);
      return View(nameof (Edit), groupViewModel);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Edit(GroupViewModel model)
    {
      try
      {
        if (!User.Departments.Contains(17))
          return View("../Error/AccessDenied");
        if (ModelState.IsValid)
        {
          _service.Update(new MASGroup()
          {
            GroupId = model.GroupId,
            GroupName = model.GroupName,
            DeptId = model.DeptId
          });
        }
        else
        {
          foreach (ModelState modelState in ViewData.ModelState.Values)
          {
            using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
            {
              if (enumerator.MoveNext())
              {
                ModelError current = enumerator.Current;
                return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "ErrorWrite",
                    message = (current.ErrorMessage + "-" + (object) current.Exception)
                }));
              }
            }
          }
        }
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        return View(ex);
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Details(int id)
    {
      MASGroup masGroup = _service.Get(id);
      GroupViewModel groupViewModel = new GroupViewModel();
      int deptId = masGroup.DeptId;
      groupViewModel.DeptName = _uow.Repository<MASCode>().Get(deptId).CodeDescription;
      groupViewModel.GroupName = masGroup.GroupName;
      return View(nameof (Details), groupViewModel);
    }
  }
}
