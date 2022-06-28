using EORIE.Data;
using EORIE.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
AmountInfo amount = new AmountInfo();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//builder.Configuration.Bind(nameof(amount), amount);
amount.Member = Convert.ToDecimal(builder.Configuration.GetSection("AmountInfo:Member").Value);
amount.Spouse = Convert.ToDecimal(builder.Configuration.GetSection("AmountInfo:Spouse").Value);
builder.Services.AddSingleton(amount);
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//builder.Services.AddDbContext();
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
    pattern: "{controller=VerifyUser}/{action=Index}/{id?}");

app.Run();
