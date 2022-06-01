using MCProject.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace MCProject;

public class Startup
{
    public const string CookieName = "TestCookie";

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(CookieName).AddCookie(CookieName, options =>
        {
            options.Cookie.Name = CookieName;
            options.LoginPath = "/account/login";
            options.AccessDeniedPath = "/account/accessdenied";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
        });
        services.AddAuthorization(options =>
        {
            //options.AddPolicy("Admin", policy =>
            //{
            //    policy.RequireClaim("IsAdmin");
            //});
        });
        services.AddDbContext<MainContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("MCProject"));
        });

        services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }
}
