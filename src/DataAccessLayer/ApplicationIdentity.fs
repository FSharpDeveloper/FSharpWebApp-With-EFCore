namespace FSharpWebApp.DataAccessLayer

    open FSharpWebApp.Models
    open Microsoft.AspNetCore.Identity
    open Microsoft.AspNetCore.Identity.EntityFrameworkCore

    type ApplicationRoleManager(roleStore:IRoleStore<ApplicationRole>) =
        inherit RoleManager<ApplicationRole>(roleStore, null, null, null, null)
    
    type ApplicationUserManager(userStore:UserStore<ApplicationUser>) =
        inherit UserManager<ApplicationUser>(userStore, null, null, null, null, null, null, null, null);   