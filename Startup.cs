﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using thingservice.Data;
using thingservice.Service;
using thingservice.Service.Interface;

namespace thingservice {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddCors (o => o.AddPolicy ("CorsPolicy", builder => {
                builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ();
            }));

            services.AddSingleton<IConfiguration> (Configuration);

            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseNpgsql (Configuration.GetConnectionString ("ThingsDb")));
            services.AddTransient<IThingGroupService, ThingGroupService> ();
            services.AddTransient<ITagService, TagService> ();

            services.AddResponseCaching ();
            services.AddMvc ((options) => {
                    options.CacheProfiles.Add ("thingscache", new CacheProfile () {
                        Duration = Convert.ToInt32 (Configuration["CacheDuration"]),
                            Location = ResponseCacheLocation.Any
                    });
                })
                .AddJsonOptions (options => {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            app.UseCors ("CorsPolicy");

            app.UseResponseCaching ();
            app.UseForwardedHeaders (new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseMvc ();
        }
    }
}