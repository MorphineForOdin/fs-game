namespace Domain

open Data

module Logic = 
    let getRandomBool = System.Random().NextDouble() >= 0.5

    let getRandomTokenSide token =
        match getRandomBool with
        | true -> token.SideA
        | false -> token.SideB

    let startCombatRound hero1 hero2 =
        let hero1Tokens = hero1.Tokens |> List.map getRandomTokenSide
        let hero2Tokens = hero2.Tokens |> List.map getRandomTokenSide
        (hero1Tokens, hero2Tokens)

    let state = startCombatRound warrior skeleton