open FSharp.Core

let myapply fopt farg =
    match fopt, farg with
    | Some f, Some arg -> arg |> f |> Some
    | _ -> None

let (<*>) = myapply //Option.apply
let retn = Some

let rec mapOption f list =
    let cons head tail = head :: tail

    //let more = retn cons //ecco perchè esce apply
    match list with
    | [] -> retn []
    | head :: tail -> retn cons <*> (f head) <*> (mapOption f tail)


[ 1 .. 10 ]
|> mapOption (fun n -> if n % 2 = 0 then Some(n / 2) else None)


[ 1 .. 10 ] |> mapOption Some
