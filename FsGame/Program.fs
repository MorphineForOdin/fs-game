open System
open Domain
open Domain.Logic

[<EntryPoint>]
let main argv =
    let result = state
    printfn "%A" result
    0