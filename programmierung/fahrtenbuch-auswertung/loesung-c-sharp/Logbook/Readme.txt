Logbook - Software

Anwenderdokumentation:
Das Programm kann im Verzeichnis 

./bin/Debug/net10.0/Logbook.exe 

gestartet werden. 
Datenbank wird automatisch erstellt.
Nach dem Start hat der Benutzer die Möglichkeit durch Eingabe 
von "1" eine neue Datei einzulesen. Mit "2" die Statistik ausgeben lassen.

Datei einlesen:
Es wird nach einem Pfad gefragt inkl. Dateiname (z.B. "C:/tmp/file.txt")
Wird der Pfad nicht gefunden, wird das Programm beendet.
Die Datei wird eingelesen und das Ergebnis dieser Datei ausgegeben und in
die Datenbank gespeichert.

Statistik:
Es wird die Statistik aller bisher gelesener Dateien ausgegeben.





















Technische Doku:
Beim Einlesen werden 2 Dictionaries verwendet, in denen der Key
das Datum, das berechnete Kilometergeld der Value ist.
Das Gesamtkilometergeld wird per Summe der Dictionary Values "berchnet".
Die Dictionaries werden dann in einer DB Tabelle gespeichert.
Die Tabelle hat ID, Datum, Type und KM-Geld als Spalten.
Beim Speichern in die DB wird zuerst für das Datum und der Typ 
alle Einträge gelöscht und dann der "neue" Wert gespeichert.

TODOS:
-Alles in einer Klasse -> Besser in mehrere Klassen z.B. PersistenzService, ParseService, ... aufteilen
-Besseres Exception Handling
-Programm in einer Schleife (3...Abbrechen getippt wird)
-Mehrere Tabellen (Typ, kmgeld)
