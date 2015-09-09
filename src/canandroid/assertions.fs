namespace canAndroid
    
    [<AutoOpen>]
    module assertions =
        open configuration

        let (==) selector value =
            let element = selector |> find
            
            reporter.report <| sprintf "Testing if element %s is equal to value %s" selector value
            
            match element with
            | Some(element) -> element.Text = value  
            | None -> false