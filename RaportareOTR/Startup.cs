using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RaportareOTR.Data;
using Microsoft.EntityFrameworkCore;
using RaportareOTR.Models;
using Microsoft.AspNetCore.Identity;
using RaportareOTR.Services;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RaportareOTR.CommonCode.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SIGAD.CommonCode.Authentication;
using RaportareOTR.CommonCode;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.Extensions.Logging;

namespace RaportareOTR
{
    public class Startup
    {

        private static string databaseConnection = "";
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            //services.AddAuthorization(options =>
            //{
            //    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser()
            //        .Build();
            //}
            //);
            //services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //});

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddNodeServices();

            services.AddSingleton(Configuration);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
            
            CreateRoles(serviceProvider);

        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Task<IdentityResult> roleResult;

            string[] roleNames = { RoleNames.OTR, RoleNames.Client };

            //Check that there is an Administrator role and create if not

            foreach (var roleName in roleNames)
            {
                Task<bool> hasAdminRole = roleManager.RoleExistsAsync(roleName);
                hasAdminRole.Wait();
                if (!hasAdminRole.Result)
                {
                    roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                    roleResult.Wait();
                }
            }

        }

        public static void AddErrorToDatabase(Exception e)
        {
            // Get stack trace for the exception with source file information
            var st = new StackTrace(e, true);
            // Get the stack frames

            string file = "";
            string fileTemp = "";
            string method = "";
            string lineNumber = "";

            foreach (StackFrame frame in st.GetFrames())
            {
                // Get the file name from the stack frame
                fileTemp = frame.GetFileName() ?? "";
                fileTemp = fileTemp.Replace('\\', '-').Split('-').Last().Trim();

                int line = frame.GetFileLineNumber();

                if (line > 0)
                {
                    file += "-> " + fileTemp + "\n";

                    // Get the method from the stack frame
                    method = "-> " + frame.GetMethod().ToString().Substring(frame.GetMethod().ToString().IndexOf(' '), frame.GetMethod().ToString().IndexOf('(') - frame.GetMethod().ToString().IndexOf(' ')) + "\n";

                    // Get the line number from the stack frame
                    lineNumber += "-> " + frame.GetFileLineNumber().ToString() + "\n";
                }
            }

            string destails = e.Message;

            if (e.InnerException != null)
            {
                var innerException = e;

                Exception realerror = e;
                while (realerror.InnerException != null)
                {
                    realerror = realerror.InnerException;
                    destails += "\n" + realerror.Message;
                }
            }

            if (databaseConnection != null)
            {
                string QueryString = "INSERT INTO Error(Description,Moment,[File],Method,Line) VALUES (@description,@moment,@file,@method,@line)";

                System.Type tipProdus = e.GetType();

                SqlConnection myConnection = new SqlConnection(databaseConnection);
                SqlDataAdapter myCommand = new SqlDataAdapter(QueryString, myConnection);
                DataSet ds = new DataSet();

                // add the elements to the list
                using (SqlConnection con = new SqlConnection(databaseConnection))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(QueryString, con))
                    {
                        cmd.Parameters.AddWithValue("@description", destails);
                        cmd.Parameters.AddWithValue("@moment", DateTime.Now);
                        cmd.Parameters.AddWithValue("@file", file);
                        cmd.Parameters.AddWithValue("@method", method);
                        cmd.Parameters.AddWithValue("@line", lineNumber);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }

            }
            else
            {
                //string folderPath = System.IO.Directory.GetCurrentDirectory();
                string path = "errors" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".txt";
                if (!System.IO.File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = System.IO.File.CreateText(path))
                    {
                        sw.WriteLine(destails);
                        sw.WriteLine(DateTime.Now);
                        sw.WriteLine(file);
                        sw.WriteLine(method);
                        sw.WriteLine(lineNumber);
                    }
                }
            }
        }

    }
}
