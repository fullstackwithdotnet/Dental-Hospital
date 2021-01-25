// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.StudentAllotmentViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class StudentAllotmentViewModel : EntityBase
  {
    [PrimaryKey]
    public long AllotId { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [Display(Name = "Allot Date")]
    [DataType(DataType.Date)]
    public DateTime? AllotDate { get; set; }

    public int PatientId { get; set; }

    public long ReferredId { get; set; }

    public int DeptId { get; set; }

    public string DeptCode { get; set; }

    [Display(Name = "Type")]
    public int AllotTypeId { get; set; }

    [Display(Name = "Student Name")]
    public int StudentId { get; set; }

    [Display(Name = "Incharge Doctor")]
    public int? DoctorId { get; set; }

    public int CaserecordId { get; set; }

    [Display(Name = "Procedure Notes")]
    public string ProcedureNotes { get; set; }

    public DateTime? ProcedureNotesDate { get; set; }

    [Display(Name = "Approval")]
    public string DoctorApproval { get; set; }

    public DateTime? DoctorApprovalDate { get; set; }

    public int ApprovalType { get; set; }

    public string ApprovalTypeVal { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    [Display(Name = "Student Name")]
    public string StudentName { get; set; }

    [Display(Name = "Reg No")]
    public string StudentRegNo { get; set; }

    public int GenderId { get; set; }

    public int Age { get; set; }

    [Display(Name = "Doctor Name")]
    public string DoctorName { get; set; }

    [Display(Name = "Date")]
    public string DisplayAllotDate { get; set; }

    [Display(Name = "Course")]
    public string CourseName { get; set; }

    [Display(Name = "Year")]
    public string CourseYearName { get; set; }

    [Display(Name = "OP #")]
    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string AgeGender { get; set; }

    public string Link { get; set; }

    [Display(Name = "Reason")]
    public string ReferredReason { get; set; }

    [Display(Name = "Alloted To")]
    public string AllotedTo { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public IEnumerable<MASStudentRegistration> StudentAllotmentlist { get; set; }

    public IEnumerable<StudentAllotmentViewModel> PreviousAllotmentlist { get; set; }

    public IEnumerable<MASCourse> AllotTypelist { get; set; }
  }
}
