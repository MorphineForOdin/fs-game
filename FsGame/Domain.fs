namespace RB4.Domain

[<AutoOpen>]
module Types = 
    type CombatTokenActionType =
        | PhysicalAttack of byte
        | MagicalAttack of byte
        | Shield of byte
        | Nothing
    type CombatTokenAction = {
        Action: CombatTokenActionType
        Initiative: bool }
    type CombatToken = Map<bool, CombatTokenAction>
    type Character = {
        Name: string
        Health: byte
        Tokens: CombatToken list }
    type Hero = { Character: Character }
    type Monster = { Character: Character }
    type Player = {
        Name: string
        Hero: Hero }
    
    type CombatAttackType = Physic | Magic
    type CombatActionType = Attack of byte * CombatAttackType
    type CombatReactionType = Block of byte
    type CombatAction =
        | Action of CombatActionType
        | Reaction of CombatReactionType
        | Pass
    
    type GetWinner = Character * Character -> Character option
    type ThrowTokens = CombatToken list -> CombatTokenAction list
    type CalculateInitiative = CombatTokenAction list -> byte