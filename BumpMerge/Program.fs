open System
open System.IO

let writeFile path contents =
    File.WriteAllText(path, contents)

let writeTestFile =
    writeFile "./Test.txt"

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
    let ancestorContents = readFile argv.[0]
    let currentContents = readFile argv.[1]
    let otherContents = readFile argv.[2]
    printfn "|| %s || %s || %s ||" ancestorContents currentContents otherContents

    let versions = Array.map (readFile >> parseVersion) argv

    let myBump = subtractVersion versions.[1] versions.[0]
    let theirBump = subtractVersion versions.[2] versions.[0]
    let totalBump = addVersion myBump theirBump
    let totalVersion = addVersion versions.[0] totalBump
    let totalVersionStr = serializeVersion totalVersion

    writeFile argv.[1] totalVersionStr

    printfn "||| Merged version: %s |||" totalVersionStr

    0 // return an integer exit code
