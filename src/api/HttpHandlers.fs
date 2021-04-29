namespace NRK.Dotnetskolen.Api

module HttpHandlers = 

    open System
    open System.Globalization
    open System.Threading.Tasks
    open Microsoft.AspNetCore.Http
    open Giraffe
    open NRK.Dotnetskolen.Dto
    open NRK.Dotnetskolen.Domain

    let parseAsDateTime (dateAsString : string) : DateTime option =
        try
            let date = DateTime.ParseExact(dateAsString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None)
            Some date
        with
        | _ -> None

    let epgHandler (getEpgForDate : DateTime -> Epg) (dateAsString : string) : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            match (parseAsDateTime dateAsString) with
            | Some date -> 
                let response = date
                                |> getEpgForDate 
                                |> fromDomain
                                |> json
                response next ctx
            | None -> RequestErrors.badRequest (text "Invalid date") (Some >> Task.FromResult) ctx