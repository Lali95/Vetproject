using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Vetproject.Data;
using Vetproject.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AddServices(builder.Services);
ConfigureSwagger(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API v1");
    });
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.MapControllers();

// Initialize the database
//InitializeDb();


app.Run();

void AddServices(IServiceCollection services)
{
    services.AddControllers();
// Add DbContext with dependency injection
    services.AddDbContext<MedicalRecordDbContext>(options =>
        options.UseSqlServer("Server=localhost,1433;Database=vetproject;Persist Security Info=False;User ID=SA;Password=VetProject2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=300;Initial Catalog=vetproject"));

    // Add DbContext with dependency injection
    services.AddDbContext<MedicineContext>(options =>
        options.UseSqlServer("Server=localhost,1433;Database=vetproject;Persist Security Info=False;User ID=SA;Password=VetProject2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=300;Initial Catalog=vetproject"));
}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });
}

void InitializeDb()
{
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    var db = serviceProvider.GetRequiredService<MedicineContext>();

    // Apply migrations and update the database schema
    db.Database.Migrate();

    // Your data initialization logic here
    InitializeMedicines(db);
}

void InitializeMedicines(MedicineContext db)
{
    db.Medicines.AddRange(
        new Medicine { Name = "Phenylbutazone (Bute)", Amount = 5 },
        new Medicine { Name = "Flunixin meglumine (Banamine)", Amount = 5 },
        new Medicine { Name = "Omeprazole (GastroGard)", Amount = 5 },
        new Medicine { Name = "Firocoxib (Equioxx)", Amount = 5 },
        new Medicine { Name = "Polysulfated glycosaminoglycan (Adequan)", Amount = 5 },
        new Medicine { Name = "Hyaluronic acid", Amount = 5 },
        new Medicine { Name = "Dexamethasone", Amount = 5 },
        new Medicine { Name = "Prednisolone", Amount = 5 },
        new Medicine { Name = "Tetracycline", Amount = 5 },
        new Medicine { Name = "Ivermectin", Amount = 5 },
        new Medicine { Name = "Pyrantel pamoate", Amount = 5 },
        new Medicine { Name = "Praziquantel", Amount = 5 },
        new Medicine { Name = "Gentamicin", Amount = 5 },
        new Medicine { Name = "Sulfadiazine/Trimethoprim (SMZ-TMP)", Amount = 5 },
        new Medicine { Name = "Ketoprofen", Amount = 5 },
        new Medicine { Name = "Acepromazine", Amount = 5 },
        new Medicine { Name = "Clenbuterol", Amount = 5 },
        new Medicine { Name = "Isoxsuprine", Amount = 5 },
        new Medicine { Name = "Methocarbamol", Amount = 5 },
        new Medicine { Name = "Epsom salts (magnesium sulfate)", Amount = 5 }
    );

    db.SaveChanges();
}

