// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.TemplateService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class TemplateService : ServiceBase<RADIOTemplate>, ITemplateService, IService<RADIOTemplate>
  {
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;
    private IMASBillingServicesService _service;

    public TemplateService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._service = (IMASBillingServicesService) new MasBillingServicesService(this._uow);
    }

    public List<TemplateSearchDetails> TemplateList(int DeptId, string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<TemplateSearchDetails>().GetEntitiesBySql(string.Format(Queries.GetTemplatebyId, (object) DeptId, (object) url)).ToList<TemplateSearchDetails>();
    }

    public IEnumerable<TemplateDetailsViewModel> ServiceListDetails(int id)
    {
      List<TemplateDetailsViewModel> detailsViewModelList = new List<TemplateDetailsViewModel>();
      int num = 16;
      return (IEnumerable<TemplateDetailsViewModel>) this._uow.Repository<TemplateDetailsViewModel>().GetEntitiesBySql(string.Format(Queries.ServiceDetails, (object) id, (object) num)).ToList<TemplateDetailsViewModel>();
    }

    public TemplateViewModal BindRadiologyModel()
    {
      TemplateViewModal templateViewModal = new TemplateViewModal();
      RADIOTemplateDetails radioTemplateDetails = new RADIOTemplateDetails();
      templateViewModal.ServicesList = this._uow.Repository<MASBillingServices>().GetAll("ServiceId NOT IN (SELECT ServiceId from RADIOTemplate) AND DeptId=" + (object) 16);
      return templateViewModal;
    }

    public int SaveRadiolgy(TemplateViewModal model)
    {
      RADIOTemplate radioTemplate = new RADIOTemplate();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<TemplateViewModal, RADIOTemplate>()));
      RADIOTemplate entity1 = Mapper.Map<TemplateViewModal, RADIOTemplate>(model);
      entity1.GroupId = this._uow.Repository<MASBillingServices>().Get(model.ServiceId).GroupId;
      entity1.RadioTempId = 0;
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      int num = this._uow.Repository<RADIOTemplate>().Add(entity1, false);
      RADIOTemplateDetails entity2 = new RADIOTemplateDetails();
      if (model.ServiceListDetails != null)
      {
        foreach (TemplateDetailsViewModel serviceListDetail in model.ServiceListDetails)
        {
          entity2.RadioTempDetId = 0;
          entity2.RadioTempId = num;
          entity2.ServiceItems = serviceListDetail.ServiceItems;
          entity2.DisplayOrder = serviceListDetail.DisplayOrder;
          entity2.MaleNormalRange = serviceListDetail.MaleNormalRange;
          entity2.FemaleNormalRange = serviceListDetail.FemaleNormalRange;
          entity2.Unit = serviceListDetail.Unit;
          entity2.IsHeader = serviceListDetail.IsHeader;
          this._uow.Repository<RADIOTemplateDetails>().Add(entity2, false);
        }
      }
      else
      {
        entity2.RadioTempDetId = 0;
        entity2.RadioTempId = num;
        entity2.ServiceItems = this._uow.Repository<MASBillingServices>().Get(model.ServiceId).ServiceName;
        entity2.DisplayOrder = "1";
        entity2.IsHeader = "N";
        this._uow.Repository<RADIOTemplateDetails>().Add(entity2, false);
      }
      return num;
    }

    public int UpdateRadiolgy(TemplateDetailsViewModel model)
    {
      RADIOTemplate entity1 = new RADIOTemplate();
      entity1.ModifiedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      this._uow.Repository<RADIOTemplate>().Update(entity1, false);
      RADIOTemplateDetails entity2 = new RADIOTemplateDetails();
      if (model.RadioTempDetId != 0)
      {
        entity2.RadioTempId = model.RadioTempId;
        entity2.RadioTempDetId = model.RadioTempDetId;
        entity2.ServiceItems = model.ServiceItems;
        entity2.DisplayOrder = model.DisplayOrder;
        entity2.MaleNormalRange = model.MaleNormalRange;
        entity2.FemaleNormalRange = model.FemaleNormalRange;
        entity2.Unit = model.Unit;
        entity2.IsHeader = model.IsHeader.Trim();
        this._uow.Repository<RADIOTemplateDetails>().Update(entity2, false);
      }
      else
      {
        entity2.RadioTempDetId = 0;
        entity2.RadioTempId = model.RadioTempId;
        entity2.RadioTempDetId = model.RadioTempDetId;
        entity2.ServiceItems = model.ServiceItems;
        entity2.DisplayOrder = model.DisplayOrder;
        entity2.MaleNormalRange = model.MaleNormalRange;
        entity2.FemaleNormalRange = model.FemaleNormalRange;
        entity2.Unit = model.Unit;
        entity2.IsHeader = model.IsHeader;
        this._uow.Repository<RADIOTemplateDetails>().Add(entity2, false);
      }
      return entity1.RadioTempId;
    }

    public List<TemplateSearchDetails> LaboratoryList(int DeptId, string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<TemplateSearchDetails>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryTemplate, (object) DeptId, (object) From_Date, (object) To_Date, (object) url)).ToList<TemplateSearchDetails>();
    }

    public TemplateViewModal BindLaboratoryModel()
    {
      TemplateViewModal templateViewModal = new TemplateViewModal();
      RADIOTemplateDetails radioTemplateDetails = new RADIOTemplateDetails();
      templateViewModal.ServicesList = this._uow.Repository<MASBillingServices>().GetAll("ServiceId NOT IN (SELECT ServiceId from RADIOTemplate) AND DeptId=" + (object) 20);
      return templateViewModal;
    }

    public IEnumerable<TemplateDetailsViewModel> ServiceListLaboratoryDetails(int id)
    {
      List<TemplateDetailsViewModel> detailsViewModelList = new List<TemplateDetailsViewModel>();
      int num = 20;
      return (IEnumerable<TemplateDetailsViewModel>) this._uow.Repository<TemplateDetailsViewModel>().GetEntitiesBySql(string.Format(Queries.ServiceDetails, (object) id, (object) num)).ToList<TemplateDetailsViewModel>();
    }
  }
}
