// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IReportService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DIMS.Services.Abstract
{
    public interface IReportService
    {
        List<ReportViewModel> ReferralDetails(string From_Date, string To_Date, int Dept);

        List<ReportViewModel> RefferalReportByDepartment(string From_Date, string To_Date, int Dept);

        List<ReportViewModel> TreatmentDetails(string From_Date, string To_Date, int Dept);

        List<ReportViewModel> PatientDetails(string From_Date, string To_Date, string VisitType, int FromDept);

        List<ReportViewModel> CategoryDetails(string From_Date, string To_Date, string VisitType, int CategoryId,
            string Area);

        ReportSearchViewModel BindCollectionReport();

        List<SelectListItem> GetDoctorById(int id);

        List<ReportViewModel> StudentWiseCensusReport(int StudentId);

        List<SelectListItem> GetStudendById(int id);

        ReportSearchViewModel BindCollectionReportforstudent();

        List<SelectListItem> GetDeptById(int id);

        List<SelectListItem> GetStudendByDeptId(int id);

        List<ReportViewModel> OPCensusByCategory(string From_Date, string To_Date);

        List<CensusVisitorCount> VistorCountsCensus(string From_Date, string To_Date, string VisitType,
            int CategoryId);
    }
}
