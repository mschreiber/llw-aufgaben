<h1>Number-Finder</h1>

<h2>Aufgabe 1</h2>
<p>
Erstelle ein Programm, das in der Kommandozeile aufgerufen werden kann und folgende Aufgabe übernimmt:
<ul>
<li> -Es soll möglich sein, eine beliebige Textdatei anzugeben (Pfadangabe und/oder per Command Line Parameter).</li>
<li> -Das Programm soll die Textdatei lesen und auf der Konsole ausgeben, wie oft eine gerade und wie oft eine ungerade Zahl darin vorkommt</li>
</ul>
Hinweis: Als Zahl ist eine Zahl gemeint, die mit der nächsten durch einen Abstand/Space getrennt ist!

<p>
Beispiel Textdatei:
<code>
12 34 344 2 777 931
1 49 382 93 022 111
13 777 887 777 3 45
777 3 928 2281 7777
</code>
In diesem Beispiel gibt:<br>
Gerade Zahlen: 7<br>
Ungerade Zahlen: 16
</p>
<p>
Beispiel Aufruf und Ergebnis:
<code>
   C:\>EvenOddFinder.exe C:\daten\datei1.txt

Gerade Zahlen: 5
Ungerade Zahlen: 18
</code>

</p>
</p>

<h2>Aufgabe 2</h2>
<p>
Erweitere das Programm, dass pro Zeile ausgegeben wird wie groß die Differenz zwischen geraden und ungeraden Zahlen ist.<br>
Im obigen Beispiel wäre das:<br>
12 34 344 2 777 931  -> es sind 4 gerade und 2 ungerade Zahlen, somit ist die Differenz 2<br>
1 49 382 93 022 111  -> es sind 2 gerade und 4 ungerade Zahlen, somit ist die Differenz 2<br>
13 777 887 777 3 45  -> es sind 0 gerade und 6 ungerade Zahlen, somit ist die Differenz 6<br>
777 3 928 2281 7777  -> es sind 1 gerade und 4 ungerade Zahlen, somit ist die Differenz 3<br>
<p>
Die Ausgabe wäre somit:
<code>
Differenz Zeile 1: 2
Differenz Zeile 2: 2
Differenz Zeile 3: 6
Differenz Zeile 4: 3
</code>

</p>
</p>
<h2>Aufgabe 3:</h2>
<p>
Erweitere das Programm, dass die Differenzwerte (z.B. Zeile 3: 6) irgendwo (z.B. Datenbank) persistiert und der Input Datei zugeordnet werden (C:\daten\datei1.txt). Damit soll es möglich sein, das Programm mit mehreren verschiedenen Input Dateien hintereinander aufzurufen.
Im Konsolen Programm soll es dadurch möglich sein, eine Statistik über alle bisher gelesenen Dateien zu erstellen. Die Statistik soll ausgeben:<br>
<ul>
<li>Welche Datei hat großte Summe an Differenzen (Ausgabe Dateiname und Summe der Differenzen)</li>
<li>Welche Datei hat in einer Zeile am meisten Differenzen und wie viele sind das (Ausgabe: Dateiname, Zeile, Differenz)</li>
<li>Liste die Dateien auf, die in einer oder mehr Zeilen 0 als Differenz haben</li> 
</ul>
<p>
Beispiel "datei1.txt":
<code>
12 34 344 2 777 931
1 49 382 93 022 111
13 777 887 777 3 45
777 3 928 2281 7777
</code>
</p>
<p>
Beispiel "datei2.txt":
<code>
11 33 481 6 775 933
4 51 384 95 124 119
17 779 889 774 2 44
776 2 926 2283 7779
</code>
</p>
<p>
Beispiel "datei3.txt":
<code>
22 56 468 4 999 813
5 67 294 81 134 211
15 889 777 554 8 92
881 6 731 1232 8881
</code>
</p>
Wird über die oben angeführten Dateien die Statistik aufgerufen kommt folgendes Ergebnis:
<code>
Die Datei in der die Differenz pro Datei am größten ist: datei1.txt (Summe der Differenzen: 13)
Datei mit der größten Differenz in einer Zeile: date1.txt, Zeile 3, Differenz 6
Folgende Dateien haben in einer Zeile gleich viele geraden wie ungeraden Zahlen:
-date2.txt
   Zeile 2
   Zeile 3
-datei3.txt
   Zeile 3
</code>
</p>
