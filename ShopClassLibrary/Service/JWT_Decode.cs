using ShopClassLibrary.ModelShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopClassLibrary.Service
{
    public class JWT_Decode
    {
        public User Check_jwt_token(string jwt_token)
        {
            try
            {


                if (!string.IsNullOrEmpty(jwt_token))
                {


                    string[] parts = jwt_token.Split('.');
                    string header = parts[0];
                    string payload = parts[1];
                    string signature = parts[2];
                    string decodedHeader = DecodeBase64Url(header);
                    string decodedPayload = DecodeBase64Url(payload);
                    // Десериализуем полезную нагрузку в объект JSON
                    var payloadss = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(decodedPayload);
                    // Извлечение значений как JsonElement, а затем преобразование к нужному типу

                    long exp = payloadss["exp"].GetInt64(); // Извлекаем значение exp как long

                    DateTime expirationDate = DateTimeOffset.FromUnixTimeSeconds(exp).DateTime;

                    if (DateTime.UtcNow > expirationDate)
                    {
                        Console.WriteLine("JWT токен истек");
                        return new User()  ;

                    }
                    else
                    {
                        string nameid = payloadss["nameid"].GetString(); // Получаем строковое значение
                        string email = payloadss["email"].GetString(); // Получаем строковое значение
                        return new User() { Id = Convert.ToInt64(nameid), Email = email };
                    }
                }
                return new User();
            }
            catch (Exception ex)
            {
                return new User();
            }
        }




            string DecodeBase64Url(string base64Url)
            {
                string base64 = base64Url.Replace('-', '+').Replace('_', '/');
                switch (base64.Length % 4)
                {
                    case 2: base64 += "=="; break;
                    case 3: base64 += "="; break;
                }
                byte[] bytes = Convert.FromBase64String(base64);
                return Encoding.UTF8.GetString(bytes);
            }
        }
    }

