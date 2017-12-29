namespace FSharpWebApp.DataAccessLayer
    type IRepository<'E> = 
        abstract member AddEntity:'E -> bool
        abstract member UpdateEntity: int * 'E -> bool
        abstract member Get:int -> 'E
        abstract member GetAll:unit -> array<'E>
        abstract member Delete:int -> bool
