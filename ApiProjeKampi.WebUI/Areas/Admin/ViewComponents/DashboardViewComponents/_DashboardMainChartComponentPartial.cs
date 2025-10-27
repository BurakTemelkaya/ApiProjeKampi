using ApiProjeKampi.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.ViewComponents.DashboardViewComponents;

public class _DashboardMainChartComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        RevenueChartViewModel vm = new()
        {
            Labels = ["Jan", "Feb", "Mar", "Apr", "May", "Jun"],
            Income = [5, 15, 14, 36, 32, 32],
            Expense = [7, 11, 30, 18, 25, 13],
        };

        return View(vm);
    }
}