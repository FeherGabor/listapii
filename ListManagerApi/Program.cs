using ListManagerApi.Services; // <- szükséges a FileStorageService használatához

var builder = WebApplication.CreateBuilder(args);

// Szolgáltatások regisztrálása
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS engedélyezése
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// 🔥 Fájlalapú tároló szolgáltatás regisztrálása
builder.Services.AddSingleton<FileStorageService>();

var app = builder.Build();

// Middleware konfiguráció
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll"); // <- FONTOS!

app.UseAuthorization();

app.MapControllers();

app.Run();
