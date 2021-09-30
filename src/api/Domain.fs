namespace NRK.Dotnetskolen

module Domain = 
  open System

  type Sending = {
    Tittel: string
    Kanel: string
    StartTidspunkt: DateTimeOffset
    SluttTidspunkt: DateTimeOffset
  }

  type Epg = Sending list