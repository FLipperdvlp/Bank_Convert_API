using Convert_API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<CurrencyRateService>();


var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute("default", "{controller=Currency}/{action=Index}/{id?}");

app.Run();