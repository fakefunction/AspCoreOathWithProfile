namespace CalculatorLib.AspCoreOathWithProfile.Web
{
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using CalculatorLib.AspCoreOathWithProfile.Web.Constants;
    using CalculatorLib.AspCoreOathWithProfile.Web.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IProfileService, ProfileService>();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = TemporaryAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            //http://codereform.com/blog/post/asp-net-core-2-1-authentication-with-social-logins/
            //https://www.membrane-soa.org/service-proxy-doc/4.4/oauth2-google.htm
            //.AddFacebook(options =>
            //{
            //    options.AppId = "";
            //    options.AppSecret = "";
            //})
            //.AddTwitter(options =>
            //{
            //    options.ConsumerKey = "";
            //    options.ConsumerSecret = "";
            //})
            //.AddGitHub(options =>
            //{
            //    options.ClientId = "";
            //    options.ClientSecret = "";
            //})
            .AddGoogle(options =>
            {
                options.ClientId = "";
                options.ClientSecret = "";
            })
            .AddCookie(options => options.LoginPath = "/auth/signin")
            .AddCookie(TemporaryAuthenticationDefaults.AuthenticationScheme);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var options = new RewriteOptions();//.AddRedirectToHttps(StatusCodes.Status301MovedPermanently, 44329);
            app.UseRewriter(options);
            app.UseStaticFiles();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
