namespace Test.Service.Utils;

using Microsoft.AspNetCore.Http;
using Test.Entity.Data;
using System.Text.Json;
using System.Text.Json.Serialization;


public static class SessionUtils
{

    /// SessionUnitls.cs

    /// <summary>
    /// Method to store user details in session
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="user"></param>
    public static void SetUser(HttpContext httpContext, User user)
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
            // ReferenceHandling = ReferenceHandling.Preserve
        };
        if (user != null)
        {
            string userData = JsonSerializer.Serialize(user, options);
            httpContext.Session.SetString("UserData", userData);

            // Store simple string in Session
            // httpContext.Session.SetString("UserId", user.Userid.ToString());
        }
        else
        {
            httpContext.Session.SetString("UserId", user!.UserId.ToString());
        }
    }

    /// <summary>
    /// Method to retrieve user details from session
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static User? GetUser(HttpContext httpContext)
    {
        // Check session first
        string? userData = httpContext.Session.GetString("UserData");

        if (string.IsNullOrEmpty(userData))
        {
            // If session is empty, check the cookie
            httpContext.Request.Cookies.TryGetValue("UserData", out userData);
        }
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
            // ReferenceHandling = ReferenceHandling.Preserve
        };

        if (userData != null)
        {
            SetUser(httpContext, JsonSerializer.Deserialize<User>(userData, options));
        }

        return string.IsNullOrEmpty(userData) ? null : JsonSerializer.Deserialize<User>(userData, options);
    }
    /*public static User? GetUser(HttpContext httpContext)
        {
            // Check session first
            string? userData = httpContext.Session.GetString("UserData");

            if (string.IsNullOrEmpty(userData))
            {
                // If session is empty, check the cookie
                httpContext.Request.Cookies.TryGetValue("UserData", out userData);
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            if(userData != null)
            {
                SetUser(httpContext, JsonSerializer.Deserialize<User>(userData, options));
            }
            return string.IsNullOrEmpty(userData) ? null : JsonSerializer.Deserialize<User>(userData, options);
        }*/


    /// <summary>
    /// Method to clear all Session data
    /// </summary>
    /// <param name="httpContext"></param>
    public static void ClearSession(HttpContext httpContext) => httpContext.Session.Clear();
}


