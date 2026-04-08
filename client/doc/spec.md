# Technické zadání: Task Management Dashboard (Frontend)

## Cíl projektu
Cílem je vytvořit jednostránkovou aplikaci (SPA) v frameworku **React** nebo **Angular**, která bude sloužit jako klientské rozhraní pro správu úkolů. Aplikace bude komunikovat s REST API a demonstrovat vaše schopnosti v oblasti správy globálního stavu, autentifikace a tvorby responzivního UI.

## Funkční požadavky

### 1. Autentifikace a Zabezpečení
* **API Key Management:** Aplikace nebude obsahovat klasický login, ale vyžaduje zadání **API klíče** (např. v jednoduchém modálním okně nebo úvodní obrazovce).
* **Global State:** Klíč musí být uložen v globálním stavu (**React Context API / Zustand** nebo **Angular Service se Signály/BehaviorSubject**).
* **Podmíněné zobrazení:** Pokud klíč není v paměti přítomen, uživateli se zobrazí pouze výzva k zadání klíče. Po zadání se zpřístupní zbytek aplikace.

### 2. Správa úkolů (CRUD)
* **Tabulkové zobrazení:** Seznam úkolů načtený z API. Sloupce: Název, Popis (zkrácený), Stav (Hotovo/Nehotovo) a Akce.
* **Filtrování:** Textové pole pro vyhledávání (filtrování podle názvu přímo přes volání API s query parametrem `?name=...`).
* **Vytvoření úkolu:** Jednoduchý formulář (v modálu nebo na samostatné podstránce).
* **Smazání úkolu:** Tlačítko v tabulce s následným potvrzením.

### 3. Validace
* Implementujte klientskou validaci formuláře pro vytvoření úkolu:
    * **Název:** povinný, minimálně 5 znaků.
    * **Popis:** volitelný.
* Pozor, po odeslání formuláře může probíhat validace také na backendu aplikace, tuto validaci je také nutné zobrazit.

## Technické požadavky

### 1. Framework a Knihovny
* **React** nebo **Angular**.


* **Styling:** Doporučujeme jakoukoliv UI knihovnu (Bootstrap, Material UI) nebo například tailwind.
* **HTTP Klient:** fetch API nebo axios (v případě Angularu HttpClient).

### 2. Architektura
* **Komponentový přístup:** Rozdělení aplikace na logické celky (např. Layout, TaskTable, TaskForm, AuthGuard).
* **Intercery/Middleware:** Implementujte mechanismus, který automaticky přidá hlavičku X-Api-Key ke každému odchozímu požadavku na API.

### 3. UX a UI
* **Loading states:** Zobrazení indikátoru načítání (spinner/skeleton) při čekání na data z API.
* **Error handling:** Uživatelsky přívětivé zobrazení chyby, pokud API vrátí např. 401 (neplatný klíč) nebo 500.
* **Responzivita:** Aplikace musí by měla být použitelná na desktopu i mobilním zařízení.

## Hodnocená kritéria

* **Správa stavu:** Jak efektivně pracujete s globálním kontextem a jak řešíte synchronizaci dat po smazání/přidání úkolu.
* **Čistota kódu:** Dodržování konvencí daného frameworku, čitelnost a modularita.
* **Práce s API:** Správné ošetření asynchronních operací (async/await, observables).
* **Typování:** Použití **TypeScriptu** a správná definice rozhraní (Interfaces/Types) pro modely.