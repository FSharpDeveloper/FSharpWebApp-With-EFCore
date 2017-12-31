namespace FSharpWebApp.Models
    open System
    open System.ComponentModel.DataAnnotations
    open System.ComponentModel.DataAnnotations.Schema
    open System.Collections.Generic
    open Microsoft.AspNetCore.Identity

    type IEntity<'K when 'K:equality> = 
        abstract member Id:'K with get

    [<CLIMutable>]
    type GroupRecord = {
        GroupId:int;
        Groupname:string;
        Description:string;
        Members:ICollection<Member>
    }
    and MemberRecord = {
        MemberId:int;
        Username:string;
        Email:string;
        Password:string;
    }
    and Member(record:MemberRecord) as this = 
        [<DefaultValue>]val mutable private memberId:int;
        [<DefaultValue>]val mutable private username:string;
        [<DefaultValue>]val mutable private email:string;
        [<DefaultValue>]val mutable private password:string;
        [<DefaultValue>]val mutable private groupid:int;
        [<DefaultValue>]val mutable private group:Group;
        do 
            this.memberId <- record.MemberId
            this.username <- record.Username
            this.email <- record.Email
            this.password <- record.Password
        new() = Member({MemberId=0;Username="";Email="";Password="";})                
        [<Key>]
        member this.MemberId 
            with get() = this.memberId
            and set value = this.memberId <- value
        [<Required>]
        member this.Username 
            with get() = this.username
            and set value = this.username <- value
        [<Required>]        
        member this.Email 
            with get() = this.email
            and set value = this.email <- value
        [<Required>]
        member this.Password 
            with get() = this.password
            and set value = this.password <- value

        member this.GroupId 
            with get() = this.groupid
            and set value = this.groupid <- value 

        interface IEntity<int> with       
            member this.Id
                with get() = this.MemberId    

    and Group(group:GroupRecord) as this = 
        [<DefaultValue>]val mutable groupid:int
        [<DefaultValue>]val mutable groupname:string
        [<DefaultValue>]val mutable description:string
        [<DefaultValue>]val mutable members:ICollection<Member>
        do 
            this.groupid <- group.GroupId
            this.groupname <- group.Groupname
            this.description <- group.Description
            this.members <- group.Members
        new() = Group({GroupId=0;Groupname="";Description="";Members=new List<Member>()})
        [<Key>]
        member this.GroupId 
            with get() = this.groupid
            and set value = this.groupid <- value
        [<Required>]
        member this.Groupname 
            with get() = this.groupname
            and set value = this.groupname <- value
        [<Required>]        
        member this.Description 
            with get() = this.description
            and set value = this.description <- value

        member this.Members 
            with get() = this.members
            and set value = this.members <- value
        interface IEntity<int> with
            member this.Id  
                with get() = this.GroupId           

    type ApplicationUser() = 
        inherit IdentityUser<string>()
        interface IEntity<string> with
            member this.Id 
                with get() = this.Id

    type ApplicationRole() =
        inherit IdentityRole<string>()
        // [<DefaultValue>]val mutable _aspNetUsers:ICollection<AspNetUserRoles>
        interface IEntity<string> with
            member this.Id 
                with get() = this.Id     
        // member this.Users
        //     with get() = this._aspNetUsers
        //     and set value = this._aspNetUsers <- value           

    type ApplicationUserRole() =
        inherit IdentityUserRole<int>()

    type ApplicationUserClaim() = 
        inherit IdentityUserClaim<int>()    

    type ApplicationUserLogin() = 
        inherit IdentityUserLogin<int>()

    type ApplicationUserToken() = 
        inherit IdentityUserToken<int>()
    // and AspNetUserRoles() = 
    //     inherit IdentityUserRole<string>()
    //     [<DefaultValue>]val mutable applicationRole:ApplicationRole
    //     [<DefaultValue>]val mutable applicationUser:ApplicationUser          

    //     [<ForeignKey("RoleId")>]
    //     member this.AspNetRole 
    //         with get() = this.applicationRole
    //         and set value = this.applicationRole <- value
    //     [<ForeignKey("UserId")>]        
    //     member this.AspNetUser
    //         with get() = this.applicationUser
    //         and set value = this.applicationUser <- value    
    //     interface IEntity<string> with
    //         member this.Id with get() = (this.RoleId)                  