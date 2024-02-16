using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Viventium.Assignment.Abstractions;
using Viventium.Assignment.Context;
using Viventium.Assignment.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDbContext<CompanyDbContext>(ServiceLifetime.Scoped)
    .AddScoped<ICompanyRepo, CompanyRepo>()
    .AddSingleton<ICsvParser, CsvParser>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
        .UseDeveloperExceptionPage()
        .UseSwagger()
        .UseSwaggerUI();
}

app
    .UseRouting()
    .UseEndpoints(x => x.MapControllers());

app.MapControllers();
app.UseHttpsRedirection();

using (IServiceScope scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
    await db.Database.MigrateAsync();
}

app.Run();