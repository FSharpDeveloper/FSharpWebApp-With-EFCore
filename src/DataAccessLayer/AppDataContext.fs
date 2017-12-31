namespace FSharpWebApp.DataAccessLayer
    open Microsoft.EntityFrameworkCore
    open FSharpWebApp.Models
    open System
    open System.Collections.Generic
    open System.Collections
    open Microsoft.AspNetCore.Identity.EntityFrameworkCore

    type AppDataContext(options, types) =         
        inherit IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
        let mutable _types: array<Type> = types;
        new (options) = new  AppDataContext(options, null)    

        [<DefaultValue>]
        val mutable _members: DbSet<Member>
        [<DefaultValue>]
        val mutable _groups:DbSet<Group>

            member public this.Members
            with get():DbSet<Member> = this._members
            and set value = this._members <- value

        member public this.Groups
            with get():DbSet<Group> = this._groups
            and set value = this._groups <- value 

        override this.OnConfiguring optionsBuilder =  
            optionsBuilder.UseSqlServer(@"Server=.\;Initial Catalog=FSCourseDb;Integrated Security=true;") |> ignore
            base.OnConfiguring optionsBuilder |> ignore

        override this.OnModelCreating modelBuilder = 
            modelBuilder.Entity<Member>().ToTable("Members") |> ignore
            base.OnModelCreating modelBuilder |> ignore