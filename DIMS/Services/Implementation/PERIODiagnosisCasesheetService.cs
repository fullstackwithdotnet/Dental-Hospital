// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.PERIODiagnosisCasesheetService
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
  public class PERIODiagnosisCasesheetService : ServiceBase<PERIODiagnosisCasesheet>, IPERIODiagnosisCasesheetService, IService<PERIODiagnosisCasesheet>
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

    public PERIODiagnosisCasesheetService(IUnitOfWork uow)
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

    public IEnumerable<PERIODiagnosisCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<PERIODiagnosisCasesheetProperties>().GetAll();
    }

    public PERIODiagnosisViewModel GetPERIOPatientDetails(int id)
    {
      return this._uow.Repository<PERIODiagnosisViewModel>().GetEntitiesBySql(string.Format("exec GetPERIODiagnosisCasesheet {0}", (object) id)).FirstOrDefault<PERIODiagnosisViewModel>();
    }

    public PERIODiagnosisViewModel BindPerioPatientModel(long allotId)
    {
      PERIODiagnosisViewModel PERIODiagnosisViewModel = new PERIODiagnosisViewModel();
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
        PERIODiagnosisViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        PERIODiagnosisViewModel.patientInformationViewModel = informationViewModel;
        PERIODiagnosisViewModel.studentAllotmentViewModel = allotmentViewModel;
        PERIODiagnosisViewModel.PatientId = informationViewModel.PatientId;
        PERIODiagnosisViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        PERIODiagnosisViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 3, 0);
        PERIODiagnosisViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 3);
        PERIODiagnosisViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(3);
        PERIODiagnosisViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 3, 0);
        PERIODiagnosisViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 3);
        PERIODiagnosisViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 3, 0);
        PERIODiagnosisViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 3,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        PERIODiagnosisViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 3, 0);
        PERIODiagnosisViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 3).ToList<ReferralStatusViewModel>();
        PERIODiagnosisViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        PERIODiagnosisViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        PERIODiagnosisViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        PERIODiagnosisViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        PERIODiagnosisViewModel.ParafunctionalHabitslist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 4)).ToList<MASCode>();
        PERIODiagnosisViewModel.BrushingHabitsMethodlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 5)).ToList<MASCode>();
        PERIODiagnosisViewModel.BrushingHabitsFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 6)).ToList<MASCode>();
        PERIODiagnosisViewModel.BrushingHabitsDurlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 7)).ToList<MASCode>();
        PERIODiagnosisViewModel.ChangingBrushFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 8)).ToList<MASCode>();
        PERIODiagnosisViewModel.BrushTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 19)).ToList<MASCode>();
        PERIODiagnosisViewModel.DentifriceTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 18)).ToList<MASCode>();
        PERIODiagnosisViewModel.BrushTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 19)).ToList<MASCode>();
        PERIODiagnosisViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) PERIODiagnosisViewModel.PatientId))
        };
        PERIODiagnosisViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) PERIODiagnosisViewModel.PatientId))
        };
        IEnumerable<PERIODiagnosisCasesheetProperties> properties = this.GetProperties();
        PERIODiagnosisViewModel.Proplist = properties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        PERIODiagnosisViewModel.medicalalertviewmodel = medicalAlertViewModel2;
      }
      return PERIODiagnosisViewModel;
    }

    public int SavePERIOPatient(PERIODiagnosisViewModel model)
    {
      model.MandatoryDummy = "Y";
      PERIODiagnosisCasesheet PERIODiagnosisCasesheet = new PERIODiagnosisCasesheet();
      PERIODiagnosisCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PERIODiagnosisViewModel, PERIODiagnosisCasesheet>())).CreateMapper().Map<PERIODiagnosisViewModel, PERIODiagnosisCasesheet>(model);
      entity1.PerioDate = DateTime.Now;
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.DietId = (int) model.Diet;
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.PerioNo = this._CasesheetNoService.GetPerioNo();
      int num = this._uow.Repository<PERIODiagnosisCasesheet>().Add(entity1, false);
      IEnumerable<PERIODiagnosisCasesheetProperties> all = this._uow.Repository<PERIODiagnosisCasesheetProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<PERIODiagnosisCasesheetProperties>((Func<PERIODiagnosisCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            PERIODiagnosisCasesheetPropertyValues entity2 = new PERIODiagnosisCasesheetPropertyValues();
            entity2.PerioId = num;
            PERIODiagnosisCasesheetProperties casesheetProperties = all.FirstOrDefault<PERIODiagnosisCasesheetProperties>((Func<PERIODiagnosisCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
            {
              entity2.PropId = casesheetProperties.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<PERIODiagnosisCasesheetPropertyValues>().Add(entity2, false);
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

    public PERIODiagnosisViewModel BindEditPERIOPatientModel(long allotId, int PerioId)
    {
      try
      {
        PERIODiagnosisViewModel perioPatientDetails = this.GetPERIOPatientDetails(PerioId);
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
        IEnumerable<PERIODiagnosisCasesheetProperties> properties = this.GetProperties();
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
        PERIODiagnosisViewModel PERIODiagnosisViewModel = this._uow.Repository<PERIODiagnosisViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayPerioApprovalStage, (object) 3, (object) perioPatientDetails.PerioId)).FirstOrDefault<PERIODiagnosisViewModel>();
        perioPatientDetails.ApprovalStage = PERIODiagnosisViewModel == null ? "Not Initiated" : (!(PERIODiagnosisViewModel.ApprovalStage == "") ? PERIODiagnosisViewModel.ApprovalStage : "Not Initiated");
        perioPatientDetails.ReadOnlyApproval4 = true;
        return perioPatientDetails;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdatePERIOPatient(PERIODiagnosisViewModel ViewModel)
    {
      ViewModel.PerioDate = Convert.ToDateTime(ViewModel.PerioDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      ViewModel.PerioId = ViewModel.PerioId;
      PERIODiagnosisCasesheet PERIODiagnosisCasesheet = new PERIODiagnosisCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PERIODiagnosisViewModel, PERIODiagnosisCasesheet>()));
      PERIODiagnosisCasesheet entity1 = Mapper.Map<PERIODiagnosisViewModel, PERIODiagnosisCasesheet>(ViewModel);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      this._uow.Repository<PERIODiagnosisCasesheet>().Update(entity1, false);
      IEnumerable<PERIODiagnosisCasesheetProperties> all1 = this._uow.Repository<PERIODiagnosisCasesheetProperties>().GetAll();
      IEnumerable<PERIODiagnosisCasesheetPropertyValues> all2 = this._uow.Repository<PERIODiagnosisCasesheetPropertyValues>().GetAll("PerioId=" + (object) entity1.PerioId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<PERIODiagnosisCasesheetProperties>((Func<PERIODiagnosisCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            PERIODiagnosisCasesheetPropertyValues entity2 = new PERIODiagnosisCasesheetPropertyValues();
            entity2.PerioId = ViewModel.PerioId;
            PERIODiagnosisCasesheetProperties property = all1.FirstOrDefault<PERIODiagnosisCasesheetProperties>((Func<PERIODiagnosisCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            PERIODiagnosisCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<PERIODiagnosisCasesheetPropertyValues>((Func<PERIODiagnosisCasesheetPropertyValues, bool>) (a =>
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
              this._uow.Repository<PERIODiagnosisCasesheetPropertyValues>().Update(entity2, false);
            }
            else
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<PERIODiagnosisCasesheetPropertyValues>().Add(entity2, false);
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

    public void UpdateAllotment(PERIODiagnosisViewModel model)
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

    public void SavefollowUp(PERIODiagnosisViewModel model)
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

    public void UpdateReferralStatus(PERIODiagnosisViewModel model)
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

    public List<StudentAllotmentViewModel> perioDiagnosisCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.PERIOApproval, (object) From_Date, (object) To_Date, (object) 3, (object) url)).ToList<StudentAllotmentViewModel>();
    }

    public PERIODiagnosisViewModel BindPERIOPatientReport(int PerioId)
    {
      try
      {
        PERIODiagnosisViewModel PERIODiagnosisViewModel1 = new PERIODiagnosisViewModel();
        PERIODiagnosisViewModel PERIODiagnosisViewModel2 = this._uow.Repository<PERIODiagnosisViewModel>().GetEntitiesBySql(string.Format("exec GetPERIODiagnosisCasesheetReport {0}", (object) PerioId)).FirstOrDefault<PERIODiagnosisViewModel>();
        PERIODiagnosisViewModel2.patientInformationViewModel = new PatientInformationViewModel()
        {
          PatientName = PERIODiagnosisViewModel2.PatientName,
          AgeGender = PERIODiagnosisViewModel2.Age.ToString() + "/" + (object) (Gender) PERIODiagnosisViewModel2.GenderId,
          Phone = PERIODiagnosisViewModel2.Phone,
          OpNo = PERIODiagnosisViewModel2.OpNo,
          Area = PERIODiagnosisViewModel2.Area
        };
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = PERIODiagnosisViewModel2.PatientId,
          FromdeptId = 3
        };
        PERIODiagnosisViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(PERIODiagnosisViewModel2.PatientId).ToList<ReferralStatusViewModel>();
        PERIODiagnosisViewModel2.followupViewModal = new FollowupViewModal()
        {
          PatientId = PERIODiagnosisViewModel2.PatientId,
          DeptId = 3,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        PERIODiagnosisViewModel2.followupList = this._FollowUpService.LoadFollowupList(PERIODiagnosisViewModel2.PatientId, 3, 0);
        PERIODiagnosisViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) PERIODiagnosisViewModel2.PatientId, (object) 3));
        PERIODiagnosisViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) PERIODiagnosisViewModel2.PatientId, (object) 3, (object) PerioId, (object) 0, (object) 20, (object) 16));
        PERIODiagnosisViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) PERIODiagnosisViewModel2.PatientId, (object) 3, (object) PerioId, (object) 0));
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        PERIODiagnosisViewModel2.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(PERIODiagnosisViewModel2.PatientId, 3, 0);
        return PERIODiagnosisViewModel2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public PERIODiagnosisViewModel BindTreatmentList(long allotId)
    {
      PERIODiagnosisViewModel PERIODiagnosisViewModel = new PERIODiagnosisViewModel();
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
        PERIODiagnosisViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        PERIODiagnosisViewModel.patientInformationViewModel = informationViewModel;
        PERIODiagnosisViewModel.studentAllotmentViewModel = allotmentViewModel;
        PERIODiagnosisViewModel.PatientId = informationViewModel.PatientId;
        PERIODiagnosisViewModel.Treatmentlist = (IEnumerable<PERIODiagnosisViewModel>) this._uow.Repository<PERIODiagnosisViewModel>().GetEntitiesBySql(string.Format(Queries.PERIOPatientTreatmentList, (object) informationViewModel.PatientId)).ToList<PERIODiagnosisViewModel>();
      }
      return PERIODiagnosisViewModel;
    }

    public void ProcedureApproval(PERIODiagnosisViewModel model)
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
