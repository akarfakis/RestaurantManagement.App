using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Api.Endpoints;
using RestaurantManagement.Api.Handlers;
using RestaurantManagement.Api.Mapping;
using RestaurantManagement.Db.Services;
using RestaurantManagement.Db.SqlServer;
using RestaurantManagement.Services.Services.Registrators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), options =>
    {
        _ = options.MigrationsAssembly("RestaurantManagement.Db");
    });

});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddMigrationService();
builder.Services.AddServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var migrationService = scope.ServiceProvider.GetService<IMigrationService>();
    await migrationService.UpdateSchemaAsync();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler(o => { });
app.RegisterEndpoints();
app.Run();
