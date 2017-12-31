namespace FSharpWebApp.DataAccessLayer
    open System.Linq
    type IRepository<'E, 'K> = 
        abstract member AddEntity:'E -> 'E
        abstract member UpdateEntity: 'K * 'E -> 'E
        abstract member Get:('K) -> 'E
        abstract member GetAll:unit -> IQueryable<'E>
        abstract member Delete:'K -> bool
