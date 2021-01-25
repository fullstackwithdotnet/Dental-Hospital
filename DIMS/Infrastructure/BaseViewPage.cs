// Decompiled with JetBrains decompiler
// Type: DIMS.Infrastructure.BaseViewPage
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System;
using System.Web.Mvc;

namespace DIMS.Infrastructure
{
  public class BaseViewPage : WebViewPage
  {
    public virtual CustomPrincipal User
    {
      get
      {
        return base.User as CustomPrincipal;
      }
    }

    public override void Execute()
    {
      throw new NotImplementedException();
    }
  }
}
