# SYSTEM OBECNOŚCI I URLOPÓW

Program służy do rejestrowania obecności pracowników, zarządzania wnioskami urlopowymi i odczytwyania historii obecności i urlopów.
Projekt wykonano w ramach zadania rekrutacyjnego.

# TECHNOLOGIE

Wykorzystano następujące technologie:
- Framework: .NET 10
- ORM: Entity Framework Core
- Baza danych: SQL Server
- Kopie zapasowe: GitHub

# BAZA DANYCH

Domyślny Connection String jest w pliku appsettings.json. Baza korzysta z mechanizmu migracji EF Core i posiada wbudowany Data Seed zawierający przykładowych pracowników.

# URUCHOMIENIE APLIKACJI

1. Wymagane są: zainstalowane środowisko .NET 10 SDK oraz SQL Server.
2. Pobierz pliki projektowe.
3. Sprawdź czy ścieżka w sekcji ConnectionString w pliku appsettings.json jest poprawna.
4a. Otwórz projekt w aplikacji Visual Studio, a następnie konsole menadżera pakietów i zaktualizuj bazę danych przy pomocy komendy: 'Update-Database'.
5a. Uruchom projekt w Visual Studio.
4b. Otwórz terminal i przejdź do folderu z projektem, następnie zaktualizuj bazę komendą: 'dotnet ef database update'.
5b. Będąc w folderze projektu odpal aplikację poleceniem dotnet run.

# ENDPOINTY

Projekt zawiera plik SystemUserAttendance.http który przy użyciu w środowiksu VisualStudio pozwala testować Endpointy. 

GET /api/employees - pobiera listę pracowników

POST /api/attendance/checkin - rejestracja wejścia pracownika

POST /api/attendance/checkout - rejestracja wyjścia prcownika

PUT /api/attendance/{id} - zmienia godzine wejścia danego wpisu

GET /api/attendance/{employeeId} - zwraca historię obecności danego pracownika

POST /api/leaves - składanie wniosku urlopowego

GET /api/leaves - lista wniosków urlopowych

PUT /api/leaves/{id}/approve - akceptacja wniosku

PUT /api/leaves/{id}/reject - odrzucenie wniosku

