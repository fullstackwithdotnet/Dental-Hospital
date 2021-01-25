// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.DCardsSearchViewModal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class DCardsSearchViewModal
  {
    public string From_Date { get; set; }

    public string To_Date { get; set; }

    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Display(Name = "Total Count")]
    public int TotalNewVisitCount { get; set; }

    [Display(Name = "Total Count")]
    public int TotalRevisitCount { get; set; }

    [Display(Name = "Actual Count")]
    public int NewVisitOrginalCount { get; set; }

    [Display(Name = "Actual Count")]
    public int RevisitOrginalCount { get; set; }

    [Display(Name = "Dcard Count")]
    public int NewVisitDcardCount { get; set; }

    [Display(Name = "Dcard Count")]
    public int RevisitDcardCount { get; set; }

    [Display(Name = "Count")]
    public int DCardCount { get; set; }

    [Display(Name = "Count")]
    public int DCardRevisitCount { get; set; }

    public int DeptId { get; set; }

    [Display(Name = "No of Count")]
    public int OPDNoofCount { get; set; }

    public string MandatoryDummy { get; set; }

    [Display(Name = "For Pedo")]
    public bool ChkForPedoPatient { get; set; }

    public IEnumerable<MASCategory> Categorylist { get; set; }

    [Display(Name = "OP Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime Reg { get; set; }

    [Display(Name = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime OPDDateRange { get; set; }

    public List<OPDCardsSearchDetails> SearchDetails { get; set; }

    [Display(Name = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime OMRDateRange { get; set; }

    [Display(Name = "No of Count")]
    public int OMRNoofCount { get; set; }

    public List<DCardSearchDetails> SearchOMRDetails { get; set; }

    [Display(Name = "OMR Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime OMRReg { get; set; }

    [Display(Name = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime OMFSDateRange { get; set; }

    public List<DCardSearchDetails> SearchOMFSDetails { get; set; }

    [Display(Name = "No of Count")]
    public int OMFSNoofCount { get; set; }

    [Display(Name = "OMFS Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime OMFSReg { get; set; }

    [Display(Name = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime PERIODateRange { get; set; }

    [Display(Name = "No of Count")]
    public int PERIONoofCount { get; set; }

    public List<DCardSearchDetails> SearchPERIODetails { get; set; }

    [Display(Name = "Perio Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime PERIOReg { get; set; }

    [Display(Name = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime CONSDateRange { get; set; }

    [Display(Name = "No of Count")]
    public int CONSNoofCount { get; set; }

    public List<DCardSearchDetails> SearchCONSDetails { get; set; }

    [Display(Name = "Treatment")]
    public int PROSTreatmentId { get; set; }

    public IEnumerable<TreatmentTypes> TreatmentTypeDetails { get; set; }

    public int FromdeptId { get; set; }

    public IEnumerable<MASDepartment> FromDepartmentList { get; set; }

    [Display(Name = "Department")]
    public int ToDeptId { get; set; }

    public IEnumerable<MASDepartment> ToDepartmentList { get; set; }

    public IEnumerable<int> FromSelectPatient { get; set; }

    public IEnumerable<int> ToSelectPatient { get; set; }

    public string SelectedPatient { get; set; }

    public IEnumerable<OPDPatientRegistration> FromRefPatientList { get; set; }

    public IEnumerable<OPDPatientRegistration> ToRefPatientList { get; set; }

    [Display(Name = "Referral Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime REFERRALReg { get; set; }

    [Display(Name = "Search Form")]
    public bool IsSearchEnable { get; set; }

    public bool IsReportEnable { get; set; }

    public int DummyEnableId { get; set; }

    [Display(Name = "Report Form")]
    public string ReportForm { get; set; }

    public string SearchForm { get; set; }
  }
}
