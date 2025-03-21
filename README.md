# Proef proeve groep #1: Gobling

## Geproduceerde Game Onderdelen
### Tom Mulder:
[Sound System:](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Sound)
- [Sound Manager](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Sound/SoundManager.cs)
- [Sound Service](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Sound/SoundService.cs) 
- [Radio](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Sound/Radio.cs) 
- [ShowIf Inspector Element](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Sound/ShowIfInspector.cs) 
- [ShowIf Inspector Element](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Sound/ShowIfAttribute.cs) 

[Customer System:](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Customer)
- [Customer Behaviour](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Customer/CustomerBehaviour.cs)
- [Customer Manager](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Customer/CustomerManager.cs)

[Item Logic System](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Item)
- [Item Data Editor Inspector](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Item/ItemDataEditor.cs)
- [Items](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Item/Items.cs)

[Trading System](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Trading)

[User System](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/User)

### Brayden Bos:
[Currency Display System:](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/CurrencyDisplay)
- [Item Movement](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/CurrencyDisplay/CurrencyDisplay.cs)

[Item Logic System](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Item)
- [Item Data](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Item/ItemData.cs)
- [Item Data Editor Inspector](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Item/ItemDataEditor.cs)
- [Item Manager](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Item/ItemManager.cs)
- [Items](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Item/Items.cs)
- [LootTable](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Item/LootTable.cs)

[Customer System:](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Customer)
- [Customer Animations](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Customer/CustomerAnimations.cs)
- [Automatic Door](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Customer/CustomerAutomaticDoor.cs)
- [Customer Behaviour](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Customer/CustomerBehaviour.cs)
- [Customer Data](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Customer/CustomerData.cs)
- [Customer Manager](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Customer/CustomerManager.cs)

[Sound System:](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Sound)
- [Radio](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Sound/Radio.cs) 

[Enum Logic System](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Enums)

[Difficulty System](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Difficulty)

[Trading System](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Trading)

[User System](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/User)

[Day Loop System](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/DayLoop)

### Sten Kristel:
[Item Logic System:](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Item)
- [Item Movement](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Item/ItemMovement.cs)
- [Item Manager](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Item/ItemManager.cs)
- [Items](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Item/Items.cs)

