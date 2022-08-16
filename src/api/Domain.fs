namespace NRK.Dotnetskolen

module Domain =

    open System
    open System.Text.RegularExpressions

    type Tittel = private Tittel of string

    let isTittelValid (tittel: string) : bool =
        let tittelRegex = Regex(@"^[\p{L}0-9\.,-:!]{5,100}$")
        tittelRegex.IsMatch(tittel)

    module Tittel =
        let create (tittel: string) : Tittel option =
            if isTittelValid tittel then
                Tittel tittel
                |> Some
            else
                None
        
        let value (Tittel tittel) = tittel


    type Kanal = private Kanal of string

    let isKanalValid (kanal: string): bool =
        let kanalRegex = Regex(@"^NRK[12]$")
        kanalRegex.IsMatch(kanal)

    module Kanal =
        let create (kanal: string) : Kanal option =
            if isKanalValid kanal then
                Kanal kanal
                |> Some
            else
                None
        
        let value (Kanal kanal) = kanal
    

    type Sendetidspunkt = private {
        Starttidspunkt: DateTimeOffset
        Sluttidspunkt: DateTimeOffset
    }

    let areStartAndSluttidspunktValid (starttidspunkt: DateTimeOffset) (sluttidspunkt: DateTimeOffset) : bool =
        starttidspunkt < sluttidspunkt

    module Sendetidspunkt =
        let create (starttidspunkt: DateTimeOffset) (sluttidspunkt: DateTimeOffset) : Sendetidspunkt option =
            if areStartAndSluttidspunktValid starttidspunkt sluttidspunkt then
                {
                    Starttidspunkt = starttidspunkt
                    Sluttidspunkt = sluttidspunkt
                }
                |> Some
            else
                None

        let starttidspunkt (sendetidspunkt: Sendetidspunkt) = sendetidspunkt.Starttidspunkt
        let sluttidspunkt (sendetidspunkt: Sendetidspunkt) = sendetidspunkt.Sluttidspunkt
    

    type Sending = {
        Tittel: Tittel
        Kanal: Kanal
        Sendetidspunkt: Sendetidspunkt
    }

    type Epg = Sending list

    module Sending =
        let create (tittel: string) (kanal: string) (starttidspunkt: DateTimeOffset) (sluttidspunkt: DateTimeOffset) : Sending option =
            let tittel = Tittel.create tittel
            let kanal = Kanal.create kanal
            let sendetidspunkt = Sendetidspunkt.create starttidspunkt sluttidspunkt

            if tittel.IsNone || kanal.IsNone || sendetidspunkt.IsNone then
                None
            else
                Some {
                    Tittel = tittel.Value
                    Kanal = kanal.Value
                    Sendetidspunkt = sendetidspunkt.Value
                }


