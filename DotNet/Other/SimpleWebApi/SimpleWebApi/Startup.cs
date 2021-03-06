﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;

namespace SimpleWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Add OpenAPI and Swagger DI services and configure documents

            // Adds the NSwag services
            services
                // Register a Swagger 2.0 document generator
                .AddSwaggerDocument(document =>
                {
                    document.DocumentName = "swagger";
                    // Add custom document processors, etc.
                    /*document.DocumentProcessors.Add(new SecurityDefinitionAppender("TEST_HEADER", new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = "TEST_HEADER",
                        In = SwaggerSecurityApiKeyLocation.Header,
                        Description = "TEST_HEADER"
                    }));*/
                    // Post process the generated document
                    document.PostProcess = d => d.Info.Title = "UEI OAuth Service";
                })
                // Register an OpenAPI 3.0 document generator
                .AddOpenApiDocument(document => document.DocumentName = "openapi");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
            //// Add OpenAPI and Swagger middlewares to serve documents and web UIs

            // URLs: 
            // - http://localhost:65384/swagger/v1/swagger.json
            // - http://localhost:65384/swagger
            // - http://localhost:65384/redoc
            // - http://localhost:65384/openapi
            // - http://localhost:65384/openapi_redoc

            // Add Swagger 2.0 document serving middleware
            app.UseSwagger(options =>
            {
                options.DocumentName = "swagger";
                options.Path = "/swagger/v1/swagger.json";
            });

            // Add web UIs to interact with the document
            app.UseSwaggerUi3(options =>
            {
                // Define web UI route
                options.Path = "/swagger";

                // Define OpenAPI/Swagger document route (defined with UseSwaggerWithApiExplorer)
                options.DocumentPath = "/swagger/v1/swagger.json";
            });
            app.UseReDoc(options =>
            {
                options.Path = "/redoc";
                options.DocumentPath = "/swagger/v1/swagger.json";
            });

            //// Add OpenAPI 3.0 document serving middleware
            app.UseSwagger(options =>
            {
                options.DocumentName = "openapi";
                options.Path = "/openapi/v1/openapi.json";
            });

            // Add web UIs to interact with the document
            app.UseSwaggerUi3(options =>
            {
                options.Path = "/openapi";
                options.DocumentPath = "/openapi/v1/openapi.json";
            });
            app.UseReDoc(options =>
            {
                options.Path = "/openapi_redoc";
                options.DocumentPath = "/openapi/v1/openapi.json";
            });

            // Add Swagger UI with multiple documents
            app.UseSwaggerUi3(options =>
            {
                // Add multiple OpenAPI/Swagger documents to the Swagger UI 3 web frontend
                options.SwaggerRoutes.Add(new SwaggerUi3Route("Swagger", "/swagger/v1/swagger.json"));
                options.SwaggerRoutes.Add(new SwaggerUi3Route("Openapi", "/openapi/v1/openapi.json"));
                options.SwaggerRoutes.Add(new SwaggerUi3Route("Petstore", "http://petstore.swagger.io/v2/swagger.json"));

                // Define web UI route
                options.Path = "/swagger_all";
            });
        }
    }
}
