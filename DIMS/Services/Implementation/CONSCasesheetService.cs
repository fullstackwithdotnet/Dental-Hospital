// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.CONSCasesheetService
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
  public class CONSCasesheetService : ServiceBase<CONSCasesheet>, ICONSCasesheetService, IService<CONSCasesheet>
  {
    private IUnitOfWork _uow;
    private IOPDPatientRegistrationService _OPDservice;
    private IMASCodeService _Dropdownservice;
    private IReferralStatusService _Referralservice;
    private IBillQueueService _BillQueueservice;
    private IMASChiefComplaintService _ChiefComplaintservice;
    private IStudentAllotmentService _AllotmentService;
    private IFollowUpService _FollowUpService;
    private ICasesheetNoService _CasesheetNoService;
    private IPrescriptionsService _PrescriptionsService;

    public CONSCasesheetService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._OPDservice = (IOPDPatientRegistrationService) new OPDPatientRegistrationService(this._uow);
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._Referralservice = (IReferralStatusService) new ReferralStatusService(this._uow);
      this._BillQueueservice = (IBillQueueService) new BillQueueService(this._uow);
      this._ChiefComplaintservice = (IMASChiefComplaintService) new MASChiefComplaintService(this._uow);
      this._AllotmentService = (IStudentAllotmentService) new StudentAllotmentService(this._uow);
      this._FollowUpService = (IFollowUpService) new FollowUpService(this._uow);
      this._CasesheetNoService = (ICasesheetNoService) new CasesheetNoService(this._uow);
      this._PrescriptionsService = (IPrescriptionsService) new PrescriptionsService(this._uow);
    }

    public CONSViewModel GetConsPatientDetails(int id)
    {
      return this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format("exec GetCONSCasesheet {0}", (object) id)).FirstOrDefault<CONSViewModel>();
    }

    public IEnumerable<CONSCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<CONSCasesheetProperties>().GetAll();
    }

    public CONSViewModel BindCONSPatientModel(long allotId)
    {
      CONSViewModel consViewModel = new CONSViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = " DeptId=" + (object) 4;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 4);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        consViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        consViewModel.patientInformationViewModel = informationViewModel;
        consViewModel.studentAllotmentViewModel = allotmentViewModel;
        consViewModel.PatientId = informationViewModel.PatientId;
        consViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        consViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 4, 0);
        consViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 4);
        consViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(4);
        consViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 4, 0);
        consViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 4);
        consViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        consViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 4).ToList<ReferralStatusViewModel>();
        consViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 4, 0);
        consViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        consViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        consViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        consViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) consViewModel.PatientId))
        };
        consViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) consViewModel.PatientId))
        };
        IEnumerable<CONSCasesheetProperties> properties = this.GetProperties();
        consViewModel.Proplist = properties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        consViewModel.medicalalertviewmodel = medicalAlertViewModel2;
      }
      return consViewModel;
    }

    public int SaveCONSPatient(CONSViewModel model)
    {
      try
      {
        model.MandatoryDummy = "Y";
        CONSCasesheet consCasesheet = new CONSCasesheet();
        CONSCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<CONSViewModel, CONSCasesheet>())).CreateMapper().Map<CONSViewModel, CONSCasesheet>(model);
        entity1.ConservativeDate = new DateTime?(DateTime.Now);
        entity1.CreatedDate = new DateTime?(DateTime.Now);
        entity1.LastVisitedDate = new DateTime?(DateTime.Now);
        entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        entity1.CONSNo = this._CasesheetNoService.GetCONSNo();
        int num = this._uow.Repository<CONSCasesheet>().Add(entity1, false);
        IEnumerable<CONSCasesheetProperties> all = this._uow.Repository<CONSCasesheetProperties>().GetAll();
        foreach (PropertyInfo property in model.GetType().GetProperties())
        {
          PropertyInfo prop = property;
          if (all.FirstOrDefault<CONSCasesheetProperties>((Func<CONSCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
          {
            string name = prop.Name;
            object obj = prop.GetValue((object) model, (object[]) null);
            if (obj != null)
            {
              CONSCasesheetPropertyValues entity2 = new CONSCasesheetPropertyValues();
              entity2.ConservativeId = num;
              CONSCasesheetProperties casesheetProperties = all.FirstOrDefault<CONSCasesheetProperties>((Func<CONSCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
              if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
              {
                entity2.PropId = casesheetProperties.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<CONSCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
        model.ConservativeId = num;
        if (model.CreatedepartmentReferredStatus != null)
        {
          ReferralStatus entity2 = new ReferralStatus();
          foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
          {
            if (createdepartmentReferredStatu.chkToDeptId)
            {
              entity2.PatientId = model.PatientId;
              entity2.FromdeptId = 4;
              entity2.ReferredReason = createdepartmentReferredStatu.ReferredReason;
              entity2.ToDeptId = createdepartmentReferredStatu.ToDeptId;
              entity2.Priority = createdepartmentReferredStatu.Priority;
              entity2.RoomNo = createdepartmentReferredStatu.RoomNo;
              entity2.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
              ReferralStatus referralStatus1 = entity2;
              DateTime now = DateTime.Now;
              DateTime? nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
              referralStatus1.FromDate = nullable1;
              ReferralStatus referralStatus2 = entity2;
              now = DateTime.Now;
              DateTime? nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
              referralStatus2.CreatedDate = nullable2;
              entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
              this._Referralservice.Add(entity2);
            }
          }
        }
        if (model.BillingQueueDetails != null)
          this._BillQueueservice.SaveInvestigation(model.BillingQueueDetails, new BillQueueDetails()
          {
            CaserecordId = num,
            ReferredTreatmentId = 0,
            DeptId = 4,
            PatientId = model.PatientId
          });
        if (model.BillingLabRadQueueDetails != null)
          this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
          {
            CaserecordId = num,
            ReferredTreatmentId = 0,
            DeptId = 4,
            PatientId = model.PatientId
          });
        if (model.PrescriptionsDetails != null)
          this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
          {
            PatientId = model.PatientId,
            AllotId = model.studentAllotmentViewModel.AllotId,
            DoctorId = model.studentAllotmentViewModel.DoctorId,
            StudentId = model.studentAllotmentViewModel.StudentId,
            DeptId = 4,
            ReferredTreatmentId = 0
          });
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public CONSViewModel BindEditCONSPatientModel(long allotId, int CONSId)
    {
      try
      {
        this._Dropdownservice.GetAllCodes();
        CONSViewModel consPatientDetails = this.GetConsPatientDetails(CONSId);
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 4;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 4);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        allotmentViewModel.DoctorId = informationViewModel.DoctorId;
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = informationViewModel.PatientId,
          FromdeptId = 4
        };
        consPatientDetails.patientInformationViewModel = informationViewModel;
        consPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 4).ToList<ReferralStatusViewModel>();
        consPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        consPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 4).ToList<ReferralStatusViewModel>();
        consPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 4, 0);
        consPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 4, 0);
        consPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 4);
        consPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 4);
        consPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(4);
        consPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        consPatientDetails.studentAllotmentViewModel = allotmentViewModel;
        consPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 4, 0);
        consPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        consPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        consPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        consPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
        consPatientDetails.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 4,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        consPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 4, 0);
        consPatientDetails.RpList = this.RpList(CONSId);
        consPatientDetails.PcList = this.PcList(CONSId);
        consPatientDetails.SpList = this.SpList(CONSId);
        consPatientDetails.EcList = this.EcList(CONSId);
        consPatientDetails.BlList = this.BlList(CONSId);
        consPatientDetails.TtList = this.TtList(CONSId);
        consPatientDetails.RcList = this.RcList(CONSId);
        consPatientDetails.ReList = this.ReList(CONSId);
        consPatientDetails.RfList = this.RfList(CONSId);
        IEnumerable<CONSCasesheetProperties> properties = this.GetProperties();
        consPatientDetails.Proplist = properties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        consPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
        consPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) consPatientDetails.PatientId))
        };
        consPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) consPatientDetails.PatientId))
        };
        consPatientDetails.approvalViewModal = new ApprovalViewModal()
        {
          ApprovalTypeId = 1,
          CaserecordId = CONSId,
          DeptId = 4,
          DoctorId = allotmentViewModel.DoctorId,
          PatientId = (long) consPatientDetails.PatientId,
          ReferredTreatmentId = 0
        };
        if (consPatientDetails.Approval1)
        {
          consPatientDetails.DisplayApproval1 = "Approved";
          consPatientDetails.ReadOnlyApproval1 = true;
        }
        else
        {
          consPatientDetails.DisplayApproval1 = "Not Approved";
          consPatientDetails.ReadOnlyApproval1 = false;
        }
        if (consPatientDetails.Approval2)
        {
          consPatientDetails.DisplayApproval2 = "Approved";
          consPatientDetails.ReadOnlyApproval2 = true;
          consPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
        }
        else
        {
          consPatientDetails.DisplayApproval2 = "Not Approved";
          consPatientDetails.ReadOnlyApproval2 = false;
          consPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
        }
        if (consPatientDetails.Approval3)
        {
          consPatientDetails.DisplayApproval3 = "Casesheet Closed";
          consPatientDetails.ReadOnlyApproval3 = true;
          consPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
          consPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
        }
        else
        {
          consPatientDetails.DisplayApproval3 = "Not Approved";
          consPatientDetails.ReadOnlyApproval3 = false;
          consPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
          consPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
        }
        CONSViewModel consViewModel = this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayConsApprovalStage, (object) 4, (object) consPatientDetails.ConservativeId)).FirstOrDefault<CONSViewModel>();
        consPatientDetails.ApprovalStage = consViewModel == null ? "Not Initiated" : (!(consViewModel.ApprovalStage == "") ? consViewModel.ApprovalStage : "Not Initiated");
        consPatientDetails.ReadOnlyApproval4 = true;
        return consPatientDetails;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public IEnumerable<CONSViewModel> RpList(int CONSId)
    {
      List<CONSViewModel> consViewModelList = new List<CONSViewModel>();
      return (IEnumerable<CONSViewModel>) this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.GetToothNumByConsId, (object) CONSId)).ToList<CONSViewModel>();
    }

    public IEnumerable<CONSViewModel> PcList(int CONSId)
    {
      List<CONSViewModel> consViewModelList = new List<CONSViewModel>();
      return (IEnumerable<CONSViewModel>) this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.GetPcToothNumDetByConsId, (object) CONSId)).ToList<CONSViewModel>();
    }

    public IEnumerable<CONSViewModel> SpList(int CONSId)
    {
      List<CONSViewModel> consViewModelList = new List<CONSViewModel>();
      return (IEnumerable<CONSViewModel>) this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.GetSpToothNumDetByConsId, (object) CONSId)).ToList<CONSViewModel>();
    }

    public IEnumerable<CONSViewModel> EcList(int CONSId)
    {
      List<CONSViewModel> consViewModelList = new List<CONSViewModel>();
      return (IEnumerable<CONSViewModel>) this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.GetEcToothNumDetByConsId, (object) CONSId)).ToList<CONSViewModel>();
    }

    public IEnumerable<CONSViewModel> BlList(int CONSId)
    {
      List<CONSViewModel> consViewModelList = new List<CONSViewModel>();
      return (IEnumerable<CONSViewModel>) this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.GetBlToothNumDetByConsId, (object) CONSId)).ToList<CONSViewModel>();
    }

    public IEnumerable<CONSViewModel> TtList(int CONSId)
    {
      List<CONSViewModel> consViewModelList = new List<CONSViewModel>();
      return (IEnumerable<CONSViewModel>) this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.GetTtToothNumDetByConsId, (object) CONSId)).ToList<CONSViewModel>();
    }

    public IEnumerable<CONSViewModel> RcList(int CONSId)
    {
      List<CONSViewModel> consViewModelList = new List<CONSViewModel>();
      return (IEnumerable<CONSViewModel>) this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.GetRcToothNumDetByConsId, (object) CONSId)).ToList<CONSViewModel>();
    }

    public IEnumerable<CONSViewModel> ReList(int CONSId)
    {
      List<CONSViewModel> consViewModelList = new List<CONSViewModel>();
      return (IEnumerable<CONSViewModel>) this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.GetReToothNumDetByConsId, (object) CONSId)).ToList<CONSViewModel>();
    }

    public IEnumerable<CONSViewModel> RfList(int CONSId)
    {
      List<CONSViewModel> consViewModelList = new List<CONSViewModel>();
      return (IEnumerable<CONSViewModel>) this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.GetRfToothNumDetByConsId, (object) CONSId)).ToList<CONSViewModel>();
    }

    public int UpdateCONSPatient(CONSViewModel ViewModel)
    {
      ViewModel.ConservativeDate = Convert.ToDateTime(ViewModel.ConservativeDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      ViewModel.ConservativeId = ViewModel.ConservativeId;
      CONSCasesheet consCasesheet = new CONSCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<CONSViewModel, CONSCasesheet>()));
      CONSCasesheet entity1 = Mapper.Map<CONSViewModel, CONSCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      this._uow.Repository<CONSCasesheet>().Update(entity1, false);
      IEnumerable<CONSCasesheetProperties> all1 = this._uow.Repository<CONSCasesheetProperties>().GetAll();
      IEnumerable<CONSCasesheetPropertyValues> all2 = this._uow.Repository<CONSCasesheetPropertyValues>().GetAll("ConservativeId=" + (object) entity1.ConservativeId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<CONSCasesheetProperties>((Func<CONSCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            CONSCasesheetPropertyValues entity2 = new CONSCasesheetPropertyValues();
            entity2.ConservativeId = ViewModel.ConservativeId;
            CONSCasesheetProperties property = all1.FirstOrDefault<CONSCasesheetProperties>((Func<CONSCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              CONSCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<CONSCasesheetPropertyValues>((Func<CONSCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.ConservativeId == ViewModel.ConservativeId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                entity2.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<CONSCasesheetPropertyValues>().Update(entity2, false);
              }
              else
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<CONSCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
      }
      ReferralStatus entity3 = new ReferralStatus();
      if (ViewModel.CreatedepartmentReferredStatus != null)
      {
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity3.PatientId = ViewModel.PatientId;
            entity3.FromdeptId = 4;
            entity3.ReferredReason = createdepartmentReferredStatu.ReferredReason;
            entity3.ToDeptId = createdepartmentReferredStatu.ToDeptId;
            entity3.Priority = createdepartmentReferredStatu.Priority;
            entity3.RoomNo = createdepartmentReferredStatu.RoomNo;
            entity3.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
            entity3.FromDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            entity3.CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            entity3.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            this._Referralservice.Add(entity3);
          }
        }
      }
      if (ViewModel.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ConservativeId,
          ReferredTreatmentId = 0,
          DeptId = 4,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ConservativeId,
          ReferredTreatmentId = 0,
          DeptId = 4,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 4,
          ReferredTreatmentId = 0
        });
      CONSRestorativeProcedureDetails entity4 = new CONSRestorativeProcedureDetails();
      ToothNumbers toothNumbers;
      if (ViewModel.RpList != null)
      {
        foreach (CONSViewModel rp in ViewModel.RpList)
        {
          if (rp.RestorativeProId != 0)
          {
            if (rp.ToothNumber != null)
            {
              entity4.ToothNumber = rp.ToothNumber.ToString();
            }
            else
            {
              CONSRestorativeProcedureDetails procedureDetails = entity4;
              toothNumbers = rp.ToothNumbers;
              string str = toothNumbers.ToString();
              procedureDetails.ToothNumber = str;
            }
            entity4.RestorativeProId = rp.RestorativeProId;
            entity4.CavityType = rp.CavityType;
            entity4.PhotographsCastsXray = rp.PhotographsCastsXray;
            entity4.RestorativeMaterial = rp.RestorativeMaterial;
            entity4.CavityPreparation = rp.CavityPreparation;
            entity4.ImpressionPattern = rp.ImpressionPattern;
            entity4.LinearBasesVarnish = rp.LinearBasesVarnish;
            entity4.MatrixBandWedges = rp.MatrixBandWedges;
            entity4.Restoration = rp.Restoration;
            entity4.FinishingAndRestoration = rp.FinishingAndRestoration;
            entity4.DirectIndirectReview1 = rp.DirectIndirectReview1;
            entity4.DirectIndirectReview2 = rp.DirectIndirectReview2;
            entity4.DirectIndirectReview3 = rp.DirectIndirectReview3;
            entity4.DeepCariesReview1 = rp.DeepCariesReview1;
            entity4.DeepCariesReview2 = rp.DeepCariesReview2;
            entity4.DeepCariesReview3 = rp.DeepCariesReview3;
            this._uow.Repository<CONSRestorativeProcedureDetails>().Update(entity4, false);
          }
          else
          {
            entity4.RestorativeProId = 0;
            entity4.ConservativeId = ViewModel.ConservativeId;
            entity4.CavityType = rp.CavityType;
            CONSRestorativeProcedureDetails procedureDetails = entity4;
            toothNumbers = rp.ToothNumbers;
            string str = toothNumbers.ToString();
            procedureDetails.ToothNumber = str;
            entity4.PhotographsCastsXray = rp.PhotographsCastsXray;
            entity4.RestorativeMaterial = rp.RestorativeMaterial;
            entity4.CavityPreparation = rp.CavityPreparation;
            entity4.ImpressionPattern = rp.ImpressionPattern;
            entity4.LinearBasesVarnish = rp.LinearBasesVarnish;
            entity4.MatrixBandWedges = rp.MatrixBandWedges;
            entity4.Restoration = rp.Restoration;
            entity4.FinishingAndRestoration = rp.FinishingAndRestoration;
            entity4.DirectIndirectReview1 = rp.DirectIndirectReview1;
            entity4.DirectIndirectReview2 = rp.DirectIndirectReview2;
            entity4.DirectIndirectReview3 = rp.DirectIndirectReview3;
            entity4.DeepCariesReview1 = rp.DeepCariesReview1;
            entity4.DeepCariesReview2 = rp.DeepCariesReview2;
            entity4.DeepCariesReview3 = rp.DeepCariesReview3;
            this._uow.Repository<CONSRestorativeProcedureDetails>().Add(entity4, false);
          }
        }
      }
      CONSPostAndCoreDetails entity5 = new CONSPostAndCoreDetails();
      ToothNumbers toothNumbersA;
      if (ViewModel.PcList != null)
      {
        foreach (CONSViewModel pc in ViewModel.PcList)
        {
          if (pc.PostAndCoreId != 0)
          {
            if (pc.ToothNumber != null)
            {
              entity5.ToothNumber = pc.ToothNumber.ToString();
            }
            else
            {
              CONSPostAndCoreDetails postAndCoreDetails = entity5;
              toothNumbersA = pc.ToothNumbersA;
              string str = toothNumbersA.ToString();
              postAndCoreDetails.ToothNumber = str;
            }
            entity5.PostAndCoreId = pc.PostAndCoreId;
            entity5.PhotographsImpression = pc.PhotographsImpression;
            entity5.TypeOfPost = pc.TypeOfPost;
            entity5.PostSpacePreparation = pc.PostSpacePreparation;
            entity5.PatternImpression = pc.PatternImpression;
            entity5.Temporization = pc.Temporization;
            entity5.InsertionCementation = pc.InsertionCementation;
            entity5.InsertionTemporization = pc.InsertionTemporization;
            entity5.PcReview1 = pc.PcReview1;
            entity5.PcReview2 = pc.PcReview2;
            entity5.PcReview3 = pc.PcReview3;
            this._uow.Repository<CONSPostAndCoreDetails>().Update(entity5, false);
          }
          else
          {
            entity5.PostAndCoreId = 0;
            entity5.ConservativeId = ViewModel.ConservativeId;
            CONSPostAndCoreDetails postAndCoreDetails = entity5;
            toothNumbersA = pc.ToothNumbersA;
            string str = toothNumbersA.ToString();
            postAndCoreDetails.ToothNumber = str;
            entity5.PhotographsImpression = pc.PhotographsImpression;
            entity5.TypeOfPost = pc.TypeOfPost;
            entity5.PostSpacePreparation = pc.PostSpacePreparation;
            entity5.PatternImpression = pc.PatternImpression;
            entity5.Temporization = pc.Temporization;
            entity5.InsertionCementation = pc.InsertionCementation;
            entity5.InsertionTemporization = pc.InsertionTemporization;
            entity5.PcReview1 = pc.PcReview1;
            entity5.PcReview2 = pc.PcReview2;
            entity5.PcReview3 = pc.PcReview3;
            this._uow.Repository<CONSPostAndCoreDetails>().Add(entity5, false);
          }
        }
      }
      CONSSurgicalProcedureDetails entity6 = new CONSSurgicalProcedureDetails();
      ToothNumbers toothNumbersB;
      if (ViewModel.SpList != null)
      {
        foreach (CONSViewModel sp in ViewModel.SpList)
        {
          if (sp.SurgicalProId != 0)
          {
            if (sp.ToothNumber != null)
            {
              entity6.ToothNumber = sp.ToothNumber.ToString();
            }
            else
            {
              CONSSurgicalProcedureDetails procedureDetails = entity6;
              toothNumbersB = sp.ToothNumbersB;
              string str = toothNumbersB.ToString();
              procedureDetails.ToothNumber = str;
            }
            entity6.SurgicalProId = sp.SurgicalProId;
            entity6.Premedication = sp.Premedication;
            entity6.AnesthesiaUsed = sp.AnesthesiaUsed;
            entity6.FlapDesign = sp.FlapDesign;
            entity6.Graft = sp.Graft;
            entity6.Suturing = sp.Suturing;
            entity6.SpReview1 = sp.SpReview1;
            entity6.SpReview2 = sp.SpReview2;
            entity6.SpReview3 = sp.SpReview3;
            this._uow.Repository<CONSSurgicalProcedureDetails>().Update(entity6, false);
          }
          else
          {
            entity6.SurgicalProId = 0;
            entity6.ConservativeId = ViewModel.ConservativeId;
            CONSSurgicalProcedureDetails procedureDetails = entity6;
            toothNumbersB = sp.ToothNumbersB;
            string str = toothNumbersB.ToString();
            procedureDetails.ToothNumber = str;
            entity6.Premedication = sp.Premedication;
            entity6.AnesthesiaUsed = sp.AnesthesiaUsed;
            entity6.FlapDesign = sp.FlapDesign;
            entity6.Graft = sp.Graft;
            entity6.Suturing = sp.Suturing;
            entity6.SpReview1 = sp.SpReview1;
            entity6.SpReview2 = sp.SpReview2;
            entity6.SpReview3 = sp.SpReview3;
            this._uow.Repository<CONSSurgicalProcedureDetails>().Add(entity6, false);
          }
        }
      }
      CONSEstheticCorrectionDetails entity7 = new CONSEstheticCorrectionDetails();
      ToothNumbers toothNumbersC;
      if (ViewModel.EcList != null)
      {
        foreach (CONSViewModel ec in ViewModel.EcList)
        {
          if (ec.EstheticCorrId != 0)
          {
            if (ec.ToothNumber != null)
            {
              entity7.ToothNumber = ec.ToothNumber.ToString();
            }
            else
            {
              CONSEstheticCorrectionDetails correctionDetails = entity7;
              toothNumbersC = ec.ToothNumbersC;
              string str = toothNumbersC.ToString();
              correctionDetails.ToothNumber = str;
            }
            entity7.EstheticCorrId = ec.EstheticCorrId;
            entity7.ImpressionPhotograph = ec.ImpressionPhotograph;
            entity7.VitalityTesting = ec.VitalityTesting;
            entity7.RadiographicInterpretation = ec.RadiographicInterpretation;
            entity7.TreatmentProcedure = ec.TreatmentProcedure;
            entity7.ToothPreparation = ec.ToothPreparation;
            entity7.ShadeSelection = ec.ShadeSelection;
            entity7.EcInsertionCementation = ec.EcInsertionCementation;
            entity7.FinishingPolishing = ec.FinishingPolishing;
            entity7.EcReview1 = ec.EcReview1;
            entity7.EcReview2 = ec.EcReview2;
            this._uow.Repository<CONSEstheticCorrectionDetails>().Update(entity7, false);
          }
          else
          {
            entity7.EstheticCorrId = 0;
            entity7.ConservativeId = ViewModel.ConservativeId;
            CONSEstheticCorrectionDetails correctionDetails = entity7;
            toothNumbersC = ec.ToothNumbersC;
            string str = toothNumbersC.ToString();
            correctionDetails.ToothNumber = str;
            entity7.ImpressionPhotograph = ec.ImpressionPhotograph;
            entity7.VitalityTesting = ec.VitalityTesting;
            entity7.RadiographicInterpretation = ec.RadiographicInterpretation;
            entity7.TreatmentProcedure = ec.TreatmentProcedure;
            entity7.ToothPreparation = ec.ToothPreparation;
            entity7.ShadeSelection = ec.ShadeSelection;
            entity7.EcInsertionCementation = ec.EcInsertionCementation;
            entity7.FinishingPolishing = ec.FinishingPolishing;
            entity7.EcReview1 = ec.EcReview1;
            entity7.EcReview2 = ec.EcReview2;
            this._uow.Repository<CONSEstheticCorrectionDetails>().Add(entity7, false);
          }
        }
      }
      CONSBleachingDetails entity8 = new CONSBleachingDetails();
      ToothNumbers toothNumbersD;
      if (ViewModel.BlList != null)
      {
        foreach (CONSViewModel bl in ViewModel.BlList)
        {
          if (bl.BleachingId != 0)
          {
            if (bl.ToothNumber != null)
            {
              entity8.ToothNumber = bl.ToothNumber.ToString();
            }
            else
            {
              CONSBleachingDetails bleachingDetails = entity8;
              toothNumbersD = bl.ToothNumbersD;
              string str = toothNumbersD.ToString();
              bleachingDetails.ToothNumber = str;
            }
            entity8.BleachingId = bl.BleachingId;
            entity8.BlVitalityTesting = bl.BlVitalityTesting;
            entity8.PhotographsCasts = bl.PhotographsCasts;
            entity8.BlRadiographicInterpretation = bl.BlRadiographicInterpretation;
            entity8.DiscolouredToothShade = bl.DiscolouredToothShade;
            entity8.AdjacentToothShade = bl.AdjacentToothShade;
            entity8.BleachingProcedure = bl.BleachingProcedure;
            entity8.BReview1 = bl.BReview1;
            entity8.BReview2 = bl.BReview2;
            entity8.BReview3 = bl.BReview3;
            entity8.ProsthesisType = bl.ProsthesisType;
            this._uow.Repository<CONSBleachingDetails>().Update(entity8, false);
          }
          else
          {
            entity8.BleachingId = 0;
            entity8.ConservativeId = ViewModel.ConservativeId;
            CONSBleachingDetails bleachingDetails = entity8;
            toothNumbersD = bl.ToothNumbersD;
            string str = toothNumbersD.ToString();
            bleachingDetails.ToothNumber = str;
            entity8.BlVitalityTesting = bl.BlVitalityTesting;
            entity8.PhotographsCasts = bl.PhotographsCasts;
            entity8.BlRadiographicInterpretation = bl.BlRadiographicInterpretation;
            entity8.DiscolouredToothShade = bl.DiscolouredToothShade;
            entity8.AdjacentToothShade = bl.AdjacentToothShade;
            entity8.BleachingProcedure = bl.BleachingProcedure;
            entity8.BReview1 = bl.BReview1;
            entity8.BReview2 = bl.BReview2;
            entity8.BReview3 = bl.BReview3;
            entity8.ProsthesisType = bl.ProsthesisType;
            this._uow.Repository<CONSBleachingDetails>().Add(entity8, false);
          }
        }
      }
      CONSTraumatisedToothDetails entity9 = new CONSTraumatisedToothDetails();
      ToothNumbers toothNumbersE;
      if (ViewModel.TtList != null)
      {
        foreach (CONSViewModel tt in ViewModel.TtList)
        {
          if (tt.TraumatisedToothId != 0)
          {
            if (tt.ToothNumber != null)
            {
              entity9.ToothNumber = tt.ToothNumber.ToString();
            }
            else
            {
              CONSTraumatisedToothDetails traumatisedToothDetails = entity9;
              toothNumbersE = tt.ToothNumbersE;
              string str = toothNumbersE.ToString();
              traumatisedToothDetails.ToothNumber = str;
            }
            entity9.TraumatisedToothId = tt.TraumatisedToothId;
            entity9.EllisType = tt.EllisType;
            entity9.SoftTissueInjuries = tt.SoftTissueInjuries;
            entity9.FacialSkeletalInjuries = tt.FacialSkeletalInjuries;
            entity9.LuxationInjuries = tt.LuxationInjuries;
            entity9.TtVitalityTesting = tt.TtVitalityTesting;
            entity9.TtRadiographicInterpretation = tt.TtRadiographicInterpretation;
            entity9.TtImpressionPhotographsCast = tt.TtImpressionPhotographsCast;
            entity9.TtRestoration = tt.TtRestoration;
            entity9.Splinting = tt.Splinting;
            entity9.CrownLengthening = tt.CrownLengthening;
            entity9.SurgicalManagement = tt.SurgicalManagement;
            entity9.OrthodonticIntrusion = tt.OrthodonticIntrusion;
            entity9.TtReview1 = tt.TtReview1;
            entity9.TtReview2 = tt.TtReview2;
            entity9.TtReview3 = tt.TtReview3;
            this._uow.Repository<CONSTraumatisedToothDetails>().Update(entity9, false);
          }
          else
          {
            entity9.TraumatisedToothId = 0;
            entity9.ConservativeId = ViewModel.ConservativeId;
            CONSTraumatisedToothDetails traumatisedToothDetails = entity9;
            toothNumbersE = tt.ToothNumbersE;
            string str = toothNumbersE.ToString();
            traumatisedToothDetails.ToothNumber = str;
            entity9.EllisType = tt.EllisType;
            entity9.SoftTissueInjuries = tt.SoftTissueInjuries;
            entity9.FacialSkeletalInjuries = tt.FacialSkeletalInjuries;
            entity9.LuxationInjuries = tt.LuxationInjuries;
            entity9.TtVitalityTesting = tt.TtVitalityTesting;
            entity9.TtRadiographicInterpretation = tt.TtRadiographicInterpretation;
            entity9.TtImpressionPhotographsCast = tt.TtImpressionPhotographsCast;
            entity9.TtRestoration = tt.TtRestoration;
            entity9.Splinting = tt.Splinting;
            entity9.CrownLengthening = tt.CrownLengthening;
            entity9.SurgicalManagement = tt.SurgicalManagement;
            entity9.OrthodonticIntrusion = tt.OrthodonticIntrusion;
            entity9.TtReview1 = tt.TtReview1;
            entity9.TtReview2 = tt.TtReview2;
            entity9.TtReview3 = tt.TtReview3;
            this._uow.Repository<CONSTraumatisedToothDetails>().Add(entity9, false);
          }
        }
      }
      CONSRootCanalDetails entity10 = new CONSRootCanalDetails();
      ToothNumbers toothNumbersF;
      if (ViewModel.RcList != null)
      {
        foreach (CONSViewModel rc in ViewModel.RcList)
        {
          if (rc.RootCanalId != 0)
          {
            if (rc.ToothNumber != null)
            {
              entity10.ToothNumber = rc.ToothNumber.ToString();
            }
            else
            {
              CONSRootCanalDetails rootCanalDetails = entity10;
              toothNumbersF = rc.ToothNumbersF;
              string str = toothNumbersF.ToString();
              rootCanalDetails.ToothNumber = str;
            }
            entity10.RootCanalId = rc.RootCanalId;
            entity10.RcRadiographicInterpretation = rc.RcRadiographicInterpretation;
            entity10.AccessOpeningCanal = rc.AccessOpeningCanal;
            entity10.NoOfCanal = rc.NoOfCanal;
            entity10.AdditionalCanals = rc.AdditionalCanals;
            entity10.WorkingLengthDetermination = rc.WorkingLengthDetermination;
            entity10.ShapingAndCleaning = rc.ShapingAndCleaning;
            entity10.RotaryInstrumentation = rc.RotaryInstrumentation;
            entity10.IrrigantUsed = rc.IrrigantUsed;
            entity10.IntracanalMedicament = rc.IntracanalMedicament;
            entity10.MasterConeSelection = rc.MasterConeSelection;
            entity10.ObturationTechnique = rc.ObturationTechnique;
            entity10.PostEndodonticRestoration = rc.PostEndodonticRestoration;
            entity10.ProstheticRehabilitation = rc.ProstheticRehabilitation;
            this._uow.Repository<CONSRootCanalDetails>().Update(entity10, false);
          }
          else
          {
            entity10.RootCanalId = 0;
            entity10.ConservativeId = ViewModel.ConservativeId;
            CONSRootCanalDetails rootCanalDetails = entity10;
            toothNumbersF = rc.ToothNumbersF;
            string str = toothNumbersF.ToString();
            rootCanalDetails.ToothNumber = str;
            entity10.RcRadiographicInterpretation = rc.RcRadiographicInterpretation;
            entity10.AccessOpeningCanal = rc.AccessOpeningCanal;
            entity10.NoOfCanal = rc.NoOfCanal;
            entity10.AdditionalCanals = rc.AdditionalCanals;
            entity10.WorkingLengthDetermination = rc.WorkingLengthDetermination;
            entity10.ShapingAndCleaning = rc.ShapingAndCleaning;
            entity10.RotaryInstrumentation = rc.RotaryInstrumentation;
            entity10.IrrigantUsed = rc.IrrigantUsed;
            entity10.IntracanalMedicament = rc.IntracanalMedicament;
            entity10.MasterConeSelection = rc.MasterConeSelection;
            entity10.ObturationTechnique = rc.ObturationTechnique;
            entity10.PostEndodonticRestoration = rc.PostEndodonticRestoration;
            entity10.ProstheticRehabilitation = rc.ProstheticRehabilitation;
            this._uow.Repository<CONSRootCanalDetails>().Add(entity10, false);
          }
        }
      }
      CONSReRctDetails entity11 = new CONSReRctDetails();
      ToothNumbers toothNumbersG;
      if (ViewModel.ReList != null)
      {
        foreach (CONSViewModel re in ViewModel.ReList)
        {
          if (re.ReRootCanalId != 0)
          {
            if (re.ToothNumber != null)
            {
              entity11.ToothNumber = re.ToothNumber.ToString();
            }
            else
            {
              CONSReRctDetails consReRctDetails = entity11;
              toothNumbersG = re.ToothNumbersG;
              string str = toothNumbersG.ToString();
              consReRctDetails.ToothNumber = str;
            }
            entity11.ReRootCanalId = re.ReRootCanalId;
            entity11.ReRadiographicInterpretation = re.ReRadiographicInterpretation;
            entity11.GpRemovalCanalLocation = re.GpRemovalCanalLocation;
            entity11.ReNoOfCanal = re.ReNoOfCanal;
            entity11.ReAdditionalCanals = re.ReAdditionalCanals;
            entity11.ReWorkingLengthDetermination = re.ReWorkingLengthDetermination;
            entity11.ReShapingAndCleaning = re.ReShapingAndCleaning;
            entity11.ReRotaryInstrumentation = re.ReRotaryInstrumentation;
            entity11.ReIrrigantUsed = re.ReIrrigantUsed;
            entity11.ReIntracanalMedicament = re.ReIntracanalMedicament;
            entity11.ReMasterConeSelection = re.ReMasterConeSelection;
            entity11.ReObturationTechnique = re.ReObturationTechnique;
            entity11.RePostEndodonticRestoration = re.RePostEndodonticRestoration;
            entity11.ReProstheticRehabilitation = re.ReProstheticRehabilitation;
            this._uow.Repository<CONSReRctDetails>().Update(entity11, false);
          }
          else
          {
            entity11.ReRootCanalId = 0;
            entity11.ConservativeId = ViewModel.ConservativeId;
            CONSReRctDetails consReRctDetails = entity11;
            toothNumbersG = re.ToothNumbersG;
            string str = toothNumbersG.ToString();
            consReRctDetails.ToothNumber = str;
            entity11.ReRadiographicInterpretation = re.ReRadiographicInterpretation;
            entity11.GpRemovalCanalLocation = re.GpRemovalCanalLocation;
            entity11.ReNoOfCanal = re.ReNoOfCanal;
            entity11.ReAdditionalCanals = re.ReAdditionalCanals;
            entity11.ReWorkingLengthDetermination = re.ReWorkingLengthDetermination;
            entity11.ReShapingAndCleaning = re.ReShapingAndCleaning;
            entity11.ReRotaryInstrumentation = re.ReRotaryInstrumentation;
            entity11.ReIrrigantUsed = re.ReIrrigantUsed;
            entity11.ReIntracanalMedicament = re.ReIntracanalMedicament;
            entity11.ReMasterConeSelection = re.ReMasterConeSelection;
            entity11.ReObturationTechnique = re.ReObturationTechnique;
            entity11.RePostEndodonticRestoration = re.RePostEndodonticRestoration;
            entity11.ReProstheticRehabilitation = re.ReProstheticRehabilitation;
            this._uow.Repository<CONSReRctDetails>().Add(entity11, false);
          }
        }
      }
      CONSIncompleteRootFormDetails entity12 = new CONSIncompleteRootFormDetails();
      ToothNumbers toothNumbersH;
      if (ViewModel.RfList != null)
      {
        foreach (CONSViewModel rf in ViewModel.RfList)
        {
          if (rf.IncompleteRootId != 0)
          {
            if (rf.ToothNumber != null)
            {
              entity12.ToothNumber = rf.ToothNumber.ToString();
            }
            else
            {
              CONSIncompleteRootFormDetails incompleteRootFormDetails = entity12;
              toothNumbersH = rf.ToothNumbersH;
              string str = toothNumbersH.ToString();
              incompleteRootFormDetails.ToothNumber = str;
            }
            entity12.IncompleteRootId = rf.IncompleteRootId;
            entity12.IncVitality = rf.IncVitality;
            entity12.IncPhotographCast = rf.IncPhotographCast;
            entity12.IncRadiographInterpretation = rf.IncRadiographInterpretation;
            entity12.IncTreatmentProcedure = rf.IncTreatmentProcedure;
            entity12.IncMaterialChoice = rf.IncMaterialChoice;
            entity12.IncSiteOfManagement = rf.IncSiteOfManagement;
            entity12.IncOtherRestorativePro = rf.IncOtherRestorativePro;
            entity12.IncReview1 = rf.IncReview1;
            entity12.IncReview2 = rf.IncReview2;
            entity12.IncReview3 = rf.IncReview3;
            this._uow.Repository<CONSIncompleteRootFormDetails>().Update(entity12, false);
          }
          else
          {
            entity12.IncompleteRootId = 0;
            entity12.ConservativeId = ViewModel.ConservativeId;
            CONSIncompleteRootFormDetails incompleteRootFormDetails = entity12;
            toothNumbersH = rf.ToothNumbersH;
            string str = toothNumbersH.ToString();
            incompleteRootFormDetails.ToothNumber = str;
            entity12.IncVitality = rf.IncVitality;
            entity12.IncPhotographCast = rf.IncPhotographCast;
            entity12.IncRadiographInterpretation = rf.IncRadiographInterpretation;
            entity12.IncTreatmentProcedure = rf.IncTreatmentProcedure;
            entity12.IncMaterialChoice = rf.IncMaterialChoice;
            entity12.IncSiteOfManagement = rf.IncSiteOfManagement;
            entity12.IncOtherRestorativePro = rf.IncOtherRestorativePro;
            entity12.IncReview1 = rf.IncReview1;
            entity12.IncReview2 = rf.IncReview2;
            entity12.IncReview3 = rf.IncReview3;
            this._uow.Repository<CONSIncompleteRootFormDetails>().Add(entity12, false);
          }
        }
      }
      return entity1.ConservativeId;
    }

    public StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      int DeptId = 4;
      string DeptCode = Department.CONS.ToString();
      return this._AllotmentService.DisplayAllotment(PatientId, ReferredId, CourseType, DeptId, DeptCode);
    }

    public void UpdateAllotment(CONSViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.ConservativeId,
        ApprovalType = model.ApprovalType,
        ReferredTreatmentId = 0
      });
    }

    public void SaveAllotment(CONSViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.ConservativeId,
        ReferredTreatmentId = 0
      });
    }

    public void SavefollowUp(CONSViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 4,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 0
      });
    }

    public void UpdateReferralStatus(CONSViewModel model)
    {
      int patientId = model.PatientId;
      int num = 4;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 0
      });
    }

    public List<CONSSearchDetails> consSearchDetails(string From_Date, string To_Date, string url)
    {
      string str = "(OP.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      return this._uow.Repository<CONSSearchDetails>().GetEntitiesBySql(string.Format(Queries.CONSSearch, (object) From_Date, (object) To_Date, (object) 4, (object) url, (object) str)).ToList<CONSSearchDetails>();
    }

    public CONSViewModel BindCONSPatientReport(int CONSId)
    {
      try
      {
        CONSViewModel consViewModel = this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format("exec GetCONSCasesheetReport {0}", (object) CONSId)).FirstOrDefault<CONSViewModel>();
        PatientInformationViewModel informationViewModel = new PatientInformationViewModel();
        informationViewModel.OpNo = consViewModel.OpNo;
        informationViewModel.PatientName = consViewModel.PatientName;
        informationViewModel.AgeGender = consViewModel.Age.ToString() + "/" + (object) (Gender) consViewModel.GenderId.Value;
        informationViewModel.Area = consViewModel.Area;
        informationViewModel.Phone = consViewModel.Phone;
        consViewModel.patientInformationViewModel = informationViewModel;
        consViewModel.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) consViewModel.PatientId, (object) 4));
        consViewModel.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) consViewModel.PatientId, (object) 4, (object) CONSId, (object) 0, (object) 20, (object) 16));
        consViewModel.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) consViewModel.PatientId, (object) 4, (object) CONSId, (object) 0));
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = consViewModel.PatientId,
          FromdeptId = 4
        };
        consViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(consViewModel.PatientId).ToList<ReferralStatusViewModel>();
        consViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = consViewModel.PatientId,
          DeptId = 4,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        consViewModel.followupList = (IEnumerable<FollowupViewModal>) this._uow.Repository<FollowupViewModal>().GetEntitiesBySql(string.Format(Queries.FollowUpDetailsList, (object) consViewModel.PatientId, (object) 4, (object) 0)).ToList<FollowupViewModal>();
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        consViewModel.studentAllotmentViewModel = allotmentViewModel;
        consViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 4, 0);
        return consViewModel;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public CONSViewModel BindTreatmentList(long allotId)
    {
      CONSViewModel consViewModel = new CONSViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = " DeptId=" + (object) 4;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        consViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        consViewModel.patientInformationViewModel = informationViewModel;
        consViewModel.studentAllotmentViewModel = allotmentViewModel;
        consViewModel.PatientId = informationViewModel.PatientId;
        consViewModel.Treatmentlist = (IEnumerable<CONSViewModel>) this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format(Queries.CONSPatientTreatmentList, (object) informationViewModel.PatientId)).ToList<CONSViewModel>();
      }
      return consViewModel;
    }

    public List<StudentAllotmentViewModel> ConsCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.CONSApproval, (object) From_Date, (object) To_Date, (object) 4, (object) url)).ToList<StudentAllotmentViewModel>();
    }

    public void ProcedureApproval(CONSViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
    }

    public void SendApproval(long AllotId)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "N",
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        AllotId = AllotId
      });
    }
  }
}
