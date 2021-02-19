using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMedia.Core.Data;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infraestructure.Filters;
using SocialMedia.Infraestructure.Repositories;
using System;

namespace SocialMediaAPI
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
            //este paquete busca en la solucion los profiles(Mappings) que se hicieron
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Se muestra en Postman el json que se envia en el post, ya nos e ve el mensaje de error
            services.AddControllers(options => 
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(options =>
             {
                 //options.SuppressModelStateInvalidFilter = true;
             });

            services.AddDbContext<SocialMediaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SocialMedia"))
            );


            services.AddTransient<IPostService, PostService>();
            // Se indica que la interface ha sido implementada en la clase postrepository
            // Con esta practica se hace más mantenible la aplicacion ya que puedes cambair de repositorio sin tener que cambiar el codigo
            // eliminar services.AddTransient<IPostRepository, PostRepository>();
            //elimnar  services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //Es un filtro ... valida los modelos de manera global en la app... para cada request
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });


        }

        private IServiceCollection AddScoped(Type type1, Type type2)
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
