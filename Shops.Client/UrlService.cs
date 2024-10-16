﻿namespace Shops.Client
{
    public class UrlService
    {
        public Dictionary<string, string> Urls { get; set; }

        public UrlService(IConfiguration configuration)
        {
            Urls = configuration.GetSection("Urls").Get<Dictionary<string, string>>() ?? new Dictionary<string, string>();
        }



        public string GetUserStore_Products_ServiceUrl()
        {
            Urls.TryGetValue("Store_Products", out var userServiceUrl);
            return userServiceUrl;
        }

        public string GetUserStore_Category_ServiceUrl()
        {
            Urls.TryGetValue("Store_Category", out var userServiceUrl);
            return userServiceUrl;
        }

        public string GetUserServiceUrl()
        {
            Urls.TryGetValue("UserService", out var userServiceUrl);
            return userServiceUrl;
        }

        public string GetStatusServiceUrl()
        {
            Urls.TryGetValue("Store_Status", out var userServiceUrl);
            return userServiceUrl;
        }
    }

}
