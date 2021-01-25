// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IMASStudentRegistrationService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IMASStudentRegistrationService : IService<MASStudentRegistration>
  {
    IEnumerable<StudentRegistrationViewModel> GetStudentList();

    IEnumerable<StudentRegistrationViewModel> GetStudentById(int id);

    IEnumerable<StudentRegistrationViewModel> GetDeptById(int id);

    IEnumerable<ReportSearchViewModel> CourseList();

    IEnumerable<StudentRegistrationViewModel> GetStudentByDeptId(int id);
  }
}
