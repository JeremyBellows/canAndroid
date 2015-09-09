namespace canAndroid

    module reporter =

        type reporter = {
            report : string -> unit
        }

        let createReporter reportFunction =
            {
                report = reportFunction
            }


    module consoleReporter =
        open reporter
        
        let report messsage =
                printf "%s\n" messsage

        let getConsoleReporter () =
            report |> createReporter