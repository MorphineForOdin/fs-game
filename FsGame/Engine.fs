namespace RB4.Engine

open RB4.Domain
open RB4.Operators
open RB4.IO
open RB4.Data
open RB4.Core

module Game =
    let configInit (envConfig: EnvironmentConfig) : GameConfig = {
        ResourcesPath = envConfig.GamePath +/ "resources"
        TokenStabilizationStep = 0.1 }
    
    let welcomeInit (config: GameConfig) =
        config.ResourcesPath +/ "welcome.txt" |> Console.printWelcome
    
    let startInit (config: GameConfig) =
        config.ResourcesPath +/ "logo.txt" |> Console.printImage
    
    let playerInit () = Console.initPlayer Heroes.heroes
    
    let combatInit = Console.printCombatStart
    
    let startCombat player =
        let randomIndex = Combat.getRandomInt Monsters.monsters.Length
        let randomEnemy = Monsters.monsters |> Array.item randomIndex
        let combatState =
            Combat.start
                Console.printCombatState
                (Player player)
                (Monster randomEnemy)
        printfn "*** WINNER ***" 
        combatState.Winner
        |> Option.map Console.printCharacter
        |> ignore
    
    let endGame = Console.printEnd
        
    let start (envConfig: EnvironmentConfig) =
        let gameConfig = configInit envConfig
        match welcomeInit gameConfig with
        | Exit -> endGame ()
        | Start ->
            startInit gameConfig
            let player = playerInit ()
            match combatInit () with
            | Skip -> endGame ()
            | Initiate -> 
                startCombat player
                endGame ()