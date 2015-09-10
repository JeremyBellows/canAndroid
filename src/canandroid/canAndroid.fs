namespace canAndroid
open OpenQA.Selenium.Appium
open OpenQA.Selenium.Appium.Android
open OpenQA.Selenium.Appium.Enums
open OpenQA.Selenium
open configuration
open exceptions

[<AutoOpen>]
module core = 
    let mutable (driver : AndroidDriver<AppiumWebElement>) = null
    
    let wait seconds =
        //Need to convert the seconds into milliseconds
        seconds * 1000 |> System.Threading.Thread.Sleep

    let setAppTo app =
        applicationName <- app
        capabilities.SetCapability(MobileCapabilityType.AppPackage, app)
    
    let setMainActivityTo activity =
        mainActivity <- activity
        capabilities.SetCapability(MobileCapabilityType.AppActivity, activity)
    
    let setDeviceNameTo deviceName =
        capabilities.SetCapability(MobileCapabilityType.DeviceName, deviceName)

    let startDriver () =
        let uri = new System.Uri(url)
        driver <- new AndroidDriver<AppiumWebElement>(uri, capabilities, System.TimeSpan.FromSeconds(60.0))    
        wait 2 //this is necessary to allow the app to start up           

    let private findFunctions = [|
            (fun selector -> selector |> By.Name)
            (fun selector -> selector |> By.TagName) 
            (fun selector -> selector |> By.ClassName)  
            (fun selector -> selector |> By.Id)  
        |]

    let private executeFindFunction selector findFunction =
        try
            Some(driver.FindElement(selector |> findFunction))
        with
        | _ -> None
    
    let getElements selector =
        let elements = findFunctions |> Array.map (selector |> executeFindFunction) |> Array.filter (fun item -> item.IsSome)
        
        match elements |> Array.length with
                | 0 -> raise <| CanAndroidFailedToFindElementException(sprintf "Failed to find any elements with selector %s" selector)
                       Array.empty                   
                | _ -> elements         

    let findMany selector =
        selector |> getElements|> Array.map (fun item -> item.Value)  
    
    let find selector =
        let elements = selector |> getElements

        if (elements |> Array.isEmpty) then
            None
        else
            elements.[0]

    let quit () =
        driver.Quit()