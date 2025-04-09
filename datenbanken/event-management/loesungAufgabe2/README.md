## Aufgabe 2

### Events mit Ticket Anzahl
Die Presse möchte mehr über Klein-Events wissen. Sie interessiert sich für Events, wo es weniger als 1000 Tickets geben wird.
Wie lautet das SQL Statement, das alle Namen und das Startdatum der Events ausgibt, die weniger als 1000 Tickets haben werden?

``` sql
select name, start_date from events where max_tickets < 1000;
```

### An wen wurden Tickets verkauft
Es gibt ein Problem mit der Veranstaltung "Rock am Platz". Der Kunde bittet sie, alle die für dieses Event eine Karte gekauft haben, herauszufinden, damit er ihnen eine E-Mail schreiben kann.
Wie lautet das SQL Statement, das die Namen (Vor- und Nachname) sowie die E-Mail Adressen der Personen ausgibt, die Tickets für "Rock am Platz" gekauft haben.
Wenn jemand mehrere Tickets gekauft hat, soll der Name/Mail nur einmal aufscheinen.

``` sql
select distinct p.first_name, p.last_name, p.email from participants p join participations pp on pp.participant_id = p.participant_id join events e on e.event_id = pp.event_id 
where e.name = 'Rock am Platz'
```

### VIP Tickets für ein Event
Der Veranstalter möchte ein Willkommensgeschenk für alle VIP Ticket Besitzer für die Veranstaltung "Rock am Platz" organisieren.
Wie lautet das SQL Statement um die Namen auszugeben, die für die Veranstaltung "Rock am Platz" ein "VIP" Ticket gekauft haben?

``` sql
select p.first_name, p.last_name from participants p 
join participations pp on pp.participant_id = p.participant_id and pp.ticket_id = t.ticket_id
join events e on e.event_id = pp.event_id 
join tickets t on t.event_id = e.event_id
join ticket_types tt on tt.ticket_type_id = t.ticket_type_id
where e.name = 'Rock am Platz' and tt.name = 'VIP'