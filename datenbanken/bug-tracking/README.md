# Bug Tracking System

## Aufgabe 1
Ihr Kunde möchte einen Bug-Tracking System entwickeln und hat keine Datenbank Erfahrung. Er bittet sie basieren auf dem bisherigen Excel, ein ER Modell in der dritten Normalform zu erstellen (aufzuzeichnen).


### Daten:

| BugTitle                    | BugDescription                                                                | BugReproducable | BugReportDate | ReporterFirstname | ReporterLastname | ReporterEMail               | BugStatus  | BugStatusChangeDate  | BugCategoryShort | BugCategoryLong     | BugPrioShort | BugPrioLong              | AssigneeFirstname | AssigneeLastname | AssigneeEMail             |
|-----------------------------|-------------------------------------------------------------------------------|-----------------|---------------|-------------------|------------------|-----------------------------|------------|----------------------|------------------|---------------------|--------------|--------------------------|-------------------|------------------|---------------------------|
| Login Button Not Working    | The login button does not respond when clicked.                               | true             | 2025-03-01    | Max               | Mustermann       | max.mustermann@example.com  | Open       | 2025-03-02           | UI               | User Interface      | High         | Critical                 | Anna              | Schmidt          | anna.schmidt@example.com   |
| App Crashes on Launch       | The app crashes immediately after startup on Android devices.                 | false            | 2025-03-03    | Laura             | Becker           | laura.becker@example.com    | In Progress| 2025-03-04           | Crash            | Application Crash   | High         | Major                    | Felix             | Müller           | felix.mueller@example.com  |
| Incorrect Price Displayed   | The displayed price is incorrect during checkout.                             | true           | 2025-03-04    | Karl              | Meier            | karl.meier@example.com      | Open       | 2025-03-04           | Backend          | Pricing Error       | Medium       | Moderate                 | John              | Klein            | john.klein@example.com     |
| Page Not Found (404) Error  | Clicking on a product link leads to a 404 error page.                         | true            | 2025-03-05    | Sophie            | Fischer          | sophie.fischer@example.com  | Open       | 2025-03-06           | Link             | Broken Links        | Low          | Minor                    | Michael           | König            | michael.koenig@example.com |
| Unable to Submit Form       | The form submission button is unresponsive on mobile browsers.                | true            | 2025-03-06    | Tom               | Wagner           | tom.wagner@example.com      | In Progress| 2025-03-07           | UI               | Form Issue          | High         | Critical                 | Lara              | Hoffmann         | lara.hoffmann@example.com  |
| Missing Images on Homepage  | Images are missing on the homepage in the gallery section.                    | false            | 2025-03-07    | Julia             | Lange            | julia.lange@example.com     | Open       | 2025-03-08           | UI               | Visual Glitch       | Medium       | Major                    | Thomas            | Weber            | thomas.weber@example.com   |
| Search Function Broken      | The search bar returns no results even when valid terms are entered.          | true           | 2025-03-08    | Markus            | Richter          | markus.richter@example.com  | Open       | 2025-03-09           | Backend          | Search Issue        | High         | Critical                 | Nadine            | Müller           | nadine.mueller@example.com |
| User Permissions Error      | Users are unable to access certain admin pages due to permission issues.      | true            | 2025-03-09    | Peter             | Schulz           | peter.schulz@example.com    | In Progress| 2025-03-10           | Security         | Permission Issue    | High         | Critical                 | Patrick           | Zimmermann       | patrick.zimmermann@example.com|
| Slow Loading Time           | The website takes more than 10 seconds to load on mobile devices.             | true            | 2025-03-10    | Anna              | Weber            | anna.weber@example.com      | Open       | 2025-03-11           | Performance      | Slow Performance    | Medium       | Major                    | Erik              | Schneider        | erik.schneider@example.com  |
| Error in PDF Download       | PDF download does not work after clicking the download button.                | true            | 2025-03-11    | Lukas             | Braun            | lukas.braun@example.com     | Open       | 2025-03-12           | Backend          | File Download Issue | Low          | Minor                    | Sven              | Fischer          | sven.fischer@example.com   |


**Hinweis:** Ein Bug "durchläuft" während des Prozesses verschiedene Stati. 
Reporter und Assignee sind verschiedne Personenkreise und müssen nicht in einer gemeinsamen Datenbank gespeichert sein.


## Aufgabe 2

