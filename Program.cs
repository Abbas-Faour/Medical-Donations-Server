using System.Text;
using API.Config;
using AutoMapper;
using Azure.Storage.Blobs;
using Core.Entites;
using Core.Entites.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IdentitySeeder>();

// Services & Repositories

builder.Services.AddScoped<IUnitOfWork,UnitofWork>();
builder.Services.AddScoped<IUserRepo,UserService>();
builder.Services.AddScoped<ICategoriesRepo,CategoriesService>();
builder.Services.AddScoped<IMedicineRepo,MedicineService>();
builder.Services.AddScoped<IFeedbackRepo,FeedbacksService>();

 // Injecting the connection string from the app settings
var blobConnection = builder.Configuration.GetValue<string>("BlobConnection");

// injecting the blobserviceclient into out DI container
builder.Services.AddSingleton(x => new BlobServiceClient(blobConnection));

// inject the blobservice into the DI
builder.Services.AddSingleton<IBlobService, BlobService>();
builder.Services.Configure<PhotoSettings>(builder.Configuration.GetSection("PhotoSettings"));

builder.Services.AddAutoMapper();

// JWT

builder.Services.AddScoped<IAuthRepo,AuthService>();
builder.Services.AddScoped<IAdminRepo,AdminService>();

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false, // Make true on production
            ValidateAudience = false, // Make true on production
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };
        o.Events = new JwtBearerEvents {
            OnAuthenticationFailed = context => {
                if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("IS-TOKEN-EXPIRED","true");
                }
                return Task.CompletedTask;
            }
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

// Seeding Database with data in the SeedingIdentity Class
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await IdentitySeeder.SeedDbAsync(userManager,roleManager);
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
