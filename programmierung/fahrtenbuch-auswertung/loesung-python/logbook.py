import sqlite3
import csv
import os

# Konstanten
KM_PRICE_CAR = 0.42
KM_PRICE_MOTORCYCLE = 0.24
DB_NAME = "milageAllowance.db"

def main():
    while True:
        print("------------------------------")
        print("Fahrtenbuch - App")
        print("------------------------------")
        print("1 ... Fahrtendatei einlesen")
        print("2 ... Auswertung über Zeitraum anzeigen")
        print("3 ... Beenden")
        
        auswahl = input("Geben Sie Ihre Auswahl ein: ")
        
        if auswahl == "1":
            parse_files()
        elif auswahl == "2":
            show_statistics()
        elif auswahl == "3":
            print("Auf Wiedersehen!")
            break
        else:
            print("Ungültige Auswahl!")

def parse_files():
    pfad = input("Geben Sie den Pfad zur Fahrtendatei ein: ")
    
    if not os.path.exists(pfad):
        print("Ungültiger Pfad!")
        return

    # Dictionaries für die Daten (Datum als Key, Preis als Value)
    car_data = {}
    motorcycle_data = {}

    try:
        with open(pfad, mode='r', encoding='utf-8') as f:
            reader = csv.reader(f, delimiter=';')
            next(reader)  # Header überspringen
            
            for row in reader:
                # Zeile verarbeiten: [Datum, StartKM, EndKM, Typ, Zweck]
                date, start_km, end_km, v_type, purpose = row
                
                if "privat" in purpose.lower():
                    continue
                
                distance = int(end_km) - int(start_km)
                
                if v_type == "PKW":
                    car_data[date] = car_data.get(date, 0.0) + (distance * KM_PRICE_CAR)
                elif v_type == "Motorrad":
                    motorcycle_data[date] = motorcycle_data.get(date, 0.0) + (distance * KM_PRICE_MOTORCYCLE)

        persist(car_data, motorcycle_data)
        
        print(f"PKW-Kilometer-Geld:        {sum(car_data.values()):10.2f}€")
        print(f"Motorrad-Kilometer-Geld:   {sum(motorcycle_data.values()):10.2f}€")

    except Exception as e:
        print(f"Fehler beim Einlesen: {e}")

def persist(car_data, motorcycle_data):
    try:
        conn = sqlite3.connect(DB_NAME)
        cursor = conn.cursor()

        # Tabelle erstellen
        cursor.execute("""
            CREATE TABLE IF NOT EXISTS data (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                kmDate TEXT,
                kmPrice REAL,
                type TEXT
            )
        """)

        # Daten einfügen (Logik: Erst löschen, falls vorhanden, dann neu einfügen)
        def save_to_db(data_dict, type_str):
            for date, price in data_dict.items():
                print(f"Persistiere {type_str}-Daten: {date} - {price:.2f}")
                # Altes Duplikat entfernen
                cursor.execute("DELETE FROM data WHERE type = ? AND kmDate = ?", (type_str, date))
                # Neu einfügen
                cursor.execute("INSERT INTO data (kmDate, kmPrice, type) VALUES (?, ?, ?)", 
                               (date, price, type_str))

        save_to_db(car_data, "PKW")
        save_to_db(motorcycle_data, "Motorrad")

        conn.commit()
        conn.close()
    except sqlite3.Error as e:
        print(f"Datenbankfehler: {e}")

def show_statistics():
    start_date = input("Startdatum eingeben (yyyy-MM-dd): ")
    end_date = input("Enddatum eingeben (yyyy-MM-dd): ")

    try:
        conn = sqlite3.connect(DB_NAME)
        cursor = conn.cursor()

        types = ["PKW", "Motorrad"]
        for t in types:
            cursor.execute("""
                SELECT SUM(kmPrice) FROM data 
                WHERE type = ? AND kmDate BETWEEN ? AND ?
            """, (t, start_date, end_date))
            
            result = cursor.fetchone()[0]
            price = result if result is not None else 0.0
            
            print(f"{t}-Kilometer-Geld: {price:10.2f}€")

        conn.close()
    except sqlite3.Error as e:
        print(f"Fehler bei der Abfrage: {e}")

if __name__ == "__main__":
    main()