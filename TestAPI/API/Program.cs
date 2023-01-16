//using API.Dal;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors();

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
//});

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(typeof(AuthorizeAttribute));
//});

//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

// Automapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// This is need for windows auth in development
//builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);

// DAL - Sql - Ef
//builder.Services.AddDbContext<SportiveContext>(o =>
//{
//    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), o =>
//    {
//        o.EnableRetryOnFailure();
//    });
    
//});

//// Config help class
//builder.Services.Configure<AppSettingsSecret>(builder.Configuration.GetSection("AppSettings:Secret"));


// Serlog
//builder.Services.AddLogging(x =>
//{
//    x.ClearProviders();
//    x.AddSerilog(dispose: true);
//});
//builder.Services.AddSingleton(Log.Logger);


// Register DAL
//builder.Services.AddDal();
//builder.Services.AddServices();
//builder.Services.AddScoped<SpRequest>();

builder.Services.AddControllersWithViews()
             .AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


// Options - appsetiing
//builder.Services.Configure<AppSettingsSecret>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddHttpContextAccessor();


//string destPath = builder.Configuration["LogPath:DubugFile"];
//SelfLog.Enable(msg =>
//{
//    File.AppendAllText(destPath, msg);
//    Debug.Print(msg);
//});


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.File(builder.Configuration["LogPath:LogFile"], rollingInterval: RollingInterval.Day)
    .CreateLogger();


builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.File(builder.Configuration["LogPath:LogFile"], rollingInterval: RollingInterval.Day);
});

var app = builder.Build();

app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true) // allow any origin
              .AllowCredentials()); // allow credentials


// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "My API");

    });
}

//app.RegisterMiddeleare();

//app.UseMiddleware<JwtMiddleware>();

app.MapControllers();
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = (context) =>
    {
        if (context.File.Name == "index.html")
        {
            context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
            context.Context.Response.Headers.Add("Expires", "-1");
        }
        else
        {
            context.Context.Response.Headers["Cache-Control"] = builder.Configuration["StaticFiles:Headers:Cache-Control"];
        }
    }
});

app.Run();
