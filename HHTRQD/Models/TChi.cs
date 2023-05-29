using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTRQD.Models
{
  public class TChi
  {
    public int Id { get; set; }
    public string TenTC { get; set; }

    public TChi(int id, string tenTC)
    {
      Id = id;
      TenTC = tenTC;
    }

    public override string ToString()
    {
      return TenTC;
    }


  }
}
