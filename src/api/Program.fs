namespace NRK.Dotnetskolen.Api

module Program = 

    open System
    open Microsoft.Extensions.Hosting
    open Microsoft.AspNetCore.Hosting
    open Microsoft.AspNetCore.Builder
    open Microsoft.Extensions.DependencyInjection
    open NRK.Dotnetskolen.Domain
    open NRK.Dotnetskolen.Api.DataAccess
    open NRK.Dotnetskolen.Api.HttpHandlers
    open NRK.Dotnetskolen.Api.Services
    open Giraffe

    let configureServices (webHostContext: WebHostBuilderContext) (services: IServiceCollection) =
      services.AddGiraffe() |> ignore

    let configureApp (getEpgForDate: DateTime -> Epg) (webHostContext: WebHostBuilderContext) (app: IApplicationBuilder) =
      let webApp = GET >=> choose [
                    route "/ping" >=> text "pong"
                    routef "/epg/%s" (epgHandler getEpgForDate) 
                ]
      app.UseGiraffe webApp  

    let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder -> 
                webBuilder
                    .Configure(configureApp (getEpgForDate getAllTransmissions))
                    .ConfigureServices(configureServices) 
                |> ignore
            )

    [<EntryPoint>]
    let main argv =
        createHostBuilder(argv).Build().Run()
        0