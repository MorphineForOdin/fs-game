namespace RB4.Engine

open RB4.Domain
open RB4.Operators
open RB4.IO
open RB4.Data
open RB4.Core

module Game =
    let configInit (envConfig: Environment.Config) : Game.Config = {
        ResourcesPath = envConfig.GamePath +/ "resources"
        TokenStabilizationStep = 0.1 }
    
    let welcomeInit (config: Game.Config) =
        config.ResourcesPath +/ "welcome.txt" |> Console.printWelcome
    
    let startInit (config: Game.Config) =
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
        
    let start (envConfig: Environment.Config) =
        let gameConfig = configInit envConfig
        match welcomeInit gameConfig with
        | Game.Exit -> endGame ()
        | Game.Start ->
            startInit gameConfig
            let player = playerInit ()
            match combatInit () with
            | Skip -> endGame ()
            | Initiate -> 
                startCombat player
                endGame ()