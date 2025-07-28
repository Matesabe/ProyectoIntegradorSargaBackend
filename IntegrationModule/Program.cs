using AppLogic.UseCase.ProdUC;
using AppLogic.UseCase.ProductUC;
using AppLogic.UseCase.PurchaseUC;
using AppLogic.UseCase.ReportUC;
using AppLogic.UseCase.User;
using AppLogic.UseCase.UserUC;
using AppLogic.UseCase.WarehouseUC;
using BusinessLogic.RepositoriesInterfaces;
using BusinessLogic.RepositoriesInterfaces.ProductsInterface;
using BusinessLogic.RepositoriesInterfaces.PurchaseInterface;
using BusinessLogic.RepositoriesInterfaces.ReportsInterface;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using BusinessLogic.RepositoriesInterfaces.WarehouseInterface;
using Infrastructure.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.DTOs.Reports;
using SharedUseCase.DTOs.User;
using SharedUseCase.DTOs.Warehouse;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Product;
using SharedUseCase.InterfacesUC.Purchase;
using SharedUseCase.InterfacesUC.Warehouse;

var builder = WebApplication.CreateBuilder(args);

// Inyecciones para los Caso de Uso de depósitos
builder.Services.AddScoped<IRepoWarehouse, WarehouseRepo>();
builder.Services.AddScoped<IClearStocks, ClearStocks>();
builder.Services.AddScoped<IUpdateStocks, UpdateStocks>();
builder.Services.AddScoped<IGetAll<WarehouseDto>, GetAllWarehouses>();

// Inyecciones para los Caso de Uso de Productos
builder.Services.AddScoped<IRepoProducts, ProductRepo>();
builder.Services.AddScoped<IRepoSubProduct, SubProductRepo>();
builder.Services.AddScoped<IAdd<SubProductDto>, AddSubProduct>();
builder.Services.AddScoped<IGetAll<SubProductDto>, GetAllSubProducts>();
builder.Services.AddScoped<IGetAll<ProductDto>, GetAllProducts>();
builder.Services.AddScoped<IGetById<SubProductDto>, GetByIdSubProduct>();
builder.Services.AddScoped<IUpdate<SubProductDto>, UpdateSubProduct>();
builder.Services.AddScoped<IRemove<SubProductDto>, DeleteSubProduct>();
builder.Services.AddScoped<IClearSubProducts, ClearSubProducts>();
builder.Services.AddScoped<IGetByProductCode<ProductDto>, GetByProductCode>();
builder.Services.AddScoped<IGetByProductCode<SubProductDto>, GetByProductCodeSubProduct>();

//Inyecciones para los caso de uso de purchases
builder.Services.AddScoped<IRepoPurchase, PurchaseRepo>();
builder.Services.AddScoped<IGetAll<PurchaseDto>, GetAllPurchases>();
builder.Services.AddScoped<IGetById<PurchaseDto>, GetByIdPurchase>();
builder.Services.AddScoped<IAdd<PurchaseDto>, AddPurchase>();
builder.Services.AddScoped<IUpdate<PurchaseDto>, UpdatePurchase>();
builder.Services.AddScoped<IRemove<PurchaseDto>, DeletePurchase>();
builder.Services.AddScoped<IClearPurchases, ClearPurchases>();

//Inyecciones para los Caso de Uso de Usuarios
builder.Services.AddScoped<IRepoUser, UserRepo>();
builder.Services.AddScoped<IGetAll<UserDto>, GetAllUsers>();
builder.Services.AddScoped<IGetById<UserDto>, GetByIdUser>();
builder.Services.AddScoped<IGetByCi<UserDto>, GetByCiUser>();

builder.Services.AddScoped<IRepoReport, ReportRepo>();
builder.Services.AddScoped<IGetAll<ReportDto>, GetAllReports>();
builder.Services.AddScoped<IAdd<ReportDto>, AddReport>();

// Inyección de SeedData para la inicialización de datos
builder.Services.AddScoped<SeedData>(); // Inyección del SeedData para la inicialización de datos

// Add services to the container.
builder.Services.AddDbContext<SargaContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
builder.Configuration.AddUserSecrets<Program>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.MapOpenApi();

    //SeedData
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var seeder = services.GetRequiredService<SeedData>();
        seeder.Run();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
