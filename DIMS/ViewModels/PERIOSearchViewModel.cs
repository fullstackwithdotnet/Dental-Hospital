// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PERIOSearchViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class PERIOSearchViewModel : EntityBase
  {
    private DateTime _From_Date = DateTime.Now;
    private DateTime _To_Date = DateTime.Now;

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "FROM DATE")]
    public DateTime From_Date
    {
      get
      {
        return this._From_Date;
      }
      set
      {
        this._From_Date = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "TO DATE")]
    public DateTime To_Date
    {
      get
      {
        return this._To_Date;
      }
      set
      {
        this._To_Date = value;
      }
    }

    [Display(Name = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateRange { get; set; }

    public List<PERIOSearchDetails> SearchDetails { get; set; }
  }
}
