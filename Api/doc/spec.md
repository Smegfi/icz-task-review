# Technické zadání: Task Management REST API

## Cíl zadání
Cílem je vytvořit funkční RESTful API v prostředí **.NET 10** (nebo novější) pro správu úkolů (To-Do list). Projekt slouží k ověření vašich znalostí v oblasti návrhu API, práce s Entity Frameworkem, zabezpečení, validace a optimalizace pomocí cachování.

## Doménový model (Task)
Každý úkol v systému musí obsahovat minimálně následující vlastnosti:
* **Id:** int, *povinné* (primární klíč).
* **Nazev:** string, *povinné*, minimální délka 5 znaků.
* **Popis:** string, *nepovinné*.
* **Hotovo:** bool, *povinné*, výchozí hodnota false.

## Funkční požadavky

### 1. CRUD Operace
Implementujte standardní HTTP endpointy (MinimalAPI nebo Controllers):
* **GET** `/api/tasks` – Seznam všech úkolů (s podporou filtrování pomocí query parametrů).
* **GET** `/api/tasks/{id}` – Detail konkrétního úkolu.
* **POST** `/api/tasks` – Vytvoření nového úkolu.
* **PUT** `/api/tasks/{id}` – Aktualizace existujícího úkolu.
* **DELETE** `/api/tasks/{id}` – Odstranění úkolu.

### 2. Filtrování (Query Parameters)
Endpoint pro seznam úkolů musí umožňovat filtrování podle názvu:
* Příklad: `/api/tasks?name=hledany-text`.

V případě že nebude nalezený žádný výsledek bude vrácen prázdný list `[]`.

### 3. Datové úložiště (EF Core)
Použijte **Entity Framework Core**. Volba poskytovatele je na vás (InMemory, SQLite v souboru nebo SQL Server). Důležité je správné oddělení datové vrstvy.

### 4. Validace
Implementujte validaci vstupních dat (např. pomocí Data Annotations nebo FluentValidation) dle specifikace v doménovém modelu. API musí vracet relevantní chybové kódy (400 Bad Request) při nevalidním vstupu.


### 5. Autentifikace (API Key)
Zabezpečte všechny endpointy pomocí API klíče:
* Klíč musí být uložen v souboru `appsettings.json`.
* Klient musí zasílat klíč v HTTP hlavičce (např. `X-Api-Key`).
* Neautorizovaný přístup musí vrátit `401 Unauthorized`.

### 6. Hybrid Cache (.NET 9)
Využijte novou knihovnu **Microsoft.Extensions.Caching.Hybrid** pro optimalizaci GET požadavků:
* Nastavte expiraci cache na **2 minuty**.
* Nezapomeňte na správnou invalidaci cache při vytvoření, úpravě nebo smazání úkolu.

### 7. Logování
Implementujte strukturované logování pomocí rozhraní `ILogger`. Logujte klíčové akce na endpointu:
* „*Byl VYTVOŘEN nový úkol*“
* „*Byl UPRAVEN úkol číslo: {id}*“
* „*Byl SMAZÁN úkol číslo: {id}*“
* Využívejte parametrů v logovacích zprávách (Structured Logging), nikoliv jen interpolované stringy.

### 8. Dokumentace
Součástí řešení musí být plně funkční **Swagger (OpenAPI)** dokumentace, která umožní testování všech endpointů.

---

## Technické standardy a hodnocení
Při hodnocení budeme brát v úvahu:
* **Clean Code:** Čitelnost, pojmenovávání proměnných a dodržování C# konvencí.
* **Architektura:** Správné rozdělení zodpovědností (Separation of Concerns).
* **Správné použití HTTP status kódů** (200, 201, 204, 400, 401, 404).
* **Efektivita:** Jak pracujete s pamětí a výkonem (zejména v kontextu cache).

