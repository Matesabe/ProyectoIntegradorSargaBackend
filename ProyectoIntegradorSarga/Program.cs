using AppLogic.UseCase.ProdUC;
using AppLogic.UseCase.ProductUC;
using AppLogic.UseCase.PromotionUC;
using AppLogic.UseCase.PurchaseUC;
using AppLogic.UseCase.RedemptionUC;
using AppLogic.UseCase.ReportUC;
using AppLogic.UseCase.User;
using AppLogic.UseCase.UserUC;
using BusinessLogic.RepositoriesInterfaces;
using BusinessLogic.RepositoriesInterfaces.ProductsInterface;
using BusinessLogic.RepositoriesInterfaces.PromotionInterface;
using BusinessLogic.RepositoriesInterfaces.PurchaseInterface;
using BusinessLogic.RepositoriesInterfaces.RedemptionInterface;
using BusinessLogic.RepositoriesInterfaces.ReportsInterface;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using Infrastructure.DataAccess.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Promotion;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.DTOs.Redemption;
using SharedUseCase.DTOs.Reports;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Product;
using SharedUseCase.InterfacesUC.Purchase;
using SharedUseCase.InterfacesUC.Redemption;
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
builder.Services.AddScoped<IRemove<UserDto>, DeleteUser>();
builder.Services.AddScoped<IUpdate<UserDto>, UpdateUser>();
builder.Services.AddScoped<IGetByCi<UserDto>, GetByCiUser>();

//Inyecciones para los Caso de Uso de Promoción
builder.Services.AddScoped<IRepoPromotion, PromotionRepo>();
builder.Services.AddScoped<IGetAll<PromotionDto>, GetAllPromotions>();
builder.Services.AddScoped<IGetById<PromotionDto>, GetByIdPromotion>();
builder.Services.AddScoped<IAdd<PromotionDto>, AddPromotion>();
builder.Services.AddScoped<IUpdate<PromotionDto>, UpdatePromotion>();
builder.Services.AddScoped<IRemove<PromotionDto>, DeletePromotion>();

//Inyecciones para los Caso de Uso de Producto
builder.Services.AddScoped<IRepoProducts, ProductRepo>();
builder.Services.AddScoped<IGetAll<ProductDto>, GetAllProducts>();
builder.Services.AddScoped<IGetById<ProductDto>, GetByIdProduct>();

// Inyecciones para los Caso de Uso de subProducto
builder.Services.AddScoped<IRepoSubProduct, SubProductRepo>();
builder.Services.AddScoped<IGetAll<SubProductDto>, GetAllSubProducts>();
builder.Services.AddScoped<IGetById<SubProductDto>, GetByIdSubProduct>();
builder.Services.AddScoped<IGetByProductId<SubProductDto>, GetByProductId>();
builder.Services.AddScoped<IGetByProductCode<ProductDto>, GetByProductCode>();
builder.Services.AddScoped<IGetByBrand<ProductDto>, GetByBrand>();

// Inyecciones de los Caso de Uso de Compras
builder.Services.AddScoped<IRepoPurchase, PurchaseRepo>();
builder.Services.AddScoped<IGetPurchaseByClientId<PurchaseDto>, GetPurchaseByClientId>();
builder.Services.AddScoped<IGetById<PurchaseDto>, GetByIdPurchase>();

//Inyecciones de los Caso de Uso de Canjes
builder.Services.AddScoped<IRepoRedemption, RedemptionRepo>();
builder.Services.AddScoped<IAdd<RedemptionDto>, AddRedemption>();
builder.Services.AddScoped<IGetAll<RedemptionDto>, GetAllRedemptions>();
builder.Services.AddScoped<IGetById<RedemptionDto>, GetByIdRedemption>();
builder.Services.AddScoped<IGetRedemptionByUserId<RedemptionDto>, GetRedemptionByUserId>();
builder.Services.AddScoped<IUpdate<RedemptionDto>, UpdateRedemption>();
builder.Services.AddScoped<IRemove<RedemptionDto>, DeleteRedemption>();

builder.Services.AddScoped<IRepoReport, ReportRepo>();
builder.Services.AddScoped<IGetAll<ReportDto>, GetAllReports>();

// Inyección de SeedData para la inicialización de datos
builder.Services.AddScoped<SeedData>(); // Inyección del SeedData para la inicialización de datos

// Add services to the container.
builder.Services.AddDbContext<SargaContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
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

//Configuración de CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:3939", //  desarrollo local
                "https://proud-desert-0f4937d0f.6.azurestaticapps.net" //  dominio de producción
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<SeedData>();
    seeder.Run();
}


app.UseHttpsRedirection();

app.UseCors("AllowLocalhost"); 

app.UseAuthentication();
app.UseAuthorization(); 


app.MapControllers();

app.Run();

