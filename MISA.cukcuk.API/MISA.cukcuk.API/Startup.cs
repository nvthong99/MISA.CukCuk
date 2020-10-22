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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MISA.cukcuk.API.DatabaseAccess;

using MISA.cukcuk.API.model;
using MISA.cukcuk.Bussiness.Bussiness;
using MISA.cukcuk.Bussiness.Interfaces;
using MISA.cukcuk.DBAccess.interfaces;
using MISA.cukcuk.DBAccess.responsitory;
using Newtonsoft.Json.Serialization;

namespace MISA.cukcuk.API
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
            services.AddControllers()
            .AddNewtonsoftJson(Options =>
            {
                Options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                Options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }
            );
            services.AddScoped<IEmployeeResponsitory, EmployeeResponsitory>();
            services.AddScoped<IDepartmentResponsitory, DepartmentResponsitory>();
            services.AddScoped<IPossitionResponsitory, PossitionResponsitory>();
            services.AddScoped<IEmployeeBussiness, EmployeeBussiness>();
            services.AddScoped<IDepartmentBussiness, DepartmentBussiness>();
            services.AddScoped<IPossitionBussiness, PossitionBussiness>();
            services.AddScoped(typeof(IBaseBussiness<>), typeof(BaseBussiness<>));
            services.AddScoped( typeof(IDataBaseAccess<>),typeof(MariaDBAccess<>) );
            services.AddScoped(typeof(IBaseResponsitory<>), typeof(BaseResponsitory<>));
            
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
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
