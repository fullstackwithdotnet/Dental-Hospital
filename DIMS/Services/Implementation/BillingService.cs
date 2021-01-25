// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.BillingService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
using DIMS.Enums;
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
  public class BillingService : ServiceBase<Billing>, IBillingService, IService<Billing>
  {
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;
    private IBillQueueService _service;

    public BillingService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._service = (IBillQueueService) new BillQueueService(this._uow);
    }

    public List<BillSearchDetails> BillingList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<BillSearchDetails>().GetEntitiesBySql(string.Format(Queries.BillQueue, (object) From_Date, (object) To_Date, (object) url)).ToList<BillSearchDetails>();
    }

    public IEnumerable<BillingViewModal> BillPaymentList(int id)
    {
      List<BillingViewModal> billingViewModalList = new List<BillingViewModal>();
      foreach (BillingViewModal billingViewModal in this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.BillPayment, (object) id)))
        billingViewModalList.Add(new BillingViewModal()
        {
          BillPayId = billingViewModal.BillPayId,
          BillId = billingViewModal.BillId,
          CodeId = billingViewModal.CodeId,
          CodeDescription = this._uow.Repository<MASCode>().Get(billingViewModal.CodeId).CodeDescription,
          ReferenceNo = billingViewModal.ReferenceNo,
          AmountReceived = billingViewModal.AmountReceived
        });
      return (IEnumerable<BillingViewModal>) billingViewModalList;
    }

    public IEnumerable<BillingViewModal> BillingServicesList(int id, int DeptId)
    {
      List<BillingViewModal> billingViewModalList = new List<BillingViewModal>();
      return (IEnumerable<BillingViewModal>) this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.BillDetforServices, (object) id, (object) DeptId)).ToList<BillingViewModal>();
    }

    public string GenerateInvoiceNo()
    {
      string str1 = "/";
      BillingViewModal billingViewModal = new BillingViewModal();
      string str2;
      if (DateTime.Now.Month > 4)
      {
        string str3 = DateTime.Now.ToString("yy");
        DateTime dateTime = DateTime.Now;
        dateTime = dateTime.AddYears(1);
        string str4 = dateTime.ToString("yy");
        str2 = str3 + str4;
      }
      else
        str2 = DateTime.Now.AddYears(-1).ToString("yy") + DateTime.Now.ToString("yy");
      Billing billing = this._uow.Repository<Billing>().GetEntitiesBySql(string.Format(Queries.GetMaxBillNo)).SingleOrDefault<Billing>();
      string str5;
      if (billing != null)
      {
        string[] strArray = billing.BillNo.Split('/');
        if (strArray.Length > 1)
        {
          int num = int.Parse(strArray[1]) + 1;
          strArray[1] = num.ToString();
          if (strArray[1].Length == 1)
            strArray[1] = "0000" + (object) num;
          else if (strArray[1].Length == 2)
            strArray[1] = "000" + (object) num;
          else if (strArray[1].Length == 3)
            strArray[1] = "00" + (object) num;
          else if (strArray[1].Length == 4)
            strArray[1] = "0" + (object) num;
        }
        str5 = strArray[1];
      }
      else
        str5 = "00001";
      return str2 + str1 + str5;
    }

    public BillingViewModal BindBillingModel(int id, int DeptId)
    {
      BillingViewModal billingViewModal = new BillingViewModal();
      BillingQueueServiceViewModel serviceViewModel1 = new BillingQueueServiceViewModel();
      PatientInformationViewModel informationViewModel = new PatientInformationViewModel();
      BillingPaymentViewModel paymentViewModel = new BillingPaymentViewModel();
      if (!string.IsNullOrEmpty(id.ToString()))
      {
        OPDPatientRegistration patientRegistration = this._uow.Repository<OPDPatientRegistration>().Get(id);
        informationViewModel.PatientId = patientRegistration.PatientId;
        informationViewModel.OpNo = patientRegistration.OpNo;
        informationViewModel.PatientName = patientRegistration.PatientName;
        informationViewModel.Phone = patientRegistration.Phone;
        informationViewModel.AgeGender = patientRegistration.Age.ToString() + "/" + (object) (Gender) patientRegistration.GenderId;
        informationViewModel.Address = patientRegistration.Address;
        billingViewModal.patientInformationViewModel = informationViewModel;
        BillQueueDetails billQueueDetails1 = this._uow.Repository<BillQueueDetails>().GetEntitiesBySql(string.Format(Queries.BillingMaster, (object) id, (object) DeptId)).SingleOrDefault<BillQueueDetails>();
        if (billQueueDetails1 != null)
        {
          billingViewModal.CreatedBy = billQueueDetails1.CreatedBy;
          billingViewModal.DeptId = billQueueDetails1.DeptId;
          billingViewModal.DeptName = this._uow.Repository<MASDepartment>().Get(billQueueDetails1.DeptId).DeptName;
          billingViewModal.DiscountGivenBy = billQueueDetails1.DiscountGivenBy;
          billingViewModal.DiscountPurpose = billQueueDetails1.DiscountPurpose;
          billingViewModal.BillQueueId = billQueueDetails1.BillQueueId;
        }
        BillQueueDetails billQueueDetails2 = this._uow.Repository<BillQueueDetails>().GetEntitiesBySql(string.Format(Queries.BillTotalAmount, (object) id, (object) DeptId)).SingleOrDefault<BillQueueDetails>();
        if (billQueueDetails2 != null)
        {
          billingViewModal.NetAmountB = billQueueDetails2.NetAmount;
          billingViewModal.BillAmount = billQueueDetails2.PayableAmount;
          billingViewModal.BalanceAmt = billQueueDetails2.BalanceAmount;
          billingViewModal.Total = billQueueDetails2.NetAmount;
          billingViewModal.TaxableAmount = billQueueDetails2.PayableAmount;
          billingViewModal.PaidAmount = billQueueDetails2.PayableAmount;
        }
        billingViewModal.BillServicesList = this.BillingServicesList(id, DeptId);
        Decimal num1 = new Decimal();
        Decimal num2 = new Decimal();
        Decimal d1 = new Decimal();
        Decimal d2 = new Decimal();
        foreach (BillingQueueServiceViewModel serviceViewModel2 in this._uow.Repository<BillingQueueServiceViewModel>().GetEntitiesBySql(string.Format(Queries.BillDetforServices, (object) id, (object) DeptId)).ToList<BillingQueueServiceViewModel>())
        {
          d2 += serviceViewModel2.PayableAmount;
          num1 += serviceViewModel2.CGST;
          num2 += serviceViewModel2.SGST;
          d1 += serviceViewModel2.TotalAmountafterTax;
        }
        billingViewModal.PayableAmountB = Math.Round(d2);
        billingViewModal.CGST = num1;
        billingViewModal.SGST = num2;
        billingViewModal.TotalAmountafterTax = Math.Round(d1);
        billingViewModal.CashReceived = Math.Round(d1);
        billingViewModal.BillAmount = Math.Round(d1);
        billingViewModal.gst = num1 + num2;
        billingViewModal.BalanceAmt = Math.Round(d1);
        billingViewModal.billqueueserviceviewModel = serviceViewModel1;
        string whereClause = "delInd=0";
        billingViewModal.Paymodelist = (IEnumerable<MASPaymode>) this._uow.Repository<MASPaymode>().GetAll(whereClause).ToList<MASPaymode>();
        billingViewModal.Paymodelist = (IEnumerable<MASPaymode>) billingViewModal.Paymodelist.Where<MASPaymode>((Func<MASPaymode, bool>) (x =>
        {
          if (x.PaymodeId != 4)
            return x.PaymodeId != 5;
          return false;
        })).ToList<MASPaymode>();
        billingViewModal.PaymodeDetails = (IEnumerable<MASPaymode>) billingViewModal.Paymodelist.Where<MASPaymode>((Func<MASPaymode, bool>) (x =>
        {
          if (x.PaymodeId != 4 && x.PaymodeId != 5)
            return x.PaymodeId != 1;
          return false;
        })).ToList<MASPaymode>();
        billingViewModal.PatientId = id;
      }
      return billingViewModal;
    }

    public int SaveBilling(BillingViewModal model)
    {
      Billing billing = new Billing();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<BillingViewModal, Billing>()));
      Billing entity1 = Mapper.Map<BillingViewModal, Billing>(model);
      entity1.BillId = 0;
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.BillNo = this.GenerateInvoiceNo();
      int num1 = this._uow.Repository<Billing>().Add(entity1, false);
      Decimal num2 = new Decimal();
      Decimal num3 = new Decimal();
      BillingDetails entity2 = new BillingDetails();
      foreach (BillingViewModal billServices in model.BillServicesList)
      {
        if (billServices.IsChecked)
        {
          if (!string.IsNullOrEmpty(billServices.PayableAmount.ToString()))
            num2 = billServices.PayableAmount;
          if (!string.IsNullOrEmpty(billServices.NetAmount.ToString()))
            num3 = billServices.NetAmount;
          Decimal num4 = num3 - num2;
          entity2.BillId = num1;
          entity2.BillQueueId = billServices.BillQueueId;
          entity2.ServiceId = billServices.ServiceId;
          entity2.ServiceQty = billServices.Qty;
          entity2.ServiceRate = billServices.Rate;
          entity2.ServiceAmount = billServices.Amount;
          entity2.DiscountPer = billServices.DiscountPer;
          entity2.DiscountAmt = billServices.DiscountAmt;
          entity2.GSTPercentage = billServices.GstPercentage;
          entity2.ServiceCGST = billServices.CGST;
          entity2.ServiceSGST = billServices.SGST;
          entity2.NetAmount = billServices.TotalAmountafterTax;
          entity2.NetAmount = billServices.TotalAmountafterTax;
          entity2.PayableAmount = num3 - (billServices.PaidAmount + num2);
          entity2.IsBillPaid = "Y";
          this._uow.Repository<BillingDetails>().Add(entity2, false);
          BillQueueDetails billQueueDetails = new BillQueueDetails();
          billQueueDetails.BalanceAmount = num3 - (billServices.PaidAmount + num2);
          billQueueDetails.PayableAmount = num3 - (billServices.PaidAmount + num2);
          billQueueDetails.PaidAmount = billServices.PaidAmount + num2;
          billQueueDetails.BillQueueId = billServices.BillQueueId;
          this._uow.Repository<BillQueueDetails>().GetEntitiesBySql(string.Format(Queries.BillingQueueBalanceUpdate, (object) billQueueDetails.BalanceAmount, (object) billQueueDetails.PayableAmount, (object) billQueueDetails.PaidAmount, (object) billQueueDetails.BillQueueId));
          if (billQueueDetails.BalanceAmount == Decimal.Zero)
            this._uow.Repository<BillQueueDetails>().GetEntitiesBySql(string.Format(Queries.BillQueueUpdate, (object) entity2.BillQueueId));
        }
      }
      BillingPaymentDetails entity3 = new BillingPaymentDetails();
      if (model.CashReceived >= Decimal.Zero)
      {
        entity3.BillPayId = 0;
        entity3.BillId = num1;
        entity3.CodeId = model.CodeId;
        entity3.ReferenceNo = model.CardReferenceNo;
        entity3.AmountReceived = model.CashReceived;
        this._uow.Repository<BillingPaymentDetails>().Add(entity3, false);
      }
      if (model.CardReceived > Decimal.Zero)
      {
        entity3.BillPayId = 0;
        entity3.BillId = num1;
        entity3.CodeId = model.PaymodeId;
        entity3.ReferenceNo = model.ReferenceNo;
        entity3.AmountReceived = model.CardReceived;
        this._uow.Repository<BillingPaymentDetails>().Add(entity3, false);
      }
      return num1;
    }

    public BillingViewModal CancelBill(int BillId)
    {
      BillingViewModal billingViewModal = new BillingViewModal();
      this._uow.Repository<Billing>().Update(new Billing()
      {
        BillDateTime = Convert.ToDateTime(billingViewModal.BillDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff")),
        ModifiedDate = new DateTime?(DateTime.Now),
        ModifiedSystem = this._Dropdownservice.GetIPAddress(false),
        BillId = BillId,
        BillCancelled = "Y"
      }, false);
      List<BillingDetails> list = this._uow.Repository<BillingDetails>().GetEntitiesBySql(string.Format(Queries.GetBillDetbyId, (object) BillId)).ToList<BillingDetails>();
      if (list != null)
      {
        BillQueueDetails billQueueDetails = new BillQueueDetails();
        foreach (BillingDetails billingDetails in list)
          this._uow.Repository<BillQueueDetails>().GetEntitiesBySql(string.Format(Queries.BillingCancelQueueBalanceUpdate, (object) billingDetails.NetAmount, (object) billingDetails.BillQueueId));
        this._uow.Repository<BillingDetails>().GetEntitiesBySql(string.Format(Queries.DeleteBilldet, (object) BillId));
        this._uow.Repository<BillingPaymentDetails>().GetEntitiesBySql(string.Format(Queries.DeletePaymentDet, (object) BillId));
      }
      return billingViewModal;
    }

    public BillingViewModal BindReport(int id, string Mode)
    {
      BillingViewModal billingViewModal1 = new BillingViewModal();
      BillingViewModal billingViewModal2 = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format("exec GetBillingReport {0},'{1}'", (object) id, (object) Mode)).FirstOrDefault<BillingViewModal>();
      billingViewModal2.AgeGender = billingViewModal2.Age + "/" + (object) (Gender) billingViewModal2.GenderId;
      Decimal billAmount = billingViewModal2.BillAmount;
      billingViewModal2.AmountinWords = this.NumberToWords(int.Parse(Math.Ceiling(billAmount).ToString())) + " Rupees Only";
      BillingViewModal billingViewModal3 = this._uow.Repository<BillingViewModal>().
          
          GetEntitiesBySql(string.Format(Queries.BillTaxDetails, (object) id)).FirstOrDefault<BillingViewModal>();
      billingViewModal2.GstPercentage = billingViewModal3.GstPercentage;
      billingViewModal2.NetAmount = billingViewModal3.NetAmount;
      billingViewModal2.BillAmount = billingViewModal3.BillAmount;
      billingViewModal2.DiscountAmt = billingViewModal3.DiscountAmt;
      return billingViewModal2;
    }

    public string NumberToWords(int number)
    {
      if (number == 0)
        return "zero";
      if (number < 0)
        return "minus " + this.NumberToWords(Math.Abs(number));
      string str = "";
      if (number / 1000000 > 0)
      {
        str = str + this.NumberToWords(number / 1000000) + " Million ";
        number %= 1000000;
      }
      if (number / 100000 > 0)
      {
        str = str + this.NumberToWords(number / 100000) + " Lakh ";
        number %= 100000;
      }
      if (number / 1000 > 0)
      {
        str = str + this.NumberToWords(number / 1000) + " Thousand ";
        number %= 1000;
      }
      if (number / 100 > 0)
      {
        str = str + this.NumberToWords(number / 100) + " Hundred ";
        number %= 100;
      }
      if (number > 0)
      {
        if (str != "")
          str += "and ";
        string[] strArray1 = new string[20]
        {
          "Zero",
          "One",
          "Two",
          "Three",
          "Four",
          "Five",
          "Six",
          "Seven",
          "Eight",
          "Nine",
          "Ten",
          "Eleven",
          "Twelve",
          "Thirteen",
          "Fourteen",
          "Fifteen",
          "Sixteen",
          "Seventeen",
          "Eighteen",
          "Nineteen"
        };
        string[] strArray2 = new string[10]
        {
          "Zero",
          "Ten",
          "Twenty",
          "Thirty",
          "Forty",
          "Fifty",
          "Sixty",
          "Seventy",
          "Eighty",
          "Ninety"
        };
        if (number < 20)
        {
          str += strArray1[number];
        }
        else
        {
          str += strArray2[number / 10];
          if (number % 10 > 0)
            str = str + "-" + strArray1[number % 10];
        }
      }
      return str;
    }

    public IEnumerable<BillingViewModal> BillServicesListforReport(int Id, string Mode)
    {
      List<BillingViewModal> billingViewModalList = new List<BillingViewModal>();
      return (IEnumerable<BillingViewModal>) this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format("exec GetBillingReport {0},'{1}'", (object) Id, (object) Mode)).ToList<BillingViewModal>();
    }

    public IEnumerable<BillingViewModal> BillPaymentListforReport(int Id, string Mode)
    {
      List<BillingViewModal> billingViewModalList = new List<BillingViewModal>();
      return (IEnumerable<BillingViewModal>) this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format("exec GetBillingReport {0},'{1}'", (object) Id, (object) Mode)).ToList<BillingViewModal>();
    }

    public List<BillSearchDetails> PaidBillsList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<BillSearchDetails>().GetEntitiesBySql(string.Format(Queries.PaidBillsList, (object) From_Date, (object) To_Date, (object) url)).ToList<BillSearchDetails>();
    }
  }
}
