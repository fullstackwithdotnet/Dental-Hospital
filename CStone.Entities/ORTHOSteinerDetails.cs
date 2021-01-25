// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORTHOSteinerDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("ORTHOSteinerDetails")]
  public class ORTHOSteinerDetails : EntityBase
  {
    [PrimaryKey]
    public int SteinerId { get; set; }

    public int OrthoId { get; set; }

    public int SteinerStaticId { get; set; }

    public string Sline { get; set; }

    public string UpperLip { get; set; }

    public string LowerLip { get; set; }

    public string Inference { get; set; }

    public string PreRx { get; set; }

    public string PostModulation { get; set; }

    public string PostRx { get; set; }

    public string Retention { get; set; }

    public string Change { get; set; }

    public string PreTreatment { get; set; }

    public string PreSurgical { get; set; }

    public string PostAlignment { get; set; }

    public string PostSurgical { get; set; }

    public string MidTreatment { get; set; }

    public string Posttreatment { get; set; }
  }
}
