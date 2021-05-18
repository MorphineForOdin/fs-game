open System
open Domain.Data
open Domain.Logic
open Domain.Console

[<EntryPoint>]
let main argv =
    printfn "*** RB3 ***"
    printHero warrior
    printHero skeleton
    printfn ""
    printfn "*** Start combat ***"
    
    let result = state
    printfn "%A" result
    0