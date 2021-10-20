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


  type Kanal = private Kanal of string

  let isChannelValid (channel: string): bool =
    List.contains channel ["NRK1"; "NRK2"]

  module Kanal =
    let create (kanal: String) : Kanal option =
      if isChannelValid kanal then
          Kanal kanal
          |> Some
      else 
          None
  
    let value (Kanal kanal) = kanal

  type Sending = {
    Tittel: Tittel
    Kanal: Kanal
    StartTidspunkt: DateTimeOffset
    SluttTidspunkt: DateTimeOffset
  }

  type Epg = Sending list

  let areStartAndEndTimesValid (startTime: DateTimeOffset) (endTime: DateTimeOffset): bool =
    startTime < endTime

  let isTransmissionValid (transmission: Sending) : bool =
    (areStartAndEndTimesValid transmission.StartTidspunkt transmission.SluttTidspunkt)