namespace NRK.Dotnetskolen.Api

module HttpHandlers =
    open System
    open System.Globalization
    open System.Threading.Tasks
    open Microsoft.AspNetCore.Http
    open NRK.Dotnetskolen.Domain
    open NRK.Dotnetskolen.Dto

    let parseAsDateTime (dateAsString: string) : DateTimeOffset option =
        try
            let date =
                DateTimeOffset.ParseExact(dateAsString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None)

            Some date
        with
        | _ -> None

    let epgHandler (getEpgForDate: DateTimeOffset -> Epg) (dateAsString: string) =
        match (parseAsDateTime dateAsString) with
        | Some date ->
            let response = date |> getEpgForDate |> fromDomain
            Results.Ok(response)
        | None -> Results.BadRequest("Invalid date")
