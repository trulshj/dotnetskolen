namespace NRK.Dotnetskolen

module Domain = 
  open System
  open System.Text.RegularExpressions

  type Sending = {
    Tittel: string
    Kanel: string
    StartTidspunkt: DateTimeOffset
    SluttTidspunkt: DateTimeOffset
  }

  type Epg = Sending list

  let isTitleValid (title: string) : bool =
      let titleRegex = Regex(@"^[\p{L}0-9\.,-:!]{5,100}$")
      titleRegex.IsMatch(title)