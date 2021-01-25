// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PROSTreatmentViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DIMS.ViewModels
{
  public class PROSTreatmentViewModel : EntityBase
  {
    public IEnumerable<PROSTreatmentViewModel> ProsList;
    public IEnumerable<PROSTreatmentViewModel> CDRegPatientList;
    public IEnumerable<PROSTreatmentViewModel> RPDRegPatientList;
    public IEnumerable<PROSTreatmentViewModel> FPDRegPatientList;
    public IEnumerable<PROSTreatmentViewModel> MFPRegPatientList;
    public IEnumerable<PROSTreatmentViewModel> DIMRegPatientList;

    [PrimaryKey]
    public int TreatmentId { get; set; }

    [Display(Name = "Treatment")]
    public string TreatmentName { get; set; }

    public string Visibility { get; set; }

    public int PatientId { get; set; }

    public long AllotId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int ProsthoCDId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int ProsthoDIMId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int ProsthoFPDId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int ProsthoMFPId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int ProsthoRPDId { get; set; }

    [Display(Name = "PROSCD No")]
    public string ProsthoCDNo { get; set; }

    [Display(Name = "PROSDIM No")]
    public string ProsthoDIMNo { get; set; }

    [Display(Name = "PROSFPD No")]
    public string ProsthoFPDNo { get; set; }

    [Display(Name = "PROSMFP No")]
    public string ProsthoMFPNo { get; set; }

    [Display(Name = "PROSRPD No")]
    public string ProsthoRPDNo { get; set; }

    [Display(Name = "Date")]
    public string ProsthoCDdateDisplay { get; set; }

    [Display(Name = "Date")]
    public string prosthoDIMdateDisplay { get; set; }

    [Display(Name = "Date")]
    public string prosthoFPDdateDisplay { get; set; }

    [Display(Name = "Date")]
    public string prosthoMFPdateDisplay { get; set; }

    [Display(Name = "Date")]
    public string prosthoRPDdateDisplay { get; set; }

    [Display(Name = "Chief complaint")]
    public string prosCDchiefcomplaint { get; set; }

    [Display(Name = "Chief complaint")]
    public string prosDIMchiefcomplaint { get; set; }

    [Display(Name = "Chief complaint")]
    public string prosFPDchiefcomplaint { get; set; }

    [Display(Name = "Chief complaint")]
    public string prosMFPchiefcomplaint { get; set; }

    [Display(Name = "Chief complaint")]
    public string prosRPDchiefcomplaint { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public StudentAllotmentViewModel studentAllotmentViewModel { get; set; }
  }
}
