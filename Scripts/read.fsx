type Reader<'e, 'r> = Reader of ('e -> 'r)

module Reader =
    let run env (Reader reader) = reader env

    let ask = Reader id

    let map f reader =
        Reader(fun env -> reader |> run env |> f)

    let bind f reader =
        let newreader env =
            let a = run env reader
            run env (f a)

        Reader newreader

    type ReaderBuilder() =
        //costante
        member self.Return(x) = Reader(fun _ -> x)
        member self.Bind(x, f) = bind f x
        member self.Zero() = Reader(fun _ -> ())


open System

type ILogger =
    abstract Debug: string -> unit


type IConsole =
    abstract PrintLine: string -> unit
    abstract ReadLine: unit -> string


let console =
    { new IConsole with
        member self.PrintLine(s) = printfn "%s" s
        member self.ReadLine() = Console.ReadLine() }

let DEBUG = "[Debug]"

let logger =
    { new ILogger with
        member self.Debug(s) = printfn $"{DEBUG} {s}" }

type IServices = { Logger: ILogger; Console: IConsole }

let service = { Logger = logger; Console = console }

//service.Logger.Debug("opla")
let reader = Reader.ReaderBuilder()

//reder one console
let easyConsole =
    reader {
        let! (c: IConsole) = Reader.ask
        c.PrintLine("ciccio stella rocks")

    }

Reader.run console easyConsole


//requiring two dependecies via a big Environment
let trickyConsole =
    reader {
        let services = Reader.ask

        let! (io, log) =
            services
            |> Reader.map (fun env -> (env.Console, env.Logger))


        log.Debug("ciccio stella rocks")
        let line = io.ReadLine()

        return line

    }

Reader.run service trickyConsole

//requiring just the two dependecies I need
let twoStrict =
    reader {
        let! (io: IConsole, log: ILogger) = Reader.ask

        log.Debug("ciccio stella rocks")
        let line = io.ReadLine()

        return line

    }

Reader.run (service.Console, service.Logger) twoStrict


// two steps
let getString =
    reader {
        let! (io: IConsole) = Reader.ask
        return io.ReadLine()
    }


Reader.run service.Console getString


let logUpprcase (text: string) =
    reader {
        let! (log: ILogger) = Reader.ask
        let upper = text.ToUpper()
        log.Debug upper

    }

let mixit =
    reader {
        let! (io: IConsole, log: ILogger) = Reader.ask
        let text = Reader.run io getString
        Reader.run log (logUpprcase text)
        //do! logUpprcase (text)
        return text
    }

Reader.run (service.Console, service.Logger) mixit

let (<!!>) env f = Reader.run env f
let (<?>) env f = Reader.run env f

(service.Console, service.Logger) <!!> mixit


//again
let m2 =
    reader {
        let! (io: IConsole, log: ILogger) = Reader.ask
        let text = io <!!> getString
        log <!!> (logUpprcase text)
        //do! logUpprcase (text)
        return text
    }

(service.Console, service.Logger) <?> m2

//simple
let gString (io: IConsole) = io.ReadLine()

console <?> Reader gString
