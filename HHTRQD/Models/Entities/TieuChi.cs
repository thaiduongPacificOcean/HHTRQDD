using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HHTRQD.Models.Entities
{
    public partial class TieuChi
    {
        public TieuChi()
        {
            LstieuChi = new HashSet<LstieuChi>();
        }

        public int Id { get; set; }
        public string MaTc { get; set; }
        public string TenTc { get; set; }

        public virtual ICollection<LstieuChi> LstieuChi { get; set; }
    }
}
