var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//config for calling web api

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//for all orgin web api 
//builder.Services.AddCors();

//for only one origin web api

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("MyPolicy", a =>
//    {
//       
//    });
//});

var app = builder.Build();
//app.UseCors(options =>
//{
//    options.AllowAnyOrigin()
//    .AllowAnyMethod()
//    .AllowAnyHeader();
//});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Peridicite}/{action=Index}/{id?}");

////dernier config for calling web api
//app.UseCors("MyPolicy");

app.Run();
