// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.BillingServicesViewModal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.ViewModels
{
    public class BillingServicesViewModal
    {
        public int ServiceId { get; set; }

        [DisplayName("Service Code")] public string ServiceCode { get; set; }

        [DisplayName("Service Name")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Enter alphabets")]
        public string ServiceName { get; set; }

        public string Service { get; set; }

        [DisplayName("Department")] public int DeptId { get; set; }

        [DisplayName("Department")] public string DeptName { get; set; }

        [DisplayName("Group")] public int GroupId { get; set; }

        [DisplayName("Group")] public string GroupName { get; set; }

        [DisplayName("HSN/SAC Code")] public string HSNSACCode { get; set; }

        [DisplayName("Amount")] public string ServiceAmount { get; set; }

        [DisplayName("GST %")]
        [RegularExpression("^[0-9]+(\\.[0-9]{1,2})$", ErrorMessage =
            "Enter valid decimal number with maximum 2 decimal places.")]
        public string GSTPercentage { get; set; }

        public IEnumerable<MASDepartment> DepartmentList { get; set; }

        public IEnumerable<SelectListItem> GroupList { get; set; }

        public int Radio { get; set; }

        public int Lab { get; set; }

        public int DelInd { get; set; }

        public IEnumerable<MASBillingServices> ServicesList { get; set; }

        public IList<SelectListItem> Services { get; set; }
    }
}
