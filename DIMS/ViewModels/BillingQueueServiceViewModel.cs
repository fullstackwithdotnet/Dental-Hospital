// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.BillingQueueServiceViewModel
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
    public class BillingQueueServiceViewModel : EntityBase
    {
        public BillingQueueServiceViewModel()
        {
            this.ChildServiceId = 0;
        }

        [Key] public int BillQueueId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? BillQueueDate { get; set; }

        public int PatientId { get; set; }

        [DisplayName("Patient Name")] public string PatientName { get; set; }

        public int DeptId { get; set; }

        [DisplayName("Department")] public string DeptName { get; set; }

        public int ServiceId { get; set; }

        public int ChildServiceId { get; set; }

        [DisplayName("Services")] public string ServiceName { get; set; }

        [DisplayName("Child Service")] public string ChildServiceName { get; set; }

        public string ServiceDeptName { get; set; }

        public string LabRadServiceName { get; set; }

        public string LabRadTeethNo { get; set; }

        public string LabRadRate { get; set; }

        public string LabRadQty { get; set; }

        public string LabRadAmount { get; set; }

        public string LabRadDiscountPer { get; set; }

        public string LabRadDiscountRs { get; set; }

        public string LabRadNetAmount { get; set; }

        [DisplayName("Group")] public int LabGroupId { get; set; }

        [DisplayName("Qty")] public int Qty { get; set; }

        [DisplayName("Rate")] public Decimal Rate { get; set; }

        [DisplayName("Amount")] public Decimal Amount { get; set; }

        [DisplayName("Disc %")] public Decimal DiscountPer { get; set; }

        [DisplayName("Disc Amt")] public Decimal DiscountAmt { get; set; }

        [DisplayName("Total")] public Decimal NetAmount { get; set; }

        [DisplayName("Teeth No")] public string TeethNo { get; set; }

        [DisplayName("Discount Given By")] public string DiscountGivenBy { get; set; }

        [DisplayName("Discount Purpose")] public string DiscountPurpose { get; set; }

        [DisplayName("Raised By")] public string CreatedBy { get; set; }

        [DisplayName("Discount Amt")] public Decimal DisAmt { get; set; }

        public bool IsChecked { get; set; }

        [DisplayName("CGST")] public Decimal CGST { get; set; }

        [DisplayName("SGST")] public Decimal SGST { get; set; }

        [DisplayName("Amount Received")] public Decimal ReceivedAmount { get; set; }

        [DisplayName("Payable")] public Decimal PayableAmount { get; set; }

        [DisplayName("Payable Request")] public Decimal PayableRequestAmount { get; set; }

        [DisplayName("Payable")] public Decimal PayableAmountDisplay { get; set; }

        [DisplayName("Paid")] public Decimal PaidAmount { get; set; }

        public Decimal BalanceAmount { get; set; }

        public int RequisitionId { get; set; }

        public int CaserecordId { get; set; }

        public int ReferredTreatmentId { get; set; }

        public string ServiceType { get; set; }

        public string GstPercentage { get; set; }

        [DisplayName("Total Amount")] public Decimal TotalAmountafterTax { get; set; }

        [DisplayName("Treatment")] public string TreatmentServiceName { get; set; }

        public IEnumerable<OPDPatientRegistration> patients { get; set; }

        public IEnumerable<MASDepartment> Department { get; set; }

        public IEnumerable<BillQueueDetails> BillingList { get; set; }

        public PatientInformationViewModel patientInformationViewModel { get; set; }

        public IEnumerable<BillingQueueServiceViewModel> BillServicesList { get; set; }

        public IEnumerable<MASBillingServices> BillDropServicesList { get; set; }

        public IEnumerable<MASBillingServices> BillDropLabRadServicesList { get; set; }

        public IEnumerable<MASDepartment> ServicesTypeDetails { get; set; }

        public IEnumerable<MASGroup> GroupList { get; set; }

        public bool ReadOnlyApproval2 { get; set; }

        public bool ReadOnlyApproval3 { get; set; }

        public string ApprovalStatus { get; set; }

        public bool IsApproved { get; set; }

        public string Approvalvalue { get; set; }
    }
}
