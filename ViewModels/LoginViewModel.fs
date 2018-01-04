namespace FSharpWebApp.ViewModels
    type LoginViewModel() =       
        member val Username:string = "" with get,set
        member val Email:string = "" with get,set
        member val Password:string = "" with get,set
        member val RememberMe:bool = false with get,set
