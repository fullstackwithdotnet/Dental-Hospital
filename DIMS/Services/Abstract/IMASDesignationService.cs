﻿// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IMASDesignationService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Metron.Entities;
using Repository.Base;

namespace DIMS.Services.Abstract
{
  public interface IMASDesignationService : IService<MASDesignation>
  {
    bool CheckDesigName(string DesigName);
  }
}
