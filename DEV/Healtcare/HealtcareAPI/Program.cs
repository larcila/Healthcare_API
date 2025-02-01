using Healthcare.BR;
using Healthcare.BR.BL;
using Healthcare.Common.Action;
using Healthcare.Common.ActionBL;
using HealthcareAPI.Logs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Healthcare API",
        Version = "v1",
        Description = "API to manage patients and follow-ups in Healthcare."
    });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter the token in the following format: Bearer {token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
     {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.WebHost.UseIISIntegration(); // Permite a IIS controlar la API
// Ensure appsettings.json file is loaded
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//to test if appsetting loads
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new string[0];
Console.WriteLine("Allowed Origins: " + string.Join(", ", allowedOrigins));
// Allowed domains to CORS
builder.Services.ConfigureCorsFromConfig(builder.Configuration);


builder.Configuration.AddEnvironmentVariables();
//Get connectionString fromappsettings.json
var connectionString = builder.Configuration.GetConnectionString("HealthcareConnection")
    ?? throw new ArgumentNullException("HealthcareConnection", "Connection string not found in appsettings.json");

//Add service using Healthcare.BR (Add Dependencies)
builder.Services.AddHealthcareServices(connectionString);
builder.Services.AddScoped<IPatientBL, PatientBL>();
builder.Services.AddScoped<IFollowUpBL, FollowUpBL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IRole_By_UserBL, Role_By_UserBL>();


//Autentication and Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
    };
});


var app = builder.Build();

// Try-Cacth error middleware
app.UseExceptionHandlingMiddleware();

// Use CORS
app.UseCors(app.Environment.IsDevelopment() ?"AllowAllOrigins" : "ConfiguredCorsPolicy");
//app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Healthcare API V1");
    });
}

//Security Policy (CSP)
//Configuration with Swagger is ignored
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
{
    app.UseSecurityHeaders(policies =>
    {
        policies.AddContentSecurityPolicy(builder =>
        {
            //Security Policy (CSP)
            builder.AddDefaultSrc().Self();
            builder.AddScriptSrc().Self().WithNonce();
            builder.AddStyleSrc().Self().WithNonce();
            builder.AddImgSrc().Self();
            builder.AddFontSrc().Self();
            builder.AddFrameAncestors().None();

            policies.AddCustomHeader("X-Content-Type-Options", "nosniff"); // Prevents MIME-sniffing
            policies.AddCustomHeader("X-Frame-Options", "DENY"); //Prevents clickjacking.
            policies.AddCustomHeader("Referrer-Policy", "strict-origin-when-cross-origin");
            policies.AddCustomHeader("X-XSS-Protection", "1; mode=block"); // Prevents XSS attacks
            policies.AddCustomHeader("Strict-Transport-Security", "max-age=31536000; includeSubDomains");

            //Security Header
            policies.AddDefaultSecurityHeaders(); // basic configuration
            policies.AddFrameOptionsDeny(); // Para X-Frame-Options: DENY.
            policies.AddXssProtectionEnabled(); // Para X-XSS-Protection: 1; mode=block.
            policies.AddContentTypeOptionsNoSniff(); // Para X-Content-Type-Options: nosniff.
            policies.AddReferrerPolicyStrictOriginWhenCrossOrigin(); // Para Referrer-Policy.
            policies.AddStrictTransportSecurityMaxAgeIncludeSubDomains(31536000); // Para HSTS.
        });
    });
});


if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapControllers();
app.Run();
