namespace FSharpWebApp.DataAccessLayer
    open Microsoft.EntityFrameworkCore
    open System.Linq
    open FSharpWebApp.Models

    type Repository<'E, 'T when 'E :not struct and 'E :> IEntity<'T> and 'T: equality>(context: AppDataContext) = 
        let _context = context//new AppDataContext()
        interface IRepository<'E, 'T> with
            member this.AddEntity(entity) = 
                let result = _context.Set<'E>().Add(entity).Entity
                _context.SaveChanges() |> ignore
                result;
            member this.Get k= 
                _context.Set<'E>().Find(k)
            member this.GetAll() = 
                _context.Set<'E>().AsQueryable()
            member this.UpdateEntity (k, entity) = 
                let e = _context.Set<'E>().Find(k)
                _context.Entry(e).State <- EntityState.Modified
                _context.SaveChanges() |> ignore
                entity

            member this.Delete k = 
                let e = _context.Set<'E>().Find(k)
                _context.Set<'E>().Remove(e) |> ignore
                _context.SaveChanges() > 0
