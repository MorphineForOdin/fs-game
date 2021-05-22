open System

open RB4.Domain
open RB4.Data
open RB4.IO.Console
open RB4.Core.Logic

[<EntryPoint>]
let main argv =
    printfn "***** RB4 *****"
    printf "Enter your name: "
    let playerName = Console.ReadLine ()
    
    printfn "Hi %s, please select hero from pool:" playerName
    Static.heroes |> Array.iteri (fun i h -> printf "[%d] - " i; printHero h)
    let hero = readHero ()
    printfn "Hey, %s, hero %s is choosen!" playerName hero.Name
    let player = { Name = playerName; Hero = hero }

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
    Console.ReadKey () |> ignore
    0