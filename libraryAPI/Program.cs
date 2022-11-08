using Microsoft.EntityFrameworkCore;
using libraryAPI.EfCore;
using libraryAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EF_DataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Ef_Postgres_Db"))
);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
// AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
Console.WriteLine("Going to sleep....");
System.Threading.Thread.Sleep(15000);
Console.WriteLine("Continuing execution....");
app.MigrationDatabase();
app.UseAuthorization();

app.MapControllers();

app.Run();