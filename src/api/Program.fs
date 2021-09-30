namespace NRK.Dotnetskolen.Api

module Program = 

    open Microsoft.Extensions.Hosting
    open Microsoft.AspNetCore.Hosting
    open Microsoft.AspNetCore.Builder
    open Microsoft.Extensions.DependencyInjection
    open NRK.Dotnetskolen.Api.HttpHandlers
    open Giraffe

    let configureServices (webHostContext: WebHostBuilderContext) (services: IServiceCollection) =
      services.AddGiraffe() |> ignore

    let configureApp (webHostContext: WebHostBuilderContext) (app: IApplicationBuilder) =
      let webApp = GET >=> choose [
                    route "/ping" >=> text "pong"
                    routef "/epg/%s" epgHandler 
                ]
      app.UseGiraffe webApp  

    let createHostBuilder args =
        Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(fun webHostBuilder -> webHostBuilder.Configure(configureApp).ConfigureServices(configureServices) |> ignore)

    [<EntryPoint>]
    let main argv =
        createHostBuilder(argv).Build().Run()
        0