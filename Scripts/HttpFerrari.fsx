//Http stuff
#r "nuget: FSharp.Data"

open FSharp.Data

let url = "https://ecomm.ynap.biz/os/os1/wcs/resources/store/FERRARI_IT/storelocator/locations/countries?attr[PHYSICAL_STORE_ATTRIBUTE_SERVICES]=PHYSICAL_STORE_ATTRIBUTE_ANYWHERE_RETURN&langId="
  
Http.RequestString
  ( url, httpMethod = "GET",
    //query   = [ "api_key", apiKey; "query", "batman" ],
    headers = [ 
        "Accept", "application/json"
        "Accept-Encoding", "gzip"
        "X-AppName", "webSite-osPlatform/3.1"
        "X-Ubertoken", "v01.YZ6mpA0u/1EPj1Lxu5D6pL5WnfebjpqRNiT4iPI5Gk3mUUjNVR0qmP0Qz3qiSPPnoIjJk4u/Ie08BZ8lNi2j3V9JNAGntUGspi4nr2CeFXhC7BEH00zcgkV0Dq3hZnN5l2EUlCIvpEhicsOKJmM0nfEBV6j5Ckp6zr9WmdccBUZ7dHGA1LvN7sr/eaeb38M8kFXes2JJTumM15qfyf+goRTcE7P//R/iISqhkSfjrSIyW3HlitmWTmO2nD7Sga0K5Kbg0s4ew/3O4NSwy9S2MMV6iEi/OhDPFVJJ0ZZAeWh1PI7WHTck9Ce64jYM2T03OUFJu/yr2g/9a6ZCIoK0ckTiaP8mw5ItxotLn3w8B/l8++r/ZQ5PaAUIMcEfj9AQOMiwmRwMezCIUuRzehNBfaKPSlDIkqIDvpp00nmXSHgXDomcbolInHFCboc4TXM91qNezfGw558pYu1duqMw1jR/vxIyyKikwqbIctkpfL2KdRSzWH1k0Ir/Wybr342Xl78kAdzWXRdQZ07qMPt4LVGfb3ZXm6Wc5Yj0Waake6NQpDDV9azeh2WWHKYPBerTfjmWefKn/Hvz/ELbguNNbRU+cHgFdDADwyn5jyyn46sOP7hRNpDePZLKuAONRMlULFsSnT43oUklBg/KmbLn8Z6Lj7kT4sd5A6pKe7ufOggqpdSZvV2hVpZrlGUhnR1I2iH6BwFzV0Hr6lSWejwb0RAtn5pFzA3G9Kh70eRRYSNvGXLt3MtVQs0ndimLYzxQWBCgz6244wDnjRgKG3zI0M8S6BzNe5hqrxUyt90KUSlkW9gXHQ4mJZXGnpmUZwYfvBBMkNWF+EHX6CQarc6sLVI6wayrTu3s6kbeMWc0wV3MNtqsRQFQXCrGflHwV1yntDX5kmlmOfcbSi73iMdis9+qLbp68czH472RGPB1L4xfA+09ayDmx0MMe8HYnto37p97f6C8shzgcTuTeQ6q0NaFAzWv8YVCNRDJ2EM1k/5SIujvFsIUuiKIPiyBTdns05MOJS99xZIzzhhVBMbEa0gTLjgDr+IZtc4o0PTES67UdClNnuSsLFqDE3rvj7X4pomllCgaiw0uWQ8hDja2C/IuGr6tp6ojbSNXIr2pt8mqvApogZ2zu4wR9BsuFoi8WZ8ez0bV5tY5tTBS/cGmYANf7uXrIaGT2zljs0r+eu4=.UecgInhLBmQy9zJaz196qQbPbYM23ApNgX0tN4x7JRc="
        "X-IBM-Client-Id", "b03e4f63-7603-4c32-97c5-13698c469630"
        "X-IBM-Client-Secret", "uF0oK2lX1yP1fS0dS1kV1mK4aQ4dV1uQ0wD8rP0lQ2iY4dK6dH"
     ])

(* val it : string =
  "{"status":200,"data":[{"code":"IT","name":"Italia","cities":[{"name":"Bologna"},{"name":"Roma"},{"name":"Maranello"},{"name":"Settimo Torinese"}]}]}"     

 *)

