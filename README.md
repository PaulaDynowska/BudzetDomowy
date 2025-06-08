# Capybara Expenses App

## 📋 Spis treści

1. [Opis projektu](#opis-projektu)
2. [Zastosowane technologie](#zastosowane-technologie)
3. [Funkcjonalności](#funkcjonalności)
4. [Instrukcja uruchomienia](#instrukcja-uruchomienia)
5. [Dane testowe](#dane-testowe)

---

## 📌 Opis projektu
Autor: Paulina Dynowska
Nr indeksu: 51290
Projekt realizowany w ramach przedmiotu:Wzorzec MVC

„Capybara Expenses App” to aplikacja webowa umożliwiająca zarządzanie budżetem domowym z wykorzystaniem wzorca MVC. Użytkownik może rejestrować swoje wydatki i wpływy, planować przyszłe operacje, generować raporty PDF, a także filtrować transakcje według daty. Interfejs graficzny został wzbogacony o estetyczne i funkcjonalne elementy nawiązujące do stylistyki ciemnozielono-srebrnej.

---

## 🛠️ Zastosowane technologie

- **ASP.NET Core MVC (C#)** – logika aplikacji i kontrolery
- **Entity Framework Core + SQLite** – obsługa bazy danych
- **HTML + Razor** – struktura i wyświetlanie widoków
- **CSS (w tym Bootstrap)** – estetyczne stylizowanie interfejsu
- **SelectList + ViewBag** – dynamiczne listy kategorii
- **iTextSharp** – generowanie raportów PDF

---

## ✅ Funkcjonalności

- ✅ Dodawanie wydatków i wpływów
- ✅ Przypisywanie kategorii do operacji
- ✅ Lista wszystkich operacji z możliwością edycji i usuwania
- ✅ Filtrowanie operacji według daty
- ✅ Dynamiczne obliczanie salda i prezentacja jego wartości
- ✅ Eksport historii do pliku PDF
- ✅ Zakładka "Planowanie" – dodawanie i przeglądanie planowanych wydatków
- ✅ Graficznie stylizowany interfejs użytkownika
- ✅ Zliczanie sumy planowanych wydatków
- ✅ Wsparcie dla ujemnych kwot i walidacji danych

---

## 🚀 Instrukcja uruchomienia
Aplikacja jest dostępne, po uruchomieniu pod adresem: http://localhost:5199
- dotnet restore
- dotnet build
- dotnet run
### 1. Klonowanie repozytorium


git clone https://github.com/PaulaDynowska/BudzetDomowy.git
cd BudzetDomowy

2. Wymagania
.NET SDK 9.0+

Visual Studio Code (lub Visual Studio)

SQLite (zintegrowany)

3. Budowanie i uruchamianie
Aplikacja jest dostępne, po uruchomieniu pod adresem: http://localhost:5199
dotnet restore
dotnet build
dotnet run


