using WritersPlatform.DataAccess.Contexts;
using WritersPlatform.DataAccess.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WritersPlatform.DataAccess.Repositories;
using WritersPlatform.Services;

namespace WritersPlatform;
public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup).Assembly);

        var usersConnectionString = configuration.GetConnectionString("UsersDbConnection");
        var compositionsConnectionString = configuration.GetConnectionString("CompositionsDbConnection");
        services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(usersConnectionString));
        services.AddDbContext<CompositionDbContext>(options => options.UseNpgsql(compositionsConnectionString));

        services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
            });

        services.AddIdentity<AppUser, IdentityRole>(options => {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
            .AddEntityFrameworkStores<UsersDbContext>()
            .AddDefaultTokenProviders();
        services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = configuration.GetValue<string>("GoogleClientId")!;
                options.ClientSecret = configuration.GetValue<string>("GoogleClientSecret")!;
                options.SignInScheme = IdentityConstants.ExternalScheme;
                options.CorrelationCookie.SameSite = SameSiteMode.Strict;
            });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.LoginPath = new PathString(CookieAuthenticationDefaults.LoginPath);
            options.LogoutPath = new PathString(CookieAuthenticationDefaults.LogoutPath);
            options.AccessDeniedPath = new PathString(CookieAuthenticationDefaults.AccessDeniedPath);
        });
        services
            .AddScoped<IGenreRepository, GenreRepository>()
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<ICompositionRepository, CompositionRepository>()
            .AddScoped<ICommentRepository, CommentRepository>();
        services
            .AddScoped<ICompositionService, CompositionService>()
            .AddScoped<IAuthorService, AuthorService>()
            .AddScoped<IGenreService, GenreService>()
            .AddScoped<ICommentService, CommentService>();

        services.AddRazorPages()
            .AddRazorRuntimeCompilation();

        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddMvc();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(); 
        }

        app.UseHttpsRedirection();

        app.UseStatusCodePages();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }

    private string[] GetAllowedOrigins(IConfiguration configuration) =>
    configuration.GetSection("AllowedOrigins").Get<string[]>() ?? new string[0];
}
