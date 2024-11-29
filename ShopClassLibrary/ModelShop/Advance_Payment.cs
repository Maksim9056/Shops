using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class Advance_Payment
    {
        public long SelectedOrderIds { get; set; }
        public long Id_User { get; set; }
        public long Id_Carte { get; set; }

        public long Count_product { get; set; }


    }

}
