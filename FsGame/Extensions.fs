namespace RB4

[<RequireQualifiedAccess>]
module String =
    open System
    let trim (input: string) =
        match input with
        | null -> String.Empty
        | _ -> input.Trim()
    let split delimiter (input: string) =
        match input with
        | null -> [||]
        | _ -> input.Split [| delimiter |]

[<RequireQualifiedAccess>]
module Array =
    let remove x (xs: 'a array) =
        match Array.tryFindIndex ((=) x) xs with
        | Some 0 -> xs.[1..]
        | Some i -> Array.append xs.[..i-1] xs.[i+1..]
        | None -> xs
    let removei i (xs: 'a array) =
        if i < xs.Length
        then Array.append xs.[..i-1] xs.[i+1..]
        else xs
    let removeil i (xs: 'a array) =
        if i < xs.Length
        then (xs.[..i-1] |> Array.toList) @ (xs.[i+1..] |> Array.toList)
        else xs |> Array.toList

[<RequireQualifiedAccess>]
module Option =
    let fromTryTuple = function
        | false, _ -> None
        | true, result -> Some result

[<RequireQualifiedAccess>]
module Map =
    let initFromList pairs =
        pairs |> List.fold
            (fun map (key, value) -> Map.add key value map)
            Map.empty

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

module QueueType =
    type queue<'a> =
        | Queue of 'a list * 'a list
    
    [<RequireQualifiedAccess>]
    module Queue =
        let empty = Queue ([], [])
        let isEmpty = function
            | Queue ([], []) -> true
            | _ -> false
        let enqueue e = function
            | Queue (fs, bs) -> Queue (e :: fs, bs)
        let dequeue = function
            | Queue ([], []) -> failwith "Empty queue!"
            | Queue (fs, b :: bs) -> b, Queue (fs, bs)
            | Queue (fs, []) -> 
                let bs = List.rev fs
                bs.Head, Queue ([], bs.Tail)

module Operators =
    open System.IO
    let (+/) parentPath siblingPath =
        Path.Combine(parentPath, siblingPath)