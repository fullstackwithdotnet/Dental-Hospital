// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.OPDRevisitRegistrationService
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
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class OPDRevisitRegistrationService : ServiceBase<OPDRevisitRegistration>, IOPDRevisitRegistrationService, IService<OPDRevisitRegistration>
  {
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;
    private IReferralStatusService _Referralservice;

    public OPDRevisitRegistrationService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._Referralservice = (IReferralStatusService) new ReferralStatusService(this._uow);
    }

    public OPDRevisitRegistrationViewModel BindPatientModel(int PatientId)
    {
      try
      {
        OPDRevisitRegistrationViewModel registrationViewModel = new OPDRevisitRegistrationViewModel();
        registrationViewModel.PatientId = PatientId;
        OPDPatientRegistration patientRegistration = this._uow.Repository<OPDPatientRegistration>().Get(PatientId);
        registrationViewModel.PatientId = patientRegistration.PatientId;
        registrationViewModel.PatientName = patientRegistration.PatientName;
        registrationViewModel.Age = patientRegistration.Age;
        registrationViewModel.AgeGender = patientRegistration.Age.ToString() + "/" + (object) (Gender) patientRegistration.GenderId;
        registrationViewModel.OPNo = patientRegistration.OpNo;
        registrationViewModel.Phone = patientRegistration.Phone;
        registrationViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(registrationViewModel.PatientId, 14).ToList<ReferralStatusViewModel>();
        registrationViewModel.RevisitFollowUpList = (IEnumerable<FollowupViewModal>) this.RevisitFollowUpList(registrationViewModel.PatientId).ToList<FollowupViewModal>();
        return registrationViewModel;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public List<OPDFollowupSearchDetails> opdFollowupSearchDetails(string From_Date, string To_Date, string url)
    {
      string str = "(OPD.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      return this._uow.Repository<OPDFollowupSearchDetails>().GetEntitiesBySql(string.Format(Queries.OPDFollowupSearch, (object) From_Date, (object) To_Date, (object) url, (object) str)).ToList<OPDFollowupSearchDetails>();
    }

    public List<OPDFollowupSearchDetails> opdFollowupSearchDetailsbyDeptId(string From_Date, string To_Date, int DeptId, string url)
    {
      string str = "(OPD.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      return this._uow.Repository<OPDFollowupSearchDetails>().GetEntitiesBySql(string.Format(Queries.OPDFollowupSearchByDept, (object) From_Date, (object) To_Date, (object) DeptId, (object) url, (object) str)).ToList<OPDFollowupSearchDetails>();
    }

    public List<OPDRevisitSearchDetails> opdRevisitSearchDetails(string From_Date, string To_Date, string url)
    {
      string str = "(OPD.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      return this._uow.Repository<OPDRevisitSearchDetails>().GetEntitiesBySql(string.Format(Queries.OPDRevisitSearch, (object) From_Date, (object) To_Date, (object) url, (object) str)).ToList<OPDRevisitSearchDetails>();
    }

    public IEnumerable<FollowupViewModal> RevisitFollowUpList(int PatientId)
    {
      try
      {
        List<FollowupViewModal> followupViewModalList = new List<FollowupViewModal>();
        return (IEnumerable<FollowupViewModal>) this._uow.Repository<FollowupViewModal>().GetEntitiesBySql(string.Format(Queries.RevisitFollowupList, (object) PatientId)).ToList<FollowupViewModal>();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public IEnumerable<OPDRevisitRegistrationViewModel> RevisitedPatientList()
    {
      try
      {
        List<OPDRevisitRegistrationViewModel> registrationViewModelList = new List<OPDRevisitRegistrationViewModel>();
        return (IEnumerable<OPDRevisitRegistrationViewModel>) this._uow.Repository<OPDRevisitRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.RevisitedList)).OrderByDescending<OPDRevisitRegistrationViewModel, int>((Func<OPDRevisitRegistrationViewModel, int>) (a => a.RevisitId)).ToList<OPDRevisitRegistrationViewModel>();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int RevisitSaveFromFollowup(OPDRevisitRegistrationViewModel model)
    {
      try
      {
        OPDRevisitRegistration revisitRegistration = new OPDRevisitRegistration();
        FollowupViewModal followupViewModal = new FollowupViewModal();
        ReferralStatus entity1 = new ReferralStatus();
        OPDRevisitRegistration entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OPDRevisitRegistrationViewModel, OPDRevisitRegistration>())).CreateMapper().Map<OPDRevisitRegistrationViewModel, OPDRevisitRegistration>(model);
        FollowUp followUp = this._uow.Repository<FollowUp>().GetEntitiesBySql(string.Format(Queries.FollowUpDetailsById, (object) model.FollowupId)).FirstOrDefault<FollowUp>();
        entity2.PatientId = followUp.PatientId;
        entity2.RevisitDate = new DateTime?(DateTime.Now);
        entity2.CreatedDate = new DateTime?(DateTime.Now);
        entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        int num = this._uow.Repository<OPDRevisitRegistration>().Add(entity2, false);
        entity1.PatientId = followUp.PatientId;
        entity1.FromdeptId = 14;
        entity1.FromDate = new DateTime?(DateTime.Now);
        entity1.ReferredReason = followUp.FollowupReason;
        entity1.ToDeptId = followUp.DeptId;
        entity1.Priority = "";
        entity1.RoomNo = "";
        entity1.VisitType = "R";
        entity1.RevisitId = num;
        entity1.FromDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        entity1.CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        this._Referralservice.Add(entity1);
        string str1 = "Visited";
        string str2 = "";
        this._uow.Repository<FollowUp>().GetEntitiesBySql(string.Format("update Followup set RevisitId=" + (object) num + ",Status='" + str1 + "',IgnoreReason='" + str2 + "' where FollowupId=" + (object) model.FollowupId + " and PatientId=" + (object) entity2.PatientId + " "));
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int RevisitSave(OPDRevisitRegistrationViewModel model)
    {
      try
      {
        OPDRevisitRegistration revisitRegistration = new OPDRevisitRegistration();
        ReferralStatus entity1 = new ReferralStatus();
        OPDRevisitRegistration entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OPDRevisitRegistrationViewModel, OPDRevisitRegistration>())).CreateMapper().Map<OPDRevisitRegistrationViewModel, OPDRevisitRegistration>(model);
        entity2.RevisitDate = new DateTime?(DateTime.Now);
        entity2.PatientId = model.PatientId;
        entity2.CreatedDate = new DateTime?(DateTime.Now);
        entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        int num = this._uow.Repository<OPDRevisitRegistration>().Add(entity2, false);
        if (model.CreatedepartmentReferredStatus != null)
        {
          foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
          {
            if (createdepartmentReferredStatu.chkToDeptId)
            {
              entity1.PatientId = entity2.PatientId;
              entity1.FromdeptId = 14;
              entity1.FromDate = new DateTime?(DateTime.Now);
              entity1.ToDeptId = createdepartmentReferredStatu.ToDeptId;
              entity1.Priority = createdepartmentReferredStatu.Priority;
              entity1.ReferredReason = createdepartmentReferredStatu.ReferredReason;
              entity1.RoomNo = createdepartmentReferredStatu.RoomNo;
              entity1.VisitType = "R";
              entity1.RevisitId = num;
              ReferralStatus referralStatus1 = entity1;
              DateTime now = DateTime.Now;
              DateTime? nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
              referralStatus1.FromDate = nullable1;
              ReferralStatus referralStatus2 = entity1;
              now = DateTime.Now;
              DateTime? nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
              referralStatus2.CreatedDate = nullable2;
              entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
              entity1.IsApproved = "Y";
              this._Referralservice.Add(entity1);
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

    public OPDRevisitRegistrationViewModel EditBindOPDRViewmodel(int PatientId, int Revisitid)
    {
      try
      {
        OPDRevisitRegistrationViewModel registrationViewModel = new OPDRevisitRegistrationViewModel();
        registrationViewModel.PatientId = PatientId;
        OPDPatientRegistration patientRegistration = this._uow.Repository<OPDPatientRegistration>().Get(PatientId);
        OPDRevisitRegistration revisitRegistration = this._uow.Repository<OPDRevisitRegistration>().Get(Revisitid);
        if (patientRegistration != null && revisitRegistration != null)
        {
          registrationViewModel.PatientId = patientRegistration.PatientId;
          registrationViewModel.PatientName = patientRegistration.PatientName;
          registrationViewModel.Age = patientRegistration.Age;
          registrationViewModel.AgeGender = patientRegistration.Age.ToString() + "/" + (object) (Gender) patientRegistration.GenderId;
          registrationViewModel.OPNo = patientRegistration.OpNo;
          registrationViewModel.Phone = patientRegistration.Phone;
          registrationViewModel.RevisitDate = Convert.ToDateTime((object) revisitRegistration.RevisitDate);
          registrationViewModel.CreatedepartmentReferredStatus = this._uow.Repository<ReferralStatusViewModel>().GetEntitiesBySql(string.Format(Queries.BindRevisitReferralList, (object) PatientId, (object) Revisitid)).ToList<ReferralStatusViewModel>();
        }
        return registrationViewModel;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateRevisit(OPDRevisitRegistrationViewModel ViewModel)
    {
      try
      {
        OPDRevisitRegistration revisitRegistration1 = new OPDRevisitRegistration();
        ReferralStatus entity1 = new ReferralStatus();
        Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OPDRevisitRegistrationViewModel, OPDRevisitRegistration>()));
        OPDRevisitRegistration entity2 = Mapper.Map<OPDRevisitRegistrationViewModel, OPDRevisitRegistration>(ViewModel);
        OPDRevisitRegistration revisitRegistration2 = entity2;
        DateTime now = DateTime.Now;
        DateTime? nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        revisitRegistration2.ModifiedDate = nullable1;
        entity2.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
        this._uow.Repository<OPDRevisitRegistration>().Update(entity2, false);
        int revisitId = entity2.RevisitId;
        if (ViewModel.CreatedepartmentReferredStatus != null)
        {
          foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
          {
            entity1.PatientId = entity2.PatientId;
            entity1.FromdeptId = 14;
            entity1.FromDate = new DateTime?(DateTime.Now);
            entity1.ToDeptId = createdepartmentReferredStatu.ToDeptId;
            entity1.Priority = createdepartmentReferredStatu.Priority;
            entity1.ReferredReason = createdepartmentReferredStatu.ReferredReason;
            entity1.RoomNo = createdepartmentReferredStatu.RoomNo;
            entity1.VisitType = "R";
            entity1.RevisitId = revisitId;
            ReferralStatus referralStatus1 = entity1;
            now = DateTime.Now;
            DateTime? nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus1.FromDate = nullable2;
            ReferralStatus referralStatus2 = entity1;
            now = DateTime.Now;
            DateTime? nullable3 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus2.ModifiedDate = nullable3;
            entity1.IsApproved = "Y";
            entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
            if (createdepartmentReferredStatu.chkToDeptId)
            {
              if (createdepartmentReferredStatu.ReferredId > 0L)
                this._Referralservice.Update(entity1);
              else
                this._Referralservice.Add(entity1);
            }
            else if (createdepartmentReferredStatu.ReferredId > 0L && !createdepartmentReferredStatu.chkToDeptId)
              this._uow.Repository<ReferralStatus>().GetEntitiesBySql(string.Format("Delete from ReferralStatus where ReferredId=" + (object) createdepartmentReferredStatu.ReferredId + " "));
          }
        }
        return revisitId;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public FollowupViewModal DisplayRescheduleDetails(int FollowupId, int DeptId)
    {
      FollowupViewModal followupViewModal = new FollowupViewModal();
      if (!string.IsNullOrEmpty(FollowupId.ToString()))
      {
        followupViewModal = this._uow.Repository<FollowupViewModal>().GetEntitiesBySql(string.Format(Queries.GetPatientDetailsforReschedule, (object) FollowupId)).SingleOrDefault<FollowupViewModal>();
        PatientInformationViewModel informationViewModel = new PatientInformationViewModel();
        informationViewModel.PatientName = followupViewModal.PatientName;
        informationViewModel.OpNo = followupViewModal.OpNo;
        informationViewModel.AgeGender = followupViewModal.Age.ToString() + "/" + followupViewModal.Gender;
        followupViewModal.FollowupId = FollowupId;
        followupViewModal.DeptId = DeptId;
        followupViewModal.patientInformationViewModel = informationViewModel;
        Department department = (Department) DeptId;
        followupViewModal.DeptCode = department.ToString();
        followupViewModal.FollowupDate = DateTime.Now;
        followupViewModal.FollowupTime = DateTime.Now;
      }
      return followupViewModal;
    }
  }
}
