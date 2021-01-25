// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.BillingViewModal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class BillingViewModal : EntityBase
  {
    private DateTime _BillDateTime = DateTime.Now;

    [Key]
    public int BillId { get; set; }

    public int BillQueueId { get; set; }

    [Display(Name = "Invoice #")]
    public string BillNo { get; set; }

    [Display(Name = "Invoice Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime BillDateTime
    {
      get
      {
        return this._BillDateTime;
      }
      set
      {
        this._BillDateTime = value;
      }
    }

    [DisplayName("Bill Date")]
    public string BillDateTimeDisplay { get; set; }

    public int PatientId { get; set; }

    [DisplayName("Total  Amount")]
    public Decimal BillAmount { get; set; }

    [DisplayName("Bill Cancelled")]
    public string BillCancelled { get; set; }

    public string Remarks { get; set; }

    [DisplayName("Raised By")]
    public string CreatedBy { get; set; }

    public int DeptId { get; set; }

    [DisplayName("Department")]
    public string DeptName { get; set; }

    [DisplayName("Department")]
    public string DeptCode { get; set; }

    [DisplayName("Amount")]
    public Decimal AmountReceived { get; set; }

    [DisplayName("Discount Given By")]
    public string DiscountGivenBy { get; set; }

    [DisplayName("Discount Purpose")]
    public string DiscountPurpose { get; set; }

    public Decimal Balance { get; set; }

    public Decimal Payable { get; set; }

    [DisplayName("Payable")]
    public Decimal PayableAmount { get; set; }

    public Decimal BalanceAmount { get; set; }

    [DisplayName("Paid")]
    public Decimal PaidAmount { get; set; }

    public Decimal BalanceAmt { get; set; }

    [DisplayName("Amount Received")]
    public Decimal ReceivedAmount { get; set; }

    [DisplayName(" Gross Amount")]
    public Decimal TaxableAmount { get; set; }

    [DisplayName("Due Balance")]
    public Decimal DueBalance { get; set; }

    [DisplayName("Sub Total")]
    public string SubTotal { get; set; }

    [DisplayName("GST")]
    public Decimal gst { get; set; }

    [DisplayName("Total")]
    public Decimal Total { get; set; }

    [DisplayName("Pay")]
    public string Pay { get; set; }

    [DisplayName("Amount")]
    public Decimal NetAmount { get; set; }

    [DisplayName("Service")]
    public string ServiceName { get; set; }

    [DisplayName("Qty")]
    public int ServiceQty { get; set; }

    [DisplayName("Rate")]
    public Decimal ServiceRate { get; set; }

    public string Link { get; set; }

    public int BillPayId { get; set; }

    [DisplayName("Reference No")]
    public string ReferenceNo { get; set; }

    public string CardReferenceNo { get; set; }

    [DisplayName("Paymode")]
    public int CodeId { get; set; }

    [DisplayName("Paymode Name")]
    public string CodeDescription { get; set; }

    public IEnumerable<MASPaymode> Paymodelist { get; set; }

    public IEnumerable<MASPaymode> PaymodeDetails { get; set; }

    public int PaymodeId { get; set; }

    public string PaymodeName { get; set; }

    public Decimal CashReceived { get; set; }

    public Decimal CardReceived { get; set; }

    [DisplayName("CGST Amt")]
    public Decimal CGST { get; set; }

    [DisplayName("SGST Amt")]
    public Decimal SGST { get; set; }

    [DisplayName("Total")]
    public Decimal TotalAmountafterTax { get; set; }

    [DisplayName("Payable Amount")]
    public Decimal PayableAmountB { get; set; }

    [DisplayName("Payable Amount")]
    public Decimal NetAmountB { get; set; }

    [DisplayName("Total")]
    public string Label { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? BillQueueDate { get; set; }

    [DisplayName("Patient Name")]
    public string PatientName { get; set; }

    public int ServiceId { get; set; }

    public Decimal GstPercentage { get; set; }

    [DisplayName("Qty")]
    public int Qty { get; set; }

    [DisplayName("Rate")]
    public Decimal Rate { get; set; }

    [DisplayName("Amount")]
    public Decimal Amount { get; set; }

    [DisplayName("Disc %")]
    public Decimal DiscountPer { get; set; }

    [DisplayName("Teeth No")]
    public string TeethNo { get; set; }

    [DisplayName("Disc Amt")]
    public Decimal DisAmt { get; set; }

    public bool IsChecked { get; set; }

    [Display(Name = "OP No.")]
    public long OpNo { get; set; }

    [ReadOnly(true)]
    [Display(Name = "Address.")]
    [DataType(DataType.MultilineText)]
    public string Address { get; set; }

    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    [DisplayName("Age/Gender")]
    public string AgeGender { get; set; }

    public string Age { get; set; }

    public int GenderId { get; set; }

    public string AmountinWords { get; set; }

    [DisplayName("Paid Status")]
    public string IsBillPaidStatus { get; set; }

    public Decimal DiscountAmt { get; set; }

    public Decimal TotDisPer { get; set; }

    public Decimal TotDisAmount { get; set; }

    public IEnumerable<MASDepartment> Department { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public BillingPaymentViewModel billingPaymentViewModal { get; set; }

    public BillingQueueServiceViewModel billqueueserviceviewModel { get; set; }

    public IEnumerable<BillingViewModal> BillPaymentList { get; set; }

    public IEnumerable<MASCode> PaymodeCardlist { get; set; }

    public IEnumerable<BillingViewModal> BillServicesList { get; set; }

    public IEnumerable<BillingViewModal> BillServicesListforReport { get; set; }

    public IEnumerable<BillingViewModal> BillPaymentListforReport { get; set; }
  }
}
