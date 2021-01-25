// Decompiled with JetBrains decompiler
// Type: CStone.Entities.PROSRPDCasesheet
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("PROSRPDCasesheet")]
  public class PROSRPDCasesheet : EntityBase
  {
    [PrimaryKey]
    public int ProsthoRPDId { get; set; }

    public string ProsthoRPDNo { get; set; }

    public int PatientId { get; set; }

    public int DietId { get; set; }

    public DateTime? ProsthoRPDDate { get; set; }

    public string ChiefComplaint { get; set; }

    public string T18 { get; set; }

    public string T17 { get; set; }

    public string T16 { get; set; }

    public string T15 { get; set; }

    public string T14 { get; set; }

    public string T13 { get; set; }

    public string T12 { get; set; }

    public string T11 { get; set; }

    public string T21 { get; set; }

    public string T22 { get; set; }

    public string T23 { get; set; }

    public string T24 { get; set; }

    public string T25 { get; set; }

    public string T26 { get; set; }

    public string T27 { get; set; }

    public string T28 { get; set; }

    public string T48 { get; set; }

    public string T47 { get; set; }

    public string T46 { get; set; }

    public string T45 { get; set; }

    public string T44 { get; set; }

    public string T43 { get; set; }

    public string T42 { get; set; }

    public string T41 { get; set; }

    public string T31 { get; set; }

    public string T32 { get; set; }

    public string T33 { get; set; }

    public string T34 { get; set; }

    public string T35 { get; set; }

    public string T36 { get; set; }

    public string T37 { get; set; }

    public string T38 { get; set; }

    public DateTime? PreliminaryImpressionDate { get; set; }

    public DateTime? DiagnosticCastsDate { get; set; }

    public DateTime? DiagnosticSurveyDate { get; set; }

    public DateTime? MouthPreparationDate { get; set; }

    public DateTime? CustomTrayFDate { get; set; }

    public DateTime? BorderMoldingDate { get; set; }

    public DateTime? FinalImpressionDate { get; set; }

    public DateTime? MasterCastDate { get; set; }

    public DateTime? SurveyingDate { get; set; }

    public DateTime? DesigningDate { get; set; }

    public DateTime? WaxPatternsDate { get; set; }

    public DateTime? CastingDate { get; set; }

    public DateTime? TrimmingPolishingDate { get; set; }

    public DateTime? MetalTrialDate { get; set; }

    public DateTime? DentureBaseOcclusalDate { get; set; }

    public DateTime? BiteRegistrationDate { get; set; }

    public DateTime? TeethArrangementDate { get; set; }

    public DateTime? TryInDate { get; set; }

    public DateTime? DentureProcessingDate { get; set; }

    public DateTime? InsertionDate { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public DateTime? LastVisitedDate { get; set; }

    public bool Approval1 { get; set; }

    public bool Approval2 { get; set; }

    public bool Approval3 { get; set; }

    public bool SendForApproval1 { get; set; }

    public bool SendForApproval2 { get; set; }

    public bool SendForApproval3 { get; set; }
  }
}
