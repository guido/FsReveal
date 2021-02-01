//pattern matching via active pattern
let (|Odd|Even|) n = if n % 2 = 0 then Even n else Odd n


//ritorna un Result
let half n =
    match n with
    | Even x -> Ok(x / 2)
    | _ -> Error(sprintf $"Necessito di un numero pari!! {n}")


let x = half 18

//binding: Railway oriented programming  - and the elevated  world
let y = 26 |> half |> Result.bind half


type Contact = { Name: string; Email: string }

let contact =
    { Name = "Gianni"
      Email = "gian@gian.ni" }
//DA QUI
open System
open System.Text.RegularExpressions

let (|IsValidName|_|) input =
    if input <> String.Empty then
        Some input
    else
        None

let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match(str)

    if m.Success then
        Some(List.tail [ for x in m.Groups -> x.Value ])
    else
        None


let (|IsValidEmail|_|) input =
    match input with
    | ParseRegex ".*?@(.*)" [ _ ] -> Some input
    | _ -> None

let (|IsValidPhone|_|) input =
    match input with
    | ParseRegex "([0-9]{10})" [ _ ] -> Some input
    | _ -> None

let (|IsValidDate|_|) (input: string) =
    let (success, value) = DateTime.TryParse(input)
    if success then Some value else None


type ValidationFailure =
    | NameIsInvalidFailure
    | EmailIsInvalidFailure
    | PhoneIsInvalidFailure

//funzioni che usano il match

let validateName input = // string -> Result<string, ValidationFailure list>
    match input with
    | IsValidName input -> Ok input
    | _ -> Error [ NameIsInvalidFailure ]

let validateEmail input = // string -> Result<string, ValidationFailure list>
    match input with
    | IsValidEmail input -> Ok input
    | _ -> Error [ EmailIsInvalidFailure ]


let validatePhone input =
    match input with
    | IsValidPhone input -> Ok input
    | _ -> Error [ PhoneIsInvalidFailure ]
// refactor ... later

validatePhone "3425"
// type Name = Name of string //strong types

let nonEmpty text = String.IsNullOrEmpty(text)
//NB esibire il tipo result brevemente

//https://www.softwarepark.cc/blog/2019/12/8/functional-validation-in-f-using-applicatives
let apply fResult xResult =
    match fResult, xResult with
    | Ok f, Ok x -> x |> f |> Ok
    | Error e, Ok _ -> Error e
    | Ok _, Error e -> Error e
    | Error ef, Error ex -> Error(List.concat [ ef; ex ])

//https://davesquared.net/2015/07/apply-pattern.html
let composedApply rf rx =
    Result.bind (fun f -> Result.map f rx) rf
//attenzione non conctena



let (<!>) = Result.map
let (<*>) = apply //composedApply



//funzione che torna un Contact
(* let create name email = { Name = name; Email = email }



let simpleCreate name = { Name = name; Email = "ciao@cic.io" }


let createContact (name: string) (email: string): Result<Contact, ValidationFailure list> =
    let n = validateName name
    let e = validateEmail email
    create <!> n <*> e

let result = createContact "" "gio" *)


type Customer =
    { Name: string
      Email: string
      Phone: string }

let createCustomer name email phone =
    { Name = name
      Email = email
      Phone = phone }

let makeCustomer name email phone =
    createCustomer 
    <!> validateName name
    <*> validateEmail email
    <*> validatePhone phone


let res =
    makeCustomer "ciccio" "ci@cc.io" "1234567890"

let res = makeCustomer "" "cic.io" "fweef"

//<!> è map
//map (a -> b) -> Ma -> Mb
let step =
    (fun x -> createCustomer <!> validateName x)

// createCustomer string -> string -> string -> Cusomer
// leggetela così string -> (string -> string -> Cusomer)

// separate la testa dalla coda

// a è string
// b è string -> string -> Cusomer
// M è Result<_, e> qui e: list of ValidationFailure
// esce fuori Mb : M(string -> string -> Customer)

//esece fuori Result<(string -> string -> Customer),ValidationFailure list>

//<*> è apply
//apply M(a->b) ->Ma ->Mb

//a è string
//b è string -> Customer
//M è Result<_, e>
//esce fuori M(string -> Customer)
let step2 =
    (fun n e ->
        createCustomer <!> validateName n
        <*> validateEmail e)
//ultimo passaggio come come sopra
// si usa nuovamnete apply per il terzo passo
//apply M(a->b) ->Ma ->Mb
//a è string
//b è Customer
//M è Result<_, e>
//esce fuori M(Customer)
//cioè Resul<Customer, e> Resultt<Customer, ValidationFailure list>
