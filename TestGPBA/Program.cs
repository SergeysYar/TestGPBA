using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TestGPBA; 

var builder = WebApplication.CreateBuilder(args);

// Добавьте строку подключения для использования SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавьте сервисы для контроллеров
builder.Services.AddControllers();

// Добавьте поддержку статических файлов и SPA
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot";
});

var app = builder.Build();

// Используйте статические файлы
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Используйте статические файлы для SPA
if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}

app.UseRouting();
app.UseAuthorization();

// Настройте конечные точки
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Инициализация базы данных
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(dbContext);
}

// Настройка SPA
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "wwwroot";
});

// Запуск приложения
app.Run();
