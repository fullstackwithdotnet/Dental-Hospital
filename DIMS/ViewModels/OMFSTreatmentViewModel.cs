// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OMFSTreatmentViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class OMFSTreatmentViewModel : EntityBase
  {
    public IEnumerable<OMFSTreatmentViewModel> OmfsList = (IEnumerable<OMFSTreatmentViewModel>) new List<OMFSTreatmentViewModel>();
    public IEnumerable<OMFSTreatmentViewModel> OmfsopList = (IEnumerable<OMFSTreatmentViewModel>) new List<OMFSTreatmentViewModel>();
    public IEnumerable<OMFSTreatmentViewModel> OmfsipList = (IEnumerable<OMFSTreatmentViewModel>) new List<OMFSTreatmentViewModel>();

    [PrimaryKey]
    public int TreatmentId { get; set; }

    public int DeptId { get; set; }

    [Display(Name = "Treatment Names")]
    public string TreatmentName { get; set; }

    public string TreatmentCode { get; set; }

    public string Visibility { get; set; }

    public int PatientId { get; set; }

    public long AllotId { get; set; }

    public long TreatmentReferredId { get; set; }

    public int OMFSOpId { get; set; }

    public int ScheduleId { get; set; }

    [Display(Name = "Schedule Date")]
    public string OMFSOpDateDisplay { get; set; }

    [Display(Name = "Op No")]
    public string OMFSOpNo { get; set; }

    [Display(Name = "Chief Complaint")]
    public string OpChiefComplaint { get; set; }

    public int OMFSIpId { get; set; }

    [Display(Name = "Date")]
    public string OMFSIpDateDisplay { get; set; }

    [Display(Name = "Ip No")]
    public string OMFSIpNo { get; set; }

    [Display(Name = "Chief Complaint")]
    public string IpChiefComplaint { get; set; }

    [Display(Name = "Operation Name")]
    public string Operation { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public StudentAllotmentViewModel studentAllotmentViewModel { get; set; }
  }
}
