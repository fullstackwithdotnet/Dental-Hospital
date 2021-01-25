// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.BillQueueService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

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
    public class BillQueueService : ServiceBase<BillQueueDetails>, IBillQueueService, IService<BillQueueDetails>
    {
        private IUnitOfWork _uow;
        private IMASCodeService _Dropdownservice;

        public BillQueueService(IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
            _Dropdownservice = new MASCodeService(_uow);
        }

        public IEnumerable<BillingQueueServiceViewModel> BillingList()
        {
            List<BillingQueueServiceViewModel> serviceViewModelList = new List<BillingQueueServiceViewModel>();
            foreach (BillQueueDetails billQueueDetails in _uow.Repository<BillQueueDetails>()
                .GetEntitiesBySql(string.Format(Queries.BillQueue)))
                serviceViewModelList.Add(new BillingQueueServiceViewModel()
                {
                    BillQueueId = billQueueDetails.BillQueueId,
                    BillQueueDate = billQueueDetails.BillQueueDate,
                    PatientId = billQueueDetails.PatientId,
                    PatientName = _uow.Repository<OPDPatientRegistration>().Get(billQueueDetails.PatientId).PatientName,
                    DeptId = billQueueDetails.DeptId,
                    DeptName = _uow.Repository<MASDepartment>().Get(billQueueDetails.DeptId).DeptName,
                    NetAmount = billQueueDetails.NetAmount
                });
            return serviceViewModelList;
        }

        public int SaveInvestigation(IEnumerable<BillingQueueServiceViewModel> model, BillQueueDetails billDetails)
        {
            if (model != null)
            {
                BillQueueDetails entity = new BillQueueDetails();
                entity.CaserecordId = billDetails.CaserecordId;
                entity.ReferredTreatmentId = billDetails.ReferredTreatmentId;
                entity.DeptId = billDetails.DeptId;
                entity.PatientId = billDetails.PatientId;
                foreach (BillingQueueServiceViewModel serviceViewModel in model)
                {
                    if (serviceViewModel != null)
                    {
                        entity.ServiceId = serviceViewModel.ServiceId;
                        entity.ChildServiceId = serviceViewModel.ChildServiceId;
                        // entity.ChildServiceName = serviceViewModel.ChildServiceName;
                        entity.Qty = serviceViewModel.Qty;
                        entity.Rate = serviceViewModel.Rate;
                        entity.Amount = serviceViewModel.Amount;
                        entity.DiscountPer = serviceViewModel.DiscountPer;
                        entity.DiscountAmt = serviceViewModel.DiscountAmt;
                        entity.NetAmount = serviceViewModel.NetAmount;
                        entity.TeethNo = serviceViewModel.TeethNo;
                        entity.DiscountGivenBy = serviceViewModel.DiscountGivenBy;
                        entity.DiscountPurpose = serviceViewModel.DiscountPurpose;
                        DateTime now;
                        if (serviceViewModel.ServiceId != 0 && serviceViewModel.BillQueueId == 0)
                        {
                            entity.PayableAmount = serviceViewModel.PayableAmount;
                            BillQueueDetails billQueueDetails1 = entity;
                            now = DateTime.Now;
                            DateTime? nullable1 =
                                new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                            billQueueDetails1.BillQueueDate = nullable1;
                            BillQueueDetails billQueueDetails2 = entity;
                            now = DateTime.Now;
                            DateTime? nullable2 =
                                new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                            billQueueDetails2.CreatedDate = nullable2;
                            entity.CreatedSystem = _Dropdownservice.GetIPAddress(false);
                            _uow.Repository<BillQueueDetails>().Add(entity, false);
                        }
                        else
                        {
                            entity.PayableAmount = serviceViewModel.PayableAmount;
                            entity.BillQueueId = serviceViewModel.BillQueueId;
                            BillQueueDetails billQueueDetails = entity;
                            now = DateTime.Now;
                            DateTime? nullable =
                                new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                            billQueueDetails.ModifiedDate = nullable;
                            entity.ModifiedSystem = _Dropdownservice.GetIPAddress(false);
                            _uow.Repository<BillQueueDetails>().Update(entity, false);
                        }
                    }
                }
            }

            return 0;
        }

        public IEnumerable<BillingQueueServiceViewModel> BillServicesList(int PatientId, int DeptId, int TreamentId)
        {
            List<BillingQueueServiceViewModel> serviceViewModelList = new List<BillingQueueServiceViewModel>();
            string str = " and S.DeptId not  in (" + 16 + "," + 20 + ")";
            return _uow.Repository<BillingQueueServiceViewModel>()
                .GetEntitiesBySql(string.Format(Queries.PatientBillQueueDetails, (object) PatientId, (object) DeptId,
                    (object) TreamentId, (object) str)).ToList<BillingQueueServiceViewModel>();
        }

        public IEnumerable<BillingQueueServiceViewModel> BillLabRadServicesList(int PatientId, int DeptId,
            int TreamentId)
        {
            List<BillingQueueServiceViewModel> serviceViewModelList = new List<BillingQueueServiceViewModel>();
            //string str = " and S.DeptId  in (" + 16 + "," + 20 + ")";
            string str = string.Empty;
            return _uow.Repository<BillingQueueServiceViewModel>()
                .GetEntitiesBySql(string.Format(Queries.PatientBillQueueDetails, (object) PatientId, (object) DeptId,
                    (object) TreamentId, (object) str)).ToList<BillingQueueServiceViewModel>();
        }

        public IEnumerable<BillingQueueServiceViewModel> BillingServicesList(int id, int DeptId)
        {
            List<BillingQueueServiceViewModel> serviceViewModelList = new List<BillingQueueServiceViewModel>();
            return _uow.Repository<BillingQueueServiceViewModel>()
                .GetEntitiesBySql(string.Format(Queries.BillDetforServices, id, DeptId))
                .ToList<BillingQueueServiceViewModel>();
        }

        public IEnumerable<BillingQueueServiceViewModel> RequisitionServicesList(int PatientId, int DeptId,
            int RequisitionId)
        {
            List<BillingQueueServiceViewModel> serviceViewModelList = new List<BillingQueueServiceViewModel>();
            foreach (BillQueueDetails billQueueDetails in _uow.Repository<BillQueueDetails>()
                .GetAll(string.Format("PatientId={0} and DeptId={1} and IsBillPaid='N' and RequisitionId={2}",
                    PatientId, DeptId, RequisitionId)).OrderBy<BillQueueDetails, int>(S => S.ServiceId))
                serviceViewModelList.Add(new BillingQueueServiceViewModel()
                {
                    BillQueueId = billQueueDetails.BillQueueId,
                    ServiceId = billQueueDetails.ServiceId,
                    ServiceName = _uow.Repository<MASBillingServices>().Get(billQueueDetails.ServiceId).ServiceName,
                    TeethNo = billQueueDetails.TeethNo,
                    Qty = billQueueDetails.Qty,
                    Rate = billQueueDetails.Rate,
                    DiscountPer = billQueueDetails.DiscountPer,
                    NetAmount = billQueueDetails.NetAmount,
                    Amount = billQueueDetails.Amount,
                    PatientId = billQueueDetails.PatientId,
                    DeptId = billQueueDetails.DeptId
                });
            return serviceViewModelList;
        }

        public IEnumerable<BillingQueueServiceViewModel> RequisitionList(int PatientId, int DeptId)
        {
            List<BillingQueueServiceViewModel> serviceViewModelList = new List<BillingQueueServiceViewModel>();
            foreach (BillQueueDetails billQueueDetails in _uow.Repository<BillQueueDetails>()
                .GetEntitiesBySql(string.Format(Queries.BillQueue)))
                serviceViewModelList.Add(new BillingQueueServiceViewModel()
                {
                    BillQueueId = billQueueDetails.BillQueueId,
                    BillQueueDate = billQueueDetails.BillQueueDate,
                    PatientId = billQueueDetails.PatientId,
                    PatientName = _uow.Repository<OPDPatientRegistration>().Get(billQueueDetails.PatientId).PatientName,
                    DeptId = billQueueDetails.DeptId,
                    DeptName = _uow.Repository<MASDepartment>().Get(billQueueDetails.DeptId).DeptName,
                    Amount = billQueueDetails.Amount
                });
            return serviceViewModelList;
        }

        public BillingQueueServiceViewModel GetServicesListByDeptId(int DeptId)
        {
            string whereClause = "DelInd = 0 AND DeptId in (" + DeptId + ")";
            return new BillingQueueServiceViewModel()
            {
                BillDropServicesList = _uow.Repository<MASBillingServices>().GetAll(whereClause)
            };
        }

        public BillingQueueServiceViewModel GetServicesListByLabRad()
        {
            //string whereClause = "DeptId in (" + (object) 16 + "," + (object) 20 + ")";
            string whereClause = "IsService = 'Y'";
            return new BillingQueueServiceViewModel()
            {
                ServicesTypeDetails = _uow.Repository<MASDepartment>().GetAll(whereClause),
                BillDropLabRadServicesList =
                    _uow.Repository<MASBillingServices>().GetAll("DelInd = 0 AND DeptId = 0"),
                GroupList = _uow.Repository<MASGroup>().GetAll("DeptId = 0 and DelInd=0 ")
            };
        }

        public IEnumerable<BillingViewModal> BillpaidtreatemList(int PatientId, int DeptId)
        {
            string str = " and MB.DeptId not in (" + 16 + "," + 20 + ")";
            return _uow.Repository<BillingViewModal>()
                .GetEntitiesBySql(string.Format(Queries.BillPaidTreatmentDetailsForPatients, PatientId, DeptId, str));
        }

        public IEnumerable<BillingViewModal> BillpaidInvestigationList(int PatientId, int DeptId)
        {
            string str = " and MB.DeptId  in (" + 16 + "," + 20 + ")";

            return _uow.Repository<BillingViewModal>()
                .GetEntitiesBySql(string.Format(Queries.BillPaidTreatmentDetailsForPatients, PatientId, DeptId, str));
        }
    }
}
