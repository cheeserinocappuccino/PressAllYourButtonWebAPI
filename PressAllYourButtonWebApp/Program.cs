using Microsoft.EntityFrameworkCore;
using PressAllYourButtonWebApp;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies; 


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add Db connection
builder.Services.AddDbContext<PressAYBDbContext>(
    opt => opt.UseSqlServer("name=ConnectionStrings:PressAYBDatabase")
    );

// Use cokkie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(op =>
    {
        op.Cookie.Name = builder.Configuration.GetValue<string>("CookieNames:Authentication");
        op.ExpireTimeSpan = TimeSpan.FromDays(60);

        // To avoid Unauthorized redirect for webAPI
        // https://stackoverflow.com/questions/45878166/asp-net-core-2-0-disable-automatic-challenge
        op.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    
    });

// enable usage of Httpcontext in custom class
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCookiePolicy(); // Enable Global 
app.UseAuthentication(); // Add to enable cokkie Auth
app.UseAuthorization();

app.MapControllers();

app.Run();
