<h1>XML-Finder</h1>

<h2>Aufgabe 1</h2>
<p>
Erstelle ein Programm, das in der Kommandozeile aufgerufen werden kann und folgende Aufgabe übernimmt:
<ul>
<li> -Es soll möglich sein, eine beliebige XML Datei anzugeben (Pfadangabe und/oder per Command Line Parameter).</li>
<li> -Das Programm soll die XML Datei lesen (dabei soll keine XML Parsing Library verwendet werden) und auf der Konsole ausgeben, wie of der Tag &lt;book&gt; vorkommt.</ul>
Es kann davon ausgegangen werden, dass die Datei in UTF-8 Encoding vorliegt und dass sie nur XML Tags enthält. Die Formatierung kann allerdings unterschiedlich sein. Ebenso müssen nicht zwingend alle Book XML Tags mit nur "Title" vorkommen, es können zwischen Book-Open und Book-End mehrere (valide) andere Tags über mehrere Zeilen sein.
<p>
Beispiel Textdatei:
<code>
<xml>
  <book>
    <title>Hello</title>
  </book>
  <book>
    <title>World</title>
  </book>
</xml>
</code>
In diesem Beispiel kommt der Tag 2 mal vor
</p>
<p>
Beispiel Aufruf und Ergebnis:
<code>
   C:\>XmlErrorFinder.exe C:\daten\datei1.xml

In der Datei "C:\daten\datei1.xml" kommt der Tag &lt;book&gt; 2 mal vor!
</code>

</p>
</p>

<h2>Aufgabe 2</h2>
<p>
Erweitere das Programm, dass auch ausgegeben wird, wie oft der "Closing-Tag" &lt;/book&gt; vorkommt:
<p>
Beispiel mit dem obigen Input:
<code>
Open-Tag: 2x
Close-Tag: 2x
</code>

</p>
</p>
<h2>Aufgabe 3:</h2>
<p>
Erweitere das Programm, dass die Zeile erkennt, wo ein Book-Closing-Tag fehlt (Annahme: Nur ein Book Tag und nur davon ein Closing Tag müssen erkannt werden, ein Closing Tag muss immer vor einem neuen Open Tag kommen). <br>
<p>
Beispiel: "datei2.xml"
<code>
<xml>
  <book>
    <title>Hello</title>
    <author>Max Mustermann</author>
  </book>
  <book>                                  <---- für diesen Open Tag fehlt der Closing Tag
      <title>World</title>
      <author>Linda Musterfrau</author>
  <book>                                  <---- hier unten vor diesem Open Tag
    <title>I am here</title>
  </book>
</xml>
</code>
</p>
Wird über die oben angeführten Dateien mit dem Programm gefüttert, muss als Ausgabe kommen:
<code>
-Das Book-Open-Tag von Zeile 6 wird nie geschlossen. 
-Vor Zeile 9 fehlt ein Book-Close-Tag 
</code>
</p>
<h2>Aufgabe 4:</h2>
<p>
Erweitere das Programm, dass in einer Persistenz gespeichert wird, wie oft die Open und Close Tags vorkommen. Ebenfalls soll eine kleine Statistik ausgegeben werden können. <br>
<p>
Statistik über
<ul>
  <li>welche bisher geparste Datei hat am Meisten Tags (egal ob open oder closed)</li>
  <li>welche bisher geparste Datei hat am Wenigsten Tags (egal ob open oder closed)</li>
  <li>Durchschnitt aller Dateien</li>
  <li>Welche Datei hat am meisten fehlende Closed Tags</li>
</ul>
<code>
</p>
</p>
