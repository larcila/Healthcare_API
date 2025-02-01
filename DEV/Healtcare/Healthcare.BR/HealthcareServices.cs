using Healthcare.BR.BL;
using Healthcare.Common.Action;
using Healthcare.Common.ActionBL;
using HealthCare.Data.Context;
using HealthCare.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Healthcare.BR
{
    public static class HealthcareServices
    {
        /// <summary>
        /// Add reference to service
        /// </summary>
        /// <example>services.AddScoped<IMyService, MyService>();</example>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddHealthcareServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<HealthcareDbcontext>(options =>
                options.UseSqlServer(connectionString));

            // Add more services
            // services.AddScoped<IMyService, MyService>();
            services.AddScoped<IPatientRepository, PatientDAL>();
            services.AddScoped<IFollowUpRepository, Follow_UpDAL>();
            services.AddScoped<IUserRepository, UserDAL>();
            services.AddScoped<IRole_By_UserRepository, Role_By_UserDAL>();
            services.AddScoped<IRoleRepository, RoleDAL>();

            return services;
        }
    }
}