let boutiques ="https://ecomm.ynap.biz/os/os1/wcs/resources/store/FERRARI_IT/storelocator/boutiques?pageSize=1000&countryCode=IT&attr[PHYSICAL_STORE_ATTRIBUTE_SERVICES]=PHYSICAL_STORE_ATTRIBUTE_ANYWHERE_RETURN&langId="

let wcs = 
Http.RequestString
  ( boutiques, httpMethod = "GET",
    //query   = [ "api_key", apiKey; "query", "batman" ],
    headers = [ 
        "Accept", "application/json"
        "Accept-Encoding", "gzip"
        "X-AppName", "webSite-osPlatform/3.1"
        "X-Ubertoken", "v01.YZ6mpA0u/1EPj1Lxu5D6pL5WnfebjpqRNiT4iPI5Gk3mUUjNVR0qmP0Qz3qiSPPnoIjJk4u/Ie08BZ8lNi2j3V9JNAGntUGspi4nr2CeFXhC7BEH00zcgkV0Dq3hZnN5l2EUlCIvpEhicsOKJmM0nfEBV6j5Ckp6zr9WmdccBUZ7dHGA1LvN7sr/eaeb38M8kFXes2JJTumM15qfyf+goRTcE7P//R/iISqhkSfjrSIyW3HlitmWTmO2nD7Sga0K5Kbg0s4ew/3O4NSwy9S2MMV6iEi/OhDPFVJJ0ZZAeWh1PI7WHTck9Ce64jYM2T03OUFJu/yr2g/9a6ZCIoK0ckTiaP8mw5ItxotLn3w8B/l8++r/ZQ5PaAUIMcEfj9AQOMiwmRwMezCIUuRzehNBfaKPSlDIkqIDvpp00nmXSHgXDomcbolInHFCboc4TXM91qNezfGw558pYu1duqMw1jR/vxIyyKikwqbIctkpfL2KdRSzWH1k0Ir/Wybr342Xl78kAdzWXRdQZ07qMPt4LVGfb3ZXm6Wc5Yj0Waake6NQpDDV9azeh2WWHKYPBerTfjmWefKn/Hvz/ELbguNNbRU+cHgFdDADwyn5jyyn46sOP7hRNpDePZLKuAONRMlULFsSnT43oUklBg/KmbLn8Z6Lj7kT4sd5A6pKe7ufOggqpdSZvV2hVpZrlGUhnR1I2iH6BwFzV0Hr6lSWejwb0RAtn5pFzA3G9Kh70eRRYSNvGXLt3MtVQs0ndimLYzxQWBCgz6244wDnjRgKG3zI0M8S6BzNe5hqrxUyt90KUSlkW9gXHQ4mJZXGnpmUZwYfvBBMkNWF+EHX6CQarc6sLVI6wayrTu3s6kbeMWc0wV3MNtqsRQFQXCrGflHwV1yntDX5kmlmOfcbSi73iMdis9+qLbp68czH472RGPB1L4xfA+09ayDmx0MMe8HYnto37p97f6C8shzgcTuTeQ6q0NaFAzWv8YVCNRDJ2EM1k/5SIujvFsIUuiKIPiyBTdns05MOJS99xZIzzhhVBMbEa0gTLjgDr+IZtc4o0PTES67UdClNnuSsLFqDE3rvj7X4pomllCgaiw0uWQ8hDja2C/IuGr6tp6ojbSNXIr2pt8mqvApogZ2zu4wR9BsuFoi8WZ8ez0bV5tY5tTBS/cGmYANf7uXrIaGT2zljs0r+eu4=.UecgInhLBmQy9zJaz196qQbPbYM23ApNgX0tN4x7JRc="
        "X-IBM-Client-Id", "b03e4f63-7603-4c32-97c5-13698c469630"
        "X-IBM-Client-Secret", "uF0oK2lX1yP1fS0dS1kV1mK4aQ4dV1uQ0wD8rP0lQ2iY4dK6dH"
     ])

printfn "%s" wcs


let boutiques ="https://ecomm.ynap.biz/os/os1/wcs/resources/store/FERRARI_IT/storelocator/boutiques"

