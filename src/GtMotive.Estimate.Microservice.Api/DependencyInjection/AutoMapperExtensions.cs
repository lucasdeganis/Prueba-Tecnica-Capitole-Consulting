using GtMotive.Estimate.Microservice.ApplicationCore.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Api.DependencyInjection
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperProfile(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }
    }
}
