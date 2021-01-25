// Decompiled with JetBrains decompiler
// Type: CStone.Entities.FileUpload
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("FileUpload")]
  public class FileUpload : EntityBase
  {
    [PrimaryKey]
    public long FileId { get; set; }

    public int PatientId { get; set; }

    public int DeptId { get; set; }

    public int FileTypeId { get; set; }

    public string FileName { get; set; }

    public string FilePath { get; set; }

    public string Description { get; set; }

    public long TestDetId { get; set; }

    public string FileDisplayPath { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
