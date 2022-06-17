using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    app.UseAuthorization(); //is the user allowed to use the particular endpoint?

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

//Setup HTTP Pipeline:

ConfigurePipeline(app);
app.Run();



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
