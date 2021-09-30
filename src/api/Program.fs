namespace NRK.Dotnetskolen.Api

module Program = 

    open Microsoft.Extensions.Hosting
    open Microsoft.AspNetCore.Hosting
    open Microsoft.AspNetCore.Builder
    open Microsoft.Extensions.DependencyInjection

    let configureServices (webHostContext: WebHostBuilderContext) (services: IServiceCollection) =
      ()

    let configureApp (webHostContext: WebHostBuilderContext) (app: IApplicationBuilder) =
      ()  

    let createHostBuilder args =
        Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(fun webHostBuilder -> webHostBuilder.Configure(configureApp).ConfigureServices(configureServices) |> ignore)

    [<EntryPoint>]
    let main argv =
        createHostBuilder(argv).Build().Run()
        0