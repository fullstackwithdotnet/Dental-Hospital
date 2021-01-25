// Decompiled with JetBrains decompiler
// Type: CStone.Entities.PROSCDCasesheet
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("PROSCDCasesheet")]
  public class PROSCDCasesheet : EntityBase
  {
    [PrimaryKey]
    public int ProsthoCDId { get; set; }

    public int PatientId { get; set; }

    public string ProsthoCDNo { get; set; }

    public DateTime? ProsthoCDDate { get; set; }

    public string ChiefComplaint { get; set; }

    public string MedicalHistory { get; set; }

    public DateTime? PrimaryImprDate { get; set; }

    public DateTime? MakingOfDisgnosDate { get; set; }

    public DateTime? DesignOfCustomDate { get; set; }

    public DateTime? PreprationOfCustomDate { get; set; }

    public DateTime? BorderMoldingDate { get; set; }

    public DateTime? FinalImpressionDate { get; set; }

    public DateTime? MakingOfMasterCastsDate { get; set; }

    public DateTime? TrialDentureBasedDate { get; set; }

    public DateTime? MaxilloMandibularDate { get; set; }

    public DateTime? TransferOfRecArticuarDate { get; set; }

    public DateTime? SelectionOfTeethDate { get; set; }

    public DateTime? ArrangementOfTeethDate { get; set; }

    public DateTime? AnteriorTryDate { get; set; }

    public DateTime? PosteriorTryDate { get; set; }

    public DateTime? LaboratoryRemDate { get; set; }

    public DateTime? FinishingPolisDate { get; set; }

    public DateTime? ClinicalRemDate { get; set; }

    public DateTime? DentureInsertionDate { get; set; }

    public DateTime? PreProsPhaseDate { get; set; }

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
