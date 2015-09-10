open canAndroid
open canAndroid.testRunner

[<EntryPoint>]
let main argv = 
    setAppTo "com.android.calculator2"
    setMainActivityTo "Calculator"
    setDeviceNameTo "AndroidEmulator"
    startDriver()

    navigateToActivity configuration.mainActivity

    let additionTest () =
        "Verify that 2 + 4 is 6" &&& (fun _ -> 
            tap "2"
            tap "+"
            tap "4"
            tap "="

            insist <| ("formula" == "6")
            insist <| ("formula" != "0")
        )
    
    let thisTestShouldFail () =
        "This test should fail" &&& (fun _ ->
            tap "2"
            tap "-"
            tap "2"
            tap "="

            insist <| ("formula" == "9001")
        )

    additionTest ()
    thisTestShouldFail ()

    run()

    printf "Press Enter to Exit..."
    System.Console.ReadLine() |> ignore
    quit()
    0 // return an integer exit code
