using System.Diagnostics;
using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models;

namespace HomeFrontCommandLibrary.IntegrationTests;

public class Test
{
    public readonly IHomeFrontCommand HomeFrontCommand = new HomeFrontCommand();

    [Fact]
    public async Task TestHistory()
    {
        var watch = new Stopwatch();

        var alertsHistory = await HomeFrontCommand.GetAlertsHistory(Language.Hebrew);

        var alertsInRussian = new List<AlertHistory>();
        foreach (var alert in alertsHistory)
        {
            alertsInRussian.Add(new AlertHistory()
            {
                AlertDate = alert.AlertDate,
                Category = await HomeFrontCommand.GetCategoryByName(alert.Category.Title, Language.Russian),
                City = await HomeFrontCommand.GetCityByName(alert.City.Name, Language.Russian)
            });
        }

        watch.Start();
        alertsHistory = await HomeFrontCommand.GetAlertsHistory(Language.Hebrew);

        alertsInRussian = new List<AlertHistory>();
        foreach (var alert in alertsHistory)
        {
            alertsInRussian.Add(new AlertHistory()
            {
                AlertDate = alert.AlertDate,
                Category = await HomeFrontCommand.GetCategoryByName(alert.Category.Title, Language.Russian),
                City = await HomeFrontCommand.GetCityByName(alert.City.Name, Language.Russian)
            });
        }
        watch.Stop();

        Assert.NotNull(alertsInRussian);
    }

    [Fact]
    public async Task TestGetCurrent()
    {
        var activeAlert = await HomeFrontCommand.GetActiveAlert();

        Assert.NotNull(activeAlert);
    }
}