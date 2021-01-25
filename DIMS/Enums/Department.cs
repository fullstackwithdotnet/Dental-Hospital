// Decompiled with JetBrains decompiler
// Type: DIMS.Enums.Department
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System;

namespace DIMS.Enums
{
  public enum Department
  {
    OMR = 1,
    OMFS = 2,
    PERIO = 3,
    CONS = 4,
    ORTHO = 5,
    PEDO = 6,
    PROSTHO = 7,
    PHD = 8,
    ORPATH = 9,
    CLEFT = 10, // 0x0000000A
    CDC = 11, // 0x0000000B
    IMP = 12, // 0x0000000C
    FOD = 13, // 0x0000000D
    REVISIT = 14, // 0x0000000E
    OPD = 15, // 0x0000000F
    RADIO = 16, // 0x00000010
    SETTINGS = 17, // 0x00000011
    BILL = 18, // 0x00000012
    REPORTS = 19, // 0x00000013
    LAB = 20, // 0x00000014
  }

    public enum DeparmentNames
    {
        OMR = 1,
        OMFS = 2,
        PERIO = 3,
        CONS = 4,
        ORTHO = 5,
        PEDO = 6,
        PROSTHO = 7,
        PHD = 8,
        ORPATH = 9,
        CLEFT = 10, // 0x0000000A
        CDC = 11, // 0x0000000B
        IMP = 12, // 0x0000000C
        FOD = 13, // 0x0000000D
        REVISIT = 14, // 0x0000000E
        OPD = 15, // 0x0000000F
        RADIO = 16, // 0x00000010
        SETTINGS = 17, // 0x00000011
        BILL = 18, // 0x00000012
        REPORTS = 19, // 0x00000013
        LAB = 20, // 0x00000014
    }

    public class DeparmentNameDto
    {
        public static string StringBusinessUnits(Department BU)
        {
            switch (BU)
            {
                case Department.OMR: return "OralMedicine and Radiology";
                case Department.OMFS: return "Oral and Maxillofacial Surgery";
                case Department.PERIO: return "Periodontics";
                case Department.CONS: return "Conservative Dentistry and Endodontics";
                case Department.ORTHO: return "Orthodontics";
                case Department.PEDO: return "Pedodontics";
                case Department.PROSTHO: return "Prosthodontics";
                case Department.PHD: return "Public Health Dentistry";
                case Department.ORPATH: return "Oral Pathology";
                case Department.CLEFT: return "Cleft";
                case Department.CDC: return "Comprehensive Dental Care";
                case Department.IMP: return "Implantology";
                case Department.FOD: return "Forensic odontology";
                case Department.REVISIT: return "Out Patient Revisit";
                case Department.OPD: return "Out Patient Dept";
                case Department.RADIO: return "Radiology";
                case Department.SETTINGS: return "Settings";
                case Department.BILL: return "Billing";
                case Department.REPORTS: return "Reports";
                case Department.LAB: return "Laboratory";
                default: return String.Empty;
            }
        }
    }
}
