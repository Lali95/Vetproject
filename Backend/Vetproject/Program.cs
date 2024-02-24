using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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


void InitializeDb()
{
    using var db = new MedicineContext();
    InitializeMedicines();
  

    void InitializeMedicines()
    {
        db.Add(new Medicine() { Name = "Phenylbutazone", Amount  = 5});
        db.Add(new Medicine() { Name = "Flunixin meglumine (Banamine)", Amount  = 5});
        db.Add(new Medicine() { Name = "Omeprazole (GastroGard)", Amount  = 5});
        db.Add(new Medicine() { Name = "Firocoxib (Equioxx)", Amount  = 5});
        db.Add(new Medicine() { Name = "Polysulfated glycosaminoglycan (Adequan)", Amount  = 5});
        db.Add(new Medicine() { Name = "Hyaluronic acid", Amount  = 5});
        db.Add(new Medicine() { Name = "Dexamethasone", Amount  = 5});
        db.Add(new Medicine() { Name = "Prednisolone", Amount  = 5});
        db.Add(new Medicine() { Name = "Tetracycline", Amount  = 5});
        db.Add(new Medicine() { Name = "Ivermectin", Amount  = 5});
        db.Add(new Medicine() { Name = "Pyrantel pamoate", Amount  = 5});
        db.Add(new Medicine() { Name = "Praziquantel", Amount  = 5});
        db.Add(new Medicine() { Name = "Gentamicin", Amount  = 5});
        db.Add(new Medicine() { Name = "Sulfadiazine/Trimethoprim", Amount  = 5});
        db.Add(new Medicine() { Name = "Ketoprofen", Amount  = 5});
        db.Add(new Medicine() { Name = "Acepromazine", Amount  = 5});
        db.Add(new Medicine() { Name = "Clenbuterol", Amount  = 5});
        db.Add(new Medicine() { Name = "Isoxsuprine", Amount  = 5});
        db.Add(new Medicine() { Name = "Methocarbamol", Amount  = 5});
        db.Add(new Medicine() { Name = "Epsom salts (magnesium sulfate)", Amount  = 5});
        db.SaveChanges();
    }
}

InitializeDb();


app.Run();

void AddServices(IServiceCollection services)
{
    services.AddControllers();
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