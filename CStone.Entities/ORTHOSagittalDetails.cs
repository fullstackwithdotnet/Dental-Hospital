// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORTHOSagittalDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("ORTHOSagittalDetails")]
  public class ORTHOSagittalDetails : EntityBase
  {
    [PrimaryKey]
    public int SagittalId { get; set; }

    public int SagittalStaticId { get; set; }

    public int OrthoId { get; set; }

    public string PreRxMeasurement { get; set; }

    public string PreRxClass { get; set; }

    public string SurgeryMeasurement { get; set; }

    public string SurgeryClass { get; set; }

    public string PostRxMeasurement { get; set; }

    public string PostRxClass { get; set; }

    public string RetentionMeasurement { get; set; }

    public string RetentionClass { get; set; }

    public string ChangeMeasurement { get; set; }

    public string ChangeClass { get; set; }
  }
}
