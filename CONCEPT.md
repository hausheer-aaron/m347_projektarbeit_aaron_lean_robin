# Konzept Gruppenarbeit M347

## 1. Thema und Idee
Unsere Web-App ist eine Wetter App mit einem einfachen Tagesplan.
Sie hilft dabei, das Wetter besser mit dem eigenen Tagesablauf zu verbinden.
Die App ist hauptsächlich für Schüler und Geschäftsleute gedacht, da die Hauptfunktion für regelmässige Ragesabläufe gedacht ist.
Sie kann natürlich auch von anderen Personen genutzt werden
Wir haben diese Idee gewählt, weil wir selbst oft wissen möchten, wie das Wetter zu wichtigen Zeiten am Tag ist.

## 2. Eingesetzte Technolog
Das Frontend wird mit Blazor WebAssembly umgesetzt.
Mudblazor benutzen wir als Frontend Framework.
Für das Backend verwenden wir eine ASP.NET Core Web API.
Als Datenbank nutzen wir MySQL, um Wetterdaten und persönliche Termine zu speichern.
Die App wird mit Docker containerisiert und mit GitLab und Visual Studio entwickelt.

## 3. MVP – Minimal Viable Product
Im MVP holt das Backend die Wetterdaten von einer Wetter-API (Open-Meteo) und speichert sie in der Datenbank.
Im Frontend kann der Benutzer das Wetter ansehen und wichtige Zeiten für den Tag eintragen.
Gespeichert werden Wetterdaten sowie ein persönlicher Tagesplan, der bearbeitet und angezeigt werden kann.
Der Benutzer kann wichtige Benachrichtigungen für sein Gerät zulassen.

## 4. Nächste Schritte

---

## 5. Externe Services