Ihr Kunde hat bereits eine Datenbank (siehe bugs.sqlite) und bitte sie um folgende Informationen:
(Wenn hier von Kunden die Rede ist, dann ist das ihr Kunde, der das Bugtracking Tool betreiben will).

### 2.1 Nicht Reproduzierbare Bugs
Der Kunde möchte wissen, welche Bugs in seiner Datenbank nicht reproduzierbar sind.
Wie lautet das SQL Statement um den Titel der nicht reproduierbaren Bugs heraus zu finden?

### 2.2 Anzahl UI Bugs
Der Kunde möchte wissen, wieviel UI Bugs er hat.
Wie lautet das SQL Statement um den Titel der Bugs auszugeben, die in der Kategorie mit dem short_name "UI" sind?

### 2.3 Anzahl "High" Prio Bugs
Der Kunde möchte wissen, wieviele Bugs mit der Prio High gemeldet worden sind.
Wie lautet das SQL Statement um die Anzahl an Bugs heraus zu finden, die die Priorität "High" haben?

### 2.4 E-Mail Adressen der Reporter Low Prio Bugs
Der Kunde will alle Reporter von noch Low Prio Bugs darüber informieren, dass sie nicht mehr in dieser Release gelöst werden.
Wie lautet das SQL Statement um die E-Mail Adressen aller Bugs auszugeben, die als Priorität "Low" haben. 
Es sollten keine Duplikate im Resultat vorhanden sein.

### 2.5 Assignee mit den meisten Bugs
Ein Mitarbeiter beklagt sich bei ihrem Kunden, dass er zuviele Bugs zugewiesen hat. Der Kunde bittet sie das zu überprüfen
Wie lautet das SQL Statement um die Anzahl an Bugs pro Assignee zu finden. Es soll der Vorname und Nachname des Assignes sowie die Anzahl an Bugs als "Anzahl" ausgegeben werden und nur von den Top 5.

### 2.6 Assignee mit mehr als 3 Bugs
Nachdem die Beschwerden nicht weniger werden, möchte ihr Kunde wissen, welcher Assignee mehr als 3 Bugs hat. 
Wie lautet das SQL Statement um die Vor- und Nachnamen und Anzahl Bugs aller Assignees auszugeben, denen mehr als 2 Bugs zugewiesen sind?

### 2.7 Bugs in einem bestimmten Zeitfenster
Ihr Kunde vermutet, gehackt worden zu sein und gibt an, dass in der Datenbank am 03.04.2025 zwischen 8:43 und 8:44 mehr als 100 Bugs eingepflegt worden sind.
Wie lautet das SQL Statement das alle Spalten von den Bugs ausgibt, die in dem besagten Zeitraum reported worden sind?


### 2.8 Bugs in denen Error vorkommt
Ihr Kunde weiß, dass es einmal mehrere Bugs gab, die etwas mit einem "404" error zu tun hatten. Er bittet sie das zu überprüfen.
Er möchte wissen, wer diesen Bug reported hat.
Wie lautet das SQL Statement, dass den Vor- und Nachnamen des Reporters ausgibt, der Bugs eingepflegt hat, die im Titel "404" oder "error" haben (Case insensitiv);

### 2.9 Nicht zugeordnete High Prio Bugs
Die Reporter beklagen sich bei ihrem Kunden, dass es viele Bugs gibt, die noch keiner Person zugeordnet sind. 
Wie lautet das SQL Statement das den Titel und das Reporting Date von den Bugs ausgibt, die noch keiner Person (Assignee) zugewiesen sind?

### 2.10 Assignee mit bestimmtem Anfangsbuchstaben
Ihr Kunde hatte ein Gewinnspiel ausgeschrieben. Es haben alle Assignees gewonnen, deren Vorname mit einem Buchstaben von A - M beginnt.
Wie lautet das SQL Statement, das die e-Mail Adressen aller Assignees ausgibt, deren Vorname mit a-f beginnt. Achtung: Es sollen nur die
ausgegeben werden, die auch einen Bug assigned sind und keine doppelten Mail Adressen!

### 2.11 Open Bugs
Ihr Kunde möchte wissen, wieviele Bugs auf "Open" stehen. Open sind die Bugs, die erst einen Statuswechseln in der Tabelle bug_states haben.
Wie lautet das SQL Statement, dass alle Ids und Titel der Bugs ausgibt, bei denen es nur einen Eintrag in der bug_states tabelle gibt?

