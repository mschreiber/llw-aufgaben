# Lehrlingsleistungswettbewerb Applikationsentwicklung

## 1 Kundenanfrage: Fahrtkosten-Management-Tool (TravelPay)

**Anfrage von:** LLWsoft – Lehrlingsleistungswettbewerb Softwarelösungen  
**Projektname:** TravelPay – Analyse und Abrechnung von Fahrtenbüchern  
**Ansprechpartnerin:** Ada Lovelace (Projektleiterin)

---

### 1.1 Projektbeschreibung

Sehr geehrtes Entwicklerteam,

unsere Außendienstmitarbeiter erfassen ihre Fahrten in digitalen Fahrtenbüchern. Wir benötigen eine Anwendung, die diese Daten einliest, die Kilometergelder gemäß den gesetzlichen Vorgaben berechnet und eine statistische Auswertung der Reisekosten ermöglicht.

Die Anwendung soll als **Kommandozeilenanwendung (CLI)** entwickelt werden. Eine grafische Oberfläche ist nicht erforderlich. Die Daten müssen für die Buchhaltung dauerhaft in einer Datenbank gespeichert werden.

### 1.2 Anforderungen

#### 1. Grundfunktionalität: Fahrtenbuch einlesen

- Die Software soll eine Textdatei (UTF-8) einlesen.
- Format pro Zeile: `Datum;Start-KM;End-KM;Fahrzeugtyp;Zweck`  
  _Beispiel: `2025-05-12;14500;14580;PKW;Kundenbesuch Maier`_
- Die Software muss die gefahrenen Kilometer (**End-KM minus Start-KM**) pro Zeile berechnen.
- Die Zeilen sind durch die exportierende Software geprüft, es kann somit davon ausgegangen werden, dass in einer Zeile immer alle Spalten befüllt und die Formate der einzelnen Spalten immer dieselben sind.
- Was allerdings sein kann ist, dass es pro Tag mehrere Zeilen z.B. für PKW oder Motorrad gibt.

Beispieldateien sind unter data/file1.txt und file2.txt zu finden.

#### 2. Berechnung des Kilometergeldes

Basierend auf dem Fahrzeugtyp gelten folgende Sätze:

- **PKW:** 0,42 € pro km
- **Motorrad:** 0,24 € pro km
- **Sonderregel:** Fahrten, deren Zweck das Wort **"Privat"** enthält (Groß-/Kleinschreibung ignorieren), werden mit **0,00 €** berechnet.
- Es können nur PKW oder Mottorrad vorkommen (keine anderen Verkehrsmittel)
- Es soll ausgegeben werden, wieviel km-Geld jeweils pro Motorrad und PKW angefallen sind.

Beispielausgabe:
PKW-Kilometer-Geld: 2503,20
Motorrad-Kilometer-Geld: 81,60

#### 3. Persistenz

- Die Fahrten müssen in einer Form persistiert werden.
- Es muss sichergestellt sein, dass pro Tag und Typ nur ein Eintrag vorhanden ist.
- Gibt es pro Tag und Typ schon einen Eintrag in der Persistenz, muss der alte gelöscht werden.
- Es müssen die Daten persistiert werden, damit im Nachgang folgende Statistik über alle eingelesenen Dateien gemacht werden kann:

#### 4. Persistente Statistik

Das Tool muss folgende aggregierte Daten aus der Datenbank für einen **bestimmten Zeitraum** abrufen können:

- **Summe pro Fahrzeugtyp:** Das gesamte auszuzahlende Kilometergeld aller Einträge pro Fahrzeugtyp.
- **Gesamtsumme:** Das gesamte auszuzahlende Kilometergeld aller Einträge.

Beispielausgabe für den Zeitraum 1.1.2025 - 31.12.2025:

```
Summe vom 01.01.2025 - 31.12.2025:
PKW-Kilometer-Geld: 14506,60
Motorrad-Kilometer-Geld: 2815,60
```

### 1.3 Technische Vorgaben

- **Struktur:** Sauberer, modularer Code (Trennung von Logik und Datenzugriff).
- **Dokumentation:** Quellcode muss ausreichend kommentiert sein (Methoden, Parameter, Rückgabewerte).

### 1.4 Dokumentation und Abgabe

- **README.md:** Kurze Anleitung zur Ausführung und Bedienung der CLI-Befehle.
- **Bestandteile:** \* Lauffähiges Programm (kompiliert/ausführbar)
  - Vollständiger Quellcode
  - README-Datei
  - Ggf. die erzeugte Datenbank-Datei (`.db`)

### 1.5 Rahmenbedingungen

- Internetnutzung nur für Recherche und Dependencies.
- KI-Tools oder externe Codeübernahme sind untersagt.
- Eigene Hardware ist nicht gestattet.

---

Viel Erfolg bei der Umsetzung!
