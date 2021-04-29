namespace NRK.Dotnetskolen.Api

module Services =

    open System
    open NRK.Dotnetskolen.Domain

    let getEpgForDate (getAllTransmissions : unit -> Epg) (date : DateTime) : Epg =
        let allTransmissions = getAllTransmissions ()
        allTransmissions |> List.filter (fun epg -> (Sendetidspunkt.startTidspunkt epg.Sendetidspunkt).Date.Equals date.Date)