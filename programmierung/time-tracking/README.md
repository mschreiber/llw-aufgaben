# Zeittracking-Analyse-Tool

## Projektbeschreibung

Für eine Zeiterfassungssoftware soll ein Tool zur Analyse von Arbeitszeitprotokollen entwickelt werden. Ziel ist es, aus Textdateien erfasste Zeitbuchungen einzulesen, zu verarbeiten und statistisch auszuwerten.

Die Anwendung wird als **Kommandozeilenprogramm (CLI)** umgesetzt.

---

## Eingabedateien

- Format: **UTF-8**
- Jede Datei enthält **genau einen Benutzer**
- Aufbau der Datei:

### Beispiel

```
UserID: 101 | Name: Max Mustermann
2026-03-30 | Projekt: Website | Start: 08:00 | Ende: 10:30
2026-03-30 | Projekt: Meeting | Start: 11:00 | Ende: 12:00
2026-03-31 | Projekt: App | Start: 09:15 | Ende: 11:45
```

### Struktur

- **Erste Zeile:**
  - UserID (eindeutig)
  - Name des Benutzers

- **Weitere Zeilen:**
  - Datum
  - Projektname
  - Startzeit
  - Endzeit

---

## Anforderungen

### 1. Datei einlesen & verarbeiten

- Einlesen von einer oder mehreren Dateien
- Parsen der ersten Zeile (UserID + Name)
- Parsen aller Zeitbuchungen
- Berechnung der Dauer:
  ```
  Dauer = Ende - Start
  ```
- Es darf davon ausgegangen werden, dass jede Zeile korrekte Daten enthält

---

### 2. Ausgabe/Auswertung (pro Datei)

- Gesamtarbeitszeit
- Arbeitszeit pro Projekt
- Top 3 Projekte mit der meisten Zeit

---

### 4. Persistenz

- Speicherung der Daten (z. B. JSON oder SQLite)
- Anforderungen:
  - Mehrere Benutzerdateien speicherbar
  - Daten bleiben dauerhaft erhalten
  - UserID ist eindeutiger Schlüssel

---

### 5. Persistente Statistik

Über alle gespeicherten Daten:

- Gesamtarbeitszeit pro Benutzer
- Gesamtarbeitszeit pro Projekt
- Gesamtarbeitszeit über alle Projekte und alle Mitarbeiter
- Durchschnittliche Arbeitszeit pro Mitarbeiter (projektunabhängig)

---

## Technische Vorgaben

- Programmiersprache frei wählbar
- Code muss:
  - sauber strukturiert sein
  - logisch getrennt sein (Parsing, Logik, Speicherung)
  - dokumentiert sein (Funktionen, Parameter, Rückgabewerte)

---

## Testen

- Funktionale Tests durchführen

---

## Abgabe

Die Abgabe muss enthalten:

- Lauffähiges Programm
- Vollständiger Quellcode
- Diese README-Datei mit:
  - Anleitung zur Ausführung
  - Beispielbefehle

---

## Rahmenbedingungen

- Internet nur für Recherche und Dependencies
- Keine KI-generierten Lösungen oder fremder Code
- Keine eigenen Geräte erlaubt
- Verstöße führen zur negativen Bewertung

---
