let a = 5
let factorial x = [ 1 .. x ] |> List.reduce (*)
let c = factorial a


type Aggregate = { Name: string; Age:int}


let print a = sprintf "%s %i" a.Name a.Age 


let f x = 1 + x
let sum x y = x + y
//not the same
let add (x,y) = x + y

int -> (int -> int)

let g = sum 10

g 1

1 |> g