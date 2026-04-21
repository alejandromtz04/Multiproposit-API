using ApiLogin.Extensions; // Importamos las extensiones para no saturar el program
using ApiLogin.Infraestructure.DB;
// using ApiLogin.Endpoints; //  importar tus Minimal API aqui

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Base de Datos Inicial
ComunDB.Configure(builder.Configuration);

// 2. REGISTRO DE SERVICIOS
builder.Services.AddInfrastructureServices(builder.Configuration);

// 3. Swagger y Controllers (Mantenemos AddControllers por si aún tienes Controladores clásicos)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. CONSTRUCCIÓN DE LA APP
var app = builder.Build();

// 5. Configuración del Pipeline (Middleware)
app.UseCors("_CEPAPolicy");

AppDomain.CurrentDomain.SetData("ContentRootPath", app.Environment.ContentRootPath);
AppDomain.CurrentDomain.SetData("WebRootPath", app.Environment.WebRootPath);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Primero Autenticación y luego Autorización
app.UseAuthentication();
app.UseAuthorization();

// 5. MAPEO DE ENDPOINTS
// Aqui iran las Minimal APIs
AuthEndpoints.Map(app);
// UsersEndpoints.Map(app);
// ForecastEndpoints.Map(app);

// activo mientras se usen controllers
app.MapControllers();
app.Run();