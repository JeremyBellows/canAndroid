namespace canAndroid
    
    [<AutoOpen>]
    module assertions =
        open configuration

        let (==) selector value =
            let elements = selector |> find
            
            reporter.report <| sprintf "Testing if element %s is equal to value %s" selector value
            match elements |> Array.length with
            | 0 -> reporter.report "Failed to find Element"
                   false
            | _ -> elements.[0].Text = value       
