namespace NRK.Dotnetskolen

module Domain = 
  open System
  open System.Text.RegularExpressions

  type Tittel = private Tittel of string

  let isTitleValid (title: string) : bool = 
    let titleRegex = Regex(@"^[\p{L}0-9\.,-:!]{5,100}$")
    titleRegex.IsMatch(title)

  module Tittel = 
    let create (tittel: String) : Tittel option =
      if isTitleValid tittel then
          Tittel tittel
          |> Some
      else
          None

    let value (Tittel tittel) = tittel

  type Sending = {
    Tittel: Tittel
    Kanal: string
    StartTidspunkt: DateTimeOffset
    SluttTidspunkt: DateTimeOffset
  }

  type Epg = Sending list

  let isChannelValid (channel: string): bool =
    List.contains channel ["NRK1"; "NRK2"]

  let areStartAndEndTimesValid (startTime: DateTimeOffset) (endTime: DateTimeOffset): bool =
    startTime < endTime

  let isTransmissionValid (transmission: Sending) : bool =
    (isChannelValid transmission.Kanal ) &&
    (areStartAndEndTimesValid transmission.StartTidspunkt transmission.SluttTidspunkt)