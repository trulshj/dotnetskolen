namespace NRK.Dotnetskolen.Api

module Program = 

    open Microsoft.Extensions.Hosting
    open Microsoft.AspNetCore.Hosting
    open Microsoft.AspNetCore.Builder

    let configureApp (webHostContext: WebHostBuilderContext) (app: IApplicationBuilder) =
      ()  

    let createHostBuilder args =
        Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(fun webHostBuilder -> webHostBuilder.Configure(configureApp) |> ignore)

    [<EntryPoint>]
    let main argv =
        createHostBuilder(argv).Build().Run()
        0