using ShopClassLibrary.ModelShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static OpenQA.Selenium.BiDi.Modules.Script.RealmInfo;

namespace Story_Test
{
     public class User_cart
    {
        HttpClient client;

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
        }

        public string Product_API_URL = "https://localhost:7105";
        public string Order_API_URL  = "https://localhost:7033/";

        public int Id_User = 1;
        public List<Product> products = new List<Product>();

        [Test]
        public async Task Product_Get_order()
        {
            try
            {
                var orders = await client.GetFromJsonAsync<List<Order>>(Order_API_URL + $"OrdersConstroler/user/{Id_User.ToString()}");
                foreach (var order in orders)
                {
                    Console.WriteLine(order.Idproduct);
                    var product = await client.GetFromJsonAsync<Product>(Product_API_URL + $"/ProductControler/{order.Idproduct.ToString()}");
                    Console.WriteLine($"Product {product.Id} {product.Name_Product} {product.ProductPrice} {order.Status.StatusName}");

                    products.Add(product);
                }

           

                bool produc = products != null;
                Assert.IsTrue(produc);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                Assert.Fail("API request failed.");
            }
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }



    }
}
