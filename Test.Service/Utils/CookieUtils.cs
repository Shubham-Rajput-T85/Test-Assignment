using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Test.Entity.Data;
using System.Text.Json.Serialization;

namespace Test.Service.Utils;

    public static class CookieUtils
    {
        /// <summary>
        /// Save JWT Token to Cookies
        /// </summary>
        /// <param name="response"></param>
        /// <param name="token"></param>
        public static void SaveJWTToken(HttpResponse response, string token)
        {
            response.Cookies.Append("SuperSecretAuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddMonths(1)
            });
            
        }

        public static string? GetJWTToken(HttpRequest request)
        {
            _ = request.Cookies.TryGetValue("SuperSecretAuthToken", out string? token);
            return token;
        }

        /// <summary>
        /// Save User data to Cookies
        /// </summary>
        /// <param name="response"></param>
        /// <param name="user"></param>
        public static void SaveUserData(HttpResponse response, User user)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
                // ReferenceHandling = ReferenceHandling.Preserve
            };
            string userData = JsonSerializer.Serialize(user,options);

            // Store user details in a cookie for 3 days
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(30),
                HttpOnly = true,
                Secure = true,
                IsEssential = true
            };
            response.Cookies.Append("UserData", userData, cookieOptions);
            
        }

        public static void ClearCookies(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete("SuperSecretAuthToken");
            httpContext.Response.Cookies.Delete("UserData");
        }
    }

