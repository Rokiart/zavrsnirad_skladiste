using System.Reflection;
using SKladisteAppl.Models;

namespace SKladisteAppl.Extensions
{
    public static class SkladisteExtensions
    {
        public static void AddSkladisteSwaggerGen(this IServiceCollection Services)
        {
            // prilagodba za dokumentaciju, čitati https://medium.com/geekculture/customizing-swagger-in-asp-net-core-5-2c98d03cbe52
            Services.AddSwaggerGen(sgo =>
            { // sgo je instanca klase SwaggerGenOptions
              // čitati https://devintxcontent.blob.core.windows.net/showcontent/Speaker%20Presentations%20Fall%202017/Web%20API%20Best%20Practices.pdf
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

        }


        public static void AddSkladisteCORS(this IServiceCollection Services)
        {
            // Svi se od svuda na sve moguće načine mogu spojitina naš API
            // Čitati https://code-maze.com/aspnetcore-webapi-best-practices/

            Services.AddCors(opcije =>
            {
                opcije.AddPolicy("CorsPolicy",
                    builder =>
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );

            });
        }
    }
}
