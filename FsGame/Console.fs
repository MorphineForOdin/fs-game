namespace RB4.IO

open System
open System.Threading
open RB4
open RB4.Domain

module Console = 
    let printImage path =
        Console.Clear ()
        Console.ResetColor ()
        Console.ForegroundColor <- ConsoleColor.Magenta
        File.readLines path
            |> Seq.iter (fun line -> printfn "%s" line; Thread.Sleep 100)
        Console.ForegroundColor <- ConsoleColor.Cyan
    
    let proceed statement =
        printf "%s [Y]es|[N]o: " statement
        match Console.ReadLine () with
        | "Y" | "y" -> true
        | _ -> false

    let printWelcome welcomeImagePath =
        printfn "... LOADING ..."
        Console.ReadKey () |> ignore
        printImage welcomeImagePath
        match proceed "Start game?" with
        | true -> Game.Start
        | false -> Game.Exit
    
    let printEnd () =
        printfn "*** BYE-BYE ***"
        Console.ResetColor ()
        Console.ReadKey () |> ignore
        ()

    let getTokenString tokenSide =
        let actionString = 
            match tokenSide.Type with
            | PhysicalAttack amount -> $"{amount}P"
            | MagicalAttack amount -> $"{amount}M"
            | Shield amount -> $"{amount}S"
            | Nothing -> "N"
        let initiativeString = 
            match tokenSide.Initiative with
            | true -> "+"
            | false -> "-"
        actionString + initiativeString

    let getTokenMapString token = 
        let sideA = Map.find true token |> getTokenString
        let sideB = Map.find false token |> getTokenString
        $"({sideA}|{sideB})"

    let getTokensString (tokens: CombatTokenMap list) =
        tokens
        |> List.map (fun (step, map) -> map)
        |> List.fold (fun acc cur -> $"{acc}{getTokenMapString cur} ") " "

    let printCharacter (character: Character) =
        let tokensStr = getTokensString character.Tokens
        printfn "HERO: %s \tH=%d; T=[%s];" character.Name character.Health tokensStr

    let rec readHero message heroes =
        printf "%s" message
        let heroInput = Console.ReadLine ()
        let heroOption = Int32.TryParse heroInput |> Option.fromTryTuple
        match heroOption with
        | Some heroIndex when heroIndex < Array.length heroes  
            -> heroes |> Array.item heroIndex
        | Some _ -> readHero "Out of range hero, try again: " heroes
        | _ -> readHero "Not valid hero, try again: " heroes

    let initPlayer (heroes: Hero array) =
        printf "Enter your name: "
        let playerName = Console.ReadLine ()
        printfn "Hi %s, please select hero from pool:" playerName
        heroes |> Array.iteri (fun i h -> printf "[%d] - " i; printCharacter h.Character)
        let hero = readHero "To choose - type hero number: " heroes
        printfn "Hey, %s! Hero %s is choosen." playerName hero.Character.Name
        { Name = playerName; Hero = hero }
    
    let printCombatStart () =
        match proceed "Start combat?" with
        | true -> Combat.Initiate
        | false -> Combat.Skip

    let printStartRound (character: Character) actions (initiative: int) =
        let actionsString = 
            actions |> List.fold (fun acc cur -> $"{acc}{getTokenString cur} ") " "
        printfn $"{character.Name} H={character.Health}; T:{actionsString}; I={initiative}"