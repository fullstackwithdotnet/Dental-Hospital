// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.ORPATHRequisitionService
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
using System.Linq;
using System.Reflection;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class ORPATHRequisitionService : ServiceBase<BillQueueDetails>, IORPATHRequisitionService, IService<BillQueueDetails>
  {
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;
    private IBillQueueService _Requistion;
    private IBillQueueService _BillQueueservice;

    public ORPATHRequisitionService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._Requistion = (IBillQueueService) new BillQueueService(this._uow);
      this._BillQueueservice = (IBillQueueService) new BillQueueService(this._uow);
    }

    public IEnumerable<ORPATHCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<ORPATHCasesheetProperties>().GetAll();
    }

    public ORPATHRequisitionViewModel GetRequisitionDetails(int Id)
    {
      return this._uow.Repository<ORPATHRequisitionViewModel>().GetEntitiesBySql(string.Format("exec GetORPATHCasesheet {0}", (object) Id)).FirstOrDefault<ORPATHRequisitionViewModel>();
    }

    public ORPATHRequisitionViewModel BindIndex(long allotId)
    {
      ORPATHRequisitionViewModel requisitionViewModel = new ORPATHRequisitionViewModel();
      PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
      requisitionViewModel.PatientId = informationViewModel.PatientId;
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      allotmentViewModel.AllotId = allotId;
      requisitionViewModel.patientInformationViewModel = informationViewModel;
      requisitionViewModel.studentAllotmentViewModel = allotmentViewModel;
      requisitionViewModel.ORPATHDetails = this._uow.Repository<ORPATHRequisitionViewModel>().GetEntitiesBySql(string.Format(Queries.ORALPATHRequisitionIndex, (object) informationViewModel.PatientId));
      return requisitionViewModel;
    }

    public ORPATHRequisitionViewModel BindRequisitionPatientModel(long allotId)
    {
      ORPATHRequisitionViewModel requisitionViewModel = new ORPATHRequisitionViewModel();
      PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      allotmentViewModel.AllotId = allotId;
      requisitionViewModel.patientInformationViewModel = informationViewModel;
      requisitionViewModel.PatientId = informationViewModel.PatientId;
      requisitionViewModel.DeptId = informationViewModel.DeptId;
      requisitionViewModel.studentAllotmentViewModel = allotmentViewModel;
      requisitionViewModel.BillingQueueDetails = this._Requistion.RequisitionServicesList(informationViewModel.PatientId, 9, 0);
      string whereClause = string.Format("DeptId =" + (object) 9);
      this._uow.Repository<MASBillingServices>().GetAll(whereClause).OrderBy<MASBillingServices, string>((Func<MASBillingServices, string>) (A => A.ServiceName));
      requisitionViewModel.BillingQueueViewModel = new BillingQueueServiceViewModel()
      {
        BillDropServicesList = (IEnumerable<MASBillingServices>) this._uow.Repository<MASBillingServices>().GetAll(whereClause).OrderBy<MASBillingServices, string>((Func<MASBillingServices, string>) (A => A.ServiceName))
      };
      requisitionViewModel.Lesionlist = this._Dropdownservice.GetCodesById(44);
      requisitionViewModel.Tendernesslist = this._Dropdownservice.GetCodesById(45);
      requisitionViewModel.Consistencylist = this._Dropdownservice.GetCodesById(9);
      requisitionViewModel.TypeOfSamplelist = this._Dropdownservice.GetCodesById(46);
      requisitionViewModel.HardTissuelist = this._Dropdownservice.GetCodesById(47);
      requisitionViewModel.TypeOfBiopsylist = this._Dropdownservice.GetCodesById(48);
      requisitionViewModel.LymphNodeStatuslist = this._Dropdownservice.GetCodesById(57);
      requisitionViewModel.SampleCollectionDate = DateTime.Now;
      requisitionViewModel.SampleCollectionTime = DateTime.Now;
      requisitionViewModel.BiopsyCollectionDate = DateTime.Now;
      requisitionViewModel.BiopsyCollectionTime = DateTime.Now;
      return requisitionViewModel;
    }

    public int SaveRequisition(ORPATHRequisitionViewModel model)
    {
      model.MandatoryDummy = "Y";
      ORPATHCasesheet orpathCasesheet = new ORPATHCasesheet();
      ORPATHCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<ORPATHRequisitionViewModel, ORPATHCasesheet>())).CreateMapper().Map<ORPATHRequisitionViewModel, ORPATHCasesheet>(model);
      entity1.DeptId = model.DeptId;
      entity1.RequisitionDate = DateTime.Now;
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      int num = this._uow.Repository<ORPATHCasesheet>().Add(entity1, false);
      IEnumerable<ORPATHCasesheetProperties> all = this._uow.Repository<ORPATHCasesheetProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<ORPATHCasesheetProperties>((Func<ORPATHCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            ORPATHCasesheetPropertyValues entity2 = new ORPATHCasesheetPropertyValues();
            entity2.RequisitionId = num;
            ORPATHCasesheetProperties casesheetProperties = all.FirstOrDefault<ORPATHCasesheetProperties>((Func<ORPATHCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
            {
              entity2.PropId = casesheetProperties.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<ORPATHCasesheetPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      BillQueueDetails entity3 = new BillQueueDetails();
      foreach (BillingQueueServiceViewModel billingQueueDetail in model.BillingQueueDetails)
      {
        if (billingQueueDetail != null && billingQueueDetail.ServiceId != 0 && billingQueueDetail.BillQueueId == 0)
        {
          entity3.DeptId = 9;
          entity3.ServiceId = billingQueueDetail.ServiceId;
          entity3.Qty = billingQueueDetail.Qty;
          entity3.Rate = billingQueueDetail.Rate;
          entity3.Amount = billingQueueDetail.Amount;
          entity3.DiscountPer = billingQueueDetail.DiscountPer;
          entity3.NetAmount = billingQueueDetail.NetAmount;
          entity3.TeethNo = billingQueueDetail.TeethNo;
          entity3.DiscountGivenBy = billingQueueDetail.DiscountGivenBy;
          entity3.DiscountPurpose = billingQueueDetail.DiscountPurpose;
          entity3.PatientId = model.PatientId;
          entity3.RequisitionId = num;
          entity3.BillQueueDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
          entity3.CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
          entity3.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          this._BillQueueservice.Add(entity3);
        }
      }
      return num;
    }

    public ORPATHRequisitionViewModel BindEditRequisitionModel(long allotId, int Id)
    {
      ORPATHRequisitionViewModel requisitionViewModel = new ORPATHRequisitionViewModel();
      ORPATHRequisitionViewModel requisitionDetails = this.GetRequisitionDetails(Id);
      PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      allotmentViewModel.AllotId = allotId;
      requisitionDetails.patientInformationViewModel = informationViewModel;
      requisitionDetails.PatientId = informationViewModel.PatientId;
      requisitionDetails.DeptId = informationViewModel.DeptId;
      requisitionDetails.studentAllotmentViewModel = allotmentViewModel;
      requisitionDetails.BillingQueueDetails = this._Requistion.RequisitionServicesList(informationViewModel.PatientId, 9, requisitionDetails.RequisitionId);
      string whereClause = string.Format("DeptId =" + (object) 9);
      this._uow.Repository<MASBillingServices>().GetAll(whereClause).OrderBy<MASBillingServices, string>((Func<MASBillingServices, string>) (A => A.ServiceName));
      requisitionDetails.BillingQueueViewModel = new BillingQueueServiceViewModel()
      {
        BillDropServicesList = (IEnumerable<MASBillingServices>) this._uow.Repository<MASBillingServices>().GetAll(whereClause).OrderBy<MASBillingServices, string>((Func<MASBillingServices, string>) (A => A.ServiceName))
      };
      requisitionDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 9);
      requisitionDetails.Lesionlist = this._Dropdownservice.GetCodesById(44);
      requisitionDetails.Tendernesslist = this._Dropdownservice.GetCodesById(45);
      requisitionDetails.Consistencylist = this._Dropdownservice.GetCodesById(9);
      requisitionDetails.TypeOfSamplelist = this._Dropdownservice.GetCodesById(46);
      requisitionDetails.HardTissuelist = this._Dropdownservice.GetCodesById(47);
      requisitionDetails.TypeOfBiopsylist = this._Dropdownservice.GetCodesById(48);
      requisitionDetails.LymphNodeStatuslist = this._Dropdownservice.GetCodesById(57);
      requisitionDetails.MLymphNodeStatusId = requisitionDetails.MLymphNodeStatusId;
      requisitionDetails.BLymphNodeStatusId = requisitionDetails.BLymphNodeStatusId;
      IEnumerable<ORPATHCasesheetProperties> properties = this.GetProperties();
      requisitionDetails.Proplist = properties;
      return requisitionDetails;
    }

    public int UpdateRequisition(ORPATHRequisitionViewModel ViewModel)
    {
      ViewModel.RequisitionDate = Convert.ToDateTime(ViewModel.RequisitionDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      ORPATHCasesheet orpathCasesheet = new ORPATHCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<ORPATHRequisitionViewModel, ORPATHCasesheet>()));
      ORPATHCasesheet entity1 = Mapper.Map<ORPATHRequisitionViewModel, ORPATHCasesheet>(ViewModel);
      entity1.RequisitionId = ViewModel.RequisitionId;
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      this._uow.Repository<ORPATHCasesheet>().Update(entity1, false);
      IEnumerable<ORPATHCasesheetProperties> all1 = this._uow.Repository<ORPATHCasesheetProperties>().GetAll();
      IEnumerable<ORPATHCasesheetPropertyValues> all2 = this._uow.Repository<ORPATHCasesheetPropertyValues>().GetAll("RequisitionId=" + (object) ViewModel.RequisitionId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<ORPATHCasesheetProperties>((Func<ORPATHCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            ORPATHCasesheetPropertyValues entity2 = new ORPATHCasesheetPropertyValues();
            entity2.RequisitionId = ViewModel.RequisitionId;
            ORPATHCasesheetProperties property = all1.FirstOrDefault<ORPATHCasesheetProperties>((Func<ORPATHCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            ORPATHCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<ORPATHCasesheetPropertyValues>((Func<ORPATHCasesheetPropertyValues, bool>) (a =>
            {
              if (a.PropId == property.PropertyId)
                return a.RequisitionId == ViewModel.RequisitionId;
              return false;
            }));
            if (casesheetPropertyValues != null)
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              entity2.ValueId = casesheetPropertyValues.ValueId;
              this._uow.Repository<ORPATHCasesheetPropertyValues>().Update(entity2, false);
            }
            else
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<ORPATHCasesheetPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      BillQueueDetails entity3 = new BillQueueDetails();
      foreach (BillingQueueServiceViewModel billingQueueDetail in ViewModel.BillingQueueDetails)
      {
        if (billingQueueDetail != null && billingQueueDetail.ServiceId != 0 && billingQueueDetail.BillQueueId == 0)
        {
          entity3.ServiceId = billingQueueDetail.ServiceId;
          entity3.Qty = billingQueueDetail.Qty;
          entity3.Rate = billingQueueDetail.Rate;
          entity3.Amount = billingQueueDetail.Amount;
          entity3.DiscountPer = billingQueueDetail.DiscountPer;
          entity3.NetAmount = billingQueueDetail.NetAmount;
          entity3.TeethNo = billingQueueDetail.TeethNo;
          entity3.RequisitionId = entity1.RequisitionId;
          entity3.DiscountGivenBy = billingQueueDetail.DiscountGivenBy;
          entity3.DiscountPurpose = billingQueueDetail.DiscountPurpose;
          entity3.BillQueueDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
          entity3.PatientId = ViewModel.PatientId;
          entity3.DeptId = 9;
          entity3.CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
          entity3.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          this._BillQueueservice.Add(entity3);
        }
      }
      return entity1.RequisitionId;
    }

    public ORPATHRequisitionViewModel DisplayRequisitionDetails(int Id)
    {
      return new ORPATHRequisitionViewModel();
    }

    public ORPATHRequisitionViewModel BindORPATHPatientReport(int RequisitionId)
    {
      try
      {
        ORPATHRequisitionViewModel requisitionViewModel1 = new ORPATHRequisitionViewModel();
        ORPATHRequisitionViewModel requisitionViewModel2 = this._uow.Repository<ORPATHRequisitionViewModel>().GetEntitiesBySql(string.Format("exec GetORPATHCasesheetReport {0}", (object) RequisitionId)).FirstOrDefault<ORPATHRequisitionViewModel>();
        requisitionViewModel2.patientInformationViewModel = new PatientInformationViewModel()
        {
          PatientName = requisitionViewModel2.PatientName,
          AgeGender = requisitionViewModel2.Age.ToString() + "/" + (object) (Gender) requisitionViewModel2.GenderId,
          Phone = requisitionViewModel2.Phone,
          OpNo = requisitionViewModel2.OpNo,
          Area = requisitionViewModel2.Area
        };
        return requisitionViewModel2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
