# 🚚 Courier Service Console Application

A command-line application that calculates **delivery cost**, **discount**, and **delivery time** for courier packages based on weight, distance, valid offer codes, and vehicle scheduling.

The project follows:

- ✔ SOLID Principles  
- ✔ Clean Architecture (Presentation → Services → Domain)  
- ✔ Dependency Injection  
- ✔ Unit Testing with xUnit  

---

## 📦 Features

### Cost & Discount Calculation
- Base delivery cost
- Weight × distance calculation
- Offer-based discount
- Total payable amount

### Delivery Time Calculation
- Vehicle scheduling
- Shipment grouping based on weight capacity
- Delivery time based on speed & distance

### Offer Management
- Add new offer codes
- Remove existing offers
- List all active offers

### Console Enhancements
- Input validation
- Menu-driven workflow
- Ability to cancel package input
- Support for multiple runs without restarting app

---

## 🧰 Requirements

Install the following before running the app:

### ✅ 1. .NET SDK (7.0 or 8.0 recommended)
Download from:  
https://dotnet.microsoft.com/en-us/download/dotnet

Check installation:

```bash
dotnet --version
```
## ✅ 2. Required NuGet Packages for Main Application

The application requires **no mandatory external packages**, but uses:

### Framework features:
- `Microsoft.Extensions.DependencyInjection` (comes with .NET SDK)

If for any reason the DI package is missing, install:

```bash
dotnet add package Microsoft.Extensions.DependencyInjection
```

---

## ✅ 3. Required NuGet Packages for Test Project

To run unit tests, the following packages MUST be installed:

### ✔ Microsoft.NET.Test.Sdk  
```bash
dotnet add CourierServiceConsApp.Tests package Microsoft.NET.Test.Sdk
```

### ✔ xUnit  
```bash
dotnet add CourierServiceConsApp.Tests package xunit
```

### ✔ xUnit Runner (Visual Studio Integration)  
```bash
dotnet add CourierServiceConsApp.Tests package xunit.runner.visualstudio
```

### ✔ (Optional but recommended) Moq for mocking  
```bash
dotnet add CourierServiceConsApp.Tests package Moq
```

Your test project `.csproj` should contain:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.9.0" />
  <PackageReference Include="xunit" Version="2.5.3" />
  <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
  <PackageReference Include="Moq" Version="4.20.70" />
</ItemGroup>
```
---

## ▶️ How to Run the Application

### **Option 1: Run in Visual Studio**

1. Open the solution `.sln`
2. Right-click **CourierServiceConsApp**
3. Select **Set as Startup Project**
4. Press **F5** or **Ctrl + F5**

---

### **Option 2: Run from Terminal/Command Line**

Navigate to the main project folder:

```bash
cd CourierServiceConsApp
dotnet run
```

---

## 🧪 How to Run Unit Tests

### **Option 1: Using Visual Studio**

1. Open **Test Explorer**  
   `Test → Test Explorer`
2. Build the solution  
   `Ctrl + Shift + B`
3. Click **Run All**

You will see results like:

```
✔ CostCalculatorTests
✔ OfferServiceTests
✔ DeliverySchedulerTests
```

---

### **Option 2: Using Command Line**

From the solution root:

```bash
dotnet test
```

Example output:

```
Passed! 3 tests passed, 0 failed.
```

---

## 🗂 Test Project Structure

```
CourierServiceConsApp.Tests
│
├── CostCalculatorTests.cs
├── OfferServiceTests.cs
└── DeliverySchedulerTests.cs
```

---

## 🏛 Project Architecture

The project follows a simple Clean Architecture structure:

```
CourierServiceConsApp
│
├── Domain                 → Business entities (Package, Offer)
│
├── Services
│   ├── Interface          → Abstractions (SOLID: DIP)
│   └── Implementations    → Business logic (Cost, Offers, Scheduler)
│
├── Infrastructure         → Offer repository storage
│
├── Presentation           → Menu, InputParser, OutputFormatter, ConsoleApp
│
└── Program.cs             → Dependency Injection Setup
```

---

## 🧬 SOLID Principles Used

### S — Single Responsibility  
Each class has only one responsibility:  
CostCalculator calculates cost, InputParser parses input, OutputFormatter prints output, etc.

### O — Open/Closed  
New offers and rules can be added without modifying core logic (OfferService + Repository pattern).

### L — Liskov Substitution  
All services can be replaced by any class implementing their interface.

### I — Interface Segregation  
Small, focused interfaces like `ICostCalculator`, `IOfferService`, `IDeliveryScheduler`.

### D — Dependency Inversion  
High-level modules depend on abstractions, not concretes.  
(Using interfaces + DI in Program.cs)

---

## 📥 Sample Input

```
Enter base delivery cost: 100
Enter number of packages: 3

Package 1 ID: PKG1
Weight: 50
Distance: 30
Offer Code: OFR001

Package 2 ID: PKG2
Weight: 75
Distance: 125
Offer Code: OFR002

Package 3 ID: PKG3
Weight: 175
Distance: 100
Offer Code: OFR003

Enter number of vehicles: 2
Enter vehicle speed: 70
Enter max carriable weight: 200
```

---

## 📤 Sample Output

```
Package ID | Discount | Total Cost | Delivery Time (hours)
----------------------------------------------------------
PKG1       | 0        | 750        | 3.98
PKG2       | 0        | 1475       | 1.78
PKG3       | 0        | 2350       | 1.42
```

---

## ➕ Extending Offer Codes

To add more offers:

1. Add via the **Manage Offers** menu  
OR  
2. Add to the OfferRepository  
OR  
3. Add custom rules to OfferService  

This system is designed to be extensible using the Open/Closed Principle.

---

## 🗂 Recommended Git Commit History

```
feat: initial project setup with clean architecture
feat: implemented cost calculator and offer service
feat: added delivery scheduler and shipment selector
feat: added console menu, input parser, output formatter
test: added xUnit test project with unit tests
docs: added README with run & test instructions
refactor: applied SOLID improvements and cleanups
```

---

## 🎯 Final Notes

This project includes:

- Clear documentation  
- Reliable input validation  
- SOLID-compliant architecture  
- Unit testing  
- Extendable offer system  
- Professional Git commit structure  

Feel free to extend the project with new rules, UI upgrades, or better scheduling algorithms.

