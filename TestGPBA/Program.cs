using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TestGPBA; 

var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� ��� ������������� SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// �������� ������� ��� ������������
builder.Services.AddControllers();

// �������� ��������� ����������� ������ � SPA
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot";
});

var app = builder.Build();

// ����������� ����������� �����
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// ����������� ����������� ����� ��� SPA
if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}

app.UseRouting();
app.UseAuthorization();

// ��������� �������� �����
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// ������������� ���� ������
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(dbContext);
}

// ��������� SPA
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "wwwroot";
});

// ������ ����������
app.Run();
