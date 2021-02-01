//https://gist.github.com/giuliohome/6bab3ce1d0ecd34a01e525f0082b1800
//copiato di peso ma interessante
[<AutoOpen>]
module IO =
    type IO<'a> =
        private
        | Return of (unit -> 'a)
        | Suspend of (unit -> IO<'a>)

    let rec run x =
        match x with
        | Return v -> v ()
        | Suspend s -> s () |> run

    [<RequireQualifiedAccess>]
    module IOMonad =
        let rtrn x = Return(fun () -> x)
        let bind f io = Suspend(fun () -> f (io |> run))
        let map f io = bind (f >> rtrn) io
        let wrap r = Return r

    type IOBuilder() =
        member this.Bind(x, f) = IOMonad.bind f x
        member this.Return(x) = IOMonad.rtrn x
        member this.ReturnFrom(io): IO<_> = io

    let io = new IOBuilder()

[<AutoOpen>]
module MaybeIO =

    type MaybeIO<'a> = IO<Option<'a>>

    let run (x: MaybeIO<'a>): 'a option = IO.run x

    [<RequireQualifiedAccess>]
    module MaybeIOMonad =
        let rtrn x: MaybeIO<'a> = IOMonad.rtrn (Some x)

        let bind f io =
            IOMonad.bind
                (function
                | Some x -> f x
                | None -> IOMonad.rtrn (None))
                io

        let map f io = bind (f >> rtrn) io
        let liftIO io = IOMonad.map Some io

    type MaybeIOBuilder() =
        member this.Bind(x, f) = MaybeIOMonad.bind f x
        member this.Return(x) = MaybeIOMonad.rtrn x
        member this.ReturnFrom(e): MaybeIO<_> = e

    let maybeIo = new MaybeIOBuilder()


//qui viene completata l'interpretazione
let readLine =
    IOMonad.wrap (fun () -> stdin.ReadLine())

let print x = IOMonad.wrap (fun () -> printfn "%A" x)

let foo =
    io {
        let! cs1 = readLine
        let! cs2 = readLine
        let x = cs1 + cs2
        let! _ = print x
        return x
    }

io {
    let! x =
        foo
        |> IOMonad.map (fun (s: string) -> s.ToUpper())

    let! _ = print x
    return ()
}
|> IO.run

//va specificato caso overload
let tryParse (str: string) =
    match System.Int32.TryParse(str) with
    | (true, int) -> Some(int)
    | _ -> None

let readInt = readLine |> IOMonad.map tryParse
let maybePrint = print >> MaybeIOMonad.liftIO

let bar =
    maybeIo {
        let! x = readInt
        let! y = io { return 20 } |> MaybeIOMonad.liftIO
        let result = x + y
        let! _ = maybePrint result
        return result
    }

bar |> MaybeIO.run
