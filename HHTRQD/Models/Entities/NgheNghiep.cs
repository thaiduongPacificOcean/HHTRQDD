using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HHTRQD.Models.Entities
{
    public partial class NgheNghiep
    {
        public NgheNghiep()
        {
            LsngheNghiep = new HashSet<LsngheNghiep>();
        }

        public int Id { get; set; }
        public string TenNn { get; set; }
        public double? TcNangLuc { get; set; }
        public double? TcKnlamViec { get; set; }
        public double? TcTuDuyLogic { get; set; }
        public double? TcKngiaoTiep { get; set; }
        public double? TcThaiDoLamViec { get; set; }

        public virtual ICollection<LsngheNghiep> LsngheNghiep { get; set; }
    }
}
