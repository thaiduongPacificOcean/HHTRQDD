using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTRQD.Models
{

    public class Nghe
    {
        public int Id { get; set; }
        public string TenNN { get; set; }
        public double? TC1 { get; set; }
        public double? TC2 { get; set; }
        public double? TC3 { get; set; }
        public double? TC4 { get; set; }
        public double? TC5 { get; set; }


        public Nghe(int id, string tenNN, double TcNangLuc, double TcKnlamViec, double TcTuDuyLogic, double TcKngiaoTiep, double TcThaiDoLamViec)
        {
            Id = id;
            TenNN = tenNN;
            TC1 = TcNangLuc;
            TC2 = TcKnlamViec;
            TC3 = TcTuDuyLogic;
            TC4 = TcKngiaoTiep;
            TC5 = TcThaiDoLamViec;
        }

        public override string ToString()
        {
            return TenNN;
        }
    }

}
