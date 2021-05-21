open System
open Domain.Data
open Domain.Logic
open Domain.Console

[<EntryPoint>]
let main argv =
    printfn "***********\n*** RB3 ***\n***********"
    printf "Enter your name: "
    let playerName = Console.ReadLine ()
    printfn "Select hero:"
    printHero warrior
    printHero skeleton
    printf "Start combat? [Y-yes|N-no]: "
    let startCombat = Console.ReadLine ()
    match startCombat with
    | "Y" | "y" -> 
        Console.Clear ()
        let winner = start (warrior, skeleton)
        printfn "*** WINNER ***"
        printHero winner
    | _ -> printfn "*** BYE-BYE ***"
    Console.ReadKey () |> ignore
    0