namespace Domain

module Console = 
    let getTokenActionString action =
        match action with
        | Damage n -> $"{n}D"
        | Shield n -> $"{n}S"
        | Agility -> "A"
        | None -> "N"
    
    let getTokenInitiativeString initiative =
        match initiative with
        | true -> "+"
        | false -> "-"
    
    let getTokenSideString side =
        getTokenActionString side.Action + getTokenInitiativeString side.Initiative

    let getTokenString token = 
        let sideA = getTokenSideString token.SideA
        let sideB = getTokenSideString token.SideB
        $"({sideA}|{sideB})"

    let getTokensString tokens =
        tokens
        |> List.fold (fun acc cur -> $"{acc} {getTokenString cur}") ""

    let printHero hero =
        let tokensS = getTokensString hero.Tokens
        printfn "HERO: %s \t(H=%d; T=[%s])" hero.Name hero.Health (tokensS.Trim ())