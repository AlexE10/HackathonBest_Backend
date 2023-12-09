//using DataLayer.Repositories;
//using DataLayer;
//using Core.Services;
//using Microsoft.AspNetCore.Cors.Infrastructure;

//namespace CarRentalBusiness.Settings
//{
//    public static class Dependencies
//    {

//        public static void Inject(WebApplicationBuilder applicationBuilder)
//        {
//            applicationBuilder.Services.AddControllers();
//            applicationBuilder.Services.AddSwaggerGen();

//            applicationBuilder.Services.AddDbContext<AppDbContext>();

//            AddRepositories(applicationBuilder.Services);
//            AddServices(applicationBuilder.Services);
//        }

//        private static void AddServices(IServiceCollection services)
//        {
//            services.AddScoped<CarService>();
//            services.AddScoped<CategoryService>();
//            services.AddScoped<RentingContractService>();
//            services.AddScoped<MechanicReportService>();
//        }

//        private static void AddRepositories(IServiceCollection services)
//        {
//            services.AddScoped<CarsRepository>();
//            services.AddScoped<CategoriesRepository>();
//            services.AddScoped<RentingContractsRepository>();
//            services.AddScoped<MechanicReportsRepository>();
//            services.AddScoped<UnitOfWork>();
//        }

//    }
//}
