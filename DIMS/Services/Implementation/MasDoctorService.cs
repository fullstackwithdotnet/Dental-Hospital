// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.MasDoctorService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System.Collections.Generic;
using System.Linq;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class MasDoctorService : ServiceBase<MASDoctor>, IMASDoctorService, IService<MASDoctor>
  {
    private IUnitOfWork _uow;

    public MasDoctorService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public IEnumerable<DoctorViewModal> ServicesList()
    {
      List<DoctorViewModal> doctorViewModalList = new List<DoctorViewModal>();
      foreach (MASDoctor masDoctor in this._uow.Repository<MASDoctor>().GetAll())
        doctorViewModalList.Add(new DoctorViewModal()
        {
          DoctorId = masDoctor.DoctorId,
          DoctorName = masDoctor.DoctorName,
          Qualification = masDoctor.Qualification,
          DeptId = masDoctor.DeptId,
          DeptName = this._uow.Repository<MASDepartment>().Get(masDoctor.DeptId).DeptName,
          DesigId = masDoctor.DesigId,
          DesigName = this._uow.Repository<MASDesignation>().Get(masDoctor.DesigId).DesigName,
          Mobile = masDoctor.Mobile,
          Email = masDoctor.Email
        });
      return (IEnumerable<DoctorViewModal>) doctorViewModalList;
    }

    public IEnumerable<DoctorViewModal> GetDoctorById(int id)
    {
      List<DoctorViewModal> doctorViewModalList = new List<DoctorViewModal>();
      foreach (MASDoctor masDoctor in this._uow.Repository<MASDoctor>().GetAll(string.Format("DeptId= {0}", (object) id)))
        doctorViewModalList.Add(new DoctorViewModal()
        {
          DoctorId = masDoctor.DoctorId,
          DoctorName = masDoctor.DoctorName
        });
      return (IEnumerable<DoctorViewModal>) doctorViewModalList;
    }

      public DoctorViewModal GetSingleDoctor(int id)
      {
          var doctor = _uow.Repository<MASDoctor>().Get(id);
          return new DoctorViewModal()
          {
              DoctorId = doctor.DoctorId,
              DoctorName = doctor.DoctorName
          };
      }

        public bool CheckDoctorName(string DoctorName, int DeptId)
    {
      bool flag = false;
      MASDoctor masDoctor = this._uow.Repository<MASDoctor>().GetEntitiesBySql(string.Format("select DoctorName from [MASDoctor] where DoctorName = '{0}' and DeptId={1}", (object) DoctorName, (object) DeptId)).FirstOrDefault<MASDoctor>();
      if (masDoctor != null && masDoctor.DoctorName != null)
        flag = true;
      return flag;
    }
  }
}
