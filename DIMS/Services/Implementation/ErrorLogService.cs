// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.ErrorLogService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using Repository.Base;
using Repository.Core;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class ErrorLogService : ServiceBase<GetCasesheetNo>, IErrorLogService
  {
    private IUnitOfWork _uow;

    public ErrorLogService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public bool WriteErrorLog(string LogMessage)
    {
      bool flag = false;
      string LogPath = HttpContext.Current.Server.MapPath("~/Content/ErrorLog/");
      DateTime now = DateTime.Now;
      now.ToString();
      this.CheckCreateLogDirectory(LogPath);
      string str = this.BuildLogLine(now, LogMessage);
      string path = LogPath + "Log_" + this.LogFileName(DateTime.Now) + ".txt";
      lock (typeof (ErrorLogService))
      {
        StreamWriter streamWriter = (StreamWriter) null;
        try
        {
          streamWriter = new StreamWriter(path, true);
          streamWriter.WriteLine(str);
          flag = true;
        }
        catch
        {
        }
        finally
        {
          streamWriter?.Close();
        }
      }
      return flag;
    }

    public void LogException(string message)
    {
      string LogPath = HttpContext.Current.Server.MapPath("~/Content/ErrorLog/");
      DateTime now = DateTime.Now;
      now.ToString();
      this.CheckCreateLogDirectory(LogPath);
      string str = this.BuildLogLine(now, message);
      string path = LogPath + "ModelLog_" + this.LogFileName(DateTime.Now) + ".txt";
      lock (typeof (ErrorLogService))
      {
        StreamWriter streamWriter = (StreamWriter) null;
        try
        {
          streamWriter = new StreamWriter(path, true);
          streamWriter.WriteLine(str);
          streamWriter.WriteLine("__________________________");
        }
        catch
        {
        }
        finally
        {
          streamWriter?.Close();
        }
      }
      HttpContext current = HttpContext.Current;
      current.Response.Clear();
      RequestContext requestContext = ((MvcHandler) current.CurrentHandler).RequestContext;
      requestContext.RouteData.Values["action"] = (object) "Error";
      requestContext.RouteData.Values["controller"] = (object) "Error";
      ControllerBuilder.Current.GetControllerFactory().CreateController(requestContext, "Error").Execute(requestContext);
      current.Server.ClearError();
    }

    private string BuildLogLine(DateTime CurrentDateTime, string LogMessage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.LogFileEntryDateTime(CurrentDateTime));
      stringBuilder.Append(" \t");
      stringBuilder.Append(LogMessage);
      return stringBuilder.ToString();
    }

    public string LogFileEntryDateTime(DateTime CurrentDateTime)
    {
      return CurrentDateTime.ToString("dd-MM-yyyy HH:mm:ss");
    }

    private string LogFileName(DateTime CurrentDateTime)
    {
      return CurrentDateTime.ToString("dd_MM_yyyy");
    }

    private bool CheckCreateLogDirectory(string LogPath)
    {
      bool flag = false;
      if (new DirectoryInfo(LogPath).Exists)
      {
        flag = true;
      }
      else
      {
        try
        {
          Directory.CreateDirectory(LogPath);
          flag = true;
        }
        catch
        {
        }
      }
      return flag;
    }
  }
}
