namespace Domain

module Logic = 
    let randomGenerator = System.Random()
    let getRandomBool () = randomGenerator.NextDouble() >= 0.5

    let getRandomTokenSide token =
        match getRandomBool () with
        | true -> token.SideA
        | false -> token.SideB
    
    let throwTokens tokens =
        tokens |> List.map getRandomTokenSide

    let sumInitiative tokens =
        tokens |> List.sumBy (fun t -> if t.Initiative then 1 else 0)

    let rec start (attacker, defender) =
        match attacker.Health, defender.Health with
        | (0uy, _) -> defender
        | (_, 0uy) -> attacker
        | _ ->
            printfn "*** ROUND ***"
            
            let attackerTokens = throwTokens attacker.Tokens
            let attackerInitiative = sumInitiative attackerTokens
            Console.printStartRound attacker attackerTokens attackerInitiative
            
            let defenderTokens = throwTokens defender.Tokens
            let defenderInitiative = sumInitiative defenderTokens
            Console.printStartRound defender defenderTokens defenderInitiative

            let firstPlayer =
                if attackerInitiative >= defenderInitiative
                then attacker
                else defender
            printfn $"{firstPlayer.Name} is a First player."

            // TODO: Actions cycle...

            let hero1H = attacker.Health - 1uy;
            let hero2H = defender.Health - 1uy;
            printfn ""
            start ({attacker with Health = hero1H}, {defender with Health = hero2H})