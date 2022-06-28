using Microsoft.Extensions.DependencyInjection.Extensions;
using RandomTextCase.SqlHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IConnectionsStringHelper, ConnectionsStringHelper>(); //interfaceleri ve modellerı ekledik.
builder.Services.AddScoped<IUserDataRepository, UserDataRepository>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RandomText}/{action=RandomText}/{id?}");

app.Run();

