// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PatientSearchViewModal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class PatientSearchViewModal
  {
    [Key]
    public long PatientId { get; set; }

    public long OPNo { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public string Gender { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime RegDate { get; set; }

    public IEnumerable<OPDPatientRegistration> patients { get; set; }
  }
}
