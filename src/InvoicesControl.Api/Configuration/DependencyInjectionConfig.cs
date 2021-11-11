using InvoicesControl.Api.Extensions;
using InvoicesControl.Api.Helper;
using InvoicesControl.Application.Interfaces;
using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.Services;
using InvoicesControl.Infra.Data.Contexts;
using InvoicesControl.Infra.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InvoicesControl.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static object IHttpContextAcessor { get; private set; }

        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<JwtHelper>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IRevenueService, RevenueService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IRevenueRepository, RevenueRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();

            return services;
        }
    }
}
