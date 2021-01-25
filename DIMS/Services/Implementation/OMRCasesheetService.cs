// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.OMRCasesheetService
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
  public class OMRCasesheetService : ServiceBase<OMRCasesheet>, IOMRCasesheetService, IService<OMRCasesheet>
  {
    private IUnitOfWork _uow;
    private IOPDPatientRegistrationService _OPDservice;
    private IMASCodeService _Dropdownservice;
    private IReferralStatusService _Referralservice;
    private IBillQueueService _BillQueueservice;
    private IStudentAllotmentService _AllotmentService;
    private IFollowUpService _FollowUpService;
    private ICasesheetNoService _CasesheetNoService;
    private IPrescriptionsService _PrescriptionsService;

    public OMRCasesheetService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._OPDservice = (IOPDPatientRegistrationService) new OPDPatientRegistrationService(this._uow);
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._Referralservice = (IReferralStatusService) new ReferralStatusService(this._uow);
      this._BillQueueservice = (IBillQueueService) new BillQueueService(this._uow);
      this._AllotmentService = (IStudentAllotmentService) new StudentAllotmentService(this._uow);
      this._FollowUpService = (IFollowUpService) new FollowUpService(this._uow);
      this._CasesheetNoService = (ICasesheetNoService) new CasesheetNoService(this._uow);
      this._PrescriptionsService = (IPrescriptionsService) new PrescriptionsService(this._uow);
    }

    public OMRViewModel GetOMRPatientDetails(int Id)
    {
      return this._uow.Repository<OMRViewModel>().GetEntitiesBySql(string.Format("exec GetOMRCasesheet {0}", (object) Id)).FirstOrDefault<OMRViewModel>();
    }

    public IEnumerable<OMRCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<OMRCasesheetProperties>().GetAll();
    }

    public OMRViewModel BindOMRPatientModel(long allotId)
    {
      OMRViewModel omrViewModel = new OMRViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = " DeptId=" + (object) 1;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 1);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        omrViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        omrViewModel.patientInformationViewModel = informationViewModel;
        omrViewModel.studentAllotmentViewModel = allotmentViewModel;
        omrViewModel.PatientId = informationViewModel.PatientId;
        omrViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        omrViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 1, 0);
        omrViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 1);
        omrViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(1);
        omrViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 1, 0);
        omrViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 1);
        omrViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        omrViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 1).ToList<ReferralStatusViewModel>();
        omrViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 1, 0);
        omrViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        omrViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        omrViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 1,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        omrViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 1, 0);
        omrViewModel.ParafunctionalHabitslist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 4)).ToList<MASCode>();
        omrViewModel.BrushingHabitsMethodlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 5)).ToList<MASCode>();
        omrViewModel.BrushingHabitsFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 6)).ToList<MASCode>();
        omrViewModel.BrushingHabitsDurlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 7)).ToList<MASCode>();
        omrViewModel.ChangingBrushFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 8)).ToList<MASCode>();
        omrViewModel.BrushTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 19)).ToList<MASCode>();
        omrViewModel.DentifriceTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 18)).ToList<MASCode>();
        omrViewModel.Consistencylist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 9)).ToList<MASCode>();
        omrViewModel.Grouplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 10)).ToList<MASCode>();
        omrViewModel.OverBitelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 11)).ToList<MASCode>();
        omrViewModel.OverJetlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 12)).ToList<MASCode>();
        omrViewModel.MoralRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 13)).ToList<MASCode>();
        omrViewModel.CanineRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 14)).ToList<MASCode>();
        omrViewModel.SkeletaRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 15)).ToList<MASCode>();
        omrViewModel.OcculsionMolarRelationlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 16)).ToList<MASCode>();
        omrViewModel.OcculsionCanineRelationlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 17)).ToList<MASCode>();
        omrViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) omrViewModel.PatientId))
        };
        omrViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) omrViewModel.PatientId))
        };
        IEnumerable<OMRCasesheetProperties> properties = this.GetProperties();
        omrViewModel.Proplist = properties;
      }
      return omrViewModel;
    }

    public int SaveOMRPatient(OMRViewModel model)
    {
      try
      {
        model.MandatoryDummy = "Y";
        OMRCasesheet omrCasesheet = new OMRCasesheet();
        OMRCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OMRViewModel, OMRCasesheet>())).CreateMapper().Map<OMRViewModel, OMRCasesheet>(model);
        entity1.OMRDate = DateTime.Now;
        entity1.CreatedDate = new DateTime?(DateTime.Now);
        entity1.LastVisitedDate = new DateTime?(DateTime.Now);
        entity1.DietId = (int) model.Diet;
        entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
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
        model.OMRId = num;
        if (model.CreatedepartmentReferredStatus != null)
        {
          ReferralStatus entity2 = new ReferralStatus();
          foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
          {
            if (createdepartmentReferredStatu.chkToDeptId)
            {
              entity2.PatientId = model.PatientId;
              entity2.FromdeptId = 1;
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
        if (model.referredtoOthersViewModel != null)
        {
          ReferredToOthers entity2 = new ReferredToOthers();
          ReferredToOthersViewModel referredtoOthersViewModel = model.referredtoOthersViewModel;
          entity2.PatientId = model.PatientId;
          entity2.DeptId = 1;
          entity2.CaseRecordId = model.OMRId;
          entity2.TreatmentId = 0;
          entity2.ReferredOthersReason = referredtoOthersViewModel.ReferredOthersReason;
          entity2.DoctorName = referredtoOthersViewModel.DoctorName;
          entity2.HospitalName = referredtoOthersViewModel.HospitalName;
          entity2.ReferredOthersId = model.ReferredOthersId;
          if (entity2.ReferredOthersId > 0)
            this._uow.Repository<ReferredToOthers>().Update(entity2, false);
          else
            this._uow.Repository<ReferredToOthers>().Add(entity2, false);
        }
        if (model.BillingQueueDetails != null)
          this._BillQueueservice.SaveInvestigation(model.BillingQueueDetails, new BillQueueDetails()
          {
            CaserecordId = num,
            ReferredTreatmentId = 0,
            DeptId = 1,
            PatientId = model.PatientId
          });
        if (model.BillingLabRadQueueDetails != null)
          this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
          {
            CaserecordId = num,
            ReferredTreatmentId = 0,
            DeptId = 1,
            PatientId = model.PatientId
          });
        if (model.PrescriptionsDetails != null)
          this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
          {
            PatientId = model.PatientId,
            AllotId = model.studentAllotmentViewModel.AllotId,
            DoctorId = model.studentAllotmentViewModel.DoctorId,
            StudentId = model.studentAllotmentViewModel.StudentId,
            DeptId = 1,
            ReferredTreatmentId = 0
          });
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public OMRViewModel BindEditOMRPatientModel(long allotId, int OMRId)
    {
      try
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        OMRViewModel omrPatientDetails = this.GetOMRPatientDetails(OMRId);
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 1;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 1);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        allotmentViewModel.DoctorId = informationViewModel.DoctorId;
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = informationViewModel.PatientId,
          FromdeptId = 1
        };
        ReferredToOthersViewModel toOthersViewModel = new ReferredToOthersViewModel();
        toOthersViewModel.PatientId = informationViewModel.PatientId;
        toOthersViewModel.DeptId = 1;
        toOthersViewModel.TreatmentId = 0;
        omrPatientDetails.referredtoOthersViewModel = this._uow.Repository<ReferredToOthersViewModel>().GetEntitiesBySql(string.Format(Queries.ReferredToOthers, (object) informationViewModel.DeptId, (object) informationViewModel.PatientId, (object) OMRId, (object) toOthersViewModel.TreatmentId)).FirstOrDefault<ReferredToOthersViewModel>();
        if (omrPatientDetails.referredtoOthersViewModel != null)
        {
          toOthersViewModel.ReferredOthersId = omrPatientDetails.referredtoOthersViewModel.ReferredOthersId;
          omrPatientDetails.ReferredOthersId = omrPatientDetails.referredtoOthersViewModel.ReferredOthersId;
        }
        else
          omrPatientDetails.ReferredOthersId = 0;
        omrPatientDetails.patientInformationViewModel = informationViewModel;
        omrPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 1).ToList<ReferralStatusViewModel>();
        omrPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        omrPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 1).ToList<ReferralStatusViewModel>();
        omrPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 1, 0);
        omrPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 1, 0);
        omrPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 1);
        omrPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 1);
        omrPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(1);
        omrPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        omrPatientDetails.studentAllotmentViewModel = allotmentViewModel;
        omrPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 1, 0);
        omrPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        omrPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        omrPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        omrPatientDetails.Diet = (Diet) omrPatientDetails.DietId.Value;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        omrPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
        omrPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
        omrPatientDetails.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 1,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        omrPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 1, 0);
        omrPatientDetails.ParafunctionalHabitslist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 4)).ToList<MASCode>();
        omrPatientDetails.BrushingHabitsMethodlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 5)).ToList<MASCode>();
        omrPatientDetails.BrushingHabitsFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 6)).ToList<MASCode>();
        omrPatientDetails.BrushingHabitsDurlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 7)).ToList<MASCode>();
        omrPatientDetails.ChangingBrushFreqlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 8)).ToList<MASCode>();
        omrPatientDetails.BrushTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 19)).ToList<MASCode>();
        omrPatientDetails.DentifriceTypelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 18)).ToList<MASCode>();
        omrPatientDetails.Consistencylist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 9)).ToList<MASCode>();
        omrPatientDetails.Grouplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 10)).ToList<MASCode>();
        omrPatientDetails.OverBitelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 11)).ToList<MASCode>();
        omrPatientDetails.OverJetlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 12)).ToList<MASCode>();
        omrPatientDetails.MoralRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 13)).ToList<MASCode>();
        omrPatientDetails.CanineRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 14)).ToList<MASCode>();
        omrPatientDetails.SkeletaRelationshiplist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 15)).ToList<MASCode>();
        omrPatientDetails.OcculsionMolarRelationlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 16)).ToList<MASCode>();
        omrPatientDetails.OcculsionCanineRelationlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 17)).ToList<MASCode>();
        IEnumerable<OMRCasesheetProperties> properties = this.GetProperties();
        omrPatientDetails.Proplist = properties;
        omrPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) omrPatientDetails.PatientId))
        };
        omrPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) omrPatientDetails.PatientId))
        };
        omrPatientDetails.approvalViewModal = new ApprovalViewModal()
        {
          ApprovalTypeId = 1,
          CaserecordId = OMRId,
          DeptId = 1,
          DoctorId = allotmentViewModel.DoctorId,
          PatientId = (long) omrPatientDetails.PatientId,
          ReferredTreatmentId = 0
        };
        if (omrPatientDetails.Approval1)
        {
          omrPatientDetails.DisplayApproval1 = "Approved";
          omrPatientDetails.ReadOnlyApproval1 = true;
        }
        else
        {
          omrPatientDetails.DisplayApproval1 = "Not Approved";
          omrPatientDetails.ReadOnlyApproval1 = false;
        }
        if (omrPatientDetails.Approval2)
        {
          omrPatientDetails.DisplayApproval2 = "Approved";
          omrPatientDetails.ReadOnlyApproval2 = true;
          omrPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
        }
        else
        {
          omrPatientDetails.DisplayApproval2 = "Not Approved";
          omrPatientDetails.ReadOnlyApproval2 = false;
          omrPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
        }
        if (omrPatientDetails.Approval3)
        {
          omrPatientDetails.DisplayApproval3 = "Casesheet Closed";
          omrPatientDetails.ReadOnlyApproval3 = true;
          omrPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
          omrPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
        }
        else
        {
          omrPatientDetails.DisplayApproval3 = "Not Approved";
          omrPatientDetails.ReadOnlyApproval3 = false;
          omrPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
          omrPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
        }
        OMRViewModel omrViewModel = this._uow.Repository<OMRViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayApprovalStage, (object) 1, (object) omrPatientDetails.OMRId)).FirstOrDefault<OMRViewModel>();
        omrPatientDetails.ApprovalStage = omrViewModel == null ? "Not Initiated" : (!(omrViewModel.ApprovalStage == "") ? omrViewModel.ApprovalStage : "Not Initiated");
        omrPatientDetails.ReadOnlyApproval4 = true;
        return omrPatientDetails;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateOMRPatient(OMRViewModel ViewModel)
    {
      ViewModel.DietId = new int?((int) ViewModel.Diet);
      ViewModel.OMRDate = Convert.ToDateTime(ViewModel.OMRDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      ViewModel.OMRId = ViewModel.OMRId;
      OMRCasesheet omrCasesheet = new OMRCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OMRViewModel, OMRCasesheet>()));
      OMRCasesheet entity1 = Mapper.Map<OMRViewModel, OMRCasesheet>(ViewModel);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
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
      if (ViewModel.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = ViewModel.PatientId;
            entity2.FromdeptId = 1;
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
      if (ViewModel.referredtoOthersViewModel != null)
      {
        ReferredToOthers entity2 = new ReferredToOthers();
        ReferredToOthersViewModel referredtoOthersViewModel = ViewModel.referredtoOthersViewModel;
        entity2.PatientId = ViewModel.PatientId;
        entity2.DeptId = 1;
        entity2.CaseRecordId = ViewModel.OMRId;
        entity2.TreatmentId = 0;
        entity2.ReferredOthersReason = referredtoOthersViewModel.ReferredOthersReason;
        entity2.DoctorName = referredtoOthersViewModel.DoctorName;
        entity2.HospitalName = referredtoOthersViewModel.HospitalName;
        entity2.ReferredOthersId = ViewModel.ReferredOthersId;
        if (entity2.ReferredOthersId > 0)
          this._uow.Repository<ReferredToOthers>().Update(entity2, false);
        else
          this._uow.Repository<ReferredToOthers>().Add(entity2, false);
      }
      if (ViewModel.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.OMRId,
          ReferredTreatmentId = 0,
          DeptId = 1,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.OMRId,
          ReferredTreatmentId = 0,
          DeptId = 1,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 1,
          ReferredTreatmentId = 0
        });
      return entity1.OMRId;
    }

    public StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      int DeptId = 1;
      string DeptCode = Department.OMR.ToString();
      return this._AllotmentService.DisplayAllotment(PatientId, ReferredId, CourseType, DeptId, DeptCode);
    }

    public void UpdateAllotment(OMRViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.OMRId,
        ApprovalType = model.ApprovalType,
        ReferredTreatmentId = 0
      });
    }

    public void SaveAllotment(OMRViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.OMRId,
        ReferredTreatmentId = 0
      });
    }

    public void SavefollowUp(OMRViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 1,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 0
      });
    }

    public void UpdateReferralStatus(OMRViewModel model)
    {
      int patientId = model.PatientId;
      int num = 1;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 0
      });
    }

    public void ProcedureApproval(OMRViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId,
        ApprovalType = model.ApprovalType
      });
    }

    public OMRViewModel DisplayOMRPatient(int Id)
    {
      return new OMRViewModel();
    }

    public List<OMRSearchDetails> omrSearchDetails(string From_Date, string To_Date, string url)
    {
      string str = "(OP.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      return this._uow.Repository<OMRSearchDetails>().GetEntitiesBySql(string.Format(Queries.OMRSearch, (object) From_Date, (object) To_Date, (object) 1, (object) url, (object) str)).ToList<OMRSearchDetails>();
    }

    public List<StudentAllotmentViewModel> omrCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.OMRApproval, (object) From_Date, (object) To_Date, (object) 1, (object) url)).ToList<StudentAllotmentViewModel>();
    }

    public OMRViewModel BindOMRPatientReport(int OMRId)
    {
      try
      {
        OMRViewModel omrViewModel1 = new OMRViewModel();
        OMRViewModel omrViewModel2 = this._uow.Repository<OMRViewModel>().GetEntitiesBySql(string.Format("exec GetOMRCasesheetReport {0}", (object) OMRId)).FirstOrDefault<OMRViewModel>();
        omrViewModel2.DietName = Convert.ToString((object) (Diet) omrViewModel2.DietId.Value);
        omrViewModel2.patientInformationViewModel = new PatientInformationViewModel()
        {
          PatientName = omrViewModel2.PatientName,
          AgeGender = omrViewModel2.Age.ToString() + "/" + (object) (Gender) omrViewModel2.GenderId,
          Phone = omrViewModel2.Phone,
          OpNo = omrViewModel2.OpNo,
          Area = omrViewModel2.Area
        };
        omrViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) omrViewModel2.PatientId, (object) 1));
        omrViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) omrViewModel2.PatientId, (object) 1, (object) OMRId, (object) 0, (object) 20, (object) 16));
        omrViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) omrViewModel2.PatientId, (object) 1, (object) OMRId, (object) 0));
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = omrViewModel2.PatientId,
          FromdeptId = 1
        };
        omrViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(omrViewModel2.PatientId).ToList<ReferralStatusViewModel>();
        omrViewModel2.followupViewModal = new FollowupViewModal()
        {
          PatientId = omrViewModel2.PatientId,
          DeptId = 1,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        omrViewModel2.followupList = (IEnumerable<FollowupViewModal>) this._uow.Repository<FollowupViewModal>().GetEntitiesBySql(string.Format(Queries.FollowUpDetailsList, (object) omrViewModel2.PatientId, (object) 1, (object) 0)).ToList<FollowupViewModal>();
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        omrViewModel2.studentProcedureNotesViewModel = (IEnumerable<StudentAllotmentViewModel>) this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedProcedureNotesDetails, (object) omrViewModel2.PatientId, (object) 1, (object) 0)).ToList<StudentAllotmentViewModel>();
        return omrViewModel2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public OMRViewModel BindTreatmentList(long allotId)
    {
      OMRViewModel omrViewModel = new OMRViewModel();
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
        omrViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        omrViewModel.patientInformationViewModel = informationViewModel;
        omrViewModel.studentAllotmentViewModel = allotmentViewModel;
        omrViewModel.PatientId = informationViewModel.PatientId;
        omrViewModel.Treatmentlist = (IEnumerable<OMRViewModel>) this._uow.Repository<OMRViewModel>().GetEntitiesBySql(string.Format(Queries.OMRPatientTreatmentList, (object) informationViewModel.PatientId)).ToList<OMRViewModel>();
      }
      return omrViewModel;
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
