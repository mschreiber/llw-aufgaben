# Online Bücher Shop Datenbank
## Aufgabe 1 
Ihr Kunde möchte einen Online-Shop für Bücher anieten. Er beauftragt Sie, das Datenbank Design dafür zu erstellen.
Sie bekommen von ihm folgende Daten und sind mit dem Programmierer des Shops in Kontakt, der Sie bittet, ein ER Modell in der dritten Normalform zu erstellen (aufzuzeichnen).
Des weiteren bittet er Sie die CREATE Statements für mindestens 3 Tabellen zu erstellen.


Daten:


| KundenVorname | KundeNachname | KundeEMail          | KundeTelefon | BestellDatum | BestellStatus  |  BuchTitel             | BuchISBN        | AutorVorname | AutorNachname | AutorGeburtsJahr | VerlagName   | VerlagSitz | VerlagWebseite     | LagerOrt  | LagerBestand |
|---------------|---------------|---------------------|--------------|--------------|----------------|------------------------|-----------------|--------------|---------------|------------------|--------------|------------|--------------------|-----------|--------------|
| Max           | Mustermann    | max@email.com       | 0123-4567890 | 2024-02-20   | Versendet      |  Einführung in SQL     | 978-3-123456-0  | Peter        | Schmidt       | 1975             | TechBooks    | Berlin     | www.techbooks.de   | Lager A   | 12           |
| Lisa          | Müller        | lisa@email.com      | 0987-6543210 | 2024-02-18   | In Bearbeitung | Java für Anfänger      | 978-3-654321-1  | Anna         | Weber         | 1982             | CodePress    | Hamburg    | www.codepress.com  | Lager B   | 8            |
| Tom           | Schneider     | tom@email.com       | 0171-2233445 | 2024-02-15   | Abgeschlossen  |  Python Programmierung | 978-3-789012-2  | Michael      | Braun         | 1968             | DevBooks     | München    | www.devbooks.net   | Lager C   | 20           |
| Sophie        | Fischer       | sophie@email.com    | 0151-3344556 | 2024-02-12   | Storniert      |  Webentwicklung        | 978-3-567890-3  | Julia        | Wagner        | 1990             | WebTech      | Köln       | www.webtech.de     | Lager D   | 5            |
| Felix         | Bauer         | felix@email.com     | 0160-9988776 | 2024-02-10   | Versendet      |  C++ Grundlagen        | 978-3-876543-4  | Markus       | Hoffman n     | 1972             | CodeMaster   | Stuttgart  | www.codemaster.com | Lager E   | 18           |
| Marie         | Lehmann       | marie@email.com     | 0176-7788990 | 2024-02-08   | Abgeschlossen  |  Künstliche Intelligenz| 978-3-345678-5  | Sandra       | Meier         | 1985             | AI Press     | Dresden    | www.aipress.de     | Lager F   | 7            |
| Jonas         | Klein         | jonas@email.com     | 0152-5566778 | 2024-02-05   | Versendet      |  Datenbanken verstehen | 978-3-908765-6  | Tobias       | Schuste r     | 1977             | DB Experts   | Leipzig    | www.dbexperts.com  | Lager G   | 10           |
| Laura         | Wagner        | laura@email.com     | 0178-1122334 | 2024-02-03   | In Bearbeitung |  JavaScript für Profis | 978-3-432109-7  | Christian    | Becker        | 1991             | WebCoding    | Bremen     | www.webcoding.net  | Lager H   | 14           |
| David         | Schmid        | david@email.com     | 0162-4455667 | 2024-02-01   | Versendet      |  IT-Security Basics    | 978-3-543210-8  | Benjamin     | Lange         | 1983             | CyberBooks   | Frankfurt  | www.cyberbooks.de  | Lager I   | 9            |
| Julia         | Hofmann       | julia@email.com     | 0159-9988776 | 2024-01-30   | Abgeschlossen  |  HTML & CSS Design     | 978-3-210987-9  | Melanie      | König         | 1979             | DesignPress  | Nürnberg   | www.designpress.de | Lager J   | 16           |
| Niklas        | Schulte       | niklas@email.com    | 0173-6677889 | 2024-01-27   | Storniert      |  Softwareentwicklung   | 978-3-765432-1  | Patrick      | Walter        | 1980             | DevWorld     | Düsseldorf | www.devworld.com   | Lager K   | 6            |
| Anna          | Richter       | anna@email.com      | 0154-2233445 | 2024-01-25   | Versendet      |  Machine Learning      | 978-3-654098-2  | Florian      | Peters        | 1976             | AI Academy   | Hannover   | www.aiacademy.net  | Lager L   | 11           |
| Martin        | Krüger        | martin@email.com    | 0177-8899001 | 2024-01-22   | Abgeschlossen  |  Cloud Computing       | 978-3-876509-3  | Daniela      | Böhm          | 1993             | CloudPress   | Dortmund   | www.cloudpress.de  | Lager M   | 13           |
| Lena          | Meier         | lena@email.com      | 0168-6677885 | 2024-01-20   | In Bearbeitung |  DevOps für Einsteiger | 978-3-231098-4  | Andreas      | Fischer       | 1987             | DevOpsWorld  | Essen      | www.devopsworld.de | Lager N   | 15           |
| Sebastian     | Neumann       | sebastian@email.com | 0153-5566773 | 2024-01-18   | Versendet      | Agile Methoden         | 978-3-908743-5  | Stefan       | Lehmann       | 1969             | AgilePress   | Stuttgart  | www.agilepress.de  | Lager O   | 17           |


