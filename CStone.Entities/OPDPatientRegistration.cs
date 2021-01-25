// Decompiled with JetBrains decompiler
// Type: CStone.Entities.OPDPatientRegistration
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
    [Table("OPDPatientRegistration")]
    public class OPDPatientRegistration : EntityBase
    {



        [PrimaryKey] public int PatientId { get; set; }

        public long OpNo { get; set; }

        public DateTime RegDate { get; set; }

        public int TitleId { get; set; }

        public int DepartmentId { get; set; }

        public string PatientName { get; set; }

        public string FatherOrHusband { get; set; }

        public DateTime Dob { get; set; }

        public int Age { get; set; }

        public int GenderId { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Area { get; set; }

        public int StateId { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public string AadharNo { get; set; }

        public int MaritalStatusId { get; set; }

        public int CategoryId { get; set; }

        public int PaymodeId { get; set; }

        public Decimal DiscountPer { get; set; }

        public Decimal TotalAmt { get; set; }

        public Decimal NetAmt { get; set; }

        public Decimal PaidAmt { get; set; }

        public string IsDummy { get; set; }

        public string Link { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedSystem { get; set; }

        public string ModifiedSystem { get; set; }

        public string DisplayDate { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileDisplayPath { get; set; }

        public string Description { get; set; }

        public bool IsCreated { get; set; }
    }
}
