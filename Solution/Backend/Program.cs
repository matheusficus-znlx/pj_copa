using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient("Katalyst", client =>
{
    client.BaseAddress = new Uri("https://development-internship-api.geopostenergy.com/WorldCup/");
});

builder.Services.AddScoped<IKatalystApiService, KatalystApiService>();
builder.Services.AddScoped<ICupService, CupService>();
builder.Services.AddScoped<ISimulationService, SimulationService>();
builder.Services.AddScoped<IKnockoutService, KnockoutService>();
builder.Services.AddScoped<IWorldCupWorkflowService, WorldCupWorkflowService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.MapControllers();
app.Run();
