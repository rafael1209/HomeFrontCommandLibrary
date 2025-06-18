# HomeFrontCommandLibrary

📡 **A C# library for retrieving alerts from Israel's Home Front Command (פיקוד העורף)**
Provides access to **real-time rocket/siren alerts** and **alert history**, with multi-language support and city names.

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
* Automatically translated alert categories and cities names (Hebrew, Russian, English, Arabic)
* Resolve city names by alert
* In-memory **caching support** for optimal performance

---

## 🚀 Quick Start

```csharp
using HomeFrontCommandLibrary;
using HomeFrontCommandLibrary.Enums;

class Program
{
    static async Task Main()
    {
        var command = new HomeFrontCommand(Language.Russian);

        var activeAlert = await command.GetActiveAlert();
        Console.WriteLine($"Alert category: {activeAlert.Category.Title}");
        Console.WriteLine("Cities:");
        foreach (var city in activeAlert.Cities)
            Console.WriteLine($" - {city.Name}");

        var history = await command.GetAlertsHistory();
        Console.WriteLine($"Last alert was on: {history.First().AlertDate}");
    }
}
```

---

## 🧩 Models

### `Alert`

```csharp
public class Alert
{
    public Category Category { get; set; }
    public List<City> Cities { get; set; }
    public DateTime AlertDate { get; set; }
}
```

### `AlertHistory`

```csharp
public class AlertHistory
{
    public Category Category { get; set; }
    public City City { get; set; }
    public DateTime AlertDate { get; set; }
}
```

### `Category`

```csharp
public class Category
{
    public int Id { get; set; }
    public int MatrixId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
```

### `City`

```csharp
public class City
{
    public int AreaId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AreaName { get; set; } = string.Empty;
    public int ProtectionTime { get; set; }
}
```

---

## 🌐 Supported Languages

| Language | Enum Value         |
| -------- | ------------------ |
| Hebrew   | `Language.Hebrew`  |
| Russian  | `Language.Russian` |
| English  | `Language.English` |
| Arabic   | `Language.Arabic`  |

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
