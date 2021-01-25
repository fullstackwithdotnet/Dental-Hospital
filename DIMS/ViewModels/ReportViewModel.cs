// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.ReportViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;

namespace DIMS.ViewModels
{
  public class ReportViewModel : EntityBase
  {
    public int RowNum { get; set; }

    public string DisplayDate { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public int Age { get; set; }

    public string GenderName { get; set; }

    public string VisitType { get; set; }

    public int CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string MaleNewVisit { get; set; }

    public string FemaleNewVisit { get; set; }

    public string MaleReVisit { get; set; }

    public string FemaleReVisit { get; set; }

    public string Area { get; set; }

    public string BillNo { get; set; }

    public string BillAmount { get; set; }

    public string DeptCode { get; set; }

    public string CodeDescription { get; set; }

    public string TotalAmount { get; set; }

    public string DiscountPer { get; set; }

    public string Balance { get; set; }

    public string DiscountAmt { get; set; }

    public string CancelledBill { get; set; }

    public string BalanceAmount { get; set; }

    public string ReceivedAmount { get; set; }

    public string NoofBills { get; set; }

    public string CourseYearName { get; set; }

    public string AllotDate { get; set; }

    public string Date { get; set; }

    public string Regular { get; set; }

    public string Camp { get; set; }

    public string BPL { get; set; }

    public string Free { get; set; }

    public string Staff { get; set; }

    public string Student { get; set; }

    public string Total { get; set; }

    public bool IsCreated { get; set; }
  }


    public class CensusVisitorCount : EntityBase
    {
        public string Date { get; set; }

        public int Children
        {
            get;
            set;
        }
        public int Male { get; set; }
        public int Female { get; set; }
    }
}
