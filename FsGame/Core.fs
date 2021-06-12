namespace RB4.Core

open RB4.Domain

module Combat = 
    let throwTokens state =
        let attackerTokens, attackerRounTokens = [], []
        let defenderTokens = []
        { state with
            AttackerRoundTokens = attackerTokens
            DefenderRoundTokens = defenderTokens }
    
    let calculateInitiative state = state
    let generateQueue state = state
    let rec tacticPhase state = state

    let rec round state =
        state
        |> throwTokens
        |> calculateInitiative
        |> generateQueue
        |> tacticPhase
        
    let start attacker defender =
        let getCharacter = function
            | Player player -> player.Hero.Character
            | Monster monster -> monster.Character

        round {
            Attacker = attacker
            AttackerTokens = (getCharacter attacker).Tokens
            AttackerRoundTokens = []
            Defender = defender
            DefenderTokens = (getCharacter defender).Tokens
            DefenderRoundTokens = [] }








    let getWinner (attacker, defender) = 
        match attacker.Health, defender.Health with
        | 0uy, _ -> Some defender
        | _, 0uy -> Some attacker
        | _ -> None

    let randomGenerator = System.Random()
    let getRandomBool step = randomGenerator.NextDouble() >= step
    let getRandomInt max = randomGenerator.Next max

    let throwTokens tokens =
        tokens |> List.map (fun (step, token) -> 
            let proc = getRandomBool step
            token |> Map.find proc)

    let sumInitiative tokens =
        tokens |> List.sumBy (fun t -> if t.Initiative then 1 else 0)

    let rec start printRound attacker defender =
        match getWinner (attacker, defender) with
        | Some winner -> winner
        | None ->
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
            start
                printRound
                {attacker with Health = hero1H}
                {defender with Health = hero2H}