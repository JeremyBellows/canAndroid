namespace canAndroid
    
    [<AutoOpen>]
    module assertions =
        open configuration
        open exceptions

        let insist booleanProperty =
            match booleanProperty with
            | true -> true
            | false -> raise <| CanAndroidInsistFailedException("Insist Failed!")
                       false

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