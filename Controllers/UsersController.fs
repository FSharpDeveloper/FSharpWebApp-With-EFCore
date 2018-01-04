namespace FSharpWebApp.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open FSharpWebApp.DataAccessLayer
open FSharpWebApp.Models
open System.Collections.Generic
open Microsoft.AspNetCore.Authorization

[<AuthorizeAttribute()>]
type UsersController (context: AppDataContext) =
    inherit Controller()
    let mutable _context = context;
    let repo =(new Repository<ApplicationUser, string>(_context) :> IRepository<ApplicationUser, string>)
    member this.Index () =
        this.View()

    member this.About () =
        this.ViewData.["Message"] <- "Your application description page."
        this.View()

    member this.Contact () =
        this.ViewData.["Message"] <- "Your contact page."
        this.View()

    member this.Error () =
        this.View();
