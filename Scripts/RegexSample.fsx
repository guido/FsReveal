#r "nuget: FSharp.Text.RegexProvider"
//open System.Text.RegularExpressions
open FSharp.Text.RegexProvider

type PhoneRegex = Regex< @"(?<AreaCode>^\d{3})-(?<PhoneNumber>\d{3}-\d{4}$)" >

PhoneRegex().TypedMatch("425-123-2345")
    .AreaCode.Value

let m = PhoneRegex().TypedMatch("not really")



[<Literal>]
let color =
    @"(?<Red>^[0-9a-fA-F]{2})(?<Green>[0-9a-fA-F]{2})(?<Blue>[0-9a-fA-F]{2}$)" //@"(?<Red>^[0-9a-fA-F]{2})"

type ColorRegex = Regex< @"(?<Red>^[0-9a-fA-F]{2})(?<Green>[0-9a-fA-F]{2})(?<Blue>[0-9a-fA-F]{2}$)" >

let yellowish = ColorRegex().TypedMatch("AABB00")

sprintf $"RED {yellowish.Red.Value}, GREEN {yellowish.Green.Value}, BLUE {yellowish.Blue.Value}"



yellowish.Blue.Value

yellowish.Green

yellowish.Red
