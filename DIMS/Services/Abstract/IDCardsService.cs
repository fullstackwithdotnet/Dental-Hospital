// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IDCardsService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IDCardsService : IService<OPDPatientRegistration>
  {
    int Referral(DateTime Reg, int PatientId, int FromDeptId, int ToDeptId);

    int SaveOPDPatient(DateTime Reg, int? GenderId, int? TitleId, int CategoryId, bool IsPedo = false);

    int SaveOMRPatient(DateTime Reg, int PatientId, int CaseSheetId);

    bool SaveOMFSPatient(DCardsSearchViewModal model);

    bool SavePERIOPatient(DCardsSearchViewModal model);

    bool SaveCONSPatient(DCardsSearchViewModal model);

    bool SaveORTHOPatient(DCardsSearchViewModal model);

    bool SavePEDOPatient(DCardsSearchViewModal model);

    bool SavePROSCDPatient(DCardsSearchViewModal model);

    bool SavePROSDIMPatient(DCardsSearchViewModal model);

    bool SavePROSFPDPatient(DCardsSearchViewModal model);

    bool SavePROSMFPPatient(DCardsSearchViewModal model);

    bool SavePROSRPDPatient(DCardsSearchViewModal model);

    bool SavePHDPatient(DCardsSearchViewModal model);

    bool SaveRevisitOMFSPatient(DCardsSearchViewModal model);

    bool SaveRevisitPERIOPatient(DCardsSearchViewModal model);

    bool SaveRevisitCONSPatient(DCardsSearchViewModal model);

    bool SaveRevisitORTHOPatient(DCardsSearchViewModal model);

    bool SaveRevisitPEDOPatient(DCardsSearchViewModal model);

    bool SaveRevisitPROSCDPatient(DCardsSearchViewModal model);

    bool SaveRevisitPROSDIMPatient(DCardsSearchViewModal model);

    bool SaveRevisitPROSFPDPatient(DCardsSearchViewModal model);

    bool SaveRevisitPROSMFPPatient(DCardsSearchViewModal model);

    bool SaveRevisitPROSRPDPatient(DCardsSearchViewModal model);

    bool SaveRevisitPHDPatient(DCardsSearchViewModal model);

    IsDummyEnable GetIsDummy();
  }
}
