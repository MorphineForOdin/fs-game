namespace RB4.Core

open RB4.QueueType
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
        let queue =
            Queue.empty
            |> Queue.enqueue first
            |> Queue.enqueue last
        { state with Queue = queue }

    let getActionFromToken = function
        | Axe damage -> PhysicalAttack damage
        | Spell damage -> MagicalAttack damage
        | _ -> PassAction
    let getReactionFromToken = function
        | Shield amount -> Block amount
        | _ -> PassReaction
    let rec tacticPhase printState state =
        match hasWinner state with
        | Some char -> { state with Winner = Some char }
        | None ->
            match Queue.isEmpty state.Queue with
            | true -> state
            | false ->
                let first, queue = Queue.dequeue state.Queue
                let action, tokens = first.ActionTrigger first.TokensPool
                // TODO: Handle active player action.
                let updatedPlayer = { first with TokensPool = tokens }
                let updatedQueue =
                    match tokens with
                    | [] -> queue
                    | _ -> queue |> Queue.enqueue updatedPlayer
                printState state |> ignore
                tacticPhase printState { state with Queue = updatedQueue }

    let rec round printState state =
        match hasWinner state with
        | Some char -> { state with Winner = Some char }
        | None ->
            state
            |> throwTokens
            |> calculateInitiative
            |> (generateQueue >> printState)
            |> tacticPhase printState
            |> round printState 
        
    let start
        printState
        (attacker, attackerActionTrigger, attackerResponseTrigger)
        (defender, defenderActionTrigger, defenderResponseTrigger) =
        round printState {
            Attacker = {
                Participant = attacker
                TokensPool = initTokensPool attacker
                CurrentHealth = (getCharacter attacker).Health
                RoundInitiative = 0uy
                ActionTrigger = attackerActionTrigger
                ResponseTrigger = attackerResponseTrigger }
            Defender = {
                Participant = defender
                TokensPool = initTokensPool defender
                CurrentHealth = (getCharacter defender).Health
                RoundInitiative = 0uy
                ActionTrigger = defenderActionTrigger
                ResponseTrigger = defenderResponseTrigger }
            Winner = None
            Queue = Queue.empty }