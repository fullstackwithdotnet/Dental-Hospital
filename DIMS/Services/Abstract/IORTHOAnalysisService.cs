// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IORTHOAnalysisService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IORTHOAnalysisService : IService<ORTHOStaticSteiner>
  {
    OrthoAnalysisViewModal BindDetails(int Id, int AnalysisId);

    IEnumerable<OrthoStaticAnalysisViewModel> AnalysisList();

    IEnumerable<OrthoAnalysisViewModal> SteinerAnalysisList(int Id);

    IEnumerable<OrthoAnalysisViewModal> DownsAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> SchwarzAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> McNamaraAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> TweedsAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> RakosiAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> BurstoneHardAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> BurstoneSoftAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> GrummonsAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> EstheticsAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> SagittalAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> DiscrepancyAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> SKeletalAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> DivergenceAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> VerticalAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> DentoUpperAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> DentoLowerAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> CompositeAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> HoldawayAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> ArnettAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> MaxillaAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> MandibleAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> MaxtoMandAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> GrowthAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> SoftTissueAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> RickettsAnalysisList(int OrthoId);

    IEnumerable<OrthoAnalysisViewModal> BjroksAnalysisList(int OrthoId);

    IEnumerable<ORTHOCasesheetProperties> GetProperties();

    OrthoAnalysisViewModal GetORTHOPatientDetails(int Id);

    OrthoAnalysisViewModal sumIncisor(int Id);

    OrthoAnalysisViewModal BindDiscrepancy(int OrthoId, int AnalysisId);

    int SaveSteiner(OrthoAnalysisViewModal model);

    int SaveDowns(OrthoAnalysisViewModal model);

    int SaveSchwarz(OrthoAnalysisViewModal model);

    int SaveRakosi(OrthoAnalysisViewModal model);

    int SaveBurstoneHard(OrthoAnalysisViewModal model);

    int SaveBurstoneSoft(OrthoAnalysisViewModal model);

    int SaveGrummons(OrthoAnalysisViewModal model);

    int SaveEsthetics(OrthoAnalysisViewModal model);

    int SaveHoldaway(OrthoAnalysisViewModal model);

    int SaveArnett(OrthoAnalysisViewModal model);

    int SaveCranial(OrthoAnalysisViewModal model);

    int SaveMaxMand(OrthoAnalysisViewModal model);

    int SaveGrowth(OrthoAnalysisViewModal model);

    int SaveDentoLower(OrthoAnalysisViewModal model);

    int SaveSoftTissue(OrthoAnalysisViewModal model);

    int SaveTweeds(OrthoAnalysisViewModal model);

    int SaveRicketts(OrthoAnalysisViewModal model);

    int SaveMcNamara(OrthoAnalysisViewModal model);

    int SaveBjroks(OrthoAnalysisViewModal model);

    int SaveSagittal(OrthoAnalysisViewModal model);

    int SaveDiscrepancy(OrthoAnalysisViewModal model);

    int SaveVerticalRelation(OrthoAnalysisViewModal model);

    int SaveComposite(OrthoAnalysisViewModal model);

    int SaveDentoUpper(OrthoAnalysisViewModal model);

    int SaveSkeletal(OrthoAnalysisViewModal model);

    int SaveJawBases(OrthoAnalysisViewModal model);

    int SaveModalAnalysis(OrthoAnalysisViewModal model);

    OrthoAnalysisViewModal BindReportAnalysisDetails(int Id);
  }
}
