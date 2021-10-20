// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

// open System
// open NRK.Dotnetskolen.Domain
//
// // Define a function to construct a message to print
// let from whom =
//     sprintf "from %s" whom
//
// [<EntryPoint>]
// let main argv =
//     let epg = [
//         {
//             Tittel = "Dagsrevyen"
//             Kanal = "NRK1"
//             StartTidspunkt = DateTimeOffset.Parse("2021-04-16T19:00:00+02:00")
//             SluttTidspunkt = DateTimeOffset.Parse("2021-04-16T19:30:00+02:00")
//         }
//     ]
//     printfn "%A" epg
//     0 // return an integer exit code
namespace NRK.Dotnetskolen.Api

module Program =

    open Microsoft.Extensions.Hosting

    open Microsoft.AspNetCore.Hosting
    open Microsoft.AspNetCore.Builder

    open Microsoft.Extensions.DependencyInjection
    open Giraffe

    open NRK.Dotnetskolen.Api.HttpHandlers

    let epgHandler (webHostContext: WebHostBuilderContext) (app: IApplicationBuilder) =
        let webApp = GET >=> choose [
                        route "/ping" >=> text "pong"
                        routef "/epg/%s" epgHandler
                    ]
        app.UseGiraffe webApp



    let configureServices (webHostContext: WebHostBuilderContext) (services: IServiceCollection) =
      services.AddGiraffe() |> ignore

    let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webHostBuilder ->
                webHostBuilder
                    .Configure(epgHandler)
                    .ConfigureServices(configureServices) |> ignore
            )

    [<EntryPoint>]
    let main argv =
        createHostBuilder(argv).Build().Run()
        0
