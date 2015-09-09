namespace canAndroid
    
    [<AutoOpen>]
    module assertions =
        open configuration

        let private equals selector value =
            let element = selector |> find

            match element with
            | Some(element) -> element.Text = value  
            | None -> false

        let (==) selector value =
            reporter.report <| sprintf "Testing if element %s is equal to value %s" selector value
            selector |> equals <| value
            
        let (!=) selector value =
            reporter.report <| sprintf "Testing if element %s is not equal to value %s" selector value
            not <| (selector |> equals <| value)