namespace RB4.Domain

[<AutoOpen>]
module Domain = 
    type CombatTokenType =
        | PhysicalAttack of byte
        | MagicalAttack of byte
        | Shield of byte
        | Nothing
    type CombatToken = {
        Type: CombatTokenType
        Initiative: bool }
    type CombatTokenMap = float * Map<bool, CombatToken>
    type Character = {
        Name: string
        Health: byte
        Tokens: CombatTokenMap list }
    type Hero = { Character: Character }
    type Monster = { Character: Character }
    type Player = {
        Name: string
        Hero: Hero }
    
    module Combat =
        type Menu = Initiate | Skip
        type CombatAttackType = Physical | Magical
        type CombatAction =
            | Attack of byte * CombatAttackType
            | Pass
        type CombatReaction =
            | Block of byte
            | Pass

    module Game =
        type Config = {
            ResourcesPath: string
            TokenStabilizationStep: float }
        type Menu = Start | Exit

    module Environment =
        type Config = { GamePath: string }