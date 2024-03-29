using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text.Json.Serialization;

void ConfigureHost(ConfigureHostBuilder host)
{
    //Retrieving parameters in AWS Parameter Store
    host.ConfigureAppConfiguration((builder) =>
    {
    //builder.AddSystemsManager(string.Format("/Live/{0}/",
    //        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")))
    //        .AddSystemsManager(string.Format("/Live/Common/"));
    });
}


void ConfigureServices(WebApplicationBuilder builder)
{
    //Setup CORs
    builder.Services.AddCors(option => option.AddPolicy("allowCORs", build =>
    {
        build.WithOrigins("http://localhost:3000", "http://marketforyouyh-env.eba-fqgiudi2.ca-central-1.elasticbeanstalk.com", "https://d2mz5o15b3dsx3.cloudfront.net").AllowAnyMethod().AllowAnyHeader();
    }));

    //Setup the database using the ApplicationDbContext
    builder.Services.AddDbContext<MKPFYDbContext>(options =>
        options.UseNpgsql( //connect to postgres db
            builder.Configuration.GetConnectionString("DefaultConnection"),
            npgsqlOptions =>
            {
                //configure what project we want to store our Code-First Migration in
                npgsqlOptions.MigrationsAssembly("MarketPlaceForYou.Repositories");
            }
        ));

    //setup authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration.GetSection("Auth0").GetValue<string>("Domain");
            options.Audience = builder.Configuration.GetSection("Auth0").GetValue<string>("Audience");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                RoleClaimType = "http://schemas.marketforyou.com/roles"
            };

        });

    //swagger
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "MarketForYou API", Version = "V1" });
        options.UseInlineDefinitionsForEnums();


        var apiXmlFile = Path.Combine(AppContext.BaseDirectory, "MarketPlaceForYou.Api.xml");
        var modelsXmlFile = Path.Combine(AppContext.BaseDirectory, "MarketPlaceForYou.Models.xml");
        options.IncludeXmlComments(apiXmlFile);
        options.IncludeXmlComments(modelsXmlFile);

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token in the text input below.\r\n\r\nExample: \"12345abcdef\"",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },

                },
            new string[]{}
            }
        });
    });
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    //Setup dependency injection
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IListingService, ListingService>();
    builder.Services.AddScoped<IFAQService, FAQService>();
    builder.Services.AddScoped<IUploadService, UploadService>();
    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<IWebNotificationService, WebNotificationService>();
    builder.Services.AddScoped<IAPListingService, APListingService>();
    builder.Services.AddScoped<IAPDashboardService, APDashboardService>();
}



//Setup HTTP request/response pipeline: (Gets used when the application is running ie APIs)
void ConfigurePipeline(WebApplication app)
{
    //app.UseHttpsRedirection(); //This will redirect to https if the request is from http
    app.UseCors("allowCORs");

    //allow hosting of static web pages
    if (!app.Environment.IsProduction())
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseSwagger(); //this will make swagger json file available. 

        //make swagger UI available at /swagger
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarketForYou V1");
        });
    }
    //if(User.IsDeleted == false && User.IsBlocked == false)
    //{
    app.UseAuthentication();
    //}
    app.UseAuthorization(); //=is the user allowed to use the particular endpoint?
    app.MapControllers();
}

//Execute DB migration:

void ExecuteMigrations(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<MKPFYDbContext>();
        context.Database.Migrate();
    }
}

//WebApplicationBuilder Instance(used to do low - level configuration and to start the application)

var builder = WebApplication.CreateBuilder(args);

//Setup the application:

ConfigureHost(builder.Host);
ConfigureServices(builder);
var app = builder.Build();

// Execute migrations on startup
ExecuteMigrations(app);

//Setup HTTP Pipeline:

ConfigurePipeline(app);
app.Run();



//public class DesignTimeMKPFYtFactory : IDesignTimeDbContextFactory<MKPFYDbContext>
//{
//    /// <summary>
//    /// Design time for migrations
//    /// </summary>
//    public MKPFYDbContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<MKPFYDbContext>();
//        optionsBuilder.UseNpgsql("Data Source=mkpfydb");

//        return new MKPFYDbContext(optionsBuilder.Options);
//    }
//}

