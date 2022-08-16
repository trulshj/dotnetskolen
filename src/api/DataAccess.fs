namespace NRK.Dotnetskolen.Api

module DataAccess = 

    open System
    open NRK.Dotnetskolen.Domain

    type SendingEntity = {
        Tittel: string
        Kanal: string
        Starttidspunkt: string
        Sluttidspunkt: string
    }

    type EpgEntity = SendingEntity list

    let database = 
        [
        {
            Tittel = "Testprogram"
            Kanal = "NRK1"
            Starttidspunkt = "2021-04-12T13:00:00Z"
            Sluttidspunkt = "2021-04-12T13:30:00Z"
        }
        {
            Tittel = "Testprogram"
            Kanal = "NRK2"
            Starttidspunkt = "2021-04-12T14:00:00Z"
            Sluttidspunkt = "2021-04-12T15:00:00Z"
        }
        {
            Tittel = "Testprogram"            
            Kanal = "NRK3"
            Starttidspunkt = "2021-04-12T14:00:00Z"
            Sluttidspunkt = "2021-04-12T16:30:00Z"
        }
    ]

    let sendingEntityToDomain (entity: SendingEntity): Sending option =
        Sending.create entity.Tittel entity.Kanal (DateTimeOffset.Parse (entity.Starttidspunkt)) (DateTimeOffset.Parse (entity.Sluttidspunkt))


    let epgEntityToDomain (entity: EpgEntity): Epg =
        entity
        |> List.map sendingEntityToDomain
        |> List.filter (fun s -> s.IsSome)
        |> List.map (fun s -> s.Value)

    let getAlleSendinger () : Epg =
        database
        |> epgEntityToDomain        