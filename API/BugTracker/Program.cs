using BugTracker.Data;
using BugTracker.Extensions;
using BugTracker.Middleware;


//args : command line args
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add app services (from ApplicationServices Extension)
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddTransient<Seed>();

//add CORS
builder.Services.AddCors();

//add authorization services (from IdentityServices Extension)
builder.Services.AddIdentityServices(builder.Configuration);



var app = builder.Build();

//Seed database 
async Task SeedDatabase()
{
    //create scope for service that lives outside the context of a http request
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<Seed>();
        await seeder.SeedData();
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
app.UseCors(policy=>policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

//JWT authentication - identify the user
app.UseAuthentication();

//check user is authorized to access requested info
app.UseAuthorization();

//create/execute http endpoints for routed controllers
app.MapControllers();

//seed data
await SeedDatabase();


await app.RunAsync();

