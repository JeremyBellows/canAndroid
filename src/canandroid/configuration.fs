namespace canandroid
open OpenQA.Selenium.Remote

    module configuration =

       let mutable (applicationName : string) = ""
       let mutable (mainActivity : string) = ""

       let mutable url = "http://localhost:4723/wd/hub"
       let mutable capabilities = DesiredCapabilities.Android()
