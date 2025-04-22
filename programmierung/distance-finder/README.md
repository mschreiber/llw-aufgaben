Erstelle ein Programm das die Textdatei(en) aus der Ordner data liest und verschiedenes berechnet.
In der Datei sind pro Zeile 2 Zahlen (durch fixe Anzahl an Spaces getrennt).

### Aufgabe 1 ###
Finde heraus, was die Summer der Unterschiede pro Zeile ist:
<code>
3   4          Unterschied: 1
4   3          Unterschied: 1 
2   5          Unterschied: 3
1   3          Unterschied: 2
3   9          Unterschied: 5
3   3          Unterschied: 0
</code>
Somit wäre bei diesem Beispiel das Ergebnis:
<code>
1+1+3+2+5+0 = 12
</code>
Wird das Programm mit small.txt aufgerufen, kommt muss also 12 herauskommen. 
Wieviel kommt mit der Datei big.txt heraus?

### Aufgabe 2 ###
Ändere das Programm, dass es nicht die Zahlen pro Zeile vergleicht sondern wie folgt:<br>
Die kleinste Zahl mit von der linken Spalte, soll mit der kleinsten von der Rechten verglichen werden.<br>
Die zweitkleinste Zahl von der linken Spalte soll mit der zweitkleinsten von der Rechten verglichen werden etc.<br>
Beispiel small.txt:

In the example list above, the pairs and distances would be as follows:

Die kleinste Zahl auf der linken Seite ist 1, die auf der Rechten 3, Unterschied somit 2.<br>
Die zweitkleinste links ist 2, auf der Rechten 3, somit 1 Unterschied.<br>
Die drittkleinste ist auf beiden Seiten 3, somit Unterschied: 0<br>
Die viertkleinste ist 3 und 4, Unterschied somit 1<br>
Die fünftkleinste ist 3 und 4, Unterschied somit 2<br>
Die größte links ist 4, rechts 9, Unterschied somit 5<br>
Alle Unterschiede zusammengezählt ergeben:<br>
2 + 1 + 0 + 1 + 2 + 5 = 11!<br>
<br>
Wie lautet dann der Unterschied von big.txt?

### Aufgabe 3 ####
Erweitere das Programm, dass der Unterschied irgendwo persistiert wird. Wird das Programm mit unterschiedlichen Dateien gefüttert, soll 
folgende Statistik ausgegeben werden:<br>
<ul>
<li>Welche Datei hat den größten Unterschied</li>
<li>Welche Datei hat den kleinsten Unterschied</li>
<li>Wie lautet der Durchschnitts-Unterschied</li>
</ul>
