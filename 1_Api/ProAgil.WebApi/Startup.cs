using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ProAgil.Domain.Identity;
using ProAgil.Domain.ProAgilContext.Handlers;
using ProAgil.Domain.ProAgilContext.Repositories.Interfaces;
using ProAgil.Infra.Context;
using ProAgil.Infra.Repository;
using ProAgil.WebApi.InfraEstructure;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
                options.SwaggerDoc("v1", new Info { Title = "Documentação", Version = "v1" });

            });
            services.AddSwaggerDocumentation();

            IdentityBuilder builder = services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<ProAgilContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();


            //Configurando o JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               }
               );

            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
               .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper();
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

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My First Swagger");
            });

            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseStaticFiles();//Para poder trabalhar com imagens, dentro do diretorio wwwroot
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = "/Resources"
            });
            app.UseMvc();
        }
    }
}