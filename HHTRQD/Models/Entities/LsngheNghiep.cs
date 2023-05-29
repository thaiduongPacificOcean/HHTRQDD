using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HHTRQD.Models.Entities
{
    public partial class LsngheNghiep
    {
        public int Id { get; set; }
        public int? Idls { get; set; }
        public int? Idnn { get; set; }

        public virtual LichSu IdlsNavigation { get; set; }
        public virtual NgheNghiep IdnnNavigation { get; set; }
    }
}
