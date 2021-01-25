// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.ReportsController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Metron.Entities;
using System.Globalization;

namespace DIMS.Controllers
{

    public class ReportController : BaseController
    {
        private IUnitOfWork _uow;
        private IReportService _service;
        private IMASCodeService _Dropdownservice;

        public ReportController(IUnitOfWork uow, IReportService service, IUserService userservice,
            IMASCodeService Dropdownservice)
            : base(uow, userservice)
        {
            this._uow = uow;
            this._Dropdownservice = Dropdownservice;
            this._service = service;
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        public ActionResult ReferralSearch()
        {

            return this.View("../Reports/DeptReferralDetails", new ReportSearchViewModel()
            {
                From_Date = DateTime.Now,
                To_Date = DateTime.Now,
                Departmentlist = this._uow.Repository<MASDepartment>()
                    .GetEntitiesBySql(string.Format(Queries.LoadDept, 0))
            });
        }

        public JsonResult GetReferralDetailsList(ReportSearchViewModel model)
        {
            //var From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string From_Date = DateTime.Parse(model.From_Date.ToString(), CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            //var To_Date = model.To_Date.ToString("yyyy-MM-dd");
            string To_Date = DateTime.Parse(model.To_Date.ToString(), CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            var deptId = model.DeptId;
            model.SearchDetails = this._service.ReferralDetails(From_Date, To_Date, deptId);
            return this.Json(model.SearchDetails);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        public ActionResult TreatmentSearch()
        {
            return this.View("../Reports/DeptTreatmentDetails", new ReportSearchViewModel()
            {
                From_Date = DateTime.Now,
                To_Date = DateTime.Now,
                Departmentlist = this._uow.Repository<MASDepartment>()
                    .GetEntitiesBySql(string.Format(Queries.LoadDept, 0))
            });
        }

        public JsonResult GetTreatmentDetailsList(ReportSearchViewModel model)
        {
            var From_Date = model.From_Date.ToString("yyyy-MM-dd");
            var To_Date = model.To_Date.ToString("yyyy-MM-dd");
            var deptId = model.DeptId;
            model.SearchDetails = this._service.TreatmentDetails(From_Date, To_Date, deptId);
            return this.Json(model.SearchDetails);
        }

        public ActionResult PatientSearch()
        {
            return this.View("../Reports/VisitTypePatientDetails", new ReportSearchViewModel()
            {
                From_Date = DateTime.Now,
                To_Date = DateTime.Now,
                RefList = this._uow.Repository<ReferralStatus>()
                    .GetEntitiesBySql(string.Format(Queries.LoadVisitType, 0))
            });
        }

        [HttpPost]
        public ActionResult GetPatientDetailsList(ReportSearchViewModel model)
        {
            var From_Date = model.From_Date.ToString("yyyy-MM-dd");
            var To_Date = model.To_Date.ToString("yyyy-MM-dd");
            var empty = string.Empty;
            var FromDept = 0;
            string VisitType;
            if (model.VisitType != null)
            {
                VisitType = model.VisitType.Trim();
                if (VisitType != null)
                {
                    if (VisitType == "N")
                        FromDept = 15;
                    else if (VisitType == "R")
                        FromDept = 14;
                }
            }
            else
            {
                VisitType = "N";
                FromDept = 15;
            }

            model.SearchDetails = this._service.PatientDetails(From_Date, To_Date, VisitType, FromDept);
            return this.View("../Reports/VisitTypePatientDetails", model);
        }

        public ActionResult CategorySearch()
        {
            var reportSearchViewModel = new ReportSearchViewModel();
            reportSearchViewModel.From_Date = DateTime.Now;
            reportSearchViewModel.To_Date = DateTime.Now;
            var whereClause = "delInd=0";
            reportSearchViewModel.categoryList =
                this._uow.Repository<MASCategory>().GetAll(whereClause)
                    .ToList<MASCategory>();
            return this.View("../Reports/CategoryPatientDetails", reportSearchViewModel);
        }

        public ActionResult CensusReport()
        {
            var reportSearchViewModel = new ReportSearchViewModel();
            reportSearchViewModel.From_Date = DateTime.Now;
            reportSearchViewModel.To_Date = DateTime.Now;
            var whereClause = "delInd=0";
            reportSearchViewModel.categoryList =
                this._uow.Repository<MASCategory>().GetAll(whereClause)
                    .ToList<MASCategory>();
            return this.View("../Reports/CensusReport", reportSearchViewModel);
        }

        public JsonResult GetCategoryDetailsList(ReportSearchViewModel model)
        {
            var From_Date = model.From_Date.ToString("yyyy-MM-dd");
            var To_Date = model.To_Date.ToString("yyyy-MM-dd");
            var VisitType = model.VisitType.Trim();
            var hashCode = model.CategoryId.GetHashCode();
            var Area = model.Area.Trim();
            model.SearchDetails = this._service.CategoryDetails(From_Date, To_Date, VisitType, hashCode, Area);
            return this.Json(model.SearchDetails);
        }

        public JsonResult GetCensusListVisitor(ReportSearchViewModel model)
        {
            var From_Date = model.From_Date.ToString("yyyy-MM-dd");
            var To_Date = model.To_Date.ToString("yyyy-MM-dd");
            var VisitType = model.VisitType.Trim();
            var hashCode = model.CategoryId;
            var records = this._service.VistorCountsCensus(From_Date, To_Date, VisitType, hashCode);
            return this.Json(records);
        }

        public ActionResult OPRegistrationCollection()
        {
            return this.View("../Reports/OPRegistrationCollectionReport",
                new ReportSearchViewModel()
                {
                    From_Date = DateTime.Now,
                    To_Date = DateTime.Now
                });
        }

        public JsonResult GetOPRegCollectionList(ReportSearchViewModel model)
        {
            var str1 = model.From_Date.ToString("yyyy-MM-dd");
            var str2 = model.To_Date.ToString("yyyy-MM-dd");
            model.SearchDetails = this._uow.Repository<ReportViewModel>()
                .GetEntitiesBySql(string.Format(Queries.OPCollectionforReport, str1, str2,
                    15)).ToList<ReportViewModel>();
            return this.Json(model.SearchDetails);
        }

        public ActionResult BillwiseCollectionReport()
        {
            return this.View("../Reports/BillwiseCollectionReport", new ReportSearchViewModel()
            {
                From_Date = DateTime.Now,
                To_Date = DateTime.Now,
                Departmentlist = this._uow.Repository<MASDepartment>()
                    .GetEntitiesBySql(string.Format(Queries.LoadServiceDept, 0))
            });
        }

        public JsonResult BillwiseCollectionList(ReportSearchViewModel model)
        {
            var str1 = model.From_Date.ToString("yyyy-MM-dd");
            var str2 = model.To_Date.ToString("yyyy-MM-dd");
            var deptId = model.DeptId;
            var str3 = "(0=0)";
            if (deptId != 0)
                str3 = "B.DeptId =" + deptId;
            model.SearchDetails = this._uow.Repository<ReportViewModel>()
                .GetEntitiesBySql(string.Format(Queries.GetBillwiseCollectionforReport, str1, str2,
                    str3)).ToList<ReportViewModel>();
            return this.Json(model.SearchDetails);
        }

        public ActionResult CollectionReport()
        {
            var reportSearchViewModel = new ReportSearchViewModel();
            return this.View("../Reports/CollectionReport",
                this._service.BindCollectionReport());
        }

        public JsonResult GetCollectionReportList(ReportSearchViewModel model)
        {
            var fromDateValue = model.FromDateValue;
            var toDateValue = model.ToDateValue;
            var deptId = model.DeptId;
            var str1 = "(0=0)";
            var str2 = "(0=0)";
            if (deptId != 0)
            {
                str1 = "B.DeptId =" + deptId;
                str2 = "DeptId =" + deptId;
            }

            model.SearchDetails = this._uow.Repository<ReportViewModel>()
                .GetEntitiesBySql(string.Format(Queries.GetDatewiseBillsCollection, (object) fromDateValue,
                    (object) toDateValue, (object) str1, (object) str2)).ToList<ReportViewModel>();
            return this.Json(model.SearchDetails);
        }

        public ActionResult CategoryWiseCollectionReport()
        {
            return this.View("../Reports/CategoryWiseCollectionReport",
                new ReportSearchViewModel()
                {
                    Departmentlist = this._uow.Repository<MASDepartment>()
                        .GetAll("DeptId in (" + 15 + "," + 14 + ") ")
                });
        }

        public JsonResult GetCategorywiseCollectionReport(ReportSearchViewModel model)
        {
            var fromDateValue = model.FromDateValue;
            var toDateValue = model.ToDateValue;
            if (model.DeptId == 15)
                model.SearchDetails = this._uow.Repository<ReportViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.GetCategorywiseCensusforNV, fromDateValue,
                        toDateValue)).ToList<ReportViewModel>();
            else if (model.DeptId == 14)
                model.SearchDetails = this._uow.Repository<ReportViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.GetCategorywiseCensusforRV, fromDateValue,
                        toDateValue)).ToList<ReportViewModel>();
            return this.Json(model.SearchDetails);
        }

