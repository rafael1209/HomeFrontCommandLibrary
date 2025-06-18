using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Interfaces;

namespace HomeFrontCommandLibrary.IntegrationTests;

public class Test
{
    public readonly IHomeFrontCommand HomeFrontCommand = new HomeFrontCommand(Language.Russian);

    [Fact]
    public async Task TestHistory()
    {
        var alertsHistory = await HomeFrontCommand.GetAlertsHistory();

        Assert.NotNull(alertsHistory);
    }

    [Fact]
    public async Task TestGetCurrent()
    {
        var activeAlert = await HomeFrontCommand.GetActiveAlert();

        Assert.NotNull(activeAlert);
    }
}