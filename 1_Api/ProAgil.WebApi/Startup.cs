using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProAgil.Domain.ProAgilContext.Handlers;
using ProAgil.Domain.ProAgilContext.Repositories.Interfaces;
using ProAgil.Infra.Context;
using ProAgil.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

namespace ProAgil.WebApi
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
             //Ao injetar o DataContext dessa forma, ja possibilita injetar o contexto dentro das controllers
            services.AddDbContext<ProAgilContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            registrandoDependencias(services);
             //adicionando a documentação dos endpoints
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.DescribeAllParametersInCamelCase();
                options.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
        }
        public void registrandoDependencias(IServiceCollection services)
        {

            #region"Contexto"
            services.AddScoped<ProAgilContext, ProAgilContext>();
            #endregion

            #region"Handlers"
            services.AddScoped<EventoHandler, EventoHandler>();
            services.AddScoped<PalestranteHandler, PalestranteHandler>();
            #endregion

            #region"Repositórios"
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IPalestranteRepository, PalestranteRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My First Swagger");
            });


            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseStaticFiles();//Para poder trabalhar com imagens, dentro do diretorio wwwroot
            app.UseMvc();
        }
    }
}