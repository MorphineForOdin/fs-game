namespace Domain

[<AutoOpen>]
module DomainTypes = 

    type TokenAction =
        | Damage of byte
        | Shield of byte
        | Agility
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