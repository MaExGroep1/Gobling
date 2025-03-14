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

### 3. Einde van de Onderhandeling
- **Bod geaccepteerd:**
  - De PawningManager wisselt het item en het geld uit tussen koper en verkoper.
  - De satisfaction van de klant stijgt.
- **Bod geweigerd:**
  - De klant vertrekt met zijn item of de speler krijgt het item terug, afhankelijk van wie de koper is.
- Na elke biedronde daalt de satisfaction van de klant licht, tenzij de onderhandeling succesvol is afgerond.
