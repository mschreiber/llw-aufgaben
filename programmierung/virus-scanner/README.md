# LLW Übungsaufgabe - Viren Scanner

Anfrage von: LLWsoft - Lehrlingsleistungswettbewerb Softwarelösungen
Projektname: Binary Analyser zur Virensuche
Ansprechpartnerin: Auri Louriod

## Projektbeschreibung
Sehr geehrtes Entwicklerteam,

im Rahmen des Lehrlingsleistungswettbewerbs für Applikationsentwickler (LLW) benötigen wir eine Software zur Analye von Binären Dateien. 
Wir sind Ziel eines Hackerangriffs geworden und einige unserer Dateien sind vermutlich durch einen Virus versäucht. Ziel des Projekts ist es, 
eine Anwendung zu entwicklen, die Binäre Dateien analysiert und auf gewisse binäre Zeichenfolgen überprüft, auswertet und eine statistische Auswertung 
ermöglicht. Je nach Ergebnis kann dann mit ziemlicher Sicherheit gesagt werden, ob diese Datei von einem Virus befallen ist oder nicht.

Im Rahmen der Anfrage soll eine Kommandozeilenanwendung (CLI) als einfach und effiziente Lösung in Betracht gezogen werden. 
Eine grafische Oberfläche (GUI) ist nicht erforderlich. Das Entwicklerteam hat die Freiheit, alternative Ansätze zu wählen, sofern diese aus 
technischer oder ergonimischer Sicht sinnvoll sind.

Im Folgenden finden sie die Anforderung an die Software:


## Anforderungen

### Grundfunktionalität: Zeichenfolgen zählen

* Analysen haben ergeben, dass Dateien, in denen die Binärfolge 0xAC gefolgt von 0xAE von einem Virus befallen sind, allerdings nur dann, 
wenn diese Folge mehr als 2 mal in der Datei vorkommt. Durch paarweise lesen von jeweils 2 bytes lässt sich das recht leicht herausfinden.

Beispiel:
0x11,0x33,0x13,0xAC,0xAE,0x02,0x03,0x01,0xFA,0xAC,0xAC,0x01,0x13   -> nicht betroffen, da die Folge 0xAC 0xAE nur einmal vorkommt.
0x11,0xAC,0xAF,0xAC,0xAE,0x02,0x03,0x01,0xAC,0xAE,0xAC,0x01,0x13   -> nicht betroffen, da die Folge nur 2x vorkommt.
0x11,0xAC,0xAF,0xAC,0xAE,0x02,0x03,0x01,0xAC,0xAE,0xAC,0xAC,0xAE   -> betroffen, da die Folge 3x vorkommt.

Die Software soll eine vom Benutzer angegebene Binärdatei einlesen (byteweise) und nach 0xAC 0xAE suchen und ausgeben ob sie versäucht ist oder nicht.
Es soll auch die Anzahl gefundener Folgen ausgegeben werden.

### Filtermöglichkeit

Um für künftige Viren gerüstet zu sein, soll der Benutzer die Möglichkeit haben, die 2 bytes nach denen gesucht wird, angeben zu können. 
Ebenso soll der Schwellwert ab wann es "virusverdächtig" ist, angegeben werden können.

### Statistische Auswertung einer Datei

Es sollen die 5 häufigsten Bytes einer Datei ausgegeben werden.
Beispiel Ausgabe:
	0x01: 9
	0x03: 7
	0x04: 6
	0x11: 5
	0xAA: 5


### Persistente Statistik

Die Top 5 Bytes sollen über mehrere Sitzungen hinweg gespeichert werden können. 

Das Speicherformat ist frei wählbar (z.b. SQLite-Datenbank).

Die Software muss sicher sein und SQL-Injection vermeiden.

