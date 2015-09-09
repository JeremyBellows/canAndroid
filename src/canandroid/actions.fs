namespace canAndroid

    [<AutoOpen>]
    module actions =
        open configuration

        let tap selector =
            let elements = selector |> findMany
            
            reporter.report <| sprintf "Tapping Element %s" selector
            match elements |> Array.length with
            | 0 -> reporter.report "Failed to find Element"
            | _ -> elements |> Array.iter(fun element -> element.Click())


        let navigateToActivity activity =
            sprintf "and-activity://%s.%s" applicationName activity |> driver.Navigate().GoToUrl