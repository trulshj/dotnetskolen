namespace NRK.Dotnetskolen.IntegrationTests

module Mock =

  open System
  open NRK.Dotnetskolen.Domain

  let getAllTransmissions () : Epg =
      let now = DateTimeOffset.Now
      [
          // Sendinger tilbake i tid
          {
              Tittel = (Tittel.create "Testprogram").Value
              Kanal = (Kanal.create "NRK1").Value
              StartTidspunkt = now.AddDays(-10.)
              SluttTidspunkt = now.AddDays(-10.).AddMinutes(30.)
          }
          {
              Tittel = (Tittel.create "Testprogram").Value
              Kanal = (Kanal.create "NRK2").Value
              StartTidspunkt = now.AddDays(-10.)
              SluttTidspunkt = now.AddDays(-10.).AddMinutes(30.)
          }
          // Sendinger i dag
          {
              Tittel = (Tittel.create "Testprogram").Value
              Kanal = (Kanal.create "NRK1").Value
              StartTidspunkt = now
              SluttTidspunkt = now.AddMinutes(30.)
          }
          {
              Tittel = (Tittel.create "Testprogram").Value
              Kanal = (Kanal.create "NRK2").Value
              StartTidspunkt = now
              SluttTidspunkt = now.AddMinutes(30.)
          }
          // Sendinger frem i tid
          {
              Tittel = (Tittel.create "Testprogram").Value
              Kanal = (Kanal.create "NRK1").Value
              StartTidspunkt = now.AddDays(10.)
              SluttTidspunkt = now.AddDays(10.).AddMinutes(30.)
          }
          {
              Tittel = (Tittel.create "Testprogram").Value
              Kanal = (Kanal.create "NRK2").Value
              StartTidspunkt = now.AddDays(10.)
              SluttTidspunkt = now.AddDays(10.).AddMinutes(30.)
          }
      ]