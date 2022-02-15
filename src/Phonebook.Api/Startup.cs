using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PhoneBook.Api.DataContext;
using PhoneBook.Api.Repositories.Implementations;
using PhoneBook.Api.Repositories.Interfaces;
using PhoneBook.Api.Services.Implementations;
using PhoneBook.Api.Services.Interfaces;
using PhoneBook.Api.Services.Mappings;
using PhoneBook.Api.Utilities;

namespace PhoneBook.Api
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

            services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhoneBook.Api", Version = "v1" });
            });

            services.AddAutoMapper(typeof(MappingsProfile));
            services.AddDbContext<PhoneBookDataContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("PhoneBookConnection"), x => x.MigrationsAssembly("PhoneBook.Api.DataContext")));

            services.AddScoped<IPhoneBookService, PhoneBookService>();
            services.AddScoped<IPhoneBookRepository, PhoneBookRepository>();

            services.AddSingleton<IExceptionHelper, ExceptionHelper>();

            services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                   options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ILogger<JsonExceptionMiddleware> logger,
            IExceptionHelper exceptionHelper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "PhoneBook.Api v1"));

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new JsonExceptionMiddleware(logger, exceptionHelper).Invoke
            });


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
