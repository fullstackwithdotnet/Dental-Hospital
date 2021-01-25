// Decompiled with JetBrains decompiler
// Type: CStone.Entities.CONSReRctDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("CONSReRctDetails")]
  public class CONSReRctDetails : EntityBase
  {
    [PrimaryKey]
    public int ReRootCanalId { get; set; }

    public int ConservativeId { get; set; }

    public string ToothNumber { get; set; }

    public string ReRadiographicInterpretation { get; set; }

    public string GpRemovalCanalLocation { get; set; }

    public string ReNoOfCanal { get; set; }

    public string ReAdditionalCanals { get; set; }

    public string ReWorkingLengthDetermination { get; set; }

    public string ReShapingAndCleaning { get; set; }

    public string ReRotaryInstrumentation { get; set; }

    public string ReIrrigantUsed { get; set; }

    public string ReIntracanalMedicament { get; set; }

    public string ReMasterConeSelection { get; set; }

    public string ReObturationTechnique { get; set; }

    public string RePostEndodonticRestoration { get; set; }

    public string ReProstheticRehabilitation { get; set; }
  }
}
