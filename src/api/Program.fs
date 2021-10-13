namespace NRK.Dotnetskolen.Api

open Microsoft.Extensions.DependencyInjection

module Program = 
    open Microsoft.AspNetCore.Hosting
    open Microsoft.AspNetCore.Builder
    open Microsoft.Extensions.Hosting
    open Giraffe
    
    let configureApp (webHostContext: WebHostBuilderContext) (app: IApplicationBuilder) =
        let webApp = route "/ping" >=> text "pong"
        app.UseGiraffe webApp
        
    let configureServices (webHostContext: WebHostBuilderContext) (services: IServiceCollection) =
        services.AddGiraffe() |> ignore
        
    let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webHostBuilder ->
                webHostBuilder
                    .Configure(configureApp)
                    .ConfigureServices(configureServices) |> ignore
            )

    [<EntryPoint>]
    let main argv =
        createHostBuilder(argv).Build().Run()
        0