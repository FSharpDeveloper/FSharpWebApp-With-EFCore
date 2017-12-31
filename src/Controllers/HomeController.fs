namespace FSharpWebApp.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open FSharpWebApp.DataAccessLayer
open FSharpWebApp.Models
open System.Collections.Generic

type HomeController (context: AppDataContext) =
    inherit Controller()
    let mutable _context = context;
    member this.Index () =
        let repo = new Repository<Group, int>(_context)
        let record = {
                GroupId = 0;
                Groupname = "group1";
                Description = "description";
                Members = List<Member>[
                    Member({
                        MemberId = 0;
                        Username = "test";
                        Email = "test@server.com";
                        Password = "password";
                    })]                             
            }
        Group(record)
        |> (repo:> IRepository<Group, int>).AddEntity
        |> ignore

        (repo:> IRepository<Group, int>).GetAll
        |> this.View

    member this.About () =
        this.ViewData.["Message"] <- "Your application description page."
        this.View()

    member this.Contact () =
        this.ViewData.["Message"] <- "Your contact page."
        this.View()

    member this.Error () =
        this.View();
