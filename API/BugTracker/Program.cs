using BugTracker.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add app services (from ApplicationServices Extension)
builder.Services.AddApplicationServices(builder.Configuration);

//add CORS
builder.Services.AddCors();

//add authorization services (from IdentityServices Extension)
builder.Services.AddIdentityServices(builder.Configuration);



var app = builder.Build();



//Middleware: Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//match http request to endpoint
app.UseRouting();

//implement CORS before auth/authen
app.UseCors(policy=>policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

//JWT authentication
app.UseAuthentication();

//check browser is authorized to access requested info
app.UseAuthorization();

//create/execute http endpoints for routed controllers
app.MapControllers();

app.Run();
