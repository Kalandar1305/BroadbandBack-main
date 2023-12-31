using System.Text;
using BroadBandBillingPaymentSystem.Data;
using BroadBandBillingPaymentSystem.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var connectionString = config.GetConnectionString("default");

//Configure DbContext with SQL
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString),ServiceLifetime.Transient);

// Add services to the container.
builder.Services.AddScoped<TokenService>();
builder.Services.AddTransient<CustomerService>();
builder.Services.AddTransient<AccountService>();
builder.Services.AddTransient<BillService>();
builder.Services.AddTransient<TarrifPlanService>();
builder.Services.AddTransient<FeedbackService>();
builder.Services.AddTransient<AdminService>();
builder.Services.AddTransient<HomePageService>();

builder.Services.AddControllers();

//Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
})
;

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
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

app.MapControllers();

AppDbContext.SeedAdmin(app);

app.Run();
