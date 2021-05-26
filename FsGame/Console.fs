namespace RB4.IO

open System
open System.IO
open RB4.Domain

module Console = 
    let readLines (filePath: string) = 
        seq {
            use reader = File.OpenText filePath
            while not reader.EndOfStream
                do yield reader.ReadLine () }

    let printWelcome () =
        Console.ForegroundColor <- ConsoleColor.Cyan
        readLines @"resources/welcome.txt"
        |> Seq.iter (fun line -> printfn "%s" line)
        Console.ResetColor ()

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

    let printHero hero =
        let tokensS = getTokensString hero.Tokens
        printfn "HERO: %s \t(H=%d; T=[%s])" hero.Name hero.Health (tokensS.Trim ())
    
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