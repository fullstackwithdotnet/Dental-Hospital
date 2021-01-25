// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.FileUploadService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
using DIMS.Enums;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class FileUploadService : ServiceBase<FileUpload>, IFileUploadService, IService<FileUpload>
  {
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;

    public FileUploadService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
    }

    public FileUploadViewModel BindFileUploadModel(long allotId)
    {
      FileUploadViewModel fileUploadViewModel = new FileUploadViewModel();
      PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
      string whereClause = " DeptId=" + (object) informationViewModel.DeptId;
      informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
      fileUploadViewModel.patientInformationViewModel = informationViewModel;
      fileUploadViewModel.PatientId = informationViewModel.PatientId;
      fileUploadViewModel.DeptId = informationViewModel.DeptId;
      fileUploadViewModel.AllotId = informationViewModel.AllotId;
      fileUploadViewModel.FileTypelist = this._Dropdownservice.GetCodesById(184);
      fileUploadViewModel.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) fileUploadViewModel.PatientId, (object) fileUploadViewModel.DeptId));
      return fileUploadViewModel;
    }

    public int SaveFileUpload(FileUploadViewModel model)
    {
      try
      {
        this.CheckCreateUploadDirectory(HttpContext.Current.Server.MapPath("~/Content/Upload/"));
        FileUpload fileUpload = new FileUpload();
        FileUpload entity = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<FileUploadViewModel, FileUpload>())).CreateMapper().Map<FileUploadViewModel, FileUpload>(model);
        int num = 0;
        foreach (HttpPostedFileBase postedFile in model.postedFiles)
        {
          if (postedFile != null && postedFile.ContentLength > 0)
          {
            string[] strArray = new string[10]
            {
              ".png",
              ".jpg",
              "jpeg",
              ".xls",
              ".xlsx",
              ".doc",
              ".docx",
              ".pdf",
              ".tiff",
              ".tif"
            };
            string lower = Path.GetExtension(postedFile.FileName).ToLower();
            if (((IEnumerable<string>) strArray).Contains<string>(lower))
            {
              string fileName = Path.GetFileName(postedFile.FileName);
              ++num;
              string withoutExtension = Path.GetFileNameWithoutExtension(fileName);
              string str1 = Guid.NewGuid().ToString();
              string filename = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Upload/") + withoutExtension + str1 + lower);
              string str2 = Path.Combine(withoutExtension + str1 + lower);
              postedFile.SaveAs(filename);
              entity.FileName = withoutExtension;
              entity.FilePath = filename;
              entity.FileDisplayPath = str2;
              entity.CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
              entity.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
              this._uow.Repository<FileUpload>().Add(entity, false);
            }
          }
        }
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool CheckCreateUploadDirectory(string LogPath)
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
