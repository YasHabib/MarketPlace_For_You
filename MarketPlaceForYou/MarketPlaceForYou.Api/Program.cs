using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;

void ConfigureHost(ConfigureHostBuilder host)
{
}


void ConfigureServices(WebApplicationBuilder builder)

{
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
    builder.Services.AddControllers();

    //Setup dependency injection
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IListingService, ListingService>();

}

//Setup HTTP request/response pipeline: (Gets used when the application is running ie APIs)


void ConfigurePipeline(WebApplication app)
{

    //app.UseHttpsRedirection(); //This will redirect to https if the request is from http

    //allow hosting of static web pages
    if (!app.Environment.IsProduction())
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();
    }
    app.UseAuthentication();
    
    app.UseAuthorization(); //:is the user allowed to use the particular endpoint?
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


//Design-time factory
//public class DesignTimeMKPFYtFactory : IDesignTimeDbContextFactory<MKPFYDbContext>
//{
//    public MKPFYDbContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<MKPFYDbContext>();
//        optionsBuilder.UseNpgsql("Data Source=mkpfydb");

//        return new MKPFYDbContext(optionsBuilder.Options);
//    }
//}


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
