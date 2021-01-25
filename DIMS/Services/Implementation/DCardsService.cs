// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.DCardsService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
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
  public class DCardsService : ServiceBase<OPDPatientRegistration>, IDCardsService, IService<OPDPatientRegistration>
  {
    private int revisitMonth = 3;
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;
    private IReferralStatusService _Referralservice;
    private IOPDPatientRegistrationService _OPDservice;

    public DCardsService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._Referralservice = (IReferralStatusService) new ReferralStatusService(this._uow);
      this._OPDservice = (IOPDPatientRegistrationService) new OPDPatientRegistrationService(this._uow);
    }

    public IsDummyEnable GetIsDummy()
    {
      return this._uow.Repository<IsDummyEnable>().GetEntitiesBySql(string.Format(Queries.GetIsDummyDetails)).FirstOrDefault<IsDummyEnable>();
    }

    public int Referral(DateTime Reg, int PatientId, int FromDeptId, int ToDeptId)
    {
      return this._uow.Repository<ReferralStatus>().Add(new ReferralStatus()
      {
        PatientId = PatientId,
        FromdeptId = FromDeptId,
        FromDate = new DateTime?(Convert.ToDateTime(Reg.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        ReferredReason = "To Diagnosis",
        ToDeptId = ToDeptId,
        TreatmentStatus = "0",
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false)
      }, false);
    }

    public MaxOpNo GetMaxOpNo(DateTime reg)
    {
      return this._uow.Repository<MaxOpNo>().GetEntitiesBySql(string.Format(string.Format("select 1 as Id, IsNULL(Max(OpNo)+1,dbo.GetOPNo(Getdate()))OpNo from OPDPatientRegistration WHERE CONVERT(char(10),RegDate,126) ='{0}'", (object) reg.ToString("yyyy-MM-dd")))).FirstOrDefault<MaxOpNo>();
    }

    public int SaveOPDPatient(DateTime Reg, int? GenderId, int? TitleId, int CategoryId, bool IsPedo = false)
    {
      OPDPatientRegistrationViewModel registrationViewModel = new OPDPatientRegistrationViewModel();
      DateTime now = DateTime.Now;
      string str1 = Reg.ToString("yyyy-MM-dd");
      string str2 = now.AddSeconds(220.0).ToString("HH:mm:ss.fff");
      string str3 = " ";
      string str4 = str2;
      DateTime dateTime1 = Convert.ToDateTime(str1 + str3 + str4);
      OPDPatientRegistrationViewModel source = this._uow.Repository<OPDPatientRegistrationViewModel>().GetEntitiesBySql(string.Format("exec GetOPDDcardPatientRegistration '{0}',{1},{2},{3}", (object) Reg.ToString("yyyy-MM-dd"), (object) GenderId, (object) TitleId, (object) IsPedo)).FirstOrDefault<OPDPatientRegistrationViewModel>();
      if (source != null)
      {
        DateTime dateTime2 = source.RegDate;
        if (dateTime2.ToString("yyyy-MM-dd") != Reg.ToString("yyyy-MM-dd") && Reg <= DateTime.Now)
        {
          OPDPatientRegistration patientRegistration1 = new OPDPatientRegistration();
          Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OPDPatientRegistrationViewModel, OPDPatientRegistration>()));
          OPDPatientRegistration entity1 = Mapper.Map<OPDPatientRegistrationViewModel, OPDPatientRegistration>(source);
          entity1.PatientId = 0;
          entity1.CategoryId = CategoryId;
          entity1.Age = source.DCardAge;
          OPDPatientRegistration patientRegistration2 = entity1;
          dateTime2 = DateTime.Now;
          DateTime dateTime3 = dateTime2.AddYears(-source.DCardAge);
          patientRegistration2.Dob = dateTime3;
          entity1.Phone = source.DCardPhone;
          entity1.Area = source.DCardArea;
          entity1.IsDummy = "Y";
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          MaxOpNo maxOpNo = this.GetMaxOpNo(Reg);
          entity1.OpNo = maxOpNo.OpNo;
          entity1.RegDate = dateTime1;
          int num = this._uow.Repository<OPDPatientRegistration>().Add(entity1, false);
          IEnumerable<OPDPatientRegistrationProperties> all = this._uow.Repository<OPDPatientRegistrationProperties>().GetAll("PropertyName='MandatoryDummy'");
          foreach (PropertyInfo property in source.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<OPDPatientRegistrationProperties>((Func<OPDPatientRegistrationProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) source, (object[]) null);
              if (obj != null)
              {
                OPDPatientRegistrationPropertyValues entity2 = new OPDPatientRegistrationPropertyValues();
                entity2.PatientId = num;
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
          return num;
        }
      }
      return 0;
    }

    public int SaveOPDRevisitPatient(DateTime RevisitReg, int PatientId)
    {
      OPDRevisitRegistrationViewModel registrationViewModel = new OPDRevisitRegistrationViewModel();
      DateTime now = DateTime.Now;
      string str1 = RevisitReg.ToString("yyyy-MM-dd");
      string str2 = now.AddSeconds(220.0).ToString("HH:mm:ss.fff");
      string str3 = " ";
      string str4 = str2;
      DateTime dateTime = Convert.ToDateTime(str1 + str3 + str4);
      this._uow.Repository<OPDPatientRegistration>().Get(PatientId);
      if (registrationViewModel == null || (!(registrationViewModel.RevisitDate.ToString("yyyy-MM-dd") != RevisitReg.ToString("yyyy-MM-dd")) || !(RevisitReg <= DateTime.Now)))
        return 0;
      return this._uow.Repository<OPDRevisitRegistration>().Add(new OPDRevisitRegistration()
      {
        RevisitDate = new DateTime?(dateTime),
        PatientId = PatientId,
        CreatedDate = new DateTime?(DateTime.Now),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false)
      }, false);
    }

    public string GetOMRNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'OMR'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(OMRNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from OMRCasesheet WHERE CONVERT(char(10),OMRDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public OMRViewModel GetOMRPatientDetails(int Id)
    {
      return this._uow.Repository<OMRViewModel>().GetEntitiesBySql(string.Format("exec GetOMRCasesheet {0}", (object) Id)).FirstOrDefault<OMRViewModel>();
    }

    public int SaveOMRPatient(DateTime Reg, int PatientId, int CaseSheetId)
    {
      if (PatientId > 0 && CaseSheetId > 0)
      {
        Reg.ToString("yyyy-MM-dd");
        int FromDeptId = 15;
        int ToDeptId = 1;
        int num1 = this.Referral(Reg, PatientId, FromDeptId, ToDeptId);
        OMRViewModel omrPatientDetails = this.GetOMRPatientDetails(CaseSheetId);
        if (num1 != 0 && omrPatientDetails != null)
        {
          int num2 = 1;
          int omrId = omrPatientDetails.OMRId;
          int num3 = 0;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) omrId + " and ReferredTreatmentId = " + (object) num3).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num3).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num4 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          omrPatientDetails.TreatmentReferredId = (long) num1;
          omrPatientDetails.MandatoryDummy = "Y";
          OMRCasesheet omrCasesheet = new OMRCasesheet();
          OMRCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OMRViewModel, OMRCasesheet>())).CreateMapper().Map<OMRViewModel, OMRCasesheet>(omrPatientDetails);
          entity2.PatientId = PatientId;
          entity2.OMRId = 0;
          entity2.OMRDate = Reg;
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.OMRNo = this.GetOMRNo(Reg);
          int num5 = this._uow.Repository<OMRCasesheet>().Add(entity2, false);
          IEnumerable<OMRCasesheetProperties> all = this._uow.Repository<OMRCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in omrPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<OMRCasesheetProperties>((Func<OMRCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) omrPatientDetails, (object[]) null);
              if (obj != null)
              {
                OMRCasesheetPropertyValues entity3 = new OMRCasesheetPropertyValues();
                entity3.OMRId = num5;
                OMRCasesheetProperties casesheetProperties = all.FirstOrDefault<OMRCasesheetProperties>((Func<OMRCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<OMRCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = omrPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 0,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(Reg);
          entity4.CaserecordId = num5;
          entity4.ReferredTreatmentId = 0;
          entity4.AllotId = (long) num4;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
          return num5;
        }
      }
      return 0;
    }

    public string GetOMFSOPNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'OMFS'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(OMFSOpNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from OMFSOPCasesheet WHERE CONVERT(char(10),OMFSOpDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public OMFSOPViewModel GetOMFSOPPatientDetails(int Id)
    {
      return this._uow.Repository<OMFSOPViewModel>().GetEntitiesBySql(string.Format("exec GetOMFSOPCasesheet {0}", (object) Id)).FirstOrDefault<OMFSOPViewModel>();
    }

    public bool SaveOMFSPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardOMFSSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, false);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 2;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        OMFSOPViewModel omfsopPatientDetails = this.GetOMFSOPPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && omfsopPatientDetails != null)
        {
          int num2 = 2;
          int omfsOpId = omfsopPatientDetails.OMFSOpId;
          int num3 = 6;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) omfsOpId + " and ReferredTreatmentId = " + (object) num3).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num3).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num4 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          omfsopPatientDetails.TreatmentReferredId = (long) num1;
          omfsopPatientDetails.MandatoryDummy = "Y";
          OMFSOPCasesheet omfsopCasesheet = new OMFSOPCasesheet();
          OMFSOPCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OMFSOPViewModel, OMFSOPCasesheet>())).CreateMapper().Map<OMFSOPViewModel, OMFSOPCasesheet>(omfsopPatientDetails);
          entity2.PatientId = PatientId;
          entity2.OMFSOpId = 0;
          entity2.OMFSOpDate = DCmodel.Reg;
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.OMFSOpNo = this.GetOMFSOPNo(DCmodel.Reg);
          int num5 = this._uow.Repository<OMFSOPCasesheet>().Add(entity2, false);
          IEnumerable<OMFSOPCasesheetProperties> all = this._uow.Repository<OMFSOPCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in omfsopPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<OMFSOPCasesheetProperties>((Func<OMFSOPCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) omfsopPatientDetails, (object[]) null);
              if (obj != null)
              {
                OMFSOPCasesheetPropertyValues entity3 = new OMFSOPCasesheetPropertyValues();
                entity3.OMFSOpId = num5;
                OMFSOPCasesheetProperties casesheetProperties = all.FirstOrDefault<OMFSOPCasesheetProperties>((Func<OMFSOPCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<OMFSOPCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = omfsopPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 6,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num5;
          entity4.ReferredTreatmentId = 6;
          entity4.AllotId = (long) num4;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public string GetPERIOOPNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'PERIO'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(PerioNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from PERIOCasesheet WHERE CONVERT(char(10),PerioDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public PERIOViewModel GetPERIOPatientDetails(int id)
    {
      return this._uow.Repository<PERIOViewModel>().GetEntitiesBySql(string.Format("exec GetPERIOCasesheet {0}", (object) id)).FirstOrDefault<PERIOViewModel>();
    }

    public bool SavePERIOPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardPERIOSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, false);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 3;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        PERIOViewModel perioPatientDetails = this.GetPERIOPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && perioPatientDetails != null)
        {
          int num2 = 3;
          int perioId = perioPatientDetails.PerioId;
          int num3 = 0;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) perioId + " and ReferredTreatmentId = " + (object) num3).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num3).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num4 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          perioPatientDetails.TreatmentReferredId = (long) num1;
          perioPatientDetails.MandatoryDummy = "Y";
          PERIOCasesheet perioCasesheet = new PERIOCasesheet();
          PERIOCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PERIOViewModel, PERIOCasesheet>())).CreateMapper().Map<PERIOViewModel, PERIOCasesheet>(perioPatientDetails);
          entity2.PatientId = PatientId;
          entity2.PerioId = 0;
          entity2.PerioDate = DCmodel.Reg;
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.PerioNo = this.GetPERIOOPNo(DCmodel.Reg);
          int num5 = this._uow.Repository<PERIOCasesheet>().Add(entity2, false);
          IEnumerable<PERIOCasesheetProperties> all = this._uow.Repository<PERIOCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in perioPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<PERIOCasesheetProperties>((Func<PERIOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) perioPatientDetails, (object[]) null);
              if (obj != null)
              {
                PERIOCasesheetPropertyValues entity3 = new PERIOCasesheetPropertyValues();
                entity3.PerioId = num5;
                PERIOCasesheetProperties casesheetProperties = all.FirstOrDefault<PERIOCasesheetProperties>((Func<PERIOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<PERIOCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = perioPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 0,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num5;
          entity4.ReferredTreatmentId = 0;
          entity4.AllotId = (long) num4;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public string GetCONSOPNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'CONS'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(CONSNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from CONSCasesheet WHERE CONVERT(char(10),ConservativeDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public CONSViewModel GetCONSPatientDetails(int id)
    {
      return this._uow.Repository<CONSViewModel>().GetEntitiesBySql(string.Format("exec GetCONSCasesheet {0}", (object) id)).FirstOrDefault<CONSViewModel>();
    }

    public bool SaveCONSPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardCONSSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, false);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 4;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        CONSViewModel consPatientDetails = this.GetCONSPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && consPatientDetails != null)
        {
          int num2 = 4;
          int conservativeId = consPatientDetails.ConservativeId;
          int num3 = 0;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) conservativeId + " and ReferredTreatmentId = " + (object) num3).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num3).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num4 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          consPatientDetails.TreatmentReferredId = (long) num1;
          consPatientDetails.MandatoryDummy = "Y";
          CONSCasesheet consCasesheet = new CONSCasesheet();
          CONSCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<CONSViewModel, CONSCasesheet>())).CreateMapper().Map<CONSViewModel, CONSCasesheet>(consPatientDetails);
          entity2.PatientId = PatientId;
          entity2.ConservativeId = 0;
          entity2.ConservativeDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.CONSNo = this.GetCONSOPNo(DCmodel.Reg);
          int num5 = this._uow.Repository<CONSCasesheet>().Add(entity2, false);
          IEnumerable<CONSCasesheetProperties> all = this._uow.Repository<CONSCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in consPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<CONSCasesheetProperties>((Func<CONSCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) consPatientDetails, (object[]) null);
              if (obj != null)
              {
                CONSCasesheetPropertyValues entity3 = new CONSCasesheetPropertyValues();
                entity3.ConservativeId = num5;
                CONSCasesheetProperties casesheetProperties = all.FirstOrDefault<CONSCasesheetProperties>((Func<CONSCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<CONSCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = consPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 0,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num5;
          entity4.ReferredTreatmentId = 0;
          entity4.AllotId = (long) num4;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public string GetORTHOOPNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'ORTHO'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(OrthoNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from ORTHOCasesheet WHERE CONVERT(char(10),OrthoDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public OrthoViewModal GetORTHOPatientDetails(int id)
    {
      return this._uow.Repository<OrthoViewModal>().GetEntitiesBySql(string.Format("exec GetORTHOCasesheet {0}", (object) id)).FirstOrDefault<OrthoViewModal>();
    }

    public bool SaveORTHOPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardORTHOSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, false);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 5;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        OrthoViewModal orthoPatientDetails = this.GetORTHOPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && orthoPatientDetails != null)
        {
          int num2 = 5;
          int orthoId = orthoPatientDetails.OrthoId;
          int num3 = 0;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) orthoId + " and ReferredTreatmentId = " + (object) num3).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num3).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num4 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          orthoPatientDetails.TreatmentReferredId = (long) num1;
          orthoPatientDetails.MandatoryDummy = "Y";
          ORTHOCasesheet orthoCasesheet = new ORTHOCasesheet();
          ORTHOCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoViewModal, ORTHOCasesheet>())).CreateMapper().Map<OrthoViewModal, ORTHOCasesheet>(orthoPatientDetails);
          entity2.PatientId = PatientId;
          entity2.OrthoId = 0;
          entity2.OrthoDate = DCmodel.Reg;
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.OrthoNo = this.GetORTHOOPNo(DCmodel.Reg);
          int num5 = this._uow.Repository<ORTHOCasesheet>().Add(entity2, false);
          IEnumerable<ORTHOCasesheetProperties> all = this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in orthoPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) orthoPatientDetails, (object[]) null);
              if (obj != null)
              {
                ORTHOCasesheetPropertyValues entity3 = new ORTHOCasesheetPropertyValues();
                entity3.OrthoId = num5;
                ORTHOCasesheetProperties casesheetProperties = all.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<ORTHOCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = orthoPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 0,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num5;
          entity4.ReferredTreatmentId = 0;
          entity4.AllotId = (long) num4;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public string GetPEDONo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'PEDO'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(PEDONo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from PEDOCasesheet WHERE CONVERT(char(10),PEDODate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public PEDOViewModel GetPEDOPatientDetails(int id)
    {
      return this._uow.Repository<PEDOViewModel>().GetEntitiesBySql(string.Format("exec GetPEDOCasesheet {0}", (object) id)).FirstOrDefault<PEDOViewModel>();
    }

    public bool SavePEDOPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardPEDOSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, true);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 6;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        PEDOViewModel pedoPatientDetails = this.GetPEDOPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && pedoPatientDetails != null)
        {
          int num2 = 6;
          int pedoId = pedoPatientDetails.PEDOId;
          int num3 = 0;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) pedoId + " and ReferredTreatmentId = " + (object) num3).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num3).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num4 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          pedoPatientDetails.TreatmentReferredId = (long) num1;
          pedoPatientDetails.MandatoryDummy = "Y";
          PEDOCasesheet pedoCasesheet = new PEDOCasesheet();
          PEDOCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PEDOViewModel, PEDOCasesheet>())).CreateMapper().Map<PEDOViewModel, PEDOCasesheet>(pedoPatientDetails);
          entity2.PatientId = PatientId;
          entity2.PEDOId = 0;
          entity2.PEDODate = DCmodel.Reg;
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.PEDONo = this.GetPEDONo(DCmodel.Reg);
          int num5 = this._uow.Repository<PEDOCasesheet>().Add(entity2, false);
          IEnumerable<PEDOCasesheetProperties> all = this._uow.Repository<PEDOCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in pedoPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<PEDOCasesheetProperties>((Func<PEDOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) pedoPatientDetails, (object[]) null);
              if (obj != null)
              {
                PEDOCasesheetPropertyValues entity3 = new PEDOCasesheetPropertyValues();
                entity3.PEDOId = num5;
                PEDOCasesheetProperties casesheetProperties = all.FirstOrDefault<PEDOCasesheetProperties>((Func<PEDOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<PEDOCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = pedoPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 0,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num5;
          entity4.ReferredTreatmentId = 0;
          entity4.AllotId = (long) num4;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public string GetPROSCDNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format("select 1 as Id, 'CD'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ProsthoCDNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from PROSCDCasesheet WHERE CONVERT(char(10),ProsthoCDDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetPROSDIMNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'DIM'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ProsthoDIMNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from PROSDIMCasesheet WHERE CONVERT(char(10),ProsthoDIMDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetPROSFPDNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'FPD'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ProsthoRPDNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo  from PROSRPDCasesheet WHERE CONVERT(char(10),ProsthoRPDDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetPROSMFPNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'MFD'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ProsthoMFPNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from PROSMFPCasesheet WHERE CONVERT(char(10),ProsthoMFPDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetPROSRPDNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'RPD'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ProsthoRPDNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from PROSRPDCasesheet WHERE CONVERT(char(10),ProsthoRPDDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public PROSCDViewModel GetPROSCDPatientDetails(int id)
    {
      return this._uow.Repository<PROSCDViewModel>().GetEntitiesBySql(string.Format("exec GetPROSCDCasesheet {0}", (object) id)).FirstOrDefault<PROSCDViewModel>();
    }

    public PROSDIMViewModel GetPROSDIMPatientDetails(int id)
    {
      return this._uow.Repository<PROSDIMViewModel>().GetEntitiesBySql(string.Format("exec GetPROSDIMCasesheet {0}", (object) id)).FirstOrDefault<PROSDIMViewModel>();
    }

    public PROSFPDViewModel GetPROSFPDPatientDetails(int id)
    {
      return this._uow.Repository<PROSFPDViewModel>().GetEntitiesBySql(string.Format("exec GetPROSFPDCasesheet {0}", (object) id)).FirstOrDefault<PROSFPDViewModel>();
    }

    public PROSMFPViewModel GetPROSMFPPatientDetails(int id)
    {
      return this._uow.Repository<PROSMFPViewModel>().GetEntitiesBySql(string.Format("exec GetPROSMFPCasesheet {0}", (object) id)).FirstOrDefault<PROSMFPViewModel>();
    }

    public PROSRPDViewModel GetPROSRPDPatientDetails(int id)
    {
      return this._uow.Repository<PROSRPDViewModel>().GetEntitiesBySql(string.Format("exec GetPROSRPDCasesheet {0}", (object) id)).FirstOrDefault<PROSRPDViewModel>();
    }

    public bool SavePROSCDPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardPROSTHOCDSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, false);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 7;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        PROSCDViewModel proscdPatientDetails = this.GetPROSCDPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && proscdPatientDetails != null)
        {
          int num2 = 7;
          int prosthoCdId = proscdPatientDetails.ProsthoCDId;
          int prosTreatmentId = DCmodel.PROSTreatmentId;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) prosthoCdId + " and ReferredTreatmentId = " + (object) prosTreatmentId).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) prosTreatmentId).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num3 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          proscdPatientDetails.TreatmentReferredId = (long) num1;
          proscdPatientDetails.MandatoryDummy = "Y";
          PROSCDCasesheet proscdCasesheet = new PROSCDCasesheet();
          PROSCDCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSCDViewModel, PROSCDCasesheet>())).CreateMapper().Map<PROSCDViewModel, PROSCDCasesheet>(proscdPatientDetails);
          entity2.PatientId = PatientId;
          entity2.ProsthoCDId = 0;
          entity2.ProsthoCDDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.ProsthoCDNo = this.GetPROSCDNo(DCmodel.Reg);
          int num4 = this._uow.Repository<PROSCDCasesheet>().Add(entity2, false);
          IEnumerable<PROSCDCasesheetProperties> all = this._uow.Repository<PROSCDCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in proscdPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<PROSCDCasesheetProperties>((Func<PROSCDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) proscdPatientDetails, (object[]) null);
              if (obj != null)
              {
                PROSCDCasesheetPropertyValues entity3 = new PROSCDCasesheetPropertyValues();
                entity3.ProsthoCDId = num4;
                PROSCDCasesheetProperties casesheetProperties = all.FirstOrDefault<PROSCDCasesheetProperties>((Func<PROSCDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = new int?(casesheetProperties.PropertyId);
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<PROSCDCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = proscdPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 1,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num4;
          entity4.ReferredTreatmentId = 1;
          entity4.AllotId = (long) num3;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public bool SavePROSDIMPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardPROSTHODIMSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, false);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 7;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        PROSDIMViewModel prosdimPatientDetails = this.GetPROSDIMPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && prosdimPatientDetails != null)
        {
          int num2 = 7;
          int prosthoDimId = prosdimPatientDetails.ProsthoDIMId;
          int prosTreatmentId = DCmodel.PROSTreatmentId;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) prosthoDimId + " and ReferredTreatmentId = " + (object) prosTreatmentId).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) prosTreatmentId).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num3 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          prosdimPatientDetails.TreatmentReferredId = (long) num1;
          prosdimPatientDetails.MandatoryDummy = "Y";
          PROSDIMCasesheet prosdimCasesheet = new PROSDIMCasesheet();
          PROSDIMCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSDIMViewModel, PROSDIMCasesheet>())).CreateMapper().Map<PROSDIMViewModel, PROSDIMCasesheet>(prosdimPatientDetails);
          entity2.PatientId = PatientId;
          entity2.ProsthoDIMId = 0;
          entity2.ProsthoDIMDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.ProsthoDIMNo = this.GetPROSDIMNo(DCmodel.Reg);
          int num4 = this._uow.Repository<PROSDIMCasesheet>().Add(entity2, false);
          IEnumerable<PROSDIMCasesheetProperties> all = this._uow.Repository<PROSDIMCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in prosdimPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<PROSDIMCasesheetProperties>((Func<PROSDIMCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) prosdimPatientDetails, (object[]) null);
              if (obj != null)
              {
                PROSDIMCasesheetPropertyValues entity3 = new PROSDIMCasesheetPropertyValues();
                entity3.ProsthoDIMId = num4;
                PROSDIMCasesheetProperties casesheetProperties = all.FirstOrDefault<PROSDIMCasesheetProperties>((Func<PROSDIMCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<PROSDIMCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = prosdimPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 5,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num4;
          entity4.ReferredTreatmentId = 5;
          entity4.AllotId = (long) num3;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public bool SavePROSFPDPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardPROSTHOFPDSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, false);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 7;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        PROSFPDViewModel prosfpdPatientDetails = this.GetPROSFPDPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && prosfpdPatientDetails != null)
        {
          int num2 = 7;
          int prosthoFpdId = prosfpdPatientDetails.ProsthoFPDId;
          int prosTreatmentId = DCmodel.PROSTreatmentId;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) prosthoFpdId + " and ReferredTreatmentId = " + (object) prosTreatmentId).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) prosTreatmentId).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num3 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          prosfpdPatientDetails.TreatmentReferredId = (long) num1;
          prosfpdPatientDetails.MandatoryDummy = "Y";
          PROSFPDCasesheet prosfpdCasesheet = new PROSFPDCasesheet();
          PROSFPDCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSFPDViewModel, PROSFPDCasesheet>())).CreateMapper().Map<PROSFPDViewModel, PROSFPDCasesheet>(prosfpdPatientDetails);
          entity2.PatientId = PatientId;
          entity2.ProsthoFPDId = 0;
          entity2.ProsthoFPDDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.ProsthoFPDNo = this.GetPROSFPDNo(DCmodel.Reg);
          int num4 = this._uow.Repository<PROSFPDCasesheet>().Add(entity2, false);
          IEnumerable<PROSFPDCasesheetProperties> all = this._uow.Repository<PROSFPDCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in prosfpdPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<PROSFPDCasesheetProperties>((Func<PROSFPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) prosfpdPatientDetails, (object[]) null);
              if (obj != null)
              {
                PROSFPDCasesheetPropertyValues entity3 = new PROSFPDCasesheetPropertyValues();
                entity3.ProsthoFPDId = num4;
                PROSFPDCasesheetProperties casesheetProperties = all.FirstOrDefault<PROSFPDCasesheetProperties>((Func<PROSFPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<PROSFPDCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = prosfpdPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 3,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num4;
          entity4.ReferredTreatmentId = 3;
          entity4.AllotId = (long) num3;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public bool SavePROSMFPPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardPROSTHOMFDSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, false);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 7;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        PROSMFPViewModel prosmfpPatientDetails = this.GetPROSMFPPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && prosmfpPatientDetails != null)
        {
          int num2 = 7;
          int prosthoMfpId = prosmfpPatientDetails.ProsthoMFPId;
          int prosTreatmentId = DCmodel.PROSTreatmentId;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) prosthoMfpId + " and ReferredTreatmentId = " + (object) prosTreatmentId).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) prosTreatmentId).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num3 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          prosmfpPatientDetails.TreatmentReferredId = (long) num1;
          prosmfpPatientDetails.MandatoryDummy = "Y";
          PROSMFPCasesheet prosmfpCasesheet = new PROSMFPCasesheet();
          PROSMFPCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSMFPViewModel, PROSMFPCasesheet>())).CreateMapper().Map<PROSMFPViewModel, PROSMFPCasesheet>(prosmfpPatientDetails);
          entity2.PatientId = PatientId;
          entity2.ProsthoMFPId = 0;
          entity2.ProsthoMFPDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.ProsthoMFPNo = this.GetPROSMFPNo(DCmodel.Reg);
          int num4 = this._uow.Repository<PROSMFPCasesheet>().Add(entity2, false);
          IEnumerable<PROSMFPCasesheetProperties> all = this._uow.Repository<PROSMFPCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in prosmfpPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<PROSMFPCasesheetProperties>((Func<PROSMFPCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) prosmfpPatientDetails, (object[]) null);
              if (obj != null)
              {
                PROSMFPCasesheetPropertyValues entity3 = new PROSMFPCasesheetPropertyValues();
                entity3.ProsthoMFPId = num4;
                PROSMFPCasesheetProperties casesheetProperties = all.FirstOrDefault<PROSMFPCasesheetProperties>((Func<PROSMFPCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<PROSMFPCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = prosmfpPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 4,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num4;
          entity4.ReferredTreatmentId = 4;
          entity4.AllotId = (long) num3;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public bool SavePROSRPDPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardPROSTHORPDSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, false);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 7;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        PROSRPDViewModel prosrpdPatientDetails = this.GetPROSRPDPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && prosrpdPatientDetails != null)
        {
          int num2 = 7;
          int prosthoRpdId = prosrpdPatientDetails.ProsthoRPDId;
          int prosTreatmentId = DCmodel.PROSTreatmentId;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) prosthoRpdId + " and ReferredTreatmentId = " + (object) prosTreatmentId).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) prosTreatmentId).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num3 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          prosrpdPatientDetails.TreatmentReferredId = (long) num1;
          prosrpdPatientDetails.MandatoryDummy = "Y";
          PROSRPDCasesheet prosrpdCasesheet = new PROSRPDCasesheet();
          PROSRPDCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSRPDViewModel, PROSRPDCasesheet>())).CreateMapper().Map<PROSRPDViewModel, PROSRPDCasesheet>(prosrpdPatientDetails);
          entity2.PatientId = PatientId;
          entity2.ProsthoRPDId = 0;
          entity2.ProsthoRPDDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DCmodel.Reg);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.ProsthoRPDNo = this.GetPROSRPDNo(DCmodel.Reg);
          int num4 = this._uow.Repository<PROSRPDCasesheet>().Add(entity2, false);
          IEnumerable<PROSRPDCasesheetProperties> all = this._uow.Repository<PROSRPDCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in prosrpdPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<PROSRPDCasesheetProperties>((Func<PROSRPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) prosrpdPatientDetails, (object[]) null);
              if (obj != null)
              {
                PROSRPDCasesheetPropertyValues entity3 = new PROSRPDCasesheetPropertyValues();
                entity3.ProsthoRPDId = num4;
                PROSRPDCasesheetProperties casesheetProperties = all.FirstOrDefault<PROSRPDCasesheetProperties>((Func<PROSRPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<PROSRPDCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = prosrpdPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 2,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num4;
          entity4.ReferredTreatmentId = 2;
          entity4.AllotId = (long) num3;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public string GetPHDNo(DateTime Now)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'PHD'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(PHDNo,9))+1,dbo.GetCasesheetNoDummy('" + Now.ToString("yyyy-MM-dd") + "')))CasesheetNo from PHDCasesheet WHERE CONVERT(char(10),PHDDate,126) ='{0}'", (object) Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public PHDViewModel GetPHDPatientDetails(int id)
    {
      return this._uow.Repository<PHDViewModel>().GetEntitiesBySql(string.Format("exec GetPHDCasesheet {0}", (object) id)).FirstOrDefault<PHDViewModel>();
    }

    public bool SavePHDPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardPHDSearch, (object) DCmodel.DCardCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"))).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int? titleId = dcardSearchDetails.TitleId;
        int? genderId = dcardSearchDetails.GenderId;
        int omrId = dcardSearchDetails.OMRId;
        int categoryId = DCmodel.CategoryId;
        int PatientId = this.SaveOPDPatient(DCmodel.Reg, genderId, titleId, categoryId, false);
        this.SaveOMRPatient(DCmodel.Reg, PatientId, omrId);
        int FromDeptId = 1;
        int ToDeptId = 8;
        int num1 = this.Referral(DCmodel.Reg, PatientId, FromDeptId, ToDeptId);
        PHDViewModel phdPatientDetails = this.GetPHDPatientDetails(dcardSearchDetails.CaseSheetId);
        if (num1 != 0 && phdPatientDetails != null)
        {
          int num2 = 8;
          int phdId = phdPatientDetails.PHDId;
          int num3 = 0;
          StudentAllotment studentAllotment = new StudentAllotment();
          StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) phdId + " and ReferredTreatmentId = " + (object) num3).LastOrDefault<StudentAllotment>();
          if (entity1 == null)
            entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num3).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
          entity1.AllotId = 0L;
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity1.PatientId = PatientId;
          entity1.ReferredId = (long) num1;
          entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
          entity1.DoctorApproval = "Y";
          entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
          entity1.CreatedDate = new DateTime?(DateTime.Now);
          entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          int num4 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
          phdPatientDetails.TreatmentReferredId = (long) num1;
          phdPatientDetails.MandatoryDummy = "Y";
          OMRCasesheet omrCasesheet = new OMRCasesheet();
          if (this._uow.Repository<OMRCasesheet>().GetAll("PatientId=" + (object) phdPatientDetails.PatientId).OrderByDescending<OMRCasesheet, int>((Func<OMRCasesheet, int>) (x => x.OMRId)).FirstOrDefault<OMRCasesheet>() == null)
          {
            int num5 = this._uow.Repository<OMRCasesheet>().Add(new OMRCasesheet()
            {
              ChiefComplaint = phdPatientDetails.ChiefComplaint,
              PatientId = PatientId,
              OMRId = 0,
              OMRDate = DCmodel.OMRReg,
              CreatedDate = new DateTime?(DateTime.Now),
              LastVisitedDate = new DateTime?(DateTime.Now),
              CreatedSystem = this._Dropdownservice.GetIPAddress(false),
              OMRNo = this.GetOMRNo(DCmodel.Reg)
            }, false);
            this._uow.Repository<OMRCasesheetPropertyValues>().Add(new OMRCasesheetPropertyValues()
            {
              PropId = 1,
              PropValues = phdPatientDetails.MandatoryDummy = "Y",
              OMRId = num5
            }, false);
          }
          PHDCasesheet phdCasesheet = new PHDCasesheet();
          PHDCasesheet entity2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PHDViewModel, PHDCasesheet>())).CreateMapper().Map<PHDViewModel, PHDCasesheet>(phdPatientDetails);
          entity2.PatientId = PatientId;
          entity2.PHDId = 0;
          entity2.PHDDate = DCmodel.Reg;
          entity2.CreatedDate = new DateTime?(DateTime.Now);
          entity2.LastVisitedDate = new DateTime?(DateTime.Now);
          entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          entity2.PHDNo = this.GetPHDNo(DCmodel.Reg);
          int num6 = this._uow.Repository<PHDCasesheet>().Add(entity2, false);
          IEnumerable<PHDCasesheetProperties> all = this._uow.Repository<PHDCasesheetProperties>().GetAll();
          foreach (PropertyInfo property in phdPatientDetails.GetType().GetProperties())
          {
            PropertyInfo prop = property;
            if (all.FirstOrDefault<PHDCasesheetProperties>((Func<PHDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
            {
              string name = prop.Name;
              object obj = prop.GetValue((object) phdPatientDetails, (object[]) null);
              if (obj != null)
              {
                PHDCasesheetPropertyValues entity3 = new PHDCasesheetPropertyValues();
                entity3.PHDId = num6;
                PHDCasesheetProperties casesheetProperties = all.FirstOrDefault<PHDCasesheetProperties>((Func<PHDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
                if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
                {
                  entity3.PropId = casesheetProperties.PropertyId;
                  entity3.PropValues = obj.ToString();
                  this._uow.Repository<PHDCasesheetPropertyValues>().Add(entity3, false);
                }
              }
            }
          }
          this._Referralservice.Update(new ReferralStatus()
          {
            VisitType = "N",
            ReferredId = phdPatientDetails.TreatmentReferredId,
            ReferredTreatmentId = 0,
            TreatmentStatus = "1",
            TreatmentDate = new DateTime?(DCmodel.Reg)
          });
          StudentAllotment entity4 = new StudentAllotment();
          entity1.AllotDate = new DateTime?(DCmodel.Reg);
          entity4.CaserecordId = num6;
          entity4.ReferredTreatmentId = 0;
          entity4.AllotId = (long) num4;
          this._uow.Repository<StudentAllotment>().Update(entity4, false);
        }
        flag = true;
      }
      return flag;
    }

    public bool SaveRevisitOMFSPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitOMFSSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 2;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<OMFSOPCasesheet>().GetEntitiesBySql(string.Format("UPDATE OMFSOPCasesheet set LastVisitedDate='{0}' where OMFSOpId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 2;
            int num3 = caseSheetId;
            int num4 = 6;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 6,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 6;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }

    public bool SaveRevisitPERIOPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitPERIOSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 3;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<PERIOCasesheet>().GetEntitiesBySql(string.Format("UPDATE PERIOCasesheet set LastVisitedDate='{0}' where PerioId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 3;
            int num3 = caseSheetId;
            int num4 = 0;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 0,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 0;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }

    public bool SaveRevisitCONSPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitCONSSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 4;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<CONSCasesheet>().GetEntitiesBySql(string.Format("UPDATE CONSCasesheet set LastVisitedDate='{0}' where ConservativeId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 4;
            int num3 = caseSheetId;
            int num4 = 0;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 0,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 0;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }

    public bool SaveRevisitORTHOPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitORTHOSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 5;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<ORTHOCasesheet>().GetEntitiesBySql(string.Format("UPDATE ORTHOCasesheet set LastVisitedDate='{0}' where OrthoId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 5;
            int num3 = caseSheetId;
            int num4 = 0;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 0,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 0;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }

    public bool SaveRevisitPEDOPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitPEDOSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 6;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<PEDOCasesheet>().GetEntitiesBySql(string.Format("UPDATE PEDOCasesheet set LastVisitedDate='{0}' where PEDOId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 6;
            int num3 = caseSheetId;
            int num4 = 0;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 0,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 0;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }

    public bool SaveRevisitPROSCDPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitPROSCDSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 7;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<PROSCDCasesheet>().GetEntitiesBySql(string.Format("UPDATE PROSCDCasesheet set LastVisitedDate='{0}' where ProsthoCDId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 7;
            int num3 = caseSheetId;
            int num4 = 1;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 1,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 1;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }

    public bool SaveRevisitPROSDIMPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitPROSDIMSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 7;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<PROSDIMCasesheet>().GetEntitiesBySql(string.Format("UPDATE PROSDIMCasesheet set LastVisitedDate='{0}' where ProsthoDIMId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 7;
            int num3 = caseSheetId;
            int num4 = 5;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 5,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 5;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }

    public bool SaveRevisitPROSFPDPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitPROSFPDSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 7;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<PROSFPDCasesheet>().GetEntitiesBySql(string.Format("UPDATE PROSFPDCasesheet set LastVisitedDate='{0}' where ProsthoFPDId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 7;
            int num3 = caseSheetId;
            int num4 = 3;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 3,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 3;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }

    public bool SaveRevisitPROSMFPPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitPROSMFPSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 7;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<PROSMFPCasesheet>().GetEntitiesBySql(string.Format("UPDATE PROSMFPCasesheet set LastVisitedDate='{0}' where ProsthoMFPId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 7;
            int num3 = caseSheetId;
            int num4 = 4;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 4,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 4;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }

    public bool SaveRevisitPROSRPDPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitPROSRPDSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 7;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<PROSRPDCasesheet>().GetEntitiesBySql(string.Format("UPDATE PROSRPDCasesheet set LastVisitedDate='{0}' where ProsthoRPDId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 7;
            int num3 = caseSheetId;
            int num4 = 2;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 2,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 2;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }

    public bool SaveRevisitPHDPatient(DCardsSearchViewModal DCmodel)
    {
      bool flag = false;
      foreach (DCardSearchDetails dcardSearchDetails in this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardRevisitPHDSearch, (object) DCmodel.DCardRevisitCount, (object) DCmodel.Reg.ToString("yyyy-MM-dd"), (object) this.revisitMonth)).ToList<DCardSearchDetails>())
      {
        int caseSheetId = dcardSearchDetails.CaseSheetId;
        int patientId = dcardSearchDetails.PatientId;
        if (this.SaveOPDRevisitPatient(DCmodel.Reg, patientId) > 0)
        {
          int FromDeptId = 14;
          int ToDeptId = 8;
          int num1 = this.Referral(DCmodel.Reg, patientId, FromDeptId, ToDeptId);
          if (num1 != 0 && caseSheetId != 0)
          {
            this._uow.Repository<PHDCasesheet>().GetEntitiesBySql(string.Format("UPDATE PHDCasesheet set LastVisitedDate='{0}' where PHDId ={1} ", (object) DCmodel.Reg, (object) caseSheetId));
            int num2 = 8;
            int num3 = caseSheetId;
            int num4 = 0;
            StudentAllotment studentAllotment = new StudentAllotment();
            StudentAllotment entity1 = this._uow.Repository<StudentAllotment>().GetAll("DeptId =" + (object) num2 + " and CaseRecordId = " + (object) num3 + " and ReferredTreatmentId = " + (object) num4).LastOrDefault<StudentAllotment>();
            if (entity1 == null)
              entity1 = this._uow.Repository<StudentAllotment>().GetAll("ProcedureNotesDate IS NOT NULL and DeptId =" + (object) num2 + " and ReferredTreatmentId = " + (object) num4).OrderByDescending<StudentAllotment, DateTime?>((Func<StudentAllotment, DateTime?>) (x => x.AllotDate)).LastOrDefault<StudentAllotment>();
            entity1.AllotId = 0L;
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity1.PatientId = patientId;
            entity1.ReferredId = (long) num1;
            entity1.ProcedureNotesDate = new DateTime?(DCmodel.Reg);
            entity1.DoctorApproval = "Y";
            entity1.DoctorApprovalDate = new DateTime?(DCmodel.Reg);
            entity1.CreatedDate = new DateTime?(DateTime.Now);
            entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            int num5 = this._uow.Repository<StudentAllotment>().Add(entity1, false);
            this._Referralservice.Update(new ReferralStatus()
            {
              VisitType = "R",
              ReferredId = (long) num1,
              ReferredTreatmentId = 0,
              TreatmentStatus = "1",
              TreatmentDate = new DateTime?(DCmodel.Reg)
            });
            StudentAllotment entity2 = new StudentAllotment();
            entity1.AllotDate = new DateTime?(DCmodel.Reg);
            entity2.CaserecordId = caseSheetId;
            entity2.ReferredTreatmentId = 0;
            entity2.AllotId = (long) num5;
            this._uow.Repository<StudentAllotment>().Update(entity2, false);
          }
          flag = true;
        }
      }
      return flag;
    }
  }
}
