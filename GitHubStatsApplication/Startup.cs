using GitHubStatsApplication.Interfaces;
using GitHubStatsApplication.Request;
using GitHubStatsApplication.Services;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace GitHubStatsApplication
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
            services.AddControllers();
            services.AddSwaggerGen(client =>
            {
                client.SwaggerDoc("v1", new OpenApiInfo { Title = "GitHub Stats API", Version = "v1" });
                // Add Enum types for dropdowns
                client.MapType<OrderByOptions>(() => new OpenApiSchema { Type = "string", Enum = (IList<IOpenApiAny>)Enum.GetNames(typeof(OrderByOptions)).Select(name => new OpenApiString(name)) });
                // Apply enum description filter
                //client.OperationFilter<AddEnumDescriptionsOperationFilter>();

                // Optionally, you can also add descriptions for enum values
                // client.DescribeAllEnumsAsStrings();
            });
            services.AddHttpClient<IGitHubStatsService, GitHubStatsService>(client =>
            {
                client.BaseAddress = new Uri(GitHubStatsConstants.GitHubRepoUrl);
                client.DefaultRequestHeaders.Add("User-Agent", GitHubStatsConstants.UserAgent);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GitHub Stats API V1");
                });
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