[Customer System:](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Customer)
- [Customer Behaviour](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Customer/CustomerBehaviour.cs)
- [Customer Manager](https://github.com/stenkristel/ProefProeveGroep1/blob/main/Assets/Scripts/Customer/CustomerManager.cs)

[Trading System](https://github.com/stenkristel/ProefProeveGroep1/tree/main/Assets/Scripts/Trading)



### Sound System

Het originele idee was om een enkel script te maken die alles regelde dit werd uiteindelijk een slecht idee en het is opgesplits in drie scripts die allemaal kleine gedeeltes van het logica doen.


### Show If

Tijdens het maken van het Sound System wou Tom iets hebben om variables te verbergen.

### Customer System


### Item Logic System


### Enum Logic System


### Difficulty System


### Trading System


### User System


### Day Loop System


## Pawning system van Brayden Bos

Het pawning systeem bestaat uit drie hoofdcomponenten:

1. **PawningManager** – Bevat het pawning-algoritme en regelt het proces.
2. **Klantlogica** – Bepaalt het gedrag en de beslissingen van de klant.
3. **User Input** – De speler beïnvloedt het onderhandelingsproces.

## Het Proces van Pawning

### 1. Start van de Onderhandeling
- Een klant wil een item kopen of verkopen.
- De prijs wordt gebaseerd op het aantal items dat de klant en de speler bezitten.
- De klant bepaalt een offset op basis van zijn **greediness** (hebzucht) en **satisfaction** (tevredenheid).
- Deze offset wordt opgeteld of afgetrokken van de basiswaarde, afhankelijk van of de klant koopt of verkoopt.

### 2. Interactief Onderhandelen
- De PawningManager wacht op de input van de speler:
  - Doorgaan met onderhandelen.
  - Het bod accepteren.
  - De onderhandeling beëindigen.
- De beslissing van de klant wordt beïnvloed door zijn greediness en satisfaction.
- Als de onderhandeling doorgaat, doet de klant een tegenbod en herhaalt het proces zich.
- Na elke biedronde daalt de satisfaction van de klant licht, tenzij de onderhandeling succesvol is afgerond.

### 3. Einde van de Onderhandeling
- **Bod geaccepteerd:**
  - De PawningManager wisselt het item en het geld uit tussen koper en verkoper.
  - De satisfaction van de klant stijgt.
- **Bod geweigerd:**
  - De klant vertrekt met zijn item of de speler krijgt het item terug, afhankelijk van wie de koper is.
```mermaid
flowchart TD
    A["Customer"] -- Give bid --> C["Customer Buy"] & D["Customer Sell"]
    B["Starting Pawn"] --> C & D
    C --> E["Give Bid"]
    D --> E
    L["New Bid Round"] --> E
    F["User Input"] --> E
    E --> G["Customer Think"]
    G --> H["Accept Bid"] & I["Reject Bid"] & L
    M["Customer"] -- Give new bid --> L

    C@{ shape: hex}
    D@{ shape: hex}
    B@{ shape: hex}
    E@{ shape: hex}
    F@{ shape: rect}
    G@{ shape: diam}
    H@{ shape: trap-b}
    I@{ shape: trap-b}
    M@{ shape: rect}
    style A stroke:#000000,fill:#C8E6C9
    style C stroke:#000000,fill:#E1BEE7
    style D stroke:#000000,fill:#BBDEFB
    style B stroke:#000000,fill:#FFE0B2
    style E stroke:#000000,fill:#FFF9C4
    style L stroke:#000000,fill:#FFF9C4
    style F stroke:#000000,fill:#FFE0B2
    style G stroke:#000000,fill:#757575
    style H stroke:#000000,fill:#C8E6C9
    style I stroke:#000000,fill:#FFCDD2

    style M stroke:#000000,fill:#C8E6C9
```

## Item Movement system van Sten Kristel
Als er een item verkocht of gekocht wordt, dan moet de item verschijnen en naar de kassa gaan. 
Ik heb hier een generiek systeem voor geschreven waar je alleen de item en twee locaties nodig hebt en je dan de item naar de kassa kan laten springen.


https://github.com/user-attachments/assets/05b71338-c4fc-48ef-b981-a5888e3fa11f

Elke Item heeft een script genaamd ItemMovement, hierin staat de logica om te verschijnen en verdwijnen, de logica dat items van de ene locatie naar de andere springen, de logica die het verschijen/verdwijnen en springen combineerd en natuurlijk de logica dat het op de juiste momenten wordt geactiveerd.

### item vershijning
Voor het verschijnen en verdwijnen gebruik ik een leantween; dit zorgt ervoor dat de items op een natuurlijke manier groter of kleiner wordt. De soort leantween die ik gebruik is EaseInElastic/EaseOutElastic. Dit zorgt ervoor dat het een meer cartoony gevoel geeft
![ItemVerschijningCodeSnippet](https://github.com/user-attachments/assets/bb791d74-7b80-4338-b9af-100c86fe04e9)

### item jump
De spring functie heeft twee paramaters; de eindPositite en de startPositie. Als de startpositie (0, 0, 0) is, dan word de startpositie de positite waar de item al is.
De duur van het springen moet altijd hetzelfde zijn, hier zorg ik voor door de afstand te delen door een locale snelheid variable.
Het springen zelf wordt gedaan door twee LeanTween tegelijkertijd aan te roepen. Eentje word gedaan op het item zelf en over de x-as, deze beweegt dus het item horizontaal naar de locatie toe. De andere word gedaan op de parent van de item; over de y-as en deze word gereversed. Deze beweegt dus eerst omhoog en dan weer naar beneden. 

![ItemJumpCodeSnippit](https://github.com/user-attachments/assets/00337b0e-f7ce-498f-911b-2dbf91d53a82)

### verschijnen en springen
Om het springen en verschijnen ook nog te combineren gebruik ik een Coroutine. Eerst begint de item met verschijnen, dan wordt er heel erg kort gewacht en dan begint de sprong, tijdens de sprong groeit de item dus nog. Dit zorgt eroor dat het echt lijkt dat de item springt. En voor het springen en dan verdwijnen wordt precies het tegenovergestelde gebruikt
![ItemJumpCodeSnippit](https://github.com/user-attachments/assets/7282fdb9-4362-44c3-b818-685000441904)
