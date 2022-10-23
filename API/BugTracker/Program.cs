using BugTracker.Data;
using BugTracker.Extensions;
using BugTracker.Middleware;
using Microsoft.EntityFrameworkCore;


//args : command line args
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add app services (from ApplicationServices Extension)
builder.Services.AddApplicationServices(builder.Configuration);

//add authorization services (from IdentityServices Extension)
builder.Services.AddIdentityServices(builder.Configuration);

//add CORS
builder.Services.AddCors();

builder.Services.AddTransient<Seed>();

var app = builder.Build();

//Seed database using command line arg (dotnet run seeddata)
if(args.Length ==1 && args[0].ToLower() == "seeddata")
{
    SeedDatabase(app);
}

void SeedDatabase(IHost app)
{
    //create scope for service that lives outside the context of a http request
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<Seed>();
        seeder.SeedData();
    }
}



//Middleware: Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

//match http request to endpoint
app.UseRouting();

//implement CORS before auth/authen - allow communication between client and API
app.UseCors(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200"));

//JWT authentication - identify the user
app.UseAuthentication();

//check user is authorized to access requested info
app.UseAuthorization();

//create/execute http endpoints for routed controllers
app.MapControllers();


//run application
await app.RunAsync();

