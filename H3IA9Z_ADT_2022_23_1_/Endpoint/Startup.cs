using Data;
using H3IA9Z_ADT_2022_23_1_Endpoint.Services;
using H3IA9Z_ADT_2022_23_1_Logic;
using H3IA9Z_ADT_2022_23_1_Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IMovieLogic, MovieLogic>();
            services.AddTransient<IVisitorLogic, VisitorLogic>();
            services.AddTransient<IReservationLogic, ReservationLogic>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IVisitorRepository, VisitorRepository>();
            services.AddTransient<IReservationsRepository, ReservationRepository>();
            services.AddTransient<ChooseYourMovieDbContext, ChooseYourMovieDbContext>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:23070"));

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}