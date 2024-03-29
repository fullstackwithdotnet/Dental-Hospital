﻿// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.DeptReferralListViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System.ComponentModel;

namespace DIMS.ViewModels
{
    public class DeptReferralListViewModel : EntityBase
    {
        public long ReferredId { get; set; }

        public int PatientId { get; set; }

        [DisplayName("Date")]
        public string FromDate { get; set; }

        [DisplayName("Time")]
        public string FromTime { get; set; }

        [DisplayName("OP No")]
        public long OpNo { get; set; }

        [DisplayName("Patient Name")]
        public string PatientName { get; set; }

        public string Phone { get; set; }

        public bool IsCreated { get; set; }

        public int Age { get; set; }

        [DisplayName("From Department")]
        public string FromDeptName { get; set; }

        public string Area { get; set; }

        public string UGStudent { get; set; }

        public string PGStudent { get; set; }

        public string ForAllotment { get; set; }

        public string Link { get; set; }
    }
}
