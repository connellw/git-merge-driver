open System.IO

let writeFile path contents =
    File.WriteAllText(path, contents)

let writeTestFile =
    writeFile "./Test.txt"

let readFile path =
    File.ReadAllText(path)

[<EntryPoint>]
let main argv =
    let ancestorContents = readFile argv.[0]
    let currentContents = readFile argv.[1]
    let otherContents = readFile argv.[2]

    printfn "|| %s || %s || %s ||" ancestorContents currentContents otherContents

    0 // return an integer exit code
