// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.LoginViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    [Display(Name = "User name")]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }
}
