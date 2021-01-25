// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.HomeController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class HomeController : Controller
  {
    private IUserService _service;

    public HomeController(IUserService service)
    {
      this._service = service;
    }

    public ActionResult Index()
    {
      return (ActionResult) this.View();
    }

    public ActionResult Login()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult Login(string userName, string password)
    {
      this.CreateAuthenticationTicket(userName, password);
      return (ActionResult) this.View();
    }

    private void CreateAuthenticationTicket(string username, string password)
    {
      User userNameAndPassword = this._service.GetUserByUserNameAndPassword(username, password);
      if (userNameAndPassword != null)
      {
        string userData = new JavaScriptSerializer().Serialize((object) new CustomPrincipalSerializedModel()
        {
          FirstName = userNameAndPassword.FirstName,
          LastName = userNameAndPassword.LastName
        });
        this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddHours(8.0), false, userData))));
      }
      else
        this.Response.RedirectToRoute("");
    }
  }
}
