using ApiLibros.Data;
using ApiLibros.LibrosMapper;
using ApiLibros.Repository;
using ApiLibros.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiLibros
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<ILibroRepository, LibroRepository>();
            services.AddScoped<IEditorialRepository, EditorialRepository>();

            services.AddAutoMapper(typeof(LibrosMappers));
            // Configuracion de la documentacion de la API
            services.AddSwaggerGen(options => 
            { 
                options.SwaggerDoc("ApiLibros", new Microsoft.OpenApi.Models.OpenApiInfo()
                { 
                    Title ="Api Libros",
                    Description = "Realizada como prueba de ingreso para NEXOS",
                    Version = "1.0",
                    Contact = new OpenApiContact
                    {
                        Name = "Divier Castañeda",
                        Email = "divierc@msn.com"
                    }
                });

                // Se parametriza el archivo XML para que aparezcan los comentarios de las funciones
                var archivoXmlComentarios = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var rutaApiComentarios = Path.Combine(AppContext.BaseDirectory, archivoXmlComentarios);
                options.IncludeXmlComments(rutaApiComentarios);

            });


            /*Damos soporte para CORS*/
            services.AddCors();

            services.AddControllers();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Linea para documentacion API
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/ApiLibros/swagger.json","API Libros");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            /*Damos soporte para CORS*/
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
