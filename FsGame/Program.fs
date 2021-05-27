open System
open RB4.Operators
open RB4.Data
open RB4.Core
open RB4.IO

[<EntryPoint>]
let main argv =
    Console.ReadKey () |> ignore
    let welcomePath = __SOURCE_DIRECTORY__ +/ "resources" +/ "welcome.txt"
    let logoPath = __SOURCE_DIRECTORY__ +/ "resources" +/ "logo.txt"
    match Console.startGame welcomePath with
    | true ->
        let player = Console.initPlayer logoPath Heroes.heroes
        match Console.proceed "Start combat?" with
            | true ->
                let randomIndex = Combat.getRandomInt Monsters.monsters.Length
                let randomEnemy = Monsters.monsters |> Array.item randomIndex
                let winner = Combat.start player.Hero randomEnemy
                printfn "*** WINNER ***"
                Console.printCharacter winner.Character
            | false -> printfn "Are you afraid, %s?" player.Name
        printfn "We would miss you %s ..." player.Name
    | false -> printfn "That's a big mistake! ..."
    
    printfn "*** BYE-BYE ***"
    Console.ResetColor ()
    Console.ReadKey () |> ignore
    0