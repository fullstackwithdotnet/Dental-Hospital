// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.OPDPatientRegistrationService
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
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class OPDPatientRegistrationService : ServiceBase<OPDPatientRegistration>, IOPDPatientRegistrationService, IService<OPDPatientRegistration>
  {
    private const int IndiaCountryId = 1;
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;
    private IMASCountryService _Countryservice;
    private IMASStateService _Stateservice;
    private IMASCityService _Cityservice;
    private IReferralStatusService _Referralservice;
    private IBillingService _Billing;

    public OPDPatientRegistrationService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._Countryservice = (IMASCountryService) new MASCountryService(this._uow);
      this._Stateservice = (IMASStateService) new MASStateService(this._uow);
      this._Cityservice = (IMASCityService) new MASCityService(this._uow);
      this._Referralservice = (IReferralStatusService) new ReferralStatusService(this._uow);
      this._Billing = (IBillingService) new BillingService(this._uow);
    }

    public OPDPatientRegistrationViewModel GetPatientDetails(int pid)
    {
      return this._uow.Repository<OPDPatientRegistrationViewModel>().GetEntitiesBySql(string.Format("exec GetOPDPatientRegistration {0}", (object) pid)).FirstOrDefault<OPDPatientRegistrationViewModel>();
    }

    public IEnumerable<OPDPatientRegistrationProperties> GetProperties()
    {
      return this._uow.Repository<OPDPatientRegistrationProperties>().GetAll();
    }

    public OPDPatientRegistrationViewModel BindPatientModel(OPDPatientRegistrationViewModel model)
    {
      string whereClause = "delInd=0";
      model.Paymodelist = (IEnumerable<MASPaymode>) this._uow.Repository<MASPaymode>().GetAll(whereClause).ToList<MASPaymode>();
      model.Categorylist = (IEnumerable<MASCategory>) this._uow.Repository<MASCategory>().GetAll(whereClause).ToList<MASCategory>();
      model.Paymodelist = (IEnumerable<MASPaymode>) model.Paymodelist.Where<MASPaymode>((Func<MASPaymode, bool>) (x =>
      {
        if (x.PaymodeId != 4 && x.PaymodeId != 2)
          return x.PaymodeId != 3;
        return false;
      })).ToList<MASPaymode>();
      model.Titlelist = this._Dropdownservice.GetCodesById(2);
      model.BloodGrouplist = this._Dropdownservice.GetCodesById(20);
      MASBillingServices masBillingServices = this._uow.Repository<MASBillingServices>().GetAll("DeptId=" + (object) 15 + " ").FirstOrDefault<MASBillingServices>();
      Decimal num = new Decimal();
      if (masBillingServices != null)
      {
        num = masBillingServices.ServiceAmount;
        model.ServiceId = masBillingServices.ServiceId;
      }
      model.TotalAmt = num;
      model.DiscountPer = Decimal.Zero;
      model.NetAmt = num;
      model.PaidAmt = num;
      model.Countries = (IList<SelectListItem>) this.LoadCountries();
      model.CountryCode = "+91";
      model.States = (IList<SelectListItem>) this.GetStatesById(Convert.ToInt32(model.Countries.FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected)).Value));
      model.Cities = (IList<SelectListItem>) this.GetCitiesById(Convert.ToInt32(model.States.FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected)).Value));
      IEnumerable<OPDPatientRegistrationProperties> properties = this.GetProperties();
      model.Proplist = properties;
      return model;
    }

    public int SavePatient(OPDPatientRegistrationViewModel model)
    {
      model.MandatoryDummy = "Y";
      model.GenderId = new int?((int) model.Gender);
      model.MaritalStatusId = new int?((int) model.MaritalStatus);
      model.ReligionId = new int?((int) model.Religion);
      model.RegDate = DateTime.Now;
      model.Dob = Convert.ToDateTime(model.Dob.ToString("yyyy-MM-dd"));
      OPDPatientRegistration patientRegistration = new OPDPatientRegistration();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OPDPatientRegistrationViewModel, OPDPatientRegistration>()));
      OPDPatientRegistration entity1 = Mapper.Map<OPDPatientRegistrationViewModel, OPDPatientRegistration>(model);
      entity1.PatientId = 0;
      entity1.DepartmentId = model.DepartmentId;
      entity1.AadharNo = model.AadharNo1 + model.AadharNo2 + model.AadharNo3;
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      string format = "RegDate='{0}'";
      DateTime now = DateTime.Now;
      string str = now.ToString("yyyy-MM-dd");
      string.Format(format, (object) str);
      MaxOpNo maxOpNo = this.GetMaxOpNo();
      entity1.OpNo = maxOpNo.OpNo;
      Decimal num1 = new Decimal();
      if (entity1.PaidAmt == Decimal.Zero)
        entity1.NetAmt = num1;
      int num2 = this._uow.Repository<OPDPatientRegistration>().Add(entity1, true);
      IEnumerable<OPDPatientRegistrationProperties> all = this._uow.Repository<OPDPatientRegistrationProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<OPDPatientRegistrationProperties>((Func<OPDPatientRegistrationProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            OPDPatientRegistrationPropertyValues entity2 = new OPDPatientRegistrationPropertyValues();
            entity2.PatientId = num2;
            OPDPatientRegistrationProperties registrationProperties = all.FirstOrDefault<OPDPatientRegistrationProperties>((Func<OPDPatientRegistrationProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(registrationProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(registrationProperties.PropertyDataType == "int") || (int) obj != 0))
            {
              entity2.PropId = registrationProperties.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<OPDPatientRegistrationPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      ReferralStatus entity3 = new ReferralStatus();
      entity3.PatientId = num2;
      entity3.FromdeptId = 15;
      if (model.CategoryId == 3)
      {
        entity3.ReferredReason = "To Diagnosis from Camp";
        entity3.ToDeptId = 8;
      }
      else
      {
        entity3.ReferredReason = "To Diagnosis";
        entity3.ToDeptId = 1;
      }
      entity3.Priority = "1";
      entity3.VisitType = "N";
      entity3.RoomNo = "";
      ReferralStatus referralStatus1 = entity3;
      now = DateTime.Now;
      DateTime? nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      referralStatus1.FromDate = nullable1;
      ReferralStatus referralStatus2 = entity3;
      now = DateTime.Now;
      DateTime? nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      referralStatus2.CreatedDate = nullable2;
      entity3.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity3.IsApproved = "Y";
      this._Referralservice.Add(entity3);
      if (model.ServiceId != 0)
      {
        BillQueueDetails entity2 = new BillQueueDetails();
        entity2.BillQueueDate = new DateTime?(DateTime.Now);
        entity2.PatientId = num2;
        entity2.DeptId = 15;
        entity2.ServiceId = model.ServiceId;
        entity2.Qty = 1;
        entity2.Rate = this._uow.Repository<MASBillingServices>().Get(model.ServiceId).ServiceAmount;
        entity2.Amount = model.PaidAmt;
        entity2.NetAmount = model.PaidAmt;
        entity2.IsBillPaid = "Y";
        entity2.CreatedBy = model.CreatedBy;
        entity2.CreatedDate = new DateTime?(DateTime.Now);
        entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        int num3 = this._uow.Repository<BillQueueDetails>().Add(entity2, true);
        int num4 = this._uow.Repository<Billing>().Add(new Billing()
        {
          PatientId = num2,
          DeptId = 15,
          BillAmount = model.PaidAmt,
          BillCancelled = "N",
          BillDateTime = DateTime.Now,
          CreatedBy = model.CreatedBy,
          CreatedDate = new DateTime?(DateTime.Now),
          CreatedSystem = this._Dropdownservice.GetIPAddress(false),
          BillNo = this._Billing.GenerateInvoiceNo()
        }, true);
        this._uow.Repository<BillingDetails>().Add(new BillingDetails()
        {
          BillId = num4,
          BillQueueId = num3,
          ServiceId = model.ServiceId,
          ServiceQty = entity2.Qty,
          ServiceRate = this._uow.Repository<MASBillingServices>().Get(model.ServiceId).ServiceAmount,
          ServiceAmount = entity2.NetAmount,
          NetAmount = entity2.NetAmount,
          IsBillPaid = "Y"
        }, true);
        this._uow.Repository<BillingPaymentDetails>().Add(new BillingPaymentDetails()
        {
          BillPayId = 0,
          BillId = num4,
          CodeId = model.PaymodeId,
          AmountReceived = model.PaidAmt
        }, true);
      }
      return num2;
    }

    public OPDPatientRegistrationViewModel BindEditPatientModel(int Id)
    {
      OPDPatientRegistrationViewModel registrationViewModel1 = new OPDPatientRegistrationViewModel();
      OPDPatientRegistrationViewModel patientDetails = this.GetPatientDetails(Id);
      patientDetails.Gender = (Gender) patientDetails.GenderId.Value;
      int? maritalStatusId = patientDetails.MaritalStatusId;
      int num1 = 0;
      if ((maritalStatusId.GetValueOrDefault() > num1 ? (maritalStatusId.HasValue ? 1 : 0) : 0) != 0)
        patientDetails.MaritalStatus = (MaritalStatus) patientDetails.MaritalStatusId.Value;
      int? religionId = patientDetails.ReligionId;
      int num2 = 0;
      if ((religionId.GetValueOrDefault() > num2 ? (religionId.HasValue ? 1 : 0) : 0) != 0)
      {
        OPDPatientRegistrationViewModel registrationViewModel2 = patientDetails;
        religionId = patientDetails.ReligionId;
        int num3 = religionId.Value;
        registrationViewModel2.Religion = (Religion) num3;
      }
      ReferralStatus referralStatus = this._uow.Repository<ReferralStatus>().GetEntitiesBySql(string.Format(Queries.GetReferralDetforPatient, (object) patientDetails.PatientId, (object) 15)).FirstOrDefault<ReferralStatus>();
      if (referralStatus != null)
      {
        patientDetails.ReferredId = referralStatus.ReferredId;
        patientDetails.CategoryEditYN = this._uow.Repository<ReferralStatus>().GetEntitiesBySql(string.Format(Queries.ChkforAllotmentbyReferId, (object) referralStatus.ReferredId)).FirstOrDefault<ReferralStatus>() != null ? "N" : "Y";
      }
      patientDetails.Countries = (IList<SelectListItem>) this.LoadCountries();
      patientDetails.States = (IList<SelectListItem>) this.GetStatesById(patientDetails.CountryId);
      patientDetails.Cities = (IList<SelectListItem>) this.GetCitiesById(patientDetails.StateId);
      patientDetails.CountryId = patientDetails.CountryId;
      patientDetails.StateId = patientDetails.StateId;
      patientDetails.CountryCode = "+91";
      patientDetails.CityId = patientDetails.CityId;
      patientDetails.RegDateDisplay = patientDetails.RegDate.ToString("dd/MM/yyyy HH:mm:ss");
      patientDetails.RegistrationDate = patientDetails.RegDate.ToString("yyyy-MM-dd HH:mm:ss");
      string aadharNo = patientDetails.AadharNo;
      if (aadharNo != null)
      {
        if (aadharNo.Length > 3)
          patientDetails.AadharNo1 = aadharNo.Substring(0, 4);
        if (aadharNo.Length > 7)
          patientDetails.AadharNo2 = aadharNo.Substring(4, 4);
        if (aadharNo.Length > 11)
          patientDetails.AadharNo3 = aadharNo.Substring(8, 4);
      }
      patientDetails.FileName = patientDetails.FileName;
      patientDetails.FilePath = patientDetails.FilePath;
      patientDetails.FileDisplayPath = patientDetails.FileDisplayPath;
      patientDetails.Description = patientDetails.Description;
      patientDetails.postedFiles = patientDetails.postedFiles;
      string whereClause = "delInd=0";
      patientDetails.Categorylist = this._Dropdownservice.GetCategoryByPatientId(patientDetails.PatientId);
      patientDetails.Categorylist = (IEnumerable<MASCategory>) this._uow.Repository<MASCategory>().GetAll(whereClause).ToList<MASCategory>();
      patientDetails.Paymodelist = this._Dropdownservice.GetPaymodeByPatientId(patientDetails.PatientId);
      patientDetails.Paymodelist = (IEnumerable<MASPaymode>) this._uow.Repository<MASPaymode>().GetAll(whereClause).ToList<MASPaymode>();
      patientDetails.Paymodelist = (IEnumerable<MASPaymode>) patientDetails.Paymodelist.Where<MASPaymode>((Func<MASPaymode, bool>) (x =>
      {
        if (x.PaymodeId != 4 && x.PaymodeId != 2)
          return x.PaymodeId != 3;
        return false;
      })).ToList<MASPaymode>();
      patientDetails.Titlelist = this._Dropdownservice.GetCodesById(2);
      patientDetails.BloodGrouplist = this._Dropdownservice.GetCodesById(20);
      OPDPatientRegistrationViewModel registrationViewModel3 = this._uow.Repository<OPDPatientRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetBillDetailsforPatientReg, (object) patientDetails.PatientId, (object) 15)).FirstOrDefault<OPDPatientRegistrationViewModel>();
      if (registrationViewModel3 != null)
      {
        patientDetails.BillId = registrationViewModel3.BillId;
        patientDetails.BillDetId = registrationViewModel3.BillDetId;
        patientDetails.BillQueueId = registrationViewModel3.BillQueueId;
        patientDetails.BillPayId = registrationViewModel3.BillPayId;
        patientDetails.ServiceId = registrationViewModel3.ServiceId;
        patientDetails.BillDateTime = registrationViewModel3.BillDateTime;
      }
      IEnumerable<OPDPatientRegistrationProperties> properties = this.GetProperties();
      patientDetails.Proplist = properties;
      return patientDetails;
    }

    public int UpdatePatient(OPDPatientRegistrationViewModel model)
    {
      try
      {
        model.GenderId = new int?((int) model.Gender);
        model.MaritalStatusId = new int?((int) model.MaritalStatus);
        model.ReligionId = new int?((int) model.Religion);
      //  model.RegDate = Convert.ToDateTime(model.RegistrationDate);
        OPDPatientRegistrationViewModel registrationViewModel1 = model;
        DateTime dateTime1 = model.Dob;
        DateTime dateTime2 = Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd"));
        registrationViewModel1.Dob = dateTime2;
        OPDPatientRegistrationViewModel registrationViewModel2 = model;
        dateTime1 = DateTime.Now;
        DateTime? nullable1 = new DateTime?(Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        registrationViewModel2.ModifiedDate = nullable1;
        model.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
        OPDPatientRegistration patientRegistration = new OPDPatientRegistration();
        Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OPDPatientRegistrationViewModel, OPDPatientRegistration>()));
        OPDPatientRegistration entity1 = Mapper.Map<OPDPatientRegistrationViewModel, OPDPatientRegistration>(model);
        entity1.AadharNo = model.AadharNo1 + model.AadharNo2 + model.AadharNo3;
        Decimal num = new Decimal();
        if (entity1.PaidAmt == Decimal.Zero)
          entity1.NetAmt = num;
        if (model.CategoryEditYN == "N")
          entity1.CategoryId = model.CategoryId;
        HttpPostedFileBase[] postedFiles = model.postedFiles;
        if (postedFiles != null)
        {
          foreach (HttpPostedFileBase httpPostedFileBase in postedFiles)
          {
            if (httpPostedFileBase != null && httpPostedFileBase.ContentLength > 0)
            {
              string[] strArray = new string[4]
              {
                ".pdf",
                ".png",
                ".jpg",
                "jpeg"
              };
              string lower = Path.GetExtension(httpPostedFileBase.FileName).ToLower();
              if (((IEnumerable<string>) strArray).Contains<string>(lower))
              {
                string withoutExtension = Path.GetFileNameWithoutExtension(Path.GetFileName(httpPostedFileBase.FileName));
                string str1 = Guid.NewGuid().ToString();
                string filename = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Upload/") + withoutExtension + str1 + lower);
                string str2 = Path.Combine(withoutExtension + str1 + lower);
                httpPostedFileBase.SaveAs(filename);
                entity1.FileName = withoutExtension;
                entity1.FilePath = filename;
                entity1.FileDisplayPath = str2;
                entity1.Description = model.Description;
              }
            }
          }
        }
        this._uow.Repository<OPDPatientRegistration>().Update(entity1, true);
        IEnumerable<OPDPatientRegistrationProperties> all1 = this._uow.Repository<OPDPatientRegistrationProperties>().GetAll();
        IEnumerable<OPDPatientRegistrationPropertyValues> all2 = this._uow.Repository<OPDPatientRegistrationPropertyValues>().GetAll("PatientId=" + (object) model.PatientId);
        foreach (PropertyInfo property1 in model.GetType().GetProperties())
        {
          PropertyInfo prop = property1;
          if (all1.FirstOrDefault<OPDPatientRegistrationProperties>((Func<OPDPatientRegistrationProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
          {
            string name = prop.Name;
            object obj = prop.GetValue((object) model, (object[]) null);
            if (obj != null)
            {
              OPDPatientRegistrationPropertyValues entity2 = new OPDPatientRegistrationPropertyValues();
              entity2.PatientId = model.PatientId;
              OPDPatientRegistrationProperties property = all1.FirstOrDefault<OPDPatientRegistrationProperties>((Func<OPDPatientRegistrationProperties, bool>) (a => a.PropertyName == prop.Name));
              if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
              {
                OPDPatientRegistrationPropertyValues registrationPropertyValues = all2.FirstOrDefault<OPDPatientRegistrationPropertyValues>((Func<OPDPatientRegistrationPropertyValues, bool>) (a =>
                {
                  if (a.PropId == property.PropertyId)
                    return a.PatientId == model.PatientId;
                  return false;
                }));
                if (registrationPropertyValues != null)
                {
                  entity2.PropId = property.PropertyId;
                  entity2.PropValues = obj.ToString();
                  entity2.ValueId = registrationPropertyValues.ValueId;
                  this._uow.Repository<OPDPatientRegistrationPropertyValues>().Update(entity2, false);
                }
                else
                {
                  entity2.PropId = property.PropertyId;
                  entity2.PropValues = obj.ToString();
                  this._uow.Repository<OPDPatientRegistrationPropertyValues>().Add(entity2, false);
                }
              }
            }
          }
        }
        if (model.BillId != 0 && model.BillDetId != 0 && (model.BillQueueId != 0 && model.BillPayId != 0))
        {
          BillQueueDetails entity2 = new BillQueueDetails()
          {
            BillQueueId = model.BillQueueId,
            PatientId = entity1.PatientId,
            DeptId = 15,
            ServiceId = model.ServiceId,
            Qty = 1
          };
          entity2.Rate = this._uow.Repository<MASBillingServices>().Get(entity2.ServiceId).ServiceAmount;
          entity2.Amount = model.PaidAmt;
          entity2.NetAmount = model.PaidAmt;
          entity2.IsBillPaid = "Y";
          entity2.ModifiedBy = model.ModifiedBy;
          entity2.ModifiedDate = new DateTime?(DateTime.Now);
          entity2.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
          this._uow.Repository<BillQueueDetails>().Update(entity2, true);
          this._uow.Repository<Billing>().Update(new Billing()
          {
            BillId = model.BillId,
            PatientId = entity1.PatientId,
            DeptId = 15,
            BillAmount = model.PaidAmt,
            BillCancelled = "N",
            BillDateTime = Convert.ToDateTime(model.BillDateTime),
            ModifiedBy = model.ModifiedBy,
            ModifiedDate = new DateTime?(DateTime.Now),
            ModifiedSystem = this._Dropdownservice.GetIPAddress(false)
          }, true);
          BillingDetails entity3 = new BillingDetails()
          {
            BillDetId = model.BillDetId,
            BillId = model.BillId,
            BillQueueId = model.BillQueueId,
            ServiceId = model.ServiceId,
            ServiceQty = entity2.Qty
          };
          entity3.ServiceRate = this._uow.Repository<MASBillingServices>().Get(entity3.ServiceId).ServiceAmount;
          entity3.ServiceAmount = entity2.Amount;
          entity3.NetAmount = entity2.NetAmount;
          entity3.IsBillPaid = "Y";
          this._uow.Repository<BillingDetails>().Update(entity3, true);
          this._uow.Repository<BillingPaymentDetails>().Update(new BillingPaymentDetails()
          {
            BillPayId = model.BillPayId,
            BillId = model.BillId,
            CodeId = model.PaymodeId,
            AmountReceived = model.PaidAmt
          }, true);
        }
        ReferralStatus entity4 = new ReferralStatus();
        entity4.PatientId = entity1.PatientId;
        entity4.FromdeptId = 15;
        entity4.ReferredId = model.ReferredId;
        if (model.CategoryId == 3)
        {
          entity4.ReferredReason = "To Diagnosis from Camp";
          entity4.ToDeptId = 8;
        }
        else
        {
          entity4.ReferredReason = "To Diagnosis";
          entity4.ToDeptId = 1;
        }
        entity4.Priority = "1";
        entity4.VisitType = "N";
        entity4.RoomNo = "";
        ReferralStatus referralStatus = entity4;
        dateTime1 = DateTime.Now;
        DateTime? nullable2 = new DateTime?(Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        referralStatus.ModifiedDate = nullable2;
        entity4.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
        this._Referralservice.Update(entity4);
        return entity1.PatientId;
      }
      catch
      {
        throw;
      }
    }

    public OPDPatientRegistrationViewModel DisplayPatient(int Id)
    {
      OPDPatientRegistrationViewModel registrationViewModel1 = new OPDPatientRegistrationViewModel();
      OPDPatientRegistrationViewModel patientDetails = this.GetPatientDetails(Id);
      patientDetails.Gender = (Gender) patientDetails.GenderId.Value;
      patientDetails.TitleName = this._Dropdownservice.GetCodeDescriptionByCodeId(patientDetails.TitleId);
      int? maritalStatusId = patientDetails.MaritalStatusId;
      int num1 = 0;
      if ((maritalStatusId.GetValueOrDefault() > num1 ? (maritalStatusId.HasValue ? 1 : 0) : 0) != 0)
        patientDetails.MaritalStatus = (MaritalStatus) patientDetails.MaritalStatusId.Value;
      int? religionId = patientDetails.ReligionId;
      int num2 = 0;
      if ((religionId.GetValueOrDefault() > num2 ? (religionId.HasValue ? 1 : 0) : 0) != 0)
      {
        OPDPatientRegistrationViewModel registrationViewModel2 = patientDetails;
        religionId = patientDetails.ReligionId;
        int num3 = religionId.Value;
        registrationViewModel2.Religion = (Religion) num3;
      }
      patientDetails.BloodGroupName = this._Dropdownservice.GetCodeDescriptionByCodeId(patientDetails.BloodGroupId);
      MASCategory masCategory = this._uow.Repository<MASCategory>().GetAll("CategoryId =" + (object) patientDetails.CategoryId).FirstOrDefault<MASCategory>();
      patientDetails.CategoryName = masCategory.CategoryName;
      MASPaymode masPaymode = this._uow.Repository<MASPaymode>().GetAll("PaymodeId =" + (object) patientDetails.PaymodeId).FirstOrDefault<MASPaymode>();
      patientDetails.PaymodeName = masPaymode.Paymode;
      patientDetails.CountryName = this._uow.Repository<MASCountry>().Get(patientDetails.CountryId).Name;
      patientDetails.StateName = this._uow.Repository<MASState>().Get(patientDetails.StateId).Name;
      patientDetails.CityName = this._uow.Repository<MASCity>().Get(patientDetails.CityId).Name;
      OPDPatientRegistrationViewModel registrationViewModel3 = patientDetails;
      DateTime dateTime = patientDetails.RegDate;
      string str1 = dateTime.ToString("dd/MM/yyyy HH:mm:ss");
      registrationViewModel3.RegDateDisplay = str1;
      OPDPatientRegistrationViewModel registrationViewModel4 = patientDetails;
      dateTime = patientDetails.Dob;
      string str2 = dateTime.ToString("dd/MM/yyyy");
      registrationViewModel4.DobDisplay = str2;
      IEnumerable<OPDPatientRegistrationProperties> properties = this.GetProperties();
      patientDetails.Proplist = properties;
      return patientDetails;
    }

    private List<SelectListItem> LoadCountries()
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      foreach (CountryViewModal countryViewModal in (IEnumerable<CountryViewModal>) this._Countryservice.CountryList().ToList<CountryViewModal>())
      {
        if (countryViewModal.CountryId == 1)
          selectListItemList.Add(new SelectListItem()
          {
            Text = countryViewModal.Name,
            Value = Convert.ToString(countryViewModal.CountryId),
            Selected = true
          });
        else
          selectListItemList.Add(new SelectListItem()
          {
            Text = countryViewModal.Name,
            Value = Convert.ToString(countryViewModal.CountryId)
          });
      }
      return selectListItemList;
    }

    public List<SelectListItem> GetCitiesById(int id)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      List<SelectListItem> list = this._Cityservice.GetCitiesById(id).Select<CityViewModal, SelectListItem>((Func<CityViewModal, SelectListItem>) (x => new SelectListItem()
      {
        Text = x.Name,
        Value = x.CityId.ToString()
      })).ToList<SelectListItem>();
      list.First<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
      return list;
    }

    public List<SelectListItem> GetStatesById(int id)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      List<SelectListItem> list = this._Stateservice.GetStatesById(id).Select<StateViewModal, SelectListItem>((Func<StateViewModal, SelectListItem>) (x => new SelectListItem()
      {
        Text = x.Name,
        Value = x.StateId.ToString()
      })).ToList<SelectListItem>();
      list.First<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
      return list;
    }

    public MaxOpNo GetMaxOpNo()
    {
      return this._uow.Repository<MaxOpNo>().GetEntitiesBySql(string.Format(string.Format("select 1 as Id, IsNULL(Max(OpNo)+1,dbo.GetOPNo(Getdate()))OpNo from OPDPatientRegistration WHERE CONVERT(char(10),RegDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd")))).FirstOrDefault<MaxOpNo>();
    }

    public List<OPDSearchDetails> opdSearchDetails(string From_Date, string To_Date, string url)
    {
      string str = "(OP.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      return this._uow.Repository<OPDSearchDetails>().GetEntitiesBySql(string.Format(Queries.OPDSearch, (object) From_Date, (object) To_Date, (object) url, (object) str)).ToList<OPDSearchDetails>();
    }

    public List<OPDSearchDetails> opdSearchDetailsList(string From_Date, string To_Date, string url, string OPNo, string PatientName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string empty = string.Empty;
      string str1 = "(OP.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str1 = "(0=0)";
      stringBuilder.Append(str1);
      if (!string.IsNullOrEmpty(OPNo))
      {
        OPNo = " and (OP.OPNo Like '%" + OPNo + "%')";
        stringBuilder.Append(OPNo);
      }
      if (!string.IsNullOrEmpty(PatientName))
      {
        PatientName = " and (OP.PatientName Like '%" + PatientName + "%')";
        stringBuilder.Append(PatientName);
      }
      if (string.IsNullOrEmpty(PatientName) && string.IsNullOrEmpty(OPNo))
      {
        string str2 = " and ((Convert(varchar(10), OP.RegDate, 126))between '" + From_Date + "' and '" + To_Date + "') ";
        stringBuilder.Append(str2);
      }
      return this._uow.Repository<OPDSearchDetails>().GetEntitiesBySql(string.Format(Queries.GetPatientDetails, (object) stringBuilder.ToString(), (object) url)).ToList<OPDSearchDetails>();
    }

    public BillingViewModal BindReport(int BillId, int DeptId)
    {
      BillingViewModal billingViewModal1 = new BillingViewModal();
      BillingViewModal billingViewModal2 = this._Billing.BindReport(BillId, "Billing");
      billingViewModal2.BillServicesListforReport = this._Billing.BillServicesListforReport(BillId, "BillingDetails");
      billingViewModal2.BillPaymentListforReport = this._Billing.BillPaymentListforReport(BillId, "BillingPaymentDetails");
      billingViewModal2.DeptId = DeptId;
      return billingViewModal2;
    }
  }
}
