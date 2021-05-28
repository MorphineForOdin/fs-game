namespace RB4.Core

open RB4.Domain

module Combat = 
    let randomGenerator = System.Random()
    let getRandomBool () = randomGenerator.NextDouble() >= 0.5
    let getRandomInt max = randomGenerator.Next max

    let throwTokens tokens =
        tokens |> List.map (fun t -> Map.find (getRandomBool ()) t)

    let sumInitiative tokens =
        tokens |> List.sumBy (fun t -> if t.Initiative then 1 else 0)
    
    let rec start printRound attacker defender =
        match attacker.Health, defender.Health with
        | (0uy, _) -> defender
        | (_, 0uy) -> attacker
        | _ ->
            printfn "*** ROUND ***"
            
            let attackerTokens = throwTokens attacker.Tokens
            let attackerInitiative = sumInitiative attackerTokens
            printRound attacker attackerTokens attackerInitiative
            
            let defenderTokens = throwTokens defender.Tokens
            let defenderInitiative = sumInitiative defenderTokens
            printRound defender defenderTokens defenderInitiative

            let firstPlayer, firstTokens =
                if attackerInitiative >= defenderInitiative
                then attacker, attackerTokens
                else defender, defenderTokens
            printfn $"{firstPlayer.Name} is a First player."

            // TODO: Actions cycle...
            printfn "Choose action: "
            //Console.printAvailableActions firstPlayer.Sides
            
            let hero1H = attacker.Health - 1uy;
            let hero2H = defender.Health - 1uy;
            printfn ""
            start printRound {attacker with Health = hero1H} {defender with Health = hero2H}