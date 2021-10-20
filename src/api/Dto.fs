namespace NRK.Dotnetskolen

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

  let fromDomain (epg: Domain.Epg) : EpgDto =
    let NRK1epg =
      epg
      |> List.filter (fun s : bool ->
        s.Kanal = "NRK1"
        )
      |> List.map (fun s ->
        let startTimeOffsetAsString = s.StartTidspunkt.ToString("o")
        let endTimeOffsetAsString = s.SluttTidspunkt.ToString("o")
        let sTittel = s.Tittel
        {
          SendingDto.Tittel = sTittel
          SendingDto.StartTidspunkt = startTimeOffsetAsString
          SendingDto.SluttTidspunkt = endTimeOffsetAsString
        }
        )
    let NRK2epg =
      epg
      |> List.filter (fun s : bool ->
        s.Kanal = "NRK2"
        )
      |> List.map (fun s ->
        let startTimeOffsetAsString = s.StartTidspunkt.ToString("o")
        let endTimeOffsetAsString = s.SluttTidspunkt.ToString("o")
        let sTittel = s.Tittel
        {
          SendingDto.Tittel = sTittel
          SendingDto.StartTidspunkt = startTimeOffsetAsString
          SendingDto.SluttTidspunkt = endTimeOffsetAsString
        }
        )
    {
      EpgDto.Nrk1 = NRK1epg
      EpgDto.Nrk2 = NRK2epg
    }
