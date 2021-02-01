#r "nuget: FSharpPlus"

open FSharpPlus

open FSharpPlus.Data

type IUserRepository =
    abstract GetUser: email:string -> Async<string>

type IShoppingListRepository =
    abstract Add: shoppingList:string list -> string list

let getUser email =
    ReaderT(fun (env: #IUserRepository) -> env.GetUser email)

let addShoppingList shoppingList =
    ReaderT(fun (env: #IShoppingListRepository) -> async { return env.Add shoppingList })

let addShoppingListM email =
    monad {
        let! user = getUser email
        let shoppingList = [ "s" ]
        return! addShoppingList shoppingList
    }

type Env() =
    interface IUserRepository with
        member this.GetUser email = async { return "Sandeep" }

    interface IShoppingListRepository with
        member this.Add shoppingList = shoppingList



ReaderT.run (addShoppingListM "a@a") (Env())
|> fun listA ->
    async {
        let! list = listA
        printfn "%A" list
    }
|> Async.Start
