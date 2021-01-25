// Decompiled with JetBrains decompiler
// Type: CStone.Entities.CONSRootCanalDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("CONSRootCanalDetails")]
  public class CONSRootCanalDetails : EntityBase
  {
    [PrimaryKey]
    public int RootCanalId { get; set; }

    public int ConservativeId { get; set; }

    public string ToothNumber { get; set; }

    public string RcRadiographicInterpretation { get; set; }

    public string AccessOpeningCanal { get; set; }

    public string NoOfCanal { get; set; }

    public string AdditionalCanals { get; set; }

    public string WorkingLengthDetermination { get; set; }

    public string ShapingAndCleaning { get; set; }

    public string RotaryInstrumentation { get; set; }

    public string IrrigantUsed { get; set; }

    public string IntracanalMedicament { get; set; }

    public string MasterConeSelection { get; set; }

    public string ObturationTechnique { get; set; }

    public string PostEndodonticRestoration { get; set; }

    public string ProstheticRehabilitation { get; set; }
  }
}
