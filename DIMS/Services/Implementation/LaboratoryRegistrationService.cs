// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.LaboratoryRegistrationService
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
  public class LaboratoryRegistrationService : ServiceBase<LaboratoryRegistration>, ILaboratoryRegistrationService, IService<LaboratoryRegistration>
  {
    private IUnitOfWork _uow;
    private IOPDPatientRegistrationService _OPDservice;
    private IMASCodeService _Dropdownservice;
    private ICasesheetNoService _CasesheetNoService;

    public LaboratoryRegistrationService(IUnitOfWork uow)
      : base(uow)
    {
      _uow = uow;
      _OPDservice = new OPDPatientRegistrationService(_uow);
      _Dropdownservice = new MASCodeService(_uow);
      _CasesheetNoService = new CasesheetNoService(_uow);
    }

    public List<LaboratoryRegistrationSearchDetails> BillingList(int DeptId, string From_Date, string To_Date, string url)
    {
      return _uow.Repository<LaboratoryRegistrationSearchDetails>().GetEntitiesBySql(string.Format(Queries.LaboratoryIndex, (object) DeptId, (object) From_Date, (object) To_Date, (object) url)).ToList();
    }

    public List<LaboratoryRegistrationSearchDetails> SearchList(string From_Date, string To_Date, string url)
    {
      return _uow.Repository<LaboratoryRegistrationSearchDetails>().GetEntitiesBySql(string.Format(Queries.LaboratorySearch, From_Date, To_Date, url)).ToList();
    }

    public LaboratoryRegistrationViewModel BindLaboratoryModel(int id, int patientid)
    {
      var registrationViewModel1 = new LaboratoryRegistrationViewModel();
      var registrationViewModel2 = _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.RadioforCreate, id, patientid)).SingleOrDefault();
      if (registrationViewModel2 != null)
      {
        registrationViewModel1.PatientId = registrationViewModel2.PatientId;
        registrationViewModel1.PatientName = registrationViewModel2.PatientName;
        registrationViewModel1.OpNo = registrationViewModel2.OpNo;
        registrationViewModel1.AgeGender = registrationViewModel2.Age + "/" + (Gender) registrationViewModel2.GenderId;
        registrationViewModel1.BillId = registrationViewModel2.BillId;
        registrationViewModel1.BillNo = registrationViewModel2.BillNo;
        registrationViewModel1.BillDateTimeDisplay = registrationViewModel2.BillDateTimeDisplay;
        registrationViewModel1.DeptCode = registrationViewModel2.DeptCode;
      }
      return registrationViewModel1;
    }

    public IEnumerable<LaboratoryRegistrationViewModel> TestNamesList(int id)
    {
      var registrationViewModelList = new List<LaboratoryRegistrationViewModel>();
      return _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.TestNamesforCreate, id, 20)).ToList();
    }

    public int SaveLaboratory(LaboratoryRegistrationViewModel model)
    {
      var laboratoryRegistration = new LaboratoryRegistration();
      Mapper.Initialize(cfg => cfg.CreateMap<LaboratoryRegistrationViewModel, LaboratoryRegistration>());
      var entity1 = Mapper.Map<LaboratoryRegistrationViewModel, LaboratoryRegistration>(model);
      entity1.LaboratoryId = 0;
      entity1.CreatedDate = DateTime.Now;
      entity1.CreatedSystem = _Dropdownservice.GetIPAddress(false);
      entity1.LaboratoryNo = _CasesheetNoService.GetLabNo();
      var num = _uow.Repository<LaboratoryRegistration>().Add(entity1, false);
      var entity2 = new LaboratoryRegistrationDetails();
      foreach (var testName in model.TestNameList)
      {
        entity2.LaboratoryDetId = 0;
        entity2.LaboratoryId = num;
        entity2.ServiceId = testName.ServiceId;
        entity2.TeethNo = testName.TeethNo;
        _uow.Repository<LaboratoryRegistrationDetails>().Add(entity2, false);
      }
      return num;
    }

    public LaboratoryRegistrationViewModel BindEditLaboratoryModel(int id)
    {
      var registrationViewModel1 = new LaboratoryRegistrationViewModel();
      var registrationViewModel2 = _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryRegDetails, id)).SingleOrDefault();
      if (registrationViewModel2 != null)
      {
        registrationViewModel1.PatientId = registrationViewModel2.PatientId;
        registrationViewModel1.LaboratoryId = registrationViewModel2.LaboratoryId;
        registrationViewModel1.LaboratoryNo = registrationViewModel2.LaboratoryNo;
        registrationViewModel1.PatientName = registrationViewModel2.PatientName;
        registrationViewModel1.OpNo = registrationViewModel2.OpNo;
        registrationViewModel1.AgeGender = registrationViewModel2.Age + "/" + (Gender) registrationViewModel2.GenderId;
        registrationViewModel1.DeptCode = registrationViewModel2.DeptCode;
        registrationViewModel1.SampleCollectedDateDisplay = registrationViewModel2.SampleCollectedDateDisplay;
        registrationViewModel1.SampleCollectedTime = registrationViewModel2.SampleCollectedTime;
        registrationViewModel1.SampleCollectedBy = registrationViewModel2.SampleCollectedBy;
      }
      return registrationViewModel1;
    }

    public IEnumerable<LaboratoryRegistrationViewModel> TestNameEditList(int LaboratoryId)
    {
      var registrationViewModelList = new List<LaboratoryRegistrationViewModel>();
      return _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.TestNamesforLaboratoryEdit, LaboratoryId)).ToList();
    }

    public IEnumerable<LaboratoryRegistrationViewModel> TestItemList(int ServiceId, int LabId, int resultId)
    {
      var registrationViewModelList = new List<LaboratoryRegistrationViewModel>();
      var entitiesBySql = _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.ChkforLaboratoryResultEntry, resultId));
      if (entitiesBySql != null && entitiesBySql.Count() != 0)
      {
        foreach (var registrationViewModel in entitiesBySql)
          registrationViewModelList.Add(new LaboratoryRegistrationViewModel()
          {
            LaboratoryResultId = registrationViewModel.LaboratoryResultId,
            LaboratoryResultDetId = registrationViewModel.LaboratoryResultDetId,
            RadioTempId = registrationViewModel.RadioTempId,
            RadioTempDetId = registrationViewModel.RadioTempDetId,
            ServiceItems = registrationViewModel.ServiceItems,
            MaleNormalRange = registrationViewModel.MaleNormalRange,
            FemaleNormalRange = registrationViewModel.FemaleNormalRange,
            Unit = registrationViewModel.Unit,
            Result = registrationViewModel.Result,
            IsHeader = registrationViewModel.IsHeader.Trim()
          });
      }
      else
      {
        foreach (var registrationViewModel in _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.TestItemsforEntry, ServiceId)))
          registrationViewModelList.Add(new LaboratoryRegistrationViewModel()
          {
            RadioTempId = registrationViewModel.RadioTempId,
            RadioTempDetId = registrationViewModel.RadioTempDetId,
            ServiceItems = registrationViewModel.ServiceItems,
            MaleNormalRange = registrationViewModel.MaleNormalRange,
            FemaleNormalRange = registrationViewModel.FemaleNormalRange,
            Unit = registrationViewModel.Unit,
            Result = registrationViewModel.Result,
            IsHeader = registrationViewModel.IsHeader.Trim()
          });
      }
      return registrationViewModelList;
    }

    public LaboratoryRegistrationViewModel BindEditHeadLaboratoryModel(int ServiceId, int LabId, int resultId)
    {
      var registrationViewModel1 = new LaboratoryRegistrationViewModel();
      var registrationViewModel2 = _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.LaboratoryTestNameHeading, ServiceId, LabId)).SingleOrDefault();
      if (registrationViewModel2 != null)
      {
        registrationViewModel1.LaboratoryId = registrationViewModel2.LaboratoryId;
        registrationViewModel1.LaboratoryNo = registrationViewModel2.LaboratoryNo;
        registrationViewModel1.LaboratoryDetId = registrationViewModel2.LaboratoryDetId;
        registrationViewModel1.RadioTempId = registrationViewModel2.RadioTempId;
        registrationViewModel1.ServiceId = registrationViewModel2.ServiceId;
        registrationViewModel1.ServiceName = registrationViewModel2.ServiceName;
        registrationViewModel1.GroupName = registrationViewModel2.GroupName;
        registrationViewModel1.LabNo = registrationViewModel2.LabNo;
        registrationViewModel1.TeethNo = registrationViewModel2.TeethNo;
        registrationViewModel1.PatientId = registrationViewModel2.PatientId;
        registrationViewModel1.DeptId = registrationViewModel2.DeptId;
      }
      var laboratoryResultEntry = _uow.Repository<LaboratoryResultEntry>().GetEntitiesBySql(string.Format(Queries.ChkforLaboratoryResult, resultId)).FirstOrDefault();
      if (laboratoryResultEntry != null)
      {
        registrationViewModel1.LaboratoryResultId = laboratoryResultEntry.LaboratoryResultId;
        registrationViewModel1.Impression = laboratoryResultEntry.Impression;
        registrationViewModel1.Remarks = laboratoryResultEntry.Remarks;
      }
      registrationViewModel1.ServiceId = ServiceId;
      registrationViewModel1.fileuploadviewmodel = new FileUploadViewModel()
      {
        FileTypelist = _Dropdownservice.GetCodesById(184),
        FileUploadlist = _uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadbyTestdetId, registrationViewModel1.PatientId, registrationViewModel1.DeptId, registrationViewModel1.LaboratoryDetId))
      };
      return registrationViewModel1;
    }

    public int SaveEditLaboratory(LaboratoryRegistrationViewModel model)
    {
      var laboratoryResultEntry = new LaboratoryResultEntry();
      Mapper.Initialize(cfg => cfg.CreateMap<LaboratoryRegistrationViewModel, LaboratoryResultEntry>());
      var num1 = 0;
      if (_uow.Repository<LaboratoryResultEntry>().GetEntitiesBySql(string.Format(Queries.ChkforLaboratoryResult, model.LaboratoryResultId)).FirstOrDefault() != null)
      {
        var entity = Mapper.Map<LaboratoryRegistrationViewModel, LaboratoryResultEntry>(model);
        entity.ModifiedDate = DateTime.Now;
        entity.ModifiedSystem = _Dropdownservice.GetIPAddress(false);
        _uow.Repository<LaboratoryResultEntry>().Update(entity, false);
      }
      else
      {
        var entity = Mapper.Map<LaboratoryRegistrationViewModel, LaboratoryResultEntry>(model);
        entity.LaboratoryResultId = 0;
        entity.CreatedDate = DateTime.Now;
        entity.CreatedSystem = _Dropdownservice.GetIPAddress(false);
        num1 = _uow.Repository<LaboratoryResultEntry>().Add(entity, false);
      }
      if (model.LaboratoryDetId > 0)
        _uow.Repository<LaboratoryRegistrationDetails>().Update(new LaboratoryRegistrationDetails()
        {
          LaboratoryDetId = model.LaboratoryDetId,
          TestDone = "Y",
          TestDate = DateTime.Now
        }, false);
      var all = _uow.Repository<LaboratoryResultEntryDetails>().GetAll("LaboratoryResultId=" + model.LaboratoryResultId);
      var entity1 = new LaboratoryResultEntryDetails();
      if (all.Count() != 0)
      {
        foreach (var testItem in model.TestItemList)
        {
          entity1.LaboratoryResultDetId = testItem.LaboratoryResultDetId;
          entity1.LaboratoryResultId = model.LaboratoryResultId;
          entity1.RadioTempDetId = testItem.RadioTempDetId;
          entity1.Result = testItem.Result;
          _uow.Repository<LaboratoryResultEntryDetails>().Update(entity1, false);
        }
      }
      else
      {
        foreach (var testItem in model.TestItemList)
        {
          entity1.LaboratoryResultDetId = 0;
          entity1.LaboratoryResultId = num1;
          entity1.RadioTempDetId = testItem.RadioTempDetId;
          entity1.Result = testItem.Result;
          _uow.Repository<LaboratoryResultEntryDetails>().Add(entity1, false);
        }
      }
      var entity2 = new FileUpload();
      var num2 = 20;
      entity2.TestDetId = model.LaboratoryDetId;
      entity2.PatientId = model.PatientId;
      entity2.FileTypeId = model.fileuploadviewmodel.FileTypeId;
      entity2.Description = model.fileuploadviewmodel.Description;
      entity2.DeptId = num2;
      var num3 = 0;
      foreach (var postedFile in model.fileuploadviewmodel.postedFiles)
      {
        if (postedFile != null && postedFile.ContentLength > 0)
        {
          var strArray = new string[7]
          {
            ".png",
            ".jpg",
            "jpeg",
            ".xls",
            ".xlsx",
            ".doc",
            ".docx"
          };
          var fileName = Path.GetFileName(postedFile.FileName);
          var extension = Path.GetExtension(postedFile.FileName);
          if (strArray.Contains(extension))
          {
            ++num3;
            var withoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var str1 = Guid.NewGuid().ToString();
            var filename = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Upload/") + withoutExtension + str1 + extension);
            var str2 = Path.Combine(withoutExtension + str1 + extension);
            postedFile.SaveAs(filename);
            entity2.FileName = withoutExtension;
            entity2.FilePath = filename;
            entity2.FileDisplayPath = str2;
            entity2.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            entity2.CreatedSystem = _Dropdownservice.GetIPAddress(false);
            _uow.Repository<FileUpload>().Add(entity2, false);
          }
        }
      }
      return num1;
    }

    public LaboratoryRegistrationViewModel BindLaboratoryPatientReport(int LabId)
    {
      var registrationViewModel1 = new LaboratoryRegistrationViewModel();
      var registrationViewModel2 = _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryReportHeaderDetails, LabId)).SingleOrDefault();
      if (registrationViewModel2 != null)
      {
        registrationViewModel1.PatientId = registrationViewModel2.PatientId;
        registrationViewModel1.PatientName = registrationViewModel2.PatientName;
        registrationViewModel1.OpNo = registrationViewModel2.OpNo;
        registrationViewModel1.AgeGender = registrationViewModel2.Age + "/" + (Gender) registrationViewModel2.GenderId;
        registrationViewModel1.LaboratoryNo = registrationViewModel2.LaboratoryNo;
        registrationViewModel1.Dateofreceiving = registrationViewModel2.Dateofreceiving;
        registrationViewModel1.DateofDispatch = registrationViewModel2.DateofDispatch;
        registrationViewModel1.DeptCode = registrationViewModel2.DeptCode;
        registrationViewModel1.BillNo = registrationViewModel2.BillNo;
      }
      registrationViewModel1.DeptId = 20;
      registrationViewModel1.TestNameReportList = _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.LaboratoryTestNameReport, LabId)).ToList();
      registrationViewModel1.TestItemList = _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.LaboratoryTestItemReport, LabId)).ToList();
      registrationViewModel1.FileUploadlist = _uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, registrationViewModel1.PatientId, registrationViewModel1.DeptId));
      return registrationViewModel1;
    }
  }
}
