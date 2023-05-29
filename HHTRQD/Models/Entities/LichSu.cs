using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HHTRQD.Models.Entities
{
    public partial class LichSu
    {
        public LichSu()
        {
            LsngheNghiep = new HashSet<LsngheNghiep>();
            LstieuChi = new HashSet<LstieuChi>();
            LstrongSo = new HashSet<LstrongSo>();
        }

        public int Id { get; set; }
        public string Ipmac { get; set; }
        public DateTime? NgayTv { get; set; }

        public virtual ICollection<LsngheNghiep> LsngheNghiep { get; set; }
        public virtual ICollection<LstieuChi> LstieuChi { get; set; }
        public virtual ICollection<LstrongSo> LstrongSo { get; set; }
    }
}
