using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Storage.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy("Youxel-Web",
                    builder => builder.WithOrigins(Configuration.GetValue<string>("JwtIssuerOptions:Audience"))
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .WithExposedHeaders("Token-Expired"));
            });


            if (!_environment.IsProduction())
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Youxel API",
                        Version = "v1",
                        Contact = new OpenApiContact { Name = "Youxel", Email = "aaaa@youxel.com" }
                    });

                    // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
                    options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                    // add Security information to each operation for OAuth2
                    options.OperationFilter<SecurityRequirementsOperationFilter>();

                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                        In = ParameterLocation.Header,
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "jwt_auth",
                            Type = ReferenceType.SecurityScheme
                        }
                    };
                    OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                        {
                    {securityScheme, new string[] { }},
                        };
                    options.AddSecurityRequirement(securityRequirements);

                });

            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Youxel-Web");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Youxel Project API v1");
                });
            }
        }
    }
}