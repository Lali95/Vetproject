using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Vetproject.Data;
using Vetproject.Data.Repository;
using Vetproject.Model;
using Vetproject.Service;

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
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<MedicalRecordDbContext>(options =>
        options.UseSqlServer(connectionString));

    services.AddDbContext<MedicineContext>(options =>
        options.UseSqlServer(connectionString));
    services.AddScoped<IMedicineRepository, MedicineRepository>();
    services.AddScoped<ILoggerService, LoggerService>();


    services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
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
                Array.Empty<string>()
            }
        });
    });
}

void InitializeDb()
{
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    var medicineDbContext = serviceProvider.GetRequiredService<MedicineContext>();

    
    medicineDbContext.Database.Migrate();

    
    InitializeMedicines(medicineDbContext);
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