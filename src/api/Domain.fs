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
        channel.Equals("NRK1") || channel.Equals("NRK2")
        
        
    let areStartAndEndTimesValid (startTime: DateTimeOffset)(endTime: DateTimeOffset): bool =
        startTime.CompareTo(endTime) < 0
        
    let isTransmissionValid (transmission: Sending) : bool =
         isTitleValid(transmission.Tittel) && isChannelValid(transmission.Kanal) && areStartAndEndTimesValid transmission.StartTidspunkt transmission.SluttTidspunkt
