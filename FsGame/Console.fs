namespace RB4.IO

open System
open RB4
open RB4.Domain

module Console = 
    let proceed statement =
        printf "%s [Y]es|[N]o: " statement
        match Console.ReadLine () with
        | "Y" | "y" -> true
        | _ -> false

    let printImage path =
        Console.Clear ()
        Console.ResetColor ()
        Console.ForegroundColor <- ConsoleColor.Magenta
        File.readLines path |> Seq.iter (fun line -> printfn "%s" line)
        Console.ForegroundColor <- ConsoleColor.Cyan

    let startGame welcomePath =
        printImage welcomePath
        proceed "Start game?"

    let getTokenActionString tokenSide =
        let actionString = 
            match tokenSide.Action with
            | PhysicalAttack amount -> $"{amount}P"
            | MagicalAttack amount -> $"{amount}M"
            | Shield amount -> $"{amount}S"
            | None -> "N"
        let initiativeString = 
            match tokenSide.Initiative with
            | true -> "+"
            | false -> "-"
        actionString + initiativeString

    let getTokenString token = 
        let sideA = Map.find true token |> getTokenActionString
        let sideB = Map.find false token |> getTokenActionString
        $"({sideA}|{sideB})"

    let getTokensString tokens =
        tokens |> List.fold (fun acc cur -> $"{acc}{getTokenString cur} ") " "

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

    let initPlayer logoPath (heroes: Hero array) =
        printImage logoPath
        printf "Enter your name: "
        let playerName = Console.ReadLine ()
        printfn "Hi %s, please select hero from pool:" playerName
        heroes |> Array.iteri (fun i h -> printf "[%d] - " i; printCharacter h.Character)
        let hero = readHero "To choose - type hero number: " heroes
        printfn "Hey, %s! Hero %s is choosen." playerName hero.Character.Name
        { Name = playerName; Hero = hero }
    
    (*
    let printStartRound (hero: Hero) sides initiative =
        let sidesStr = getTokensSideString sides
        printfn $"{hero.Name} H={hero.Health}; T:{sidesStr}; I={initiative}"
    
    let printAvailableActions sides =
        sides
        |> List.filter (fun s -> match s.Action with | Shield _ -> false | _ -> true)
        |> List.map (fun s -> match s.Action with | Damage _ -> "Damage [D]" | _ -> "")
        |> List.filter (fun s -> s <> "")
        |> List.append ["Pass [P]"]
        |> Seq.distinctBy (fun s -> s)
        |> List.ofSeq
        |> List.iter (fun s -> printfn "%s" s)
    *)