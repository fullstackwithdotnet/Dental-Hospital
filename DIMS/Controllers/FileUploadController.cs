// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.FileUploadController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DIMS.Controllers
{
  public class FileUploadController : Controller
  {
    private IFileUploadService _Service;

    public FileUploadController(IFileUploadService Service)
    {
      this._Service = Service;
    }

    public ActionResult Index()
    {
      return (ActionResult) this.View();
    }

    public ActionResult Create(long Id)
    {
      FileUploadViewModel fileUploadViewModel = new FileUploadViewModel();
      return (ActionResult) this.View((object) this._Service.BindFileUploadModel(Id));
    }

      [HttpPost]
      public ActionResult Create(FileUploadViewModel model)
      {
          if (!base.ModelState.IsValid)
          {
              ActionResult action;
              using (IEnumerator<ModelState> enumerator = base.ViewData.ModelState.Values.GetEnumerator())
              {
                  while (enumerator.MoveNext())
                  {
                      using (IEnumerator<ModelError> enumerator1 = enumerator.Current.Errors.GetEnumerator())
                      {
                          if (enumerator1.MoveNext())
                          {
                              ModelError current = enumerator1.Current;
                              string str = string.Concat(current.ErrorMessage, "-", current.Exception);
                              action = base.RedirectToAction("ErrorWrite", new RouteValueDictionary(new { controller = "Error", action = "ErrorWrite", message = str }));
                              return action;
                          }
                      }
                  }
                  model = this._Service.BindFileUploadModel(model.AllotId);
                  return base.View(model);
              }
              return action;
          }
          else
          {
              int num = this._Service.SaveFileUpload(model);
              ((dynamic)base.ViewBag).UploadStatus = string.Concat(num.ToString(), " files uploaded successfully.");
          }
          model = this._Service.BindFileUploadModel(model.AllotId);
          return base.View(model);
      }

        public FileResult DownloadFile(string fileName)
    {
      string fileName1 = Path.Combine(this.Server.MapPath("~/Content/Upload/"), fileName);
      return (FileResult) this.File(fileName1, MimeMapping.GetMimeMapping(fileName1), fileName);
    }

    [HttpPost]
    public JsonResult DeleteFileUploadDetails(int FileId)
    {
      this._Service.Delete(FileId);
      return this.Json((object) true);
    }
  }
}
