using ContactApplication.Data;
using ContactApplication.Entities;
using ContactApplication.Services;
using ContactApplication.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation();
builder.Services.AddDbContext<ContactContext>();
builder.Services.AddScoped<ContactContextSeed>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IValidator<Contact>, CreateContactValidator>();
builder.Services.AddScoped<IPasswordHasher<Contact>, PasswordHasher<Contact>>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ContactContextSeed>();

seeder.Seed();
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
    pattern: "{controller=Contact}/{action=Index}/{id?}");

app.Run();
