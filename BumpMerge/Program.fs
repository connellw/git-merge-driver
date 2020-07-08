open System
open System.IO

let writeFile path contents =
    File.WriteAllText(path, contents)

let readFile path =
    File.ReadAllText(path)

let parseVersion (text: string) =
    text.Trim().Split '.'
    |> Seq.map Int32.Parse

let serializeVersion (a: seq<int>) =
    String.Join ('.', a)

let uncurry f (a,b) = f a b

let zipMap f a b =
    Seq.zip a b
    |> Seq.map (uncurry f)

let addVersion = zipMap (+)
let subtractVersion = zipMap (-)

[<EntryPoint>]
let main argv =
    let versions = Array.map (readFile >> parseVersion) argv

    let ancestor = versions.[0]
    let current = versions.[1]
    let other = versions.[2]

    let getBump version = subtractVersion version ancestor

    let totalVersion =
        addVersion (getBump current) (getBump other)
        |> addVersion ancestor
        |> serializeVersion

    writeFile argv.[1] totalVersion

    printfn "||| Merged version: %s |||" totalVersion

    0