using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.DashboardViewComponents;

public class _DashboardGroupReservationComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}