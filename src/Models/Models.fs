namespace FSharpWebApp.Models
    open System
    open System.ComponentModel.DataAnnotations
    open System.ComponentModel.DataAnnotations.Schema
    open System.Collections.Generic

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
        [<DefaultValue>]val mutable memberId:int;
        [<DefaultValue>]val mutable username:string;
        [<DefaultValue>]val mutable email:string;
        [<DefaultValue>]val mutable password:string;
        [<DefaultValue>]val mutable groupid:int;
        [<DefaultValue>]val mutable group:Group;
        do 
            this.memberId <- record.MemberId
            this.username <- record.Username
            this.email <- record.Email
            this.password <- record.Password
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
        [<DefaultValue>]val mutable groupid:int;
        [<DefaultValue>]val mutable groupname:string;
        [<DefaultValue>]val mutable description:string;
        [<DefaultValue>]val mutable members:ICollection<Member>;
        do 
            this.groupid <- group.GroupId
            this.groupname <- group.Groupname
            this.description <- group.Description
            this.members <- group.Members

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