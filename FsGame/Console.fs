namespace RB4.IO

open System
open RB4.Domain

module Console = 
    let printWelcome () =
        Console.ForegroundColor <- ConsoleColor.Cyan
        printfn @"                                                                                                                                            "
        printfn @"                                  -+oo++-                                                          +o####-                                  "
        printfn @"                              -+oo#W@#+-                                                            --+#@@##o+-                             "
        printfn @"                        -+o#@@@#o##-     -                                                      -++o+   -o#oo@W@@#+-                        "
        printfn @"                    -+#@WWW@#+ -o- -      o+   -                                              -#@@@+      -o- +#@WWW@#o-                    "
        printfn @"                 +#@WWW@###o+ o#   -oo+---@Wo--#+                                          -#@@W@@o++--    -@+ +##o#WWWW@#+                 "
        printfn @"              +#@WWW@#o#@@oo-#@      -o@@@@WWW@##+---                                     -WW#@@@#@@@@o-    -Wo+o#@#oo@WWWW@#-              "
        printfn @"           -o@WWWW@oo#WWoo#+#W+      +o#@@#@@W@##@@@#                                    o#@@#@W#--o#@Wo     #Woo#+#W@oo#WWWWW@+            "
        printfn @"         -#@WWWW@oo#WW#+#@+#WW     +@W#o@#- +@#+++---  +o+-                       -+#-  #Woo+++-    +#WWo    +WWoo@ooWW@o+#WWWWW@+          "
        printfn @"        #WWWWW@oo@WWWooWW+#WWW-    +#W@##+   #Wo-++   -#---                        +##  ---     --+-+@W@-    -WWW+oW#+#WW@o+#WWWWW@-        "
        printfn @"      -@WWW@#++#WWW#-oWWooWWWW#+++o@WWW@@##+---+- ++  ##+o-                       -++@#      -+o#@##@WWo     +WWW@-#W@+oWWW@oo#@WWWWo       "
        printfn @"     +WW@o-  +@WWW#-#WW#-WWWWWWWWWWW##WWWWW@o++    +o@@  -  #-                       -@W@#- +#oo#@@WW@@#+   -#WWWW#-@W@++WWWWo  +#WWW#      "
        printfn @"    +W@+    oWWWW# #WW@-#WWWWW@#oo## -WWWWWW@@##o#@@o#o     @@- -++++++oooo#o-   +oo- oo+@W@##@WWWWW@#@WW##@WWWWWWW+oWW@++@WWW#   -o@W#     "
        printfn @"   -@o     oWWW@o-#WWWo-@WWW#-       -W#ooo@WWWWWW#- o-     #WW@#+-        -o@@@W@+   o   #WWW@WW#+oo#@@@WWWWWWWWWW# @WW@-+WWWW#     oWo    "
        printfn @"   +-     +WWW#- oWWWW-+WWW+          -   oWWW@W@@o -+-   +@#@###+         -oo#WW#       +@o##@WW-        -+o@WWWWWW oWWW@ -#WWW#     -#-   "
        printfn @"          #WW+  -@WW@- oWW+              +--WW@#+@W-     #@+ -# -o#+     -+- -@o oo+     @Wo#WWWW@##+         +@WWWW--@WWWo  +WWW+      +   "
        printfn @"         -WW+   oWW#   +W@              ---+WWW#oWo+    #o    ++   -oo-      #o   --o    ##+#WWWWWWo            oWWW+  #WW@-  +WW#          "
        printfn @"         +Wo    @W@    -Wo             +oWWWWW##@Wo    oo      -   +  +o++- oo      +o    #W##WWWWW@o+-          oWW-   #WWo   +WW-         "
        printfn @"         +W-    WW+     @+              -@WW@o#Woo-   -o        -+o+    -- -#       -@-   @WW+o@@WWWWW-           @W-   -@W#    #W-         "
        printfn @"         -#     W@      +-        --++o#@W@ooo@W+     -++    -+o+-         #-        @o   -@@+@@##@@WW@oo+-       o#     oW@    +@-         "
        printfn @"          o     @#               -#WWWW@#o+@W+#+       oo +ooo-  o+       -#   -++- -Wo       +o#####@@WWW+       +-     -W@    -@          "
        printfn @"          -     #o                +W@#####+-      ---+o@W@#+      #+      ---    -o#@W-            -++o#@WW@#-            @@    -o          "
        printfn @"                +o               +@#o##+           -+o##W#+++---  -#  o++++++++oooo@WW#-               -+o#WW#-           #o    --          "
        printfn @"                 +           +##@Wo##-                  #W-        oo             o@++o#o                 +o@W@+          #+                "
        printfn @"                            -+#@W#oo                     o@o        @-  o-       +o-    --                 -o@WW#         +                 "
        printfn @"                               -Wo#-                      -o#+      ## +o      -++-                         +#W##o                          "
        printfn @"                                @@o+                         +++++++o@o@+--+ooo+                            o@W+ @-                         "
        printfn @"                               +WW@o+                           ---+#WW@##o+-                             -#@#-  #-                         "
        printfn @"                               ooo#@#o++---                         +Wo                                 -@@o    +#                          "
        printfn @"                                    +@@o++o#+                       o+                                 #W@-    +o                           "
        printfn @"                                     -@@##+-oo+                                                       @#@@  -++-                            "
        printfn @"                                         -#@-o-+                                                     +W -@#--                               "
        printfn @"                                           W-o---                                                     #o+-#+                                "
        printfn @"                                          +@ o                                                         +o+o@+                               "
        printfn @"                                       -o@@--+                                                           --+@o-                             "
        printfn @"                                    -+o##+  +                                                               o@-++-                          "
        printfn @"                                   -+o+-    +                                                         -     #@   +-                         "
        printfn @"                                   -o-       -                                                    ++--o---+#W+    -                         "
        printfn @"                                   o                                                                 --ooooo-    -                          "
        printfn @"                                   -                                                                                                        "
        printfn @"                                                                                                                                            "
        printfn @"         __          __            __          "
        printfn @"        /\ \        / /\       __ /\ \         "
        printfn @"       /  \ \      / /  \     /\_\\ \ \        "
        printfn @"      / /\ \ \    / / /\ \   / / / \ \ \       "
        printfn @"     / / /\ \_\  / / /\ \ \ / / /   \ \ \      "
        printfn @"    / / /_/ / / / / /\ \_\ \\ \ \____\ \ \     "
        printfn @"   / / /__\/ / / / /\ \ \___\\ \________\ \    "
        printfn @"  / / /_____/ / / /  \ \ \__/ \/________/\ \   "
        printfn @" / / /\ \ \  / / /____\_\ \             \ \ \  "
        printfn @"/ / /  \ \ \/ / /__________\             \ \_\ "
        printfn @"\/_/    \_\/\/_____________/              \/_/ "
        Console.ResetColor ()

    (*
    let getTokenActionString action =
        match action with
        | Damage n -> $"{n}D"
        | Shield n -> $"{n}S"
        | None -> "N"

    let getTokenInitiativeString initiative =
        match initiative with
        | true -> "+"
        | false -> "-"
        
    let getTokenSideString side =
        getTokenActionString side.Action + getTokenInitiativeString side.Initiative

    let getTokensSideString sides =
        sides |> List.fold (fun acc cur -> $"{acc} ({getTokenSideString cur})") ""

    let getTokenString token = 
        let sideA = getTokenSideString token.SideA
        let sideB = getTokenSideString token.SideB
        $"({sideA}|{sideB})"

    let getTokensString tokens =
        tokens |> List.fold (fun acc cur -> $"{acc} {getTokenString cur}") ""

    let printHero hero =
        let tokensS = getTokensString hero.Tokens
        printfn "HERO: %s \t(H=%d; T=[%s])" hero.Name hero.Health (tokensS.Trim ())
    
    let printStartRound (hero: Hero) sides initiative =
        let sidesStr = getTokensSideString sides
        printfn $"{hero.Name} H={hero.Health}; T:{sidesStr}; I={initiative}"
    
    let printAvailableActions sides =
        sides
        |> List.filter (fun s -> match s.Action with | Shield _ -> false | _ -> true)
        |> List.map (fun s -> match s.Action with | Damage _ -> "Damage [D]" | _ -> "")
        |> List.filter (fun s -> s <> "")
        |> List.append ["Pass [P]"]
        |> Seq.distinctBy (fun s -> s)
        |> List.ofSeq
        |> List.iter (fun s -> printfn "%s" s)
    *)