namespace FSharpWebApp.DataAccessLayer
    open Microsoft.EntityFrameworkCore
    open System.Linq
    open FSharpWebApp.Models

    type Repository<'E, 'T when 'E :not struct and 'E :> IEntity<'T> and 'T: equality>(context: AppDataContext) = 
        let _context = context//new AppDataContext()
        interface IRepository<'E> with
            member this.AddEntity(entity) = 
                _context.Set<'E>().Add(entity) |> ignore 
                _context.SaveChanges() > 0
            member this.Get k= 
                _context.Set<'E>().Find(k)
            member this.GetAll() = 
                _context.Set<'E>().ToList().ToArray()
            member this.UpdateEntity (k, entity) = 
                let e = _context.Set<'E>().Find(k)
                _context.Entry(e).State <- EntityState.Modified
                _context.SaveChanges() > 0
            member this.Delete k = 
                let e = _context.Set<'E>().Find(k)
                _context.Set<'E>().Remove(e) |> ignore
                _context.SaveChanges() > 0
