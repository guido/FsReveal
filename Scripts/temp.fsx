let a = 5
let factorial x = [ 1 .. x ] |> List.reduce (*)
let c = factorial a
