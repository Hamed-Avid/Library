using Library.Persistence.EF;
using Library.Persistence.EF.Books;
using Library.Persistence.EF.Categories;
using Library.Persistence.EF.People;
using Library.Persistence.EF.Trusteeship;
using Library.Services;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.Services.Categories;
using Library.Services.Categories.Contracts;
using Library.Services.People;
using Library.Services.People.Contracts;
using Library.Services.Trusteeship;
using Library.Services.Trusteeship.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.RestApi
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
            services.AddControllers().AddNewtonsoftJson(_ => _.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen();
            services.AddDbContext<EFDataContext>();
            services.AddScoped<UnitOfwork, EFUnitOfWork>();
            services.AddScoped<BookRepository, EFBookRepository>();
            services.AddScoped<BookService, BookAppService>();
            services.AddScoped<CategoryRepository, EFCategoryRepository>();
            services.AddScoped<CategoryService, CategoryAppService>();
            services.AddScoped<PersonRepository, EFPersonRepository>();
            services.AddScoped<PersonService, PersonAppService>();
            services.AddScoped<TrusteeRepository, EFTrusteeRepository>();
            services.AddScoped<TrusteeService, TrusteeAppService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HttpReplApi v1");
            });
        }
    }
}
