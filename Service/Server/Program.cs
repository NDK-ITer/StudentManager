using Application.Services;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SendMail.ClassDefine;
using SendMail.Interfaces;
using Server.Extension;
using Server.FileMethods;
using Server.Middleware;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConnectString");
var tempImagesFolderPath = Path.Combine("TempImages");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHostedService(provider => new ImageCleanupService(tempImagesFolderPath));
builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ImageMethod>();
builder.Services.AddTransient<DocumentMethod>();
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
    FileProvider = new CompositeFileProvider(
        new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "PublicFile")),
        new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "TempImages"))
    ),
    RequestPath = "/public"
});


app.MapControllers();

app.Run();
