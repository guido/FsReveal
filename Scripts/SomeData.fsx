#r "nuget: Fsharp.Data"


//Prima Regexp in caso la rete no vada



//remeber  VPN on
// dotnet nuget list source
// dotnet nuget disable source "YOOX stable"
//disbilitare

[<Literal>]
let countriesUrl =
    "http://api.yoox.net/Geo.API/1.0/country/MONTBLANC.json?isCommerce=True"

open FSharp.Data

type countryData = JsonProvider<countriesUrl>

let coutriesResult = countryData.Load(countriesUrl)


coutriesResult.Countries
|> Array.map (fun c -> (c.Name, c.CurrencySign))



//moving to async

countryData.AsyncLoad(countriesUrl)
|> Async.RunSynchronously


let cd =
    countryData.AsyncLoad(countriesUrl)
    |> Async.RunSynchronously


//this is a computation Expression
let download =
    async {
        let! data = countryData.AsyncLoad(countriesUrl)
        return data
    }

download |> Async.RunSynchronously


let printDownload =
    async {
        let! data = countryData.AsyncLoad(countriesUrl)

        data.Countries
        |> Array.iter (fun c -> printfn "%s" c.Name)

        return ()
    }

printDownload |> Async.RunSynchronously
