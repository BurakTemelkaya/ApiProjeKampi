using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.DashboardViewComponents;

public class _DashboardClaudeAIComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}