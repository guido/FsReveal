#r "nuget: Unquote"

open Swensen.Unquote

let f x = x + x

<@ f 2 = 4 @>

test <@ f2 = 4 @>
test <@ f 2 = 14 @>


test
    <@ ([ 3; 2; 1; 0 ] |> List.map ((+) 1)) = [ 1 + 3 .. 1 + 0 ]
       && f 3 = 6 @>
