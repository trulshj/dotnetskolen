namespace NRK.Dotnetskolen

module Domain =

    open System
    open System.Text.RegularExpressions

    type Sending =
        { Tittel: string
          Kanal: string
          Starttidspunkt: DateTimeOffset
          Sluttidspunkt: DateTimeOffset }

    type Epg = Sending list

    let isTittelValid (tittel: string) : bool =
        let tittelRegex = Regex(@"^[\p{L}0-9\.,-:!]{5,100}$")
        tittelRegex.IsMatch tittel

    let isKanalValid (kanal: string) : bool =
        kanal.Equals "NRK1" || kanal.Equals "NRK2"

    let areStartAndSluttidspunktValid (starttidspunkt: DateTimeOffset) (sluttidspunkt: DateTimeOffset) =
        DateTimeOffset.Compare(starttidspunkt, sluttidspunkt) < 0

    let isSendingValid (sending: Sending) : bool =
        isTittelValid sending.Tittel
        && isKanalValid sending.Kanal
        && areStartAndSluttidspunktValid sending.Starttidspunkt sending.Sluttidspunkt
