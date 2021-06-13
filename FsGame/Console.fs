namespace RB4.IO

open System
open System.Threading
open RB4
open RB4.Domain
open RB4.Core

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
            | Axe amount -> $"{amount}P"
            | Spell amount -> $"{amount}M"
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
        |> List.fold (fun acc (proc, map) -> $"{acc}{proc}={getTokenMapString map} ") " "

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
        | true -> Initiate
        | false -> Skip

    let getPrintState name health tokens roundT initiative =
        let tokensStr = getTokensString tokens
        let actionsString = roundT |> List.fold (fun acc cur -> $"{acc}{getTokenString cur} ") " "
        $"{name} H={health}; T=[{tokensStr}]; R=[{actionsString}]; I={initiative}"
    
    let printCombatState (state: CombatState) =
        let attackerState =
            getPrintState
                (Combat.getCharacter state.Attacker.Participant).Name
                state.Attacker.Health
                state.Attacker.Tokens
                state.Attacker.RoundTokens
                state.Attacker.Initiate
        printfn $"Attacker: {attackerState}"
        let defenderState =
            getPrintState
                (Combat.getCharacter state.Defender.Participant).Name
                state.Defender.Health
                state.Defender.Tokens
                state.Defender.RoundTokens
                state.Defender.Initiate
        printfn $"Defender: {defenderState}"
        printfn ""
        Console.ReadKey () |> ignore
        state