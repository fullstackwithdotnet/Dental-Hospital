// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.RadioRegistrationService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
using DIMS.Enums;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Metron.Entities;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DIMS.Services.Implementation
{
    public class RadioRegistrationService : ServiceBase<RADIORegistration>, IRadioRegistrationService, IService<RADIORegistration>
    {
        private IUnitOfWork _uow;
        private IOPDPatientRegistrationService _OPDservice;
        private IMASCodeService _Dropdownservice;
        private ICasesheetNoService _CasesheetNoService;

        public RadioRegistrationService(IUnitOfWork uow)
          : base(uow)
        {
            this._uow = uow;
            this._OPDservice = (IOPDPatientRegistrationService)new OPDPatientRegistrationService(this._uow);
            this._Dropdownservice = (IMASCodeService)new MASCodeService(this._uow);
            this._CasesheetNoService = (ICasesheetNoService)new CasesheetNoService(this._uow);
        }

        public List<RadioRegistrationSearchDetails> BillingList(int DeptId, string From_Date, string To_Date, string url)
        {
            return this._uow.Repository<RadioRegistrationSearchDetails>().GetEntitiesBySql(string.Format(Queries.RadioIndex, (object)DeptId, (object)From_Date, (object)To_Date, (object)url)).ToList<RadioRegistrationSearchDetails>();
        }

        public List<RadioRegistrationSearchDetails> SearchList(string From_Date, string To_Date, string url)
        {
            return this._uow.Repository<RadioRegistrationSearchDetails>().GetEntitiesBySql(string.Format(Queries.RadioSearch, (object)From_Date, (object)To_Date, (object)url)).ToList<RadioRegistrationSearchDetails>();
        }

        public RadioRegistrationViewModel BindRadiologyModel(int id, int patientid)
        {
            RadioRegistrationViewModel registrationViewModel1 = new RadioRegistrationViewModel();
            RadioRegistrationViewModel registrationViewModel2 = this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.RadioforCreate, (object)id, (object)patientid)).SingleOrDefault<RadioRegistrationViewModel>();
            if (registrationViewModel2 != null)
            {
                registrationViewModel1.PatientId = registrationViewModel2.PatientId;
                registrationViewModel1.PatientName = registrationViewModel2.PatientName;
                registrationViewModel1.OpNo = registrationViewModel2.OpNo;
                registrationViewModel1.AgeGender = registrationViewModel2.Age.ToString() + "/" + (object)(Gender)registrationViewModel2.GenderId;
                registrationViewModel1.BillId = registrationViewModel2.BillId;
                registrationViewModel1.BillNo = registrationViewModel2.BillNo;
                registrationViewModel1.BillDateTimeDisplay = registrationViewModel2.BillDateTimeDisplay;
                registrationViewModel1.DeptCode = registrationViewModel2.DeptCode;
            }
            return registrationViewModel1;
        }

        public IEnumerable<RadioRegistrationViewModel> TestNamesList(int id)
        {
            List<RadioRegistrationViewModel> registrationViewModelList = new List<RadioRegistrationViewModel>();
            foreach (RadioRegistrationViewModel registrationViewModel in this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.TestNamesforCreate, (object)id, (object)16)))
                registrationViewModelList.Add(new RadioRegistrationViewModel()
                {
                    BillId = registrationViewModel.BillId,
                    ServiceId = registrationViewModel.ServiceId,
                    ServiceName = registrationViewModel.ServiceName,
                    TeethNo = registrationViewModel.TeethNo,
                    GroupName = registrationViewModel.GroupName
                });
            return (IEnumerable<RadioRegistrationViewModel>)registrationViewModelList;
        }

        public int SaveRadiolgy(RadioRegistrationViewModel model)
        {
            RADIORegistration radioRegistration = new RADIORegistration();
            Mapper.Initialize((Action<IMapperConfiguration>)(cfg => cfg.CreateMap<RadioRegistrationViewModel, RADIORegistration>()));
            RADIORegistration entity1 = Mapper.Map<RadioRegistrationViewModel, RADIORegistration>(model);
            entity1.LabId = 0;
            entity1.RadioDate = new DateTime?(DateTime.Now);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            entity1.LabNo = this._CasesheetNoService.GetRadioNo();
            int num = this._uow.Repository<RADIORegistration>().Add(entity1, false);
            RADIORegistrationDetails entity2 = new RADIORegistrationDetails();
            foreach (RadioRegistrationViewModel testName in model.TestNameList)
            {
                entity2.LabDetId = 0;
                entity2.LabId = num;
                entity2.ServiceId = testName.ServiceId;
                entity2.TeethNo = testName.TeethNo;
                this._uow.Repository<RADIORegistrationDetails>().Add(entity2, false);
            }
            return num;
        }

        public RadioRegistrationViewModel BindEditRadiologyModel(int id)
        {
            RadioRegistrationViewModel registrationViewModel1 = new RadioRegistrationViewModel();
            RadioRegistrationViewModel registrationViewModel2 = this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetRadioRegDetails, (object)id)).SingleOrDefault<RadioRegistrationViewModel>();
            if (registrationViewModel2 != null)
            {
                registrationViewModel1.PatientId = registrationViewModel2.PatientId;
                registrationViewModel1.LabId = registrationViewModel2.LabId;
                registrationViewModel1.LabNo = registrationViewModel2.LabNo;
                registrationViewModel1.PatientName = registrationViewModel2.PatientName;
                registrationViewModel1.OpNo = registrationViewModel2.OpNo;
                registrationViewModel1.AgeGender = registrationViewModel2.Age.ToString() + "/" + (object)(Gender)registrationViewModel2.GenderId;
                registrationViewModel1.CreatedDateDisplay = registrationViewModel2.CreatedDateDisplay;
                registrationViewModel1.DeptCode = registrationViewModel2.DeptCode;
            }
            return registrationViewModel1;
        }

        public IEnumerable<RadioRegistrationViewModel> TestNameEditList(int labId)
        {
            List<RadioRegistrationViewModel> registrationViewModelList = new List<RadioRegistrationViewModel>();
            foreach (RadioRegistrationViewModel registrationViewModel in this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.TestNamesforEdit, (object)labId)))
                registrationViewModelList.Add(new RadioRegistrationViewModel()
                {
                    LabId = registrationViewModel.LabId,
                    LabDetId = registrationViewModel.LabDetId,
                    ResultId = registrationViewModel.ResultId,
                    RadioTempId = registrationViewModel.RadioTempId,
                    ServiceId = registrationViewModel.ServiceId,
                    ServiceName = registrationViewModel.ServiceName,
                    TeethNo = registrationViewModel.TeethNo,
                    GroupName = registrationViewModel.GroupName,
                    TestDone = registrationViewModel.TestDone
                });
            return (IEnumerable<RadioRegistrationViewModel>)registrationViewModelList;
        }

        public RadioRegistrationViewModel ResultEntry(int ServiceId, int LabId, int resultId)
        {
            RadioRegistrationViewModel registrationViewModel = new RadioRegistrationViewModel();
            RADIOResultEntry radioResultEntry = this._uow.Repository<RADIOResultEntry>().GetEntitiesBySql(string.Format(Queries.CheckforResult, (object)resultId)).FirstOrDefault<RADIOResultEntry>();
            registrationViewModel.ResultId = radioResultEntry.ResultId;
            registrationViewModel.Impression = radioResultEntry.Impression;
            registrationViewModel.Remarks = radioResultEntry.Remarks;
            return registrationViewModel;
        }

        public IEnumerable<RadioRegistrationViewModel> TestItemList(int ServiceId, int LabId, int resultId)
        {
            List<RadioRegistrationViewModel> registrationViewModelList = new List<RadioRegistrationViewModel>();
            IEnumerable<RadioRegistrationViewModel> entitiesBySql = this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.CheckforResultEntry, (object)resultId));
            if (entitiesBySql != null && entitiesBySql.Count<RadioRegistrationViewModel>() != 0)
            {
                foreach (RadioRegistrationViewModel registrationViewModel in entitiesBySql)
                    registrationViewModelList.Add(new RadioRegistrationViewModel()
                    {
                        ResultId = registrationViewModel.ResultId,
                        ResultDetId = registrationViewModel.ResultDetId,
                        RadioTempId = registrationViewModel.RadioTempId,
                        RadioTempDetId = registrationViewModel.RadioTempDetId,
                        ServiceItems = registrationViewModel.ServiceItems,
                        Result = registrationViewModel.Result,
                        IsHeader = registrationViewModel.IsHeader
                    });
            }
            else
            {
                foreach (RadioRegistrationViewModel registrationViewModel in this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.TestItemsforEntry, (object)ServiceId)))
                    registrationViewModelList.Add(new RadioRegistrationViewModel()
                    {
                        RadioTempId = registrationViewModel.RadioTempId,
                        RadioTempDetId = registrationViewModel.RadioTempDetId,
                        ServiceItems = registrationViewModel.ServiceItems,
                        Result = registrationViewModel.Result,
                        IsHeader = registrationViewModel.IsHeader
                    });
            }
            return (IEnumerable<RadioRegistrationViewModel>)registrationViewModelList;
        }

        public RadioRegistrationViewModel BindEditHeadRadiologyModel(int ServiceId, int LabId, int ResultId)
        {
            RadioRegistrationViewModel registrationViewModel1 = new RadioRegistrationViewModel();
            if (ResultId != 0)
                registrationViewModel1 = this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.CheckforResult, (object)ResultId)).FirstOrDefault<RadioRegistrationViewModel>();
            RadioRegistrationViewModel registrationViewModel2 = this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.TestNameHeading, (object)ServiceId, (object)LabId)).SingleOrDefault<RadioRegistrationViewModel>();
            if (registrationViewModel2 != null)
            {
                registrationViewModel1.LabId = registrationViewModel2.LabId;
                registrationViewModel1.LabDetId = registrationViewModel2.LabDetId;
                registrationViewModel1.RadioTempId = registrationViewModel2.RadioTempId;
                registrationViewModel1.ServiceId = registrationViewModel2.ServiceId;
                registrationViewModel1.ServiceName = registrationViewModel2.ServiceName;
                registrationViewModel1.GroupName = registrationViewModel2.GroupName;
                registrationViewModel1.LabNo = registrationViewModel2.LabNo;
                registrationViewModel1.TeethNo = registrationViewModel2.TeethNo;
                registrationViewModel1.PatientId = registrationViewModel2.PatientId;
                registrationViewModel1.DeptId = registrationViewModel2.DeptId;
            }
            registrationViewModel1.TestItemList = this.TestItemList(ServiceId, LabId, ResultId);
            registrationViewModel1.fileuploadviewmodel = new FileUploadViewModel()
            {
                FileTypelist = this._Dropdownservice.GetCodesById(184),
                FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadbyTestdetId, (object)registrationViewModel1.PatientId, (object)registrationViewModel1.DeptId, (object)registrationViewModel2.LabDetId))
            };
            return registrationViewModel1;
        }

        public int SaveEditRadiolgy(RadioRegistrationViewModel model)
        {
            RADIOResultEntry radioResultEntry = new RADIOResultEntry();
            Mapper.Initialize((Action<IMapperConfiguration>)(cfg => cfg.CreateMap<RadioRegistrationViewModel, RADIOResultEntry>()));
            int num1 = 0;
            if (this._uow.Repository<RADIOResultEntry>().GetEntitiesBySql(string.Format(Queries.RadioResultEntryUpdate, (object)model.ResultId)).FirstOrDefault<RADIOResultEntry>() != null)
            {
                RADIOResultEntry entity = Mapper.Map<RadioRegistrationViewModel, RADIOResultEntry>(model);
                entity.ModifiedBy = model.CreatedBy;
                entity.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                entity.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
                this._uow.Repository<RADIOResultEntry>().Update(entity, false);
            }
            else
            {
                RADIOResultEntry entity = Mapper.Map<RadioRegistrationViewModel, RADIOResultEntry>(model);
                entity.ResultId = 0;
                entity.CreatedDate = new DateTime?(DateTime.Now);
                entity.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
                num1 = this._uow.Repository<RADIOResultEntry>().Add(entity, false);
            }
            if (model.LabDetId > 0)
                this._uow.Repository<RADIORegistrationDetails>().Update(new RADIORegistrationDetails()
                {
                    LabDetId = model.LabDetId,
                    TestDone = "Y",
                    TestDate = DateTime.Now
                }, false);
            IEnumerable<RADIOResultEntryDetails> all = this._uow.Repository<RADIOResultEntryDetails>().GetAll("ResultId=" + (object)model.ResultId);
            RADIOResultEntryDetails entity1 = new RADIOResultEntryDetails();
            if (all.Count<RADIOResultEntryDetails>() != 0)
            {
                foreach (RadioRegistrationViewModel testItem in model.TestItemList)
                {
                    entity1.ResultDetId = testItem.ResultDetId;
                    entity1.ResultId = model.ResultId;
                    entity1.RadioTempDetId = testItem.RadioTempDetId;
                    entity1.Result = testItem.Result;
                    this._uow.Repository<RADIOResultEntryDetails>().Update(entity1, false);
                }
            }
            else
            {
                foreach (RadioRegistrationViewModel testItem in model.TestItemList)
                {
                    entity1.ResultDetId = 0;
                    entity1.ResultId = num1;
                    entity1.RadioTempDetId = testItem.RadioTempDetId;
                    entity1.Result = testItem.Result;
                    this._uow.Repository<RADIOResultEntryDetails>().Add(entity1, false);
                }
            }
            FileUpload entity2 = new FileUpload();
            int num2 = 16;
            entity2.TestDetId = (long)model.LabDetId;
            entity2.PatientId = model.PatientId;
            entity2.FileTypeId = model.fileuploadviewmodel.FileTypeId;
            entity2.Description = model.fileuploadviewmodel.Description;
            entity2.DeptId = num2;
            int num3 = 0;
            foreach (HttpPostedFileBase postedFile in model.fileuploadviewmodel.postedFiles)
            {
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    string[] strArray = new string[8]
                    {
            ".png",
            ".jpg",
            "jpeg",
            ".xls",
            ".xlsx",
            ".doc",
            ".docx",
            ".pdf"
                    };
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    if (((IEnumerable<string>)strArray).Contains<string>(extension))
                    {
                        ++num3;
                        string withoutExtension = Path.GetFileNameWithoutExtension(fileName);
                        string str1 = Guid.NewGuid().ToString();
                        string filename = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Upload/") + withoutExtension + str1 + extension);
                        string str2 = Path.Combine(withoutExtension + str1 + extension);
                        postedFile.SaveAs(filename);
                        entity2.FileName = withoutExtension;
                        entity2.FilePath = filename;
                        entity2.FileDisplayPath = str2;
                        entity2.CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                        entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
                        this._uow.Repository<FileUpload>().Add(entity2, false);
                    }
                }
            }
            return num1;
        }

        public RadioRegistrationViewModel BindReportRadiologyModel(int LabId)
        {
            RadioRegistrationViewModel registrationViewModel1 = new RadioRegistrationViewModel();
            RadioRegistrationViewModel registrationViewModel2 = this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetPatientInfomforRadioReport, (object)LabId)).FirstOrDefault<RadioRegistrationViewModel>();
            if (registrationViewModel2 != null)
            {
                registrationViewModel1.PatientId = registrationViewModel2.PatientId;
                registrationViewModel1.PatientName = registrationViewModel2.PatientName;
                registrationViewModel1.OpNo = registrationViewModel2.OpNo;
                registrationViewModel1.AgeGender = registrationViewModel2.Age.ToString() + "/" + registrationViewModel2.Gender;
                registrationViewModel1.LabNo = registrationViewModel2.LabNo;
                registrationViewModel1.CreatedDateReceiving = registrationViewModel2.CreatedDateReceiving;
                registrationViewModel1.CreatedDateDispatch = registrationViewModel2.CreatedDateDispatch;
                registrationViewModel1.DeptCode = registrationViewModel2.DeptCode;
            }
            registrationViewModel1.DeptId = 16;
            registrationViewModel1.TestNameReportList = (IEnumerable<RadioRegistrationViewModel>)this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.TestNamesforReport, (object)LabId)).ToList<RadioRegistrationViewModel>();
            registrationViewModel1.TestItemList = (IEnumerable<RadioRegistrationViewModel>)this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetTestNamesforReport, (object)LabId)).ToList<RadioRegistrationViewModel>();
            registrationViewModel1.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object)registrationViewModel1.PatientId, (object)registrationViewModel1.DeptId));
            return registrationViewModel1;
        }
    }
}
