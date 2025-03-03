using Microsoft.EntityFrameworkCore;
using PTemp_Cabrera.Data;
using PTemp_Cabrera.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Conexion a la base de datos
builder.Services.AddDbContext<DbtempCabreraContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();


/*
NOTAS ADICIONALES:
Proyecto creado con VS CODE, .NET 7.0

Commando Scaffold para creacion de Modelos en base a Tablas (Enfoque Database First):
dotnet ef dbcontext scaffold "Server=localhost;Database=DBTemp_Cabrera;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Models

*/