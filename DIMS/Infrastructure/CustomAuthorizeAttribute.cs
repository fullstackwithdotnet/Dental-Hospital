// Decompiled with JetBrains decompiler
// Type: DIMS.Infrastructure.CustomAuthorizeAttribute
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DIMS.Infrastructure
{
  public class CustomAuthorizeAttribute : AuthorizeAttribute
  {
    protected virtual CustomPrincipal CurrentUser
    {
      get
      {
        return HttpContext.Current.User as CustomPrincipal;
      }
    }

    public override void OnAuthorization(AuthorizationContext filterContext)
    {
      if (filterContext.HttpContext.Request.IsAuthenticated)
      {
        if (!string.IsNullOrEmpty(this.Roles) && !this.CurrentUser.IsInRole(this.Roles))
          filterContext.Result = (ActionResult) new RedirectToRouteResult(new RouteValueDictionary((object) new
          {
            controller = "Error",
            action = "AccessDenied"
          }));
        if (string.IsNullOrEmpty(this.Users) || this.Users.Contains(this.CurrentUser.UserId.ToString()))
          return;
        filterContext.Result = (ActionResult) new RedirectToRouteResult(new RouteValueDictionary((object) new
        {
          controller = "Error",
          action = "AccessDenied"
        }));
      }
      else
        filterContext.Result = (ActionResult) new RedirectToRouteResult(new RouteValueDictionary((object) new
        {
          controller = "Account",
          action = "Index"
        }));
    }
  }
}
