namespace FSharpWebApp.Migrations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Infrastructure
open Microsoft.EntityFrameworkCore.Design
open Microsoft.EntityFrameworkCore.Migrations
open Microsoft.EntityFrameworkCore.Storage
open Microsoft.EntityFrameworkCore.Metadata
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Microsoft.EntityFrameworkCore.Migrations.Operations
open Microsoft.EntityFrameworkCore.Migrations.Operations.Builders
open Microsoft.EntityFrameworkCore.Migrations.Operations.Builders
open Microsoft.EntityFrameworkCore.Metadata.Internal
open System.Linq
open FSharpWebApp.DataAccessLayer
open FSharpWebApp.Models

    type MembersTable = 
        {   MemberId:OperationBuilder<AddColumnOperation>;             
            Username:OperationBuilder<AddColumnOperation>;             
            Email:OperationBuilder<AddColumnOperation>;         
            Password:OperationBuilder<AddColumnOperation>;
            GroupId:OperationBuilder<AddColumnOperation>; }
    
    type GroupsTable = 
        {   GroupId:OperationBuilder<AddColumnOperation>;             
            Groupname:OperationBuilder<AddColumnOperation>;             
            Description:OperationBuilder<AddColumnOperation>; }

    [<DbContext(typeof<AppDataContext>)>]
    [<Migration("29122017_Initial")>]
    type Init() = 
        inherit Migration()
        override this.Up(migrationBuilder: MigrationBuilder) = 
            migrationBuilder
                .CreateTable(
                    name = "Groups",
                    columns = 
                        (fun table ->
                            {
                                GroupId = table.Column<int>(nullable = false).Annotation("SqlServer:ValueGeneratedStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                                Groupname = table.Column<string>(nullable = false)
                                Description = table.Column<string>(nullable = false)
                            }),
                    constraints = fun table -> table.PrimaryKey("PK_GroupId", (fun x -> x.GroupId :> obj)) |> ignore
                ) |> ignore         
            migrationBuilder
                .CreateTable(
                    name = "Members",
                    columns = 
                        (fun table ->
                            {
                                MemberId = table.Column<int>(nullable = false).Annotation("SqlServer:ValueGeneratedStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                                Username = table.Column<string>(nullable = false)
                                Email = table.Column<string>(nullable = false)
                                Password = table.Column<string>(nullable = false)     
                                GroupId = table.Column<int>(nullable = false)     
                            }),
                    constraints = (fun table -> 
                        table.PrimaryKey("PK_MemberId", (fun x -> x.MemberId :> obj)) |> ignore
                        table.ForeignKey("FK_MemberGroup_GroupId", (fun x -> x.GroupId :> obj), "Groups", "GroupId") |> ignore)
                ) |> ignore
               
        override this.Down(migrationBuilder: MigrationBuilder) = 
            migrationBuilder.DropTable(name = "Members") |> ignore
            migrationBuilder.DropTable(name = "Groups") |> ignore
        override this.BuildTargetModel(modelBuilder: ModelBuilder) = 
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                |> ignore
            modelBuilder.Entity("FSharpWebApp.Models.Member", 
                fun e -> 
                    e.Property<int>("MemberId").ValueGeneratedOnAdd() |> ignore
                    e.Property<string>("Username") |> ignore            
                    e.Property<string>("Email") |> ignore
                    e.Property<string>("Password") |> ignore) 
                |> ignore
            modelBuilder.Entity("FSharpWebApp.Models.Group", 
                fun e -> 
                    e.Property<int>("GroupId").ValueGeneratedOnAdd() |> ignore
                    e.Property<string>("Groupname") |> ignore            
                    e.Property<string>("Description") |> ignore
                    e.HasMany("Member", "Members") |> ignore)
                    //.Property<ICollection<Member>>("Members") |> ignore) 
                |> ignore            
