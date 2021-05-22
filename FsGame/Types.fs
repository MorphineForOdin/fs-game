namespace RB4.Domain

[<AutoOpen>]
module Types = 
    type TokenAction =
        | Damage of byte
        | Shield of byte
        | None

    type TokenSide = {
        Action: TokenAction;
        Initiative: bool }

    type Token = {
        SideA: TokenSide;
        SideB: TokenSide }

    type Hero = {
        Name: string;
        Health: byte;
        Tokens: Token list }

    type Player = {
        Name: string
        Hero: Hero }

    type HeroCombatState = {
        Hero: Hero;
        Sides: TokenSide list }