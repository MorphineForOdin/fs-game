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
            |> Seq.iter (fun line -> printfn "%s" line; Thread.Sleep 50)
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
        | true -> Start
        | false -> Exit
    
    let printEnd () =
        printfn "*** BYE-BYE ***"
        Console.ResetColor ()
        Console.ReadKey () |> ignore
        ()

    let getTokenTypeString tokenSide =
        let actionString = 
            match tokenSide.Type with
            | Axe amount -> $"{amount}P"
            | Spell amount -> $"{amount}M"
            | Shield amount -> $"{amount}S"
            | Nothing -> " N"
        let initiativeString = 
            match tokenSide.IsInitiative with
            | true -> "+"
            | false -> "-"
        actionString + initiativeString

    let getTokenMapString token = 
        let sideA = Map.find true token |> getTokenTypeString
        let sideB = Map.find false token |> getTokenTypeString
        $"({sideA}|{sideB})"
    
    let getTokenString (proc, map) =
        sprintf "%.1f=%s " proc (getTokenMapString map)

    let getTokensString (tokens: CombatTokenMap list) =
        tokens
        |> List.fold (fun acc token -> $"{acc}{getTokenString token} ") " "
        |> String.trim

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

    let getPrintState name health tokens initiative =
        let tokensString =
            tokens
            |> List.map (fun ts ->
                let tokenStr = getTokenString ts.Token |> String.trim
                let stateStr = getTokenTypeString ts.State
                $"({stateStr})--{tokenStr}")
            |> List.reduce (fun acc str -> $"{acc} {str}")
        $"%-15s{name} H={health};\tT=[{tokensString}];\tI={initiative}"
    
    let printCombatState (state: CombatState) =
        let attackerState =
            getPrintState
                (Combat.getCharacter state.Attacker.Participant).Name
                state.Attacker.CurrentHealth
                state.Attacker.TokensPool
                state.Attacker.RoundInitiative
        printfn $"Attacker: {attackerState}"
        let defenderState =
            getPrintState
                (Combat.getCharacter state.Defender.Participant).Name
                state.Defender.CurrentHealth
                state.Defender.TokensPool
                state.Defender.RoundInitiative
        printfn $"Defender: {defenderState}"
        printfn ""
        Console.ReadKey () |> ignore
        state