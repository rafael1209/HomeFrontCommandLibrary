# HomeFrontCommandLibrary

📡 **A C# library for retrieving alerts from Israel's Home Front Command (פיקוד העורף)**
Provides access to **real-time rocket/siren alerts** and **alert history**, with multi-language support for city and category names.

---

## 📦 Installation

Via NuGet:

```bash
dotnet add package HomeFrontCommand
```

Or clone this repository and reference the project directly.

---

## ✅ Features

* Get the **current active alert**
* Fetch **historical alert data**
* **All responses include all languages** (Hebrew, Russian, English, Arabic) — no need to specify language
* Get all cached cities with `GetAllCities()`
* In-memory **caching support** for optimal performance

---

## 🚀 Quick Start

```csharp
using HomeFrontCommandLibrary;

class Program
{
    static async Task Main()
    {
        var command = new HomeFrontCommand();

        // Get current active alert
        var activeAlert = await command.GetActiveAlert();
        if (activeAlert.Category != null)
        {
            // Access category in any language
            Console.WriteLine($"Alert (Hebrew): {activeAlert.Category.Title.Hebrew}");
            Console.WriteLine($"Alert (Russian): {activeAlert.Category.Title.Russian}");
            Console.WriteLine($"Alert (English): {activeAlert.Category.Title.English}");

            // Access cities in any language
            foreach (var city in activeAlert.Cities!)
            {
                Console.WriteLine($" - {city.Name.Hebrew} / {city.Name.English}");
            }
        }

        // Get alert history
        var history = await command.GetAlertsHistory();
        foreach (var alert in history.Take(5))
        {
            Console.WriteLine($"{alert.AlertDate}: {alert.City.Name.Hebrew} - {alert.Category.Title.Russian}");
        }

        // Get all cached cities
        var allCities = command.GetAllCities();
        Console.WriteLine($"Total cities: {allCities.Count}");
    }
}
```

---

## 🧩 Models

### `Alert`

```csharp
public class Alert
{
    public Category? Category { get; set; }
    public List<City>? Cities { get; set; }
    public DateTime AlertDate { get; set; }
}
```

### `AlertHistory`

```csharp
public class AlertHistory
{
    public required Category Category { get; set; }
    public required City City { get; set; }
    public DateTime AlertDate { get; set; }
}
```

### `Category`

```csharp
public class Category
{
    public int Id { get; set; }
    public int MatrixId { get; set; }
    public required CategoryTitle Title { get; set; }
    public required CategoryDescription Description { get; set; }
}

public class CategoryTitle
{
    public string Hebrew { get; set; }
    public string English { get; set; }
    public string Russian { get; set; }
    public string Arabic { get; set; }
}

public class CategoryDescription
{
    public string Hebrew { get; set; }
    public string English { get; set; }
    public string Russian { get; set; }
    public string Arabic { get; set; }
}
```

### `City`

```csharp
public class City
{
    public int Id { get; set; }
    public int AreaId { get; set; }
    public required CityName Name { get; set; }
    public required ReshutName Reshut { get; set; }
    public string AreaName { get; set; }
    public int ProtectionTime { get; set; }
}

public class CityName
{
    public string Hebrew { get; set; }
    public string English { get; set; }
    public string Russian { get; set; }
    public string Arabic { get; set; }
}

public class ReshutName
{
    public string Hebrew { get; set; }
    public string English { get; set; }
    public string Russian { get; set; }
    public string Arabic { get; set; }
}
```

---

## 🌐 Multi-Language Support

All responses automatically include translations in all supported languages:

| Language | Property Access        |
| -------- | ---------------------- |
| Hebrew   | `.Hebrew`              |
| Russian  | `.Russian`             |
| English  | `.English`             |
| Arabic   | `.Arabic`              |

**Example:**
```csharp
var city = await command.GetCityByName("תל אביב");
Console.WriteLine(city.Name.Hebrew);   // תל אביב
Console.WriteLine(city.Name.English);  // Tel Aviv
Console.WriteLine(city.Name.Russian);  // Тель-Авив
Console.WriteLine(city.Name.Arabic);   // تل أبيب
```

---

## 🧠 Dependencies

* [`Newtonsoft.Json`](https://www.nuget.org/packages/Newtonsoft.Json) – for JSON serialization
* [`Microsoft.Extensions.Caching.Memory`](https://www.nuget.org/packages/Microsoft.Extensions.Caching.Memory) – in-memory caching

---

## 🛠 Roadmap

* [ ] Dependency Injection support
* [ ] Unit test coverage
* [ ] Redis or external cache support
* [ ] Additional data endpoints

---

## 📄 License

MIT © Rafael Chasman
