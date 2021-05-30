namespace RB4

[<RequireQualifiedAccess>]
module Option =
    let fromTryTuple = function
        | false, _ -> None
        | true, result -> Some result

[<RequireQualifiedAccess>]
module Map =
    let initFromList pairs =
        pairs |> List.fold (fun m (key, value) -> Map.add key value m) Map.empty

[<RequireQualifiedAccess>]
module File =
    open System.IO
    let readLines (filePath: string) = 
        seq {
            use reader = File.OpenText filePath
            while not reader.EndOfStream
                do yield reader.ReadLine () }

[<RequireQualifiedAccess>]
module Result =
    let fromOption error = function
        | Some value -> Ok value
        | None -> Error error

module Operators =
    open System.IO
    let (+/) parentPath siblingPath = Path.Combine(parentPath, siblingPath)