let opt = Some 5

let another = None

let tellme opt =
    match opt with
    | Some x -> sprintf "abbaimo un valore: %s" x
    | _ -> "ANCHE NO"



"gio" |> Some |> tellme
//forma equivalente senza pipe: si sta passando una option a tellme
(Some "gio") |> tellme
None |> tellme

//altri pattern amtchin

let (|Pari|Dispari|) n = if n % 2 = 0 then Pari else Dispari



let By2 n =
    match n with
    | Pari -> n / 2 |> Some
    | Dispari -> None


By2 4

By2 3


7 |> By2

//problema

32 |> By2 |> Option.bind By2
//|> Option.bind By2


let (|Divisible|_|) q p = if p % q = 0 then Some p else None

let By q p =
    match p with
    | Divisible q p -> p / q |> Some
    | _ -> None

//point free style
let ByAgain q =
    function
    | Divisible q p -> p / q |> Some
    | _ -> None

By 5 17
By 5 15

ByAgain 5 17
ByAgain 5 15


//defining a Maybe from scracth

type Maybe<'a> =
    | Just of 'a
    | BadLuck

let divByRepet a b c =
    let first = By b c
    let result = first |> Option.bind (By a)
    result

let better a b c = c |> By b |> Option.bind (By a)



better 3 5 30

better 3 5 31





let (>>=) x f = x |> Option.bind f

//32 |> By2 >>= By2

let evenbetter a b c = (c |> By b) >>= By a

evenbetter 2 5 31


//CE the happy path :D
type OptionBuilder() =
    member _.Return x = Some x

    member self.Bind(x, f) =
        match x with
        | Some stuff -> stuff |> f
        | _ -> None

let option = OptionBuilder()

//Dealing with the happy path only
//possiamo dimenticarci in gran parte del binding
//rimandando la soluzione del problema a l 'contesto' option
let happiness a b c d =
    option {
        let! x = d |> By c
        let! y = x |> By b
        let! z = y |> By a
        return z
    }

//notare la somiglianza con async/await e nulla propagation
happiness 2 3 4 3000
happiness 1 1 2 3


type LogBuilder() =
    member _.Return x = x

    member _.Bind(x, f) =
        printfn $"[--- {x} --]"
        x |> f

let log = LogBuilder()

let code =
    log {
        let! x = 18
        let! txt = "testo"
        let hidden = "questo no"

        let! si = "di nuovo"
        return x
    }
