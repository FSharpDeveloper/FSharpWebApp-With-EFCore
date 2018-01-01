namespace FSharpWebApp.ApiControllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open FSharpWebApp.DataAccessLayer
open FSharpWebApp.Models
open System.Collections.Generic
open Microsoft.AspNetCore.Identity
open Microsoft.AspNetCore.Identity


[<Route("api/users")>]
type UsersApiController (context: AppDataContext, userManager: UserManager<ApplicationUser>, roleManager:RoleManager<ApplicationRole>, signinManager:SignInManager<ApplicationUser>) =
    inherit Controller()
    //inherit ApiController<ApplicationUser, string>(context)
    // ApplicationUser * string -> IActionResult
    let mutable _context = context;
    let repo = (new Repository<ApplicationUser, string>(_context) :> IRepository<ApplicationUser, string>)
    let RoleManager = roleManager
    let UserManager = userManager
    let SigninManager = signinManager
    [<HttpGet>]
    member this.Get () =
        this.Json(UserManager.Users.ToList())

    [<HttpPost>]
    member this.Post (user:ApplicationUser, password) = 
        async {
            user.Id <- Guid.NewGuid().ToString()
            let! result = UserManager.CreateAsync(user, password) |> Async.AwaitTask
            if result.Succeeded then 
                SigninManager.SignInAsync(user, false, null) |> Async.AwaitTask |> ignore 
                return (this.Ok(user) :> IActionResult)          
            else 
                return (this.View(user) :> IActionResult)   
        }