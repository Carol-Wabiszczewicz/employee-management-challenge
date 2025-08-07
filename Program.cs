using EmployeeManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;


public partial class Program { 
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddFluentValidation();;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();      
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(context);
}


app.UseAuthorization();
app.MapControllers();
app.Run();
} 