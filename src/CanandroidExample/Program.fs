open canAndroid

[<EntryPoint>]
let main argv = 
    setAppTo "com.android.calculator2"
    setMainActivityTo "Calculator"
    setDeviceNameTo "AndroidEmulator"
    startDriver()

    navigateToActivity configuration.mainActivity

    tap "2"
    tap "+"
    tap "4"
    tap "="

    match "formula" == "6" with
    | true -> printf "Test Passed\n"
    | false -> printf "Test Failed\n"

    printf "Press Enter to Exit..."
    System.Console.ReadLine() |> ignore
    quit()
    0 // return an integer exit code
