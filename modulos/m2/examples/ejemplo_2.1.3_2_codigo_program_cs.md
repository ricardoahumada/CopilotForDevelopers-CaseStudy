# Código C#: Program.cs para ASP.NET Core 8

**Contexto de uso:** Archivo Program.cs generado con configuración completa de servicios.

**Archivo Program.cs generado:**

```csharp
using PortalEmpleo.Infrastructure.Data;
using PortalEmpleo.Infrastructure.Identity;
using PortalEmpleo.Application;
using PortalEmpleo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseApiServices();

app.MapControllers();

app.Run();
```

**Métodos de extensión usados:**

| Método | Capa | Registra |
|--------|------|----------|
| `AddApplication()` | Application | Services, MediatR, AutoMapper |
| `AddInfrastructure()` | Infrastructure | DbContext, Repositories |
| `AddApiServices()` | API | Controllers, Swagger, Filters |
| `UseApiServices()` | API | Middleware pipeline |
