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
open Microsoft.EntityFrameworkCore.Migrations.Operations.Builders
open System
open System.Collections.Generic

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

    type AspNetRolesTable = 
        {   Id:OperationBuilder<AddColumnOperation>; 
            ConcurrencyStamp:OperationBuilder<AddColumnOperation>;
            Name:OperationBuilder<AddColumnOperation>;
            NormalizedName:OperationBuilder<AddColumnOperation>;}

    type AspNetUsersTable = 
        {   Id:OperationBuilder<AddColumnOperation>;
            AccessFailedCount:OperationBuilder<AddColumnOperation>;
            ConcurrencyStamp:OperationBuilder<AddColumnOperation>;
            Email:OperationBuilder<AddColumnOperation>;
            EmailConfirmed:OperationBuilder<AddColumnOperation>;
            LockoutEnabled:OperationBuilder<AddColumnOperation>;
            LockoutEnd:OperationBuilder<AddColumnOperation>;
            NormalizedEmail:OperationBuilder<AddColumnOperation>;
            NormalizedUserName:OperationBuilder<AddColumnOperation>;
            PasswordHash:OperationBuilder<AddColumnOperation>;
            PhoneNumber:OperationBuilder<AddColumnOperation>;
            PhoneNumberConfirmed:OperationBuilder<AddColumnOperation>;
            SecurityStamp:OperationBuilder<AddColumnOperation>;
            TwoFactorEnabled:OperationBuilder<AddColumnOperation>;
            UserName:OperationBuilder<AddColumnOperation>; }

    type AspNetRoleClaimsTable = 
        {   Id:OperationBuilder<AddColumnOperation>;
            ClaimType:OperationBuilder<AddColumnOperation>;
            ClaimValue:OperationBuilder<AddColumnOperation>;
            RoleId:OperationBuilder<AddColumnOperation>; }   

    type AspNetUserClaimsTable =
        {   Id:OperationBuilder<AddColumnOperation>;
            ClaimType:OperationBuilder<AddColumnOperation>;
            ClaimValue:OperationBuilder<AddColumnOperation>;
            UserId:OperationBuilder<AddColumnOperation>; }    

    type AspNetUserLogins = 
        {   LoginProvider:OperationBuilder<AddColumnOperation>;
            ProviderKey:OperationBuilder<AddColumnOperation>;
            ProviderDisplayName:OperationBuilder<AddColumnOperation>;
            UserId:OperationBuilder<AddColumnOperation>; }      

    type LoginProviderKey = 
        { LoginProvider:obj; ProviderKey:obj }

    type AspNetUserRolesTable = 
        {   UserId:OperationBuilder<AddColumnOperation>;
            RoleId:OperationBuilder<AddColumnOperation>; } 

    type AspNetUserTokensTable = 
        {   UserId:OperationBuilder<AddColumnOperation>;
            LoginProvider:OperationBuilder<AddColumnOperation>;
            Name:OperationBuilder<AddColumnOperation>;
            Value:OperationBuilder<AddColumnOperation>; }    

    type AspNetUserTokensKey = 
        {   UserId:obj; LoginProvider:obj; Name:obj }
    [<DbContext(typeof<AppDataContext>)>]
    [<Migration("01012018_Initial")>]
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

            migrationBuilder.CreateTable(
                name = "AspNetRoles",
                columns = (fun table ->
                    {
                        Id = table.Column<string>(nullable = false)
                        ConcurrencyStamp = table.Column<string>(nullable = true)
                        Name = table.Column<string>(maxLength = Nullable<int>(256), nullable = true)
                        NormalizedName = table.Column<string>(maxLength = Nullable<int>(256), nullable = true)
                    }),
                constraints = fun table ->
                        table.PrimaryKey("PK_AspNetRoles", fun x -> x.Id :> obj) |> ignore
                ) |> ignore
            
            migrationBuilder.CreateTable(
                name = "AspNetUsers",
                columns = (fun table ->
                    {
                        Id = table.Column<string>(nullable= false)
                        AccessFailedCount = table.Column<int>(nullable= false)
                        ConcurrencyStamp = table.Column<string>(nullable= true)
                        Email = table.Column<string>(maxLength= Nullable<int>(256), nullable= true)
                        EmailConfirmed = table.Column<bool>(nullable= false)
                        LockoutEnabled = table.Column<bool>(nullable= false)
                        LockoutEnd = table.Column<DateTimeOffset>(nullable= true)
                        NormalizedEmail = table.Column<string>(maxLength= Nullable<int>(256), nullable= true)
                        NormalizedUserName = table.Column<string>(maxLength= Nullable<int>(256), nullable= true)
                        PasswordHash = table.Column<string>(nullable= true)
                        PhoneNumber = table.Column<string>(nullable= true)
                        PhoneNumberConfirmed = table.Column<bool>(nullable= false)
                        SecurityStamp = table.Column<string>(nullable= true)
                        TwoFactorEnabled = table.Column<bool>(nullable= false)
                        UserName = table.Column<string>(maxLength= Nullable<int>(256), nullable= true)
                    }),
                constraints = fun table ->                
                    table.PrimaryKey("PK_AspNetUsers", fun x -> x.Id :> obj) |> ignore
            ) |> ignore

            migrationBuilder.CreateTable(
                name = "AspNetRoleClaims",
                columns = (fun table ->
                    {
                        Id = table.Column<int>(nullable= false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        ClaimType = table.Column<string>(nullable= true)
                        ClaimValue = table.Column<string>(nullable= true)
                        RoleId = table.Column<string>(nullable= false)
                    }),
                constraints = fun table ->                
                    table.PrimaryKey("PK_AspNetRoleClaims", fun x -> x.Id :> obj) |> ignore
                    table.ForeignKey(
                        name = "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column = (fun x -> x.RoleId :> obj),
                        principalTable = "AspNetRoles",
                        principalColumn = "Id",
                        onDelete = ReferentialAction.Cascade) |> ignore
                ) |> ignore
            migrationBuilder.CreateTable(
                name = "AspNetUserClaims",
                columns = (fun table ->
                    {
                        Id = table.Column<int>(nullable= false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        ClaimType = table.Column<string>(nullable= true)
                        ClaimValue = table.Column<string>(nullable= true)
                        UserId = table.Column<string>(nullable= false)
                    }),
                constraints = fun table ->                
                    table.PrimaryKey("PK_AspNetUserClaims", fun x -> x.Id :> obj) |> ignore 
                    table.ForeignKey(
                        name= "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column= (fun x -> x.UserId :> obj),
                        principalTable= "AspNetUsers",
                        principalColumn= "Id",
                        onDelete= ReferentialAction.Cascade) |> ignore
            ) |> ignore
            migrationBuilder.CreateTable(
                name= "AspNetUserLogins",
                columns= (fun table ->
                    {
                        LoginProvider = table.Column<string>(nullable= false)
                        ProviderKey = table.Column<string>(nullable= false)
                        ProviderDisplayName = table.Column<string>(nullable= true)
                        UserId = table.Column<string>(nullable= false)
                    })
                // constraints= fun table ->
                //     table.PrimaryKey("PK_AspNetUserLogins", fun x ->  { LoginProvider = x.LoginProvider:>obj; ProviderKey = x.ProviderKey:>obj  } :> obj ) |> ignore
                //     table.ForeignKey(
                //         name= "FK_AspNetUserLogins_AspNetUsers_UserId",
                //         column= (fun x -> x.UserId :> obj),
                //         principalTable= "AspNetUsers",
                //         principalColumn= "Id",
                //         onDelete= ReferentialAction.Cascade) |> ignore
            ) |> ignore

            migrationBuilder.CreateTable(
                name = "AspNetUserRoles",
                columns = (fun table ->
                    {
                        UserId = table.Column<string>(nullable= false)
                        RoleId = table.Column<string>(nullable= false)
                    }),
                constraints = fun table ->
                    table.PrimaryKey("PK_AspNetUserRoles", fun x -> { UserId = x.UserId; RoleId = x.RoleId } :> obj) |> ignore
                    table.ForeignKey(
                        name= "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column= (fun x -> x.RoleId :> obj),
                        principalTable= "AspNetRoles",
                        principalColumn= "Id",
                        onDelete= ReferentialAction.Cascade) |> ignore
                    table.ForeignKey(
                        name= "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column= (fun x -> x.UserId :> obj),
                        principalTable= "AspNetUsers",
                        principalColumn= "Id",
                        onDelete= ReferentialAction.Cascade) |> ignore
            ) |> ignore

            migrationBuilder.CreateTable(
                name = "AspNetUserTokens",
                columns = (fun table ->
                    {
                        UserId = table.Column<string>(nullable= false)
                        LoginProvider = table.Column<string>(nullable= false)
                        Name = table.Column<string>(nullable= false)
                        Value = table.Column<string>(nullable= true)
                     })
                // constraints = fun table ->
                //     table.PrimaryKey("PK_AspNetUserTokens", fun x -> { UserId = x.UserId; LoginProvider = x.LoginProvider; Name = x.Name } :> obj) |> ignore
                //     table.ForeignKey(
                //         name= "FK_AspNetUserTokens_AspNetUsers_UserId",
                //         column= (fun x -> x.UserId :> obj),
                //         principalTable= "AspNetUsers",
                //         principalColumn= "Id",
                //         onDelete= ReferentialAction.Cascade) |> ignore
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
                    // e.Property<ICollection<Member>>("Members") |> ignore)
                |> ignore     


            modelBuilder.Entity("FSharpWebApp.Models.ApplicationRole",
                fun b ->
                    b.Property<string>("Id") |> ignore
                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken() |> ignore
                    b.Property<string>("Name")
                        .HasMaxLength(256) |> ignore
                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256) |> ignore
                    b.HasKey("Id") |> ignore
                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL") |> ignore
                    // b.HasMany("ApplicationUser", "Users") |> ignore                    
                    b.ToTable("AspNetRoles") |> ignore
                ) |> ignore

            modelBuilder.Entity("FSharpWebApp.Models.ApplicationUser", 
                fun b ->
                    b.Property<string>("Id") |> ignore
                    b.Property<int>("AccessFailedCount") |> ignore
                    b.Property<string>("ConcurrencyStamp") 
                        .IsConcurrencyToken() |> ignore
                    b.Property<string>("Email")
                        .HasMaxLength(256) |> ignore
                    b.Property<bool>("EmailConfirmed") |> ignore
                    b.Property<bool>("LockoutEnabled") |> ignore
                    b.Property<Nullable<DateTimeOffset>>("LockoutEnd") |> ignore
                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256) |> ignore
                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256) |> ignore
                    b.Property<string>("PasswordHash") |> ignore
                    b.Property<string>("PhoneNumber") |> ignore
                    b.Property<bool>("PhoneNumberConfirmed") |> ignore
                    b.Property<string>("SecurityStamp") |> ignore
                    b.Property<bool>("TwoFactorEnabled") |> ignore
                    b.Property<string>("UserName")
                        .HasMaxLength(256) |> ignore
                    b.HasKey("Id") |> ignore
                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex") |> ignore
                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL") |> ignore
                    b.ToTable("AspNetUsers") |> ignore
                ) |> ignore

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", 
                fun b ->                
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd() |> ignore
                    b.Property<string>("ClaimType") |> ignore
                    b.Property<string>("ClaimValue") |> ignore
                    b.Property<string>("RoleId") |> ignore
                    b.HasKey("Id") |> ignore
                    b.HasIndex("RoleId") |> ignore
                    b.ToTable("AspNetRoleClaims") |> ignore
                ) |> ignore

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", 
                fun b ->                
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd() |> ignore
                    b.Property<string>("ClaimType") |> ignore
                    b.Property<string>("ClaimValue") |> ignore
                    b.Property<string>("UserId") |> ignore
                    b.HasKey("Id") |> ignore
                    b.HasIndex("UserId") |> ignore
                    b.ToTable("AspNetUserClaims") |> ignore
                ) |> ignore

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", 
                fun b ->                
                    b.Property<string>("LoginProvider") |> ignore
                    b.Property<string>("ProviderKey") |> ignore
                    b.Property<string>("ProviderDisplayName") |> ignore
                    b.Property<string>("UserId") |> ignore
                    b.HasKey("LoginProvider", "ProviderKey") |> ignore
                    b.HasIndex("UserId") |> ignore
                    b.ToTable("AspNetUserLogins") |> ignore
                ) |> ignore

            modelBuilder.Entity("FSharpWebApp.Models.AspNetUserRoles", 
                fun b ->                
                    b.Property<string>("UserId") |> ignore
                    b.Property<string>("RoleId") |> ignore
                    b.HasKey("UserId", "RoleId") |> ignore
                    b.HasIndex("RoleId") |> ignore
                    b.ToTable("AspNetUserRoles") |> ignore
                ) |> ignore

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", 
                fun b ->                
                    b.Property<string>("UserId") |> ignore
                    b.Property<string>("LoginProvider") |> ignore
                    b.Property<string>("Name") |> ignore
                    b.Property<string>("Value") |> ignore
                    b.HasKey("UserId", "LoginProvider", "Name") |> ignore
                    b.ToTable("AspNetUserTokens") |> ignore
                ) |> ignore                                
