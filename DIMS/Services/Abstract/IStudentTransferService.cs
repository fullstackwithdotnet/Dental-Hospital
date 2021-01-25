// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IStudentTransferService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IStudentTransferService : IService<MASStudentRegistration>
  {
    StudentTransferViewModel BindStudentTransferModel();

    List<SelectListItem> GetCourseYearById(int id);

    List<SelectListItem> GetAllStudent(string StudentCourseId, string StudentYearId, int DeptId, string BatchTypeId);

    int UpdateStudentTransfer(StudentTransferViewModel model);
  }
}
