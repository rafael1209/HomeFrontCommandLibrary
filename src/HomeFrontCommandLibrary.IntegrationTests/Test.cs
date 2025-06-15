using HomeFrontCommandLibrary.Interfaces;

namespace HomeFrontCommandLibrary.IntegrationTests;

public class Test
{
    public readonly IHomeFrontCommand HomeFrontCommand = new HomeFrontCommand();

    [Fact]
    public async void TestHistory()
    {
        var alertsHistory = await HomeFrontCommand.GetAlertsHistory();

        Assert.NotNull(alertsHistory);
    }

    [Fact]
    public async void TestGetCurrent()
    {
        var activeAlert = await HomeFrontCommand.GetActiveAlert();

        Assert.NotNull(activeAlert);
    }
}