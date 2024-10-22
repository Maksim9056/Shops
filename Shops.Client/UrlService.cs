using ShopClassLibrary.ModelShop;
using System.Collections.ObjectModel;

namespace Shops.Client
{
    /// <summary>
    /// Клас для получение определение конфигураций для адресов к  API 
    /// </summary>
    public class UrlService
    {

        /// <summary>
        /// Конфигурация  настроек
        /// </summary>
        private readonly IConfiguration _IUserService;

        /// <summary>
        /// Запращивает конфигурацию и записывает  настройки json файла в конфигурации для получение адреса
        /// </summary>
        /// <param name="configuration"></param>
        public UrlService(IConfiguration configuration)
        {
            try
            {


                _IUserService = configuration;
                //Urls = _IUserService.GetSection("Urls").Get<Dictionary<string, string>>() ?? new Dictionary<string, string>();

                Console.WriteLine();
            } catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Возращает Store_Image адрес
        /// </summary>
        /// <returns></returns>
        public string GetStoreImage_Products_ServiceUrl()
        {


            //Urls.TryGetValue("Store_Image", out var userServiceUrl);
            return _IUserService["Urls:Store_Image"];
        }

        /// <summary>
        /// Возращает Store_Products адрес
        /// </summary>
        /// <returns></returns>
        public string GetUserStore_Products_ServiceUrl()
        {
            return _IUserService["Urls:Store_Products"];

            //Urls.TryGetValue("Store_Products", out var userServiceUrl);
            //return userServiceUrl;
        }

        /// <summary>
        /// Возращает Store_Category адрес
        /// </summary>
        /// <returns></returns>
        public string GetUserStore_Category_ServiceUrl()
        {
            return _IUserService["Urls:Store_Category"];

            //Urls.TryGetValue("Store_Category", out var userServiceUrl);
            //return userServiceUrl;
        }
        /// <summary>
        /// Возращает UserService адрес
        /// </summary>
        /// <returns></returns>
        public string GetUserServiceUrl()
        {
            return _IUserService["Urls:UserService"];

            //Urls.TryGetValue("UserService", out var userServiceUrl);
            //return userServiceUrl;
        }

        /// <summary>
        /// Возращает Store_Status адрес
        /// </summary>
        /// <returns></returns>
        public string GetStatusServiceUrl()
        {
            return _IUserService["Urls:Store_Status"];

            //Urls.TryGetValue("Store_Status", out var userServiceUrl);
            //return userServiceUrl;
        }
    }

}
