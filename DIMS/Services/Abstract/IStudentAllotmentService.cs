// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IStudentAllotmentService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IStudentAllotmentService : IService<StudentAllotment>
  {
    StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType, int DeptId, string DeptCode);

    IEnumerable<StudentAllotmentViewModel> LoadProcedureNotesList(int patientId, int DeptId, int TreatmentId);

    Decimal ShowPatientBalance(int patientId, int DeptId);
  }
}
