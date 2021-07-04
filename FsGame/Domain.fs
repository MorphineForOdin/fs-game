namespace RB4.Domain

open RB4.QueueType

[<AutoOpen>]
module Domain = 
    // * Environment:
    type EnvironmentConfig = { GamePath: string }
    
    // * Game:
    type GameConfig = {
        ResourcesPath: string
        TokenStabilizationStep: float }
    type GameMenu = Start | Exit

    // * Combat tokens:
    type CombatTokenType =
        | Axe of byte
        | Spell of byte
        | Shield of byte
        | Nothing
    type CombatToken = {
        Type: CombatTokenType
        IsInitiative: bool }
    type CombatTokenMap = float * Map<bool, CombatToken>
    type CombatTokenState = {
        Token: CombatTokenMap
        State: CombatToken }

    // * Charackters:
    type Character = {
        Name: string
        Health: byte
        Tokens: CombatTokenMap list }
    type Hero = { Character: Character }
    type Monster = { Character: Character }
    type Player = {
        Name: string
        Hero: Hero }
        
    // TODO: Remove legacy combat menu DU.
    type Menu = Initiate | Skip

    // * Combat:
    type CombatAction =
        | PhysicalAttack of byte
        | MagicalAttack of byte
        | PassAction
    type CombatReaction =
        | Block of byte
        | PassReaction
    type CombatActionTrigger = CombatTokenState list -> CombatAction * CombatTokenState list
    type CombatResponseTrigger = CombatTokenState list -> CombatReaction * CombatTokenState list
    type CombatParticipantType = Player of Player | Monster of Monster
    type CombatParticipant = {
        Participant: CombatParticipantType
        TokensPool: CombatTokenState list
        CurrentHealth: byte
        RoundInitiative: byte
        ActionTrigger: CombatActionTrigger
        ResponseTrigger: CombatResponseTrigger }
    type CombatState = {
        Attacker: CombatParticipant
        Defender: CombatParticipant
        Winner: Character option
        Queue: CombatParticipant queue }