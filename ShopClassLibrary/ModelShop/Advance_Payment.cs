using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class Advance_Payment
    {
        public HashSet<long> SelectedOrderIds { get; set; } = new HashSet<long>();
        public long Id_User { get; set; }
    }

}
