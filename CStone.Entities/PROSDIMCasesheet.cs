// Decompiled with JetBrains decompiler
// Type: CStone.Entities.PROSDIMCasesheet
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("PROSDIMCasesheet")]
  public class PROSDIMCasesheet : EntityBase
  {
    [PrimaryKey]
    public int ProsthoDIMId { get; set; }

    public string ProsthoDIMNo { get; set; }

    public int PatientId { get; set; }

    public DateTime? ProsthoDIMDate { get; set; }

    public string ChiefComplaint { get; set; }

    public string HistoryOfPresent { get; set; }

    public string DentalHistory { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public DateTime? LastVisitedDate { get; set; }

    public DateTime? DiagnosisTpDiscDate { get; set; }

    public DateTime? TreatmentOptionSelDate { get; set; }

    public DateTime? PreMedicationDate { get; set; }

    public DateTime? PostMedicationDate { get; set; }

    public DateTime? ImplantPlacementDate { get; set; }

    public DateTime? SutureRemovalDate { get; set; }

    public DateTime? SecondStageSurgeryDate { get; set; }

    public DateTime? ImpressionImpAbutDate { get; set; }

    public DateTime? JigTrialDate { get; set; }

    public DateTime? TryInDate { get; set; }

    public DateTime? ProsthesisInsertionDate { get; set; }

    public DateTime? ReviewDate { get; set; }

    public bool Approval1 { get; set; }

    public bool Approval2 { get; set; }

    public bool Approval3 { get; set; }

    public bool SendForApproval1 { get; set; }

    public bool SendForApproval2 { get; set; }

    public bool SendForApproval3 { get; set; }
  }
}
