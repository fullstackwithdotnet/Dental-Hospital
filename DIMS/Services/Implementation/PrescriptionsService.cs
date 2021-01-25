// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.PrescriptionsService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class PrescriptionsService : ServiceBase<Prescriptions>, IPrescriptionsService, IService<Prescriptions>
  {
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;

    public PrescriptionsService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
    }

    public PrescriptionsViewModel LoadPrescriptionsList()
    {
      return new PrescriptionsViewModel()
      {
        Typelist = this._Dropdownservice.GetCodesById(274)
      };
    }

    public IEnumerable<PrescriptionsViewModel> LoadPrescriptionsList(int patientId, int DeptId, int TreatmentId)
    {
      return (IEnumerable<PrescriptionsViewModel>) this._uow.Repository<PrescriptionsViewModel>().GetEntitiesBySql(string.Format(Queries.LoadPrescription, (object) patientId, (object) DeptId, (object) TreatmentId)).ToList<PrescriptionsViewModel>();
    }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionsList(int PatientId)
    {
      return (IEnumerable<PrescriptionsViewModel>) this._uow.Repository<PrescriptionsViewModel>().GetEntitiesBySql(string.Format(Queries.PreviousPrescriptions, (object) PatientId)).ToList<PrescriptionsViewModel>();
    }

    public PrescriptionsViewModel PrescriptionReport(long AllotId)
    {
      PrescriptionsViewModel prescriptionsViewModel1 = new PrescriptionsViewModel();
      IEnumerable<PrescriptionsViewModel> list = (IEnumerable<PrescriptionsViewModel>) this._uow.Repository<PrescriptionsViewModel>().GetEntitiesBySql(string.Format(Queries.GetPrescriptionDetailsforPatient, (object) AllotId)).ToList<PrescriptionsViewModel>();
      PrescriptionsViewModel prescriptionsViewModel2 = list.First<PrescriptionsViewModel>();
      prescriptionsViewModel2.PrescriptionsList = list;
      return prescriptionsViewModel2;
    }

    public int SavePrescriptions(IEnumerable<PrescriptionsViewModel> model, Prescriptions prescriptionsDetails)
    {
      if (model != null)
      {
        Prescriptions entity = new Prescriptions();
        entity.PatientId = prescriptionsDetails.PatientId;
        entity.DoctorId = prescriptionsDetails.DoctorId;
        entity.AllotId = prescriptionsDetails.AllotId;
        entity.StudentId = prescriptionsDetails.StudentId;
        entity.DeptId = prescriptionsDetails.DeptId;
        entity.ReferredTreatmentId = prescriptionsDetails.ReferredTreatmentId;
        foreach (PrescriptionsViewModel prescriptionsViewModel in model)
        {
          if (prescriptionsViewModel != null && prescriptionsViewModel.PrescriptionId == 0)
          {
            Prescriptions prescriptions1 = entity;
            DateTime now = DateTime.Now;
            DateTime? nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            prescriptions1.PrescriptionDate = nullable1;
            entity.TypeId = prescriptionsViewModel.TypeId;
            entity.PresMedication = prescriptionsViewModel.PresMedication;
            entity.PresStrength = prescriptionsViewModel.PresStrength;
            entity.PrescriptionQty = prescriptionsViewModel.PrescriptionQty;
            entity.PresFrequency = prescriptionsViewModel.PresFrequency;
            entity.PresDays = prescriptionsViewModel.PresDays;
            entity.PresTimes = prescriptionsViewModel.PresTimes;
            entity.PresNotes = prescriptionsViewModel.PresNotes;
            Prescriptions prescriptions2 = entity;
            now = DateTime.Now;
            DateTime? nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            prescriptions2.CreatedDate = nullable2;
            entity.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            this._uow.Repository<Prescriptions>().Add(entity, false);
          }
        }
      }
      return 0;
    }

    public MedicalAlertViewModel BindMedicalAlert(int PatientId)
    {
      MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
      medicalAlertViewModel1.MedicalAlertList = (IEnumerable<MedicalAlertViewModel>) this._uow.Repository<MedicalAlertViewModel>().GetEntitiesBySql(string.Format(Queries.GetMedicalAlertvaluesForOMR, (object) PatientId, (object) 3, (object) 24)).ToList<MedicalAlertViewModel>();
      string str = string.Empty;
      if (medicalAlertViewModel1.MedicalAlertList.Count<MedicalAlertViewModel>() > 0)
      {
        foreach (MedicalAlertViewModel medicalAlert in medicalAlertViewModel1.MedicalAlertList)
          str = !(str == "") ? str + ",- -" + medicalAlert.PROPVALUES : medicalAlert.PROPVALUES;
        medicalAlertViewModel1.PROPVALUES = str;
      }
      MedicalAlertViewModel medicalAlertViewModel2 = this._uow.Repository<MedicalAlertViewModel>().GetEntitiesBySql(string.Format(Queries.GetMLCValueforPatient, (object) PatientId, (object) 10)).FirstOrDefault<MedicalAlertViewModel>();
      if (medicalAlertViewModel2 != null)
        medicalAlertViewModel1.MLCValue = medicalAlertViewModel2.PROPVALUES;
      return medicalAlertViewModel1;
    }
  }
}
