// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.FileUploadViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class FileUploadViewModel : EntityBase
  {
    [PrimaryKey]
    public long FileId { get; set; }

    public int PatientId { get; set; }

    public int DeptId { get; set; }

    [Display(Name = "File Type")]
    public int FileTypeId { get; set; }

    [Required(ErrorMessage = "Please Upload File")]
    [Display(Name = "Upload File")]
    public HttpPostedFileBase[] postedFiles { get; set; }

    public string FileDisplayPath { get; set; }

    public string FilePath { get; set; }

    [Display(Name = "File Name")]
    public string FileName { get; set; }

    [Display(Name = "Summary")]
    public string Description { get; set; }

    public long TestDetId { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public long AllotId { get; set; }

    [Display(Name = "File Type")]
    public string FileTypeName { get; set; }

    public IEnumerable<MASCode> FileTypelist { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }
  }
}
