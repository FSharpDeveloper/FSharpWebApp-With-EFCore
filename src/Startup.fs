namespace FSharpWebApp

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.EntityFrameworkCore
open FSharpWebApp.DataAccessLayer
open Microsoft.AspNetCore.Identity
open Microsoft.AspNetCore.Identity.EntityFrameworkCore
open FSharpWebApp.Models
open Microsoft.AspNetCore.Http

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        let connectionString = this.Configuration.GetConnectionString("DefaultConnection")
        services
            .AddDbContext<AppDataContext>
                (fun(options) -> options.UseSqlServer(connectionString) |> ignore) |> ignore

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<AppDataContext>()
            .AddDefaultTokenProviders() |> ignore           
        // services
        //     .AddAuthentication()
        //     .AddCookie(fun options -> 
        //         options.LoginPath <- PathString("/Account/Unauthorized/")
        //         options.AccessDeniedPath <- PathString("/Account/Forbidden/")
        //     ) |> ignore
        services.AddMvc() |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =

        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
            app.UseBrowserLink() |> ignore
            app.UseDatabaseErrorPage() |> ignore         
        else
            app.UseExceptionHandler("/Home/Error") |> ignore

        app.UseStaticFiles() |> ignore

        app.UseAuthentication() |> ignore

        app.UseMvc(fun routes ->
            routes.MapRoute(
                name = "default",
                template = "{controller=Home}/{action=Index}/{id?}") |> ignore
            ) |> ignore

    member val Configuration : IConfiguration = null with get, set
