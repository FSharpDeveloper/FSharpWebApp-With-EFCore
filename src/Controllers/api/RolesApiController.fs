namespace FSharpWebApp.ApiControllers

    open System
    open System.Collections.Generic
    open System.Linq
    open System.Threading.Tasks
    open Microsoft.AspNetCore.Mvc
    open Microsoft.EntityFrameworkCore
    open FSharpWebApp.DataAccessLayer
    open FSharpWebApp.Models
    open System.Collections.Generic
    open Microsoft.AspNetCore.Identity
    open Microsoft.AspNetCore.Identity
    open Microsoft.AspNetCore.Identity.EntityFrameworkCore
    open FSharpWebApp.DataAccessLayer
    open Microsoft.AspNetCore.Identity

    type roleReturn = {RoleName:string;Users:List<ApplicationUser>}

    [<Route("api/roles")>]
    type RolesApiController (context: AppDataContext, userManager: UserManager<ApplicationUser>, roleManager: RoleManager<ApplicationRole>) as this =
        inherit ApiController<ApplicationRole, string>(context)        
        let mutable _context = context;
        let repo = (new Repository<ApplicationRole, string>(_context) :> IRepository<ApplicationRole, string>)
        let RoleManager = roleManager
        let UserManager = userManager
        [<HttpGet>]
        [<Produces("application/json")>]
        override this.Get () =
            let result = 
                RoleManager
                    .Roles.ToList()

            result
            |> this.Json :> IActionResult
     