namespace canAndroid
open OpenQA.Selenium.Appium
open OpenQA.Selenium.Appium.Android
open OpenQA.Selenium.Appium.Enums
open OpenQA.Selenium
open configuration

module canAndroidMain = 
    let mutable (driver : AndroidDriver<AppiumWebElement>) = null

    let puts text =
        printf "%s\n" text
           
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
        System.Threading.Thread.Sleep(2000) 

    let navigateToActivity activity =
        sprintf "and-activity://%s.%s" applicationName activity |> driver.Navigate().GoToUrl

    let find selector =
        let findFunctions = [|
            (fun selector -> selector |> By.Name)
            (fun selector -> selector |> By.TagName) 
            (fun selector -> selector |> By.ClassName)  
            (fun selector -> selector |> By.Id)  
        |]

        let executeFindFunction findFunction =
            try
                Some(driver.FindElement(selector |> findFunction))
            with
            | _ -> None

        findFunctions |> Array.map executeFindFunction |> Array.filter (fun item -> item.IsSome) |> Array.map (fun item -> item.Value)
    
    let tap selector =
        let elements = selector |> find
        puts <| sprintf "Tapping Element %s" selector
        match elements |> Array.length with
        | 0 -> puts "Failed to find Element"
        | _ -> elements |> Array.iter(fun element -> element.Click())

    let (==) selector value =
        let elements = selector |> find
        puts <| sprintf "Testing if element %s is equal to %s" selector value
        match elements |> Array.length with
        | 0 -> puts "Failed to find Element"
               false
        | _ -> elements.[0].Text = value        
               

    let quit () =
        driver.Quit()