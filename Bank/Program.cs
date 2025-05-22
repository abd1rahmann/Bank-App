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

            // Läs rätt konfig beroende på miljö
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Hämta rätt connection string
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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
                try
                {
                    var initializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
                    initializer.SeedData();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"?? FEL vid seedning: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                    throw; 
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
