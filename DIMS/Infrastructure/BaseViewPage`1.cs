// Decompiled with JetBrains decompiler
// Type: DIMS.Infrastructure.BaseViewPage`1
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System.Web.Mvc;

namespace DIMS.Infrastructure
{
  public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
  {
    public virtual CustomPrincipal User
    {
      get
      {
        return base.User as CustomPrincipal;
      }
    }
  }
}
