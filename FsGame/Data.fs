namespace RB4.Data

open RB4.Domain

module Static =
    let tokenSide action initiative =
        { Action = action; Initiative = initiative }
    let positiveSide action = tokenSide action false
    let negativeSide action = tokenSide action true
    let token sideA sideB =
        { SideA = sideA; SideB = sideB }

    let hero1 = {
        Name = "Lord Hawthorne"
        Health = 9uy
        Tokens = [
            token (negativeSide (Damage 2uy)) (negativeSide (Shield 1uy))
            token (negativeSide (Damage 1uy)) (positiveSide None)
            token (negativeSide (Damage 1uy)) (negativeSide None) ] }
    let hero2 = {
        Name = "Master Thorn"
        Health = 8uy
        Tokens = [
            token (negativeSide (Damage 1uy)) (negativeSide (Shield 1uy))
            token (positiveSide (Damage 1uy)) (negativeSide None)
            token (negativeSide (Damage 1uy)) (positiveSide None) ] }
    let heroes = [| hero1; hero2 |]

    let skeleton = { 
        Name = "Skeleton"
        Health = 6uy
        Tokens = [
            token (negativeSide (Damage 1uy)) (positiveSide None)
            token (positiveSide (Damage 1uy)) (negativeSide (Shield 1uy))
            token (negativeSide (Damage 1uy)) (negativeSide (Shield 1uy)) ] }
    let warlock = { 
        Name = "Warlock"
        Health = 5uy
        Tokens = [
            token (negativeSide (Damage 2uy)) (positiveSide None)
            token (negativeSide (Damage 1uy)) (positiveSide None)
            token (negativeSide (Damage 1uy)) (positiveSide (Shield 1uy)) ] }
    let enemies = [| skeleton; warlock |]