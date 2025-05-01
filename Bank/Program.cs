using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Accounts;
using Services.Countries;
using Services.Customer;
using Services.Customers;
using Services.Gender;
using Services.Transactions;

namespace Bank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<BankAppDataContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BankAppDataContext>();

            builder.Services.AddTransient<DataInitializer>();
            builder.Services.AddScoped<ICustomersService, CustomersService>();
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
			builder.Services.AddScoped<ICountriesService, CountriesService>();
			builder.Services.AddScoped<IGenderService, GenderService>();
			builder.Services.AddScoped<ITransactionService, TransactionService>();


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRazorPages();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetService<DataInitializer>().SeedData();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
