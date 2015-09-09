namespace canAndroid
open OpenQA.Selenium.Appium
open OpenQA.Selenium.Appium.Android
open OpenQA.Selenium.Appium.Enums
open OpenQA.Selenium
open configuration

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
               
    let quit () =
        driver.Quit()