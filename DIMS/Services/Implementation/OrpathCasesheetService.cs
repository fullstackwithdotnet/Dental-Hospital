// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.OrpathCasesheetService
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
  public class OrpathCasesheetService : ServiceBase<ORPATHCasesheet>, IOrpathCasesheetService, IService<ORPATHCasesheet>
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

    public OrpathCasesheetService(IUnitOfWork uow)
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

    public StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      int DeptId = 9;
      string DeptCode = Department.ORPATH.ToString();
      return this._AllotmentService.DisplayAllotment(PatientId, ReferredId, CourseType, DeptId, DeptCode);
    }

    public ORPATHRequisitionViewModel GetRequisitionDetails(int Id)
    {
      return this._uow.Repository<ORPATHRequisitionViewModel>().GetEntitiesBySql(string.Format("exec GetORPATHCasesheet {0}", (object) Id)).FirstOrDefault<ORPATHRequisitionViewModel>();
    }

    public List<OrpathRequisitionSearchDetails> OrpathSearchDetails(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<OrpathRequisitionSearchDetails>().GetEntitiesBySql(string.Format(Queries.OrpathSearch, (object) From_Date, (object) To_Date, (object) 9, (object) url)).ToList<OrpathRequisitionSearchDetails>();
    }

    public List<StudentAllotmentViewModel> orpathCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.OrpathApproval, (object) From_Date, (object) To_Date, (object) 9, (object) url)).ToList<StudentAllotmentViewModel>();
    }

    public IEnumerable<ORPATHCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<ORPATHCasesheetProperties>().GetAll();
    }

    public ORPATHRequisitionViewModel BindTreatmentList(long allotId)
    {
      ORPATHRequisitionViewModel requisitionViewModel = new ORPATHRequisitionViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = " DeptId=" + (object) 9;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 9);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        requisitionViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        requisitionViewModel.patientInformationViewModel = informationViewModel;
        requisitionViewModel.studentAllotmentViewModel = allotmentViewModel;
        requisitionViewModel.PatientId = informationViewModel.PatientId;
        requisitionViewModel.Treatmentlist = (IEnumerable<ORPATHRequisitionViewModel>) this._uow.Repository<ORPATHRequisitionViewModel>().GetEntitiesBySql(string.Format(Queries.OrpathPatientTreatmentList, (object) informationViewModel.PatientId)).ToList<ORPATHRequisitionViewModel>();
      }
      return requisitionViewModel;
    }

    public ORPATHRequisitionViewModel BindRequisitionPatientModel(long allotId)
    {
      ORPATHRequisitionViewModel requisitionViewModel = new ORPATHRequisitionViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = " DeptId=" + (object) 9;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 9);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        requisitionViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        requisitionViewModel.patientInformationViewModel = informationViewModel;
        requisitionViewModel.studentAllotmentViewModel = allotmentViewModel;
        requisitionViewModel.PatientId = informationViewModel.PatientId;
        requisitionViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        requisitionViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 9, 0);
        requisitionViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 9);
        requisitionViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(9);
        requisitionViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 9, 0);
        requisitionViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 9);
        requisitionViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        requisitionViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 9).ToList<ReferralStatusViewModel>();
        requisitionViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 9, 0);
        requisitionViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        requisitionViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        requisitionViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        requisitionViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 9,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        requisitionViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 9, 0);
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        requisitionViewModel.medicalalertviewmodel = medicalAlertViewModel2;
        requisitionViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) requisitionViewModel.PatientId))
        };
        requisitionViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) requisitionViewModel.PatientId))
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
        IEnumerable<ORPATHCasesheetProperties> properties = this.GetProperties();
        requisitionViewModel.Proplist = properties;
      }
      return requisitionViewModel;
    }

    public int SaveOrpathPatient(ORPATHRequisitionViewModel model)
    {
      try
      {
        model.MandatoryDummy = "Y";
        ORPATHCasesheet orpathCasesheet = new ORPATHCasesheet();
        ORPATHCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<ORPATHRequisitionViewModel, ORPATHCasesheet>())).CreateMapper().Map<ORPATHRequisitionViewModel, ORPATHCasesheet>(model);
        entity1.RequisitionDate = DateTime.Now;
        entity1.LastVisitedDate = new DateTime?(DateTime.Now);
        entity1.CreatedDate = new DateTime?(DateTime.Now);
        entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        entity1.RequisitionNo = this._CasesheetNoService.GetOralPathNo();
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
        model.RequisitionId = num;
        if (model.CreatedepartmentReferredStatus != null)
        {
          ReferralStatus entity2 = new ReferralStatus();
          foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
          {
            if (createdepartmentReferredStatu.chkToDeptId)
            {
              entity2.PatientId = model.PatientId;
              entity2.FromdeptId = 9;
              entity2.ReferredReason = createdepartmentReferredStatu.ReferredReason;
              entity2.ToDeptId = createdepartmentReferredStatu.ToDeptId;
              entity2.Priority = createdepartmentReferredStatu.Priority;
              entity2.RoomNo = createdepartmentReferredStatu.RoomNo;
              entity2.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
              entity2.FromDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
              entity2.CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
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
            DeptId = 9,
            PatientId = model.PatientId
          });
        if (model.BillingLabRadQueueDetails != null)
          this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
          {
            CaserecordId = num,
            ReferredTreatmentId = 0,
            DeptId = 9,
            PatientId = model.PatientId
          });
        if (model.PrescriptionsDetails != null)
          this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
          {
            PatientId = model.PatientId,
            AllotId = model.studentAllotmentViewModel.AllotId,
            DoctorId = model.studentAllotmentViewModel.DoctorId,
            StudentId = model.studentAllotmentViewModel.StudentId,
            DeptId = 9,
            ReferredTreatmentId = 0
          });
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public ORPATHRequisitionViewModel BindEditOrpathPatientModel(long allotId, int Id)
    {
      try
      {
        ORPATHRequisitionViewModel requisitionDetails = this.GetRequisitionDetails(Id);
        this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 9;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 9);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        allotmentViewModel.DoctorId = informationViewModel.DoctorId;
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = informationViewModel.PatientId,
          FromdeptId = 9
        };
        requisitionDetails.patientInformationViewModel = informationViewModel;
        requisitionDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 9).ToList<ReferralStatusViewModel>();
        requisitionDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        requisitionDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 9).ToList<ReferralStatusViewModel>();
        requisitionDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 9, 0);
        requisitionDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        requisitionDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 9);
        requisitionDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 9, 0);
        requisitionDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 9);
        requisitionDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(9);
        requisitionDetails.studentAllotmentViewModel = allotmentViewModel;
        requisitionDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 9, 0);
        requisitionDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        requisitionDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        requisitionDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        requisitionDetails.TreatmentReferredId = informationViewModel.ReferredId;
        requisitionDetails.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 9,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        requisitionDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 9, 0);
        requisitionDetails.Lesionlist = this._Dropdownservice.GetCodesById(44);
        requisitionDetails.Tendernesslist = this._Dropdownservice.GetCodesById(45);
        requisitionDetails.Consistencylist = this._Dropdownservice.GetCodesById(9);
        requisitionDetails.TypeOfSamplelist = this._Dropdownservice.GetCodesById(46);
        requisitionDetails.HardTissuelist = this._Dropdownservice.GetCodesById(47);
        requisitionDetails.TypeOfBiopsylist = this._Dropdownservice.GetCodesById(48);
        requisitionDetails.LymphNodeStatuslist = this._Dropdownservice.GetCodesById(57);
        IEnumerable<ORPATHCasesheetProperties> properties = this.GetProperties();
        requisitionDetails.Proplist = properties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        requisitionDetails.medicalalertviewmodel = medicalAlertViewModel2;
        requisitionDetails.approvalViewModal = new ApprovalViewModal()
        {
          ApprovalTypeId = 1,
          CaserecordId = Id,
          DeptId = 9,
          DoctorId = allotmentViewModel.DoctorId,
          PatientId = (long) requisitionDetails.PatientId,
          ReferredTreatmentId = 0
        };
        requisitionDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) requisitionDetails.PatientId))
        };
        requisitionDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) requisitionDetails.PatientId))
        };
        if (requisitionDetails.Approval1)
        {
          requisitionDetails.DisplayApproval1 = "Approved";
          requisitionDetails.ReadOnlyApproval1 = true;
        }
        else
        {
          requisitionDetails.DisplayApproval1 = "Not Approved";
          requisitionDetails.ReadOnlyApproval1 = false;
        }
        if (requisitionDetails.Approval2)
        {
          requisitionDetails.DisplayApproval2 = "Approved";
          requisitionDetails.ReadOnlyApproval2 = true;
          requisitionDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
        }
        else
        {
          requisitionDetails.DisplayApproval2 = "Not Approved";
          requisitionDetails.ReadOnlyApproval2 = false;
          requisitionDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
        }
        if (requisitionDetails.Approval3)
        {
          requisitionDetails.DisplayApproval3 = "Casesheet Closed";
          requisitionDetails.ReadOnlyApproval3 = true;
          requisitionDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
          requisitionDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
        }
        else
        {
          requisitionDetails.DisplayApproval3 = "Not Approved";
          requisitionDetails.ReadOnlyApproval3 = false;
          requisitionDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
          requisitionDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
        }
        ORPATHRequisitionViewModel requisitionViewModel = this._uow.Repository<ORPATHRequisitionViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayOrpathApprovalStage, (object) 9, (object) requisitionDetails.RequisitionId)).FirstOrDefault<ORPATHRequisitionViewModel>();
        requisitionDetails.ApprovalStage = requisitionViewModel == null ? "Not Initiated" : (!(requisitionViewModel.ApprovalStage == "") ? requisitionViewModel.ApprovalStage : "Not Initiated");
        requisitionDetails.ReadOnlyApproval4 = true;
        return requisitionDetails;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateOrpathPatient(ORPATHRequisitionViewModel ViewModel)
    {
      ViewModel.RequisitionDate = Convert.ToDateTime(ViewModel.RequisitionDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      ViewModel.RequisitionId = ViewModel.RequisitionId;
      ORPATHCasesheet orpathCasesheet = new ORPATHCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<ORPATHRequisitionViewModel, ORPATHCasesheet>()));
      ORPATHCasesheet entity1 = Mapper.Map<ORPATHRequisitionViewModel, ORPATHCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(DateTime.Now);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
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
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
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
      }
      if (ViewModel.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = ViewModel.PatientId;
            entity2.FromdeptId = 9;
            entity2.ReferredReason = createdepartmentReferredStatu.ReferredReason;
            entity2.ToDeptId = createdepartmentReferredStatu.ToDeptId;
            entity2.Priority = createdepartmentReferredStatu.Priority;
            entity2.RoomNo = createdepartmentReferredStatu.RoomNo;
            entity2.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
            entity2.FromDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            entity2.CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
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
        entity2.DeptId = 9;
        entity2.CaseRecordId = ViewModel.RequisitionId;
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
          CaserecordId = ViewModel.RequisitionId,
          ReferredTreatmentId = 0,
          DeptId = 9,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.RequisitionId,
          ReferredTreatmentId = 0,
          DeptId = 9,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 9,
          ReferredTreatmentId = 0
        });
      return entity1.RequisitionId;
    }

    public void SavefollowUp(ORPATHRequisitionViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 9,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 0
      });
    }

    public void UpdateAllotment(ORPATHRequisitionViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.RequisitionId,
        ReferredTreatmentId = 0
      });
    }

    public void UpdateReferralStatus(ORPATHRequisitionViewModel model)
    {
      int patientId = model.PatientId;
      int num = 9;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 0
      });
    }

    public void ProcedureApproval(ORPATHRequisitionViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
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
        requisitionViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) requisitionViewModel2.PatientId, (object) 9));
        requisitionViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) requisitionViewModel2.PatientId, (object) 9, (object) RequisitionId, (object) 0, (object) 20, (object) 16));
        requisitionViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) requisitionViewModel2.PatientId, (object) 9, (object) RequisitionId, (object) 0));
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = requisitionViewModel2.PatientId,
          FromdeptId = 9
        };
        requisitionViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(requisitionViewModel2.PatientId).ToList<ReferralStatusViewModel>();
        requisitionViewModel2.followupViewModal = new FollowupViewModal()
        {
          PatientId = requisitionViewModel2.PatientId,
          DeptId = 9,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        requisitionViewModel2.followupList = (IEnumerable<FollowupViewModal>) this._uow.Repository<FollowupViewModal>().GetEntitiesBySql(string.Format(Queries.FollowUpDetailsList, (object) requisitionViewModel2.PatientId, (object) 9, (object) 0)).ToList<FollowupViewModal>();
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        requisitionViewModel2.studentProcedureNotesViewModel = (IEnumerable<StudentAllotmentViewModel>) this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedProcedureNotesDetails, (object) requisitionViewModel2.PatientId, (object) 9, (object) 0)).ToList<StudentAllotmentViewModel>();
        return requisitionViewModel2;
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
