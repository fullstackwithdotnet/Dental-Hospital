// Decompiled with JetBrains decompiler
// Type: DIMS.BundleConfig
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System.Web.Optimization;

namespace DIMS
{
  public class BundleConfig
  {
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js", "~/Scripts/jquery-ui-{version}.js", "~/Scripts/additional-methods.min.js", "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.unobtrusive-ajax.min.js", "~/Scripts/momentdaterangepicker.js"));
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*", new IItemTransform[0]));
      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js", "~/Scripts/wickedpicker.js", "~/Scripts/Utils.js", "~/Scripts/helptext.js", "~/Scripts/dataTables.min.js", "~/Scripts/daterangepicker.js", "~/Scripts/app.js"));
      bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/_all-skins.css", "~/Content/AdminLTE.css", "~/Content/ionicons.css", "~/Content/custom.css", "~/Content/datepicker3.css", "~/Content/jquery-ui.min.css", "~/Content/wickedpicker.css", "~/Content/dataTables.min.css", "~/Content/daterangepicker.css", "~/Content/opensans.css", "~/Content/font-awesome.css"));
      bundles.Add(new StyleBundle("~/Content/themes/base/css").Include("~/Content/themes/base/core.css", "~/Content/themes/base/resizable.css", "~/Content/themes/base/selectable.css", "~/Content/themes/base/accordion.css", "~/Content/themes/base/autocomplete.css", "~/Content/themes/base/button.css", "~/Content/themes/base/dialog.css", "~/Content/themes/base/slider.css", "~/Content/themes/base/tabs.css", "~/Content/themes/base/datepicker.css", "~/Content/themes/base/progressbar.css", "~/Content/themes/base/theme.css"));
    }
  }
}
