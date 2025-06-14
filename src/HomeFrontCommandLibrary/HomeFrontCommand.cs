using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Services;

namespace HomeFrontCommandLibrary;

public class HomeFrontCommand(Language language = Language.Hebrew) : IHomeFrontCommand
{
    private IAlertService _alertService = new AlertService();

    public Language Language = language;

    public async Task GetActiveAlert()
    {

    }
}