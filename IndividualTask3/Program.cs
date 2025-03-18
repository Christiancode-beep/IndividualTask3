using IndividualTask3.Interfaces;
using IndividualTask3.Models;
using IndividualTask3.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CustomerFieldDbContext>(options =>
    options.UseInMemoryDatabase("CustomerFieldsDb"));
builder.Services.AddScoped<ICustomerFieldService, CustomerFieldService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CustomerFieldDbContext>();
    dbContext.Database.EnsureCreated();
}

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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CustomerFields}/{action=Index}/{id?}");

app.Run();
