using SKladisteAppl.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// prilagodba za dokumentaciju, �itati https://medium.com/geekculture/customizing-swagger-in-asp-net-core-5-2c98d03cbe52
builder.Services.AddSwaggerGen(sgo =>
{ // sgo je instanca klase SwaggerGenOptions
  // �itati https://devintxcontent.blob.core.windows.net/showcontent/Speaker%20Presentations%20Fall%202017/Web%20API%20Best%20Practices.pdf
    var o = new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Skladiste API",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "roman.zaric@gmail.com",
            Name = "Roman Zaric"
        },
        Description = "Ovo je dokumentacija za Skladiste API",
        License = new Microsoft.OpenApi.Models.OpenApiLicense()
        {
            Name = "Skladisno poslovanje"
        }
    };
    sgo.SwaggerDoc("v1", o);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    sgo.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

});

// Svi se od svuda na sve mogu�e na�ine mogu spojitina na� API
// �itati https://code-maze.com/aspnetcore-webapi-best-practices/
builder.Services.AddCors(opcije =>
{
    opcije.AddPolicy("CorsPolicy",
        builder =>
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );

});


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
    // mogu�nost generiranja poziva rute u CMD i Powershell
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
