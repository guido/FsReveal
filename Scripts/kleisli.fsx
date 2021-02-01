let close n = [ n - 1; n + 1 ]

let generate n = [ 1 .. n ]
close 4

#r "nuget: FsharpPlus"

open FSharpPlus

let twice = close >=> close

twice 80

10 |> (generate >=> generate) //|> List.length

open System

let length (s: string) =
    if String.IsNullOrEmpty(s) then
        None
    else
        s.Length |> Some



let Even n = n % 2 = 0

let f = length >=> (Some << Even)
