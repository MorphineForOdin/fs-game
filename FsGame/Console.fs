namespace RB4.IO

open System
open RB4
open RB4.Operators
open RB4.Domain

module Console = 
    let startGame () =
        Console.Clear ()
        Console.ForegroundColor <- ConsoleColor.Magenta
        let welcomePath = __SOURCE_DIRECTORY__ +/ @"resources\welcome.txt"
        File.readLines welcomePath |> Seq.iter (fun line -> printfn "%s" line)
        Console.ForegroundColor <- ConsoleColor.Cyan
        printf "Start game? [Y]es|[N]o: "
        match Console.ReadLine () with
        | "Y" | "y" | "Yes" | "yes" | "YES" -> 
            Console.ResetColor ()
            true
        | _ -> false

    let printHero (hero: Hero) =
        printfn "HERO: %s" hero.Character.Name
        //let tokensS = getTokensString hero.Tokens
        //printfn "HERO: %s \t(H=%d; T=[%s])" hero.Name hero.Health (tokensS.Trim ())

    let rec readHero heroes =
        let heroInput = Console.ReadLine ()
        let heroOption = Int32.TryParse heroInput |> Option.fromTryTuple
        match heroOption with
        | Some heroIndex -> heroes |> Array.item heroIndex
        | _ ->
            printf "Not valid hero, try again: "
            readHero heroes

    let initPlayer (heroes: Hero array) =
        Console.Clear ()
        Console.ForegroundColor <- ConsoleColor.Magenta
        let welcomePath = __SOURCE_DIRECTORY__ +/ @"resources\logo.txt"
        File.readLines welcomePath |> Seq.iter (fun line -> printfn "%s" line)
        Console.ForegroundColor <- ConsoleColor.Cyan
        printf "Enter your name: "
        let playerName = Console.ReadLine ()
        printfn "Hi %s, please select hero from pool:" playerName
        heroes |> Array.iteri (fun i h -> printf "[%d] - " i; printHero h)
        let hero = readHero heroes
        printfn "Hey, %s! Hero %s is choosen." playerName hero.Character.Name
        { Name = playerName; Hero = hero }
    
    (*
    let getTokenActionString action =
        match action with
        | Damage n -> $"{n}D"
        | Shield n -> $"{n}S"
        | None -> "N"

    let getTokenInitiativeString initiative =
        match initiative with
        | true -> "+"
        | false -> "-"
        
    let getTokenSideString side =
        getTokenActionString side.Action + getTokenInitiativeString side.Initiative

    let getTokensSideString sides =
        sides |> List.fold (fun acc cur -> $"{acc} ({getTokenSideString cur})") ""

    let getTokenString token = 
        let sideA = getTokenSideString token.SideA
        let sideB = getTokenSideString token.SideB
        $"({sideA}|{sideB})"

    let getTokensString tokens =
        tokens |> List.fold (fun acc cur -> $"{acc} {getTokenString cur}") ""
    
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