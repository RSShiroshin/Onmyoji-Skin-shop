using System;
using System.Collections.Generic;

namespace PRN_Project2.Models
{
    public partial class VerifyHe160324
    {
        public string? UserName { get; set; }
        public string? VerifyCode { get; set; }

        public virtual UserHe160324? UserNameNavigation { get; set; }
    }
}
