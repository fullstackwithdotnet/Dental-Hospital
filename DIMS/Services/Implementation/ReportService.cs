// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.ReportService
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
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
    public class ReportService : ServiceBase<GetCasesheetNo>, IReportService
    {
        private const int DepartmentId = 1;
        private const int CourseId = 1;
        private IUnitOfWork _uow;
        private IMASDepartmentService _Deptservice;
        private IMASDoctorService _Doctorservice;
        private IMASStudentRegistrationService _student;

        public ReportService(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
            this._Deptservice = (IMASDepartmentService) new MasDepartmentService(this._uow);
            this._Doctorservice = (IMASDoctorService) new MasDoctorService(this._uow);
            this._student = (IMASStudentRegistrationService) new MASStudentRegistrationService(this._uow);
        }

        public List<ReportViewModel> ReferralDetails(string From_Date, string To_Date, int Dept)
        {
            return this._uow.Repository<ReportViewModel>()
                .GetEntitiesBySql(string.Format(Queries.ReportReferralDetails, (object) From_Date, (object) To_Date,
                    (object) Dept)).ToList<ReportViewModel>();
        }

        public List<ReportViewModel> RefferalReportByDepartment(string From_Date, string To_Date, int Dept)
        {
            return this._uow.Repository<ReportViewModel>()
                .GetEntitiesBySql(string.Format(Queries.RefferalReportByDepartment, (object)From_Date, (object)To_Date,
                    (object)Dept)).ToList<ReportViewModel>();
        }

        public List<ReportViewModel> TreatmentDetails(string From_Date, string To_Date, int Dept)
        {
            return this._uow.Repository<ReportViewModel>()
                .GetEntitiesBySql(string.Format(Queries.ReportTreatmentDetails, (object) From_Date, (object) To_Date,
                    (object) Dept)).ToList<ReportViewModel>();
        }

        public List<ReportViewModel> PatientDetails(string From_Date, string To_Date, string VisitType, int FromDept)
        {
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            if (VisitType == "Both")
            {
                string str1 = "and (VisitType ='N') and Fromdeptid in (" + (object) 15 + ")";
                string str2 = "and (VisitType ='R') and Fromdeptid in (" + (object) 14 + ")";
                return this._uow.Repository<ReportViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.OPCensusReportPatientDetailsBoth, (object) From_Date,
                        (object) To_Date, (object) str1, (object) str2)).ToList<ReportViewModel>();
            }

            string str = " and (VisitType ='" + VisitType + "')  and Fromdeptid in (" + (object) FromDept + ")";
            return this._uow.Repository<ReportViewModel>()
                .GetEntitiesBySql(string.Format(Queries.OPCensusReportPatientDetails, (object) From_Date,
                    (object) To_Date, (object) str)).ToList<ReportViewModel>();
        }

        public List<ReportViewModel> CategoryDetails(string From_Date, string To_Date, string VisitType, int CategoryId,
            string Area)
        {
            string empty = string.Empty;
            string str = !(VisitType == "B")
                ? (!(VisitType == "N")
                    ? (!(VisitType == "R")
                        ? " and (VisitType ='" + VisitType + "')"
                        : "and (VisitType ='R') and Fromdeptid in (" + (object) 14 + ")")
                    : "and (VisitType ='N') and Fromdeptid in (" + (object) 15 + ")")
                : "and (0=0)";
            Area = !string.IsNullOrEmpty(Area) ? " and (OP.Area Like '" + Area + "%')" : "and (0=0)";
            return this._uow.Repository<ReportViewModel>()
                .GetEntitiesBySql(string.Format(Queries.OpCensusCategoryDetails, (object) From_Date, (object) To_Date,
                    (object) str, (object) CategoryId, (object) Area)).ToList<ReportViewModel>();
        }

        public List<CensusVisitorCount> VistorCountsCensus(string From_Date, string To_Date, string VisitType,
            int CategoryId)
        {
            return this._uow.Repository<CensusVisitorCount>()
                .GetEntitiesBySql(string.Format(Queries.CensusCountReportVisitors, (object) From_Date, (object) To_Date,
                    VisitType, (object) CategoryId)).ToList<CensusVisitorCount>();
        }

        public ReportSearchViewModel BindCollectionReport()
        {
            ReportSearchViewModel reportSearchViewModel = new ReportSearchViewModel()
            {
                DeptList = (IList<SelectListItem>) this.LoadDepartment()
            };
            reportSearchViewModel.DoctorList = (IList<SelectListItem>) this.GetDoctorById(
                Convert.ToInt32(reportSearchViewModel.DeptList
                    .FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected)).Value));
            return reportSearchViewModel;
        }

        private List<SelectListItem> LoadDepartment()
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            foreach (DepartmentViewModal departmentViewModal in (IEnumerable<DepartmentViewModal>) this._Deptservice
                .DepartmentList().ToList<DepartmentViewModal>())
            {
                if (departmentViewModal.DeptId == 1)
                    selectListItemList.Add(new SelectListItem()
                    {
                        Text = departmentViewModal.DeptName,
                        Value = Convert.ToString(departmentViewModal.DeptId),
                        Selected = true
                    });
                else
                    selectListItemList.Add(new SelectListItem()
                    {
                        Text = departmentViewModal.DeptName,
                        Value = Convert.ToString(departmentViewModal.DeptId)
                    });
            }

            return selectListItemList;
        }

        public List<SelectListItem> GetDoctorById(int id)
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            List<SelectListItem> list = this._Doctorservice.GetDoctorById(id).Select<DoctorViewModal, SelectListItem>(
                (Func<DoctorViewModal, SelectListItem>) (x => new SelectListItem()
                {
                    Text = x.DoctorName,
                    Value = x.DoctorId.ToString()
                })).ToList<SelectListItem>();
            list.FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
            return list;
        }

        public List<ReportViewModel> StudentWiseCensusReport(int StudentId)
        {
            return this._uow.Repository<ReportViewModel>()
                .GetEntitiesBySql(string.Format(Queries.StudentWiseCensusReport, (object) StudentId))
                .ToList<ReportViewModel>();
        }

        public List<ReportViewModel> OPCensusByCategory(string From_Date, string To_Date)
        {
            return (List<ReportViewModel>) null;
        }

        public ReportSearchViewModel BindCollectionReportforstudent()
        {
            ReportSearchViewModel reportSearchViewModel = new ReportSearchViewModel()
            {
                CoursesList = (IList<SelectListItem>) this.LoadCourses()
            };
            reportSearchViewModel.StudentList = (IList<SelectListItem>) this.GetStudendById(
                Convert.ToInt32(reportSearchViewModel.CoursesList
                    .FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected)).Value));
            reportSearchViewModel.Departmentlist = this._uow.Repository<MASDepartment>()
                .GetEntitiesBySql(string.Format(Queries.LoadDept, (object) 0));
            return reportSearchViewModel;
        }

        private List<SelectListItem> LoadCourses()
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            foreach (ReportSearchViewModel reportSearchViewModel in (IEnumerable<ReportSearchViewModel>) this._student
                .CourseList().ToList<ReportSearchViewModel>())
            {
                if (reportSearchViewModel.CourseId == 1)
                    selectListItemList.Add(new SelectListItem()
                    {
                        Text = reportSearchViewModel.CourseName,
                        Value = Convert.ToString(reportSearchViewModel.CourseId),
                        Selected = true
                    });
                else
                    selectListItemList.Add(new SelectListItem()
                    {
                        Text = reportSearchViewModel.CourseName,
                        Value = Convert.ToString(reportSearchViewModel.CourseId)
                    });
            }

            return selectListItemList;
        }

        public List<SelectListItem> GetStudendById(int id)
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            List<SelectListItem> list = this._student.GetStudentById(id)
                .Select<StudentRegistrationViewModel, SelectListItem>(
                    (Func<StudentRegistrationViewModel, SelectListItem>) (x => new SelectListItem()
                    {
                        Text = x.StudentName,
                        Value = x.StudentId.ToString()
                    })).ToList<SelectListItem>();
            list.FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
            return list;
        }

        public List<SelectListItem> GetStudendByDeptId(int id)
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            List<SelectListItem> list = this._student.GetStudentByDeptId(id)
                .Select<StudentRegistrationViewModel, SelectListItem>(
                    (Func<StudentRegistrationViewModel, SelectListItem>) (x => new SelectListItem()
                    {
                        Text = x.StudentName,
                        Value = x.StudentId.ToString()
                    })).ToList<SelectListItem>();
            list.FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
            return list;
        }

        public List<SelectListItem> GetDeptById(int id)
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            List<SelectListItem> list = this._student.GetDeptById(id)
                .Select<StudentRegistrationViewModel, SelectListItem>(
                    (Func<StudentRegistrationViewModel, SelectListItem>) (x => new SelectListItem()
                    {
                        Text = x.DeptName,
                        Value = x.DeptId.ToString()
                    })).ToList<SelectListItem>();
            list.FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
            return list;
        }
    }
}
