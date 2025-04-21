# Neuca

Recruitment task for Neuca company

## Zadanie

Proszę zaimplementować system sprzedaży biletów lotniczych scharakteryzowany poniżej. Interesuje nas tylko implementacja modelu domenowego. Nie musisz implementować API i warstwy infrastruktury np. zapis i odczyt z bazy danych. Rozwiązanie zadania możesz wrzucić na repozytorium github i podesłać do nas link lub podesłać kod źródłowy mailem. Rozwiązanie musi być zgodne z zasadami SOLID.

Pojęcia podstawowe:
**Flight Id** – oznacza unikalne ID lotu. Składa się z trzech liter (oznaczający kod IATA linii lotniczej fizycznie wykonującej lot), 5-ciu cyfr i trzech liter na końcu. Przykład: KLM 12345 BCA
**Tenant** – system umożliwia użycie przez wielu klientów na raz. Takiego klienta określa się mianem tenant. Każdy tenant należy do jednej z grup A lub B. Uwaga! Istnieje różnica w funkcjonalnościach pomiędzy tenant-em typu A i B.
**Flight** - oprócz ID zawiera trasę od, do, godzinę i dni tygodnia wylotu.
**Flight price** – cena lotu, która może być różna dla różnych dat i godzin
USE CASES (dla obydwu grup tenant-ów)
• Możliwość ręcznego dodania lotu
• Możliwość zakupienia danego lotu wyszukanego po ID
• Możliwość zastosowania zniżki do ceny:

Do ceny lotu można dodać zniżkę, zawsze o wysokości 5 euro. Zniżki działają per criteria i się kumulują, przy czym cena lotu nie może wynieść mniej niż 20 euro. Product manager chciałby mieć możliwość łatwego dodawania nowych kryteriów zniżki, bo spodziewa się ich dużo w przyszłości.
Na początku są to jednak następujące kryteria:
• data wylotu przypada w urodziny kupującego
• jest to lot do Afryki, odlatujący w czwartek
Przykład: 
Cena lotu wejściowa to 30 euro. Kupujący ma dziś urodziny i leci w czwartek do Afryki. Zastosowano obydwa kryteria więc cena końcowa wynosi 20 euro. Cena lotu to 21 euro i kupujący ma dziś urodziny. Nie można zastosować żadnego kryterium, ponieważ cena wyniosłaby poniżej 20 euro.

USE CASE specyficzny dla grupy A
• System powinien zapisywać jakie kryteria zniżek zostały zastosowane do każdego zakupu.
USE CASE specyficzny dla grupy B
• Systemowi NIE wolno zapisywać kryteriów zastosowanych do zakupu.

Interesuje nas przede wszystkim zamodelowanie i zaimplementowanie domeny, kod związany z infrastrukturą nie jest konieczny. W razie pytań do zadania służymy pomocą.
