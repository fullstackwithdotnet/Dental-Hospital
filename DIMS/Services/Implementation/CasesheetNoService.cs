// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.CasesheetNoService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using Repository.Base;
using Repository.Core;
using System;
using System.Linq;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class CasesheetNoService : ServiceBase<GetCasesheetNo>, ICasesheetNoService
  {
    private IUnitOfWork _uow;

    public CasesheetNoService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public string GetOMRNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'OMR'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(OMRNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from OMRCasesheet WHERE CONVERT(char(10),OMRDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetPerioNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'PERIO'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(PerioNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from PERIOCasesheet WHERE CONVERT(char(10),PerioDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetPEDONo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'PEDO'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(PEDONo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from PEDOCasesheet WHERE CONVERT(char(10),PEDODate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetPHDNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'PHD'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(PHDNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from PHDCasesheet WHERE CONVERT(char(10),PHDDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetOrthoNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'ORTHO'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(OrthoNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from ORTHOCasesheet WHERE CONVERT(char(10),OrthoDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetCONSNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'CONS'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(CONSNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from CONSCasesheet WHERE CONVERT(char(10),ConservativeDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetProsthoCDNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format("select 1 as Id, 'CD'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ProsthoCDNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from PROSCDCasesheet WHERE CONVERT(char(10),ProsthoCDDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetProsthoRPDNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'RPD'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ProsthoRPDNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from PROSRPDCasesheet WHERE CONVERT(char(10),ProsthoRPDDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetProsthoDIMNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format("select 1 as Id, 'DIM'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ProsthoDIMNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from PROSDIMCasesheet WHERE CONVERT(char(10),ProsthoDIMDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetProsthoFPDNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format("select 1 as Id, 'FPD'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ProsthoFPDNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from PROSFPDCasesheet  WHERE CONVERT(char(10),ProsthoFPDDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetProsthoMFPNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format("select 1 as Id, 'MFD'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ProsthoMFPNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from PROSMFPCasesheet WHERE CONVERT(char(10),ProsthoMFPDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetOMFSOPNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format("select 1 as Id, 'OMFS'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(OMFSOpNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from OMFSOPCasesheet WHERE CONVERT(char(10),OMFSOpDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetOMFSIPNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format("select 1 as Id, 'IP'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(OMFSIpNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from OMFSIPCasesheet WHERE CONVERT(char(10),OMFSIpDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetOrpathNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format("select 1 as Id, 'IP'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(OMFSIpNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from OMFSIPCasesheet WHERE CONVERT(char(10),OMFSIpDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetBillingServiceNo(int DeptId)
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format("  select 1 as Id, (SELECT TOP 1  DeptCode FROM MASDepartment WHERE DeptId={0}) +''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(ServiceCode,3))+1, '101'))CasesheetNo from MASBillingServices WHERE DeptId={0}", (object) DeptId)).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetOralPathNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'ORPATH'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(RequisitionNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from ORPATHCasesheet WHERE CONVERT(char(10),RequisitionDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetRadioNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format(" select 1 as Id, 'RADIO'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(LabNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from RADIORegistration WHERE CONVERT(char(10),RadioDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }

    public string GetLabNo()
    {
      GetCasesheetNo getCasesheetNo = this._uow.Repository<GetCasesheetNo>().GetEntitiesBySql(string.Format("select 1 as Id, 'LAB'+''+CONVERT(VARCHAR(15),IsNULL(Max(RIGHT(LaboratoryNo,9))+1,dbo.GetCasesheetNo(Getdate())))CasesheetNo from LaboratoryRegistration WHERE CONVERT(char(10),SampleCollectedDate,126) ='{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd"))).SingleOrDefault<GetCasesheetNo>();
      if (getCasesheetNo != null)
        return getCasesheetNo.CasesheetNo;
      return string.Empty;
    }
  }
}
