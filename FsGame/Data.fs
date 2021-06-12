namespace RB4.Data

open RB4
open RB4.Domain

module Heroes =
    let lordHawthorne: Hero = {
        Character = {
            Name = "Lord Hawthorne"
            Health = 9uy
            Tokens = [
                (0.5, Map.initFromList [
                    true, { Type = Axe 2uy; Initiative = false }
                    false, { Type = Shield 1uy; Initiative = false } ])
                (0.5, Map.initFromList [
                    true, { Type = Axe 1uy; Initiative = false }
                    false, { Type = Nothing; Initiative = true } ])
                (0.5, Map.initFromList [
                    true, { Type = Axe 1uy; Initiative = false }
                    false, { Type = Nothing; Initiative = false } ]) ] } }
    let masterThorn: Hero = {
        Character = {
            Name = "Master Thorn"
            Health = 8uy
            Tokens = [
                (0.5, Map.initFromList [
                    true, { Type = Spell 1uy; Initiative = false }
                    false, { Type = Shield 1uy; Initiative = false } ])
                (0.5, Map.initFromList [
                    true, { Type = Spell 1uy; Initiative = true }
                    false, { Type = Nothing; Initiative = false } ])
                (0.5, Map.initFromList [
                    true, { Type = Spell 1uy; Initiative = false }
                    false, { Type = Nothing; Initiative = true } ]) ] } }
    let heroes = [| lordHawthorne; masterThorn |]

module Monsters =
    let skeleton : Monster = {
        Character = {
            Name = "Skeleton"
            Health = 6uy
            Tokens = [
                (0.5, Map.initFromList [
                    true, { Type = Axe 1uy; Initiative = false }
                    false, { Type = Nothing; Initiative = true } ])
                (0.5, Map.initFromList [
                    true, { Type = Axe 1uy; Initiative = true }
                    false, { Type = Shield 1uy; Initiative = false } ])
                (0.5, Map.initFromList [
                    true, { Type = Axe 1uy; Initiative = false }
                    false, { Type = Shield 1uy; Initiative = false } ]) ] } }
    let warlock : Monster = {
        Character = {
            Name = "Warlock"
            Health = 5uy
            Tokens = [
                (0.5, Map.initFromList [
                    true, { Type = Spell 2uy; Initiative = false }
                    false, { Type = Nothing; Initiative = true } ])
                (0.5, Map.initFromList [
                    true, { Type = Spell 1uy; Initiative = false }
                    false, { Type = Nothing; Initiative = true } ])
                (0.5, Map.initFromList [
                    true, { Type = Spell 1uy; Initiative = false }
                    false, { Type = Shield 1uy; Initiative = true } ]) ] } }
    let monsters = [| skeleton; warlock |]