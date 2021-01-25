// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.PERIOCasesheetService
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
  public class PERIOCasesheetService : ServiceBase<PERIOCasesheet>, IPERIOCasesheetService, IService<PERIOCasesheet>
  {
    private IUnitOfWork _uow;
    private IOPDPatientRegistrationService _OPDservice;
    private IMASCodeService _Dropdownservice;
    private IMASChiefComplaintService _ChiefComplaintservice;
    private IReferralStatusService _Referralservice;
    private IBillQueueService _BillQueueservice;
    private IMASPresentIllnessService _PresentIllnessService;
    private IStudentAllotmentService _AllotmentService;
    private IFollowUpService _FollowUpService;
    private ICasesheetNoService _CasesheetNoService;
    private IPrescriptionsService _PrescriptionsService;

    public PERIOCasesheetService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._OPDservice = (IOPDPatientRegistrationService) new OPDPatientRegistrationService(this._uow);
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._ChiefComplaintservice = (IMASChiefComplaintService) new MASChiefComplaintService(this._uow);
      this._Referralservice = (IReferralStatusService) new ReferralStatusService(this._uow);
      this._BillQueueservice = (IBillQueueService) new BillQueueService(this._uow);
      this._PresentIllnessService = (IMASPresentIllnessService) new MASPresentIllnessService(this._uow);
      this._AllotmentService = (IStudentAllotmentService) new StudentAllotmentService(this._uow);
      this._FollowUpService = (IFollowUpService) new FollowUpService(this._uow);
      this._CasesheetNoService = (ICasesheetNoService) new CasesheetNoService(this._uow);
      this._PrescriptionsService = (IPrescriptionsService) new PrescriptionsService(this._uow);
    }

    public IEnumerable<PERIOCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<PERIOCasesheetProperties>().GetAll();
    }

    public PERIOViewModel GetPERIOPatientDetails(int id)
    {
      return this._uow.Repository<PERIOViewModel>().GetEntitiesBySql(string.Format("exec GetPERIOCasesheet {0}", (object) id)).FirstOrDefault<PERIOViewModel>();
    }

    public PERIOViewModel BindPerioPatientModel(long allotId)
    {
      PERIOViewModel perioViewModel = new PERIOViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 3;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 3);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        perioViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        perioViewModel.patientInformationViewModel = informationViewModel;
        perioViewModel.studentAllotmentViewModel = allotmentViewModel;
        perioViewModel.PatientId = informationViewModel.PatientId;
        perioViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        perioViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 3, 0);
        perioViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 3);
        perioViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(3);
        perioViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 3, 0);
        perioViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 3);
        perioViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 3, 0);
        perioViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 3,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        perioViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 3, 0);
        perioViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 3).ToList<ReferralStatusViewModel>();
        perioViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        perioViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        perioViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        perioViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        perioViewModel.ParafunctionalHabitslist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 4)).ToList<MASCode>();
        perioViewModel.BrushingHabitsMethodlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 5)).ToList<MASCode>();
        perioViewModel.BrushingHabitsFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 6)).ToList<MASCode>();
        perioViewModel.BrushingHabitsDurlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 7)).ToList<MASCode>();
        perioViewModel.ChangingBrushFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 8)).ToList<MASCode>();
        perioViewModel.BrushTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 19)).ToList<MASCode>();
        perioViewModel.DentifriceTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 18)).ToList<MASCode>();
        perioViewModel.BrushTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 19)).ToList<MASCode>();
        perioViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) perioViewModel.PatientId))
        };
        perioViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) perioViewModel.PatientId))
        };
        IEnumerable<PERIOCasesheetProperties> properties = this.GetProperties();
        perioViewModel.Proplist = properties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        perioViewModel.medicalalertviewmodel = medicalAlertViewModel2;
      }
      return perioViewModel;
    }

    public int SavePERIOPatient(PERIOViewModel model)
    {
      model.MandatoryDummy = "Y";
      PERIOCasesheet perioCasesheet = new PERIOCasesheet();
      PERIOCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PERIOViewModel, PERIOCasesheet>())).CreateMapper().Map<PERIOViewModel, PERIOCasesheet>(model);
      entity1.PerioDate = DateTime.Now;
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.DietId = (int) model.Diet;
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.PerioNo = this._CasesheetNoService.GetPerioNo();
      int num = this._uow.Repository<PERIOCasesheet>().Add(entity1, false);
      IEnumerable<PERIOCasesheetProperties> all = this._uow.Repository<PERIOCasesheetProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<PERIOCasesheetProperties>((Func<PERIOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            PERIOCasesheetPropertyValues entity2 = new PERIOCasesheetPropertyValues();
            entity2.PerioId = num;
            PERIOCasesheetProperties casesheetProperties = all.FirstOrDefault<PERIOCasesheetProperties>((Func<PERIOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
            {
              entity2.PropId = casesheetProperties.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<PERIOCasesheetPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      model.PerioId = num;
      if (model.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = model.PatientId;
            entity2.FromdeptId = 3;
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
          DeptId = 3,
          PatientId = model.PatientId
        });
      if (model.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 0,
          DeptId = 3,
          PatientId = model.PatientId
        });
      if (model.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = model.PatientId,
          AllotId = model.studentAllotmentViewModel.AllotId,
          DoctorId = model.studentAllotmentViewModel.DoctorId,
          StudentId = model.studentAllotmentViewModel.StudentId,
          DeptId = 3,
          ReferredTreatmentId = 0
        });
      return num;
    }

    public PERIOViewModel BindEditPERIOPatientModel(long allotId, int PerioId)
    {
      try
      {
        PERIOViewModel perioPatientDetails = this.GetPERIOPatientDetails(PerioId);
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 3;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 3);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        allotmentViewModel.DoctorId = informationViewModel.DoctorId;
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = informationViewModel.PatientId,
          FromdeptId = 3
        };
        perioPatientDetails.patientInformationViewModel = informationViewModel;
        perioPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 3).ToList<ReferralStatusViewModel>();
        perioPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        perioPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 3).ToList<ReferralStatusViewModel>();
        perioPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 3, 0);
        perioPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        perioPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 3);
        perioPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 3, 0);
        perioPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 3);
        perioPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(3);
        perioPatientDetails.studentAllotmentViewModel = allotmentViewModel;
        perioPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 3, 0);
        perioPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        perioPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        perioPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        perioPatientDetails.Diet = (Diet) perioPatientDetails.DietId;
        perioPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
        perioPatientDetails.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 3,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        perioPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 3, 0);
        perioPatientDetails.ParafunctionalHabitslist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 4)).ToList<MASCode>();
        perioPatientDetails.BrushingHabitsMethodlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 5)).ToList<MASCode>();
        perioPatientDetails.BrushingHabitsFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 6)).ToList<MASCode>();
        perioPatientDetails.BrushingHabitsDurlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 7)).ToList<MASCode>();
        perioPatientDetails.ChangingBrushFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 8)).ToList<MASCode>();
        perioPatientDetails.BrushTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 19)).ToList<MASCode>();
        perioPatientDetails.DentifriceTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 18)).ToList<MASCode>();
        perioPatientDetails.BrushTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 19)).ToList<MASCode>();
        IEnumerable<PERIOCasesheetProperties> properties = this.GetProperties();
        perioPatientDetails.Proplist = properties;
        perioPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) perioPatientDetails.PatientId))
        };
        perioPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) perioPatientDetails.PatientId))
        };
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        perioPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
        perioPatientDetails.approvalViewModal = new ApprovalViewModal()
        {
          ApprovalTypeId = 1,
          CaserecordId = PerioId,
          DeptId = 3,
          DoctorId = allotmentViewModel.DoctorId,
          PatientId = (long) perioPatientDetails.PatientId,
          ReferredTreatmentId = 0
        };
        if (perioPatientDetails.Approval1)
        {
          perioPatientDetails.DisplayApproval1 = "Approved";
          perioPatientDetails.ReadOnlyApproval1 = true;
        }
        else
        {
          perioPatientDetails.DisplayApproval1 = "Not Approved";
          perioPatientDetails.ReadOnlyApproval1 = false;
        }
        if (perioPatientDetails.Approval2)
        {
          perioPatientDetails.DisplayApproval2 = "Approved";
          perioPatientDetails.ReadOnlyApproval2 = true;
          perioPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
        }
        else
        {
          perioPatientDetails.DisplayApproval2 = "Not Approved";
          perioPatientDetails.ReadOnlyApproval2 = false;
          perioPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
        }
        if (perioPatientDetails.Approval3)
        {
          perioPatientDetails.DisplayApproval3 = "Casesheet Closed";
          perioPatientDetails.ReadOnlyApproval3 = true;
          perioPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
          perioPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
        }
        else
        {
          perioPatientDetails.DisplayApproval3 = "Not Approved";
          perioPatientDetails.ReadOnlyApproval3 = false;
          perioPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
          perioPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
        }
        PERIOViewModel perioViewModel = this._uow.Repository<PERIOViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayPerioApprovalStage, (object) 3, (object) perioPatientDetails.PerioId)).FirstOrDefault<PERIOViewModel>();
        perioPatientDetails.ApprovalStage = perioViewModel == null ? "Not Initiated" : (!(perioViewModel.ApprovalStage == "") ? perioViewModel.ApprovalStage : "Not Initiated");
        perioPatientDetails.ReadOnlyApproval4 = true;
        return perioPatientDetails;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdatePERIOPatient(PERIOViewModel ViewModel)
    {
      ViewModel.PerioDate = Convert.ToDateTime(ViewModel.PerioDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      ViewModel.PerioId = ViewModel.PerioId;
      PERIOCasesheet perioCasesheet = new PERIOCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PERIOViewModel, PERIOCasesheet>()));
      PERIOCasesheet entity1 = Mapper.Map<PERIOViewModel, PERIOCasesheet>(ViewModel);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      this._uow.Repository<PERIOCasesheet>().Update(entity1, false);
      IEnumerable<PERIOCasesheetProperties> all1 = this._uow.Repository<PERIOCasesheetProperties>().GetAll();
      IEnumerable<PERIOCasesheetPropertyValues> all2 = this._uow.Repository<PERIOCasesheetPropertyValues>().GetAll("PerioId=" + (object) entity1.PerioId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<PERIOCasesheetProperties>((Func<PERIOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            PERIOCasesheetPropertyValues entity2 = new PERIOCasesheetPropertyValues();
            entity2.PerioId = ViewModel.PerioId;
            PERIOCasesheetProperties property = all1.FirstOrDefault<PERIOCasesheetProperties>((Func<PERIOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            PERIOCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<PERIOCasesheetPropertyValues>((Func<PERIOCasesheetPropertyValues, bool>) (a =>
            {
              if (a.PropId == property.PropertyId)
                return a.PerioId == ViewModel.PerioId;
              return false;
            }));
            if (casesheetPropertyValues != null)
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              entity2.ValueId = casesheetPropertyValues.ValueId;
              this._uow.Repository<PERIOCasesheetPropertyValues>().Update(entity2, false);
            }
            else
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<PERIOCasesheetPropertyValues>().Add(entity2, false);
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
            entity3.FromdeptId = 3;
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
          CaserecordId = ViewModel.PerioId,
          ReferredTreatmentId = 0,
          DeptId = 3,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.PerioId,
          ReferredTreatmentId = 0,
          DeptId = 3,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 3,
          ReferredTreatmentId = 0
        });
      return entity1.PerioId;
    }

    public StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      int DeptId = 3;
      string DeptCode = Department.PERIO.ToString();
      return this._AllotmentService.DisplayAllotment(PatientId, ReferredId, CourseType, DeptId, DeptCode);
    }

    public void UpdateAllotment(PERIOViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.PerioId,
        ReferredTreatmentId = 0
      });
    }

    public void SavefollowUp(PERIOViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 3,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 0
      });
    }

    public void UpdateReferralStatus(PERIOViewModel model)
    {
      int patientId = model.PatientId;
      int num = 3;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 0
      });
    }

    public List<PERIOSearchDetails> perioSearchDetails(string From_Date, string To_Date, string url)
    {
      string str = "(OP.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      return this._uow.Repository<PERIOSearchDetails>().GetEntitiesBySql(string.Format(Queries.PERIOSearch, (object) From_Date, (object) To_Date, (object) 3, (object) url, (object) str)).ToList<PERIOSearchDetails>();
    }

    public List<StudentAllotmentViewModel> perioCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.PERIOApproval, (object) From_Date, (object) To_Date, (object) 3, (object) url)).ToList<StudentAllotmentViewModel>();
    }

    public PERIOViewModel BindPERIOPatientReport(int PerioId)
    {
      try
      {
        PERIOViewModel perioViewModel1 = new PERIOViewModel();
        PERIOViewModel perioViewModel2 = this._uow.Repository<PERIOViewModel>().GetEntitiesBySql(string.Format("exec GetPERIOCasesheetReport {0}", (object) PerioId)).FirstOrDefault<PERIOViewModel>();
        perioViewModel2.patientInformationViewModel = new PatientInformationViewModel()
        {
          PatientName = perioViewModel2.PatientName,
          AgeGender = perioViewModel2.Age.ToString() + "/" + (object) (Gender) perioViewModel2.GenderId,
          Phone = perioViewModel2.Phone,
          OpNo = perioViewModel2.OpNo,
          Area = perioViewModel2.Area
        };
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = perioViewModel2.PatientId,
          FromdeptId = 3
        };
        perioViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(perioViewModel2.PatientId).ToList<ReferralStatusViewModel>();
        perioViewModel2.followupViewModal = new FollowupViewModal()
        {
          PatientId = perioViewModel2.PatientId,
          DeptId = 3,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        perioViewModel2.followupList = this._FollowUpService.LoadFollowupList(perioViewModel2.PatientId, 3, 0);
        perioViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) perioViewModel2.PatientId, (object) 3));
        perioViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) perioViewModel2.PatientId, (object) 3, (object) PerioId, (object) 0, (object) 20, (object) 16));
        perioViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) perioViewModel2.PatientId, (object) 3, (object) PerioId, (object) 0));
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        perioViewModel2.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(perioViewModel2.PatientId, 3, 0);
        return perioViewModel2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public PERIOViewModel BindTreatmentList(long allotId)
    {
      PERIOViewModel perioViewModel = new PERIOViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 3;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        perioViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        perioViewModel.patientInformationViewModel = informationViewModel;
        perioViewModel.studentAllotmentViewModel = allotmentViewModel;
        perioViewModel.PatientId = informationViewModel.PatientId;
        perioViewModel.Treatmentlist = (IEnumerable<PERIOViewModel>) this._uow.Repository<PERIOViewModel>().GetEntitiesBySql(string.Format(Queries.PERIOPatientTreatmentList, (object) informationViewModel.PatientId)).ToList<PERIOViewModel>();
      }
      return perioViewModel;
    }

    public void ProcedureApproval(PERIOViewModel model)
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
