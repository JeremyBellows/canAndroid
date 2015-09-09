namespace canAndroid

    open OpenQA.Selenium.Remote

    module configuration =
       open reporter

       let mutable (applicationName : string) = ""
       let mutable (mainActivity : string) = ""

       let mutable url = "http://localhost:4723/wd/hub"
       let mutable capabilities = DesiredCapabilities.Android()

       let mutable (reporter : reporter) = consoleReporter.getConsoleReporter()