namespace FSharpWebApp.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.Identity
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Authentication
open FSharpWebApp.DataAccessLayer
open FSharpWebApp.Models
open System.Collections.Generic
open FSharpWebApp.ViewModels
open Microsoft.Extensions.Logging
open System

module AsyncModule =
    let await a =  (a |> Async.AwaitTask)

type AccountController (context: AppDataContext, userManager, signinManager, logger) =
    inherit Controller()
    let mutable _context = context;
    let SigninManager:SignInManager<ApplicationUser> = signinManager
    let UserManager:UserManager<ApplicationUser> = userManager
    let Logger:ILogger<AccountController> = logger

    member this.AddErrors (result:IdentityResult) = 
        result
            .Errors
            .ToList()
            .ForEach(            
                fun e -> this.ModelState.AddModelError("", e.Description)) |> ignore


    member this.RedirectToLocal(returnUrl:string) = 
        if this.Url.IsLocalUrl(returnUrl) then
            this.Redirect(returnUrl) :> IActionResult
        else
            this.RedirectToAction("Index", "Home") :> IActionResult
            
    [<TempData>]
    member val ErrorMessage:string = "" with get,set
    
    [<AllowAnonymous>]
    [<HttpGet>]
    member this.Login(?returnUrl:string) = 
        async {
            this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme) |> Async.AwaitTask |> ignore
            let returnUrlValue = 
                match returnUrl with
                | Some s -> s
                | None -> ""
            this.ViewData.["ReturnUrl"] <- returnUrlValue
            this.View() |> ignore
        }

    [<HttpPost>]
    [<AllowAnonymous>]
    member this.Login(model:LoginViewModel, ?returnUrl) = 
        async {
            if this.ModelState.IsValid then
                let returnUrlValue = 
                    match returnUrl with
                        | Some s -> s 
                        | None -> ""
                this.ViewData.["ReturnUrl"] <- returnUrlValue
                let result = SigninManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false)
                if result.Result.Succeeded then
                    return RedirectToActionResult("Index", "Home", null, false) :> IActionResult
                else        
                    return this.View() :> IActionResult
            else                
                return this.View(model) :> IActionResult                
        }   
        
    [<HttpGet>]
    [<AllowAnonymous>]
    member this.Register(returnUrl:string) = 
        this.ViewData.["ReturnUrl"] <- returnUrl
        this.View()

    [<HttpPost>]
    [<AllowAnonymous>]
    [<ValidateAntiForgeryToken>]
    member this.Register(model:RegisterViewModel, ?returnUrl:string) = 
        async {
            let returnUrlValue = 
                match returnUrl with 
                | Some s -> s
                | None -> ""

            this.ViewData.["ReturnUrl"] <- returnUrlValue
            if this.ModelState.IsValid then 
                let user = ApplicationUser(UserName = model.Email, Email = model.Email)
                let! result = UserManager.CreateAsync(user, model.Password) |> Async.AwaitTask
                if result.Succeeded then                            
                    let code = UserManager.GenerateEmailConfirmationTokenAsync(user) |> Async.AwaitTask
                    (SigninManager.SignInAsync(user, isPersistent= false) |> Async.AwaitTask) |> ignore
                    return (this.Redirect(returnUrlValue) :> IActionResult)
                else
                    return this.View(model) :> IActionResult    
            else                             
                return this.View(model) :> IActionResult
        }