namespace NRK.Dotnetskolen

module Domain = 

    open System
    open System.Text.RegularExpressions

    type Sending = {
        Tittel: string
        Kanal: string
        StartTidspunkt: DateTimeOffset
        SluttTidspunkt: DateTimeOffset
    }

    type Epg = Sending list
    
    let isTitleValid (title: string) : bool =
        let titleRegex = Regex(@"^[\p{L}0-9\.,-:!]{5,100}$")
        titleRegex.IsMatch(title)
        
    let isChannelValid (channel: string) : bool =
        let validChannels = ["NRK1"; "NRK2"]
        validChannels |> List.contains channel
        
    let areStartAndEndTimesValid (startTime: DateTimeOffset) (endTime: DateTimeOffset) =
       let compared = startTime |> endTime.CompareTo
       if compared > 0 then true else false
       
    let isTransmissionValid (transmission: Sending) : bool =
        (transmission.Tittel |> isTitleValid) && 
        (transmission.Kanal |> isChannelValid ) && 
        (areStartAndEndTimesValid transmission.StartTidspunkt transmission.SluttTidspunkt)
