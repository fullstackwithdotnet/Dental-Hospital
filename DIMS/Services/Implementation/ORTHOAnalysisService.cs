// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.ORTHOAnalysisService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
using DIMS.Enums;
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
  public class ORTHOAnalysisService : ServiceBase<ORTHOStaticSteiner>, IORTHOAnalysisService, IService<ORTHOStaticSteiner>
  {
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;

    public ORTHOAnalysisService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
    }

    public OrthoAnalysisViewModal GetORTHOPatientDetails(int Id)
    {
      return this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format("exec GetORTHOCasesheet {0}", (object) Id)).FirstOrDefault<OrthoAnalysisViewModal>();
    }

    public OrthoAnalysisViewModal BindDetails(int Id, int AnalysisId)
    {
      OrthoAnalysisViewModal analysisViewModal = new OrthoAnalysisViewModal();
      OrthoAnalysisViewModal orthoPatientDetails = this.GetORTHOPatientDetails(Id);
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) orthoPatientDetails.OrthoId, (object) AnalysisId));
      if (entitiesBySql != null && entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
          orthoPatientDetails.Inference = inferenceDetails.Inference;
      }
      orthoPatientDetails.AnalysisId = AnalysisId;
      orthoPatientDetails.AnalysisDisplayName = this._uow.Repository<ORTHOAnalysis>().Get(AnalysisId).AnalysisDisplayName;
      return orthoPatientDetails;
    }

    public IEnumerable<OrthoStaticAnalysisViewModel> AnalysisList()
    {
      List<OrthoStaticAnalysisViewModel> analysisViewModelList = new List<OrthoStaticAnalysisViewModel>();
      foreach (OrthoStaticAnalysisViewModel analysisViewModel in this._uow.Repository<OrthoStaticAnalysisViewModel>().GetEntitiesBySql(string.Format(Queries.Analysis)))
        analysisViewModelList.Add(new OrthoStaticAnalysisViewModel()
        {
          AnalysisId = analysisViewModel.AnalysisId,
          AnalysisDisplayName = analysisViewModel.AnalysisDisplayName,
          Visibility = analysisViewModel.Visibility
        });
      return (IEnumerable<OrthoStaticAnalysisViewModel>) analysisViewModelList;
    }

    public IEnumerable<OrthoAnalysisViewModal> SteinerAnalysisList(int Id)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.SteinerforPatient, (object) Id));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            SteinerStaticId = analysisViewModal.SteinerStaticId,
            Letters = analysisViewModal.Letters,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            SteinerId = analysisViewModal.SteinerId,
            OrthoId = analysisViewModal.OrthoId,
            PreTreatment = analysisViewModal.PreTreatment,
            PreSurgical = analysisViewModal.PreSurgical,
            PostAlignment = analysisViewModal.PostAlignment,
            PostSurgical = analysisViewModal.PostSurgical,
            MidTreatment = analysisViewModal.MidTreatment,
            Posttreatment = analysisViewModal.Posttreatment,
            Retention = analysisViewModal.Retention,
            UpperLip = analysisViewModal.UpperLip,
            LowerLip = analysisViewModal.LowerLip,
            Inference = analysisViewModal.Inference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Steiner)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            SteinerStaticId = analysisViewModal.SteinerStaticId,
            Letters = analysisViewModal.Letters,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> DownsAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.DownsforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            DownsStaticId = analysisViewModal.DownsStaticId,
            DownsId = analysisViewModal.DownsId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PreTreatment = analysisViewModal.PreTreatment,
            PreSurgical = analysisViewModal.PreSurgical,
            PostAlignment = analysisViewModal.PostAlignment,
            PostSurgical = analysisViewModal.PostSurgical,
            MidTreatment = analysisViewModal.MidTreatment,
            Posttreatment = analysisViewModal.Posttreatment,
            Retention = analysisViewModal.Retention,
            Inference = analysisViewModal.Inference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Downs)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            DownsStaticId = analysisViewModal.DownsStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> SchwarzAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.SchwarzforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            SchwarzStaticId = analysisViewModal.SchwarzStaticId,
            SchwarzId = analysisViewModal.SchwarzId,
            Parameters = analysisViewModal.Parameters,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PreTreatment = analysisViewModal.PreTreatment,
            PreSurgical = analysisViewModal.PreSurgical,
            PostAlignment = analysisViewModal.PostAlignment,
            PostSurgical = analysisViewModal.PostSurgical,
            MidTreatment = analysisViewModal.MidTreatment,
            Posttreatment = analysisViewModal.Posttreatment,
            Retention = analysisViewModal.Retention,
            Inference = analysisViewModal.Inference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Schwarz)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            SchwarzStaticId = analysisViewModal.SchwarzStaticId,
            Parameters = analysisViewModal.Parameters,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> McNamaraAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.McNamaraforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            McNamaraStaticId = analysisViewModal.McNamaraStaticId,
            McNamaraId = analysisViewModal.McNamaraId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PreTreatment = analysisViewModal.PreTreatment,
            PreSurgical = analysisViewModal.PreSurgical,
            PostAlignment = analysisViewModal.PostAlignment,
            PostSurgical = analysisViewModal.PostSurgical,
            MidTreatment = analysisViewModal.MidTreatment,
            Posttreatment = analysisViewModal.Posttreatment,
            Retention = analysisViewModal.Retention,
            Inference = analysisViewModal.Inference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.McNamara, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            McNamaraStaticId = analysisViewModal.McNamaraStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> TweedsAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.TweedsforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            TweedsStaticId = analysisViewModal.TweedsStaticId,
            TweedsId = analysisViewModal.TweedsId,
            Angle = analysisViewModal.Angle,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PreTreatment = analysisViewModal.PreTreatment,
            PreSurgical = analysisViewModal.PreSurgical,
            PostAlignment = analysisViewModal.PostAlignment,
            PostSurgical = analysisViewModal.PostSurgical,
            MidTreatment = analysisViewModal.MidTreatment,
            Posttreatment = analysisViewModal.Posttreatment,
            Retention = analysisViewModal.Retention,
            Inference = analysisViewModal.Inference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Tweeds, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            TweedsStaticId = analysisViewModal.TweedsStaticId,
            Angle = analysisViewModal.Angle,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> RakosiAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.RakosiforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            RakosiStaticId = analysisViewModal.RakosiStaticId,
            RakosiId = analysisViewModal.RakosiId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PreTreatment = analysisViewModal.PreTreatment,
            PreSurgical = analysisViewModal.PreSurgical,
            PostAlignment = analysisViewModal.PostAlignment,
            PostSurgical = analysisViewModal.PostSurgical,
            MidTreatment = analysisViewModal.MidTreatment,
            Posttreatment = analysisViewModal.Posttreatment,
            Retention = analysisViewModal.Retention,
            Inference = analysisViewModal.Inference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Rakosi, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            RakosiStaticId = analysisViewModal.RakosiStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> BurstoneHardAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.BurstoneHardforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            BurstoneHardStaticId = analysisViewModal.BurstoneHardStaticId,
            BurstoneHardId = analysisViewModal.BurstoneHardId,
            Measurements = analysisViewModal.Measurements,
            Male = analysisViewModal.Male,
            Female = analysisViewModal.Female,
            Indicator = analysisViewModal.Indicator,
            OrthoId = analysisViewModal.OrthoId,
            PreTreatment = analysisViewModal.PreTreatment,
            PreSurgical = analysisViewModal.PreSurgical,
            PostAlignment = analysisViewModal.PostAlignment,
            PostSurgical = analysisViewModal.PostSurgical,
            MidTreatment = analysisViewModal.MidTreatment,
            Posttreatment = analysisViewModal.Posttreatment,
            Retention = analysisViewModal.Retention,
            Inference = analysisViewModal.Inference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.BurstoneHard, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            BurstoneHardStaticId = analysisViewModal.BurstoneHardStaticId,
            Measurements = analysisViewModal.Measurements,
            Male = analysisViewModal.Male,
            Female = analysisViewModal.Female,
            Indicator = analysisViewModal.Indicator
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> BurstoneSoftAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.BurstoneSoftforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            BurstoneSoftStaticId = analysisViewModal.BurstoneSoftStaticId,
            BurstoneSoftId = analysisViewModal.BurstoneSoftId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            Indicator = analysisViewModal.Indicator,
            OrthoId = analysisViewModal.OrthoId,
            PreTreatment = analysisViewModal.PreTreatment,
            PreSurgical = analysisViewModal.PreSurgical,
            PostAlignment = analysisViewModal.PostAlignment,
            PostSurgical = analysisViewModal.PostSurgical,
            MidTreatment = analysisViewModal.MidTreatment,
            Posttreatment = analysisViewModal.Posttreatment,
            Retention = analysisViewModal.Retention,
            Inference = analysisViewModal.Inference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.BurstoneSoft, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            BurstoneSoftStaticId = analysisViewModal.BurstoneSoftStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            Indicator = analysisViewModal.Indicator
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> GrummonsAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.FrontalGrumforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            FrontalGrumStaticId = analysisViewModal.FrontalGrumStaticId,
            FrontalGrumId = analysisViewModal.FrontalGrumId,
            Field = analysisViewModal.Field,
            OrthoId = analysisViewModal.OrthoId,
            LeftPreRx = analysisViewModal.LeftPreRx,
            LeftPostRx = analysisViewModal.LeftPostRx,
            RightPreRx = analysisViewModal.RightPreRx,
            RightPostRx = analysisViewModal.RightPostRx,
            DiffPreRx = analysisViewModal.DiffPreRx,
            DiffPostRx = analysisViewModal.DiffPostRx,
            Inference = analysisViewModal.Inference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.FrontalGrum, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            FrontalGrumStaticId = analysisViewModal.FrontalGrumStaticId,
            Field = analysisViewModal.Field
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> EstheticsAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.FrontalEsthetforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            FrontalEstheticsStaticId = analysisViewModal.FrontalEstheticsStaticId,
            FrontalEstheticId = analysisViewModal.FrontalEstheticId,
            Field = analysisViewModal.Field,
            Percentage = analysisViewModal.Percentage,
            OrthoId = analysisViewModal.OrthoId,
            Value = analysisViewModal.Value
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.FrontalEsthetics, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            FrontalEstheticsStaticId = analysisViewModal.FrontalEstheticsStaticId,
            Field = analysisViewModal.Field,
            Percentage = analysisViewModal.Percentage
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> SagittalAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.SagittalforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            SagittalStaticId = analysisViewModal.SagittalStaticId,
            SagittalId = analysisViewModal.SagittalId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            PreRxMeasurement = analysisViewModal.PreRxMeasurement,
            PreRxClass = analysisViewModal.PreRxClass,
            SurgeryMeasurement = analysisViewModal.SurgeryMeasurement,
            SurgeryClass = analysisViewModal.SurgeryClass,
            PostRxMeasurement = analysisViewModal.PostRxMeasurement,
            PostRxClass = analysisViewModal.PostRxClass,
            RetentionMeasurement = analysisViewModal.RetentionMeasurement,
            RetentionClass = analysisViewModal.RetentionClass,
            ChangeMeasurement = analysisViewModal.ChangeMeasurement,
            ChangeClass = analysisViewModal.ChangeClass,
            OrthoId = analysisViewModal.OrthoId,
            Value = analysisViewModal.Value
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Sagittal, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            SagittalStaticId = analysisViewModal.SagittalStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> DiscrepancyAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.DiscrepancyforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            DiscrepancyStaticId = analysisViewModal.DiscrepancyStaticId,
            DiscrepancyId = analysisViewModal.DiscrepancyId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            Indicator = analysisViewModal.Indicator,
            PreRx = analysisViewModal.PreRx,
            PostModulation = analysisViewModal.PostModulation,
            MidRx = analysisViewModal.MidRx,
            PostRx = analysisViewModal.PostRx,
            Retention = analysisViewModal.Retention,
            Change = analysisViewModal.Change,
            OrthoId = analysisViewModal.OrthoId
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Discrepancy, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            DiscrepancyStaticId = analysisViewModal.DiscrepancyStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            Indicator = analysisViewModal.Indicator
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> SKeletalAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.SkeletalforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            SkeletalStaticId = analysisViewModal.SkeletalStaticId,
            SkeletalId = analysisViewModal.SkeletalId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PreRx = analysisViewModal.PreRx,
            PostModulation = analysisViewModal.PostModulation,
            PostRx = analysisViewModal.PostRx,
            Retention = analysisViewModal.Retention,
            Change = analysisViewModal.Change,
            Inference = analysisViewModal.Inference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Skeletal, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            SkeletalStaticId = analysisViewModal.SkeletalStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> DivergenceAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.JawforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            JawBasesStaticId = analysisViewModal.JawBasesStaticId,
            JawBasesId = analysisViewModal.JawBasesId,
            Measurements = analysisViewModal.Measurements,
            OrthoId = analysisViewModal.OrthoId,
            PreRx = analysisViewModal.PreRx,
            PostModulation = analysisViewModal.PostModulation,
            PostRx = analysisViewModal.PostRx,
            Retention = analysisViewModal.Retention,
            Change = analysisViewModal.Change
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.JawBases, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            JawBasesStaticId = analysisViewModal.JawBasesStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> VerticalAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.VerticalforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            VerticalStaticId = analysisViewModal.VerticalStaticId,
            VerticalId = analysisViewModal.VerticalId,
            Measurements = analysisViewModal.Measurements,
            Indicator = analysisViewModal.Indicator,
            OrthoId = analysisViewModal.OrthoId,
            PreRx = analysisViewModal.PreRx,
            PostModulation = analysisViewModal.PostModulation,
            PostRx = analysisViewModal.PostRx,
            Retention = analysisViewModal.Retention,
            Change = analysisViewModal.Change
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Vertical, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            VerticalStaticId = analysisViewModal.VerticalStaticId,
            Measurements = analysisViewModal.Measurements,
            Indicator = analysisViewModal.Indicator
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> DentoUpperAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.DentoUpperforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            DentoUpperStaticId = analysisViewModal.DentoUpperStaticId,
            DentoUpperId = analysisViewModal.DentoUpperId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PreRx = analysisViewModal.PreRx,
            PostModulation = analysisViewModal.PostModulation,
            MidRx = analysisViewModal.MidRx,
            PostRx = analysisViewModal.PostRx,
            Retention = analysisViewModal.Retention,
            Change = analysisViewModal.Change
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.DentoUpper, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            DentoUpperStaticId = analysisViewModal.DentoUpperStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> DentoLowerAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.DentoLowerforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            DentoLowerStaticId = analysisViewModal.DentoLowerStaticId,
            DentoLowerId = analysisViewModal.DentoLowerId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PreRx = analysisViewModal.PreRx,
            PostModulation = analysisViewModal.PostModulation,
            MidRx = analysisViewModal.MidRx,
            PostRx = analysisViewModal.PostRx,
            Retention = analysisViewModal.Retention,
            Change = analysisViewModal.Change
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.DentoLower, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            DentoLowerStaticId = analysisViewModal.DentoLowerStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> CompositeAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.CompositeforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            CompositeStaticId = analysisViewModal.CompositeStaticId,
            CompositeId = analysisViewModal.CompositeId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            Indicator = analysisViewModal.Indicator,
            OrthoId = analysisViewModal.OrthoId,
            Actual = analysisViewModal.Actual,
            CompositeInference = analysisViewModal.CompositeInference
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Composite, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            CompositeStaticId = analysisViewModal.CompositeStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            Indicator = analysisViewModal.Indicator
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> HoldawayAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.HoldawayforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            HoldawayStaticId = analysisViewModal.HoldawayStaticId,
            HoldawayId = analysisViewModal.HoldawayId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            Indicator = analysisViewModal.Indicator,
            OrthoId = analysisViewModal.OrthoId,
            Value = analysisViewModal.Value,
            HoldInference = analysisViewModal.HoldInference,
            PreRx = analysisViewModal.PreRx,
            PostRx = analysisViewModal.PostRx,
            During = analysisViewModal.During
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Holdaway, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            HoldawayStaticId = analysisViewModal.HoldawayStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            Indicator = analysisViewModal.Indicator
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> ArnettAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.ArnettforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            ArnettStaticId = analysisViewModal.ArnettStaticId,
            ArnettId = analysisViewModal.ArnettId,
            Measurements = analysisViewModal.Measurements,
            Female = analysisViewModal.Female,
            Male = analysisViewModal.Male,
            Indicator = analysisViewModal.Indicator,
            OrthoId = analysisViewModal.OrthoId,
            PreRx = analysisViewModal.PreRx,
            PostModulation = analysisViewModal.PostModulation
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Arnett, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            ArnettStaticId = analysisViewModal.ArnettStaticId,
            Measurements = analysisViewModal.Measurements,
            Female = analysisViewModal.Female,
            Male = analysisViewModal.Male,
            Indicator = analysisViewModal.Indicator
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> MaxillaAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.MaxillaforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            CranialMaxStaticId = analysisViewModal.CranialMaxStaticId,
            CranialMaxId = analysisViewModal.CranialMaxId,
            CranialMaxMeasurements = analysisViewModal.CranialMaxMeasurements,
            CranialMaxMean = analysisViewModal.CranialMaxMean,
            OrthoId = analysisViewModal.OrthoId,
            PtValue = analysisViewModal.PtValue
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Maxilla, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            CranialMaxStaticId = analysisViewModal.CranialMaxStaticId,
            CranialMaxMeasurements = analysisViewModal.CranialMaxMeasurements,
            CranialMaxMean = analysisViewModal.CranialMaxMean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> MandibleAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.MandibleforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            CranialManStaticId = analysisViewModal.CranialManStaticId,
            CranialManId = analysisViewModal.CranialManId,
            CranialManMeasurements = analysisViewModal.CranialManMeasurements,
            CranialManMean = analysisViewModal.CranialManMean,
            OrthoId = analysisViewModal.OrthoId,
            PtValue = analysisViewModal.PtValue
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Mandible, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            CranialManStaticId = analysisViewModal.CranialManStaticId,
            CranialManMeasurements = analysisViewModal.CranialManMeasurements,
            CranialManMean = analysisViewModal.CranialManMean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> MaxtoMandAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.MaxMandforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            MaxManStaticId = analysisViewModal.MaxManStaticId,
            MaxManId = analysisViewModal.MaxManId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PtValue = analysisViewModal.PtValue
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.MaxMand, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            MaxManStaticId = analysisViewModal.MaxManStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> GrowthAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.GrowthforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            GrowthStaticId = analysisViewModal.GrowthStaticId,
            GrowthId = analysisViewModal.GrowthId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PtValue = analysisViewModal.PtValue
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Growth, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            GrowthStaticId = analysisViewModal.GrowthStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> SoftTissueAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.SoftTissueforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            SoftTissueStaticId = analysisViewModal.SoftTissueStaticId,
            SoftTissueId = analysisViewModal.SoftTissueId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PtValue = analysisViewModal.PtValue
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.SoftTissue, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            SoftTissueStaticId = analysisViewModal.SoftTissueStaticId,
            Measurements = analysisViewModal.Measurements,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> RickettsAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.RickettsforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            RickettsStaticId = analysisViewModal.RickettsStaticId,
            RickettsId = analysisViewModal.RickettsId,
            Angles = analysisViewModal.Angles,
            MeanAt = analysisViewModal.MeanAt,
            AgeChange = analysisViewModal.AgeChange,
            OrthoId = analysisViewModal.OrthoId,
            PreRx = analysisViewModal.PreRx,
            During = analysisViewModal.During,
            PostRx = analysisViewModal.PostRx
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Ricketts, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            RickettsStaticId = analysisViewModal.RickettsStaticId,
            Angles = analysisViewModal.Angles,
            MeanAt = analysisViewModal.MeanAt,
            AgeChange = analysisViewModal.AgeChange
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<OrthoAnalysisViewModal> BjroksAnalysisList(int OrthoId)
    {
      List<OrthoAnalysisViewModal> analysisViewModalList = new List<OrthoAnalysisViewModal>();
      IEnumerable<OrthoAnalysisViewModal> entitiesBySql = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.BjroksforPatient, (object) OrthoId));
      if (entitiesBySql != null && entitiesBySql.Count<OrthoAnalysisViewModal>() != 0)
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in entitiesBySql)
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            BjroksStaticId = analysisViewModal.BjroksStaticId,
            BjroksId = analysisViewModal.BjroksId,
            Angles = analysisViewModal.Angles,
            Mean = analysisViewModal.Mean,
            OrthoId = analysisViewModal.OrthoId,
            PreRx = analysisViewModal.PreRx,
            During = analysisViewModal.During,
            PostRx = analysisViewModal.PostRx
          });
      }
      else
      {
        foreach (OrthoAnalysisViewModal analysisViewModal in this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format(Queries.Bjroks, (object) OrthoId)))
          analysisViewModalList.Add(new OrthoAnalysisViewModal()
          {
            BjroksStaticId = analysisViewModal.BjroksStaticId,
            Angles = analysisViewModal.Angles,
            Mean = analysisViewModal.Mean
          });
      }
      return (IEnumerable<OrthoAnalysisViewModal>) analysisViewModalList;
    }

    public IEnumerable<ORTHOCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
    }

    public OrthoAnalysisViewModal BindDiscrepancy(int OrthoId, int AnalysisId)
    {
      OrthoAnalysisViewModal analysisViewModal = new OrthoAnalysisViewModal();
      OrthoAnalysisViewModal orthoPatientDetails = this.GetORTHOPatientDetails(OrthoId);
      orthoPatientDetails.OrthoId = OrthoId;
      orthoPatientDetails.AnalysisId = AnalysisId;
      orthoPatientDetails.MidlineList = this._Dropdownservice.GetCodesById(49);
      orthoPatientDetails.Proplist = this.GetProperties();
      return orthoPatientDetails;
    }

    public OrthoAnalysisViewModal sumIncisor(int Id)
    {
      OrthoAnalysisViewModal analysisViewModal = new OrthoAnalysisViewModal();
      OrthoAnalysisViewModal orthoPatientDetails = this.GetORTHOPatientDetails(Id);
      Decimal num1 = new Decimal();
      Decimal num2 = new Decimal();
      Decimal num3 = new Decimal();
      Decimal num4 = new Decimal();
      Decimal num5 = new Decimal();
      Decimal num6 = new Decimal();
      Decimal num7 = new Decimal();
      if (!string.IsNullOrEmpty(orthoPatientDetails.Upper1Rt.ToString()))
        num1 = orthoPatientDetails.Upper1Rt;
      if (!string.IsNullOrEmpty(orthoPatientDetails.Upper1Lt.ToString()))
        num2 = orthoPatientDetails.Upper1Lt;
      if (!string.IsNullOrEmpty(orthoPatientDetails.Upper2Rt.ToString()))
        num3 = orthoPatientDetails.Upper2Rt;
      if (!string.IsNullOrEmpty(orthoPatientDetails.Upper2Lt.ToString()))
        num4 = orthoPatientDetails.Upper2Lt;
      Decimal num8 = num1 + num2 + num3 + num4;
      if (!string.IsNullOrEmpty(num8.ToString()))
        orthoPatientDetails.SumIncisors = num8;
      if (!string.IsNullOrEmpty(orthoPatientDetails.SumIncisors.ToString()))
      {
        Decimal num9 = num8 * new Decimal(100) / new Decimal(80);
        orthoPatientDetails.CalcPreMolar = num9;
      }
      if (!string.IsNullOrEmpty(orthoPatientDetails.SumIncisors.ToString()))
      {
        Decimal num9 = num8 * new Decimal(100) / new Decimal(64);
        orthoPatientDetails.CalcMolar = num9;
      }
      return orthoPatientDetails;
    }

    public int SaveSteiner(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOSteinerDetails> source = this._uow.Repository<ORTHOSteinerDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOSteinerDetails, int>((Func<ORTHOSteinerDetails, int>) (x => x.SteinerId));
      ORTHOSteinerDetails entity2 = new ORTHOSteinerDetails();
      if (source.Count<ORTHOSteinerDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal steinerAnalysis in model.SteinerAnalysisList)
        {
          entity2.SteinerId = steinerAnalysis.SteinerId;
          entity2.SteinerStaticId = steinerAnalysis.SteinerStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = steinerAnalysis.PreTreatment;
          entity2.PreSurgical = steinerAnalysis.PreSurgical;
          entity2.PostAlignment = steinerAnalysis.PostAlignment;
          entity2.PostSurgical = steinerAnalysis.PostSurgical;
          entity2.MidTreatment = steinerAnalysis.MidTreatment;
          entity2.Posttreatment = steinerAnalysis.Posttreatment;
          entity2.Retention = steinerAnalysis.Retention;
          this._uow.Repository<ORTHOSteinerDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal steinerAnalysis in model.SteinerAnalysisList)
        {
          entity2.SteinerStaticId = steinerAnalysis.SteinerStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = steinerAnalysis.PreTreatment;
          entity2.PreSurgical = steinerAnalysis.PreSurgical;
          entity2.PostAlignment = steinerAnalysis.PostAlignment;
          entity2.PostSurgical = steinerAnalysis.PostSurgical;
          entity2.MidTreatment = steinerAnalysis.MidTreatment;
          entity2.Posttreatment = steinerAnalysis.Posttreatment;
          entity2.Retention = steinerAnalysis.Retention;
          this._uow.Repository<ORTHOSteinerDetails>().Add(entity2, false);
        }
      }
      IEnumerable<ORTHOCasesheetProperties> all1 = this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
      IEnumerable<ORTHOCasesheetPropertyValues> all2 = this._uow.Repository<ORTHOCasesheetPropertyValues>().GetAll("OrthoId=" + (object) model.OrthoId);
      foreach (PropertyInfo property1 in model.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            ORTHOCasesheetPropertyValues entity3 = new ORTHOCasesheetPropertyValues();
            entity3.OrthoId = model.OrthoId;
            ORTHOCasesheetProperties property = all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              ORTHOCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<ORTHOCasesheetPropertyValues>((Func<ORTHOCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.OrthoId == model.OrthoId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                entity3.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Update(entity3, false);
              }
              else
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Add(entity3, false);
              }
            }
          }
        }
      }
      return model.OrthoId;
    }

    public int SaveDowns(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHODownsDetails> source = this._uow.Repository<ORTHODownsDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHODownsDetails, int>((Func<ORTHODownsDetails, int>) (x => x.DownsId));
      ORTHODownsDetails entity2 = new ORTHODownsDetails();
      if (source.Count<ORTHODownsDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal downsAnalysis in model.DownsAnalysisList)
        {
          entity2.DownsId = downsAnalysis.DownsId;
          entity2.DownsStaticId = downsAnalysis.DownsStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = downsAnalysis.PreTreatment;
          entity2.PreSurgical = downsAnalysis.PreSurgical;
          entity2.PostAlignment = downsAnalysis.PostAlignment;
          entity2.PostSurgical = downsAnalysis.PostSurgical;
          entity2.MidTreatment = downsAnalysis.MidTreatment;
          entity2.Posttreatment = downsAnalysis.Posttreatment;
          entity2.Retention = downsAnalysis.Retention;
          this._uow.Repository<ORTHODownsDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal downsAnalysis in model.DownsAnalysisList)
        {
          entity2.DownsId = 0;
          entity2.DownsStaticId = downsAnalysis.DownsStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = downsAnalysis.PreTreatment;
          entity2.PreSurgical = downsAnalysis.PreSurgical;
          entity2.PostAlignment = downsAnalysis.PostAlignment;
          entity2.PostSurgical = downsAnalysis.PostSurgical;
          entity2.MidTreatment = downsAnalysis.MidTreatment;
          entity2.Posttreatment = downsAnalysis.Posttreatment;
          entity2.Retention = downsAnalysis.Retention;
          this._uow.Repository<ORTHODownsDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveSchwarz(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOSchwarzDetails> source = this._uow.Repository<ORTHOSchwarzDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOSchwarzDetails, int>((Func<ORTHOSchwarzDetails, int>) (x => x.SchwarzId));
      ORTHOSchwarzDetails entity2 = new ORTHOSchwarzDetails();
      if (source.Count<ORTHOSchwarzDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal schwarzAnalysis in model.SchwarzAnalysisList)
        {
          entity2.SchwarzId = schwarzAnalysis.SchwarzId;
          entity2.SchwarzStaticId = schwarzAnalysis.SchwarzStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = schwarzAnalysis.PreTreatment;
          entity2.PreSurgical = schwarzAnalysis.PreSurgical;
          entity2.PostAlignment = schwarzAnalysis.PostAlignment;
          entity2.PostSurgical = schwarzAnalysis.PostSurgical;
          entity2.MidTreatment = schwarzAnalysis.MidTreatment;
          entity2.Posttreatment = schwarzAnalysis.Posttreatment;
          entity2.Retention = schwarzAnalysis.Retention;
          this._uow.Repository<ORTHOSchwarzDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal schwarzAnalysis in model.SchwarzAnalysisList)
        {
          entity2.SchwarzId = 0;
          entity2.SchwarzStaticId = schwarzAnalysis.SchwarzStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = schwarzAnalysis.PreTreatment;
          entity2.PreSurgical = schwarzAnalysis.PreSurgical;
          entity2.PostAlignment = schwarzAnalysis.PostAlignment;
          entity2.PostSurgical = schwarzAnalysis.PostSurgical;
          entity2.MidTreatment = schwarzAnalysis.MidTreatment;
          entity2.Posttreatment = schwarzAnalysis.Posttreatment;
          entity2.Retention = schwarzAnalysis.Retention;
          this._uow.Repository<ORTHOSchwarzDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveRakosi(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHORakosiDetails> source = this._uow.Repository<ORTHORakosiDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHORakosiDetails, int>((Func<ORTHORakosiDetails, int>) (x => x.RakosiId));
      ORTHORakosiDetails entity2 = new ORTHORakosiDetails();
      if (source.Count<ORTHORakosiDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal rakosiAnalysis in model.RakosiAnalysisList)
        {
          entity2.RakosiId = rakosiAnalysis.RakosiId;
          entity2.RakosiStaticId = rakosiAnalysis.RakosiStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = rakosiAnalysis.PreTreatment;
          entity2.PreSurgical = rakosiAnalysis.PreSurgical;
          entity2.PostAlignment = rakosiAnalysis.PostAlignment;
          entity2.PostSurgical = rakosiAnalysis.PostSurgical;
          entity2.MidTreatment = rakosiAnalysis.MidTreatment;
          entity2.Posttreatment = rakosiAnalysis.Posttreatment;
          entity2.Retention = rakosiAnalysis.Retention;
          this._uow.Repository<ORTHORakosiDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal rakosiAnalysis in model.RakosiAnalysisList)
        {
          entity2.RakosiId = 0;
          entity2.RakosiStaticId = rakosiAnalysis.RakosiStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = rakosiAnalysis.PreTreatment;
          entity2.PreSurgical = rakosiAnalysis.PreSurgical;
          entity2.PostAlignment = rakosiAnalysis.PostAlignment;
          entity2.PostSurgical = rakosiAnalysis.PostSurgical;
          entity2.MidTreatment = rakosiAnalysis.MidTreatment;
          entity2.Posttreatment = rakosiAnalysis.Posttreatment;
          entity2.Retention = rakosiAnalysis.Retention;
          this._uow.Repository<ORTHORakosiDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveBurstoneHard(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOBurstoneHardDetails> source = this._uow.Repository<ORTHOBurstoneHardDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOBurstoneHardDetails, int>((Func<ORTHOBurstoneHardDetails, int>) (x => x.BurstoneHardId));
      ORTHOBurstoneHardDetails entity2 = new ORTHOBurstoneHardDetails();
      if (source.Count<ORTHOBurstoneHardDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal burstoneHardAnalysis in model.BurstoneHardAnalysisList)
        {
          entity2.BurstoneHardId = burstoneHardAnalysis.BurstoneHardId;
          entity2.BurstoneHardStaticId = burstoneHardAnalysis.BurstoneHardStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = burstoneHardAnalysis.PreTreatment;
          entity2.PreSurgical = burstoneHardAnalysis.PreSurgical;
          entity2.PostAlignment = burstoneHardAnalysis.PostAlignment;
          entity2.PostSurgical = burstoneHardAnalysis.PostSurgical;
          entity2.MidTreatment = burstoneHardAnalysis.MidTreatment;
          entity2.Posttreatment = burstoneHardAnalysis.Posttreatment;
          entity2.Retention = burstoneHardAnalysis.Retention;
          this._uow.Repository<ORTHOBurstoneHardDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal burstoneHardAnalysis in model.BurstoneHardAnalysisList)
        {
          entity2.BurstoneHardId = 0;
          entity2.BurstoneHardStaticId = burstoneHardAnalysis.BurstoneHardStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = burstoneHardAnalysis.PreTreatment;
          entity2.PreSurgical = burstoneHardAnalysis.PreSurgical;
          entity2.PostAlignment = burstoneHardAnalysis.PostAlignment;
          entity2.PostSurgical = burstoneHardAnalysis.PostSurgical;
          entity2.MidTreatment = burstoneHardAnalysis.MidTreatment;
          entity2.Posttreatment = burstoneHardAnalysis.Posttreatment;
          entity2.Retention = burstoneHardAnalysis.Retention;
          this._uow.Repository<ORTHOBurstoneHardDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveBurstoneSoft(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOBurstoneSoftDetails> source = this._uow.Repository<ORTHOBurstoneSoftDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOBurstoneSoftDetails, int>((Func<ORTHOBurstoneSoftDetails, int>) (x => x.BurstoneSoftId));
      ORTHOBurstoneSoftDetails entity2 = new ORTHOBurstoneSoftDetails();
      if (source.Count<ORTHOBurstoneSoftDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal burstoneSoftAnalysis in model.BurstoneSoftAnalysisList)
        {
          entity2.BurstoneSoftId = burstoneSoftAnalysis.BurstoneSoftId;
          entity2.BurstoneSoftStaticId = burstoneSoftAnalysis.BurstoneSoftStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = burstoneSoftAnalysis.PreTreatment;
          entity2.PreSurgical = burstoneSoftAnalysis.PreSurgical;
          entity2.PostAlignment = burstoneSoftAnalysis.PostAlignment;
          entity2.PostSurgical = burstoneSoftAnalysis.PostSurgical;
          entity2.MidTreatment = burstoneSoftAnalysis.MidTreatment;
          entity2.Posttreatment = burstoneSoftAnalysis.Posttreatment;
          entity2.Retention = burstoneSoftAnalysis.Retention;
          this._uow.Repository<ORTHOBurstoneSoftDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal burstoneSoftAnalysis in model.BurstoneSoftAnalysisList)
        {
          entity2.BurstoneSoftId = 0;
          entity2.BurstoneSoftStaticId = burstoneSoftAnalysis.BurstoneSoftStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = burstoneSoftAnalysis.PreTreatment;
          entity2.PreSurgical = burstoneSoftAnalysis.PreSurgical;
          entity2.PostAlignment = burstoneSoftAnalysis.PostAlignment;
          entity2.PostSurgical = burstoneSoftAnalysis.PostSurgical;
          entity2.MidTreatment = burstoneSoftAnalysis.MidTreatment;
          entity2.Posttreatment = burstoneSoftAnalysis.Posttreatment;
          entity2.Retention = burstoneSoftAnalysis.Retention;
          this._uow.Repository<ORTHOBurstoneSoftDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveGrummons(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOFrontalGrumDetails> source = this._uow.Repository<ORTHOFrontalGrumDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOFrontalGrumDetails, int>((Func<ORTHOFrontalGrumDetails, int>) (x => x.FrontalGrumId));
      ORTHOFrontalGrumDetails entity2 = new ORTHOFrontalGrumDetails();
      if (source.Count<ORTHOFrontalGrumDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal grummonsAnalysis in model.GrummonsAnalysisList)
        {
          entity2.FrontalGrumId = grummonsAnalysis.FrontalGrumId;
          entity2.FrontalGrumStaticId = grummonsAnalysis.FrontalGrumStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.LeftPreRx = grummonsAnalysis.LeftPreRx;
          entity2.LeftPostRx = grummonsAnalysis.LeftPostRx;
          entity2.RightPreRx = grummonsAnalysis.RightPreRx;
          entity2.RightPostRx = grummonsAnalysis.RightPostRx;
          entity2.DiffPreRx = grummonsAnalysis.DiffPreRx;
          entity2.DiffPostRx = grummonsAnalysis.DiffPostRx;
          this._uow.Repository<ORTHOFrontalGrumDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal grummonsAnalysis in model.GrummonsAnalysisList)
        {
          entity2.FrontalGrumId = 0;
          entity2.FrontalGrumStaticId = grummonsAnalysis.FrontalGrumStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.LeftPreRx = grummonsAnalysis.LeftPreRx;
          entity2.LeftPostRx = grummonsAnalysis.LeftPostRx;
          entity2.RightPreRx = grummonsAnalysis.RightPreRx;
          entity2.RightPostRx = grummonsAnalysis.RightPostRx;
          entity2.DiffPreRx = grummonsAnalysis.DiffPreRx;
          entity2.DiffPostRx = grummonsAnalysis.DiffPostRx;
          this._uow.Repository<ORTHOFrontalGrumDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveEsthetics(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOFronEstheticsDetails> source = this._uow.Repository<ORTHOFronEstheticsDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOFronEstheticsDetails, int>((Func<ORTHOFronEstheticsDetails, int>) (x => x.FrontalEstheticId));
      ORTHOFronEstheticsDetails entity2 = new ORTHOFronEstheticsDetails();
      if (source.Count<ORTHOFronEstheticsDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal estheticsAnalysis in model.EstheticsAnalysisList)
        {
          entity2.FrontalEstheticId = estheticsAnalysis.FrontalEstheticId;
          entity2.FrontalEstheticsStaticId = estheticsAnalysis.FrontalEstheticsStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.Value = estheticsAnalysis.Value;
          this._uow.Repository<ORTHOFronEstheticsDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal estheticsAnalysis in model.EstheticsAnalysisList)
        {
          entity2.FrontalEstheticId = 0;
          entity2.FrontalEstheticsStaticId = estheticsAnalysis.FrontalEstheticsStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.Value = estheticsAnalysis.Value;
          this._uow.Repository<ORTHOFronEstheticsDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveHoldaway(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOHoldawayDetails> source = this._uow.Repository<ORTHOHoldawayDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOHoldawayDetails, int>((Func<ORTHOHoldawayDetails, int>) (x => x.HoldawayId));
      ORTHOHoldawayDetails entity2 = new ORTHOHoldawayDetails();
      if (source.Count<ORTHOHoldawayDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal holdawayAnalysis in model.HoldawayAnalysisList)
        {
          entity2.HoldawayId = holdawayAnalysis.HoldawayId;
          entity2.HoldawayStaticId = holdawayAnalysis.HoldawayStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = holdawayAnalysis.PreRx;
          entity2.PostRx = holdawayAnalysis.PostRx;
          entity2.During = holdawayAnalysis.During;
          this._uow.Repository<ORTHOHoldawayDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal holdawayAnalysis in model.HoldawayAnalysisList)
        {
          entity2.HoldawayId = 0;
          entity2.HoldawayStaticId = holdawayAnalysis.HoldawayStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = holdawayAnalysis.PreRx;
          entity2.PostRx = holdawayAnalysis.PostRx;
          entity2.During = holdawayAnalysis.During;
          this._uow.Repository<ORTHOHoldawayDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveArnett(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOArnettDetails> source = this._uow.Repository<ORTHOArnettDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOArnettDetails, int>((Func<ORTHOArnettDetails, int>) (x => x.ArnettId));
      ORTHOArnettDetails entity2 = new ORTHOArnettDetails();
      if (source.Count<ORTHOArnettDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal arnettAnalysis in model.ArnettAnalysisList)
        {
          entity2.ArnettId = arnettAnalysis.ArnettId;
          entity2.ArnettStaticId = arnettAnalysis.ArnettStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = arnettAnalysis.PreRx;
          entity2.PostModulation = arnettAnalysis.PostModulation;
          this._uow.Repository<ORTHOArnettDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal arnettAnalysis in model.ArnettAnalysisList)
        {
          entity2.ArnettId = 0;
          entity2.ArnettStaticId = arnettAnalysis.ArnettStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = arnettAnalysis.PreRx;
          entity2.PostModulation = arnettAnalysis.PostModulation;
          this._uow.Repository<ORTHOArnettDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveCranial(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      string whereClause = "OrthoId=" + (object) model.OrthoId;
      IOrderedEnumerable<ORTHOCranialMaxillaDetails> source1 = this._uow.Repository<ORTHOCranialMaxillaDetails>().GetAll(whereClause).OrderBy<ORTHOCranialMaxillaDetails, int>((Func<ORTHOCranialMaxillaDetails, int>) (x => x.CranialMaxId));
      ORTHOCranialMaxillaDetails entity2 = new ORTHOCranialMaxillaDetails();
      if (source1.Count<ORTHOCranialMaxillaDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal maxillaAnalysis in model.MaxillaAnalysisList)
        {
          entity2.CranialMaxId = maxillaAnalysis.CranialMaxId;
          entity2.CranialMaxStaticId = maxillaAnalysis.CranialMaxStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PtValue = maxillaAnalysis.PtValue;
          this._uow.Repository<ORTHOCranialMaxillaDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal maxillaAnalysis in model.MaxillaAnalysisList)
        {
          entity2.CranialMaxId = 0;
          entity2.CranialMaxStaticId = maxillaAnalysis.CranialMaxStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PtValue = maxillaAnalysis.PtValue;
          this._uow.Repository<ORTHOCranialMaxillaDetails>().Add(entity2, false);
        }
      }
      IOrderedEnumerable<ORTHOCranialMandDetails> source2 = this._uow.Repository<ORTHOCranialMandDetails>().GetAll(whereClause).OrderBy<ORTHOCranialMandDetails, int>((Func<ORTHOCranialMandDetails, int>) (x => x.CranialManId));
      ORTHOCranialMandDetails entity3 = new ORTHOCranialMandDetails();
      if (source2.Count<ORTHOCranialMandDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal mandibleAnalysis in model.MandibleAnalysisList)
        {
          entity3.CranialManId = mandibleAnalysis.CranialManId;
          entity3.CranialManStaticId = mandibleAnalysis.CranialManStaticId;
          entity3.OrthoId = model.OrthoId;
          entity3.PtValue = mandibleAnalysis.PtValue;
          this._uow.Repository<ORTHOCranialMandDetails>().Update(entity3, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal mandibleAnalysis in model.MandibleAnalysisList)
        {
          entity3.CranialManId = 0;
          entity3.CranialManStaticId = mandibleAnalysis.CranialManStaticId;
          entity3.OrthoId = model.OrthoId;
          entity3.PtValue = mandibleAnalysis.PtValue;
          this._uow.Repository<ORTHOCranialMandDetails>().Add(entity3, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveMaxMand(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOMaxMandDetails> source = this._uow.Repository<ORTHOMaxMandDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOMaxMandDetails, int>((Func<ORTHOMaxMandDetails, int>) (x => x.MaxManId));
      ORTHOMaxMandDetails entity2 = new ORTHOMaxMandDetails();
      if (source.Count<ORTHOMaxMandDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal maxtoMandAnalysis in model.MaxtoMandAnalysisList)
        {
          entity2.MaxManId = maxtoMandAnalysis.MaxManId;
          entity2.MaxManStaticId = maxtoMandAnalysis.MaxManStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PtValue = maxtoMandAnalysis.PtValue;
          this._uow.Repository<ORTHOMaxMandDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal maxtoMandAnalysis in model.MaxtoMandAnalysisList)
        {
          entity2.MaxManId = 0;
          entity2.MaxManStaticId = maxtoMandAnalysis.MaxManStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PtValue = maxtoMandAnalysis.PtValue;
          this._uow.Repository<ORTHOMaxMandDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveGrowth(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOGrowthDetails> source = this._uow.Repository<ORTHOGrowthDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOGrowthDetails, int>((Func<ORTHOGrowthDetails, int>) (x => x.GrowthId));
      ORTHOGrowthDetails entity2 = new ORTHOGrowthDetails();
      if (source.Count<ORTHOGrowthDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal growthAnalysis in model.GrowthAnalysisList)
        {
          entity2.GrowthId = growthAnalysis.GrowthId;
          entity2.GrowthStaticId = growthAnalysis.GrowthStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PtValue = growthAnalysis.PtValue;
          this._uow.Repository<ORTHOGrowthDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal growthAnalysis in model.GrowthAnalysisList)
        {
          entity2.GrowthId = 0;
          entity2.GrowthStaticId = growthAnalysis.GrowthStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PtValue = growthAnalysis.PtValue;
          this._uow.Repository<ORTHOGrowthDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveDentoLower(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHODentoLowerDetails> source = this._uow.Repository<ORTHODentoLowerDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHODentoLowerDetails, int>((Func<ORTHODentoLowerDetails, int>) (x => x.DentoLowerId));
      ORTHODentoLowerDetails entity2 = new ORTHODentoLowerDetails();
      if (source.Count<ORTHODentoLowerDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal dentoLowerAnalysis in model.DentoLowerAnalysisList)
        {
          entity2.DentoLowerId = dentoLowerAnalysis.DentoLowerId;
          entity2.DentoLowerStaticId = dentoLowerAnalysis.DentoLowerStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = dentoLowerAnalysis.PreRx;
          entity2.PostModulation = dentoLowerAnalysis.PostModulation;
          entity2.MidRx = dentoLowerAnalysis.MidRx;
          entity2.PostRx = dentoLowerAnalysis.PostRx;
          entity2.Retention = dentoLowerAnalysis.Retention;
          entity2.Change = dentoLowerAnalysis.Change;
          this._uow.Repository<ORTHODentoLowerDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal dentoLowerAnalysis in model.DentoLowerAnalysisList)
        {
          entity2.DentoLowerId = 0;
          entity2.DentoLowerStaticId = dentoLowerAnalysis.DentoLowerStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = dentoLowerAnalysis.PreRx;
          entity2.PostModulation = dentoLowerAnalysis.PostModulation;
          entity2.MidRx = dentoLowerAnalysis.MidRx;
          entity2.PostRx = dentoLowerAnalysis.PostRx;
          entity2.Retention = dentoLowerAnalysis.Retention;
          entity2.Change = dentoLowerAnalysis.Change;
          this._uow.Repository<ORTHODentoLowerDetails>().Add(entity2, false);
        }
      }
      IEnumerable<ORTHOCasesheetProperties> all1 = this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
      IEnumerable<ORTHOCasesheetPropertyValues> all2 = this._uow.Repository<ORTHOCasesheetPropertyValues>().GetAll("OrthoId=" + (object) model.OrthoId);
      foreach (PropertyInfo property1 in model.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            ORTHOCasesheetPropertyValues entity3 = new ORTHOCasesheetPropertyValues();
            entity3.OrthoId = model.OrthoId;
            ORTHOCasesheetProperties property = all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              ORTHOCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<ORTHOCasesheetPropertyValues>((Func<ORTHOCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.OrthoId == model.OrthoId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                entity3.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Update(entity3, false);
              }
              else
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Add(entity3, false);
              }
            }
          }
        }
      }
      return model.OrthoId;
    }

    public int SaveSoftTissue(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOSoftTissueDetails> source = this._uow.Repository<ORTHOSoftTissueDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOSoftTissueDetails, int>((Func<ORTHOSoftTissueDetails, int>) (x => x.SoftTissueId));
      ORTHOSoftTissueDetails entity2 = new ORTHOSoftTissueDetails();
      if (source.Count<ORTHOSoftTissueDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal softTissueAnalysis in model.SoftTissueAnalysisList)
        {
          entity2.SoftTissueId = softTissueAnalysis.SoftTissueId;
          entity2.SoftTissueStaticId = softTissueAnalysis.SoftTissueStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PtValue = softTissueAnalysis.PtValue;
          this._uow.Repository<ORTHOSoftTissueDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal softTissueAnalysis in model.SoftTissueAnalysisList)
        {
          entity2.SoftTissueId = 0;
          entity2.SoftTissueStaticId = softTissueAnalysis.SoftTissueStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PtValue = softTissueAnalysis.PtValue;
          this._uow.Repository<ORTHOSoftTissueDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveTweeds(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOTweedsDetails> source = this._uow.Repository<ORTHOTweedsDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOTweedsDetails, int>((Func<ORTHOTweedsDetails, int>) (x => x.TweedsId));
      ORTHOTweedsDetails entity2 = new ORTHOTweedsDetails();
      if (source.Count<ORTHOTweedsDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal tweedsAnalysis in model.TweedsAnalysisList)
        {
          entity2.TweedsId = tweedsAnalysis.TweedsId;
          entity2.TweedsStaticId = tweedsAnalysis.TweedsStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = tweedsAnalysis.PreTreatment;
          entity2.PreSurgical = tweedsAnalysis.PreSurgical;
          entity2.PostAlignment = tweedsAnalysis.PostAlignment;
          entity2.PostSurgical = tweedsAnalysis.PostSurgical;
          entity2.MidTreatment = tweedsAnalysis.MidTreatment;
          entity2.Posttreatment = tweedsAnalysis.Posttreatment;
          entity2.Retention = tweedsAnalysis.Retention;
          this._uow.Repository<ORTHOTweedsDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal tweedsAnalysis in model.TweedsAnalysisList)
        {
          entity2.TweedsId = 0;
          entity2.TweedsStaticId = tweedsAnalysis.TweedsStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = tweedsAnalysis.PreTreatment;
          entity2.PreSurgical = tweedsAnalysis.PreSurgical;
          entity2.PostAlignment = tweedsAnalysis.PostAlignment;
          entity2.PostSurgical = tweedsAnalysis.PostSurgical;
          entity2.MidTreatment = tweedsAnalysis.MidTreatment;
          entity2.Posttreatment = tweedsAnalysis.Posttreatment;
          entity2.Retention = tweedsAnalysis.Retention;
          this._uow.Repository<ORTHOTweedsDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveRicketts(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHORickettsDetails> source = this._uow.Repository<ORTHORickettsDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHORickettsDetails, int>((Func<ORTHORickettsDetails, int>) (x => x.RickettsId));
      ORTHORickettsDetails entity2 = new ORTHORickettsDetails();
      if (source.Count<ORTHORickettsDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal rickettsAnalysis in model.RickettsAnalysisList)
        {
          entity2.RickettsId = rickettsAnalysis.RickettsId;
          entity2.RickettsStaticId = rickettsAnalysis.RickettsStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = rickettsAnalysis.PreRx;
          entity2.PostRx = rickettsAnalysis.PostRx;
          entity2.During = rickettsAnalysis.During;
          this._uow.Repository<ORTHORickettsDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal rickettsAnalysis in model.RickettsAnalysisList)
        {
          entity2.RickettsId = 0;
          entity2.RickettsStaticId = rickettsAnalysis.RickettsStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = rickettsAnalysis.PreRx;
          entity2.PostRx = rickettsAnalysis.PostRx;
          entity2.During = rickettsAnalysis.During;
          this._uow.Repository<ORTHORickettsDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveMcNamara(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOMcNamaraDetails> source = this._uow.Repository<ORTHOMcNamaraDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOMcNamaraDetails, int>((Func<ORTHOMcNamaraDetails, int>) (x => x.McNamaraId));
      ORTHOMcNamaraDetails entity2 = new ORTHOMcNamaraDetails();
      if (source.Count<ORTHOMcNamaraDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal mcNamaraAnalysis in model.McNamaraAnalysisList)
        {
          entity2.McNamaraId = mcNamaraAnalysis.McNamaraId;
          entity2.McNamaraStaticId = mcNamaraAnalysis.McNamaraStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = mcNamaraAnalysis.PreTreatment;
          entity2.PreSurgical = mcNamaraAnalysis.PreSurgical;
          entity2.PostAlignment = mcNamaraAnalysis.PostAlignment;
          entity2.PostSurgical = mcNamaraAnalysis.PostSurgical;
          entity2.MidTreatment = mcNamaraAnalysis.MidTreatment;
          entity2.Posttreatment = mcNamaraAnalysis.Posttreatment;
          entity2.Retention = mcNamaraAnalysis.Retention;
          this._uow.Repository<ORTHOMcNamaraDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal mcNamaraAnalysis in model.McNamaraAnalysisList)
        {
          entity2.McNamaraId = 0;
          entity2.McNamaraStaticId = mcNamaraAnalysis.McNamaraStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreTreatment = mcNamaraAnalysis.PreTreatment;
          entity2.PreSurgical = mcNamaraAnalysis.PreSurgical;
          entity2.PostAlignment = mcNamaraAnalysis.PostAlignment;
          entity2.PostSurgical = mcNamaraAnalysis.PostSurgical;
          entity2.MidTreatment = mcNamaraAnalysis.MidTreatment;
          entity2.Posttreatment = mcNamaraAnalysis.Posttreatment;
          entity2.Retention = mcNamaraAnalysis.Retention;
          this._uow.Repository<ORTHOMcNamaraDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveBjroks(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOBjroksDetails> source = this._uow.Repository<ORTHOBjroksDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOBjroksDetails, int>((Func<ORTHOBjroksDetails, int>) (x => x.BjroksStaticId));
      ORTHOBjroksDetails entity2 = new ORTHOBjroksDetails();
      if (source.Count<ORTHOBjroksDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal bjroksAnalysis in model.BjroksAnalysisList)
        {
          entity2.BjroksId = bjroksAnalysis.BjroksStaticId;
          entity2.BjroksStaticId = bjroksAnalysis.BjroksStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = bjroksAnalysis.PreRx;
          entity2.PostRx = bjroksAnalysis.PostRx;
          entity2.During = bjroksAnalysis.During;
          this._uow.Repository<ORTHOBjroksDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal bjroksAnalysis in model.BjroksAnalysisList)
        {
          entity2.BjroksId = 0;
          entity2.BjroksStaticId = bjroksAnalysis.BjroksStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = bjroksAnalysis.PreRx;
          entity2.PostRx = bjroksAnalysis.PostRx;
          entity2.During = bjroksAnalysis.During;
          this._uow.Repository<ORTHOBjroksDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveSagittal(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOSagittalDetails> source = this._uow.Repository<ORTHOSagittalDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOSagittalDetails, int>((Func<ORTHOSagittalDetails, int>) (x => x.SagittalId));
      ORTHOSagittalDetails entity2 = new ORTHOSagittalDetails();
      if (source.Count<ORTHOSagittalDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal sagittalAnalysis in model.SagittalAnalysisList)
        {
          entity2.SagittalId = sagittalAnalysis.SagittalId;
          entity2.SagittalStaticId = sagittalAnalysis.SagittalStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRxMeasurement = sagittalAnalysis.PreRxMeasurement;
          entity2.PreRxClass = sagittalAnalysis.PreRxClass;
          entity2.PostRxMeasurement = sagittalAnalysis.PostRxMeasurement;
          entity2.PostRxClass = sagittalAnalysis.PostRxClass;
          entity2.SurgeryClass = sagittalAnalysis.SurgeryClass;
          entity2.SurgeryMeasurement = sagittalAnalysis.SurgeryMeasurement;
          entity2.RetentionMeasurement = sagittalAnalysis.RetentionMeasurement;
          entity2.RetentionClass = sagittalAnalysis.RetentionClass;
          entity2.ChangeMeasurement = sagittalAnalysis.ChangeMeasurement;
          entity2.ChangeClass = sagittalAnalysis.ChangeClass;
          this._uow.Repository<ORTHOSagittalDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal sagittalAnalysis in model.SagittalAnalysisList)
        {
          entity2.SagittalId = 0;
          entity2.SagittalStaticId = sagittalAnalysis.SagittalStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRxMeasurement = sagittalAnalysis.PreRxMeasurement;
          entity2.PreRxClass = sagittalAnalysis.PreRxClass;
          entity2.PostRxMeasurement = sagittalAnalysis.PostRxMeasurement;
          entity2.PostRxClass = sagittalAnalysis.PostRxClass;
          entity2.SurgeryClass = sagittalAnalysis.SurgeryClass;
          entity2.SurgeryMeasurement = sagittalAnalysis.SurgeryMeasurement;
          entity2.RetentionMeasurement = sagittalAnalysis.RetentionMeasurement;
          entity2.RetentionClass = sagittalAnalysis.RetentionClass;
          entity2.ChangeMeasurement = sagittalAnalysis.ChangeMeasurement;
          entity2.ChangeClass = sagittalAnalysis.ChangeClass;
          this._uow.Repository<ORTHOSagittalDetails>().Add(entity2, false);
        }
      }
      IEnumerable<ORTHOCasesheetProperties> all1 = this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
      IEnumerable<ORTHOCasesheetPropertyValues> all2 = this._uow.Repository<ORTHOCasesheetPropertyValues>().GetAll("OrthoId=" + (object) model.OrthoId);
      foreach (PropertyInfo property1 in model.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            ORTHOCasesheetPropertyValues entity3 = new ORTHOCasesheetPropertyValues();
            entity3.OrthoId = model.OrthoId;
            ORTHOCasesheetProperties property = all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              ORTHOCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<ORTHOCasesheetPropertyValues>((Func<ORTHOCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.OrthoId == model.OrthoId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                entity3.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Update(entity3, false);
              }
              else
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Add(entity3, false);
              }
            }
          }
        }
      }
      return model.OrthoId;
    }

    public int SaveDiscrepancy(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHODiscrepancyDetails> source = this._uow.Repository<ORTHODiscrepancyDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHODiscrepancyDetails, int>((Func<ORTHODiscrepancyDetails, int>) (x => x.DiscrepancyId));
      ORTHODiscrepancyDetails entity2 = new ORTHODiscrepancyDetails();
      if (source.Count<ORTHODiscrepancyDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal discrepancyAnalysis in model.DiscrepancyAnalysisList)
        {
          entity2.DiscrepancyId = discrepancyAnalysis.DiscrepancyId;
          entity2.DiscrepancyStaticId = discrepancyAnalysis.DiscrepancyStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = discrepancyAnalysis.PreRx;
          entity2.PostRx = discrepancyAnalysis.PostRx;
          entity2.PostModulation = discrepancyAnalysis.PostModulation;
          entity2.Retention = discrepancyAnalysis.Retention;
          entity2.Change = discrepancyAnalysis.Change;
          this._uow.Repository<ORTHODiscrepancyDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal discrepancyAnalysis in model.DiscrepancyAnalysisList)
        {
          entity2.DiscrepancyId = 0;
          entity2.DiscrepancyStaticId = discrepancyAnalysis.DiscrepancyStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = discrepancyAnalysis.PreRx;
          entity2.PostRx = discrepancyAnalysis.PostRx;
          entity2.PostModulation = discrepancyAnalysis.PostModulation;
          entity2.Retention = discrepancyAnalysis.Retention;
          entity2.Change = discrepancyAnalysis.Change;
          this._uow.Repository<ORTHODiscrepancyDetails>().Add(entity2, false);
        }
      }
      IEnumerable<ORTHOCasesheetProperties> all1 = this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
      IEnumerable<ORTHOCasesheetPropertyValues> all2 = this._uow.Repository<ORTHOCasesheetPropertyValues>().GetAll("OrthoId=" + (object) model.OrthoId);
      foreach (PropertyInfo property1 in model.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            ORTHOCasesheetPropertyValues entity3 = new ORTHOCasesheetPropertyValues();
            entity3.OrthoId = model.OrthoId;
            ORTHOCasesheetProperties property = all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              ORTHOCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<ORTHOCasesheetPropertyValues>((Func<ORTHOCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.OrthoId == model.OrthoId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                entity3.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Update(entity3, false);
              }
              else
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Add(entity3, false);
              }
            }
          }
        }
      }
      return model.OrthoId;
    }

    public int SaveVerticalRelation(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOVerticalDetails> source = this._uow.Repository<ORTHOVerticalDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOVerticalDetails, int>((Func<ORTHOVerticalDetails, int>) (x => x.VerticalId));
      ORTHOVerticalDetails entity2 = new ORTHOVerticalDetails();
      if (source.Count<ORTHOVerticalDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal verticalAnalysis in model.VerticalAnalysisList)
        {
          entity2.VerticalId = verticalAnalysis.VerticalId;
          entity2.VerticalStaticId = verticalAnalysis.VerticalStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = verticalAnalysis.PreRx;
          entity2.PostRx = verticalAnalysis.PostRx;
          entity2.MidRx = verticalAnalysis.MidRx;
          entity2.PostModulation = verticalAnalysis.PostModulation;
          entity2.Retention = verticalAnalysis.Retention;
          entity2.Change = verticalAnalysis.Change;
          this._uow.Repository<ORTHOVerticalDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal verticalAnalysis in model.VerticalAnalysisList)
        {
          entity2.VerticalId = 0;
          entity2.VerticalStaticId = verticalAnalysis.VerticalStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = verticalAnalysis.PreRx;
          entity2.PostRx = verticalAnalysis.PostRx;
          entity2.MidRx = verticalAnalysis.MidRx;
          entity2.PostModulation = verticalAnalysis.PostModulation;
          entity2.Retention = verticalAnalysis.Retention;
          entity2.Change = verticalAnalysis.Change;
          this._uow.Repository<ORTHOVerticalDetails>().Add(entity2, false);
        }
      }
      IEnumerable<ORTHOCasesheetProperties> all1 = this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
      IEnumerable<ORTHOCasesheetPropertyValues> all2 = this._uow.Repository<ORTHOCasesheetPropertyValues>().GetAll("OrthoId=" + (object) model.OrthoId);
      foreach (PropertyInfo property1 in model.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            ORTHOCasesheetPropertyValues entity3 = new ORTHOCasesheetPropertyValues();
            entity3.OrthoId = model.OrthoId;
            ORTHOCasesheetProperties property = all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              ORTHOCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<ORTHOCasesheetPropertyValues>((Func<ORTHOCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.OrthoId == model.OrthoId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                entity3.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Update(entity3, false);
              }
              else
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Add(entity3, false);
              }
            }
          }
        }
      }
      return model.OrthoId;
    }

    public int SaveComposite(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOCompositeDetails> source = this._uow.Repository<ORTHOCompositeDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOCompositeDetails, int>((Func<ORTHOCompositeDetails, int>) (x => x.CompositeId));
      ORTHOCompositeDetails entity2 = new ORTHOCompositeDetails();
      if (source.Count<ORTHOCompositeDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal compositeAnalysis in model.CompositeAnalysisList)
        {
          entity2.CompositeId = compositeAnalysis.CompositeId;
          entity2.CompositeStaticId = compositeAnalysis.CompositeStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.Actual = compositeAnalysis.Actual;
          entity2.CompositeInference = compositeAnalysis.CompositeInference;
          this._uow.Repository<ORTHOCompositeDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal compositeAnalysis in model.CompositeAnalysisList)
        {
          entity2.CompositeId = 0;
          entity2.CompositeStaticId = compositeAnalysis.CompositeStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.Actual = compositeAnalysis.Actual;
          entity2.CompositeInference = compositeAnalysis.CompositeInference;
          this._uow.Repository<ORTHOCompositeDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveDentoUpper(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHODentoUpperDetails> source = this._uow.Repository<ORTHODentoUpperDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHODentoUpperDetails, int>((Func<ORTHODentoUpperDetails, int>) (x => x.DentoUpperId));
      ORTHODentoUpperDetails entity2 = new ORTHODentoUpperDetails();
      if (source.Count<ORTHODentoUpperDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal dentoUpperAnalysis in model.DentoUpperAnalysisList)
        {
          entity2.DentoUpperId = dentoUpperAnalysis.DentoUpperId;
          entity2.DentoUpperStaticId = dentoUpperAnalysis.DentoUpperStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = dentoUpperAnalysis.PreRx;
          entity2.PostRx = dentoUpperAnalysis.PostRx;
          entity2.MidRx = dentoUpperAnalysis.MidRx;
          entity2.PostModulation = dentoUpperAnalysis.PostModulation;
          entity2.Retention = dentoUpperAnalysis.Retention;
          entity2.Change = dentoUpperAnalysis.Change;
          this._uow.Repository<ORTHODentoUpperDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal dentoUpperAnalysis in model.DentoUpperAnalysisList)
        {
          entity2.DentoUpperId = 0;
          entity2.DentoUpperStaticId = dentoUpperAnalysis.DentoUpperStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = dentoUpperAnalysis.PreRx;
          entity2.PostRx = dentoUpperAnalysis.PostRx;
          entity2.MidRx = dentoUpperAnalysis.MidRx;
          entity2.PostModulation = dentoUpperAnalysis.PostModulation;
          entity2.Retention = dentoUpperAnalysis.Retention;
          entity2.Change = dentoUpperAnalysis.Change;
          this._uow.Repository<ORTHODentoUpperDetails>().Add(entity2, false);
        }
      }
      IEnumerable<ORTHOCasesheetProperties> all1 = this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
      IEnumerable<ORTHOCasesheetPropertyValues> all2 = this._uow.Repository<ORTHOCasesheetPropertyValues>().GetAll("OrthoId=" + (object) model.OrthoId);
      foreach (PropertyInfo property1 in model.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            ORTHOCasesheetPropertyValues entity3 = new ORTHOCasesheetPropertyValues();
            entity3.OrthoId = model.OrthoId;
            ORTHOCasesheetProperties property = all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              ORTHOCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<ORTHOCasesheetPropertyValues>((Func<ORTHOCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.OrthoId == model.OrthoId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                entity3.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Update(entity3, false);
              }
              else
              {
                entity3.PropId = property.PropertyId;
                entity3.PropValues = obj.ToString();
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Add(entity3, false);
              }
            }
          }
        }
      }
      return model.OrthoId;
    }

    public int SaveSkeletal(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOSkeletalDetails> source = this._uow.Repository<ORTHOSkeletalDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOSkeletalDetails, int>((Func<ORTHOSkeletalDetails, int>) (x => x.SkeletalId));
      ORTHOSkeletalDetails entity2 = new ORTHOSkeletalDetails();
      if (source.Count<ORTHOSkeletalDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal skeletalAnalysis in model.SKeletalAnalysisList)
        {
          entity2.SkeletalId = skeletalAnalysis.SkeletalId;
          entity2.SkeletalStaticId = skeletalAnalysis.SkeletalStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = skeletalAnalysis.PreRx;
          entity2.PostRx = skeletalAnalysis.PostRx;
          entity2.PostModulation = skeletalAnalysis.PostModulation;
          entity2.Retention = skeletalAnalysis.Retention;
          entity2.Change = skeletalAnalysis.Change;
          this._uow.Repository<ORTHOSkeletalDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal skeletalAnalysis in model.SKeletalAnalysisList)
        {
          entity2.SkeletalId = 0;
          entity2.SkeletalStaticId = skeletalAnalysis.SkeletalStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = skeletalAnalysis.PreRx;
          entity2.PostRx = skeletalAnalysis.PostRx;
          entity2.PostModulation = skeletalAnalysis.PostModulation;
          entity2.Retention = skeletalAnalysis.Retention;
          entity2.Change = skeletalAnalysis.Change;
          this._uow.Repository<ORTHOSkeletalDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveJawBases(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IOrderedEnumerable<ORTHOJawBasesDetails> source = this._uow.Repository<ORTHOJawBasesDetails>().GetAll("OrthoId=" + (object) model.OrthoId).OrderBy<ORTHOJawBasesDetails, int>((Func<ORTHOJawBasesDetails, int>) (x => x.JawBasesId));
      ORTHOJawBasesDetails entity2 = new ORTHOJawBasesDetails();
      if (source.Count<ORTHOJawBasesDetails>() != 0)
      {
        foreach (OrthoAnalysisViewModal divergenceAnalysis in model.DivergenceAnalysisList)
        {
          entity2.JawBasesId = divergenceAnalysis.JawBasesId;
          entity2.JawBasesStaticId = divergenceAnalysis.JawBasesStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = divergenceAnalysis.PreRx;
          entity2.PostRx = divergenceAnalysis.PostRx;
          entity2.PostModulation = divergenceAnalysis.PostModulation;
          entity2.Retention = divergenceAnalysis.Retention;
          entity2.Change = divergenceAnalysis.Change;
          this._uow.Repository<ORTHOJawBasesDetails>().Update(entity2, false);
        }
      }
      else
      {
        foreach (OrthoAnalysisViewModal divergenceAnalysis in model.DivergenceAnalysisList)
        {
          entity2.JawBasesId = 0;
          entity2.JawBasesStaticId = divergenceAnalysis.JawBasesStaticId;
          entity2.OrthoId = model.OrthoId;
          entity2.PreRx = divergenceAnalysis.PreRx;
          entity2.PostRx = divergenceAnalysis.PostRx;
          entity2.PostModulation = divergenceAnalysis.PostModulation;
          entity2.Retention = divergenceAnalysis.Retention;
          entity2.Change = divergenceAnalysis.Change;
          this._uow.Repository<ORTHOJawBasesDetails>().Add(entity2, false);
        }
      }
      return model.OrthoId;
    }

    public int SaveModalAnalysis(OrthoAnalysisViewModal model)
    {
      ORTHOInferenceDetails entity1 = new ORTHOInferenceDetails();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<OrthoAnalysisViewModal, ORTHOInferenceDetails>()));
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.InferenceforPatient, (object) model.OrthoId, (object) model.AnalysisId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() != 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          entity1.InferenceId = inferenceDetails.InferenceId;
          entity1.Inference = model.Inference;
          this._uow.Repository<ORTHOInferenceDetails>().Update(entity1, false);
        }
      }
      else
        this._uow.Repository<ORTHOInferenceDetails>().Add(Mapper.Map<OrthoAnalysisViewModal, ORTHOInferenceDetails>(model), false);
      IEnumerable<ORTHOCasesheetProperties> all1 = this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
      IEnumerable<ORTHOCasesheetPropertyValues> all2 = this._uow.Repository<ORTHOCasesheetPropertyValues>().GetAll("OrthoId=" + (object) model.OrthoId);
      foreach (PropertyInfo property1 in model.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            ORTHOCasesheetPropertyValues entity2 = new ORTHOCasesheetPropertyValues();
            entity2.OrthoId = model.OrthoId;
            ORTHOCasesheetProperties property = all1.FirstOrDefault<ORTHOCasesheetProperties>((Func<ORTHOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              ORTHOCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<ORTHOCasesheetPropertyValues>((Func<ORTHOCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.OrthoId == model.OrthoId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                entity2.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Update(entity2, false);
              }
              else
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
      }
      IEnumerable<ORTHOCasesheetProperties> properties = this.GetProperties();
      model.Proplist = properties;
      return model.OrthoId;
    }

    public OrthoAnalysisViewModal BindReportAnalysisDetails(int OrthoId)
    {
      OrthoAnalysisViewModal analysisViewModal = this._uow.Repository<OrthoAnalysisViewModal>().GetEntitiesBySql(string.Format("exec GetORTHOCasesheetReport {0}", (object) OrthoId)).FirstOrDefault<OrthoAnalysisViewModal>();
      analysisViewModal.patientInformationViewModel = new PatientInformationViewModel()
      {
        OpNo = analysisViewModal.OpNo,
        PatientName = analysisViewModal.PatientName,
        AgeGender = analysisViewModal.Age.ToString() + "/" + (object) (Gender) analysisViewModal.GenderId,
        Area = analysisViewModal.Area,
        Phone = analysisViewModal.Phone
      };
      IEnumerable<ORTHOInferenceDetails> entitiesBySql = this._uow.Repository<ORTHOInferenceDetails>().GetEntitiesBySql(string.Format(Queries.GetInferencebyOrthoId, (object) OrthoId));
      if (entitiesBySql.Count<ORTHOInferenceDetails>() > 0)
      {
        foreach (ORTHOInferenceDetails inferenceDetails in entitiesBySql)
        {
          if (inferenceDetails.AnalysisId == 1)
          {
            analysisViewModal.SteinerAnalysisList = this.SteinerAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.AnalysisDisplayName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.Inference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 3)
          {
            analysisViewModal.DownsAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.DownsAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.DownsAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.DownsInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 4)
          {
            analysisViewModal.SchwarzAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.SchwarzAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.SchwarzAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.SchwarzInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 5)
          {
            analysisViewModal.RakosiAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.RakosiAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.RakosiAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.RakosiInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 6)
          {
            analysisViewModal.BurstoneHardAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.BurstoneHardAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.HardAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.HardInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 7)
          {
            analysisViewModal.BurstoneSoftAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.BurstoneSoftAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.SoftAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.SoftInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 8)
          {
            analysisViewModal.GrummonsAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.GrummonsAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.GrummonsAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.GrummonsInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 9)
          {
            analysisViewModal.EstheticsAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.EstheticsAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.EstheticsAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.EstheticsInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 10)
          {
            analysisViewModal.HoldawayAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.HoldawayAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.HoldawayAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.HoldawayInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 17)
          {
            analysisViewModal.TweedsAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.TweedsAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.TweedsAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.TweedsInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 18)
          {
            analysisViewModal.RickettsAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.RickettsAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.RickettsAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.RickettsInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 19)
          {
            analysisViewModal.McNamaraAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this.McNamaraAnalysisList(OrthoId).ToList<OrthoAnalysisViewModal>();
            analysisViewModal.McNamaraAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.McNamaraInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 24)
          {
            analysisViewModal.WitsAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.WitsInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 27)
          {
            analysisViewModal.LinderAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.LinderInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 33)
          {
            analysisViewModal.MoyerAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.MoyerInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 34)
          {
            analysisViewModal.TanakaAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.TanakaInference = inferenceDetails.Inference;
          }
          else if (inferenceDetails.AnalysisId == 35)
          {
            analysisViewModal.RadiographicAnalysisName = this._uow.Repository<ORTHOAnalysis>().Get(inferenceDetails.AnalysisId).AnalysisDisplayName;
            analysisViewModal.RadiographicInference = inferenceDetails.Inference;
          }
        }
      }
      return analysisViewModal;
    }
  }
}
