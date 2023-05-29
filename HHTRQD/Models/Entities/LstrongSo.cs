using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HHTRQD.Models.Entities
{
    public partial class LstrongSo
    {
        public int Id { get; set; }
        public int? Idls { get; set; }
        public int? Row { get; set; }
        public int? Column { get; set; }
        public double? Value { get; set; }

        public virtual LichSu IdlsNavigation { get; set; }
    }
}
