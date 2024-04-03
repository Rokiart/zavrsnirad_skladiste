using SKladisteAppl.Data;
using Microsoft.EntityFrameworkCore;

using SKladisteAppl.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSkladisteSwaggerGen();
builder.Services.AddSkladisteCORS  ();


// dodavanje baze podataka
builder.Services.AddDbContext<SkladisteContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString(name: "SkladisteContext"))
);
;

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    // moguænost generiranja poziva rute u CMD i Powershell
    app.UseSwaggerUI(opcije =>
    {
        opcije.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        opcije.ConfigObject.
        AdditionalItems.Add("requestSnippetsEnabled", true);
    });
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseDefaultFiles();
app.UseDeveloperExceptionPage();
app.MapFallbackToFile("index.html");

app.Run();
