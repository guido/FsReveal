type User = { Name: string; Email: string }

type IUserRepository =
    abstract GetUserCollection: unit -> seq<User>

type IEnvironment =
    abstract UserRepository: unit -> IUserRepository


//ora costruiamo una funzione in termini di IUserRepository
let getUserByEmail (user_repository: IUserRepository) email =
    user_repository.GetUserCollection()
    |> Seq.tryFind (fun x -> x.Email = email)


//ora rimodelliamo in stile dependency injection:
//esplicitiamo che abbiamo una dipendanza sull'ambiente.. non necessariament euna grande idea
//sarebbe preferibile lasciare la cosa accadere implicitamenmte

let boundGetUserByEmail (environment: IEnvironment) email =
    let repository = environment.UserRepository()
    getUserByEmail repository email


#r "nuget: FSharpx.Extras"
open FSharpx


let compute n m = n * m
let getint () = 18

let dcompute n (gi:unit -> int) =
    gi() |> compute n




let rui = Reader.reader {
    return! getint
}

Reader.ask rui ()

rui ()


Reader.asks 


let comp = Reader.reader {
    let! (simple:unit -> int) = Reader.ask
    let x = simple ()
    
    printfn "%i" x
    
} 

comp rui




let dcomp n = Reader.reader {
    let! (simple:unit -> int) = Reader.ask
    let x = simple ()
    let y = compute n x
    return y
    
} 


dcomp 10 rui

