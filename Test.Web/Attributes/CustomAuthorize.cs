using Test.Service;
using Test.Service.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Test.Service.Interfaces;

namespace Test.Web.Attributes;

    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public CustomAuthorize(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
    //
            // Inject JwtService to use in Middleware.
            var jwtService = context.HttpContext.RequestServices.GetService(typeof(IJwtService)) as IJwtService;
            
            // Get the token from Cookie
            var token = CookieUtils.GetJWTToken(context.HttpContext.Request);

            // Validate Token
            var principal = jwtService?.ValidateToken(token ?? "");
            if (principal == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            context.HttpContext.User = principal;

            if (_roles.Length > 0)
            {
                // Get Role Claim from the principal
                var userRole = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!_roles.Contains(userRole))
                {
                    context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                }
            }

    //
        }
    }
