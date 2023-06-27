using jwt.Data;
using jwt.Repository;
using jwt.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using jwt.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployee,implementIEmployee>();



builder.Services.Configure<jwtConfig>(builder.Configuration.GetSection("JWT"));
 

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme; 
    opt.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(jwt=>{
        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWT:SecretKey").Value);
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey=new SymmetricSecurityKey(key),
            ValidateIssuer=false,
            ValidateAudience=false,
            ValidateLifetime=true,
            RequireExpirationTime=false
    
        };
    });


builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
}) .AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddLogging(options =>options.AddConsole());
builder.Services.AddTransient<GlobalErrorHandling>();


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

app.UseMiddleware<GlobalErrorHandling>();

app.MapControllers();

app.Run();
