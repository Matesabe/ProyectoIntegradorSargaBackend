using AppLogic.UseCase.ProductUC;
using AppLogic.UseCase.User;
using AppLogic.UseCase.UserUC;
using AppLogic.UseCase.WarehouseUC;
using BusinessLogic.RepositoriesInterfaces;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using BusinessLogic.RepositoriesInterfaces.WarehouseInterface;
using Infrastructure.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Product;
using SharedUseCase.InterfacesUC.Warehouse;

var builder = WebApplication.CreateBuilder(args);

// Inyecciones para los Caso de Uso de depósitos
builder.Services.AddScoped<IRepoWarehouse, WarehouseRepo>();
builder.Services.AddScoped<IClearStocks, ClearStocks>();
builder.Services.AddScoped<IUpdateStocks, UpdateStocks>();

// Inyecciones para los Caso de Uso de Productos
builder.Services.AddScoped<IRepoSubProduct, SubProductRepo>();
builder.Services.AddScoped<IAdd<SubProductDto>, AddSubProduct>();
builder.Services.AddScoped<IGetAll<SubProductDto>, GetAllSubProducts>();
builder.Services.AddScoped<IGetById<SubProductDto>, GetByIdSubProduct>();
builder.Services.AddScoped<IUpdate<SubProductDto>, UpdateSubProduct>();
builder.Services.AddScoped<IRemove<SubProductDto>, DeleteSubProduct>();
builder.Services.AddScoped<IClearSubProducts, ClearSubProducts>();

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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