let wcs = 
Http.RequestString
  ( boutiques, httpMethod = "GET",
    query   = [ 
      "pageSize", "1000"
      "countryCode", "IT"
      "attr[PHYSICAL_STORE_ATTRIBUTE_SERVICES]", "PHYSICAL_STORE_ATTRIBUTE_ANYWHERE_RETURN"
      "langId", ""],
    headers = [ 
        "Accept", "application/json"
        "Accept-Encoding", "gzip"
        "X-AppName", "webSite-osPlatform/3.1"
        "X-Ubertoken", "v01.YZ6mpA0u/1EPj1Lxu5D6pL5WnfebjpqRNiT4iPI5Gk3mUUjNVR0qmP0Qz3qiSPPnoIjJk4u/Ie08BZ8lNi2j3V9JNAGntUGspi4nr2CeFXhC7BEH00zcgkV0Dq3hZnN5l2EUlCIvpEhicsOKJmM0nfEBV6j5Ckp6zr9WmdccBUZ7dHGA1LvN7sr/eaeb38M8kFXes2JJTumM15qfyf+goRTcE7P//R/iISqhkSfjrSIyW3HlitmWTmO2nD7Sga0K5Kbg0s4ew/3O4NSwy9S2MMV6iEi/OhDPFVJJ0ZZAeWh1PI7WHTck9Ce64jYM2T03OUFJu/yr2g/9a6ZCIoK0ckTiaP8mw5ItxotLn3w8B/l8++r/ZQ5PaAUIMcEfj9AQOMiwmRwMezCIUuRzehNBfaKPSlDIkqIDvpp00nmXSHgXDomcbolInHFCboc4TXM91qNezfGw558pYu1duqMw1jR/vxIyyKikwqbIctkpfL2KdRSzWH1k0Ir/Wybr342Xl78kAdzWXRdQZ07qMPt4LVGfb3ZXm6Wc5Yj0Waake6NQpDDV9azeh2WWHKYPBerTfjmWefKn/Hvz/ELbguNNbRU+cHgFdDADwyn5jyyn46sOP7hRNpDePZLKuAONRMlULFsSnT43oUklBg/KmbLn8Z6Lj7kT4sd5A6pKe7ufOggqpdSZvV2hVpZrlGUhnR1I2iH6BwFzV0Hr6lSWejwb0RAtn5pFzA3G9Kh70eRRYSNvGXLt3MtVQs0ndimLYzxQWBCgz6244wDnjRgKG3zI0M8S6BzNe5hqrxUyt90KUSlkW9gXHQ4mJZXGnpmUZwYfvBBMkNWF+EHX6CQarc6sLVI6wayrTu3s6kbeMWc0wV3MNtqsRQFQXCrGflHwV1yntDX5kmlmOfcbSi73iMdis9+qLbp68czH472RGPB1L4xfA+09ayDmx0MMe8HYnto37p97f6C8shzgcTuTeQ6q0NaFAzWv8YVCNRDJ2EM1k/5SIujvFsIUuiKIPiyBTdns05MOJS99xZIzzhhVBMbEa0gTLjgDr+IZtc4o0PTES67UdClNnuSsLFqDE3rvj7X4pomllCgaiw0uWQ8hDja2C/IuGr6tp6ojbSNXIr2pt8mqvApogZ2zu4wR9BsuFoi8WZ8ez0bV5tY5tTBS/cGmYANf7uXrIaGT2zljs0r+eu4=.UecgInhLBmQy9zJaz196qQbPbYM23ApNgX0tN4x7JRc="
        "X-IBM-Client-Id", "b03e4f63-7603-4c32-97c5-13698c469630"
        "X-IBM-Client-Secret", "uF0oK2lX1yP1fS0dS1kV1mK4aQ4dV1uQ0wD8rP0lQ2iY4dK6dH"
     ])

printfn "%s" wcs


open System.IO

let sw = new StreamWriter(@"c:\nuovo.txt")

fprintf sw "%s" "ciao"
// use partial application to fix the TextWriter
//let print format = fprintf sw format

sw.Flush()
sw.Dispose()


mprint wcs

sw.Dispose()



//print on file
