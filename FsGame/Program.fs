open System
open RB4.Data
open RB4.IO

[<EntryPoint>]
let main argv =
    match Console.startGame () with
    | true ->
        Console.initPlayer Heroes.heroes |> ignore
    | false -> printfn "*** BYE-BYE ***"
    
    (*
    let randomIndex = getRandomInt Static.enemies.Length
    let randomEnemy = Static.enemies |> Array.item randomIndex
    printf "\nStart combat? [Y-yes|N-no]: "
    let startCombat = Console.ReadLine ()
    match startCombat with
    | "Y" | "y" -> 
        Console.Clear ()
        let winner = start player.Hero randomEnemy
        printfn "*** WINNER ***"
        printHero winner
    | _ -> printfn "*** BYE-BYE ***"
    *)

    Console.ResetColor ()
    Console.ReadKey () |> ignore
    0