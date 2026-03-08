using System.Diagnostics;
using HomeFrontCommandLibrary.Interfaces;

namespace HomeFrontCommandLibrary.IntegrationTests;

public class Test
{
    public readonly IHomeFrontCommand HomeFrontCommand = new HomeFrontCommand();

    [Fact]
    public async Task TestHistory()
    {
        var watch = new Stopwatch();

        var alertsHistory = await HomeFrontCommand.GetAlertsHistory();

        Assert.NotNull(alertsHistory);
        Assert.All(alertsHistory, alert =>
        {
            Assert.NotNull(alert.Category);
            Assert.NotNull(alert.City);
            Assert.NotEmpty(alert.Category.Title.Hebrew);
            Assert.NotEmpty(alert.City.Name.Hebrew);
        });

        watch.Start();
        alertsHistory = await HomeFrontCommand.GetAlertsHistory();
        watch.Stop();

        Assert.NotNull(alertsHistory);
    }

    [Fact]
    public async Task TestGetCurrent()
    {
        var activeAlert = await HomeFrontCommand.GetActiveAlert();

        Assert.NotNull(activeAlert);
    }

    [Fact]
    public async Task TestActiveAlertExample()
    {
        var activeAlert = await HomeFrontCommand.GetActiveAlertExample("ניתן לצאת מהמרחב המוגן");

        Assert.NotNull(activeAlert);
        Assert.NotNull(activeAlert.Category);
        Assert.NotEmpty(activeAlert.Category.Title.Hebrew);
        Assert.NotEmpty(activeAlert.Category.Title.Russian);
        Assert.NotEmpty(activeAlert.Category.Title.English);
        Assert.NotEmpty(activeAlert.Category.Title.Arabic);
    }

    [Fact]
    public void TestGetAllCities()
    {
        var cities = HomeFrontCommand.GetAllCities();

        Assert.NotNull(cities);
    }
}