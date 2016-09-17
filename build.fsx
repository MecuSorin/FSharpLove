// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Testing
open System

MSBuildDefaults.Verbosity = Some(Quiet)

// Directories

let buildDir  = "./build/"
let deployDir = "./deploy/"
let testDir = "./test/"
// define test dlls
let testDlls = !! (testDir + "/*Tests.dll")

// Filesets
let appReferences  =
    !! "/**/Dojo*.*proj" -- "/**/*Tests.*proj"
let appTestsReferences  =
    !! "/**/Dojo*Tests.*proj"


// version info
let version = "0.1"  // or retrieve from CI server
let cleanDirs () = CleanDirs [buildDir; deployDir; testDir]
// Targets
Target "Clean" (fun _ -> cleanDirs() )

Target "Build" (fun _ ->
    // compile all projects below src/app/
    MSBuildDebug buildDir "Build" appReferences
        |> Log "AppBuild-Output: "
)

Target "Deploy" (fun _ ->
    !! (buildDir + "/**/*.*")
        -- "*.zip"
        |> Zip buildDir (deployDir + "ApplicationName." + version + ".zip")
)

// Build order
"Clean"
  ==> "Build"
  ==> "Deploy"

let buildTest () =
    MSBuildDebug testDir "Build" appTestsReferences
        |> Log "AppBuild-Output: "
let runTest () = 
    testDlls
        |> xUnit2 (fun p -> 
            {p with 
                ShadowCopy = false;
                HtmlOutputPath = Some (testDir @@ "html") })

Target "BuildTest" (fun _ -> buildTest() )

Target "Test" (fun _ -> runTest() )

"Clean"
  ==> "BuildTest"
  ==> "Test"

// start build
RunTargetOrDefault "Test"
