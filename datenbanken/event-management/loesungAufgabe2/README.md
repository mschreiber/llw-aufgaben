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
select distinct p.first_name, p.last_name, p.email from participants p 
join participations pp on pp.participant_id = p.participant_id 
join events e on e.event_id = pp.event_id 
where e.name = 'Rock am Platz'
```

### VIP Tickets für ein Event
Der Veranstalter möchte ein Willkommensgeschenk für alle VIP Ticket Besitzer für die Veranstaltung "Rock am Platz" organisieren.
Wie lautet das SQL Statement um die Namen auszugeben, die für die Veranstaltung "Rock am Platz" ein "VIP" Ticket gekauft haben?

``` sql
select p.first_name, p.last_name
from participants p
join participations pp ON pp.participant_id = p.participant_id
join tickets t ON pp.ticket_id = t.ticket_id
join events e ON t.event_id = e.event_id
join ticket_types tt ON t.ticket_type_id = tt.ticket_type_id
where e.name = 'Rock am Platz' AND tt.name = 'VIP';
```

### Events mit 2 Veranstalter
Ihr Kunde möchte wissen, welche der Veranstaltungen von 2 Veranstalter gemeinsam organisiert werden.
Wie lautet das SQL Statement, das den Namen der Events ausgibt, die von genau 2 Veranstaltern organisiert werden?

``` sql
select e.name from organizers o 
join organizer_events oe on o.organizer_id = oe.organizer_id 
join events e on e.event_id = oe.event_id
group by e.event_id having count(o.organizer_id) == 2;
```

### Preis eines Tickets
Frau "Klein" ruft an und beklagt sich über den hohen Ticketpreis.
Wie lautet das SQL Statement um den Event Namen und den Preis für die Tickets auszugeben, die von Frau "Klein" gekauft wurden?

``` sql
select e.name, t.price 
from events e
join participations pp on pp.event_id = e.event_id
join participants p on p.participant_id = pp.participant_id
join tickets t on t.event_id = e.event_id and pp.ticket_id = t.ticket_id
where p.last_name = 'Klein';

```

### Noch nicht verkaufte Tickets
Ihr Kund will wissen, welche Tickets noch nicht verkauft worden sind.
Wie lautet das SQL Statement, dass den Namen des Events und den Ticketpreis für alle Tickets ausgibt, die noch nicht verkauft worden sind?

``` sql
select e.name, t.price from tickets t
join events e on e.event_id = t.event_id
left join participations p on p.ticket_id = t.ticket_id where p.ticket_id is null;
```

### Anzahl noch nicht verkauften Tickets
Jetzt möchte der Kunde nur die Anzahl der noch nicht verkauften Tickets pro Event wissen.
Wie lautet das SQL Statement um den Event Namen und die Anzahl der noch offenen Tickets auszugeben?

``` sql
select e.name, count(t.ticket_id) from tickets t
join events e on e.event_id = t.event_id
left join participations p on p.ticket_id = t.ticket_id 
where p.ticket_id is null
group by e.event_id;
```

### Anzahl noch nicht verkauften NICHT VIP Tickets
Jetzt interessiert sich der Kunde für noch nicht verkaufte Tickets, die keine VIP Tickets sind.
Wie lautet das SQL Statement, das den Event Namen und die Anzahl der noch offenen Tickets ausgibt, die keine "VIP" Tickets sind?
Das Ergebnis soll nach Anzahl offener Tickets absteigend sortiert werden.

``` sql
select e.name, count(t.ticket_id) as cnt from tickets t
join events e on e.event_id = t.event_id
join ticket_types tt on tt.ticket_type_id = t.ticket_type_id
left join participations p on p.ticket_id = t.ticket_id 
where p.ticket_id is null
and tt.name != 'VIP'
group by e.event_id
order by cnt desc;
```