namespace RB4.Core

open RB4.Domain

module Combat = 
    let randomGenerator = System.Random()
    let getRandomBool step = randomGenerator.NextDouble() >= step
    let getRandomInt max = randomGenerator.Next max

    let getCharacter = function
        | Player player -> player.Hero.Character
        | Monster monster -> monster.Character
    let initTokensPool participant =
        (getCharacter participant).Tokens
        |> List.map (fun token -> {
            Token = token
            State = { Type = Nothing; IsInitiative = false }})

    let hasWinner (state: CombatState) =
        match state.Attacker.CurrentHealth, state.Defender.CurrentHealth with
        | 0uy, _ -> Some (getCharacter state.Defender.Participant)
        | _, 0uy -> Some (getCharacter state.Attacker.Participant)
        | _ -> None

    let updateProc step proc = function
        | true -> proc + step
        | false -> proc - step
    let procToken (proc, map) =
        let side = getRandomBool proc
        let newProc = updateProc 0.1 proc side
        let tokenState = map |> Map.find side
        let updatedTokenMap = (newProc, map)
        { Token = updatedTokenMap; State = tokenState }
    let procTokens (tokens: CombatTokenState list) =
        tokens
        |> List.map (fun t -> procToken t.Token)
    let throwTokens state =
        let attackerTokens = procTokens state.Attacker.TokensPool
        let defenderTokens = procTokens state.Defender.TokensPool
        let attacker = { state.Attacker with TokensPool = attackerTokens }
        let defender = { state.Defender with TokensPool = defenderTokens }
        { state with Attacker = attacker; Defender = defender }

    let getInitiative tokens =
        tokens
        |> List.map (fun s -> s.State)
        |> List.sumBy (fun t -> if t.IsInitiative then 1uy else 0uy)
    let calculateInitiative state =
        let attackerInit = getInitiative state.Attacker.TokensPool
        let defenderInit = getInitiative state.Defender.TokensPool
        let attacker = { state.Attacker with RoundInitiative = attackerInit }
        let defender = { state.Defender with RoundInitiative = defenderInit }
        { state with Attacker = attacker; Defender = defender }

    let generateQueue state = 
        let first, last =
            if state.Attacker.RoundInitiative >= state.Defender.RoundInitiative
            then state.Attacker, state.Defender
            else state.Defender, state.Attacker
        { state with Queue = [first; last] }

    let rec tacticPhase state = state

    let rec round printState state =
        match hasWinner state with
        | Some char -> { state with Winner = Some char }
        | None ->
            state
            |> throwTokens
            |> calculateInitiative
            |> (generateQueue >> printState)
            |> tacticPhase
            |> round printState 
        
    let start printState attacker defender =
        round printState {
            Attacker = {
                Participant = attacker
                TokensPool = initTokensPool attacker
                CurrentHealth = (getCharacter attacker).Health
                RoundInitiative = 0uy }
            Defender = {
                Participant = defender
                TokensPool = initTokensPool defender
                CurrentHealth = (getCharacter defender).Health
                RoundInitiative = 0uy }
            Winner = None
            Queue = [] }