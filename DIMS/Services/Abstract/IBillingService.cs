// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IBillingService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IBillingService : IService<Billing>
  {
    IEnumerable<BillingViewModal> BillPaymentList(int id);

    List<BillSearchDetails> BillingList(string From_Date, string To_Date, string url);

    BillingViewModal BindBillingModel(int id, int DeptId);

    string GenerateInvoiceNo();

    int SaveBilling(BillingViewModal model);

    IEnumerable<BillingViewModal> BillingServicesList(int id, int DeptId);

    List<BillSearchDetails> PaidBillsList(string From_Date, string To_Date, string url);

    BillingViewModal CancelBill(int BillId);

    BillingViewModal BindReport(int id, string Mode);

    string NumberToWords(int number);

    IEnumerable<BillingViewModal> BillServicesListforReport(int Id, string Mode);

    IEnumerable<BillingViewModal> BillPaymentListforReport(int Id, string Mode);
  }
}
