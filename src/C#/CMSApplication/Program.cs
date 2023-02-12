using CMSApplication.Auth;
using CMSApplication.Data;
using CMSApplication.Data.Entity;
using CMSApplication.OptionsSetup;
using CMSApplication.Services.Abstraction;
using CMSApplication.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenXmlPowerTools;
using System.Reflection;
using System.Text.Json.Serialization;
using CMSApplication.Identity;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
string connectionString = builder.Configuration.GetConnectionString("Database")!;

builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, MyClaimFactory<User>>();


builder.Services.AddDbContext<DBContext>(
    (sp, optionsBuilder) =>
    {
        optionsBuilder.UseSqlServer(connectionString);
    });

builder.Services.AddIdentity<User, ApplicationRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;

        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;


    })
    .AddClaimsPrincipalFactory<MyClaimFactory<User>>()
    .AddEntityFrameworkStores<DBContext>()
    .AddDefaultTokenProviders()
    ;


builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();


builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IExcelService, ExcelService>();
builder.Services.AddSingleton<IFileService,  FileService>(); 
builder.Services.AddScoped<IQuizService,     QuizService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IScoreService,    ScoreService>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("callng",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200", "https://localhost:7046", "http://127.0.0.1:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
        });
});


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;


    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.UseCors("callng");

//app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithOrigins("http://localhost:4200"));
//app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowAnyOrigin());
app.MapControllers()
    .RequireCors("callng");
;

app.Run();
