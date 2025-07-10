using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Connection;
using DataAccessLayer.DTOs;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using BusinessLayer.Common.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.StackExchangeRedis;

//using BusinessLogicLayer.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedGlobal", config =>
    {
        config.PermitLimit = 100;
        config.Window = TimeSpan.FromMinutes(1);
        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        config.QueueLimit = 0;
    });

    options.AddFixedWindowLimiter("FixedSelective", config =>
    {
        config.PermitLimit = 10;  // Buradaki endpoint’ler için 10 istek/dakika
        config.Window = TimeSpan.FromMinutes(1);
        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        config.QueueLimit = 0;
    });

    options.AddSlidingWindowLimiter("loginRateLimit", config =>
    {
        config.PermitLimit = 3;           // 1 dakika içinde izin verilen maksimum istek sayýsý (toplam izin)
        config.SegmentsPerWindow = 10;      // 1 dakikalýk pencereyi kaç parçaya böleceðimiz (örn. 10 parçaya bölünür)
        config.Window = TimeSpan.FromSeconds(10);  // Rate limiting için zaman aralýðý (burada 1 dakika)
        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;  // Kuyrukta bekleyen istekler varsa öncelik sýrasý
        config.QueueLimit = 0;               // Kuyruktaki bekleyen istek sayýsý limiti (0 ise kuyruk yok, ekstra istekler reddedilir)
    });


    options.RejectionStatusCode = 429;
});


builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse("Redis:ConnectionString");
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // Redis baðlantý adresi
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT token'ýnýzý buraya girin. Örn: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        string secret = builder.Configuration.GetValue<string>("AppSettings:SecretKey");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            RoleClaimType = "role" // sade anahtar
        };
    });

// Role bazlý yetkilendirme
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin"));
});









builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IImagesService, ImagesService>();
builder.Services.AddScoped<IOrderTransactionService, OrderTransactionService>();
builder.Services.AddScoped<ICustomerAddressesService, CustomerAddressesService>();
builder.Services.AddSingleton<IRedisService, RedisService>();
builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
builder.Services.AddScoped<DatabaseExecutions>();
builder.Services.AddScoped<ParameterList>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// middleware olarak adlandýrýlýyorlar.
app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseCors("AllowAngular");

app.UseAuthentication(); // ?? Bu satýr önemli!

app.UseAuthorization();

app.MapControllers();

app.Run();

