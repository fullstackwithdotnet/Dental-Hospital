// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MASCodeService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using Repository.Base;
using Repository.Core;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class MASCodeService : ServiceBase<MASCode>, IMASCodeService, IService<MASCode>
  {
    private IUnitOfWork _uow;

    public MASCodeService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<MASCode> GetAllCodes()
    {
      return this._uow.Repository<MASCode>().GetEntitiesBySql(string.Format("SELECT C.CodeId,C.CodeTypeId,C.CodeDescription FROM MASCode AS C INNER JOIN MASCodeType AS CT ON C.CodeTypeId = CT.CodeTypeId where C.DelInd=0"));
    }

    public IEnumerable<MASCode> GetCodesById(int Id = 0)
    {
      return this._uow.Repository<MASCode>().GetEntitiesBySql(string.Format("SELECT C.CodeId,C.CodeDescription FROM MASCode AS C INNER JOIN MASCodeType AS CT ON C.CodeTypeId = CT.CodeTypeId where  c.CodeTypeId={0} and C.DelInd=0", (object) Id));
    }

    public string GetCodeDescriptionByCodeId(int? codeId = 0)
    {
      if (codeId.HasValue)
      {
        MASCode masCode = this._uow.Repository<MASCode>().GetEntitiesBySql(string.Format("SELECT CodeId , CodeDescription FROM MASCode Where CodeId={0}", (object) codeId)).SingleOrDefault<MASCode>();
        if (masCode != null)
          return masCode.CodeDescription;
      }
      return string.Empty;
    }

    public string GetIPAddress(bool GetLan = false)
    {
      string str = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
      if (string.IsNullOrEmpty(str))
        str = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
      if (string.IsNullOrEmpty(str))
        str = HttpContext.Current.Request.UserHostAddress;
      if (string.IsNullOrEmpty(str) || str.Trim() == "::1")
      {
        GetLan = true;
        str = string.Empty;
      }
      if (GetLan && string.IsNullOrEmpty(str))
      {
        foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
          if (address.AddressFamily == AddressFamily.InterNetwork)
            return address.ToString();
        }
      }
      return str;
    }

    public IEnumerable<MASCategory> GetCategoryByPatientId(int Id)
    {
      return this._uow.Repository<MASCategory>().GetEntitiesBySql(string.Format("SELECT C.CATEGORYID,C.CATEGORYNAME FROM MASCATEGORY AS C INNER JOIN OPDPATIENTREGISTRATION AS P ON P.CATEGORYID=C.CATEGORYID WHERE P.PATIENTID={0}", (object) Id));
    }

    public IEnumerable<MASPaymode> GetPaymodeByPatientId(int Id)
    {
      return this._uow.Repository<MASPaymode>().GetEntitiesBySql(string.Format("SELECT P.PAYMODEID,P.PAYMODE FROM MASPAYMODE AS P INNER JOIN OPDPATIENTREGISTRATION AS OP ON OP.PAYMODEID=P.PAYMODEID WHERE OP.PATIENTID={0}", (object) Id));
    }
  }
}
