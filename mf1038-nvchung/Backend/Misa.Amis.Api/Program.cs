using Misa.Amis.Api.Middlewares;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;
using Misa.Amis.Core.Services;
using Misa.Amis.Infrastructure.Repositories;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Cau hinh json naming policy

builder.Services.AddControllers().AddJsonOptions(opts =>
{
  opts.JsonSerializerOptions.PropertyNamingPolicy = null;
});
//Add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Cau hinh Cors
builder.Services.AddCors(opts =>
{
  opts.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
});
// Dependency Injection
builder.Services.AddSingleton<Func<MySqlConnection>>(_ => () => new MySqlConnection(builder.Configuration.GetConnectionString("Amis")));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.Run();
