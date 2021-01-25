// Decompiled with JetBrains decompiler
// Type: CStone.Entities.MASDoctor
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
    public class MASDoctor : EntityBase
    {

        public MASDoctor()
        {
            DelInd = false;
        }

        [PrimaryKey] public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string DoctorRegNo { get; set; }

        public string Qualification { get; set; }

        public int DeptId { get; set; }

        public int DesigId { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Anthetist { get; set; }

        public string Surgeon { get; set; }

        public bool DelInd { get; set; }

        public string ApprovalPassword { get; set; }

        public DateTime DateofJoin { get; set; }
    }
}
