using Microsoft.EntityFrameworkCore;
using ContosoPizza.Infrastructure.Data;
using ContosoPizza.Domain.Interfaces;
using ContosoPizza.Infrastructure.Repositories;



var builder = WebApplication.CreateBuilder(args);

// ==================== CONFIGURAÇÃO DE SERVIÇOS ====================

// Adiciona o contexto do banco de dados com a string de conexão
builder.Services.AddControllers();


// Configurar Swagger/OpenAPI (usando Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==================== ENTITY FRAMEWORK CORE + POSTGRESQL ====================

// Registrar ApplicationDbContext com PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(Options =>
{
    // Obtém a string de conexão do appsettings.json
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    // Configura o DbContext para usar PostgreSQL
    Options.UseNpgsql(connectionString); // Nessa linha está a configuração do provedor PostgreSQL

    // Habilita o log de consultas SQL no console (opcional)
    if (builder.Environment.IsDevelopment()) // se estiver em ambiente de desenvolvimento
    {
        Options.EnableSensitiveDataLogging();
        Options.EnableDetailedErrors();
    }
});


// ==================== DEPENDENCY INJECTION (DI) ====================

// TODO: Registrar repositórios e services aqui
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
// builder.Services.AddScoped<IPizzaService, PizzaService>();

// ==================== BUILD DA APLICAÇÃO ====================

var app = builder.Build();


// ==================== MIDDLEWARES ====================


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ==================== EXECUTAR APLICAÇÃO ====================


app.Run();






