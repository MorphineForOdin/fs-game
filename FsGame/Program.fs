open RB4.Engine

[<EntryPoint>]
let main argv =
    Game.start { GamePath = __SOURCE_DIRECTORY__ }
    0