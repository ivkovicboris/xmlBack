using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RentACar.BLL.Contracts;
using RentACar.BLL.Services;
using RentACar.DAL.Context;
using RentACar.DAL.Entites;
using RentACar.DAL.Repositories;
using RentACar.DAL.Repositories.Abstract;

namespace RentACar
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<RentContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };
                });
            services.AddDbContext<RentContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("ConnectionString"));
            });

            services.AddAutoMapper();

            services.AddTransient<RentSeeder>();

            services.AddScoped<IUserContract, UserService>();
            services.AddScoped<ICarContract, CarService>();
            services.AddScoped<IAdContract, AdService>();

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Client>, Repository<Client>>();
            services.AddScoped<IRepository<Agent>, Repository<Agent>>();
            services.AddScoped<IRepository<Car>, Repository<Car>>();
            services.AddScoped<IRepository<CarBrand>, Repository<CarBrand>>();
            services.AddScoped<IRepository<ModelOfCar>, Repository<ModelOfCar>>();
            services.AddScoped<IRepository<FuelType>, Repository<FuelType>>();
            services.AddScoped<IRepository<Ad>, Repository<Ad>>();
            services.AddScoped<IRepository<AdRequest>, Repository<AdRequest>>();
            services.AddScoped<IRepository<AdAdRequest>, Repository<AdAdRequest>>();

            services.AddCors();
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default", "{controller}/{action}/{id}",
                    new { controller = "Account", Action = "Register" });
            });
        }
    }
}
