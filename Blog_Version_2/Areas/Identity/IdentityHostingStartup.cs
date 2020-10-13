using System;
using Blog_Version_2.Areas.Identity.Data;
using Blog_Version_2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Blog_Version_2.Areas.Identity.IdentityHostingStartup))]
namespace Blog_Version_2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BlogContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BlogContextConnection")));

                services.AddDefaultIdentity<BlogUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<BlogContext>();
            });
        }
    }
}