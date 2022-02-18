using System.Reflection;
using AutoMapper;
using Cryptocop.Software.API.Extensions;
using Cryptocop.Software.API.Helpers;
using Cryptocop.Software.API.Mappings;
using Cryptocop.Software.API.Middlewares;
using Cryptocop.Software.API.Repositories.Contexts;
using Cryptocop.Software.API.Repositories.Implementations;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Services.Implementations;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Services.Publisher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Newtonsoft.Json;

namespace Cryptocop.Software.API
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContext<CryptocopDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("CryptocopDatabase"), options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtTokenAuthentication(Configuration);
            
            // Service
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICryptoCurrencyService, CryptoCurrencyService>();
            services.AddTransient<IExchangeService, ExchangeService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IQueueService, QueueService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();

            // Repository
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ClaimsAuthorizationHelper>();
            
            var jwtConfig = Configuration.GetSection("JwtConfig");
            services.AddTransient<ITokenService>((c) => new TokenService(
                jwtConfig.GetValue<string>("secret"),
                jwtConfig.GetValue<string>("expirationInMinutes"),
                jwtConfig.GetValue<string>("issuer"),
                jwtConfig.GetValue<string>("audience")
            ));
            
            var mapperConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });
            
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            services.AddTransient<IRabbitMqClient, RabbitMqClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGlobalExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
