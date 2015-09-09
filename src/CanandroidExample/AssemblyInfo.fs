namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("CanandroidExample")>]
[<assembly: AssemblyProductAttribute("canAndroid")>]
[<assembly: AssemblyDescriptionAttribute("Android UI Testing Framework")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
