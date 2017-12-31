namespace FSharpWebApp.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open FSharpWebApp.DataAccessLayer
open FSharpWebApp.Models
open System.Collections.Generic
open Microsoft.EntityFrameworkCore.Infrastructure
open Microsoft.EntityFrameworkCore

type GroupsController (context: AppDataContext) =
    inherit Controller()
    let mutable _context = context;
    let repo = (new Repository<Group, int>(_context):> IRepository<Group, int>)
    member this.Index () =
        this.View()
    member this.LoadGroups() =
        let result = 
            repo.GetAll()
                .Include(fun g -> g.Members)
                .Select(
                    fun g -> 
                    {
                        GroupId=g.GroupId;
                        Groupname=g.Groupname;
                        Description=g.Description; 
                        Members=g.Members
                    })
                .ToList()

        this.Json(result) 
    member this.Create (entity) =
        repo.AddEntity(entity)
        |> ignore
        this.View()

    member this.Error () =
        this.View();
