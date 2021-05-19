open Domain.Data
open Domain.Logic
open Domain.Console

[<EntryPoint>]
let main argv =
    printfn "*** RB3 ***"
    printHero warrior
    printHero skeleton
    printfn ""
    let winner = start (warrior, skeleton)
    printfn "*** WINNER ***"
    printHero winner
    0