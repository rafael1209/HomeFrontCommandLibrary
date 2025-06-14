using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Services;

namespace HomeFrontCommandLibrary.IntegrationTests;

public class Test
{
    public readonly IAlertService AlertService = new AlertService();

    [Fact]
    public async void TestHistory()
    {
        var alertsHistory = await AlertService.GetAlertsHistory();

        Assert.NotNull(alertsHistory);
    }

    [Fact]
    public async void TestGetCurrent()
    {
        var alert = await AlertService.GetCurrentAlert();

        Assert.NotNull(alert);
    }
}