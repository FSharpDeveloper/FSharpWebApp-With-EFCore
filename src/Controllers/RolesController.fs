namespace FSharpWebApp.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open FSharpWebApp.DataAccessLayer
open FSharpWebApp.Models
open System.Collections.Generic

type CreatedResult = { id:obj }

type RolesController (context: AppDataContext) =
    inherit Controller()
    let mutable _context = context;
    let repo = (new Repository<ApplicationRole, string>(_context) :> IRepository<ApplicationRole, string>)

    member this.Index () =
        this.View()

    [<HttpGet>]
    [<Produces("application/json")>]
    member this.Get () =
        repo.GetAll().ToList()
        |> this.Json
    [<HttpPost>]
    member this.Post (entity) =
        let result = repo.AddEntity(entity)
        this.Created(uri= "/roles/",value= { id=result.Id })

    member this.Contact () =
        this.ViewData.["Message"] <- "Your contact page."
        this.View()

    member this.Error () =
        this.View();
