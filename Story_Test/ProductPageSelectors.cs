using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_Test
{
    public class ProductPageSelectors
    {

        public static string XPath_click_product_button_add = "/html/body/div[1]/main/article/a";



        public static string XPath_Name_Product = "/html/body/div[1]/main/article/form/div[1]/input";
        public static string XPath_ProductsDescription = "/html/body/div[1]/main/article/form/div[2]/textarea";
        public static string XPath_ProductCount = "/html/body/div[1]/main/article/form/div[3]/input";
        public static string XPath_ProductPrice = "/html/body/div[1]/main/article/form/div[4]/input";
        public static  string CssSelector_Fille = "input[type='file']";

        public static string XPath_saveButton = "/html/body/div[1]/main/article/form/button";
        public  static string CssSelector_SelectCategory_Электроника = "body > div.page > main > article > form > div:nth-child(5) > div > div:nth-child(1) > label > input[type=checkbox]";
        public  static string CssSelector_SelectCategory_Бытовая_техника = "body > div.page > main > article > form > div:nth-child(5) > div > div:nth-child(2) > label > input[type=checkbox]";
        public  static string CssSelector_SelectCategory_Мода = "body > div.page > main > article > form > div:nth-child(5) > div > div:nth-child(3) > label > input[type=checkbox]";
        public  static string CssSelector_SelectCategory_Спорт_и_фитнес = "body > div.page > main > article > form > div:nth-child(5) > div > div:nth-child(4) > label > input[type=checkbox]";

    }
}
