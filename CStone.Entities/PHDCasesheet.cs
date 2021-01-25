// Decompiled with JetBrains decompiler
// Type: CStone.Entities.PHDCasesheet
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("PHDCasesheet")]
  public class PHDCasesheet : EntityBase
  {
    [PrimaryKey]
    public int PHDId { get; set; }

    public int PatientId { get; set; }

    public string PHDNo { get; set; }

    public DateTime PHDDate { get; set; }

    public string ChiefComplaint { get; set; }

    public string T55 { get; set; }

    public string T54 { get; set; }

    public string T53 { get; set; }

    public string T52 { get; set; }

    public string T51 { get; set; }

    public string T61 { get; set; }

    public string T62 { get; set; }

    public string T63 { get; set; }

    public string T64 { get; set; }

    public string T65 { get; set; }

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

    public string T85 { get; set; }

    public string T84 { get; set; }

    public string T83 { get; set; }

    public string T82 { get; set; }

    public string T81 { get; set; }

    public string T71 { get; set; }

    public string T72 { get; set; }

    public string T73 { get; set; }

    public string T74 { get; set; }

    public string T75 { get; set; }

    public int? OMRId { get; set; }

    public bool IsCampPatient { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? LastVisitedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public bool Approval1 { get; set; }

    public bool Approval2 { get; set; }

    public bool Approval3 { get; set; }

    public bool SendForApproval1 { get; set; }

    public bool SendForApproval2 { get; set; }

    public bool SendForApproval3 { get; set; }
  }
}
