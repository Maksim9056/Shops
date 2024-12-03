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
        public static string XPath_click_page_Add = "/html/body/div[1]/main";
        public static string XPath_tag_name = "button";
        public static string XPath_saveButton = "/html/body/div[1]/main/article/form/button";
        public  static string CssSelector_SelectCategory_Электроника = "body > div.page > main > article > form > div:nth-child(5) > div > div:nth-child(1) > label > input[type=checkbox]";
        public  static string CssSelector_SelectCategory_Бытовая_техника = "body > div.page > main > article > form > div:nth-child(5) > div > div:nth-child(2) > label > input[type=checkbox]";
        public  static string CssSelector_SelectCategory_Мода = "body > div.page > main > article > form > div:nth-child(5) > div > div:nth-child(3) > label > input[type=checkbox]";
        public  static string CssSelector_SelectCategory_Спорт_и_фитнес = "body > div.page > main > article > form > div:nth-child(5) > div > div:nth-child(4) > label > input[type=checkbox]";


        public static string XPath_click_page_log = "/html[1]/body[1]/div[1]/div[1]/div[2]/nav[1]/div[4]/a[1]";

        public static string XPath_click_page_cartes_bank = "/html[1]/body[1]/div[1]/div[1]/div[2]/nav[1]/div[5]/a[1]";

        public static string XPath_click_page_log_email = "/html/body/div[1]/main/article/div[1]/input";
        public static string XPath_click_page_log_password = "/html/body/div[1]/main/article/div[2]/input";

        public static string XPath_click_page_log_button = "/html[1]/body[1]/div[1]/main[1]/article[1]/button[1]";
        public static string XPath_click_button = "/html/body/div[1]/main/article/div/div/div[2]/button";
        public static string XPath_click_button_plus = "/html/body/div[1]/main/article/div/div/div[2]/div/button[2]";
        public static string XPath_click_button_ = "/html/body/div[1]/div/div[2]/nav/div[2]/a";
        public static string XPath_click_button_v2 = "/html/body/div[1]/main/article/div/div/div[2]/button";
        public static string XPath_click_carte = "/html/body/div[1]/main/article/div[3]/form/div[1]/div[18]/div[2]";

        public static string XPath_click_ = "/html[1]/body[1]/div[1]/div[1]/div[2]/nav[1]/div[2]/a[1]";

        public static string XPath_click_carte__plus = "/html/body/div[1]/main/article/div[3]/form/div[1]/div[18]/div[2]/div/div/button[2]";




        public static string XPath_click_carte__buy = "//button[contains(text(),'Оплатить выбранные')]";


        public static string XPath_click_carte__buy_success = "/html/body/div[1]/main/article/div[3]/form/div[2]/p";
    }
}
