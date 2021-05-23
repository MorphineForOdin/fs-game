namespace RB4

[<RequireQualifiedAccess>]
module Option =
    let fromTryTuple = function
        | false, _ -> None
        | true, result -> Some result

module Map =
    let initFromList pairs =
        pairs
        |> List.fold (fun m (key, value) -> Map.add key value m) Map.empty