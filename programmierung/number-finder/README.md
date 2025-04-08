<h1>Number-Finder</h1>

<h2>Aufgabe 1</h2>
<p>
Erstelle ein Programm, das in der Kommandozeile aufgerufen werden kann und folgende Aufgabe übernimmt:
<ul>
<li> -Es soll möglich sein, eine beliebige Textdatei anzugeben (Pfadangabe und/oder per Command Line Parameter).</li>
<li> -Das Programm soll die Textdatei lesen und auf der Konsole ausgeben, wie oft in der Textdatei die Zahl 777 vorkommt. 
</ul>

<p>
Beispiel Textdatei:
<code>
11 34 347 7 777 931
1 49 382 93 022 111
13 777 887 777 3 45
777 3 928 2281 7777
</code>
In diesem Beispiel kommt die Zahl 777 4mal vor, 1x Zeile 1, 2x Zeile 3, 1x Zeile 4. Hinweis 7777 in der letzten Zeile wird nicht gezählt!
</p>
<p>
Beispiel Aufruf und Ergebnis:
<code>
   C:\>NumberFinder.exe C:\daten\datei1.txt

In der Datei "C:\daten\datei1.txt" kommt die Zahl 4 mal vor!
</code>

</p>
</p>

<h2>Aufgabe 2</h2>
<p>
Erweitere das Programm, dass pro Zeile ausgegeben wird, wie oft die Zahl gefunden wurde. Wenn in einer Zeile die Zahl nicht gefunden wird, wird für diese Zeile nichts ausgegeben:
<p>
Beispiel mit dem obigen Input:
<code>
Zeile 1: 1x
Zeile 3: 2x
Zeile 4: 1x

Summe: 4x
</code>

</p>
</p>
<h2>Aufgabe 3:</h2>
<p>
Erweitere das Programm, dass die gefundenen Zeilen und deren Wert (z.B. Zeile 3: 2x) irgendwo (z.B. Datenbank) persistiert und der Input Datei zugeordnet werden (C:\daten\datei1.txt). Damit soll es möglich sein, das Programm mit mehreren verschiedenen Input Dateien hintereinander aufzurufen.
Im Konsolen Programm soll es dadurch möglich sein, eine Statistik über alle Dateien zu erstellen. Die Statistik soll ausgeben:<br>
<ul>
<li>Welche Datei hat am meisten Funde und wie viele</li>
<li>Welche Datei hat in einer Zeile am meisten und wie viele</li>
<li>Welche Dateien haben in der Zeile zwei, drei Matches</li>
<li>Welche Dateien haben mehr als 4 Matches</li> 
</ul>
<p>
Beispiel "datei1.txt":
<code>
11 34 347 7 777 931
1 49 382 93 022 111
13 777 887 777 3 45
777 3 928 2281 7777
</code>
</p>
<p>
Beispiel "datei2.txt":
<code>
13 34 443 7 555 931
1 49 382 44 022 233
13 777 887 000 3 54
777 777 777 777 777
</code>
</p>
<p>
Beispiel "datei3.txt":
<code>
13 777 777 7 55 931
1 49 777 44 777 777
13 888 887 000 3 54
775 7 000 65 6 65 6
</code>
</p>
Wird über die oben angeführten Dateien die Statistik aufgerufen kommt folgendes Ergebnis:
<code>
Datei mit den meisten Matches: datei2.txt : 6 Matches
Datei mit den meisten Matches pro Zeile: date2.txt: 5 Matches in Zeile 4
Datei "datei3.txt" hat in Zeile 2, drei Matches
Folgende Datei(en) haben mehr als 4 Matches:
-datei2.txt
-datei3.txt
</code>
</p>
