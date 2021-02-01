(*             foreach (string fileName in fileNames)
            {
                LabelsResponseDto result = apiHttpClient.GetAsync<LabelsResponseDto>($"{context.Esite}/labels/ytos.{context.Device}/{request.LabelsContext}/{request.FolderName}/{fileName.ToLowerInvariant()}.json?languageId={languageId}")
                    .ConfigureAwait(false).GetAwaiter().GetResult();
                response.Add(result);
            } *)

#r "nuget: FSharp.Data"

#r @"C:\Dev\corefront-netcore\src\Widgets\YNAP.Corefront.Widget.Common\bin\Debug\net5.0\YNAP.Corefront.Widget.Common.dll"

#r @"C:\Dev\corefront-netcore\src\Core\Application\YNAP.Corefront.Application.Abstractions\bin\Debug\net5.0\YNAP.Corefront.Application.Abstractions.dll"

open YNAP.Corefront.Widget.Common.Localization
open YNAP.Corefront.Application.Abstractions.Domains.Localization

open System


type CfContext =
    { Division: string
      Isocode: string
      Device: string
      LangId: int }
    member this.Esite = $"{this.Division}_{this.Isocode}"


let context =
    { Division = "VALENTINO"
      Isocode = "US"
      Device = "desktop"
      LangId = 19 }

//context.Esite
(*   "APIs": {
    "legacyCartUrl": "https://api.yoox.net/Cart.API/1.6/",
    "geoApiUrl": "https://api.yoox.net/Geo.API/v2/",
    "cmsApiUrl": "https://api.yoox.net/Cms.API/1.1/",
    "wcsApiUrl": "https://nonprod.ynap.biz/api/int04/",
    "coremediaApiUrl": "http://lb.pcae.cms01.int4.ewe1.aws.dev.e-comm/",
    "gpsApi": "https://cards.int.gps.ynap.com/"
  },
  "Secrets": {
    "X-IBM-Client-Id": "8eaacd90-4582-4455-b5de-d4ec2d824251",
    "X-IBM-Client-Secret": "jX1tK8gB4lQ7uK6cI0rU4qX7iT4aU2mD4jO7cD4lX7dJ1uV6qQ"
  }, *)
//$"{context.Esite}/labels/ytos.{context.Device}/{request.LabelsContext}/{request.FolderName}/{fileName.ToLowerInvariant()}.json?languageId={languageId}")

let cartLabel = CartDomain()

(* cartLabel.LabelsContext
cartLabel.Folder
cartLabel.FileNames *)


(*
$"{context.Esite}/labels/ytos.{context.Device}/{cartlabel.LabelsContext}/{cartlabel.FolderName}/{
                                                                                                     fileName.ToLowerInvariant
                                                                                                         ()
}.json?languageId={languageId}"

let verboseurl (root: string) (ctx: CfContext) (label: ILabelsDomain) =
    label.FileNames
    |> Seq.map
        (fun name ->
            $"{root}/%s{ctx.Esite}/labels/ytos.{ctx.Device}/{label.LabelsContext}/{label.Folder}/{name.ToLowerInvariant()}.json?languageId={
                                                                                                                                               ctx.LangId
            }")
 *)

let url root (ctx: CfContext) (label: ILabelsDomain) =
    label.FileNames
    |> Seq.map
        (fun name ->
            $"{root}%s{ctx.Esite}/labels/ytos.{ctx.Device}/{label.LabelsContext}/{label.Folder}/{name}.json?languageId={
                                                                                                                            ctx.LangId
            }")

let cmsApiUrl = "https://api.yoox.net/Cms.API/1.1/"

open FSharp.Data

let itemLabel = ItemDomain()

#time

url cmsApiUrl context cartLabel
|> Seq.toArray
|> Array.map Http.AsyncRequestString
|> Async.Parallel
|> Async.RunSynchronously

#time

type lbl =
    JsonProvider<"https://api.yoox.net/Cms.API/1.1/VALENTINO_US/labels/ytos.desktop/plugins/cart/accessible.json?languageId=19">

#time

url cmsApiUrl context itemLabel
|> Seq.toArray
|> Array.map lbl.AsyncLoad
|> Async.Parallel
|> Async.RunSynchronously

#time


#time

let all =
    async {
        let! labels =
            url cmsApiUrl context itemLabel
            |> Seq.toArray
            |> Array.map lbl.AsyncLoad
            |> Async.Parallel

        return labels |> Array.collect (fun l -> l.Labels)

    }
    |> Async.RunSynchronously

let labelMap =
    all
    |> Array.map (fun x -> (x.Key, x.Value))
    |> Map.ofArray

#time

labelMap.Count
labelMap.["fallWinter17"]
labelMap.["addToWishList_btn"]
