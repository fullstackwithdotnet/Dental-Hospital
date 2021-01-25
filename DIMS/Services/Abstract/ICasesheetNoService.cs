// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.ICasesheetNoService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

namespace DIMS.Services.Abstract
{
  public interface ICasesheetNoService
  {
    string GetOMRNo();

    string GetPerioNo();

    string GetOrthoNo();

    string GetCONSNo();

    string GetPEDONo();

    string GetProsthoCDNo();

    string GetProsthoRPDNo();

    string GetProsthoDIMNo();

    string GetProsthoFPDNo();

    string GetProsthoMFPNo();

    string GetOMFSOPNo();

    string GetOMFSIPNo();

    string GetPHDNo();

    string GetOralPathNo();

    string GetBillingServiceNo(int DeptId);

    string GetRadioNo();

    string GetLabNo();
  }
}
