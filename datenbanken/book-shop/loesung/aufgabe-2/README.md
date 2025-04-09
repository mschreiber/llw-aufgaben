## Lösungen zu Aufgabe 2

Ihr Kunde hat bereits eine Datenbank (siehe bookShop.sqlite) und bitte sie um folgende Informationen:
(Wenn hier von Kunden die Rede ist, dann ist das ihr Kunde, der den Webshop betreiben will).
(Käufer ist der Käufer eines Buches innerhalb des Webshops).

### 2.1 Anzahl günstige Bücher
Der Kunde möchte auf seiner Internetseite einen Teaser anbringen, in dem steht "Folgende Bücher für unter 14,- Euro!!..
Wie lautet das SQL Statement um herauszufinden welche Bücher weniger als 14,-- Euro kosten (alle Spalten sollen ausgegeben werden)?

`select * from books where price < 14.00;`


### 2.2 Anzahl Bücher in bestimmtem Lager
Der Kunde möchte wissen, wieviele Bücher mit den IDs 1, 3, 6 und 9 es in den Lagern gibt.
Wie lautet das SQL Statement dazu, wenn nur die Spalte mit der Anzahl und die id des Lagers ausgegeben werden sollen?

`select warehouse_id, book_count from warehouse_books where book_id in (1,3,6,9);`


### 2.3 Autoren mit bestimmten Alter
Der Kunde möchte den Autoren, die zwischen 1940 und 1965 geboren sind, eine Nachricht schicken.
Wie lautet das SQL Statement um alle Autoren (Nur Vor- und Nachname sowie Geburtsjahr) zu finden, die zwischen 1940 und 1965 geboren sind?

`select first_name, last_name, year_of_birth from autors where year_of_birth between 1940 and 1965;`


### 2.4 Bücher die von einem bestimmten Käufer bestellt worden sind
Der Käufer mit der id 1 ruft beim Kunden an und möchte wissen, welche Bücher er bisher bestellt hat.
Wie lautet das SQL Statement um die Bücher ids auszugeben, die der Käufer mit der id 1 bisher bestellt hat?

`select book_id from book_orders inner join orders on orders.order_id = book_orders.order_id where customer_id = 1;`


### 2.5 E-Mail Versand an alle Käufer von einem bestimmten Buch
Leider gibt es eine Rückruf-Aktion eines Buches. Ihr Kunde will alle Käufer dieses Buches per E-Mail informieren und braucht aus der Datenbank daher alle E-Mail Adressen.
Wie lautet das SQL Statement um alle E-Mail Adressen der Käufer zu bekommen (nur die E-Mail Adressen), die das Buch mit dem Titel 'Wuthering Heights' gekauft haben.
Beachte, dass pro Käufer die E-Mail Adresse nur einmal ausgegeben werden soll, damit ein Käufert ggf. nicht 2 E-Mails bekommt.

`select distinct c.email from customers c join orders o on c.customer_id = o.customer_id join book_orders bo on o.order_id = bo.order_id join books b on bo.book_id = b.book_id where b.title = 'Wuthering Heights';`


### 2.6 Gewinnspiel für bestimmte Namen
Ihr Kunde hat ein Gewinnspiel gemacht. Alle Kunden, bei denen der Vor- oder Nachname mit "Jo" beginnt haben gewonnen. Ihr Kunde möchte die Personen anrufen.
Wie lautet das SQL Statement um an alle Vor- und Nachnamen, sowie Telefonnummer der Käufer raus zu suchen, bei denen der Vor- oder Nachnamen mit "Jo" beginnt.

`select first_name, last_name, phone from customers where first_name like 'Jo%' or last_name like 'Jo%';`

### 2.7 Update E-Mail Adresse
Ihr Kunde bekommt von dem Käufer Bob Builder einen Anruf. Er bitte ihren Kunden seine E-Mail Adresse auf bob@builder.com zu ändern.
Wie lautet das SQL Statement um die E-Mail Adresse für den Käufer mit dem Vornamen "Bob" und dem Nachnamen "Builder" auf "bob@builder.com" zu ändern?

`update customers set email='bob@builder.com' where first_name='Bob' and last_name='Builder';`


### 2.8 Lagerstand
Ihr Kunde möchte wissen, wieviele Bücher durchschnittlich und maximal pro Lager vorhanden sind. Da der Kunde die Daten in ein bestehendes Excel kopieren will, ist es wichtig, 
dass die Spalten mit "Lager", "MaxAnzahl" und "Durchschnitt" beschriftet sind.
Wie lautet das SQL Statement um die durchschnittliche Anzahl sowie die maximale Anzahl an Bücher pro Lager (Name, MaxCount, AvgCount) ausgegeben werden?

`select w.name as Name, max(wb.book_count) as MaxAnzahl, avg(wb.book_count) as Durchschnitt from warehouses w left join warehouse_books wb on w.warehouse_id = wb.warehouse_id group by w.warehouse_id;`


### 2.9 Bestseller
Ihr Kunde möchte auf seiner Webseite die Bestseller auflisten. Für ihn gilt als Grenze, dass das Buch mindestens 3 mal verkauft werden musste.
Aufgelistest werden sollen, Buchtitel, Preis und wie oft es verkauft worden ist. Wichtig ist dem Kunden auch, dass es die Bücher mit den meinsten Verkäufszahlen ganz oben erscheinen.
Wie lautet das SQL Statement um die Bücher aufzulisten, die mehr als 3 mal verkaut worden sind (Titel, Preis, Anzahl Verkäufe)?

`select b.title, sum(bo.amount) as sellCount from books b left join book_orders bo on b.book_id = bo.book_id group by b.book_id having sellCount >= 3 order by sellCount desc;`