module Dojos

open System

let openUrl (url: string) =
    ignore (System.Diagnostics.Process.Start(url))

[<EntryPoint>]
let main argv =
    openUrl("https://github.com/MecuSorin/FSharpLove")
    0 // return an integer exit code
