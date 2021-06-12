namespace RB4.Domain

[<AutoOpen>]
module Domain = 
    type CombatTokenType =
        | Axe of byte
        | Spell of byte
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
        
    type Menu = Initiate | Skip
    type CombatAction =
        | PhysicalAttack of byte
        | MagicalAttack of byte
        | Pass
    type CombatReaction =
        | Block of byte
        | Pass
    type CombatParticipant = Player of Player | Monster of Monster
    type CombatState = {
        Attacker: CombatParticipant
        AttackerTokens: CombatTokenMap list
        AttackerRoundTokens: CombatToken list
        Defender: CombatParticipant
        DefenderTokens: CombatTokenMap list
        DefenderRoundTokens: CombatToken list }

    module Game =
        type Config = {
            ResourcesPath: string
            TokenStabilizationStep: float }
        type Menu = Start | Exit

    module Environment =
        type Config = { GamePath: string }