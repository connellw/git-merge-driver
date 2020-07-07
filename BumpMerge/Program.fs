open System
open System.IO

let writeFile path contents =
    File.WriteAllText(path, contents)

let writeTestFile =
    writeFile "./Test.txt"

let readFile path =
    File.ReadAllText(path)

type SemVer = {
    Major: int
    Minor: int
    Patch: int
}

let version major minor patch = {
    Major = major
    Minor = minor
    Patch = patch
}

let versionArr (arr: int array) =
    version arr.[0] arr.[1] arr.[2]

let parseVersion (text: string) =
    text.Trim().Split '.'
    |> Array.map Int32.Parse
    |> versionArr

[<EntryPoint>]
let main argv =
    let ancestorContents = readFile argv.[0]
    let currentContents = readFile argv.[1]
    let otherContents = readFile argv.[2]
    printfn "|| %s || %s || %s ||" ancestorContents currentContents otherContents

    let versions = Array.map (readFile >> parseVersion) argv

    printfn "||| Merged version: %s |||" "1.1.1"

    0 // return an integer exit code
