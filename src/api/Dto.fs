namespace NRK.Dotnetskolen

module Dto =
    open NRK.Dotnetskolen.Domain

    type SendingDto =
        { Tittel: string
          Starttidspunkt: string
          Sluttidspunkt: string }

    type EpgDto =
        { Nrk1: SendingDto list
          Nrk2: SendingDto list }

    let sendingerFilter (sendinger: Sending list) (kanal: string) : Sending list =
        List.filter (fun sending -> sending.Kanal.Equals kanal) sendinger

    let sendingerDtoMapper (sendinger: Sending list) : SendingDto list =
        List.map
            (fun (sending: Sending) ->
                { Tittel = sending.Tittel
                  Starttidspunkt = sending.Starttidspunkt.ToString("o")
                  Sluttidspunkt = sending.Sluttidspunkt.ToString("o") })
            sendinger

    let fromDomain (domain: Domain.Epg) : EpgDto =
        // let Sendinger = domain.Filter( s -> s.)
        let Nrk1Sendinger = sendingerFilter domain "NRK1"
        let Nrk2Sendinger = sendingerFilter domain "NRK2"

        let Nrk1SendingerDto = sendingerDtoMapper Nrk1Sendinger
        let Nrk2SendingerDto = sendingerDtoMapper Nrk2Sendinger


        { Nrk1 = Nrk1SendingerDto
          Nrk2 = Nrk2SendingerDto }
