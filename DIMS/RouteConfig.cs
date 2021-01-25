// Decompiled with JetBrains decompiler
// Type: DIMS.RouteConfig
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System.Web.Mvc;
using System.Web.Routing;

namespace DIMS
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.MapRoute("Default", "{controller}/{action}/{id}", (object) new
      {
        controller = "Account",
        action = "Index",
        id = UrlParameter.Optional
      });
    }
  }
}
