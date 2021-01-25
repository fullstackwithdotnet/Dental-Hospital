// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.OMFSCasesheetService
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
  public class OMFSCasesheetService : ServiceBase<OMFSOPCasesheet>, IOMFSCasesheetService, IService<OMFSOPCasesheet>
  {
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;
    private IOPDPatientRegistrationService _OPDservice;
    private IMASChiefComplaintService _ChiefComplaintservice;
    private IReferralStatusService _Referralservice;
    private IBillQueueService _BillQueueservice;
    private IStudentAllotmentService _AllotmentService;
    private IFollowUpService _FollowUpService;
    private ICasesheetNoService _CasesheetNoService;
    private IPrescriptionsService _PrescriptionsService;

    public OMFSCasesheetService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = new MASCodeService(this._uow);
      this._OPDservice = new OPDPatientRegistrationService(this._uow);
      this._ChiefComplaintservice = new MASChiefComplaintService(this._uow);
      this._Referralservice = new ReferralStatusService(this._uow);
      this._BillQueueservice = new BillQueueService(this._uow);
      this._AllotmentService = new StudentAllotmentService(this._uow);
      this._FollowUpService = new FollowUpService(this._uow);
      this._CasesheetNoService = new CasesheetNoService(this._uow);
      this._PrescriptionsService = new PrescriptionsService(this._uow);
    }

    public IEnumerable<OMFSOPCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<OMFSOPCasesheetProperties>().GetAll();
    }

    public IEnumerable<OMFSIPCasesheetProperties> GetIPProperties()
    {
      return this._uow.Repository<OMFSIPCasesheetProperties>().GetAll();
    }

    public OMFSOPViewModel GetOMFSOPPatientDetails(int Id)
    {
      return this._uow.Repository<OMFSOPViewModel>().GetEntitiesBySql(string.Format("exec GetOMFSOPCasesheet {0}", Id)).FirstOrDefault();
    }

    public IEnumerable<OMFSTreatmentViewModel> TreatmentList()
    {
      var treatmentViewModelList = new List<OMFSTreatmentViewModel>();
      return this._uow.Repository<OMFSTreatmentViewModel>().GetEntitiesBySql(string.Format(Queries.TreatmentsTypes, 2)).ToList();
    }

    public IEnumerable<OMFSTreatmentViewModel> OMFSOPList(int patientId)
    {
      var treatmentViewModelList = new List<OMFSTreatmentViewModel>();
      return this._uow.Repository<OMFSTreatmentViewModel>().GetEntitiesBySql(string.Format(Queries.OMFSOpList, patientId)).ToList();
    }

    public IEnumerable<OMFSTreatmentViewModel> OMFSIPList(int patientId)
    {
      var treatmentViewModelList = new List<OMFSTreatmentViewModel>();
      return this._uow.Repository<OMFSTreatmentViewModel>().GetEntitiesBySql(string.Format(Queries.OMFSIpList, patientId)).ToList();
    }

    public OMFSTreatmentViewModel BindTreatmentModel(long allotId, int patientId)
    {
      var treatmentViewModel = new OMFSTreatmentViewModel();
      var informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, allotId)).SingleOrDefault();
      var whereClause = "DeptId=" + 2;
      informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (Gender) informationViewModel.GenderId;
      informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 2);
      var allotmentViewModel = new StudentAllotmentViewModel();
      allotmentViewModel.AllotId = allotId;
      allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
      allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
      treatmentViewModel.TreatmentReferredId = informationViewModel.ReferredId;
      treatmentViewModel.patientInformationViewModel = informationViewModel;
      treatmentViewModel.studentAllotmentViewModel = allotmentViewModel;
      treatmentViewModel.PatientId = patientId;
      treatmentViewModel.AllotId = allotId;
      treatmentViewModel.OmfsList = this.TreatmentList();
      treatmentViewModel.OmfsopList = this.OMFSOPList(patientId);
      return treatmentViewModel;
    }

    public OMFSOPViewModel BindOmfsOpPatientModel(long allotId, int TreatmentId)
    {
      var omfsopViewModel = new OMFSOPViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        var informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, allotId)).SingleOrDefault();
        var whereClause = "DeptId=" + 2;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 2);
        var allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        omfsopViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        omfsopViewModel.patientInformationViewModel = informationViewModel;
        omfsopViewModel.studentAllotmentViewModel = allotmentViewModel;
        omfsopViewModel.PatientId = informationViewModel.PatientId;
        omfsopViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        omfsopViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 2, 6);
        omfsopViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 2);
        omfsopViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(2);
        omfsopViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 2, 6);
        omfsopViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 2);
        omfsopViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 2, 6);
        omfsopViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 2).ToList();
        omfsopViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList();
        omfsopViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        omfsopViewModel.PrescriptionsDetails = this._PrescriptionsService.LoadPrescriptionsList(informationViewModel.PatientId, 2, 6);
        omfsopViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        omfsopViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 2,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        omfsopViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 2, 0);
        omfsopViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, omfsopViewModel.PatientId))
        };
        omfsopViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, omfsopViewModel.PatientId))
        };
        var properties = this.GetProperties();
        omfsopViewModel.Proplist = properties;
        var medicalAlertViewModel1 = new MedicalAlertViewModel();
        var medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        omfsopViewModel.medicalalertviewmodel = medicalAlertViewModel2;
      }
      return omfsopViewModel;
    }

    public int SaveOMFSOPPatient(OMFSOPViewModel model)
    {
      try
      {
        model.MandatoryDummy = "Y";
        var omfsopCasesheet = new OMFSOPCasesheet();
        var entity1 = new MapperConfiguration(cfg => cfg.CreateMap<OMFSOPViewModel, OMFSOPCasesheet>()).CreateMapper().Map<OMFSOPViewModel, OMFSOPCasesheet>(model);
        entity1.OMFSOpDate = DateTime.Now;
        entity1.CreatedDate = new DateTime?(DateTime.Now);
        entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        entity1.LastVisitedDate = new DateTime?(DateTime.Now);
        entity1.DietId = (int) model.Diet;
        entity1.OMFSOpNo = this._CasesheetNoService.GetOMFSOPNo();
        var num = this._uow.Repository<OMFSOPCasesheet>().Add(entity1, false);
        var all = this._uow.Repository<OMFSOPCasesheetProperties>().GetAll();
        foreach (var property in model.GetType().GetProperties())
        {
          var prop = property;
          if (all.FirstOrDefault(a => a.PropertyName == prop.Name) != null)
          {
            var name = prop.Name;
            var obj = prop.GetValue(model, null);
            if (obj != null)
            {
              var entity2 = new OMFSOPCasesheetPropertyValues();
              entity2.OMFSOpId = num;
              var casesheetProperties = all.FirstOrDefault(a => a.PropertyName == prop.Name);
              if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
              {
                entity2.PropId = casesheetProperties.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<OMFSOPCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
        if (model.CreatedepartmentReferredStatus != null)
        {
          var entity2 = new ReferralStatus();
          foreach (var createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
          {
            if (createdepartmentReferredStatu.chkToDeptId)
            {
              entity2.PatientId = model.PatientId;
              entity2.FromdeptId = 2;
              entity2.ReferredReason = createdepartmentReferredStatu.ReferredReason;
              entity2.ToDeptId = createdepartmentReferredStatu.ToDeptId;
              entity2.Priority = createdepartmentReferredStatu.Priority;
              entity2.RoomNo = createdepartmentReferredStatu.RoomNo;
              entity2.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
              var referralStatus1 = entity2;
              var now = DateTime.Now;
              var nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
              referralStatus1.FromDate = nullable1;
              var referralStatus2 = entity2;
              now = DateTime.Now;
              var nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
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
            DeptId = 2,
            PatientId = model.PatientId,
            ReferredTreatmentId = 6
          });
        if (model.BillingLabRadQueueDetails != null)
          this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
          {
            CaserecordId = num,
            ReferredTreatmentId = 6,
            DeptId = 2,
            PatientId = model.PatientId
          });
        if (model.PrescriptionsDetails != null)
          this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
          {
            PatientId = model.PatientId,
            AllotId = model.studentAllotmentViewModel.AllotId,
            DoctorId = model.studentAllotmentViewModel.DoctorId,
            StudentId = model.studentAllotmentViewModel.StudentId,
            DeptId = 2,
            ReferredTreatmentId = 6
          });
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public OMFSOPViewModel BindEditOmfsOpModel(long allotId, int OMFSOpId)
    {
      var omfsopPatientDetails = this.GetOMFSOPPatientDetails(OMFSOpId);
      var informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, allotId)).SingleOrDefault();
      var whereClause = "DeptId=" + 2;
      informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (Gender) informationViewModel.GenderId;
      informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 2);
      var allotmentViewModel = new StudentAllotmentViewModel();
      allotmentViewModel.AllotId = allotId;
      allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
      allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
      allotmentViewModel.DoctorId = informationViewModel.DoctorId;
      var referralStatusViewModel = new ReferralStatusViewModel()
      {
        PatientId = informationViewModel.PatientId,
        FromdeptId = 2
      };
      omfsopPatientDetails.patientInformationViewModel = informationViewModel;
      omfsopPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 2).ToList();
      omfsopPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList();
      omfsopPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 2).ToList();
      omfsopPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 2, 6);
      omfsopPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
      omfsopPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 2);
      omfsopPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 2, 6);
      omfsopPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 2);
      omfsopPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(2);
      omfsopPatientDetails.studentAllotmentViewModel = allotmentViewModel;
      omfsopPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 2, 6);
      omfsopPatientDetails.Diet = (Diet) omfsopPatientDetails.DietId;
      omfsopPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
      omfsopPatientDetails.followupViewModal = new FollowupViewModal()
      {
        PatientId = informationViewModel.PatientId,
        DeptId = 2,
        FollowupDate = DateTime.Now,
        FollowupTime = DateTime.Now
      };
      omfsopPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 2, 6);
      omfsopPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
      omfsopPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
      omfsopPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
      var properties = this.GetProperties();
      omfsopPatientDetails.Proplist = properties;
      omfsopPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
      {
        RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, omfsopPatientDetails.PatientId))
      };
      omfsopPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
      {
        LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, omfsopPatientDetails.PatientId))
      };
      var medicalAlertViewModel1 = new MedicalAlertViewModel();
      var medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
      omfsopPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
      omfsopPatientDetails.approvalViewModal = new ApprovalViewModal()
      {
        ApprovalTypeId = 1,
        CaserecordId = OMFSOpId,
        DeptId = 2,
        DoctorId = allotmentViewModel.DoctorId,
        PatientId = omfsopPatientDetails.PatientId,
        ReferredTreatmentId = 6
      };
      if (omfsopPatientDetails.Approval1)
      {
        omfsopPatientDetails.DisplayApproval1 = "Approved";
        omfsopPatientDetails.ReadOnlyApproval1 = true;
      }
      else
      {
        omfsopPatientDetails.DisplayApproval1 = "Not Approved";
        omfsopPatientDetails.ReadOnlyApproval1 = false;
      }
      if (omfsopPatientDetails.Approval2)
      {
        omfsopPatientDetails.DisplayApproval2 = "Approved";
        omfsopPatientDetails.ReadOnlyApproval2 = true;
        omfsopPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
      }
      else
      {
        omfsopPatientDetails.DisplayApproval2 = "Not Approved";
        omfsopPatientDetails.ReadOnlyApproval2 = false;
        omfsopPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
      }
      if (omfsopPatientDetails.Approval3)
      {
        omfsopPatientDetails.DisplayApproval3 = "Casesheet Closed";
        omfsopPatientDetails.ReadOnlyApproval3 = true;
        omfsopPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
        omfsopPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
      }
      else
      {
        omfsopPatientDetails.DisplayApproval3 = "Not Approved";
        omfsopPatientDetails.ReadOnlyApproval3 = false;
        omfsopPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
        omfsopPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
      }
      var omfsopViewModel = this._uow.Repository<OMFSOPViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayOmfsApprovalStage, 2, omfsopPatientDetails.OMFSOpId)).FirstOrDefault();
      omfsopPatientDetails.ApprovalStage = omfsopViewModel == null ? "Not Initiated" : (!(omfsopViewModel.ApprovalStage == "") ? omfsopViewModel.ApprovalStage : "Not Initiated");
      omfsopPatientDetails.ReadOnlyApproval4 = true;
      return omfsopPatientDetails;
    }

    public int UpdateOMFSOPPatient(OMFSOPViewModel model)
    {
      model.OMFSOpId = model.OMFSOpId;
      model.OMFSOpDate = Convert.ToDateTime(model.OMFSOpDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      var omfsopCasesheet = new OMFSOPCasesheet();
      Mapper.Initialize(cfg => cfg.CreateMap<OMFSOPViewModel, OMFSOPCasesheet>());
      var entity1 = Mapper.Map<OMFSOPViewModel, OMFSOPCasesheet>(model);
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.DietId = (int) model.Diet;
      this._uow.Repository<OMFSOPCasesheet>().Update(entity1, false);
      var all1 = this._uow.Repository<OMFSOPCasesheetProperties>().GetAll();
      var all2 = this._uow.Repository<OMFSOPCasesheetPropertyValues>().GetAll("OMFSOpId=" + model.OMFSOpId);
      foreach (var property1 in model.GetType().GetProperties())
      {
        var prop = property1;
        if (all1.FirstOrDefault(a => a.PropertyName == prop.Name) != null)
        {
          var name = prop.Name;
          var obj = prop.GetValue(model, null);
          if (obj != null)
          {
            var entity2 = new OMFSOPCasesheetPropertyValues();
            entity2.OMFSOpId = model.OMFSOpId;
            var property = all1.FirstOrDefault(a => a.PropertyName == prop.Name);
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              var casesheetPropertyValues = all2.FirstOrDefault(a =>
              {
                  if (a.PropId == property.PropertyId)
                      return a.OMFSOpId == model.OMFSOpId;
                  return false;
              });
              if (casesheetPropertyValues != null)
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                entity2.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<OMFSOPCasesheetPropertyValues>().Update(entity2, false);
              }
              else
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<OMFSOPCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
      }
      if (model.CreatedepartmentReferredStatus != null)
      {
        var entity2 = new ReferralStatus();
        foreach (var createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = model.PatientId;
            entity2.FromdeptId = 2;
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
          CaserecordId = model.OMFSOpId,
          DeptId = 2,
          PatientId = model.PatientId,
          ReferredTreatmentId = 6
        });
      if (model.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = model.OMFSOpId,
          ReferredTreatmentId = 6,
          DeptId = 2,
          PatientId = model.PatientId
        });
      if (model.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = model.PatientId,
          AllotId = model.studentAllotmentViewModel.AllotId,
          DoctorId = model.studentAllotmentViewModel.DoctorId,
          StudentId = model.studentAllotmentViewModel.StudentId,
          DeptId = 2,
          ReferredTreatmentId = 6
        });
      return entity1.OMFSOpId;
    }

    public StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType)
    {
      var allotmentViewModel = new StudentAllotmentViewModel();
      var DeptId = 2;
      var DeptCode = Department.OMFS.ToString();
      return this._AllotmentService.DisplayAllotment(PatientId, ReferredId, CourseType, DeptId, DeptCode);
    }

    public void OMFSOpUpdateAllotment(OMFSOPViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.OMFSOpId,
        ReferredTreatmentId = 6
      });
    }

    public void OMFSOPSavefollowUp(OMFSOPViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 2,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 6
      });
    }

    public void OMFSOpUpdateReferralStatus(OMFSOPViewModel model)
    {
      var patientId = model.PatientId;
      var num = 2;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + patientId + "," + num + "," + model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 6
      });
    }

    public List<OMFSSearchDetails> omfsIPSearchDetails(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<OMFSSearchDetails>().GetEntitiesBySql(string.Format(Queries.OMFSIPSearch, (object) From_Date, (object) To_Date, (object) 2, (object) url)).ToList();
    }

    public List<OMFSSearchDetails> omfsOPSearchDetails(string From_Date, string To_Date, string url)
    {
      var str = "(OP.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count() > 0)
        str = "(0=0)";
      return this._uow.Repository<OMFSSearchDetails>().GetEntitiesBySql(string.Format(Queries.OMFSOPSearch, (object) From_Date, (object) To_Date, (object) 2, (object) url, (object) str)).ToList();
    }

    public void ProcedureApprovalOp(OMFSOPViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
    }

    public List<StudentAllotmentViewModel> omfsOpCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.OMFSOpApproval, (object) From_Date, (object) To_Date, (object) 2, (object) url)).ToList();
    }

    public List<StudentAllotmentViewModel> omfsIpCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.OMFSIpApproval, (object) From_Date, (object) To_Date, (object) 2, (object) url)).ToList();
    }

    public OMFSOPViewModel BindOMFSOPPatientReport(int OMFSOpId)
    {
      try
      {
        var omfsopViewModel1 = new OMFSOPViewModel();
        var omfsopViewModel2 = this._uow.Repository<OMFSOPViewModel>().GetEntitiesBySql(string.Format("exec GetOMFSOPCasesheetReport {0}", OMFSOpId)).FirstOrDefault();
        omfsopViewModel2.patientInformationViewModel = new PatientInformationViewModel()
        {
          PatientName = omfsopViewModel2.PatientName,
          AgeGender = omfsopViewModel2.Age.ToString() + "/" + (Gender) omfsopViewModel2.GenderId,
          Phone = omfsopViewModel2.Phone,
          OpNo = omfsopViewModel2.OpNo,
          Area = omfsopViewModel2.Area
        };
        omfsopViewModel2.Diet = (Diet) omfsopViewModel2.DietId;
        omfsopViewModel2.DietName = Convert.ToString(omfsopViewModel2.Diet);
        omfsopViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, omfsopViewModel2.PatientId, 2));
        omfsopViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) omfsopViewModel2.PatientId, (object) 2, (object) OMFSOpId, (object) 6, (object) 20, (object) 16));
        omfsopViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) omfsopViewModel2.PatientId, (object) 2, (object) OMFSOpId, (object) 6));
        var referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = omfsopViewModel2.PatientId,
          FromdeptId = 2
        };
        omfsopViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(omfsopViewModel2.PatientId).ToList();
        omfsopViewModel2.followupViewModal = new FollowupViewModal()
        {
          PatientId = omfsopViewModel2.PatientId,
          DeptId = 2,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        omfsopViewModel2.followupList = this._FollowUpService.LoadFollowupList(omfsopViewModel2.PatientId, 2, 6);
        omfsopViewModel2.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(omfsopViewModel2.PatientId, 2, 6);
        return omfsopViewModel2;
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
