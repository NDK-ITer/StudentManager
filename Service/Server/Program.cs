using Application.Services;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SendMail.ClassDefine;
using SendMail.Interfaces;
using Server.FileMethods;
using Server.Middleware;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConnectString");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ImageMethod>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("myCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3005")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<BodyToFormMiddleware>();

app.UseCors("myCorsPolicy");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "PublicFile")),
    RequestPath = "/public"
});

app.MapControllers();

app.Run();
