using Microsoft.AspNetCore.Authentication.Cookies;
using Test.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




// add database context
var connectionString = builder.Configuration.GetConnectionString("DbConnection");




DependencyInjection.RegisterServices(builder.Services, connectionString!);



// Add authentication using cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect to login if not authenticated
        options.LogoutPath = "/Account/Logout";
        // options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect if unauthorized
        options.AccessDeniedPath = "/Home/Error";
    });

    
builder.Services.AddAuthorization();



// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseSession(); //Enable Session



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