        public JsonResult GetDoctor(int id)
        {
            return this.Json(new SelectList(this._service.GetDoctorById(id), "Value", "Text"));
        }

        public ActionResult StudentWiseSearch(ReportSearchViewModel model)
        {
            model = this._service.BindCollectionReportforstudent();
            return this.View("../Reports/StudentWiseCensusReport", model);
        }

        public JsonResult GetStudentDetailsList(int StudentId)
        {
            return this.Json(new ReportSearchViewModel()
            {
                SearchDetails = this._service.StudentWiseCensusReport(StudentId)
            }.SearchDetails);
        }

        public JsonResult GetStudent(int id)
        {
            if (1 == id)
                return this.Json(new SelectList(this._service.GetStudendById(id), "Value",
                    "Text"));
            return this.Json(new SelectList("", "Value", "Text"));
        }

        public JsonResult GetDept(int id)
        {
            return this.Json(new SelectList(this._service.GetStudendByDeptId(id), "Value",
                "Text"));
        }

        public JsonResult GetOPCensusByCategoryDetails(ReportSearchViewModel model)
        {
            var From_Date = model.From_Date.ToString("yyyy-MM-dd");
            var To_Date = model.To_Date.ToString("yyyy-MM-dd");
            model.SearchDetails = this._service.OPCensusByCategory(From_Date, To_Date);
            return this.Json(model.SearchDetails);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        public ActionResult RefferalReportByDepartment()
        {

            return this.View("../Reports/RefferalReportByDepartment", new ReportSearchViewModel()
            {
                From_Date = DateTime.Now,
                To_Date = DateTime.Now,
                Departmentlist = this._uow.Repository<MASDepartment>()
                    .GetEntitiesBySql(string.Format(Queries.LoadDept, 0))
            });
        }

        public JsonResult GetRefferalReportByDepartment(ReportSearchViewModel model)
        {
            var From_Date = model.From_Date.ToString("yyyy-MM-dd");
            var To_Date = model.To_Date.ToString("yyyy-MM-dd");
            var deptId = model.DeptId;
            model.SearchDetails = this._service.RefferalReportByDepartment(From_Date, To_Date, deptId);
            return this.Json(model.SearchDetails);
        }
    }
}
