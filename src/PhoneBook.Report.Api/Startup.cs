using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PhoneBook.Report.Api.DataContext;
using PhoneBook.Report.Api.Services.Interfaces;
using PhoneBook.Report.Api.Services.Implementations;
using PhoneBook.Report.Api.Repositories.Interfaces;
using PhoneBook.Report.Api.Repositories.Implementations;
using PhoneBook.Report.Api.Mappings;
using System.Text.Json.Serialization;

namespace PhoneBook.Report.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhoneBook.Report.Api", Version = "v1" });
           });

            services.AddDbContext<PhoneBookReportDataContext>(options =>
         options.UseNpgsql(Configuration.GetConnectionString("PhoneBookReportConnection"), x => x.MigrationsAssembly("PhoneBook.Report.Api.DataContext")));

            services.AddAutoMapper(typeof(MappingsProfile));

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddSingleton<IMessagePublisher, KafkaMessagePublisher>();
            services.AddSingleton<IMessageReceiver, KafkaMessageReceiver>();
            
            services.AddHostedService<MessageConsumer>();
                 

            services.AddControllers()
              .AddJsonOptions(options =>
              {
                  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                  options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "PhoneBook.Report.Api v1"));
            }

            app.UseDefaultFiles();

            app.UseStaticFiles();

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
