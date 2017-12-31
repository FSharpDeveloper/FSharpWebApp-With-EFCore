namespace FSharpWebApp.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.Identity
open Microsoft.AspNetCore.Authorization
open FSharpWebApp.DataAccessLayer
open FSharpWebApp.Models
open System.Collections.Generic
open FSharpWebApp.ViewModels

type AccountController (context: AppDataContext, userManager:UserManager<ApplicationUser>, signinManager:SignInManager<ApplicationUser>) =
    inherit Controller()
    let mutable _context = context;
    let SigninManager = signinManager
    let UserManager = userManager
    [<HttpGet>]
    member this.Login() = 
        this.View()
    [<HttpPost>]
    member this.Login(model:LoginViewModel) = 
        async {
            let result = SigninManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false)
            if result.Result.Succeeded then
                return RedirectToActionResult("Index", "Home", null, false) :> IActionResult
            else        
                return this.View() :> IActionResult
        }
        
    [<HttpGet>]
    [<AllowAnonymous>]
    member this.Register(returnUrl:string) = 
        this.ViewData.["ReturnUrl"] <- returnUrl
        this.View()

    [<HttpPost>]
    [<AllowAnonymous>]
    [<ValidateAntiForgeryToken>]
    member this.Register(model:RegisterViewModel, returnUrl:string) = 
        async {
            this.ViewData.["ReturnUrl"] <- returnUrl
            if this.ModelState.IsValid then 
                let user = ApplicationUser(UserName = model.Email, Email = model.Email)
                let! result = UserManager.CreateAsync(user, model.Password) |> Async.AwaitTask
                if result.Succeeded then                            
                    let code = UserManager.GenerateEmailConfirmationTokenAsync(user) 
                    SigninManager.SignInAsync(user, isPersistent= false)
            this.View(model)
        }