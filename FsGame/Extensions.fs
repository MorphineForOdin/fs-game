namespace RB4

open System.IO

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
    let readLines (filePath: string) = 
        seq {
            use reader = File.OpenText filePath
            while not reader.EndOfStream
                do yield reader.ReadLine () }

module Operators =
    let (+/) parentPath siblingPath = Path.Combine(parentPath, siblingPath)