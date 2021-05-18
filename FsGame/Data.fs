namespace Domain

module Data =

    let warrior = { 
        Name = "Warrior"
        Health = 10uy
        Tokens = [
            { 
                SideA = { Action = Damage 2uy; Initiative = false }
                SideB = { Action = None; Initiative = true }
            }; { 
                SideA = { Action = Damage 1uy; Initiative = false }
                SideB = { Action = Shield 1uy; Initiative = false }
            }; { 
                SideA = { Action = Damage 1uy; Initiative = false }
                SideB = { Action = None; Initiative = false }
            } ] }
            
    let skeleton = { 
        Name = "Skeleton"
        Health = 6uy
        Tokens = [
            { 
                SideA = { Action = Damage 1uy; Initiative = false }
                SideB = { Action = None; Initiative = true }
            }; { 
                SideA = { Action = Damage 1uy; Initiative = true }
                SideB = { Action = Shield 1uy; Initiative = false }
            }; { 
                SideA = { Action = Damage 1uy; Initiative = false }
                SideB = { Action = Shield 1uy; Initiative = false }
            } ] }