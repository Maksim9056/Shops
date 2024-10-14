using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class Category
    {
        public long Id { get; set; }
        public string Name_Category { get; set; }
        public string Category_Description { get; set; }

        public  byte[] Image_Category { get; set; }
    }
}
