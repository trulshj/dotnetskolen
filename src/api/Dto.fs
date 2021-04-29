namespace NRK.Dotnetskolen

open NRK.Dotnetskolen.Domain

module Dto =

  type SendingDto = {
      Tittel: string
      StartTidspunkt: string
      SluttTidspunkt: string
  }

  type EpgDto = {
    Nrk1: SendingDto list
    Nrk2: SendingDto list
  }
  
  let fromDomain (domain: Domain.Epg) : EpgDto =
      let hentSendingerIKanal (kanal:string) =
          List.filter (fun (x:Domain.Sending) -> Kanal.value x.Kanal = kanal)

      let tilSendingDto (s:Domain.Sending) : SendingDto =
          {
              Tittel = Tittel.value s.Tittel
              StartTidspunkt = (Sendetidspunkt.startTidspunkt s.Sendetidspunkt).ToString("o")
              SluttTidspunkt = (Sendetidspunkt.sluttTidspunkt s.Sendetidspunkt).ToString("o")
          }

      let nrk1Sendinger = domain |> hentSendingerIKanal "NRK1" |> List.map tilSendingDto
      let nrk2Sendinger = domain |> hentSendingerIKanal "NRK2" |> List.map tilSendingDto
      
      {
          Nrk1 = nrk1Sendinger
          Nrk2 = nrk2Sendinger
      }
