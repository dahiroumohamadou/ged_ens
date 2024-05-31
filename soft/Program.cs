using HarfBuzzSharp;
using Microsoft.AspNetCore.Authentication.Cookies;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using soft.FileUploadService;

var builder = WebApplication.CreateBuilder(args);
//config questpdf 
//QuestPDF.Settings.License = LicenseType.Community;
// Add services to the container.
builder.Services.AddControllersWithViews();
// add interface for upload photo
builder.Services.AddScoped<IFileUploadService, LocalFileUploadService>();
//debut  config security
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);

    });

//fin config security 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
//fin config security
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
