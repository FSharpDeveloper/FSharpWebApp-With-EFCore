namespace FSharpWebApp.ViewModels
    open System
    open System.ComponentModel.DataAnnotations
    type RegisterViewModel() =
        [<Required>]
        [<EmailAddress>]
        [<Display(Name = "Email")>]
        member val Email:string = "" with get,set

        [<Required>]
        [<StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)>]
        [<DataType(DataType.Password)>]
        [<Display(Name = "Password")>]
        member val Password:string = "" with get,set

        [<DataType(DataType.Password)>]
        [<Display(Name = "Confirm password")>]
        [<Compare("Password", ErrorMessage = "The password and confirmation password do not match.")>]
        member val ConfirmPassword:string = "" with get,set
    