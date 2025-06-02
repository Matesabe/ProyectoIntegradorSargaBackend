using AppLogic.UseCase.User;
using AppLogic.UseCase.UserUC;
using BusinessLogic.RepositoriesInterfaces;
using Infrastructure.DataAccess.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyecciones para los Caso de Uso de Usuario
builder.Services.AddScoped<IRepoUser, UserRepo>();
builder.Services.AddScoped<IGetAll<UserDto>, GetAllUsers>();
builder.Services.AddScoped<IGetById<UserDto>, GetByIdUser>();
builder.Services.AddScoped<IGetByName<UserDto>, GetByNameUser>();
builder.Services.AddScoped<IGetByEmail<UserDto>, GetByEmailUser>();
builder.Services.AddScoped<IAdd<UserDto>, AddUser>();
builder.Services.AddScoped<IRemove, DeleteUser>();
builder.Services.AddScoped<IUpdate<UserDto>, UpdateUser>();
builder.Services.AddScoped<IGetByCi<UserDto>, GetByCiUser>();

builder.Services.AddScoped<SeedData>(); // Inyección del SeedData para la inicialización de datos


// Inyecta el contex y la cadena de conexion que la toma desde el json
//.Services.AddDbContext<LibreriaContext>();
// Add services to the container.
builder.Services.AddDbContext<SargaContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("PruebaUsuarios"))
);

//Configuración de seguridad
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
//Configuración de Swagger, headers de tokens
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingrese un token JWT con el prefijo 'Bearer '",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

var config = new ConfigurationBuilder()
    .Build();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var seeder = services.GetRequiredService<SeedData>();
        seeder.Run();
    }

    //using (var scope = app.Services.CreateScope())
    //{
    //    var services = scope.ServiceProvider;
    //    //var context = services.GetRequiredService<LibreriaContext>();
    //    //context.Database.Migrate(); // o EnsureCreated() según tu caso
    //    var seeder = services.GetRequiredService<SeedData>();
    //    seeder.Run();
    //}


}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