Beachte:
Eine Bestellung kann mehrere Bücher beinhalten und ein Buch kann ich mehreren Warehouses gelagert sein.

## Aufgabe 2

Ihr Kunde hat bereits eine Datenbank (siehe bookShop.sqlite) und bitte sie um folgende Informationen:
(Wenn hier von Kunden die Rede ist, dann ist das ihr Kunde, der den Webshop betreiben will).
(Käufer ist der Käufer eines Buches innerhalb des Webshops).

### 2.1 Anzahl günstige Bücher
Der Kunde möchte auf seiner Internetseite einen Teaser anbringen, in dem steht "Folgende Bücher für unter 14,- Euro!!..."
Wie lautet das SQL Statement um herauszufinden welche Bücher weniger als 14,-- Euro kosten (alle Spalten sollen ausgegeben werden)?


### 2.2 Anzahl Bücher in bestimmtem Lager
Der Kunde möchte wissen, wieviele Bücher mit den IDs 1, 3, 6 und 9 es in den Lagern gibt.
Wie lautet das SQL Statement dazu, wenn nur die Spalte mit der Anzahl und die id des Lagers ausgegeben werden sollen?

### 2.3 Autoren mit bestimmten Alter
Der Kunde möchte den Autoren, die zwischen 1940 und 1965 geboren sind, eine Nachricht schicken.
Wie lautet das SQL Statement um alle Autoren (Nur Vor- und Nachname sowie Geburtsjahr) zu finden, die zwischen 1940 und 1965 geboren sind?


### 2.4 Bücher die von einem bestimmten Käufer bestellt worden sind
Der Käufer mit der id 1 ruft beim Kunden an und möchte wissen, welche Bücher er bisher bestellt hat.
Wie lautet das SQL Statement um die Bücher ids auszugeben, die der Käufer mit der id 1 bisher bestellt hat?


### 2.5 E-Mail Versand an alle Käufer von einem bestimmten Buch
Leider gibt es eine Rückruf-Aktion eines Buches. Ihr Kunde will alle Käufer dieses Buches per E-Mail informieren und braucht aus der Datenbank daher alle E-Mail Adressen.
Wie lautet das SQL Statement um alle E-Mail Adressen der Käufer zu bekommen (nur die E-Mail Adressen), die das Buch mit dem Titel 'Wuthering Heights' gekauft haben.
Beachte, dass pro Käufer die E-Mail Adresse nur einmal ausgegeben werden soll, damit ein Käufert ggf. nicht 2 E-Mails bekommt.


### 2.6 Gewinnspiel für bestimmte Namen
Ihr Kunde hat ein Gewinnspiel gemacht. Alle Kunden, bei denen der Vor- oder Nachname mit "Jo" beginnt haben gewonnen. Ihr Kunde möchte die Personen anrufen.
Wie lautet das SQL Statement um an alle Vor- und Nachnamen, sowie Telefonnummer der Käufer raus zu suchen, bei denen der Vor- oder Nachnamen mit "Jo" beginnt.


### 2.7 Update E-Mail Adresse
Ihr Kunde bekommt von dem Käufer Bob Builder einen Anruf. Er bitte ihren Kunden seine E-Mail Adresse auf bob@builder.com zu ändern.
Wie lautet das SQL Statement um die E-Mail Adresse für den Käufer mit dem Vornamen "Bob" und dem Nachnamen "Builder" auf "bob@builder.com" zu ändern?


### 2.8 Lagerstand
Ihr Kunde möchte wissen, wieviele Bücher durchschnittlich und maximal pro Lager vorhanden sind. Da der Kunde die Daten in ein bestehendes Excel kopieren will, ist es wichtig, 
dass die Spalten mit "Lager", "MaxAnzahl" und "Durchschnitt" beschriftet sind.
Wie lautet das SQL Statement um die durchschnittliche Anzahl sowie die maximale Anzahl an Bücher pro Lager (Name, MaxCount, AvgCount) ausgegeben werden?


### 2.9 Bestseller
Ihr Kunde möchte auf seiner Webseite die Bestseller auflisten. Für ihn gilt als Grenze, dass das Buch mindestens 3 mal verkauft werden musste.
Aufgelistest werden sollen, Buchtitel, Preis und wie oft es verkauft worden ist. Wichtig ist dem Kunden auch, dass es die Bücher mit den meinsten Verkäufszahlen ganz oben erscheinen.
Wie lautet das SQL Statement um die Bücher aufzulisten, die mehr als 3 mal verkaut worden sind (Titel, Preis, Anzahl Verkäufe)?

select b.title, sum(bo.amount) as sellCount from books b left join book_orders bo on b.book_id = bo.book_id group by b.book_id having sellCount >= 3 order by sellCount desc;
