type Aggregate = { Name: string; Age: int }
let print a = sprintf "%s %i" a.Name a.Age

//how to FizzBuzz?\ maybe some test?
let (|Fizz|_|) n = if n % 3 = 0 then Some n else None

let (|Buzz|_|) n = if n % 5 = 0 then Some n else None


let fizzBuzz n =
    match n with
    | Fizz _ & Buzz _ -> "FizzBuzz"
    | Fizz _ -> "Fizz"
    | Buzz _ -> "Buzz"
    | _ -> n.ToString()


[ 1 .. 100 ] |> List.map fizzBuzz


let f x = 1 + x
let sum x y = x + y

//a -> b -> c

//a -> (b -> c)
//not the same
let add (x, y) = x + y
