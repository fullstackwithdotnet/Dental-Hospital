// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.DoctorViewModal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
    public class DoctorViewModal : EntityBase
    {
        private DateTime _DateofJoin = DateTime.Now;

        [PrimaryKey] public int DoctorId { get; set; }

        [Display(Name = "Doctor No")] public string DoctorRegNo { get; set; }

        [Display(Name = "Doctor Name")] public string DoctorName { get; set; }

        [Display(Name = "Qualification")] public string Qualification { get; set; }

        public int DeptId { get; set; }

        [DisplayName("Department")] public string DeptName { get; set; }

        public int DesigId { get; set; }

        [DisplayName("Designation")] public string DesigName { get; set; }

        public string Link { get; set; }

        [Display(Name = "Mobile")] public string Mobile { get; set; }

        [Display(Name = "Email")] public string Email { get; set; }

        public string Anthetist { get; set; }

        public string Surgeon { get; set; }

        [Display(Name = "Anesthetist")] public bool IschkAnthetist { get; set; }

        [Display(Name = "Surgeon")] public bool IschkSurgeon { get; set; }

        [Display(Name = "Block/Ublock")] public bool IsBlocked { get; set; }

        [Display(Name = "Approval Password")] public string ApprovalPassword { get; set; }

        [Display(Name = "Confirm Password")] public string ConfirmApprovalPassword { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Join")]
        public DateTime DateofJoin
        {
            get { return this._DateofJoin; }
            set { this._DateofJoin = value; }
        }

        public string DateofJoinDisplay { get; set; }

        public IEnumerable<MASDepartment> DepartmentList { get; set; }

        public IEnumerable<MASDesignation> DesignationList { get; set; }

        [Display(Name = "New Password")] public string NewPassword { get; set; }


    }
}
