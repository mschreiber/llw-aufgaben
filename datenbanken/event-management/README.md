# Event Management System

## Aufgabe 1

Ihr Kunde möchte ein Event-Management-System, mit dem er seine Veranstaltungen administieren kann. Er bittet sie, den Datenbankteil des Projekts zu übernehmen.
Erstellen sie ein ER Diagramm in der dritten Normalform, das folgende Daten abbildet:

### Daten:

| EventName            | EventDatumStart     | EventDatumEnde      | EventMaxAnzahl | OrtPLZ | OrtName   | OrtLokal       | VeranstalterName   | VeranstalterEMail         | TeilnehmerVorname | TeilnehmerNachname | TeilnehmerEMail             | TicketPreis | TicketTypName | TicketTypBeschreibung                   | TeilnahmeBuchungsdatum |
| -------------------- | ------------------- | ------------------- | -------------- | ------ | --------- | -------------- | ------------------ | ------------------------- | ----------------- | ------------------ | --------------------------- | ----------- | ------------- | --------------------------------------- | ---------------------- |
| Rock Festival        | 2025-06-01 18:00:00 | 2025-06-02 02:00:00 | 5000           | 80331  | München   | Olympiastadion | EventPlus GmbH     | eventplus@rockfest.de     | Max               | Mustermann         | max.mustermann@mail.com     | 120.00      | VIP           | Zugang zum VIP-Bereich und Meet & Greet | 2025-05-01 10:30:00    |
| Tech Conference 2025 | 2025-07-15 09:00:00 | 2025-07-15 18:00:00 | 2000           | 10115  | Berlin    | Messehalle 1   | TechInnovations AG | info@techinnovations.com  | Laura             | Schmidt            | laura.schmidt@tech.com      | 250.00      | Standard      | Eintritt zur Konferenz und Workshops    | 2025-06-20 08:00:00    |
| Kunst Gala 2025      | 2025-09-12 19:00:00 | 2025-09-12 23:00:00 | 300            | 70173  | Stuttgart | Kunstmuseum    | ArtVision e.V.     | kontakt@artvision.de      | Thomas            | Becker             | thomas.becker@kunstmail.com | 150.00      | Regular       | Zugang zur Kunstgalerie und Empfang     | 2025-09-01 11:00:00    |
| Summer Party         | 2025-08-01 20:00:00 | 2025-08-02 04:00:00 | 1000           | 80331  | München   | P1 Club        | PartyEvents UG     | party@p1club.de           | Anna              | Müller             | anna.mueller@mail.com       | 50.00       | Standard      | Eintritt zur Sommerparty mit DJs        | 2025-07-15 14:00:00    |
| Business Networking  | 2025-10-10 08:00:00 | 2025-10-10 16:00:00 | 500            | 50667  | Köln      | Köln Messe     | CorporateConnect   | info@corporateconnect.com | Michael           | Wagner             | michael.wagner@business.com | 100.00      | VIP           | Zugang zu exklusiven Business-Panels    | 2025-09-30 09:00:00    |
| Film Festival 2025   | 2025-11-05 18:00:00 | 2025-11-07 23:59:00 | 2000           | 90402  | Nürnberg  | CineStar Kino  | FilmFest GmbH      | info@filmfest.de          | Sandra            | Hoffmann           | sandra.hoffmann@film.com    | 80.00       | Standard      | Zugang zu allen Filmvorführungen        | 2025-10-15 13:00:00    |

**Hinweis:** Tickets werden vorab in der DB angelegt (z.b. für eine Veranstaltung 1000 Stück). Durch das Kaufen eines Tickets (TeilnahmeBuchungsDatum) wird ein Ticket einem Teilnehmer zugeordnet. Ein Teilnehmer kann viele Tickets kaufen. Ein Veranstalter kann viele Events organisieren und ein Event kann von mehreren Veranstaltern organisiert werden!

## Aufgabe 2

### Events mit Ticket Anzahl
Die Presse möchte mehr über Klein-Events wissen. Sie interessiert sich für Events, wo es weniger als 1000 Tickets geben wird.
Wie lautet das SQL Statement, das alle Namen und das Startdatum der Events ausgibt, die weniger als 1000 Tickets haben werden?

### An wen wurden Tickets verkauft
Es gibt ein Problem mit der Veranstaltung "Rock am Platz". Der Kunde bittet sie, alle die für dieses Event eine Karte gekauft haben, herauszufinden, damit er ihnen eine E-Mail schreiben kann.
Wie lautet das SQL Statement, das die Namen (Vor- und Nachname) sowie die E-Mail Adressen der Personen ausgibt, die Tickets für "Rock am Platz" gekauft haben.
Wenn jemand mehrere Tickets gekauft hat, soll der Name/Mail nur einmal aufscheinen.

### VIP Tickets für ein Event
Der Veranstalter möchte ein Willkommensgeschenk für alle VIP Ticket Besitzer für die Veranstaltung "Rock am Platz" organisieren.
Wie lautet das SQL Statement um die Namen auszugeben, die für die Veranstaltung "Rock am Platz" ein "VIP" Ticket gekauft haben?

### Events mit 2 Veranstalter
Ihr Kunde möchte wissen, welche der Veranstaltungen von 2 Veranstalter gemeinsam organisiert werden.
Wie lautet das SQL Statement, das den Namen der Events ausgibt, die von genau 2 Veranstaltern organisiert werden?

### Preis eines Tickets
Frau "Klein" ruft an und beklagt sich über den hohen Ticketpreis.
Wie lautet das SQL Statement um den Event Namen und den Preis für die Tickets auszugeben, die von Frau "Klein" gekauft wurden?

### Noch nicht verkaufte Tickets
Ihr Kund will wissen, welche Tickets noch nicht verkauft worden sind.
Wie lautet das SQL Statement, dass den Namen des Events und den Ticketpreis für alle Tickets ausgibt, die noch nicht verkauft worden sind?

### Anzahl noch nicht verkauften Tickets
Jetzt möchte der Kunde nur die Anzahl der noch nicht verkauften Tickets pro Event wissen.
Wie lautet das SQL Statement um den Event Namen und die Anzahl der noch offenen Tickets auszugeben?


### Anzahl noch nicht verkauften NICHT VIP Tickets
Jetzt interessiert sich der Kunde für noch nicht verkaufte Tickets, die keine VIP Tickets sind.
Wie lautet das SQL Statement, das den Event Namen und die Anzahl der noch offenen Tickets ausgibt, die keine "VIP" Tickets sind?
Das Ergebnis soll nach Anzahl offener Tickets absteigend sortiert werden.