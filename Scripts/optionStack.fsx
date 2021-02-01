let lorem =
    @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum"

open System

let words = lorem.Split() |> Array.toList

let owords =
    words
    |> List.map
        (fun word ->
            if word.StartsWith("a") then
                None
            else
                Some word)

words |> List.map (fun word -> word.Length)

owords
|> List.map (Option.map (fun (s: string) -> s.Length))

//avanzato

#r "nuget: FSharpPlus"

open FSharpPlus

monad {
    let! x = words
    x |> (fun word -> word.Length)

}


open FSharpPlus.Data
let z = OptionT(owords)
let z' = OptionT.lift words

monad {
    let! x = OptionT(owords)
    x |> (fun word -> word.Length)

}
|> OptionT.run

//o anche
OptionT(owords)
|> OptionT.map (fun word -> word.Length)


//reader
let f (start: string) v = start.Length + v

let g s = fun v -> f s v

let (conf: ReaderT<unit, string>) = ReaderT.lift "text"

let gasr = ReaderT.lift g
ReaderT.map g conf
 

