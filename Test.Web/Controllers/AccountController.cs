using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Entity.Models;
using Test.Service.Interfaces;
using Test.Service.Utils;

namespace Test.Web.Controllers;

public class AccountController : Controller
{

    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;
    // private readonly PizzaShopContext _context;

    public AccountController(IJwtService jwtService , IAuthService authService)
    {
        _jwtService = jwtService;
        _authService = authService;
    }



    public IActionResult Login()
    {

        var user = SessionUtils.GetUser(HttpContext);
        var token = Request.Cookies["SuperSecretAuthToken"];
        var principal = (token != null)? _jwtService.ValidateToken(token) : null;
            if(principal == null){
                return View();
            }
            if (user == null)
                return View();
            else
            {
                return RedirectToAction( "MainPage","Home",user);
            }
        // return View();
    }


    public IActionResult Logout(){
        
        CookieUtils.ClearCookies(HttpContext);
        SessionUtils.ClearSession(HttpContext);
        return RedirectToAction("Login","Account");
    }

  [HttpPost]
        public async Task<IActionResult> Login(LoginView model)
        {
            if(!ModelState.IsValid){
                return View(model);
            }
            //Authenticate User
            var user = await _authService.AuthenticateUser(model.Email, model.Password);
            if (user == null)
            {
                // ViewBag.ErrorMessage = "Invalid email or password.";
                // return View();
                
                return RedirectToAction("Error", "Home");
            }

            // Generate JWT Token
            DateTime expireTime = DateTime.UtcNow.AddHours(1);
            // if (model.RememberMe)
            // {
            //     expireTime = DateTime.UtcNow.AddMonths(1);
            // }
            // var token = _jwtService.GenerateJwtToken(user.Firstname + " " + user.Lastname, user.Email, user.Role.Rolename,expireTime);


            var token = _jwtService.GenerateJwtToken(user.Name,user.Role,expireTime);  // add custom role here 


            // Store token in cookie
            CookieUtils.SaveJWTToken(Response, token);

            // Save User Data to Cookie for Remember Me functionality.
            // if (model.RememberMe)
            // {
            //     CookieUtils.SaveUserData(Response, user);
            // }

            // Store User details in Session
            SessionUtils.SetUser(HttpContext, user);

            // return Json("success");

            return RedirectToAction("MainPage", "Home",user);
        }

}
