namespace RB4

[<RequireQualifiedAccess>]
module Option =
    let fromTryTuple = function
        | false, _ -> None
        | true, result -> Some result
