namespace NRK.Dotnetskolen.Api

module Services =

    open System
    open NRK.Dotnetskolen.Domain

    let getEpgForDate (getAlleSendinger : unit -> Epg) (date: DateTimeOffset) : Epg =
        let alleSendinger = getAlleSendinger ()
        List.filter (fun s -> (Sendetidspunkt.starttidspunkt s.Sendetidspunkt).Date.Date = date.Date.Date) alleSendinger