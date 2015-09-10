namespace canAndroid

    module testRunner =
        
        type test = {
            description : string
            testFunction : unit -> unit
        }

        type result =
        | Pass
        | Fail        

        let mutable tests = new ResizeArray<test>()

        let (&&&) description testFunction =
            {
                description = description
                testFunction = testFunction
            } 
            |> tests.Add

        let run() =
            let runTest test =
                try
                    test.testFunction()
                    result.Pass
                with
                | ex -> sprintf "Test Failed: %s" ex.Message|> configuration.reporter.report
                        ex.StackTrace |> configuration.reporter.report
                        result.Fail

            "Running Tests..." |> configuration.reporter.report

            let executedTests = tests.ToArray() |> Array.map (fun test -> test |> runTest)
            
            "Test run complete" |> configuration.reporter.report
            
            executedTests |> Array.filter (fun testResult -> testResult = result.Pass) |> Array.length |> sprintf "Passed: %i" |> configuration.reporter.report
            executedTests |> Array.filter (fun testResult -> testResult = result.Fail) |> Array.length |> sprintf "Failed: %i" |> configuration.reporter.report
