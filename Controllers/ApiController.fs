namespace FSharpWebApp.ApiControllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open FSharpWebApp.DataAccessLayer
open FSharpWebApp.Models
open System.Collections.Generic

type CreatedResult = { id:obj }

type ApiController<'E, 'K when 'E:not struct and 'E :> IEntity<'K> and 'K: equality> (context: AppDataContext) =
    inherit Controller()
    let mutable _context = context;
    let repo = (new Repository<'E, 'K>(_context) :> IRepository<'E, 'K>)
    abstract member Get:unit -> IActionResult
    abstract member Post:'E -> IActionResult

    [<HttpGet>]
    [<Produces("application/json")>]
    default this.Get() =
        repo.GetAll().ToList()
        |> this.Json :> IActionResult
    [<HttpPost>]
    default this.Post (entity) =
        let result = repo.AddEntity(entity) 
        this.Created(uri= "",value= result) :> IActionResult

    [<HttpPut>]
    member this.Put (id, entity) =
        let result = repo.UpdateEntity(id, entity) 
        this.Ok(result)        

    [<HttpDelete>]
    member this.Delete (id) =
        let result = repo.Delete(id) 
        this.Ok(result)