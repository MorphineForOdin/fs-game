namespace RB4.Data

open RB4
open RB4.Domain

module Heroes =
    let lordHawthorne: Hero = {
        Character = {
            Name = "Lord Hawthorne"
            Health = 9uy
            Tokens = [
                Map.initFromList [
                    true, { Action = PhysicalAttack 2uy; Initiative = false }
                    false, { Action = Shield 1uy; Initiative = false } ]
                Map.initFromList [
                    true, { Action = PhysicalAttack 1uy; Initiative = false }
                    false, { Action = None; Initiative = true } ]
                Map.initFromList [
                    true, { Action = PhysicalAttack 1uy; Initiative = false }
                    false, { Action = None; Initiative = false } ] ] } }
    let masterThorn: Hero = {
        Character = {
            Name = "Master Thorn"
            Health = 8uy
            Tokens = [
                Map.initFromList [
                    true, { Action = MagicalAttack 1uy; Initiative = false }
                    false, { Action = Shield 1uy; Initiative = false } ]
                Map.initFromList [
                    true, { Action = MagicalAttack 1uy; Initiative = true }
                    false, { Action = None; Initiative = false } ]
                Map.initFromList [
                    true, { Action = MagicalAttack 1uy; Initiative = false }
                    false, { Action = None; Initiative = true } ] ] } }
    let heroes = [| lordHawthorne; masterThorn |]

module Monsters =
    let skeleton : Monster = {
        Character = {
            Name = "Skeleton"
            Health = 6uy
            Tokens = [
                Map.initFromList [
                    true, { Action = PhysicalAttack 1uy; Initiative = false }
                    false, { Action = None; Initiative = true } ]
                Map.initFromList [
                    true, { Action = PhysicalAttack 1uy; Initiative = true }
                    false, { Action = Shield 1uy; Initiative = false } ]
                Map.initFromList [
                    true, { Action = PhysicalAttack 1uy; Initiative = false }
                    false, { Action = Shield 1uy; Initiative = false } ] ] } }
    let warlock : Monster = {
        Character = {
            Name = "Warlock"
            Health = 5uy
            Tokens = [
                Map.initFromList [
                    true, { Action = MagicalAttack 2uy; Initiative = false }
                    false, { Action = None; Initiative = true } ]
                Map.initFromList [
                    true, { Action = MagicalAttack 1uy; Initiative = false }
                    false, { Action = None; Initiative = true } ]
                Map.initFromList [
                    true, { Action = MagicalAttack 1uy; Initiative = false }
                    false, { Action = Shield 1uy; Initiative = true } ] ] } }
    let monsters = [| skeleton; warlock |]