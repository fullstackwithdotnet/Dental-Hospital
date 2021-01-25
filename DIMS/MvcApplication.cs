// Decompiled with JetBrains decompiler
// Type: DIMS.MvcApplication
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Unity;

namespace DIMS
{
  public class MvcApplication : HttpApplication
  {
    public static UnityContainer Container;

    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      MvcApplication.Container = new UnityContainer();
      UnityConfig.RegisterComponents(MvcApplication.Container);
    }

    protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
    {
      HttpCookie cookie = this.Request.Cookies[FormsAuthentication.FormsCookieName];
      if (cookie == null)
        return;
      FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(cookie.Value);
      CustomPrincipalSerializedModel principalSerializedModel = JsonConvert.DeserializeObject<CustomPrincipalSerializedModel>(authenticationTicket.UserData);
      HttpContext.Current.User = (IPrincipal) new CustomPrincipal(authenticationTicket.Name)
      {
        UserId = principalSerializedModel.UserId,
        FirstName = principalSerializedModel.FirstName,
        LastName = principalSerializedModel.LastName,
        Roles = principalSerializedModel.Roles.ToList<string>(),
        Departments = principalSerializedModel.Departments.ToList<int>()
      };
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
      CultureInfo cultureInfo;
      try
      {
        cultureInfo = CultureInfo.CreateSpecificCulture(this.Request.UserLanguages[0]);
      }
      catch
      {
        cultureInfo = CultureInfo.CurrentCulture;
      }
      Thread.CurrentThread.CurrentCulture = cultureInfo;
    }

    protected void Application_Error()
    {
      this.Server.GetLastError();
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileTypeAttribute : ValidationAttribute, IClientValidatable
    {
      private const string _DefaultErrorMessage = "Only the following file types are allowed: {0}";

      private IEnumerable<string> _ValidTypes { get; set; }

      public FileTypeAttribute(string validTypes)
      {
        this._ValidTypes = ((IEnumerable<string>) validTypes.Split(',')).Select<string, string>((Func<string, string>) (s => s.Trim().ToLower()));
        this.ErrorMessage = string.Format("Only the following file types are allowed: {0}", (object) string.Join(" or ", this._ValidTypes));
      }

      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
      {
        IEnumerable<HttpPostedFileBase> httpPostedFileBases = value as IEnumerable<HttpPostedFileBase>;
        if (httpPostedFileBases != null)
        {
          foreach (HttpPostedFileBase httpPostedFileBase in httpPostedFileBases)
          {
            HttpPostedFileBase file = httpPostedFileBase;
            if (file != null && !this._ValidTypes.Any<string>((Func<string, bool>) (e => file.FileName.EndsWith(e))))
              return new ValidationResult(this.ErrorMessageString);
          }
        }
        return ValidationResult.Success;
      }

      public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
      {
        ModelClientValidationRule clientValidationRule = new ModelClientValidationRule()
        {
          ValidationType = "filetype",
          ErrorMessage = this.ErrorMessageString
        };
        clientValidationRule.ValidationParameters.Add("validtypes", (object) string.Join(",", this._ValidTypes));
        yield return clientValidationRule;
      }
    }
  }
}
