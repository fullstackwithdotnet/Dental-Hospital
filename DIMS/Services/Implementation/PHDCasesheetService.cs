// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.PHDCasesheetService
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
  public class PHDCasesheetService : ServiceBase<PHDCasesheet>, IPHDCasesheetService, IService<PHDCasesheet>
  {
    private IUnitOfWork _uow;
    private IOPDPatientRegistrationService _OPDservice;
    private IMASCodeService _Dropdownservice;
    private IMASChiefComplaintService _ChiefComplaintservice;
    private IMASPresentIllnessService _PresentIllnessService;
    private IReferralStatusService _Referralservice;
    private IBillQueueService _BillQueueservice;
    private IStudentAllotmentService _AllotmentService;
    private IFollowUpService _FollowUpService;
    private ICasesheetNoService _CasesheetNoService;
    private IPrescriptionsService _PrescriptionsService;

    public PHDCasesheetService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._OPDservice = (IOPDPatientRegistrationService) new OPDPatientRegistrationService(this._uow);
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._ChiefComplaintservice = (IMASChiefComplaintService) new MASChiefComplaintService(this._uow);
      this._PresentIllnessService = (IMASPresentIllnessService) new MASPresentIllnessService(this._uow);
      this._Referralservice = (IReferralStatusService) new ReferralStatusService(this._uow);
      this._BillQueueservice = (IBillQueueService) new BillQueueService(this._uow);
      this._AllotmentService = (IStudentAllotmentService) new StudentAllotmentService(this._uow);
      this._FollowUpService = (IFollowUpService) new FollowUpService(this._uow);
      this._CasesheetNoService = (ICasesheetNoService) new CasesheetNoService(this._uow);
      this._PrescriptionsService = (IPrescriptionsService) new PrescriptionsService(this._uow);
    }

    public IEnumerable<PHDCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<PHDCasesheetProperties>().GetAll();
    }

    public IEnumerable<OMRCasesheetProperties> GetOMRProperties()
    {
      return this._uow.Repository<OMRCasesheetProperties>().GetAll();
    }

    public PHDViewModel GetOMRPatientDetails(int Id)
    {
      return this._uow.Repository<PHDViewModel>().GetEntitiesBySql(string.Format("exec GetOMRCasesheet {0}", (object) Id)).FirstOrDefault<PHDViewModel>();
    }

    public PHDViewModel GetPHDPatientDetails(int Id)
    {
      return this._uow.Repository<PHDViewModel>().GetEntitiesBySql(string.Format("exec GetPHDCasesheet {0}", (object) Id)).FirstOrDefault<PHDViewModel>();
    }

    public OMRPHDCasesheetViewModel GetPHDOMRPatientDetails(int Id)
    {
      return this._uow.Repository<OMRPHDCasesheetViewModel>().GetEntitiesBySql(string.Format("exec GetOMRCasesheet {0}", (object) Id)).FirstOrDefault<OMRPHDCasesheetViewModel>();
    }

    public PHDViewModel BindPHDPatientModel(long allotId)
    {
      PHDViewModel phdViewModel = new PHDViewModel();
      IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        string empty = string.Empty;
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(empty);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 8);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        phdViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        phdViewModel.patientInformationViewModel = informationViewModel;
        phdViewModel.studentAllotmentViewModel = allotmentViewModel;
        phdViewModel.PatientId = informationViewModel.PatientId;
        phdViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 3).ToList<ReferralStatusViewModel>();
        phdViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        phdViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        phdViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        phdViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        phdViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 8,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        phdViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 8, 0);
        phdViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(8);
        phdViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        phdViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 8, 0);
        phdViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 8);
        phdViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 8, 0);
        phdViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 8);
        phdViewModel.ParafunctionalHabitslist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 4)).ToList<MASCode>();
        phdViewModel.BrushingHabitsMethodlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 5)).ToList<MASCode>();
        phdViewModel.BrushingHabitsFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 6)).ToList<MASCode>();
        phdViewModel.BrushingHabitsDurlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 7)).ToList<MASCode>();
        phdViewModel.ChangingBrushFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 8)).ToList<MASCode>();
        phdViewModel.BrushTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 19)).ToList<MASCode>();
        phdViewModel.DentifriceTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 18)).ToList<MASCode>();
        phdViewModel.Consistencylist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 9)).ToList<MASCode>();
        phdViewModel.Grouplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 10)).ToList<MASCode>();
        phdViewModel.OverBitelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 11)).ToList<MASCode>();
        phdViewModel.OverJetlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 12)).ToList<MASCode>();
        phdViewModel.MoralRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 13)).ToList<MASCode>();
        phdViewModel.CanineRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 14)).ToList<MASCode>();
        phdViewModel.SkeletaRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 15)).ToList<MASCode>();
        phdViewModel.OcculsionMolarRelationlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 16)).ToList<MASCode>();
        phdViewModel.OcculsionCanineRelationlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 17)).ToList<MASCode>();
        phdViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) phdViewModel.PatientId))
        };
        phdViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) phdViewModel.PatientId))
        };
        IEnumerable<PHDCasesheetProperties> properties = this.GetProperties();
        phdViewModel.Proplist = properties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        phdViewModel.medicalalertviewmodel = medicalAlertViewModel2;
        phdViewModel.LiquidsFrequency = "X5";
        phdViewModel.SolidAndStickyFrequency = "X10";
        phdViewModel.SlowlyDissolvingFrequency = "X15";
      }
      return phdViewModel;
    }

    public int SavePHDPatient(PHDViewModel model)
    {
      model.MandatoryDummy = "Y";
      OMRCasesheet omrCasesheet = new OMRCasesheet();
      if (this._uow.Repository<OMRCasesheet>().GetAll("PatientId=" + (object) model.PatientId).OrderByDescending<OMRCasesheet, int>((Func<OMRCasesheet, int>) (x => x.OMRId)).FirstOrDefault<OMRCasesheet>() == null)
        model.OMRId = this.SaveOMRPHDDetails(model);
      PHDCasesheet phdCasesheet = new PHDCasesheet();
      PHDCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PHDViewModel, PHDCasesheet>())).CreateMapper().Map<PHDViewModel, PHDCasesheet>(model);
      entity1.PHDDate = DateTime.Now;
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.PHDNo = this._CasesheetNoService.GetPHDNo();
      int num = this._uow.Repository<PHDCasesheet>().Add(entity1, false);
      IEnumerable<PHDCasesheetProperties> all = this._uow.Repository<PHDCasesheetProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<PHDCasesheetProperties>((Func<PHDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            PHDCasesheetPropertyValues entity2 = new PHDCasesheetPropertyValues();
            entity2.PHDId = num;
            PHDCasesheetProperties casesheetProperties = all.FirstOrDefault<PHDCasesheetProperties>((Func<PHDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
            {
              entity2.PropId = casesheetProperties.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<PHDCasesheetPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      model.PHDId = num;
      if (model.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = model.PatientId;
            entity2.FromdeptId = 8;
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
          DeptId = 8,
          PatientId = model.PatientId
        });
      if (model.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 0,
          DeptId = 8,
          PatientId = model.PatientId
        });
      if (model.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = model.PatientId,
          AllotId = model.studentAllotmentViewModel.AllotId,
          DoctorId = model.studentAllotmentViewModel.DoctorId,
          StudentId = model.studentAllotmentViewModel.StudentId,
          DeptId = 8,
          ReferredTreatmentId = 0
        });
      return num;
    }

    public int SaveOMRPHDDetails(PHDViewModel model)
    {
      model.MandatoryDummy = "Y";
      OMRCasesheet omrCasesheet = new OMRCasesheet();
      OMRCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PHDViewModel, OMRCasesheet>())).CreateMapper().Map<PHDViewModel, OMRCasesheet>(model);
      entity1.OMRDate = DateTime.Now;
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.OMRNo = this._CasesheetNoService.GetOMRNo();
      int num = this._uow.Repository<OMRCasesheet>().Add(entity1, false);
      IEnumerable<OMRCasesheetProperties> all = this._uow.Repository<OMRCasesheetProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<OMRCasesheetProperties>((Func<OMRCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            OMRCasesheetPropertyValues entity2 = new OMRCasesheetPropertyValues();
            entity2.OMRId = num;
            OMRCasesheetProperties casesheetProperties = all.FirstOrDefault<OMRCasesheetProperties>((Func<OMRCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
            {
              entity2.PropId = casesheetProperties.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<OMRCasesheetPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      return num;
    }

    public PHDViewModel BindEditPHDPatientModel(long allotId, int PHDId)
    {
      try
      {
        PHDViewModel phdPatientDetails = this.GetPHDPatientDetails(PHDId);
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 8;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 8);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        allotmentViewModel.DoctorId = informationViewModel.DoctorId;
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = informationViewModel.PatientId,
          FromdeptId = 8
        };
        phdPatientDetails.patientInformationViewModel = informationViewModel;
        phdPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        phdPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 8);
        phdPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 8, 0);
        phdPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 8).ToList<ReferralStatusViewModel>();
        phdPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        phdPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 8).ToList<ReferralStatusViewModel>();
        phdPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 8, 0);
        phdPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 8);
        phdPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(8);
        phdPatientDetails.studentAllotmentViewModel = allotmentViewModel;
        phdPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 8, 0);
        phdPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
        phdPatientDetails.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 8,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        phdPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 8, 0);
        phdPatientDetails.ParafunctionalHabitslist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 4)).ToList<MASCode>();
        phdPatientDetails.BrushingHabitsMethodlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 5)).ToList<MASCode>();
        phdPatientDetails.BrushingHabitsFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 6)).ToList<MASCode>();
        phdPatientDetails.BrushingHabitsDurlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 7)).ToList<MASCode>();
        phdPatientDetails.ChangingBrushFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 8)).ToList<MASCode>();
        phdPatientDetails.BrushTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 19)).ToList<MASCode>();
        phdPatientDetails.DentifriceTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 18)).ToList<MASCode>();
        phdPatientDetails.Consistencylist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 9)).ToList<MASCode>();
        phdPatientDetails.Grouplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 10)).ToList<MASCode>();
        phdPatientDetails.OverBitelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 11)).ToList<MASCode>();
        phdPatientDetails.OverJetlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 12)).ToList<MASCode>();
        phdPatientDetails.MoralRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 13)).ToList<MASCode>();
        phdPatientDetails.CanineRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 14)).ToList<MASCode>();
        phdPatientDetails.SkeletaRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 15)).ToList<MASCode>();
        phdPatientDetails.OcculsionMolarRelationlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 16)).ToList<MASCode>();
        phdPatientDetails.OcculsionCanineRelationlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 17)).ToList<MASCode>();
        phdPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        phdPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        phdPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        phdPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
        phdPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) phdPatientDetails.PatientId))
        };
        phdPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) phdPatientDetails.PatientId))
        };
        phdPatientDetails.approvalViewModal = new ApprovalViewModal()
        {
          ApprovalTypeId = 1,
          CaserecordId = PHDId,
          DeptId = 8,
          DoctorId = allotmentViewModel.DoctorId,
          PatientId = (long) phdPatientDetails.PatientId,
          ReferredTreatmentId = 0
        };
        IEnumerable<PHDCasesheetProperties> properties = this.GetProperties();
        phdPatientDetails.Proplist = properties;
        if (phdPatientDetails.Approval1)
        {
          phdPatientDetails.DisplayApproval1 = "Approved";
          phdPatientDetails.ReadOnlyApproval1 = true;
        }
        else
        {
          phdPatientDetails.DisplayApproval1 = "Not Approved";
          phdPatientDetails.ReadOnlyApproval1 = false;
        }
        if (phdPatientDetails.Approval2)
        {
          phdPatientDetails.DisplayApproval2 = "Approved";
          phdPatientDetails.ReadOnlyApproval2 = true;
          phdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
        }
        else
        {
          phdPatientDetails.DisplayApproval2 = "Not Approved";
          phdPatientDetails.ReadOnlyApproval2 = false;
          phdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
        }
        if (phdPatientDetails.Approval3)
        {
          phdPatientDetails.DisplayApproval3 = "Casesheet Closed";
          phdPatientDetails.ReadOnlyApproval3 = true;
          phdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
          phdPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
        }
        else
        {
          phdPatientDetails.DisplayApproval3 = "Not Approved";
          phdPatientDetails.ReadOnlyApproval3 = false;
          phdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
          phdPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
        }
        PHDViewModel phdViewModel = this._uow.Repository<PHDViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayPHDApprovalStage, (object) 8, (object) phdPatientDetails.PHDId)).FirstOrDefault<PHDViewModel>();
        phdPatientDetails.ApprovalStage = phdViewModel == null ? "Not Initiated" : (!(phdViewModel.ApprovalStage == "") ? phdViewModel.ApprovalStage : "Not Initiated");
        phdPatientDetails.ReadOnlyApproval4 = true;
        return phdPatientDetails;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdatePHDPatient(PHDViewModel ViewModel)
    {
      ViewModel.PHDDate = Convert.ToDateTime(ViewModel.PHDDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      ViewModel.PHDId = ViewModel.PHDId;
      ViewModel.DietId = new int?((int) ViewModel.Diet);
      PHDCasesheet phdCasesheet = new PHDCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PHDViewModel, PHDCasesheet>()));
      PHDCasesheet entity1 = Mapper.Map<PHDViewModel, PHDCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      this._uow.Repository<PHDCasesheet>().Update(entity1, false);
      IEnumerable<PHDCasesheetProperties> all1 = this._uow.Repository<PHDCasesheetProperties>().GetAll();
      IEnumerable<PHDCasesheetPropertyValues> all2 = this._uow.Repository<PHDCasesheetPropertyValues>().GetAll("PHDId=" + (object) ViewModel.PHDId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<PHDCasesheetProperties>((Func<PHDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            PHDCasesheetPropertyValues entity2 = new PHDCasesheetPropertyValues();
            entity2.PHDId = ViewModel.PHDId;
            PHDCasesheetProperties property = all1.FirstOrDefault<PHDCasesheetProperties>((Func<PHDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              PHDCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<PHDCasesheetPropertyValues>((Func<PHDCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.PHDId == ViewModel.PHDId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                entity2.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<PHDCasesheetPropertyValues>().Update(entity2, false);
              }
              else
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<PHDCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
      }
      if (ViewModel.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = ViewModel.PatientId;
            entity2.FromdeptId = 8;
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
      if (ViewModel.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.PHDId,
          ReferredTreatmentId = 0,
          DeptId = 8,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.PHDId,
          ReferredTreatmentId = 0,
          DeptId = 8,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 8,
          ReferredTreatmentId = 0
        });
      return entity1.PHDId;
    }

    public void UpdateOMRPHDDetails(PHDViewModel ViewModel)
    {
      ViewModel.omrPHDCasesheetViewModel.DietId = new int?((int) ViewModel.omrPHDCasesheetViewModel.Diet);
      ViewModel.omrPHDCasesheetViewModel.OMRDate = Convert.ToDateTime(ViewModel.omrPHDCasesheetViewModel.OMRDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      ViewModel.OMRId = ViewModel.OMRId;
      OMRCasesheet omrCasesheet = new OMRCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PHDViewModel, OMRCasesheet>()));
      OMRCasesheet entity1 = Mapper.Map<PHDViewModel, OMRCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      this._uow.Repository<OMRCasesheet>().Update(entity1, false);
      IEnumerable<OMRCasesheetProperties> all1 = this._uow.Repository<OMRCasesheetProperties>().GetAll();
      IEnumerable<OMRCasesheetPropertyValues> all2 = this._uow.Repository<OMRCasesheetPropertyValues>().GetAll("OMRId=" + (object) ViewModel.OMRId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<OMRCasesheetProperties>((Func<OMRCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            OMRCasesheetPropertyValues entity2 = new OMRCasesheetPropertyValues();
            entity2.OMRId = ViewModel.OMRId;
            OMRCasesheetProperties property = all1.FirstOrDefault<OMRCasesheetProperties>((Func<OMRCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              OMRCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<OMRCasesheetPropertyValues>((Func<OMRCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.OMRId == ViewModel.OMRId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                entity2.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<OMRCasesheetPropertyValues>().Update(entity2, false);
              }
              else
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<OMRCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
      }
    }

    public StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      int DeptId = 8;
      string DeptCode = Department.PHD.ToString();
      return this._AllotmentService.DisplayAllotment(PatientId, ReferredId, CourseType, DeptId, DeptCode);
    }

    public void UpdateAllotment(PHDViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.PHDId,
        ReferredTreatmentId = 0
      });
    }

    public void SavefollowUp(PHDViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 8,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 0
      });
    }

    public void UpdateReferralStatus(PHDViewModel model)
    {
      int patientId = model.PatientId;
      int num = 8;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 0
      });
    }

    public void ProcedureApproval(PHDViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
    }

    public List<PHDSearchDetails> phdSearchDetails(string From_Date, string To_Date, string url)
    {
      string str = "(OP.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      return this._uow.Repository<PHDSearchDetails>().GetEntitiesBySql(string.Format(Queries.PHDSearch, (object) From_Date, (object) To_Date, (object) 8, (object) url, (object) str)).ToList<PHDSearchDetails>();
    }

    public List<StudentAllotmentViewModel> phdCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.PHDApproval, (object) From_Date, (object) To_Date, (object) 8, (object) url)).ToList<StudentAllotmentViewModel>();
    }

    public PHDViewModel BindTreatmentList(long allotId)
    {
      PHDViewModel phdViewModel = new PHDViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 1;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        phdViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        phdViewModel.patientInformationViewModel = informationViewModel;
        phdViewModel.studentAllotmentViewModel = allotmentViewModel;
        phdViewModel.PatientId = informationViewModel.PatientId;
        phdViewModel.Treatmentlist = (IEnumerable<PHDViewModel>) this._uow.Repository<PHDViewModel>().GetEntitiesBySql(string.Format(Queries.PHDPatientTreatmentList, (object) informationViewModel.PatientId)).ToList<PHDViewModel>();
      }
      return phdViewModel;
    }

    public PHDViewModel BindPHDPatientReport(int PHDId)
    {
      try
      {
        PHDViewModel phdViewModel = this._uow.Repository<PHDViewModel>().GetEntitiesBySql(string.Format("exec GetPHDCasesheetReport {0}", (object) PHDId)).FirstOrDefault<PHDViewModel>();
        PatientInformationViewModel informationViewModel = new PatientInformationViewModel();
        informationViewModel.OpNo = phdViewModel.OpNo;
        informationViewModel.PatientName = phdViewModel.PatientName;
        informationViewModel.AgeGender = phdViewModel.Age.ToString() + "/" + (object) (Gender) phdViewModel.GenderId;
        informationViewModel.Area = phdViewModel.Area;
        informationViewModel.Phone = phdViewModel.Phone;
        phdViewModel.patientInformationViewModel = informationViewModel;
        phdViewModel.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) phdViewModel.PatientId, (object) 8));
        phdViewModel.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) phdViewModel.PatientId, (object) 8, (object) PHDId, (object) 0, (object) 20, (object) 16));
        phdViewModel.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) phdViewModel.PatientId, (object) 8, (object) PHDId, (object) 0));
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = phdViewModel.PatientId,
          FromdeptId = 8
        };
        phdViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(phdViewModel.PatientId).ToList<ReferralStatusViewModel>();
        phdViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = phdViewModel.PatientId,
          DeptId = 8,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        phdViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 8, 0);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        phdViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 8, 0);
        return phdViewModel;
      }
      catch (Exception ex)
      {
        throw ex;
      }
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
