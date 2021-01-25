// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.ITemplateService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface ITemplateService : IService<RADIOTemplate>
  {
    List<TemplateSearchDetails> TemplateList(int DeptId, string From_Date, string To_Date, string url);

    IEnumerable<TemplateDetailsViewModel> ServiceListDetails(int id);

    TemplateViewModal BindRadiologyModel();

    int SaveRadiolgy(TemplateViewModal model);

    int UpdateRadiolgy(TemplateDetailsViewModel model);

    List<TemplateSearchDetails> LaboratoryList(int DeptId, string From_Date, string To_Date, string url);

    TemplateViewModal BindLaboratoryModel();

    IEnumerable<TemplateDetailsViewModel> ServiceListLaboratoryDetails(int id);
  }
}
