let a = 5
let factorial x = [ 1 .. x ] |> List.reduce (*)
let c = factorial a



let f x = 1 + x
let sum x y = x + y
//non the same
let add (x, y) = x + y


(* int -> string -> Option<int>

a -> b -> c

a   b -> c *)
sum 1 2


add (1, 2)

//partial application
let increment = sum 1

increment 4

4 |> increment

4 |> increment |> increment

[ 1 .. 10 ] |> List.map increment


List.map increment [ 1 .. 10 ]

//Primitive obsession
type Partnumber = Partnumber of string // single case

//discriminated union
type Contact =
    | Phone of string
    | CellPhone of string

let p = Partnumber "whatever"

//qui vedimao già una forma di deconstructuion
let print (Partnumber p) = sprintf "this is %s" p

"ciao" |> Partnumber |> print


type Aggregate = { Name: string; Age: int }

let print a = sprintf "%s %i" a.Name a.Age

print { Name = "Gian"; Age = 13 }
