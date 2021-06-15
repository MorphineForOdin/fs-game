namespace RB4.Core

open RB4.Domain

module Combat = 
    let randomGenerator = System.Random()
    let getRandomBool step = randomGenerator.NextDouble() >= step
    let getRandomInt max = randomGenerator.Next max

    let getCharacter = function
        | Player player -> player.Hero.Character
        | Monster monster -> monster.Character
    let hasWinner (state: CombatState) =
        match state.Attacker.Health, state.Defender.Health with
        | 0uy, _ -> Some (getCharacter state.Defender.Participant)
        | _, 0uy -> Some (getCharacter state.Attacker.Participant)
        | _ -> None

    let updateProc step proc = function
        | true -> proc + step
        | false -> proc - step
    let procToken (proc, map) =
        let side = getRandomBool proc
        let newProc = updateProc 0.1 proc side
        let token = map |> Map.find side
        let newMap = (newProc, map)
        (newMap, token)
    let getTokens tokens =
        tokens
        |> List.map procToken
        |> List.unzip
    let throwTokens state =
        let aT, aRT = getTokens state.Attacker.Tokens
        let dT, dRT = getTokens state.Defender.Tokens
        let attacker = { state.Attacker with Tokens = aT; RoundTokens = aRT }
        let defender = { state.Defender with Tokens = dT; RoundTokens = dRT }
        { state with Attacker = attacker; Defender = defender }

    let getInitiative tokens =
        tokens |> List.sumBy (fun t -> if t.IsInitiative then 1uy else 0uy)
    let calculateInitiative state =
        let aInit = getInitiative state.Attacker.RoundTokens
        let dInit = getInitiative state.Defender.RoundTokens
        let attacker = { state.Attacker with Initiative = aInit }
        let defender = { state.Defender with Initiative = dInit }
        { state with Attacker = attacker; Defender = defender }

    let generateQueue state = 
        let first, last =
            if state.Attacker.Initiative >= state.Defender.Initiative
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
                Tokens = (getCharacter attacker).Tokens
                Health = (getCharacter attacker).Health
                RoundTokens = []
                Initiative = 0uy }
            Defender = {
                Participant = defender
                Tokens = (getCharacter defender).Tokens
                Health = (getCharacter defender).Health
                RoundTokens = []
                Initiative = 0uy }
            Winner = None
            Queue = [] }