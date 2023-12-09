using DataLayer.Repositories;
using DataLayer;
using Core.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Authentication;

namespace FreeCoursesPlatform.Settings
{
    public static class Dependencies
    {

        public static void Inject(WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddControllers();
            applicationBuilder.Services.AddSwaggerGen();

            applicationBuilder.Services.AddDbContext<AppDbContext>();

            AddRepositories(applicationBuilder.Services);
            AddServices(applicationBuilder.Services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<AuthorizationService>();
            services.AddScoped<CourseService>();
            services.AddScoped<CategoryService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<CategoriesRepository>();
            services.AddScoped<CoursesRepository>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<UnitOfWork>();
        }
    }
}
