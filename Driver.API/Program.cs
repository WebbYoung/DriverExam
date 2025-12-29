using Driver.API.Filters;
using Driver.IService;
using Driver.Service;
using Driver.Service.Entities;
using Driver.Service.Services;
using Driver.Service.ValueObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(e =>
    {
        e.AllowAnyMethod()
        .AllowAnyOrigin()
        .AllowAnyHeader();
    });
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.Configure<MvcOptions>(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
    opt.Filters.Add<ValidFilter>();
});
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(e =>
    {
        var jwtOpt = builder.Configuration.GetSection("JWT").Get<JWTSettings>();
        string? secKeyStr = jwtOpt?.SecKey??throw new ArgumentNullException();
        byte[] keyBytes = Encoding.UTF8.GetBytes(secKeyStr);
        var secKey = new SymmetricSecurityKey(keyBytes);
        e.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = secKey
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//using var context = new DriverDbContext();
//context.Questions.RemoveRange(context.Questions);
//context.SaveChanges();
//for (int i = 0; i < 20; i++)
//{
//    List<Option> options = new List<Option>();
//    Queue<char> chars = new Queue<char>();
//    chars.Enqueue('A');
//    chars.Enqueue('B');
//    chars.Enqueue('C');
//    chars.Enqueue('D');
//    for (int j = 0; j < 4; j++)
//    {
//        Option opt = new Option()
//        {
//            Content = Guid.NewGuid().ToString(),
//            IsAnswer = false,
//            OptionChar = chars.Dequeue(),
//        };
//        options.Add(opt);
//    }
//    options[Random.Shared.Next(options.Count)].IsAnswer = true;
//    Question question = new Question()
//    {
//        QuestionType = QuestionType.Single,
//        Options = options,
//        Title = Guid.NewGuid().ToString(),
//        PicPath = "E:\\Documents\\驾考一点通\\js\\ybjk.jpg"
//    };
//    context.Questions.Add(question);
//}
//for (int i = 0; i < 20; i++)
//{
//    List<Option> options = new List<Option>();
//    Queue<char> chars = new Queue<char>();
//    chars.Enqueue('A');
//    chars.Enqueue('B');
//    chars.Enqueue('C');
//    chars.Enqueue('D');
//    for (int j = 0; j < 4; j++)
//    {
//        Option opt = new Option()
//        {
//            Content = Guid.NewGuid().ToString(),
//            IsAnswer = false,
//            OptionChar = chars.Dequeue(),
//        };
//        options.Add(opt);
//    }
//    for(int k = 0;k < 2; k++)
//    {
//        options[Random.Shared.Next(4)].IsAnswer = true;
//    }
//    Question question = new Question()
//    {
//        QuestionType = QuestionType.Multiple,
//        Options = options,
//        Title = Guid.NewGuid().ToString(),
//        PicPath = "E:\\Documents\\驾考一点通\\js\\ybjk.jpg"
//    };
//    context.Questions.Add(question);
//}
//context.SaveChanges();

app.MapControllers();

app.Run();

