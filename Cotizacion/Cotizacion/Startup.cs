using Cotizacion.BL;
using Cotizacion.DAL.Command;
using Cotizacion.ExternalServices;
using Cotizacion.Models;
using Cotizacion.MongoServices;
using COTIZACION.Core.BL.Cotizacion;
using COTIZACION.Core.BLDep.Cotizacion;
using COTIZACION.Core.DAL.Cotizacion;
using COTIZACION.Core.DALDep.Cotizacion;
using IGP.LayerData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cotizacion
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddHttpClient("Dolar", c =>
            //{
            //    c.BaseAddress = new Uri("http://www.bancoprovincia.com.ar/Principal/Dolar");
            //});
            services.AddHttpClient<IBancoProvinciaService, BancoProvinciaService>();
            services.Configure<MongoConnectionStrings>(Configuration.GetSection("MongoConnectionStrings"));
            services.Configure<SQLConnectionStrings>(Configuration.GetSection("SQLConnectionStrings"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IMongoConnectionFactory, MongoConnectionFactory>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IUsuarioDAL, UsuarioDAL>();
            services.AddScoped<IUsuarioBL, UsuarioBL>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddTransient(typeof(CnMsSQL<>));
            services.AddTransient<ICotizacionFac, CotizacionFac>();
            services.AddTransient<IGetCotizacion, GetDolar>();
            services.AddTransient<IGetCotizacion, GetPeso>();
            services.AddTransient<IGetCotizacion, GetReal>();
            services.AddTransient<ITransactionCustom, TransactionCustom>();
            services.AddTransient<IDbConnectionFactory, SqlConnectionFactory>();
            services.AddTransient<ISaveUsuario, SaveUsuario>();

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
